using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace CNCToolshop {
    public partial class TextToGcode : MetroForm {

        private MainForm mF;
        private PointF StartPoint = new PointF(0.0f, 0.0f), SubStartPoint = new PointF(0.0f, 0.0f), EndPoint = new PointF(0.0f, 0.0f);
        private GraphicsPath Path;
        private Font font = SystemFonts.DefaultFont;
        private Pen RemoveMaterial = new Pen(Color.White, 0.50f), Transition = new Pen(Color.Red, 0.1f);

        public TextToGcode(MainForm mainForm) {
            this.mF = mainForm;
            InitializeComponent();

            cmb_heightUnits.SelectedIndex = 0;
            cmb_depthUnits.SelectedIndex = 0;
        }

        private void btn_font_Click(object sender, EventArgs e) {
            fontDialog1.ShowDialog();
            if (fontDialog1.Font != null)
                font = fontDialog1.Font;
        }

        private void tile_getGcode_Click(object sender, EventArgs e) {
            if (Path != null)
                Path.Reset();
            StartPoint.X = 0;
            StartPoint.Y = 0;
            SubStartPoint.X = 0;
            SubStartPoint.Y = 0;
            EndPoint.X = 0;
            EndPoint.Y = 0;

            float heightIn;
            try {
                heightIn = (float)Convert.ToDouble(tb_height.Text);
            } catch (Exception ex) {
                MessageBox.Show("Invalid height.");
                return;
            }
            if (cmb_heightUnits.SelectedIndex == 1) heightIn /= 2.54f;

            double depthMM;
            try {
                depthMM = Convert.ToDouble(tb_depth.Text);
            } catch (Exception ex) {
                MessageBox.Show("Invalid depth.");
                return;
            }
            if (cmb_depthUnits.SelectedIndex == 0) depthMM *= 25.4;
            else if (cmb_depthUnits.SelectedIndex == 2) depthMM *= 10;

            font = new Font(font.Name, heightIn * 72, font.Style);
            Path = new GraphicsPath();
            Path.AddString(tb_text.Text, new FontFamily(font.Name), (int)font.Style, font.Size, new PointF(0.0f, 0.0f), StringFormat.GenericDefault);
            Path.Flatten();
            Path.FillMode = FillMode.Winding;

            string GCode = "G90 G94 G40 G49 G17 G21\n";
            GCode += "M05\n";
            GCode += "M09\n";
            GCode += $"S{(int)mF.config.spindleSpeed.mags[0]} M03\n";
            GCode += "G54\n";
            GCode += "M08\n";

            mF.v.refresh();
            PointF o = mF.v.cnc2g(mF.mPos[0], mF.mPos[1]);
            mF.v.g.TranslateTransform(o.X, o.Y);
            for (double d = 1; d <= Math.Floor(depthMM); d++)
                GCode += pass(d);
            GCode += pass(depthMM);
            mF.v.g.TranslateTransform(-o.X, -o.Y);
            mF.pnl_visualizer.Invalidate();

            GCode += "M09\n";
            GCode += "M05\n";
            GCode += "M30";

            mF.tb_gCode.Text = GCode;
            File.WriteAllText(mF.loadedFile, GCode);
        }

        private string pass(double d) {
            string GCode = "";
            double feed = Convert.ToDouble(mF.config.feedRate.mags[0]);
            if (mF.config.feedRate.unitsIndex == 0) feed *= 25.4;
            else if (mF.config.feedRate.unitsIndex == 2) feed *= 10;
            Graphics g = mF.v.g;

            float YOffset = Path.GetBounds().Height * 3.0f;
            for (int i = 0; i < Path.PointCount; i++) {
                switch ((PathPointType)Path.PathData.Types[i]) {
                    case PathPointType.Start:
                        EndPoint.X = Path.PathPoints[i].X; EndPoint.Y = Path.PathPoints[i].Y;
                        g.DrawLine(RemoveMaterial, StartPoint, SubStartPoint);
                        if (i != 0)
                            GCode += $"G01 X{Math.Round(SubStartPoint.X, 3)} Y{Math.Round(-SubStartPoint.Y + YOffset, 3)} F{(int)Math.Round(feed)}\n";
                        StartPoint.X = SubStartPoint.X; StartPoint.Y = SubStartPoint.Y;
                        g.DrawLine(Transition, StartPoint, EndPoint);

                        if (i != 0)
                            GCode += "\n";
                        GCode += "G00 Z20\n";
                        GCode += $"G00 X{Math.Round(EndPoint.X, 3)} Y{Math.Round(-EndPoint.Y + YOffset, 3)}\n";
                        GCode += $"G01 Z{Math.Round(-d, 4)} F{(int)Math.Round(feed)}\n";

                        SubStartPoint.X = EndPoint.X; SubStartPoint.Y = EndPoint.Y;
                        StartPoint.X = EndPoint.X; StartPoint.Y = EndPoint.Y;
                        break;
                    default:
                        EndPoint.X = Path.PathPoints[i].X; EndPoint.Y = Path.PathPoints[i].Y;
                        g.DrawLine(RemoveMaterial, StartPoint, EndPoint);
                        GCode += $"G01 X{Math.Round(EndPoint.X, 3)} Y{Math.Round(-EndPoint.Y + YOffset, 3)} F{(int)Math.Round(feed)}\n";
                        StartPoint.X = EndPoint.X; StartPoint.Y = EndPoint.Y;
                        break;
                }
            }

            EndPoint = Path.GetLastPoint();
            GCode += $"G00 X{Math.Round(SubStartPoint.X, 3)} Y{Math.Round(-SubStartPoint.Y + YOffset, 3)}\n";
            GCode += "G00 Z20\n";
            return GCode;
        }

    }
}