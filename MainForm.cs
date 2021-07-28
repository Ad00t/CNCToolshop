using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using ZenCNC.STEAM.grbl;
using Newtonsoft.Json;
using System.Drawing;
using Microsoft.AppCenter.Crashes;

namespace CNCToolshop {
    public partial class MainForm : MetroForm {

        public GrblClient grbl = new GrblClient();
        public Config config;
        public MainVisualizer v;

        public TextToGcode textToGcode;
        public ShapeToGcode shapeToGcode;

        public string loadedFile = "loaded.nc";
        public string configFile = @"config\config.json";

        public List<double> wPos = new List<double>(3) { 0, 0, 0 };
        public List<double> mPos = new List<double>(3) { 0, 0, 0 };

        public bool isDebug = false;
        public bool isSpindleSpinning;

        public MainForm() {
            InitializeComponent();
            checkLastCrash();
            Text = $"CNC Toolshop {System.Configuration.ConfigurationManager.AppSettings["version"]}";
            DoubleBuffered = true;

            initConfig();
            loadPorts();

            grbl.ResponseReceived += Grbl_ResponseReceived;
            grbl.ErrorEvent += Grbl_ErrorEvent;
            grbl.AlarmEvent += Grbl_AlarmEvent;
            grbl.GCodeStatusUpdate += Grbl_GCodeStatusUpdate;
            USBUtil.USBPortChangeEvent += USBUtil_USBPortChangeEvent;
            USBUtil.init();

            v = new MainVisualizer(this, pnl_visualizer);
            v.refresh();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            File.WriteAllText(loadedFile, string.Empty);
        }
        private async void checkLastCrash() {
            if (await Crashes.HasCrashedInLastSessionAsync()) {
                ErrorReport crashReport = await Crashes.GetLastSessionCrashReportAsync();
                MessageBox.Show($"Looks like the last session crashed.\nStackTrace:{crashReport.StackTrace}");
            }
        }

        private void loadPorts() {
            cmb_port.Items.Clear();
            foreach (PortDesc port in GrblClient.GetSerialPorts()) {
                cmb_port.Items.Add(port.DeviceId);
            }
        }
        private void initConfig() {
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile));
            tb_waLength.Text = Convert.ToString(config.workArea.mags[0]);
            tb_waWidth.Text = Convert.ToString(config.workArea.mags[1]);
            cmb_waUnits.SelectedIndex = config.workArea.unitsIndex;
            tb_feedRate.Text = Convert.ToString(config.feedRate.mags[0]);
            cmb_feedRateUnits.SelectedIndex = config.feedRate.unitsIndex;
            tb_spindleSpeed.Text = Convert.ToString(config.spindleSpeed.mags[0]);
            cmb_spindleSpeedUnits.SelectedIndex = config.spindleSpeed.unitsIndex;
        }

        private void initGrbl(string port) {
            try {
                grbl.Close();
                grbl.Open(GrblClient.GetSerialPorts().Find(x => x.DeviceId.Equals(port)));
                if (grbl.IsConnected)
                    MessageBox.Show($"GRBL connected on port {port}.");
            } catch (Exception e) {
                MessageBox.Show($"GRBL setup/connection failed (likely port not found).\nConnected: {grbl.IsConnected}\nPort: {port}\nMessage: {e.Message}\nStacktrace:{e.StackTrace}");
            }
        }
        private bool verifyGrbl() {
            if (isDebug || (grbl != null && grbl.IsConnected)) {
                return true;
            } else {
                MessageBox.Show("GRBL connection isn't active.\nPlease select a port to connect.");
                return false;
            }
        }
        private void cmb_port_SelectedIndexChanged(object sender, EventArgs e) {
            if (!cmb_port.Text.Equals(string.Empty))
                initGrbl(cmb_port.Text);
        }
        private void tile_refreshPorts_Click(object sender, EventArgs e) {
            loadPorts();
        }

        private void Grbl_ErrorEvent(object sender, EventArgs e) {
            GrblErrorEventArgs args = (GrblErrorEventArgs)e;
            tb_consoleLog.Text += $"GRBL: error - {args} (Line: {args.Line})\n";
        }
        private void Grbl_AlarmEvent(object sender, EventArgs e) {
            GrblErrorEventArgs args = (GrblErrorEventArgs)e;
            tb_consoleLog.Text += $"GRBL: alarm - {args} (Line: {args.Line})\n";
        }
        private void Grbl_ResponseReceived(object sender, EventArgs e) {
            GrblResponseEventArgs args = (GrblResponseEventArgs)e;
            if (!args.Response.StartsWith("<"))
                tb_consoleLog.Text += $"GRBL: {args.Response}\n";
            lbl_status.Text = $"Status: {args.State}";

            wPos[0] = args.WPos.X; mPos[0] = args.MPos.X;
            wPos[1] = args.WPos.Y; mPos[1] = args.MPos.Y;
            wPos[2] = args.WPos.Z; mPos[2] = args.MPos.Z;
            lbl_wX.Text = $"wX: {wPos[0]}"; lbl_mX.Text = $"mX: {mPos[0]}";
            lbl_wY.Text = $"wY: {wPos[1]}"; lbl_mY.Text = $"mY: {mPos[1]}";
            lbl_wZ.Text = $"wZ: {wPos[2]}"; lbl_mZ.Text = $"mZ: {mPos[2]}";
            v.refresh();
        }
        private void Grbl_GCodeStatusUpdate(object sender, EventArgs e) {
            GrblGCodeStatusEventArgs args = (GrblGCodeStatusEventArgs)e;
            lbl_status.Text = $"Status: {args.FileStatus}";
            tb_consoleLog.Text += $"GRBL: gcode - {args.gcode} (Line: {args.CurrentLine} / {args.TotalLines})\n";
        }
        private void USBUtil_USBPortChangeEvent(object sender, EventArgs e) {
            PortStateChangeArg args = (PortStateChangeArg)e;
            grbl.Close();
            loadPorts();
            MessageBox.Show("GRBL connection closed.\nPlease select a port.");
        }

        private void pnl_visualizer_Paint(object sender, PaintEventArgs e) {
            e.Graphics.DrawImage(v.buffer, new Point(0, 0));
        }
        private void btn_clearVisualizer_Click(object sender, EventArgs e) {
            v.refresh();
        }
        private void enterConsole() {
            if (verifyGrbl()) {
                tb_consoleLog.Text += $"> {tb_consoleIn.Text}\n";
                grbl.Send(tb_consoleIn.Text);
                if (grbl.isParameterString(tb_consoleIn.Text))
                    grbl.ProcessParameter(tb_consoleIn.Text);
                tb_consoleIn.Text = "";
            }
        }
        private void tb_consoleIn_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                enterConsole();
            }
        }
        private void btn_consoleSend_Click(object sender, EventArgs e) {
            enterConsole();
        }

        private void tile_importGcode_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                try {
                    v.shouldDrawText = false;
                    tb_gCode.Text = File.ReadAllText(openFileDialog1.FileName);
                    File.WriteAllText(loadedFile, tb_gCode.Text);
                } catch (Exception ex) {
                    MessageBox.Show($"Couldn't import G-Code.\nFile: {openFileDialog1.FileName}\nMessage: {ex.Message}\nStacktrace:{ex.StackTrace}");
                }
            }
        }
        private void tile_text_Click(object sender, EventArgs e) {
            textToGcode = new TextToGcode(this);
            textToGcode.ShowDialog();
        }
        private void tile_shapes_Click(object sender, EventArgs e) {
            shapeToGcode = new ShapeToGcode(this);
            shapeToGcode.ShowDialog();
        }

        private void tile_xPos_Click(object sender, EventArgs e) {
            if (verifyGrbl()) grbl.CMD_JOG("X", true);
        }
        private void tile_xNeg_Click(object sender, EventArgs e) {
            if (verifyGrbl()) grbl.CMD_JOG("X", false);
        }
        private void tile_yPos_Click(object sender, EventArgs e) {
            if (verifyGrbl()) grbl.CMD_JOG("Y", true);
        }
        private void tile_yNeg_Click(object sender, EventArgs e) {
            if (verifyGrbl()) grbl.CMD_JOG("Y", false);
        }
        private void tile_zPos_Click(object sender, EventArgs e) {
            if (verifyGrbl()) grbl.CMD_JOG("Z", true);
        }
        private void tile_zNeg_Click(object sender, EventArgs e) {
            if (verifyGrbl()) grbl.CMD_JOG("Z", false);
        }

        private void tile_zero_Click(object sender, EventArgs e) {
            if (verifyGrbl()) grbl.CMD_ZEROALL();
        }
        private void tile_home_Click(object sender, EventArgs e) {
            if (verifyGrbl()) grbl.CMD_HOME();
        }

        private void tile_printPlay_Click(object sender, EventArgs e) {
            if (verifyGrbl()) {
                if (grbl.IsGCodeLoaded) {
                    grbl.SendChar('~');
                } else {
                    try {
                        grbl.OpenGCode(loadedFile);
                        grbl.StartFile();
                    } catch (Exception ex) {
                        MessageBox.Show($"Couldn't start G-Code.\nConnected: {grbl.IsConnected}" +
                            $"\nFile: {Path.GetFullPath(loadedFile)} ({(int)(new FileInfo(loadedFile).Length / 1024.0)} KB)" +
                            $"\nLoaded: {grbl.IsGCodeLoaded}\nMessage: {ex.Message}\nStacktrace:{ex.StackTrace}");
                    }
                }
            }
        }
        private void tile_printPause_Click(object sender, EventArgs e) {
            if (verifyGrbl()) grbl.SendChar('!');
        }

        private void btn_saveCnfg_Click(object sender, EventArgs e) {
            UnitQuantity workArea, feedRate, spindleSpeed;

            try {
                workArea = new UnitQuantity(new List<double>() { Convert.ToDouble(tb_waLength.Text), Convert.ToDouble(tb_waWidth.Text) }, cmb_waUnits.SelectedIndex);
            } catch (Exception ex) {
                MessageBox.Show("Invalid work area.");
                return;
            }

            try {
                feedRate = new UnitQuantity(Convert.ToDouble(tb_feedRate.Text), cmb_feedRateUnits.SelectedIndex);
            } catch (Exception ex) {
                MessageBox.Show("Invalid feed rate.");
                return;
            }

            try {
                spindleSpeed = new UnitQuantity(Convert.ToDouble(tb_spindleSpeed.Text), cmb_spindleSpeedUnits.SelectedIndex);
            } catch (Exception ex) {
                MessageBox.Show("Invalid spindle speed.");
                return;
            }

            config = new Config(workArea, feedRate, spindleSpeed);
            string json = JsonConvert.SerializeObject(config);
            File.WriteAllText(configFile, json);
        }
    }
}
