using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ZenCNC.STEAM.grbl {
    /// <summary>
    /// Main Grbl client class
    /// </summary>
    public class GrblClient {

        public static string grblVersion = "0.9j";
        public SerialPort grblPort;
        public PortDesc portDesc;

        private static Hashtable errorHash;
        private static Hashtable alarmHash;

        private GCodeFile gcodeFile = new GCodeFile();
        private readonly static object lockObject = new object();
        public bool IsGCodeLoaded = false;

        private bool IsPause = false;
        private bool doQuery = true;
        public bool IsConnected = false;

        private Queue gcodeQueue = new Queue();
        private string lastLineSent = string.Empty;

        private SortedDictionary<string, string> parameterHash = new SortedDictionary<string, string>();
        private Queue<byte> receivedData = new Queue<byte>();

        private float _jogFeedrate = 300;
        public GrblGCodeStatusEventArgs gCodeArgs = new GrblGCodeStatusEventArgs();
        public GrblResponseEventArgs respArgs = new GrblResponseEventArgs();

        private int QUERY_INTERVAL = 1000;
        private char interruptChar;
        static int FAST_INTERVAL = 100;
        static int SLOW_INTERVAL = 1000;

        private Thread queryThread = null;
        private DelayedCommand DelayedCommand = null;

        private void ExecuteDelayedCommand() {
            if (DelayedCommand != null && DelayedCommand.IsTimeExpried()) {
                Send(DelayedCommand.Command);
                DelayedCommand = null;
            }
        }

        public void SetFastInterval() {
            QUERY_INTERVAL = FAST_INTERVAL;
        }
        public void SetSlowInterval() {
            QUERY_INTERVAL = SLOW_INTERVAL;
        }

        public float JogFeedRate {
            get {
                return _jogFeedrate;
            }
            set {
                _jogFeedrate = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public GrblClient() {
            XmlDocument xmlDoc = new XmlDocument();
            string[] resources = GetType().Assembly.GetManifestResourceNames();
            string resource = string.Empty;
            foreach (var res in resources) {
                if (res.ToUpper().IndexOf(grblVersion.ToUpper()) >= 0) {
                    resource = res;
                    break;
                }
            }

            if (resource.Length > 0) {
                using (Stream stream = GetType().Assembly.
                    GetManifestResourceStream(resource)) {
                    using (StreamReader sr = new StreamReader(stream)) {
                        xmlDoc.LoadXml(sr.ReadToEnd());
                    }
                }
            } else {
                throw new Exception("No GRBL Definition File Found");
            }

            errorHash = new Hashtable();
            foreach (XmlNode errorNode in xmlDoc.SelectNodes("/grbl/Errors/Error")) {
                string code = errorNode.Attributes["code"].Value;
                string desc = errorNode.InnerText;
                errorHash.Add(code, desc);
            }
            alarmHash = new Hashtable();
            foreach (XmlNode errorNode in xmlDoc.SelectNodes("/grbl/Alarms/Alarm")) {
                string code = errorNode.Attributes["code"].Value;
                string desc = errorNode.InnerText;
                alarmHash.Add(code, desc);
            }
        }

        /// <summary>
        /// Load a gcode file
        /// </summary>
        /// <param name="fileName">GCode file path</param>
        public void OpenGCode(string fileName) {
            gcodeFile.Clear();
            gcodeFile.OpenFileForContinuousReading(fileName);
            IsGCodeLoaded = true;

            gCodeArgs.TotalLines = gcodeFile.TotalLines;
            gCodeArgs.CurrentLine = 0;
            gCodeArgs.FileStatus = FileStatus.Loaded;
            GCodeStatusUpdate(this, gCodeArgs);
        }

        /// <summary>
        /// Start to run a GCode file, if the file has been loaded
        /// </summary>
        public void StartFile() {
            if (gcodeFile == null)
                return;
            gCodeArgs.FileStatus = FileStatus.Running;

            GCodeLine startLn = gcodeFile.ReadNextGCodeLine();
            gcodeFile.CurrentLine++;
            if (startLn == null) {
                string filePath = gcodeFile.FilePath;
                OpenGCode(filePath);
                startLn = gcodeFile.ReadNextGCodeLine();
            }
            if (startLn.IsSupported) {
                Send(startLn.inputLine);
            } else {
                CMD_QUERY();
            }

        }

        /// <summary>
        /// Get all arduino ports
        /// </summary>
        /// <returns></returns>
        public static List<PortDesc> GetSerialPorts() {
            List<PortDesc> result = new List<PortDesc>();
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'")) {
                string[] portnames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();

                foreach (var port in ports) {
                    string caption = port["Caption"].ToString();
                    int idx1 = caption.IndexOf("(");
                    int idx2 = caption.LastIndexOf(")");
                    int len = idx2 - idx1 - 1;

                    string portName = caption.Substring(idx1 + 1, len);
                    if (port["Caption"].ToString().ToUpper().Contains("ARDUINO")
                        || port["Caption"].ToString().ToUpper().Contains("CH340")) {
                        foreach (string testName in portnames) {
                            if (testName.ToUpper().Equals(portName)) {
                                result.Add(new PortDesc() { DeviceId = portName, Caption = port["Caption"].ToString() });
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Close Serial Port
        /// </summary>
        public void Close() {
            if (grblPort != null && grblPort.IsOpen) {
                if (queryThread != null && queryThread.IsAlive)
                    queryThread.Abort();
                grblPort.Close();
                grblPort.Dispose();
            }
            portDesc = null;
            IsConnected = false;
        }
        /// <summary>
        /// Open grbl port
        /// </summary>
        /// <param name="portDesc">port description object</param>
        /// <returns></returns>
        public bool Open(PortDesc portDesc) {
            IsConnected = false;
            GrblParameterBase.Init();

            grblPort = new SerialPort(portDesc.DeviceId, 115200, Parity.None, 8, StopBits.One);
            grblPort.DataReceived += GrblPort_DataReceived;
            grblPort.ErrorReceived += GrblPort_ErrorReceived;

            if (!grblPort.IsOpen) {
                try {
                    grblPort.Open();
                    this.portDesc = portDesc;

                    Thread.Sleep(1000);
                    CMD_RESET();
                    Thread.Sleep(1000);
                    CMD_QUERY();
                } catch (Exception e) { }
            }

            if (grblPort.IsOpen) {
                PortStateChangeArg arg = new PortStateChangeArg();
                arg.State = "Connected";
                OnPortEvent(arg);

                CMD_GETOFFSET();
                queryThread = new Thread(delegate () {
                    StartQueryLoop();
                });
                queryThread.Start();
                IsConnected = true;
            }

            return IsConnected;
        }

        private void GrblPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e) {
            OnError(new GrblErrorEventArgs() { Code = "38", Desc = GetErrorDesc("38"), Line = lastLineSent });
        }

        /// <summary>
        /// Event handler for grbl port response
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrblPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            byte[] data = new byte[grblPort.BytesToRead];
            int readCnt = grblPort.Read(data, 0, data.Length);
            data.ToList().ForEach(b => receivedData.Enqueue(b));
            ProcessData();
        }

        /// <summary>
        /// Process grbl response data
        /// </summary>
        private void ProcessData() {
            int currentStart = 0;
            byte[] bytes = receivedData.ToArray();
            int count = 0;

            for (int i = 0; i < bytes.Length; i++) {
                if (bytes[i] == '\n') {
                    if (i == 0) { // Newline at beginning of line is left over from previous line
                        count++;
                        continue;
                    }

                    count += i - currentStart + 1;
                    string response = Encoding.UTF8.GetString(bytes, currentStart, i - currentStart);
                    currentStart = i + 1;
                    response = response.Trim();
                    
                    if (response.Length > 0) {
                        respArgs.Response = response;
                        if (response.Trim().ToUpper().Equals("OK")) {
                            ExecuteQueue();
                            if (IsGCodeLoaded) {
                                gcodeFile.CommandOK();
                                SendNextLineNew();
                            }
                        } else if (response.ToUpper().StartsWith("ERROR")) {
                            string[] errFlds = response.Split(':');
                            OnError(new GrblErrorEventArgs() { Code = errFlds[1], Desc = GetErrorDesc(errFlds[1]), Line = lastLineSent });
                        } else if (response.ToUpper().StartsWith("ALARM")) {
                            string[] errFlds = response.Split(':');
                            OnAlarm(new GrblErrorEventArgs() { Code = errFlds[1], Desc = GetAlarmDesc(errFlds[1]), Line = lastLineSent });
                        }

                        if (respArgs.State == MachineState.RUN || respArgs.State == MachineState.JOG)
                            QUERY_INTERVAL = 100;
                        else
                            QUERY_INTERVAL = 100;
                        OnResponseReceived(respArgs);
                    }
                }
            }

            for (int j = 0; j < count; j++)
                receivedData.Dequeue();
        }

        #region "Parameters"

        /// <summary>
        /// Get all grbl parameters
        /// </summary>
        /// <returns>List of grbl parameter object</returns>
        public List<GrblParameterBase> GetParameters() {
            List<GrblParameterBase> result = new List<GrblParameterBase>();
            foreach (var entry in parameterHash) {
                string id = entry.Key.ToString();
                string val = entry.Value.ToString();
                GrblParameterBase param = new GrblParameterBase(id, val);
                result.Add(param);
            }

            return result.OrderBy(p => Convert.ToInt16(p.ID)).ToList();
        }

        /// <summary>
        /// Process Grbl parameter string
        /// </summary>
        /// <param name="parameterStr"></param>
        public void ProcessParameter(string parameterStr) {
            parameterStr = parameterStr.Substring(1);
            string[] flds = parameterStr.Split('=');
            SetParameter(flds[0].Trim(), flds[1].Trim());
            if (GrblParameterBase.IsLastParameter(flds[0])) {
                GrblResponseEventArgs args = new GrblResponseEventArgs() { EventType = "ParameterUpdate" };
                OnResponseReceived(args);
            }
        }

        /// <summary>
        /// Checks if a string is a Grbl parameter string
        /// </summary>
        /// <param name="str"></param>
        public bool isParameterString(string str) {
            return str.StartsWith("$") && str.Contains("=") && str[1] != 'N';
        }

        /// <summary>
        /// Update a Grbl parameter
        /// </summary>
        /// <param name="id">Grbl parameter id</param>
        /// <param name="val">Value</param>
        public void UpdateParameter(string id, string val) {
            Send("$" + id + "=" + val);
            Thread.Sleep(500);
            CMD_QUERYPARAMS();
        }

        /// <summary>
        /// Set in memory grbl parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="val"></param>
        private void SetParameter(string id, string val) {
            if (!parameterHash.ContainsKey(id)) {
                parameterHash.Add(id, val);
            } else {
                parameterHash[id] = val;
            }
        }

        #endregion

        #region "Misc."

        /// <summary>
        /// Get Error description by grbl error code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetErrorDesc(string code) {
            return (string)errorHash[code];
        }

        /// <summary>
        /// Get Alarm description by grbl alarm code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetAlarmDesc(string code) {
            return (string)alarmHash[code];
        }

        /// <summary>
        /// Execute command in the queue
        /// </summary>
        private void ExecuteQueue() {
            if (gcodeQueue.Count > 0) {
                Send((string)gcodeQueue.Dequeue());
            }
        }

        #endregion

        #region "Send"

        /// <summary>
        /// Get next line from GCode file and send it to grbl port
        /// </summary>
        private void SendNextLineNew() {
            if (IsPause)
                return;

            GCodeLine gcodeline = gcodeFile.ReadNextGCodeLine();
            if (gcodeline != null) {
                Send(gcodeline.inputLine);
                gCodeArgs.gcode = gcodeline.inputLine;
                gCodeArgs.CurrentLine = gcodeFile.CurrentLineNum;
                gCodeArgs.TotalLines = gcodeFile.TotalLines;
                GCodeStatusUpdate(this, gCodeArgs);
            } else {
                gCodeArgs.FileStatus = FileStatus.Stopped;
                GCodeStatusUpdate(this, gCodeArgs);
            }
        }

        /// <summary>
        /// Send a string command
        /// </summary>
        /// <param name="cmd"></param>
        public void Send(string cmd) {
            cmd = cmd.Trim() + "\n";
            if (cmd.Trim().Length == 0 && !cmd.Equals("\n"))
                return;
            char[] charArray = cmd.ToCharArray();
            Send(charArray);
            lastLineSent = cmd.Trim();
            QUERY_INTERVAL = 200;
        }

        /// <summary>
        /// Send a char array command to grbl port
        /// </summary>
        /// <param name="bytes"></param>
        public void Send(char[] bytes) {
            if (grblPort != null && grblPort.IsOpen) {
                lock (lockObject) {
                    try {
                        grblPort.Write(bytes, 0, bytes.Length);
                    } catch (Exception e) {
                        PortStateChangeArg arg = new PortStateChangeArg();
                        arg.State = "Disconnected";
                        OnPortEvent(arg);
                    }
                }
            } else {
                PortStateChangeArg arg = new PortStateChangeArg();
                arg.State = "Disconnected";
                OnPortEvent(arg);
            }
        }

        public void SendByte(int byteInt) {
            byte b = Convert.ToByte(byteInt);
            byte[] barray = { b };
            grblPort.Write(barray, 0, barray.Length);
        }

        /// <summary>
        /// Send a char command
        /// </summary>
        /// <param name="c"></param>
        public void SendChar(char c) {
            char[] array = { c };
            Send(array);
        }

        /// <summary>
        /// Start a query loop, until grbl port closed
        /// </summary>
        private void StartQueryLoop() {
            while (doQuery && grblPort.IsOpen) {
                if (interruptChar != '0') {
                    SendChar(interruptChar);
                    interruptChar = '0';
                }
                ExecuteDelayedCommand();
                CMD_QUERY();
                Thread.Sleep(QUERY_INTERVAL);
            }
        }

        #endregion

        #region "Commands"

        /// <summary>
        /// Send parameter query command
        /// </summary>
        public void CMD_QUERYPARAMS() {
            Send("$$");
        }

        /// <summary>
        /// Send get offset command
        /// </summary>
        public void CMD_GETOFFSET() {
            Send("$#");
        }

        /// <summary>
        /// Send soft reset command
        /// </summary>
        public void CMD_RESET() {
            char ch = (char)short.Parse("18", System.Globalization.NumberStyles.AllowHexSpecifier);
            char[] charray = new char[1];
            charray[0] = ch;
            Send(charray);
            IsGCodeLoaded = false;
            if (gcodeFile != null) {
                gcodeFile.Close();
            }

        }
        public void CMD_HOME() {
            Send("$H");
        }
        public void CMD_ZERO(string axis) {
            Send("G92 " + axis.ToUpper() + "0");
        }

        public void CMD_ZEROALL() {
            Send("G92 X0 Y0 Z0");
        }
        public void CMD_UNLOCK() {
            Send("$X");
        }

        public void CMD_PROBE(float x, float y) {
            Send($"G38.2 X{x.ToString("0.000")} Y{y.ToString("0.000")} Z-100 F100");
        }

        /// <summary>
        /// Combination Communication Function - Query
        /// </summary>
        public void CMD_QUERY() {
            if (grblPort != null)
                SendChar('?');
        }

        /// <summary>
        /// Stop Jogging
        /// </summary>
        public void CMD_JC() {
            SendByte(Convert.ToByte('\x0085'));
        }

        /// <summary>
        /// Start Jogging
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="isPositive"></param>
        public void CMD_JOG(string axis, bool isPositive) {
            SetFastInterval();
            string feed = "F" + JogFeedRate.ToString("0");
            string dist = $"{(isPositive ? "" : "-")}10";
            string line = $"G91 G21 {axis}{dist} {feed}";

            if (Convert.ToDouble(grblVersion.Substring(0, 3)) >= 1.1) {
                Send($"$J={line}");
            } else {
                Send(line);
            }
        }
        public bool IsRelative { get; set; }

        #endregion

        #region "Events"

        /// <summary>
        /// COM Port Event
        /// </summary>
        public event EventHandler PortEvent;
        protected virtual void OnPortEvent(PortStateChangeArg e) {
            EventHandler handler = PortEvent;
            if (handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// Grbl Error Event
        /// </summary>
        public event EventHandler ErrorEvent;
        protected virtual void OnError(GrblErrorEventArgs e) {
            EventHandler handler = ErrorEvent;
            if (handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// Grbl Alarm Event
        /// </summary>
        public event EventHandler AlarmEvent;
        protected virtual void OnAlarm(GrblErrorEventArgs e) {
            EventHandler handler = AlarmEvent;
            if (handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// Grbl Response Event
        /// </summary>
        public event EventHandler ResponseReceived;
        protected virtual void OnResponseReceived(EventArgs e) {
            EventHandler handler = ResponseReceived;
            if (handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// GCode Execution status update event
        /// </summary>
        public event EventHandler GCodeStatusUpdate;
        protected virtual void OnGCodeStatusUpdate(EventArgs e) {
            EventHandler handler = GCodeStatusUpdate;
            if (handler != null) {
                handler(this, e);
            }
        }

        #endregion

    }
}
