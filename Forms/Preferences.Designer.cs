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
            this.dlg_folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnl_General = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.num_Height = new System.Windows.Forms.NumericUpDown();
            this.num_Width = new System.Windows.Forms.NumericUpDown();
            this.pnl_colorButtonHitbox = new System.Windows.Forms.Panel();
            this.lbl_backgroundColorPic = new System.Windows.Forms.Label();
            this.pic_colorBox = new System.Windows.Forms.PictureBox();
            this.lbl_backgroundColor = new System.Windows.Forms.Label();
            this.splt_Canvas = new System.Windows.Forms.Splitter();
            this.lbl_canvasHeight = new System.Windows.Forms.Label();
            this.lbl_canvasWidth = new System.Windows.Forms.Label();
            this.lbl_defaultCanvasSize = new System.Windows.Forms.Label();
            this.btn_defSavPathBrowse = new System.Windows.Forms.Button();
            this.txt_defaultSavePath = new System.Windows.Forms.TextBox();
            this.lbl_defaultSavePath = new System.Windows.Forms.Label();
            this.pnl_Submit = new System.Windows.Forms.Panel();
            this.btn_submitButton = new System.Windows.Forms.Button();
            this.list_Menu = new System.Windows.Forms.ListBox();
            this.dlg_colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnl_General.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Width)).BeginInit();
            this.pnl_colorButtonHitbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_colorBox)).BeginInit();
            this.pnl_Submit.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlg_folderBrowser
            // 
            this.dlg_folderBrowser.RootFolder = System.Environment.SpecialFolder.UserProfile;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnl_General);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnl_Submit);
            this.splitContainer1.Panel2.Controls.Add(this.list_Menu);
            this.splitContainer1.Size = new System.Drawing.Size(524, 291);
            this.splitContainer1.SplitterDistance = 398;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnl_General
            // 
            this.pnl_General.Controls.Add(this.splitContainer2);
            this.pnl_General.Controls.Add(this.btn_defSavPathBrowse);
            this.pnl_General.Controls.Add(this.txt_defaultSavePath);
            this.pnl_General.Controls.Add(this.lbl_defaultSavePath);
            this.pnl_General.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_General.Location = new System.Drawing.Point(0, 0);
            this.pnl_General.Name = "pnl_General";
            this.pnl_General.Size = new System.Drawing.Size(398, 291);
            this.pnl_General.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer2.Location = new System.Drawing.Point(0, 57);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.num_Height);
            this.splitContainer2.Panel1.Controls.Add(this.num_Width);
            this.splitContainer2.Panel1.Controls.Add(this.pnl_colorButtonHitbox);
            this.splitContainer2.Panel1.Controls.Add(this.lbl_backgroundColor);
            this.splitContainer2.Panel1.Controls.Add(this.splt_Canvas);
            this.splitContainer2.Panel1.Controls.Add(this.lbl_canvasHeight);
            this.splitContainer2.Panel1.Controls.Add(this.lbl_canvasWidth);
            this.splitContainer2.Panel1.Controls.Add(this.lbl_defaultCanvasSize);
            this.splitContainer2.Size = new System.Drawing.Size(398, 234);
            this.splitContainer2.SplitterDistance = 136;
            this.splitContainer2.TabIndex = 7;
            // 
            // num_Height
            // 
            this.num_Height.Location = new System.Drawing.Point(43, 97);
            this.num_Height.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.num_Height.Name = "num_Height";
            this.num_Height.Size = new System.Drawing.Size(54, 20);
            this.num_Height.TabIndex = 18;
            // 
            // num_Width
            // 
            this.num_Width.Location = new System.Drawing.Point(43, 58);
            this.num_Width.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.num_Width.Name = "num_Width";
            this.num_Width.Size = new System.Drawing.Size(54, 20);
            this.num_Width.TabIndex = 17;
            // 
            // pnl_colorButtonHitbox
            // 
            this.pnl_colorButtonHitbox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnl_colorButtonHitbox.Controls.Add(this.lbl_backgroundColorPic);
            this.pnl_colorButtonHitbox.Controls.Add(this.pic_colorBox);
            this.pnl_colorButtonHitbox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnl_colorButtonHitbox.Location = new System.Drawing.Point(24, 173);
            this.pnl_colorButtonHitbox.Name = "pnl_colorButtonHitbox";
            this.pnl_colorButtonHitbox.Size = new System.Drawing.Size(89, 32);
            this.pnl_colorButtonHitbox.TabIndex = 16;
            this.pnl_colorButtonHitbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_colorButtonHitbox_MouseClick);
            // 
            // lbl_backgroundColorPic
            // 
            this.lbl_backgroundColorPic.AutoSize = true;
            this.lbl_backgroundColorPic.Location = new System.Drawing.Point(35, 9);
            this.lbl_backgroundColorPic.Name = "lbl_backgroundColorPic";
            this.lbl_backgroundColorPic.Size = new System.Drawing.Size(35, 13);
            this.lbl_backgroundColorPic.TabIndex = 17;
            this.lbl_backgroundColorPic.Text = "White";
            this.lbl_backgroundColorPic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_colorButtonHitbox_MouseClick);
            // 
            // pic_colorBox
            // 
            this.pic_colorBox.BackColor = System.Drawing.Color.White;
            this.pic_colorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_colorBox.Location = new System.Drawing.Point(3, 3);
            this.pic_colorBox.Name = "pic_colorBox";
            this.pic_colorBox.Size = new System.Drawing.Size(25, 25);
            this.pic_colorBox.TabIndex = 16;
            this.pic_colorBox.TabStop = false;
            this.pic_colorBox.Click += new System.EventHandler(this.pnl_colorButtonHitbox_MouseClick);
            // 
            // lbl_backgroundColor
            // 
            this.lbl_backgroundColor.AutoSize = true;
            this.lbl_backgroundColor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_backgroundColor.Location = new System.Drawing.Point(22, 136);
            this.lbl_backgroundColor.Name = "lbl_backgroundColor";
            this.lbl_backgroundColor.Size = new System.Drawing.Size(92, 13);
            this.lbl_backgroundColor.TabIndex = 13;
            this.lbl_backgroundColor.Text = "Background Color";
            // 
            // splt_Canvas
            // 
            this.splt_Canvas.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splt_Canvas.Cursor = System.Windows.Forms.Cursors.Default;
            this.splt_Canvas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splt_Canvas.Location = new System.Drawing.Point(0, 123);
            this.splt_Canvas.Name = "splt_Canvas";
            this.splt_Canvas.Size = new System.Drawing.Size(134, 109);
            this.splt_Canvas.TabIndex = 12;
            this.splt_Canvas.TabStop = false;
            // 
            // lbl_canvasHeight
            // 
            this.lbl_canvasHeight.AutoSize = true;
            this.lbl_canvasHeight.Location = new System.Drawing.Point(48, 83);
            this.lbl_canvasHeight.Name = "lbl_canvasHeight";
            this.lbl_canvasHeight.Size = new System.Drawing.Size(41, 13);
            this.lbl_canvasHeight.TabIndex = 9;
            this.lbl_canvasHeight.Text = "Height:";
            // 
            // lbl_canvasWidth
            // 
            this.lbl_canvasWidth.AutoSize = true;
            this.lbl_canvasWidth.Location = new System.Drawing.Point(48, 42);
            this.lbl_canvasWidth.Name = "lbl_canvasWidth";
            this.lbl_canvasWidth.Size = new System.Drawing.Size(38, 13);
            this.lbl_canvasWidth.TabIndex = 8;
            this.lbl_canvasWidth.Text = "Width:";
            // 
            // lbl_defaultCanvasSize
            // 
            this.lbl_defaultCanvasSize.AutoSize = true;
            this.lbl_defaultCanvasSize.Location = new System.Drawing.Point(17, 20);
            this.lbl_defaultCanvasSize.Name = "lbl_defaultCanvasSize";
            this.lbl_defaultCanvasSize.Size = new System.Drawing.Size(103, 13);
            this.lbl_defaultCanvasSize.TabIndex = 7;
            this.lbl_defaultCanvasSize.Text = "Default Canvas Size";
            // 
            // btn_defSavPathBrowse
            // 
            this.btn_defSavPathBrowse.Location = new System.Drawing.Point(320, 28);
            this.btn_defSavPathBrowse.Name = "btn_defSavPathBrowse";
            this.btn_defSavPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.btn_defSavPathBrowse.TabIndex = 2;
            this.btn_defSavPathBrowse.Text = "Browse..";
            this.btn_defSavPathBrowse.UseVisualStyleBackColor = true;
            this.btn_defSavPathBrowse.Click += new System.EventHandler(this.btn_defSavPathBrowse_Click);
            // 
            // txt_defaultSavePath
            // 
            this.txt_defaultSavePath.Enabled = false;
            this.txt_defaultSavePath.Location = new System.Drawing.Point(16, 29);
            this.txt_defaultSavePath.Name = "txt_defaultSavePath";
            this.txt_defaultSavePath.Size = new System.Drawing.Size(298, 20);
            this.txt_defaultSavePath.TabIndex = 1;
            // 
            // lbl_defaultSavePath
            // 
            this.lbl_defaultSavePath.AutoSize = true;
            this.lbl_defaultSavePath.Location = new System.Drawing.Point(13, 13);
            this.lbl_defaultSavePath.Name = "lbl_defaultSavePath";
            this.lbl_defaultSavePath.Size = new System.Drawing.Size(94, 13);
            this.lbl_defaultSavePath.TabIndex = 0;
            this.lbl_defaultSavePath.Text = "Default Save Path";
            // 
            // pnl_Submit
            // 
            this.pnl_Submit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Submit.Controls.Add(this.btn_submitButton);
            this.pnl_Submit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Submit.Location = new System.Drawing.Point(0, 255);
            this.pnl_Submit.Name = "pnl_Submit";
            this.pnl_Submit.Size = new System.Drawing.Size(122, 36);
            this.pnl_Submit.TabIndex = 1;
            // 
            // btn_submitButton
            // 
            this.btn_submitButton.Location = new System.Drawing.Point(23, 6);
            this.btn_submitButton.Name = "btn_submitButton";
            this.btn_submitButton.Size = new System.Drawing.Size(75, 23);
            this.btn_submitButton.TabIndex = 0;
            this.btn_submitButton.Text = "Submit";
            this.btn_submitButton.UseVisualStyleBackColor = true;
            this.btn_submitButton.Click += new System.EventHandler(this.btn_submitButton_Click);
            // 
            // list_Menu
            // 
            this.list_Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_Menu.FormattingEnabled = true;
            this.list_Menu.Items.AddRange(new object[] {
            "+ General",
            "+ Visuals",
            "+ Performance",
            "    -Playback",
            "+ Hotkeys"});
            this.list_Menu.Location = new System.Drawing.Point(0, 0);
            this.list_Menu.Name = "list_Menu";
            this.list_Menu.Size = new System.Drawing.Size(122, 291);
            this.list_Menu.TabIndex = 0;
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 291);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(540, 330);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(540, 330);
            this.Name = "Preferences";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Preferences - TISFAT : Zero";
            this.Load += new System.EventHandler(this.Preferences_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnl_General.ResumeLayout(false);
            this.pnl_General.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.num_Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Width)).EndInit();
            this.pnl_colorButtonHitbox.ResumeLayout(false);
            this.pnl_colorButtonHitbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_colorBox)).EndInit();
            this.pnl_Submit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog dlg_folderBrowser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnl_General;
        private System.Windows.Forms.ListBox list_Menu;
        private System.Windows.Forms.Button btn_defSavPathBrowse;
        private System.Windows.Forms.TextBox txt_defaultSavePath;
        private System.Windows.Forms.Label lbl_defaultSavePath;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lbl_backgroundColor;
        private System.Windows.Forms.Label lbl_canvasHeight;
        private System.Windows.Forms.Label lbl_canvasWidth;
        private System.Windows.Forms.Label lbl_defaultCanvasSize;
        private System.Windows.Forms.ColorDialog dlg_colorDialog;
        private System.Windows.Forms.Panel pnl_colorButtonHitbox;
        private System.Windows.Forms.Label lbl_backgroundColorPic;
        private System.Windows.Forms.PictureBox pic_colorBox;
        private System.Windows.Forms.Splitter splt_Canvas;
        private System.Windows.Forms.Panel pnl_Submit;
        private System.Windows.Forms.Button btn_submitButton;
        private System.Windows.Forms.NumericUpDown num_Height;
        private System.Windows.Forms.NumericUpDown num_Width;
    }
}