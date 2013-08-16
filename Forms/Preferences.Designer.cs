namespace TISFAT_ZERO
{
    partial class Preferences
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbl_canvasY = new System.Windows.Forms.Label();
            this.lbl_canvasX = new System.Windows.Forms.Label();
            this.txt_canvasY = new System.Windows.Forms.TextBox();
            this.txt_canvasX = new System.Windows.Forms.TextBox();
            this.lbl_defaultCanvasSize = new System.Windows.Forms.Label();
            this.btn_browseLocation = new System.Windows.Forms.Button();
            this.txt_defaultSaveLoc = new System.Windows.Forms.TextBox();
            this.lbl_defaultSaveLoc = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_Close = new System.Windows.Forms.Button();
            this.dlg_folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(532, 255);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbl_canvasY);
            this.tabPage1.Controls.Add(this.lbl_canvasX);
            this.tabPage1.Controls.Add(this.txt_canvasY);
            this.tabPage1.Controls.Add(this.txt_canvasX);
            this.tabPage1.Controls.Add(this.lbl_defaultCanvasSize);
            this.tabPage1.Controls.Add(this.btn_browseLocation);
            this.tabPage1.Controls.Add(this.txt_defaultSaveLoc);
            this.tabPage1.Controls.Add(this.lbl_defaultSaveLoc);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(524, 229);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbl_canvasY
            // 
            this.lbl_canvasY.AutoSize = true;
            this.lbl_canvasY.Location = new System.Drawing.Point(64, 67);
            this.lbl_canvasY.Name = "lbl_canvasY";
            this.lbl_canvasY.Size = new System.Drawing.Size(12, 13);
            this.lbl_canvasY.TabIndex = 7;
            this.lbl_canvasY.Text = "y";
            // 
            // lbl_canvasX
            // 
            this.lbl_canvasX.AutoSize = true;
            this.lbl_canvasX.Location = new System.Drawing.Point(10, 67);
            this.lbl_canvasX.Name = "lbl_canvasX";
            this.lbl_canvasX.Size = new System.Drawing.Size(12, 13);
            this.lbl_canvasX.TabIndex = 6;
            this.lbl_canvasX.Text = "x";
            // 
            // txt_canvasY
            // 
            this.txt_canvasY.Location = new System.Drawing.Point(82, 64);
            this.txt_canvasY.Name = "txt_canvasY";
            this.txt_canvasY.Size = new System.Drawing.Size(30, 20);
            this.txt_canvasY.TabIndex = 5;
            // 
            // txt_canvasX
            // 
            this.txt_canvasX.Location = new System.Drawing.Point(28, 64);
            this.txt_canvasX.Name = "txt_canvasX";
            this.txt_canvasX.Size = new System.Drawing.Size(30, 20);
            this.txt_canvasX.TabIndex = 4;
            // 
            // lbl_defaultCanvasSize
            // 
            this.lbl_defaultCanvasSize.AutoSize = true;
            this.lbl_defaultCanvasSize.Location = new System.Drawing.Point(6, 48);
            this.lbl_defaultCanvasSize.Name = "lbl_defaultCanvasSize";
            this.lbl_defaultCanvasSize.Size = new System.Drawing.Size(106, 13);
            this.lbl_defaultCanvasSize.TabIndex = 3;
            this.lbl_defaultCanvasSize.Text = "Default Canvas Size:";
            // 
            // btn_browseLocation
            // 
            this.btn_browseLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_browseLocation.Location = new System.Drawing.Point(291, 25);
            this.btn_browseLocation.Name = "btn_browseLocation";
            this.btn_browseLocation.Size = new System.Drawing.Size(20, 20);
            this.btn_browseLocation.TabIndex = 2;
            this.btn_browseLocation.Text = ". . .";
            this.btn_browseLocation.UseVisualStyleBackColor = true;
            this.btn_browseLocation.Click += new System.EventHandler(this.btn_browseLocation_Click);
            // 
            // txt_defaultSaveLoc
            // 
            this.txt_defaultSaveLoc.Location = new System.Drawing.Point(8, 25);
            this.txt_defaultSaveLoc.Name = "txt_defaultSaveLoc";
            this.txt_defaultSaveLoc.Size = new System.Drawing.Size(277, 20);
            this.txt_defaultSaveLoc.TabIndex = 1;
            // 
            // lbl_defaultSaveLoc
            // 
            this.lbl_defaultSaveLoc.AutoSize = true;
            this.lbl_defaultSaveLoc.Location = new System.Drawing.Point(5, 9);
            this.lbl_defaultSaveLoc.Name = "lbl_defaultSaveLoc";
            this.lbl_defaultSaveLoc.Size = new System.Drawing.Size(119, 13);
            this.lbl_defaultSaveLoc.TabIndex = 0;
            this.lbl_defaultSaveLoc.Text = "Default Save Location: ";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(516, 229);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(437, 261);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.button1_Click);
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 296);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(540, 330);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(540, 330);
            this.Name = "Preferences";
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.Preferences_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_browseLocation;
        private System.Windows.Forms.TextBox txt_defaultSaveLoc;
        private System.Windows.Forms.Label lbl_defaultSaveLoc;
        private System.Windows.Forms.FolderBrowserDialog dlg_folderBrowser;
        private System.Windows.Forms.Label lbl_canvasY;
        private System.Windows.Forms.Label lbl_canvasX;
        private System.Windows.Forms.TextBox txt_canvasY;
        private System.Windows.Forms.TextBox txt_canvasX;
        private System.Windows.Forms.Label lbl_defaultCanvasSize;
    }
}