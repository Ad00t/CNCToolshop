using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNCToolshop {
    public class MainVisualizer {

        public MainForm main;
        public Panel p;
        public Graphics g;
        public Image buffer;
        public bool shouldDrawText;

        public float S;
        public float gWaLength;
        public float gWaWidth;
        public float gOx;
        public float gOy;

        public MainVisualizer(MainForm main, Panel p) {
            this.main = main;
            this.p = p;
            
            buffer = new Bitmap(p.Width, p.Height, PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(buffer);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            S = p.Width / (float)main.config.workArea.mags[0];
            gWaLength = (float)main.config.workArea.mags[0] * S - 2 * S;
            gWaWidth = (float)main.config.workArea.mags[1] * S - 2 * S;
            gOx = p.Width / 2 - gWaLength / 2;
            gOy = p.Height / 2 - gWaWidth / 2;
            g.TranslateTransform(gOx, gOy);
        }

        public void paintWorkArea() {
            g.FillRectangle(new SolidBrush(Color.Gray), new RectangleF(0, 0, gWaLength, gWaWidth));

            float posR = S / 3; // max value of S/2
            PointF gMpos = cnc2g(main.mPos[0], main.mPos[1]);
            RectangleF gPos = new RectangleF(gMpos.X - posR, gMpos.Y - posR, 2 * posR, 2 * posR);
            if (main.isSpindleSpinning) {
                g.FillEllipse(new SolidBrush(Color.FromArgb(40, Color.Black)), gPos);
            }
            g.FillEllipse(new SolidBrush(Color.Green), gPos);
        }

        public void refresh() {
            g.Clear(Color.Transparent);
            paintWorkArea();
            p.Invalidate();
        }

        public PointF cnc2g(double x, double y) {
            float gX = (float)x;
            float gY = (float)y;
            gY = gWaWidth - gY;
            return new PointF(gX + gWaLength/2, gY - gWaWidth/2); // machine zero: bottom left corner
        }

    }
}
