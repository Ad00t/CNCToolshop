using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace FontToGcode
{
    public partial class Form1 : Form
    {
        PointF StartPoint = new PointF(0.0f, 0.0f), SubStartPoint = new PointF(0.0f, 0.0f), EndPoint = new PointF(0.0f, 0.0f);
        GraphicsPath Path;
        Graphics f;
        Font font;
        PathData data;
        Pen RemoveMaterial = new Pen(Color.White, 0.50f), Transition = new Pen(Color.Red, 0.1f);
 
        public Form1()
        {
            InitializeComponent();
        }

        private string FontToGcode(string Text, PointF origin, float FeedRate, float Depth, float RetractDistance)
        {
            String GCode = "";
            Path = new GraphicsPath();
            StringFormat stringformat = StringFormat.GenericDefault;
            FontFamily family = new FontFamily(font.Name);
            int style = (int)FontStyle.Bold;
            Path.AddString(Text, family, style, font.Size, origin, stringformat);
            data = new PathData();
            Path.Flatten();

            Path.FillMode = FillMode.Winding;

            return GCode;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            if (fontDialog1.Font != null)
                font = fontDialog1.Font;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            //this.richTextBox1.Text = 
            FontToGcode(toolStripTextBox1.Text, new PointF(0.0f, 0.0f), 10.0f, 3.0f, 1.0f);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            f = this.panel1.CreateGraphics();
            f.DrawPath(RemoveMaterial, Path);
            f.Save();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            f = this.panel1.CreateGraphics();
            f.ScaleTransform(5.0f, 5.0f);

            String GCode = "";

            float YOffset = Path.GetBounds().Height*3.0f;

            for (int i = 0; i < Path.PointCount; i++)
            {

                switch ((PathPointType)Path.PathData.Types[i])
                {
                    case PathPointType.Start:

                        EndPoint.X = Path.PathPoints[i].X;
                        EndPoint.Y = Path.PathPoints[i].Y;

                        //if (i != 0)
                        //{
                        f.DrawLine(RemoveMaterial, StartPoint, SubStartPoint);

                        //    GCode += "\nG01 Z -2 F 5\n";
                        if (i != 0)
                        GCode += "G01 X " + SubStartPoint.X.ToString() + " Y " + (-SubStartPoint.Y + YOffset).ToString() + "\n";

                        StartPoint.X = SubStartPoint.X;
                        StartPoint.Y = SubStartPoint.Y;
                        //}

                        f.DrawLine(Transition, StartPoint, EndPoint);

                        if (i != 0)
                            GCode += "\n";

                        GCode += "G00 Z 1\n";
                        GCode += "G00 X " + EndPoint.X.ToString() + " Y " + (-EndPoint.Y + YOffset).ToString() + "\n";
                        GCode += "G01 Z -2 F 5\n";

                        SubStartPoint.X = EndPoint.X;
                        SubStartPoint.Y = EndPoint.Y;
                        StartPoint.X = EndPoint.X;
                        StartPoint.Y = EndPoint.Y;

                        break;

                    default:

                        EndPoint.X = Path.PathPoints[i].X;
                        EndPoint.Y = Path.PathPoints[i].Y;


                        // Delay to see the paths while being drawn. Use it wisely, be careful not to use long intervals! be warned.
                        //for (int h = 0; h < 20000000; h++) ;

                        f.DrawLine(RemoveMaterial, StartPoint, EndPoint);

                        GCode += "G01 X " + EndPoint.X.ToString() + " Y " + (-EndPoint.Y + YOffset).ToString() + "\n";

                        StartPoint.X = EndPoint.X;
                        StartPoint.Y = EndPoint.Y;

                        break;
                }
            }

            EndPoint = Path.GetLastPoint();
            f.DrawLine(RemoveMaterial, EndPoint, SubStartPoint);

            GCode += "G00 X " + SubStartPoint.X.ToString() + " Y " + (-SubStartPoint.Y + YOffset).ToString() + "\n";
            GCode += "G00 Z 1\n";

            this.richTextBox1.Text = GCode;

            Path.Reset();  
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Path.Reset();
            richTextBox1.Text = "";
            f.Clear(Color.Black);
            StartPoint.X = 0;
            StartPoint.Y = 0;
            SubStartPoint.X = 0;
            SubStartPoint.Y = 0;
            EndPoint.X = 0;
            EndPoint.Y = 0;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
           
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }
    }
}