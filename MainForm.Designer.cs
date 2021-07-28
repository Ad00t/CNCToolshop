using System.Drawing;
using System.Windows.Forms;

namespace CNCToolshop
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tb_gCode = new System.Windows.Forms.RichTextBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.pnl_homeGcode = new MetroFramework.Controls.MetroPanel();
            this.btn_consoleSend = new MetroFramework.Controls.MetroButton();
            this.tb_consoleIn = new System.Windows.Forms.RichTextBox();
            this.lbl_console = new MetroFramework.Controls.MetroLabel();
            this.tb_consoleLog = new System.Windows.Forms.RichTextBox();
            this.lbl_gCode = new MetroFramework.Controls.MetroLabel();
            this.pnl_homeDisp = new MetroFramework.Controls.MetroPanel();
            this.lbl_status = new MetroFramework.Controls.MetroLabel();
            this.lbl_mZ = new MetroFramework.Controls.MetroLabel();
            this.lbl_mY = new MetroFramework.Controls.MetroLabel();
            this.lbl_mX = new MetroFramework.Controls.MetroLabel();
            this.pnl_visualizer = new System.Windows.Forms.Panel();
            this.btn_clearVisualizer = new MetroFramework.Controls.MetroButton();
            this.tile_printPause = new MetroFramework.Controls.MetroTile();
            this.tile_printPlay = new MetroFramework.Controls.MetroTile();
            this.lbl_wZ = new MetroFramework.Controls.MetroLabel();
            this.lbl_wY = new MetroFramework.Controls.MetroLabel();
            this.lbl_wX = new MetroFramework.Controls.MetroLabel();
            this.pnl_homeOptions = new MetroFramework.Controls.MetroPanel();
            this.tile_home = new MetroFramework.Controls.MetroTile();
            this.tile_zNeg = new MetroFramework.Controls.MetroTile();
            this.tile_zPos = new MetroFramework.Controls.MetroTile();
            this.tile_zero = new MetroFramework.Controls.MetroTile();
            this.tile_yNeg = new MetroFramework.Controls.MetroTile();
            this.tile_xPos = new MetroFramework.Controls.MetroTile();
            this.tile_xNeg = new MetroFramework.Controls.MetroTile();
            this.tile_yPos = new MetroFramework.Controls.MetroTile();
            this.homePosLabel = new MetroFramework.Controls.MetroLabel();
            this.tile_text = new MetroFramework.Controls.MetroTile();
            this.tile_importGcode = new MetroFramework.Controls.MetroTile();
            this.tile_shapes = new MetroFramework.Controls.MetroTile();
            this.featuresLabel = new MetroFramework.Controls.MetroLabel();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.pnl_config = new MetroFramework.Controls.MetroPanel();
            this.cmb_waUnits = new MetroFramework.Controls.MetroComboBox();
            this.tb_waWidth = new MetroFramework.Controls.MetroTextBox();
            this.tb_waLength = new MetroFramework.Controls.MetroTextBox();
            this.lbl_workArea = new MetroFramework.Controls.MetroLabel();
            this.cmb_spindleSpeedUnits = new MetroFramework.Controls.MetroComboBox();
            this.tb_spindleSpeed = new MetroFramework.Controls.MetroTextBox();
            this.lbl_spindleSpeed = new MetroFramework.Controls.MetroLabel();
            this.btn_saveCnfg = new MetroFramework.Controls.MetroButton();
            this.cmb_feedRateUnits = new MetroFramework.Controls.MetroComboBox();
            this.tb_feedRate = new MetroFramework.Controls.MetroTextBox();
            this.lbl_feedRate = new MetroFramework.Controls.MetroLabel();
            this.metroStyleExtender1 = new MetroFramework.Components.MetroStyleExtender(this.components);
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.tile_refreshPorts = new MetroFramework.Controls.MetroTile();
            this.cmb_port = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.pnl_homeGcode.SuspendLayout();
            this.pnl_homeDisp.SuspendLayout();
            this.pnl_visualizer.SuspendLayout();
            this.pnl_homeOptions.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.pnl_config.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_gCode
            // 
            this.metroStyleExtender1.SetApplyMetroTheme(this.tb_gCode, true);
            this.tb_gCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tb_gCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.tb_gCode, "tb_gCode");
            this.tb_gCode.Name = "tb_gCode";
            this.tb_gCode.ReadOnly = true;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // metroTabControl1
            // 
            resources.ApplyResources(this.metroTabControl1, "metroTabControl1");
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.metroTabControl1.HotTrack = true;
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.pnl_homeGcode);
            this.metroTabPage1.Controls.Add(this.pnl_homeDisp);
            this.metroTabPage1.Controls.Add(this.pnl_homeOptions);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarSize = 9;
            resources.ApplyResources(this.metroTabPage1, "metroTabPage1");
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarSize = 12;
            // 
            // pnl_homeGcode
            // 
            this.pnl_homeGcode.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.pnl_homeGcode.Controls.Add(this.btn_consoleSend);
            this.pnl_homeGcode.Controls.Add(this.tb_consoleIn);
            this.pnl_homeGcode.Controls.Add(this.lbl_console);
            this.pnl_homeGcode.Controls.Add(this.tb_consoleLog);
            this.pnl_homeGcode.Controls.Add(this.tb_gCode);
            this.pnl_homeGcode.Controls.Add(this.lbl_gCode);
            this.pnl_homeGcode.HorizontalScrollbarBarColor = true;
            this.pnl_homeGcode.HorizontalScrollbarHighlightOnWheel = false;
            this.pnl_homeGcode.HorizontalScrollbarSize = 9;
            resources.ApplyResources(this.pnl_homeGcode, "pnl_homeGcode");
            this.pnl_homeGcode.Name = "pnl_homeGcode";
            this.pnl_homeGcode.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.pnl_homeGcode.VerticalScrollbarBarColor = true;
            this.pnl_homeGcode.VerticalScrollbarHighlightOnWheel = false;
            this.pnl_homeGcode.VerticalScrollbarSize = 12;
            // 
            // btn_consoleSend
            // 
            resources.ApplyResources(this.btn_consoleSend, "btn_consoleSend");
            this.btn_consoleSend.Name = "btn_consoleSend";
            this.btn_consoleSend.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_consoleSend.Click += new System.EventHandler(this.btn_consoleSend_Click);
            // 
            // tb_consoleIn
            // 
            this.metroStyleExtender1.SetApplyMetroTheme(this.tb_consoleIn, true);
            this.tb_consoleIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tb_consoleIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.tb_consoleIn, "tb_consoleIn");
            this.tb_consoleIn.Name = "tb_consoleIn";
            this.tb_consoleIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_consoleIn_KeyDown);
            // 
            // lbl_console
            // 
            this.lbl_console.FontSize = MetroFramework.MetroLabelSize.Tall;
            resources.ApplyResources(this.lbl_console, "lbl_console");
            this.lbl_console.Name = "lbl_console";
            this.lbl_console.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tb_consoleLog
            // 
            this.metroStyleExtender1.SetApplyMetroTheme(this.tb_consoleLog, true);
            this.tb_consoleLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tb_consoleLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.tb_consoleLog, "tb_consoleLog");
            this.tb_consoleLog.Name = "tb_consoleLog";
            this.tb_consoleLog.ReadOnly = true;
            // 
            // lbl_gCode
            // 
            this.lbl_gCode.FontSize = MetroFramework.MetroLabelSize.Tall;
            resources.ApplyResources(this.lbl_gCode, "lbl_gCode");
            this.lbl_gCode.Name = "lbl_gCode";
            this.lbl_gCode.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // pnl_homeDisp
            // 
            this.pnl_homeDisp.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.pnl_homeDisp.Controls.Add(this.lbl_status);
            this.pnl_homeDisp.Controls.Add(this.lbl_mZ);
            this.pnl_homeDisp.Controls.Add(this.lbl_mY);
            this.pnl_homeDisp.Controls.Add(this.lbl_mX);
            this.pnl_homeDisp.Controls.Add(this.pnl_visualizer);
            this.pnl_homeDisp.Controls.Add(this.tile_printPause);
            this.pnl_homeDisp.Controls.Add(this.tile_printPlay);
            this.pnl_homeDisp.Controls.Add(this.lbl_wZ);
            this.pnl_homeDisp.Controls.Add(this.lbl_wY);
            this.pnl_homeDisp.Controls.Add(this.lbl_wX);
            this.pnl_homeDisp.HorizontalScrollbarBarColor = true;
            this.pnl_homeDisp.HorizontalScrollbarHighlightOnWheel = false;
            this.pnl_homeDisp.HorizontalScrollbarSize = 9;
            resources.ApplyResources(this.pnl_homeDisp, "pnl_homeDisp");
            this.pnl_homeDisp.Name = "pnl_homeDisp";
            this.pnl_homeDisp.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.pnl_homeDisp.VerticalScrollbarBarColor = true;
            this.pnl_homeDisp.VerticalScrollbarHighlightOnWheel = false;
            this.pnl_homeDisp.VerticalScrollbarSize = 12;
            // 
            // lbl_status
            // 
            this.lbl_status.FontSize = MetroFramework.MetroLabelSize.Tall;
            resources.ApplyResources(this.lbl_status, "lbl_status");
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl_mZ
            // 
            resources.ApplyResources(this.lbl_mZ, "lbl_mZ");
            this.lbl_mZ.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_mZ.Name = "lbl_mZ";
            this.lbl_mZ.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl_mY
            // 
            resources.ApplyResources(this.lbl_mY, "lbl_mY");
            this.lbl_mY.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_mY.Name = "lbl_mY";
            this.lbl_mY.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl_mX
            // 
            resources.ApplyResources(this.lbl_mX, "lbl_mX");
            this.lbl_mX.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_mX.Name = "lbl_mX";
            this.lbl_mX.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // pnl_visualizer
            // 
            this.metroStyleExtender1.SetApplyMetroTheme(this.pnl_visualizer, true);
            this.pnl_visualizer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnl_visualizer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_visualizer.Controls.Add(this.btn_clearVisualizer);
            this.pnl_visualizer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.pnl_visualizer, "pnl_visualizer");
            this.pnl_visualizer.Name = "pnl_visualizer";
            this.pnl_visualizer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_visualizer_Paint);
            // 
            // btn_clearVisualizer
            // 
            resources.ApplyResources(this.btn_clearVisualizer, "btn_clearVisualizer");
            this.btn_clearVisualizer.Name = "btn_clearVisualizer";
            this.btn_clearVisualizer.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_clearVisualizer.Click += new System.EventHandler(this.btn_clearVisualizer_Click);
            // 
            // tile_printPause
            // 
            resources.ApplyResources(this.tile_printPause, "tile_printPause");
            this.tile_printPause.Name = "tile_printPause";
            this.tile_printPause.Style = MetroFramework.MetroColorStyle.Orange;
            this.tile_printPause.TileImage = ((System.Drawing.Image)(resources.GetObject("tile_printPause.TileImage")));
            this.tile_printPause.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tile_printPause.TileTextFontSize = MetroFramework.MetroTileTextSize.Small;
            this.tile_printPause.UseTileImage = true;
            this.tile_printPause.Click += new System.EventHandler(this.tile_printPause_Click);
            // 
            // tile_printPlay
            // 
            resources.ApplyResources(this.tile_printPlay, "tile_printPlay");
            this.tile_printPlay.Name = "tile_printPlay";
            this.tile_printPlay.Style = MetroFramework.MetroColorStyle.Green;
            this.tile_printPlay.TileImage = ((System.Drawing.Image)(resources.GetObject("tile_printPlay.TileImage")));
            this.tile_printPlay.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tile_printPlay.TileTextFontSize = MetroFramework.MetroTileTextSize.Small;
            this.tile_printPlay.UseTileImage = true;
            this.tile_printPlay.Click += new System.EventHandler(this.tile_printPlay_Click);
            // 
            // lbl_wZ
            // 
            resources.ApplyResources(this.lbl_wZ, "lbl_wZ");
            this.lbl_wZ.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_wZ.Name = "lbl_wZ";
            this.lbl_wZ.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl_wY
            // 
            resources.ApplyResources(this.lbl_wY, "lbl_wY");
            this.lbl_wY.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_wY.Name = "lbl_wY";
            this.lbl_wY.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl_wX
            // 
            resources.ApplyResources(this.lbl_wX, "lbl_wX");
            this.lbl_wX.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_wX.Name = "lbl_wX";
            this.lbl_wX.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // pnl_homeOptions
            // 
            resources.ApplyResources(this.pnl_homeOptions, "pnl_homeOptions");
            this.pnl_homeOptions.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.pnl_homeOptions.Controls.Add(this.tile_home);
            this.pnl_homeOptions.Controls.Add(this.tile_zNeg);
            this.pnl_homeOptions.Controls.Add(this.tile_zPos);
            this.pnl_homeOptions.Controls.Add(this.tile_zero);
            this.pnl_homeOptions.Controls.Add(this.tile_yNeg);
            this.pnl_homeOptions.Controls.Add(this.tile_xPos);
            this.pnl_homeOptions.Controls.Add(this.tile_xNeg);
            this.pnl_homeOptions.Controls.Add(this.tile_yPos);
            this.pnl_homeOptions.Controls.Add(this.homePosLabel);
            this.pnl_homeOptions.Controls.Add(this.tile_text);
            this.pnl_homeOptions.Controls.Add(this.tile_importGcode);
            this.pnl_homeOptions.Controls.Add(this.tile_shapes);
            this.pnl_homeOptions.Controls.Add(this.featuresLabel);
            this.pnl_homeOptions.HorizontalScrollbar = true;
            this.pnl_homeOptions.HorizontalScrollbarBarColor = true;
            this.pnl_homeOptions.HorizontalScrollbarHighlightOnWheel = false;
            this.pnl_homeOptions.HorizontalScrollbarSize = 9;
            this.pnl_homeOptions.Name = "pnl_homeOptions";
            this.pnl_homeOptions.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.pnl_homeOptions.VerticalScrollbar = true;
            this.pnl_homeOptions.VerticalScrollbarBarColor = true;
            this.pnl_homeOptions.VerticalScrollbarHighlightOnWheel = false;
            this.pnl_homeOptions.VerticalScrollbarSize = 12;
            // 
            // tile_home
            // 
            resources.ApplyResources(this.tile_home, "tile_home");
            this.tile_home.Name = "tile_home";
            this.tile_home.Style = MetroFramework.MetroColorStyle.Purple;
            this.tile_home.Click += new System.EventHandler(this.tile_home_Click);
            // 
            // tile_zNeg
            // 
            resources.ApplyResources(this.tile_zNeg, "tile_zNeg");
            this.tile_zNeg.Name = "tile_zNeg";
            this.tile_zNeg.Style = MetroFramework.MetroColorStyle.Teal;
            this.tile_zNeg.Click += new System.EventHandler(this.tile_zNeg_Click);
            // 
            // tile_zPos
            // 
            resources.ApplyResources(this.tile_zPos, "tile_zPos");
            this.tile_zPos.Name = "tile_zPos";
            this.tile_zPos.Style = MetroFramework.MetroColorStyle.Teal;
            this.tile_zPos.Click += new System.EventHandler(this.tile_zPos_Click);
            // 
            // tile_zero
            // 
            resources.ApplyResources(this.tile_zero, "tile_zero");
            this.tile_zero.Name = "tile_zero";
            this.tile_zero.Style = MetroFramework.MetroColorStyle.Purple;
            this.tile_zero.Click += new System.EventHandler(this.tile_zero_Click);
            // 
            // tile_yNeg
            // 
            resources.ApplyResources(this.tile_yNeg, "tile_yNeg");
            this.tile_yNeg.Name = "tile_yNeg";
            this.tile_yNeg.Style = MetroFramework.MetroColorStyle.Green;
            this.tile_yNeg.Click += new System.EventHandler(this.tile_yNeg_Click);
            // 
            // tile_xPos
            // 
            resources.ApplyResources(this.tile_xPos, "tile_xPos");
            this.tile_xPos.Name = "tile_xPos";
            this.tile_xPos.Style = MetroFramework.MetroColorStyle.Red;
            this.tile_xPos.Click += new System.EventHandler(this.tile_xPos_Click);
            // 
            // tile_xNeg
            // 
            resources.ApplyResources(this.tile_xNeg, "tile_xNeg");
            this.tile_xNeg.Name = "tile_xNeg";
            this.tile_xNeg.Style = MetroFramework.MetroColorStyle.Red;
            this.tile_xNeg.Click += new System.EventHandler(this.tile_xNeg_Click);
            // 
            // tile_yPos
            // 
            resources.ApplyResources(this.tile_yPos, "tile_yPos");
            this.tile_yPos.Name = "tile_yPos";
            this.tile_yPos.Style = MetroFramework.MetroColorStyle.Green;
            this.tile_yPos.Click += new System.EventHandler(this.tile_yPos_Click);
            // 
            // homePosLabel
            // 
            this.homePosLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            resources.ApplyResources(this.homePosLabel, "homePosLabel");
            this.homePosLabel.Name = "homePosLabel";
            this.homePosLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tile_text
            // 
            resources.ApplyResources(this.tile_text, "tile_text");
            this.tile_text.Name = "tile_text";
            this.tile_text.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.tile_text.UseMnemonic = false;
            this.tile_text.Click += new System.EventHandler(this.tile_text_Click);
            // 
            // tile_importGcode
            // 
            resources.ApplyResources(this.tile_importGcode, "tile_importGcode");
            this.tile_importGcode.Name = "tile_importGcode";
            this.tile_importGcode.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.tile_importGcode.Click += new System.EventHandler(this.tile_importGcode_Click);
            // 
            // tile_shapes
            // 
            resources.ApplyResources(this.tile_shapes, "tile_shapes");
            this.tile_shapes.Name = "tile_shapes";
            this.tile_shapes.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.tile_shapes.Click += new System.EventHandler(this.tile_shapes_Click);
            // 
            // featuresLabel
            // 
            this.featuresLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            resources.ApplyResources(this.featuresLabel, "featuresLabel");
            this.featuresLabel.Name = "featuresLabel";
            this.featuresLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.pnl_config);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarSize = 9;
            resources.ApplyResources(this.metroTabPage2, "metroTabPage2");
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarSize = 12;
            // 
            // pnl_config
            // 
            resources.ApplyResources(this.pnl_config, "pnl_config");
            this.pnl_config.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.pnl_config.Controls.Add(this.cmb_waUnits);
            this.pnl_config.Controls.Add(this.tb_waWidth);
            this.pnl_config.Controls.Add(this.tb_waLength);
            this.pnl_config.Controls.Add(this.lbl_workArea);
            this.pnl_config.Controls.Add(this.cmb_spindleSpeedUnits);
            this.pnl_config.Controls.Add(this.tb_spindleSpeed);
            this.pnl_config.Controls.Add(this.lbl_spindleSpeed);
            this.pnl_config.Controls.Add(this.btn_saveCnfg);
            this.pnl_config.Controls.Add(this.cmb_feedRateUnits);
            this.pnl_config.Controls.Add(this.tb_feedRate);
            this.pnl_config.Controls.Add(this.lbl_feedRate);
            this.pnl_config.HorizontalScrollbar = true;
            this.pnl_config.HorizontalScrollbarBarColor = true;
            this.pnl_config.HorizontalScrollbarHighlightOnWheel = false;
            this.pnl_config.HorizontalScrollbarSize = 9;
            this.pnl_config.Name = "pnl_config";
            this.pnl_config.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.pnl_config.VerticalScrollbar = true;
            this.pnl_config.VerticalScrollbarBarColor = true;
            this.pnl_config.VerticalScrollbarHighlightOnWheel = false;
            this.pnl_config.VerticalScrollbarSize = 12;
            // 
            // cmb_waUnits
            // 
            this.cmb_waUnits.FontSize = MetroFramework.MetroLinkSize.Small;
            this.cmb_waUnits.FormattingEnabled = true;
            resources.ApplyResources(this.cmb_waUnits, "cmb_waUnits");
            this.cmb_waUnits.Items.AddRange(new object[] {
            resources.GetString("cmb_waUnits.Items"),
            resources.GetString("cmb_waUnits.Items1"),
            resources.GetString("cmb_waUnits.Items2"),
            resources.GetString("cmb_waUnits.Items3")});
            this.cmb_waUnits.Name = "cmb_waUnits";
            this.cmb_waUnits.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tb_waWidth
            // 
            resources.ApplyResources(this.tb_waWidth, "tb_waWidth");
            this.tb_waWidth.Name = "tb_waWidth";
            this.tb_waWidth.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tb_waLength
            // 
            resources.ApplyResources(this.tb_waLength, "tb_waLength");
            this.tb_waLength.Name = "tb_waLength";
            this.tb_waLength.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl_workArea
            // 
            this.lbl_workArea.FontSize = MetroFramework.MetroLabelSize.Tall;
            resources.ApplyResources(this.lbl_workArea, "lbl_workArea");
            this.lbl_workArea.Name = "lbl_workArea";
            this.lbl_workArea.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // cmb_spindleSpeedUnits
            // 
            this.cmb_spindleSpeedUnits.FontSize = MetroFramework.MetroLinkSize.Small;
            this.cmb_spindleSpeedUnits.FormattingEnabled = true;
            resources.ApplyResources(this.cmb_spindleSpeedUnits, "cmb_spindleSpeedUnits");
            this.cmb_spindleSpeedUnits.Items.AddRange(new object[] {
            resources.GetString("cmb_spindleSpeedUnits.Items")});
            this.cmb_spindleSpeedUnits.Name = "cmb_spindleSpeedUnits";
            this.cmb_spindleSpeedUnits.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tb_spindleSpeed
            // 
            resources.ApplyResources(this.tb_spindleSpeed, "tb_spindleSpeed");
            this.tb_spindleSpeed.Name = "tb_spindleSpeed";
            this.tb_spindleSpeed.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl_spindleSpeed
            // 
            this.lbl_spindleSpeed.FontSize = MetroFramework.MetroLabelSize.Tall;
            resources.ApplyResources(this.lbl_spindleSpeed, "lbl_spindleSpeed");
            this.lbl_spindleSpeed.Name = "lbl_spindleSpeed";
            this.lbl_spindleSpeed.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btn_saveCnfg
            // 
            resources.ApplyResources(this.btn_saveCnfg, "btn_saveCnfg");
            this.btn_saveCnfg.Name = "btn_saveCnfg";
            this.btn_saveCnfg.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_saveCnfg.Click += new System.EventHandler(this.btn_saveCnfg_Click);
            // 
            // cmb_feedRateUnits
            // 
            this.cmb_feedRateUnits.FontSize = MetroFramework.MetroLinkSize.Small;
            this.cmb_feedRateUnits.FormattingEnabled = true;
            resources.ApplyResources(this.cmb_feedRateUnits, "cmb_feedRateUnits");
            this.cmb_feedRateUnits.Items.AddRange(new object[] {
            resources.GetString("cmb_feedRateUnits.Items"),
            resources.GetString("cmb_feedRateUnits.Items1"),
            resources.GetString("cmb_feedRateUnits.Items2")});
            this.cmb_feedRateUnits.Name = "cmb_feedRateUnits";
            this.cmb_feedRateUnits.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tb_feedRate
            // 
            resources.ApplyResources(this.tb_feedRate, "tb_feedRate");
            this.tb_feedRate.Name = "tb_feedRate";
            this.tb_feedRate.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl_feedRate
            // 
            this.lbl_feedRate.FontSize = MetroFramework.MetroLabelSize.Tall;
            resources.ApplyResources(this.lbl_feedRate, "lbl_feedRate");
            this.lbl_feedRate.Name = "lbl_feedRate";
            this.lbl_feedRate.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tile_refreshPorts
            // 
            resources.ApplyResources(this.tile_refreshPorts, "tile_refreshPorts");
            this.tile_refreshPorts.Name = "tile_refreshPorts";
            this.tile_refreshPorts.TileImage = ((System.Drawing.Image)(resources.GetObject("tile_refreshPorts.TileImage")));
            this.tile_refreshPorts.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tile_refreshPorts.TileTextFontSize = MetroFramework.MetroTileTextSize.Small;
            this.tile_refreshPorts.UseTileImage = true;
            // 
            // cmb_port
            // 
            this.cmb_port.FontSize = MetroFramework.MetroLinkSize.Small;
            this.cmb_port.FormattingEnabled = true;
            resources.ApplyResources(this.cmb_port, "cmb_port");
            this.cmb_port.Name = "cmb_port";
            this.cmb_port.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cmb_port.SelectedIndexChanged += new System.EventHandler(this.cmb_port_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            resources.ApplyResources(this.metroLabel1, "metroLabel1");
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tile_refreshPorts);
            this.Controls.Add(this.cmb_port);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroTabControl1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Resizable = false;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.pnl_homeGcode.ResumeLayout(false);
            this.pnl_homeDisp.ResumeLayout(false);
            this.pnl_homeDisp.PerformLayout();
            this.pnl_visualizer.ResumeLayout(false);
            this.pnl_homeOptions.ResumeLayout(false);
            this.pnl_homeOptions.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.pnl_config.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.IO.FileSystemWatcher fileSystemWatcher1;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public MetroFramework.Controls.MetroTabControl metroTabControl1;
        public MetroFramework.Controls.MetroTabPage metroTabPage1;
        public MetroFramework.Controls.MetroTabPage metroTabPage2;
        public MetroFramework.Controls.MetroPanel pnl_homeOptions;
        public MetroFramework.Controls.MetroPanel pnl_homeDisp;
        public MetroFramework.Controls.MetroPanel pnl_config;
        public MetroFramework.Controls.MetroPanel pnl_homeGcode;
        public MetroFramework.Controls.MetroLabel featuresLabel;
        public MetroFramework.Controls.MetroTile tile_shapes;
        public MetroFramework.Controls.MetroTile tile_text;
        public MetroFramework.Controls.MetroTile tile_importGcode;
        public MetroFramework.Controls.MetroLabel lbl_gCode;
        public MetroFramework.Controls.MetroLabel homePosLabel;
        public MetroFramework.Controls.MetroTile tile_yPos;
        public MetroFramework.Controls.MetroTile tile_yNeg;
        public MetroFramework.Controls.MetroTile tile_xPos;
        public MetroFramework.Controls.MetroTile tile_xNeg;
        public MetroFramework.Controls.MetroTile tile_zero;
        public MetroFramework.Controls.MetroTextBox tb_feedRate;
        public MetroFramework.Controls.MetroLabel lbl_feedRate;
        public MetroFramework.Controls.MetroComboBox cmb_feedRateUnits;
        public MetroFramework.Controls.MetroLabel lbl_wX;
        public MetroFramework.Controls.MetroLabel lbl_wZ;
        public MetroFramework.Controls.MetroLabel lbl_wY;
        public MetroFramework.Controls.MetroTile tile_zNeg;
        public MetroFramework.Controls.MetroTile tile_zPos;
        public MetroFramework.Controls.MetroButton btn_saveCnfg;
        public MetroFramework.Controls.MetroTile tile_printPlay;
        public MetroFramework.Controls.MetroTile tile_printPause;
        public RichTextBox tb_gCode;
        public MetroFramework.Controls.MetroTile tile_home;
        public MetroFramework.Components.MetroStyleExtender metroStyleExtender1;
        public MetroFramework.Components.MetroStyleManager metroStyleManager1;
        public MetroFramework.Controls.MetroComboBox cmb_spindleSpeedUnits;
        public MetroFramework.Controls.MetroTextBox tb_spindleSpeed;
        public MetroFramework.Controls.MetroLabel lbl_spindleSpeed;
        public MetroFramework.Controls.MetroLabel lbl_workArea;
        public MetroFramework.Controls.MetroComboBox cmb_waUnits;
        public MetroFramework.Controls.MetroTextBox tb_waWidth;
        public MetroFramework.Controls.MetroTextBox tb_waLength;
        public Panel pnl_visualizer;
        public MetroFramework.Controls.MetroLabel lbl_mZ;
        public MetroFramework.Controls.MetroLabel lbl_mY;
        public MetroFramework.Controls.MetroLabel lbl_mX;
        public MetroFramework.Controls.MetroLabel lbl_console;
        public RichTextBox tb_consoleLog;
        public RichTextBox tb_consoleIn;
        public MetroFramework.Controls.MetroButton btn_consoleSend;
        public MetroFramework.Controls.MetroLabel lbl_status;
        public MetroFramework.Controls.MetroButton btn_clearVisualizer;
        public MetroFramework.Controls.MetroTile tile_refreshPorts;
        public MetroFramework.Controls.MetroComboBox cmb_port;
        public MetroFramework.Controls.MetroLabel metroLabel1;
    }
}

