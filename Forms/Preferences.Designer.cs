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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("General", System.Windows.Forms.HorizontalAlignment.Center);
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Properties");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Updates");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
			this.dlg_folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.pnl_Updates = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.pnl_Submit = new System.Windows.Forms.Panel();
			this.btn_submitButton = new System.Windows.Forms.Button();
			this.dlg_colorDialog = new System.Windows.Forms.ColorDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.pnl_Updates.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.pnl_Updates);
			this.splitContainer1.Panel1.Controls.Add(this.pnl_General);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.listView1);
			this.splitContainer1.Panel2.Controls.Add(this.pnl_Submit);
			this.splitContainer1.Size = new System.Drawing.Size(524, 292);
			this.splitContainer1.SplitterDistance = 398;
			this.splitContainer1.TabIndex = 0;
			// 
			// pnl_Updates
			// 
			this.pnl_Updates.Controls.Add(this.label5);
			this.pnl_Updates.Controls.Add(this.label4);
			this.pnl_Updates.Controls.Add(this.checkBox4);
			this.pnl_Updates.Controls.Add(this.checkBox3);
			this.pnl_Updates.Controls.Add(this.checkBox2);
			this.pnl_Updates.Controls.Add(this.label3);
			this.pnl_Updates.Controls.Add(this.label2);
			this.pnl_Updates.Controls.Add(this.comboBox1);
			this.pnl_Updates.Controls.Add(this.checkBox1);
			this.pnl_Updates.Controls.Add(this.label1);
			this.pnl_Updates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_Updates.Location = new System.Drawing.Point(0, 0);
			this.pnl_Updates.Name = "pnl_Updates";
			this.pnl_Updates.Size = new System.Drawing.Size(398, 292);
			this.pnl_Updates.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
			this.label5.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.label5.Location = new System.Drawing.Point(32, 195);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(335, 12);
			this.label5.TabIndex = 6;
			this.label5.Text = "These versions will automatically update when TISFAT:Zero checks for updates.";
			this.label5.Visible = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(144, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Versions to download:";
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Enabled = false;
			this.checkBox4.Location = new System.Drawing.Point(256, 171);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(58, 17);
			this.checkBox4.TabIndex = 4;
			this.checkBox4.Text = "Nightly";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Enabled = false;
			this.checkBox3.Location = new System.Drawing.Point(170, 171);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(48, 17);
			this.checkBox3.TabIndex = 4;
			this.checkBox3.Text = "Beta";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Enabled = false;
			this.checkBox2.Location = new System.Drawing.Point(84, 171);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(56, 17);
			this.checkBox2.TabIndex = 4;
			this.checkBox2.Text = "Stable";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(166, 75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Use Version:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
			this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.label2.Location = new System.Drawing.Point(12, 115);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(372, 29);
			this.label2.TabIndex = 2;
			this.label2.Text = "You shouldn\'t see this";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "Stable (Reccomended)",
            "Beta",
            "Nightly (Not Reccomended)"});
			this.comboBox1.Location = new System.Drawing.Point(93, 91);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(212, 21);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(104, 266);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(191, 17);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "Automatically Update TISFAT:Zero";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Build Version: 2.0.1.12";
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
			this.pnl_General.Size = new System.Drawing.Size(398, 292);
			this.pnl_General.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitContainer2.Location = new System.Drawing.Point(0, 58);
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
			// listView1
			// 
			this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			listViewGroup1.Header = "General";
			listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
			listViewGroup1.Name = "lvg_General";
			this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.HideSelection = false;
			listViewItem1.Group = listViewGroup1;
			listViewItem1.StateImageIndex = 0;
			listViewItem2.Group = listViewGroup1;
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(122, 256);
			this.listView1.StateImageList = this.imageList1;
			this.listView1.TabIndex = 2;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.SmallIcon;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "bullet_arrow_right.png");
			this.imageList1.Images.SetKeyName(1, "bullet_arrow_down.png");
			// 
			// pnl_Submit
			// 
			this.pnl_Submit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_Submit.Controls.Add(this.btn_submitButton);
			this.pnl_Submit.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnl_Submit.Location = new System.Drawing.Point(0, 256);
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
			// Preferences
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(524, 292);
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
			this.pnl_Updates.ResumeLayout(false);
			this.pnl_Updates.PerformLayout();
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
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Panel pnl_Updates;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
	}
}