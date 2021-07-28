using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl {
    public class GCodeFile {

        private string _filename;
        private FileStream stream;
        private StreamReader stream_in = null;

        private Queue<int> _counter = new Queue<int>();
        private int queueTotal = 0;
        private int MAX_BUFFER = 127;
        private long fileLength = 0;

        private bool isEnd = false;
        private List<string> lines;
        private int currentPosition = 0;

        public List<GCodeLine> gcodeLines;
        public int CurrentLineNum { get; set; }
        public static object lockNextLine = new object();
        public GCodeFileStatusEnum Status { get; set; }

        /// <summary>
        /// Reset GCodeFile status, and clear all lines in memory
        /// </summary>
        public void Clear() {
            if (stream != null)
                stream.Close();

            _counter = new Queue<int>();
            queueTotal = 0;
            isEnd = false;
            Status = GCodeFileStatusEnum.NoFile;
            currentPosition = 0;
            CurrentLine = 0;
            CurrentLineNum = 0;
            lines = null;
        }

        /// <summary>
        /// GCode File Name
        /// </summary>
        public string FileName {
            get {
                return _filename;
            }
        }

        /// <summary>
        /// Total lines of the GCode File
        /// </summary>
        public int TotalLines { get; set; }

        /// <summary>
        /// Current executing line
        /// </summary>
        public int CurrentLine { get; set; }


        public string FilePath { get; set; }

        public void CommandOK() {
            if (_counter.Count > 0) 
                queueTotal -= _counter.Dequeue();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public GCodeFile() {
            _filename = string.Empty;
            TotalLines = -1;
            CurrentLine = -1;
            Status = GCodeFileStatusEnum.NoFile;
        }

        public void AddGCodeLine(string ln) {
            if (gcodeLines == null)
                gcodeLines = new List<GCodeLine>();
            ln = ln.Trim();

            if (gcodeLines.Count == 0) {
                GCodeLine newLn = new GCodeLine(ln.Trim());
                newLn.SetInitialPosition();
                gcodeLines.Add(newLn);
            } else {
                GCodeLine prevLn = gcodeLines[gcodeLines.Count - 1];
                GCodeLine newLn = new GCodeLine(ln.Trim(), prevLn);
                gcodeLines.Add(newLn);
            }
        }

        GCodeLine curLine = null;

        public void OpenFileForContinuousReading(string filename) {
            if (filename == null || filename.Length == 0) {
                return;
            }
            stream_in = new StreamReader(filename);
            using (StreamReader sr = new StreamReader(filename)) {
                string content = sr.ReadToEnd();
                string[] lns = content.Split('\n');
                TotalLines = lns.Length;
                CurrentLineNum = 0;
            }

            FilePath = filename;
            Status = GCodeFileStatusEnum.Loaded;
        }

        public GCodeLine ReadNextGCodeLine() {
            GCodeLine gcodeLn = null;
            string ln = stream_in.ReadLine();
            if (ln == null) {
                CurrentLineNum++;
            } else {
                gcodeLn = new GCodeLine(ln);
                CurrentLineNum++;
            }
            return gcodeLn;
        }

        /// <summary>
        /// Open the gcode file
        /// </summary>
        /// <param name="filename"></param>
        public void OpenFile(string filename) {
            _filename = filename;
            if (File.Exists(filename)) {
                gcodeLines = new List<GCodeLine>();
                using (StreamReader sr = new StreamReader(_filename)) {
                    string content = sr.ReadToEnd();
                    string[] lns = content.Split('\n');
                    lines = new List<string>();
                    foreach (string ln in lns) {
                        AddGCodeLine(ln);
                        lines.Add(ln.Trim());
                    }
                    TotalLines = lns.Length;
                    CurrentLineNum = 0;
                }

                curLine = gcodeLines[0];
                Status = GCodeFileStatusEnum.Loaded;
            } else {
                Status = GCodeFileStatusEnum.Error;
            }
            ResetSeek();
        }


        public GCodeLine GetStartLine() {
            GCodeLine line = curLine;
            while (!line.IsSupported) {
                line = line.next;
            }
            curLine = line;
            return curLine;
        }

        /// <summary>
        /// For continious reading, set the seek position to begining
        /// </summary>
        public void ResetSeek() {
            currentPosition = 0;
            if (stream != null)
                stream.Seek(currentPosition, SeekOrigin.Begin);
            isEnd = false;
            queueTotal = 0;
        }

        public GCodeLine NextSupportedLine() {
            GCodeLine ln = curLine.next;
            while (ln != null && !ln.IsSupported) {
                ln = ln.next;
            }
            curLine = ln;
            return curLine;
        }

        // Get next line from the gcode file stream
        public string NextLine() {
            if (isEnd) return "EOF";

            lock (lockNextLine) {
                if (CurrentLineNum < TotalLines) {
                    string retLine = lines[CurrentLineNum];
                    CurrentLineNum++;
                    return retLine;
                } else {
                    isEnd = true;
                    return "EOF";
                }
            }

            int readCnt = 0;
            string result = string.Empty;
            byte[] b = new byte[100];
            int charRead = stream.Read(b, 0, b.Length);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < charRead; i++) {
                if (currentPosition + readCnt >= fileLength) { // EOF reached
                    isEnd = true;
                    result = sb.ToString().Trim();
                    sb.Clear();
                    break;
                }

                char c = Convert.ToChar(b[i]);
                if (c == '\n' || c == '\r') {
                    if (c == '\r' && i < b.Length) {
                        char cn = Convert.ToChar(b[i + 1]);
                        if (cn == '\n') {
                            readCnt++;
                        }
                    }

                    result = sb.ToString().Trim();
                    sb.Clear();
                    readCnt++;
                    break;
                } else {
                    sb.Append(c);
                    readCnt++;
                }
            }


            // If the total buffer size less than grbl MAX buffer, the line will be sent, set seek position
            if (queueTotal + result.Length <= MAX_BUFFER) {
                currentPosition += readCnt;
                queueTotal += result.Length + 1;

                stream.Seek(currentPosition, SeekOrigin.Begin);
                _counter.Enqueue(result.Length + 1);
            } else { // If the total buffer size reached the limit, the current line will be ignored and return empty string, grbl will not send empty line
                stream.Seek(currentPosition, SeekOrigin.Begin);
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// Close the file stream
        /// </summary>
        public void Close() {
            if (stream != null)
                stream.Close();

            _counter.Clear();
            queueTotal = 0;
        }

    }

    //GCode file status ENUM
    public enum GCodeFileStatusEnum {
        NoFile,
        Loaded,
        Runing,
        Paused,
        Stopped,
        Error
    }
}
