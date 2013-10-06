namespace TISFAT_ZERO
{
    partial class Sticked
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
            this.GL_GRAPHICS = new OpenTK.GLControl();
            this.pnl_toolBox = new System.Windows.Forms.Panel();
            this.pnl_Stats = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbl_Handles = new System.Windows.Forms.Label();
            this.lbl_Segments = new System.Windows.Forms.Label();
            this.pnl_toolboxMain = new System.Windows.Forms.Panel();
            this.pnl_toolPanel = new System.Windows.Forms.Panel();
            this.lbl_toolBox = new System.Windows.Forms.Label();
            this.pnl_lineProps = new System.Windows.Forms.Panel();
            this.btn_remBitmap = new System.Windows.Forms.Button();
            this.btn_addBitmap = new System.Windows.Forms.Button();
            this.com_lineBitmap = new System.Windows.Forms.ComboBox();
            this.lbl_lineBitmap = new System.Windows.Forms.Label();
            this.com_lineType = new System.Windows.Forms.ComboBox();
            this.lbl_lineType = new System.Windows.Forms.Label();
            this.num_lineAlpha = new System.Windows.Forms.NumericUpDown();
            this.lbl_lineAlpha = new System.Windows.Forms.Label();
            this.pic_lineColor = new System.Windows.Forms.PictureBox();
            this.lbl_lineColor = new System.Windows.Forms.Label();
            this.lbl_lineProps = new System.Windows.Forms.Label();
            this.pnl_handleProps = new System.Windows.Forms.Panel();
            this.num_handleAlpha = new System.Windows.Forms.NumericUpDown();
            this.lbl_handleAlpha = new System.Windows.Forms.Label();
            this.pic_handleColor = new System.Windows.Forms.PictureBox();
            this.lbl_handleColor = new System.Windows.Forms.Label();
            this.lbl_handleProps = new System.Windows.Forms.Label();
            this.dlg_Color = new System.Windows.Forms.ColorDialog();
            this.mnu_Main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originalTisfatFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitStickEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_toolBox.SuspendLayout();
            this.pnl_Stats.SuspendLayout();
            this.pnl_toolboxMain.SuspendLayout();
            this.pnl_lineProps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_lineAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_lineColor)).BeginInit();
            this.pnl_handleProps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_handleAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_handleColor)).BeginInit();
            this.mnu_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // GL_GRAPHICS
            // 
            this.GL_GRAPHICS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GL_GRAPHICS.BackColor = System.Drawing.Color.Black;
            this.GL_GRAPHICS.Location = new System.Drawing.Point(0, 27);
            this.GL_GRAPHICS.Name = "GL_GRAPHICS";
            this.GL_GRAPHICS.Size = new System.Drawing.Size(711, 484);
            this.GL_GRAPHICS.TabIndex = 2;
            this.GL_GRAPHICS.VSync = false;
            this.GL_GRAPHICS.Paint += new System.Windows.Forms.PaintEventHandler(this.GL_GRAPHICS_Paint);
            this.GL_GRAPHICS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GL_GRAPHICS_MouseDown);
            this.GL_GRAPHICS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GL_GRAPHICS_MouseMove);
            this.GL_GRAPHICS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GL_GRAPHICS_MouseUp);
            this.GL_GRAPHICS.Resize += new System.EventHandler(this.GL_GRAPHICS_Resize);
            // 
            // pnl_toolBox
            // 
            this.pnl_toolBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_toolBox.Controls.Add(this.pnl_Stats);
            this.pnl_toolBox.Controls.Add(this.pnl_toolboxMain);
            this.pnl_toolBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_toolBox.Location = new System.Drawing.Point(710, 0);
            this.pnl_toolBox.Name = "pnl_toolBox";
            this.pnl_toolBox.Size = new System.Drawing.Size(174, 511);
            this.pnl_toolBox.TabIndex = 3;
            // 
            // pnl_Stats
            // 
            this.pnl_Stats.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnl_Stats.Controls.Add(this.checkBox2);
            this.pnl_Stats.Controls.Add(this.checkBox1);
            this.pnl_Stats.Controls.Add(this.lbl_Handles);
            this.pnl_Stats.Controls.Add(this.lbl_Segments);
            this.pnl_Stats.Location = new System.Drawing.Point(0, -1);
            this.pnl_Stats.Name = "pnl_Stats";
            this.pnl_Stats.Size = new System.Drawing.Size(173, 85);
            this.pnl_Stats.TabIndex = 1;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(68, 65);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox2.Size = new System.Drawing.Size(93, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Draw Handles";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1, 65);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(64, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Obey IK";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lbl_Handles
            // 
            this.lbl_Handles.AutoSize = true;
            this.lbl_Handles.Location = new System.Drawing.Point(93, 9);
            this.lbl_Handles.Name = "lbl_Handles";
            this.lbl_Handles.Size = new System.Drawing.Size(52, 13);
            this.lbl_Handles.TabIndex = 1;
            this.lbl_Handles.Text = "Handles: ";
            // 
            // lbl_Segments
            // 
            this.lbl_Segments.AutoSize = true;
            this.lbl_Segments.Location = new System.Drawing.Point(6, 9);
            this.lbl_Segments.Name = "lbl_Segments";
            this.lbl_Segments.Size = new System.Drawing.Size(57, 13);
            this.lbl_Segments.TabIndex = 0;
            this.lbl_Segments.Text = "Segments:";
            // 
            // pnl_toolboxMain
            // 
            this.pnl_toolboxMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnl_toolboxMain.BackColor = System.Drawing.Color.Silver;
            this.pnl_toolboxMain.Controls.Add(this.pnl_toolPanel);
            this.pnl_toolboxMain.Controls.Add(this.lbl_toolBox);
            this.pnl_toolboxMain.Controls.Add(this.pnl_lineProps);
            this.pnl_toolboxMain.Controls.Add(this.lbl_lineProps);
            this.pnl_toolboxMain.Controls.Add(this.pnl_handleProps);
            this.pnl_toolboxMain.Controls.Add(this.lbl_handleProps);
            this.pnl_toolboxMain.Location = new System.Drawing.Point(0, 85);
            this.pnl_toolboxMain.Name = "pnl_toolboxMain";
            this.pnl_toolboxMain.Size = new System.Drawing.Size(172, 424);
            this.pnl_toolboxMain.TabIndex = 0;
            // 
            // pnl_toolPanel
            // 
            this.pnl_toolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_toolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_toolPanel.Location = new System.Drawing.Point(0, 271);
            this.pnl_toolPanel.Name = "pnl_toolPanel";
            this.pnl_toolPanel.Size = new System.Drawing.Size(173, 153);
            this.pnl_toolPanel.TabIndex = 6;
            // 
            // lbl_toolBox
            // 
            this.lbl_toolBox.AutoSize = true;
            this.lbl_toolBox.Location = new System.Drawing.Point(6, 255);
            this.lbl_toolBox.Name = "lbl_toolBox";
            this.lbl_toolBox.Size = new System.Drawing.Size(45, 13);
            this.lbl_toolBox.TabIndex = 5;
            this.lbl_toolBox.Text = "Toolbox";
            // 
            // pnl_lineProps
            // 
            this.pnl_lineProps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_lineProps.Controls.Add(this.btn_remBitmap);
            this.pnl_lineProps.Controls.Add(this.btn_addBitmap);
            this.pnl_lineProps.Controls.Add(this.com_lineBitmap);
            this.pnl_lineProps.Controls.Add(this.lbl_lineBitmap);
            this.pnl_lineProps.Controls.Add(this.com_lineType);
            this.pnl_lineProps.Controls.Add(this.lbl_lineType);
            this.pnl_lineProps.Controls.Add(this.num_lineAlpha);
            this.pnl_lineProps.Controls.Add(this.lbl_lineAlpha);
            this.pnl_lineProps.Controls.Add(this.pic_lineColor);
            this.pnl_lineProps.Controls.Add(this.lbl_lineColor);
            this.pnl_lineProps.Location = new System.Drawing.Point(0, 113);
            this.pnl_lineProps.Name = "pnl_lineProps";
            this.pnl_lineProps.Size = new System.Drawing.Size(173, 139);
            this.pnl_lineProps.TabIndex = 4;
            // 
            // btn_remBitmap
            // 
            this.btn_remBitmap.Location = new System.Drawing.Point(103, 102);
            this.btn_remBitmap.Name = "btn_remBitmap";
            this.btn_remBitmap.Size = new System.Drawing.Size(56, 23);
            this.btn_remBitmap.TabIndex = 9;
            this.btn_remBitmap.Text = "Remove";
            this.btn_remBitmap.UseVisualStyleBackColor = true;
            // 
            // btn_addBitmap
            // 
            this.btn_addBitmap.Location = new System.Drawing.Point(45, 102);
            this.btn_addBitmap.Name = "btn_addBitmap";
            this.btn_addBitmap.Size = new System.Drawing.Size(56, 23);
            this.btn_addBitmap.TabIndex = 9;
            this.btn_addBitmap.Text = "Add";
            this.btn_addBitmap.UseVisualStyleBackColor = true;
            // 
            // com_lineBitmap
            // 
            this.com_lineBitmap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_lineBitmap.FormattingEnabled = true;
            this.com_lineBitmap.Location = new System.Drawing.Point(42, 75);
            this.com_lineBitmap.Name = "com_lineBitmap";
            this.com_lineBitmap.Size = new System.Drawing.Size(121, 21);
            this.com_lineBitmap.TabIndex = 8;
            // 
            // lbl_lineBitmap
            // 
            this.lbl_lineBitmap.AutoSize = true;
            this.lbl_lineBitmap.Location = new System.Drawing.Point(3, 79);
            this.lbl_lineBitmap.Name = "lbl_lineBitmap";
            this.lbl_lineBitmap.Size = new System.Drawing.Size(42, 13);
            this.lbl_lineBitmap.TabIndex = 7;
            this.lbl_lineBitmap.Text = "Bitmap:";
            // 
            // com_lineType
            // 
            this.com_lineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_lineType.FormattingEnabled = true;
            this.com_lineType.Items.AddRange(new object[] {
            "Line",
            "Rectangle",
            "Circle"});
            this.com_lineType.Location = new System.Drawing.Point(43, 46);
            this.com_lineType.Name = "com_lineType";
            this.com_lineType.Size = new System.Drawing.Size(121, 21);
            this.com_lineType.TabIndex = 6;
            // 
            // lbl_lineType
            // 
            this.lbl_lineType.AutoSize = true;
            this.lbl_lineType.Location = new System.Drawing.Point(6, 49);
            this.lbl_lineType.Name = "lbl_lineType";
            this.lbl_lineType.Size = new System.Drawing.Size(34, 13);
            this.lbl_lineType.TabIndex = 5;
            this.lbl_lineType.Text = "Type:";
            // 
            // num_lineAlpha
            // 
            this.num_lineAlpha.Location = new System.Drawing.Point(122, 14);
            this.num_lineAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_lineAlpha.Name = "num_lineAlpha";
            this.num_lineAlpha.Size = new System.Drawing.Size(41, 20);
            this.num_lineAlpha.TabIndex = 5;
            this.num_lineAlpha.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_lineAlpha.ValueChanged += new System.EventHandler(this.num_lineAlpha_ValueChanged);
            // 
            // lbl_lineAlpha
            // 
            this.lbl_lineAlpha.AutoSize = true;
            this.lbl_lineAlpha.Location = new System.Drawing.Point(79, 16);
            this.lbl_lineAlpha.Name = "lbl_lineAlpha";
            this.lbl_lineAlpha.Size = new System.Drawing.Size(37, 13);
            this.lbl_lineAlpha.TabIndex = 4;
            this.lbl_lineAlpha.Text = "Alpha:";
            // 
            // pic_lineColor
            // 
            this.pic_lineColor.BackColor = System.Drawing.Color.Black;
            this.pic_lineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_lineColor.Location = new System.Drawing.Point(45, 10);
            this.pic_lineColor.Name = "pic_lineColor";
            this.pic_lineColor.Size = new System.Drawing.Size(25, 25);
            this.pic_lineColor.TabIndex = 3;
            this.pic_lineColor.TabStop = false;
            this.pic_lineColor.Click += new System.EventHandler(this.pic_lineColor_Click);
            // 
            // lbl_lineColor
            // 
            this.lbl_lineColor.AutoSize = true;
            this.lbl_lineColor.Location = new System.Drawing.Point(5, 16);
            this.lbl_lineColor.Name = "lbl_lineColor";
            this.lbl_lineColor.Size = new System.Drawing.Size(37, 13);
            this.lbl_lineColor.TabIndex = 2;
            this.lbl_lineColor.Text = "Color: ";
            // 
            // lbl_lineProps
            // 
            this.lbl_lineProps.AutoSize = true;
            this.lbl_lineProps.Location = new System.Drawing.Point(6, 97);
            this.lbl_lineProps.Name = "lbl_lineProps";
            this.lbl_lineProps.Size = new System.Drawing.Size(80, 13);
            this.lbl_lineProps.TabIndex = 2;
            this.lbl_lineProps.Text = "Line Properties:";
            // 
            // pnl_handleProps
            // 
            this.pnl_handleProps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_handleProps.Controls.Add(this.num_handleAlpha);
            this.pnl_handleProps.Controls.Add(this.lbl_handleAlpha);
            this.pnl_handleProps.Controls.Add(this.pic_handleColor);
            this.pnl_handleProps.Controls.Add(this.lbl_handleColor);
            this.pnl_handleProps.Location = new System.Drawing.Point(-1, 21);
            this.pnl_handleProps.Name = "pnl_handleProps";
            this.pnl_handleProps.Size = new System.Drawing.Size(173, 73);
            this.pnl_handleProps.TabIndex = 1;
            // 
            // num_handleAlpha
            // 
            this.num_handleAlpha.Location = new System.Drawing.Point(124, 28);
            this.num_handleAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_handleAlpha.Name = "num_handleAlpha";
            this.num_handleAlpha.Size = new System.Drawing.Size(41, 20);
            this.num_handleAlpha.TabIndex = 3;
            this.num_handleAlpha.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_handleAlpha.ValueChanged += new System.EventHandler(this.num_handleAlpha_ValueChanged);
            // 
            // lbl_handleAlpha
            // 
            this.lbl_handleAlpha.AutoSize = true;
            this.lbl_handleAlpha.Location = new System.Drawing.Point(81, 30);
            this.lbl_handleAlpha.Name = "lbl_handleAlpha";
            this.lbl_handleAlpha.Size = new System.Drawing.Size(37, 13);
            this.lbl_handleAlpha.TabIndex = 2;
            this.lbl_handleAlpha.Text = "Alpha:";
            // 
            // pic_handleColor
            // 
            this.pic_handleColor.BackColor = System.Drawing.Color.Black;
            this.pic_handleColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_handleColor.Location = new System.Drawing.Point(47, 24);
            this.pic_handleColor.Name = "pic_handleColor";
            this.pic_handleColor.Size = new System.Drawing.Size(25, 25);
            this.pic_handleColor.TabIndex = 1;
            this.pic_handleColor.TabStop = false;
            this.pic_handleColor.Click += new System.EventHandler(this.pic_handleColor_Click);
            // 
            // lbl_handleColor
            // 
            this.lbl_handleColor.AutoSize = true;
            this.lbl_handleColor.Location = new System.Drawing.Point(7, 30);
            this.lbl_handleColor.Name = "lbl_handleColor";
            this.lbl_handleColor.Size = new System.Drawing.Size(37, 13);
            this.lbl_handleColor.TabIndex = 0;
            this.lbl_handleColor.Text = "Color: ";
            // 
            // lbl_handleProps
            // 
            this.lbl_handleProps.AutoSize = true;
            this.lbl_handleProps.Location = new System.Drawing.Point(6, 5);
            this.lbl_handleProps.Name = "lbl_handleProps";
            this.lbl_handleProps.Size = new System.Drawing.Size(94, 13);
            this.lbl_handleProps.TabIndex = 0;
            this.lbl_handleProps.Text = "Handle Properties:";
            // 
            // mnu_Main
            // 
            this.mnu_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnu_Main.Location = new System.Drawing.Point(0, 0);
            this.mnu_Main.Name = "mnu_Main";
            this.mnu_Main.Size = new System.Drawing.Size(710, 24);
            this.mnu_Main.TabIndex = 4;
            this.mnu_Main.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.importToolStripMenuItem,
            this.exitStickEditorToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.newToolStripMenuItem.Text = "New..";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveAsToolStripMenuItem.Text = "Save As..";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.originalTisfatFileToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.importToolStripMenuItem.Text = "Import..";
            // 
            // originalTisfatFileToolStripMenuItem
            // 
            this.originalTisfatFileToolStripMenuItem.Name = "originalTisfatFileToolStripMenuItem";
            this.originalTisfatFileToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.originalTisfatFileToolStripMenuItem.Text = "Original Tisfat (.sff)";
            // 
            // exitStickEditorToolStripMenuItem
            // 
            this.exitStickEditorToolStripMenuItem.Name = "exitStickEditorToolStripMenuItem";
            this.exitStickEditorToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exitStickEditorToolStripMenuItem.Text = "Exit Stick Editor";
            this.exitStickEditorToolStripMenuItem.Click += new System.EventHandler(this.exitStickEditorToolStripMenuItem_Click);
            // 
            // Sticked
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.mnu_Main);
            this.Controls.Add(this.pnl_toolBox);
            this.Controls.Add(this.GL_GRAPHICS);
            this.MainMenuStrip = this.mnu_Main;
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "Sticked";
            this.Text = "Stick Editor";
            this.Load += new System.EventHandler(this.Sticked_Load);
            this.pnl_toolBox.ResumeLayout(false);
            this.pnl_Stats.ResumeLayout(false);
            this.pnl_Stats.PerformLayout();
            this.pnl_toolboxMain.ResumeLayout(false);
            this.pnl_toolboxMain.PerformLayout();
            this.pnl_lineProps.ResumeLayout(false);
            this.pnl_lineProps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_lineAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_lineColor)).EndInit();
            this.pnl_handleProps.ResumeLayout(false);
            this.pnl_handleProps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_handleAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_handleColor)).EndInit();
            this.mnu_Main.ResumeLayout(false);
            this.mnu_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl GL_GRAPHICS;
        private System.Windows.Forms.Panel pnl_toolBox;
        private System.Windows.Forms.Panel pnl_toolboxMain;
        private System.Windows.Forms.Panel pnl_handleProps;
        private System.Windows.Forms.PictureBox pic_handleColor;
        private System.Windows.Forms.Label lbl_handleColor;
        private System.Windows.Forms.Label lbl_handleProps;
        private System.Windows.Forms.ColorDialog dlg_Color;
        private System.Windows.Forms.NumericUpDown num_handleAlpha;
        private System.Windows.Forms.Label lbl_handleAlpha;
        private System.Windows.Forms.Panel pnl_Stats;
        private System.Windows.Forms.MenuStrip mnu_Main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originalTisfatFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitStickEditorToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lbl_Handles;
        private System.Windows.Forms.Label lbl_Segments;
        private System.Windows.Forms.Panel pnl_toolPanel;
        private System.Windows.Forms.Label lbl_toolBox;
        private System.Windows.Forms.Panel pnl_lineProps;
        private System.Windows.Forms.Button btn_remBitmap;
        private System.Windows.Forms.Button btn_addBitmap;
        private System.Windows.Forms.ComboBox com_lineBitmap;
        private System.Windows.Forms.Label lbl_lineBitmap;
        private System.Windows.Forms.ComboBox com_lineType;
        private System.Windows.Forms.Label lbl_lineType;
        private System.Windows.Forms.NumericUpDown num_lineAlpha;
        private System.Windows.Forms.Label lbl_lineAlpha;
        private System.Windows.Forms.PictureBox pic_lineColor;
        private System.Windows.Forms.Label lbl_lineColor;
        private System.Windows.Forms.Label lbl_lineProps;

    }
}