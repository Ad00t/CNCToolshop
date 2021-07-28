namespace CNCToolshop {
    partial class TextToGcode {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.metroStyleExtender1 = new MetroFramework.Components.MetroStyleExtender(this.components);
            this.tb_text = new MetroFramework.Controls.MetroTextBox();
            this.btn_font = new MetroFramework.Controls.MetroButton();
            this.tile_getGcode = new MetroFramework.Controls.MetroTile();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.tb_height = new MetroFramework.Controls.MetroTextBox();
            this.cmb_heightUnits = new MetroFramework.Controls.MetroComboBox();
            this.cmb_depthUnits = new MetroFramework.Controls.MetroComboBox();
            this.tb_depth = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_text
            // 
            this.tb_text.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tb_text.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.tb_text.Location = new System.Drawing.Point(23, 85);
            this.tb_text.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.tb_text.Name = "tb_text";
            this.tb_text.Size = new System.Drawing.Size(279, 30);
            this.tb_text.TabIndex = 7;
            this.tb_text.Text = "Text";
            this.tb_text.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btn_font
            // 
            this.btn_font.Location = new System.Drawing.Point(308, 85);
            this.btn_font.Name = "btn_font";
            this.btn_font.Size = new System.Drawing.Size(92, 30);
            this.btn_font.TabIndex = 8;
            this.btn_font.Text = "Select Font";
            this.btn_font.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_font.Click += new System.EventHandler(this.btn_font_Click);
            // 
            // tile_getGcode
            // 
            this.tile_getGcode.AutoSize = true;
            this.tile_getGcode.Location = new System.Drawing.Point(146, 183);
            this.tile_getGcode.Margin = new System.Windows.Forms.Padding(4);
            this.tile_getGcode.Name = "tile_getGcode";
            this.tile_getGcode.Size = new System.Drawing.Size(133, 51);
            this.tile_getGcode.Style = MetroFramework.MetroColorStyle.Green;
            this.tile_getGcode.TabIndex = 9;
            this.tile_getGcode.Text = "Get G-Code";
            this.tile_getGcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tile_getGcode.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.tile_getGcode.Click += new System.EventHandler(this.tile_getGcode_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tb_height
            // 
            this.tb_height.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tb_height.Location = new System.Drawing.Point(23, 128);
            this.tb_height.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.tb_height.Name = "tb_height";
            this.tb_height.Size = new System.Drawing.Size(100, 30);
            this.tb_height.TabIndex = 10;
            this.tb_height.Text = "Height";
            this.tb_height.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // cmb_heightUnits
            // 
            this.cmb_heightUnits.FontSize = MetroFramework.MetroLinkSize.Small;
            this.cmb_heightUnits.FormattingEnabled = true;
            this.cmb_heightUnits.ItemHeight = 19;
            this.cmb_heightUnits.Items.AddRange(new object[] {
            "in",
            "cm"});
            this.cmb_heightUnits.Location = new System.Drawing.Point(130, 130);
            this.cmb_heightUnits.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_heightUnits.Name = "cmb_heightUnits";
            this.cmb_heightUnits.Size = new System.Drawing.Size(73, 25);
            this.cmb_heightUnits.TabIndex = 16;
            this.cmb_heightUnits.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // cmb_depthUnits
            // 
            this.cmb_depthUnits.FontSize = MetroFramework.MetroLinkSize.Small;
            this.cmb_depthUnits.FormattingEnabled = true;
            this.cmb_depthUnits.ItemHeight = 19;
            this.cmb_depthUnits.Items.AddRange(new object[] {
            "in",
            "mm",
            "cm"});
            this.cmb_depthUnits.Location = new System.Drawing.Point(327, 130);
            this.cmb_depthUnits.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_depthUnits.Name = "cmb_depthUnits";
            this.cmb_depthUnits.Size = new System.Drawing.Size(73, 25);
            this.cmb_depthUnits.TabIndex = 18;
            this.cmb_depthUnits.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tb_depth
            // 
            this.tb_depth.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tb_depth.Location = new System.Drawing.Point(220, 128);
            this.tb_depth.Name = "tb_depth";
            this.tb_depth.Size = new System.Drawing.Size(100, 30);
            this.tb_depth.TabIndex = 17;
            this.tb_depth.Text = "Depth";
            this.tb_depth.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // TextToGcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 259);
            this.Controls.Add(this.cmb_depthUnits);
            this.Controls.Add(this.tb_depth);
            this.Controls.Add(this.cmb_heightUnits);
            this.Controls.Add(this.tb_height);
            this.Controls.Add(this.tile_getGcode);
            this.Controls.Add(this.btn_font);
            this.Controls.Add(this.tb_text);
            this.MaximizeBox = false;
            this.Name = "TextToGcode";
            this.Resizable = false;
            this.Text = "Generate Text G-Code";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FontDialog fontDialog1;
        private MetroFramework.Components.MetroStyleExtender metroStyleExtender1;
        private MetroFramework.Controls.MetroTextBox tb_text;
        private MetroFramework.Controls.MetroButton btn_font;
        private MetroFramework.Controls.MetroTile tile_getGcode;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroTextBox tb_height;
        private MetroFramework.Controls.MetroComboBox cmb_heightUnits;
        private MetroFramework.Controls.MetroComboBox cmb_depthUnits;
        private MetroFramework.Controls.MetroTextBox tb_depth;
    }
}

