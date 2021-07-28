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
    public partial class ShapeToGcode : MetroForm {

        private MainForm mainForm;

        public ShapeToGcode(MainForm mainForm) {
            this.mainForm = mainForm;
            InitializeComponent();
        }
        
    }
}