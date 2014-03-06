using OpenTK.Graphics;

namespace TISFAT_ZERO
{
	partial class StickEditor
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
			this.pnl_toolBox = new System.Windows.Forms.Panel();
			this.pnl_Stats = new System.Windows.Forms.Panel();
			this.pnl_brushProps = new System.Windows.Forms.Panel();
			this.num_brushThickness = new System.Windows.Forms.NumericUpDown();
			this.lbl_brushThickness = new System.Windows.Forms.Label();
			this.lbl_brushProps = new System.Windows.Forms.Label();
			this.chk_drawHandles = new System.Windows.Forms.CheckBox();
			this.chk_obeyIK = new System.Windows.Forms.CheckBox();
			this.pnl_toolboxMain = new System.Windows.Forms.Panel();
			this.pnl_toolPanel = new System.Windows.Forms.Panel();
			this.btn_toolRemove = new System.Windows.Forms.Button();
			this.btn_toolAdd = new System.Windows.Forms.Button();
			this.btn_toolMove = new System.Windows.Forms.Button();
			this.btn_toolPointer = new System.Windows.Forms.Button();
			this.lbl_toolBox = new System.Windows.Forms.Label();
			this.pnl_lineProps = new System.Windows.Forms.Panel();
			this.num_bitmapYOffs = new System.Windows.Forms.NumericUpDown();
			this.lbl_bitmapYOffs = new System.Windows.Forms.Label();
			this.num_bitmapXOffs = new System.Windows.Forms.NumericUpDown();
			this.lbl_bitmapXOffs = new System.Windows.Forms.Label();
			this.num_bitmapRotation = new System.Windows.Forms.NumericUpDown();
			this.lbl_bitmapRotation = new System.Windows.Forms.Label();
			this.tkb_Rotation = new System.Windows.Forms.TrackBar();
			this.num_drawOrder = new System.Windows.Forms.NumericUpDown();
			this.lbl_drawOrder = new System.Windows.Forms.Label();
			this.num_lineThickness = new System.Windows.Forms.NumericUpDown();
			this.lbl_lineThickness = new System.Windows.Forms.Label();
			this.chk_lineVisible = new System.Windows.Forms.CheckBox();
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
			this.chk_handleVisible = new System.Windows.Forms.CheckBox();
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
			this.lbl_lineLength = new System.Windows.Forms.Label();
			this.lbl_jointPosition = new System.Windows.Forms.Label();
			this.dlg_openFile = new System.Windows.Forms.OpenFileDialog();
			this.dlg_saveFile = new System.Windows.Forms.SaveFileDialog();
			this.dlg_openBitmap = new System.Windows.Forms.OpenFileDialog();
			this.GL_GRAPHICS = new OpenTK.GLControl();
			this.lbl_bitmapID = new System.Windows.Forms.Label();
			this.pnl_toolBox.SuspendLayout();
			this.pnl_Stats.SuspendLayout();
			this.pnl_brushProps.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_brushThickness)).BeginInit();
			this.pnl_toolboxMain.SuspendLayout();
			this.pnl_toolPanel.SuspendLayout();
			this.pnl_lineProps.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapYOffs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapXOffs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapRotation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tkb_Rotation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_drawOrder)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_lineThickness)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_lineAlpha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_lineColor)).BeginInit();
			this.pnl_handleProps.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_handleAlpha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_handleColor)).BeginInit();
			this.mnu_Main.SuspendLayout();
			this.SuspendLayout();
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
			this.pnl_Stats.Controls.Add(this.pnl_brushProps);
			this.pnl_Stats.Controls.Add(this.lbl_brushProps);
			this.pnl_Stats.Controls.Add(this.chk_drawHandles);
			this.pnl_Stats.Controls.Add(this.chk_obeyIK);
			this.pnl_Stats.Location = new System.Drawing.Point(0, -1);
			this.pnl_Stats.Name = "pnl_Stats";
			this.pnl_Stats.Size = new System.Drawing.Size(173, 85);
			this.pnl_Stats.TabIndex = 1;
			// 
			// pnl_brushProps
			// 
			this.pnl_brushProps.Controls.Add(this.num_brushThickness);
			this.pnl_brushProps.Controls.Add(this.lbl_brushThickness);
			this.pnl_brushProps.Location = new System.Drawing.Point(-1, 17);
			this.pnl_brushProps.Name = "pnl_brushProps";
			this.pnl_brushProps.Size = new System.Drawing.Size(174, 42);
			this.pnl_brushProps.TabIndex = 6;
			// 
			// num_brushThickness
			// 
			this.num_brushThickness.Location = new System.Drawing.Point(94, 11);
			this.num_brushThickness.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.num_brushThickness.Name = "num_brushThickness";
			this.num_brushThickness.Size = new System.Drawing.Size(46, 20);
			this.num_brushThickness.TabIndex = 1;
			this.num_brushThickness.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
			// 
			// lbl_brushThickness
			// 
			this.lbl_brushThickness.AutoSize = true;
			this.lbl_brushThickness.Location = new System.Drawing.Point(34, 13);
			this.lbl_brushThickness.Name = "lbl_brushThickness";
			this.lbl_brushThickness.Size = new System.Drawing.Size(59, 13);
			this.lbl_brushThickness.TabIndex = 0;
			this.lbl_brushThickness.Text = "Thickness:";
			// 
			// lbl_brushProps
			// 
			this.lbl_brushProps.AutoSize = true;
			this.lbl_brushProps.Location = new System.Drawing.Point(0, 2);
			this.lbl_brushProps.Name = "lbl_brushProps";
			this.lbl_brushProps.Size = new System.Drawing.Size(87, 13);
			this.lbl_brushProps.TabIndex = 5;
			this.lbl_brushProps.Text = "Brush Properties:";
			// 
			// chk_drawHandles
			// 
			this.chk_drawHandles.AutoSize = true;
			this.chk_drawHandles.Checked = true;
			this.chk_drawHandles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk_drawHandles.Location = new System.Drawing.Point(68, 65);
			this.chk_drawHandles.Name = "chk_drawHandles";
			this.chk_drawHandles.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chk_drawHandles.Size = new System.Drawing.Size(93, 17);
			this.chk_drawHandles.TabIndex = 4;
			this.chk_drawHandles.Text = "Draw Handles";
			this.chk_drawHandles.UseVisualStyleBackColor = true;
			this.chk_drawHandles.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// chk_obeyIK
			// 
			this.chk_obeyIK.AutoSize = true;
			this.chk_obeyIK.Location = new System.Drawing.Point(1, 65);
			this.chk_obeyIK.Name = "chk_obeyIK";
			this.chk_obeyIK.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chk_obeyIK.Size = new System.Drawing.Size(64, 17);
			this.chk_obeyIK.TabIndex = 3;
			this.chk_obeyIK.Text = "Obey IK";
			this.chk_obeyIK.UseVisualStyleBackColor = true;
			this.chk_obeyIK.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
			this.pnl_toolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnl_toolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_toolPanel.Controls.Add(this.btn_toolRemove);
			this.pnl_toolPanel.Controls.Add(this.btn_toolAdd);
			this.pnl_toolPanel.Controls.Add(this.btn_toolMove);
			this.pnl_toolPanel.Controls.Add(this.btn_toolPointer);
			this.pnl_toolPanel.Location = new System.Drawing.Point(0, 271);
			this.pnl_toolPanel.Name = "pnl_toolPanel";
			this.pnl_toolPanel.Size = new System.Drawing.Size(173, 153);
			this.pnl_toolPanel.TabIndex = 6;
			// 
			// btn_toolRemove
			// 
			this.btn_toolRemove.Location = new System.Drawing.Point(91, 81);
			this.btn_toolRemove.Name = "btn_toolRemove";
			this.btn_toolRemove.Size = new System.Drawing.Size(75, 62);
			this.btn_toolRemove.TabIndex = 2;
			this.btn_toolRemove.Text = "Remove Joint";
			this.btn_toolRemove.UseVisualStyleBackColor = true;
			this.btn_toolRemove.Click += new System.EventHandler(this.btn_toolRemove_Click);
			// 
			// btn_toolAdd
			// 
			this.btn_toolAdd.Location = new System.Drawing.Point(91, 6);
			this.btn_toolAdd.Name = "btn_toolAdd";
			this.btn_toolAdd.Size = new System.Drawing.Size(75, 62);
			this.btn_toolAdd.TabIndex = 1;
			this.btn_toolAdd.Text = "Add Joint";
			this.btn_toolAdd.UseVisualStyleBackColor = true;
			this.btn_toolAdd.Click += new System.EventHandler(this.btn_toolAdd_Click);
			// 
			// btn_toolMove
			// 
			this.btn_toolMove.Location = new System.Drawing.Point(5, 81);
			this.btn_toolMove.Name = "btn_toolMove";
			this.btn_toolMove.Size = new System.Drawing.Size(75, 62);
			this.btn_toolMove.TabIndex = 0;
			this.btn_toolMove.Text = "Move Joint";
			this.btn_toolMove.UseVisualStyleBackColor = true;
			this.btn_toolMove.Click += new System.EventHandler(this.btn_toolMove_Click);
			// 
			// btn_toolPointer
			// 
			this.btn_toolPointer.Location = new System.Drawing.Point(5, 6);
			this.btn_toolPointer.Name = "btn_toolPointer";
			this.btn_toolPointer.Size = new System.Drawing.Size(75, 62);
			this.btn_toolPointer.TabIndex = 0;
			this.btn_toolPointer.Text = "Pointer";
			this.btn_toolPointer.UseVisualStyleBackColor = true;
			this.btn_toolPointer.Click += new System.EventHandler(this.btn_toolPointer_Click);
			// 
			// lbl_toolBox
			// 
			this.lbl_toolBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_toolBox.Location = new System.Drawing.Point(6, 255);
			this.lbl_toolBox.Name = "lbl_toolBox";
			this.lbl_toolBox.Size = new System.Drawing.Size(45, 13);
			this.lbl_toolBox.TabIndex = 5;
			this.lbl_toolBox.Text = "Toolbox";
			// 
			// pnl_lineProps
			// 
			this.pnl_lineProps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.pnl_lineProps.AutoScroll = true;
			this.pnl_lineProps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_lineProps.Controls.Add(this.num_bitmapYOffs);
			this.pnl_lineProps.Controls.Add(this.lbl_bitmapYOffs);
			this.pnl_lineProps.Controls.Add(this.num_bitmapXOffs);
			this.pnl_lineProps.Controls.Add(this.lbl_bitmapXOffs);
			this.pnl_lineProps.Controls.Add(this.num_bitmapRotation);
			this.pnl_lineProps.Controls.Add(this.lbl_bitmapRotation);
			this.pnl_lineProps.Controls.Add(this.tkb_Rotation);
			this.pnl_lineProps.Controls.Add(this.num_drawOrder);
			this.pnl_lineProps.Controls.Add(this.lbl_drawOrder);
			this.pnl_lineProps.Controls.Add(this.num_lineThickness);
			this.pnl_lineProps.Controls.Add(this.lbl_lineThickness);
			this.pnl_lineProps.Controls.Add(this.chk_lineVisible);
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
			// num_bitmapYOffs
			// 
			this.num_bitmapYOffs.Location = new System.Drawing.Point(92, 224);
			this.num_bitmapYOffs.Maximum = new decimal(new int[] {
            900000,
            0,
            0,
            0});
			this.num_bitmapYOffs.Name = "num_bitmapYOffs";
			this.num_bitmapYOffs.Size = new System.Drawing.Size(43, 20);
			this.num_bitmapYOffs.TabIndex = 21;
			this.num_bitmapYOffs.ValueChanged += new System.EventHandler(this.num_bitmapYOffs_ValueChanged);
			// 
			// lbl_bitmapYOffs
			// 
			this.lbl_bitmapYOffs.AutoSize = true;
			this.lbl_bitmapYOffs.Location = new System.Drawing.Point(11, 227);
			this.lbl_bitmapYOffs.Name = "lbl_bitmapYOffs";
			this.lbl_bitmapYOffs.Size = new System.Drawing.Size(74, 13);
			this.lbl_bitmapYOffs.TabIndex = 20;
			this.lbl_bitmapYOffs.Text = "Bitmap Y Offs:";
			// 
			// num_bitmapXOffs
			// 
			this.num_bitmapXOffs.Location = new System.Drawing.Point(92, 198);
			this.num_bitmapXOffs.Maximum = new decimal(new int[] {
            900000,
            0,
            0,
            0});
			this.num_bitmapXOffs.Name = "num_bitmapXOffs";
			this.num_bitmapXOffs.Size = new System.Drawing.Size(43, 20);
			this.num_bitmapXOffs.TabIndex = 19;
			this.num_bitmapXOffs.ValueChanged += new System.EventHandler(this.num_bitmapXOffs_ValueChanged);
			// 
			// lbl_bitmapXOffs
			// 
			this.lbl_bitmapXOffs.AutoSize = true;
			this.lbl_bitmapXOffs.Location = new System.Drawing.Point(11, 201);
			this.lbl_bitmapXOffs.Name = "lbl_bitmapXOffs";
			this.lbl_bitmapXOffs.Size = new System.Drawing.Size(74, 13);
			this.lbl_bitmapXOffs.TabIndex = 18;
			this.lbl_bitmapXOffs.Text = "Bitmap X Offs:";
			// 
			// num_bitmapRotation
			// 
			this.num_bitmapRotation.Location = new System.Drawing.Point(91, 134);
			this.num_bitmapRotation.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.num_bitmapRotation.Name = "num_bitmapRotation";
			this.num_bitmapRotation.Size = new System.Drawing.Size(61, 20);
			this.num_bitmapRotation.TabIndex = 17;
			this.num_bitmapRotation.ValueChanged += new System.EventHandler(this.num_bitmapRotation_ValueChanged);
			// 
			// lbl_bitmapRotation
			// 
			this.lbl_bitmapRotation.AutoSize = true;
			this.lbl_bitmapRotation.Location = new System.Drawing.Point(2, 136);
			this.lbl_bitmapRotation.Name = "lbl_bitmapRotation";
			this.lbl_bitmapRotation.Size = new System.Drawing.Size(85, 13);
			this.lbl_bitmapRotation.TabIndex = 16;
			this.lbl_bitmapRotation.Text = "Bitmap Rotation:";
			// 
			// tkb_Rotation
			// 
			this.tkb_Rotation.Location = new System.Drawing.Point(-1, 152);
			this.tkb_Rotation.Maximum = 359;
			this.tkb_Rotation.Name = "tkb_Rotation";
			this.tkb_Rotation.Size = new System.Drawing.Size(152, 45);
			this.tkb_Rotation.TabIndex = 15;
			this.tkb_Rotation.TickFrequency = 30;
			this.tkb_Rotation.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.tkb_Rotation.Scroll += new System.EventHandler(this.tkb_Rotation_Scroll);
			// 
			// num_drawOrder
			// 
			this.num_drawOrder.Location = new System.Drawing.Point(91, 250);
			this.num_drawOrder.Name = "num_drawOrder";
			this.num_drawOrder.Size = new System.Drawing.Size(43, 20);
			this.num_drawOrder.TabIndex = 14;
			this.num_drawOrder.ValueChanged += new System.EventHandler(this.num_drawOrder_ValueChanged);
			// 
			// lbl_drawOrder
			// 
			this.lbl_drawOrder.AutoSize = true;
			this.lbl_drawOrder.Location = new System.Drawing.Point(21, 252);
			this.lbl_drawOrder.Name = "lbl_drawOrder";
			this.lbl_drawOrder.Size = new System.Drawing.Size(64, 13);
			this.lbl_drawOrder.TabIndex = 13;
			this.lbl_drawOrder.Text = "Draw Order:";
			// 
			// num_lineThickness
			// 
			this.num_lineThickness.Location = new System.Drawing.Point(91, 278);
			this.num_lineThickness.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.num_lineThickness.Name = "num_lineThickness";
			this.num_lineThickness.Size = new System.Drawing.Size(43, 20);
			this.num_lineThickness.TabIndex = 12;
			this.num_lineThickness.ValueChanged += new System.EventHandler(this.num_lineThickness_ValueChanged);
			// 
			// lbl_lineThickness
			// 
			this.lbl_lineThickness.AutoSize = true;
			this.lbl_lineThickness.Location = new System.Drawing.Point(31, 280);
			this.lbl_lineThickness.Name = "lbl_lineThickness";
			this.lbl_lineThickness.Size = new System.Drawing.Size(59, 13);
			this.lbl_lineThickness.TabIndex = 11;
			this.lbl_lineThickness.Text = "Thickness:";
			// 
			// chk_lineVisible
			// 
			this.chk_lineVisible.AutoSize = true;
			this.chk_lineVisible.Location = new System.Drawing.Point(49, 303);
			this.chk_lineVisible.Name = "chk_lineVisible";
			this.chk_lineVisible.Size = new System.Drawing.Size(56, 17);
			this.chk_lineVisible.TabIndex = 10;
			this.chk_lineVisible.Text = "Visible";
			this.chk_lineVisible.UseVisualStyleBackColor = true;
			// 
			// btn_remBitmap
			// 
			this.btn_remBitmap.Location = new System.Drawing.Point(96, 102);
			this.btn_remBitmap.Name = "btn_remBitmap";
			this.btn_remBitmap.Size = new System.Drawing.Size(56, 23);
			this.btn_remBitmap.TabIndex = 9;
			this.btn_remBitmap.Text = "Remove";
			this.btn_remBitmap.UseVisualStyleBackColor = true;
			// 
			// btn_addBitmap
			// 
			this.btn_addBitmap.Location = new System.Drawing.Point(38, 102);
			this.btn_addBitmap.Name = "btn_addBitmap";
			this.btn_addBitmap.Size = new System.Drawing.Size(56, 23);
			this.btn_addBitmap.TabIndex = 9;
			this.btn_addBitmap.Text = "Add";
			this.btn_addBitmap.UseVisualStyleBackColor = true;
			this.btn_addBitmap.Click += new System.EventHandler(this.btn_addBitmap_Click);
			// 
			// com_lineBitmap
			// 
			this.com_lineBitmap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.com_lineBitmap.FormattingEnabled = true;
			this.com_lineBitmap.Location = new System.Drawing.Point(39, 75);
			this.com_lineBitmap.Name = "com_lineBitmap";
			this.com_lineBitmap.Size = new System.Drawing.Size(114, 21);
			this.com_lineBitmap.TabIndex = 8;
			this.com_lineBitmap.SelectionChangeCommitted += new System.EventHandler(this.com_lineBitmap_SelectionChangeCommitted);
			// 
			// lbl_lineBitmap
			// 
			this.lbl_lineBitmap.AutoSize = true;
			this.lbl_lineBitmap.Location = new System.Drawing.Point(0, 79);
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
            "Circle"});
			this.com_lineType.Location = new System.Drawing.Point(39, 46);
			this.com_lineType.Name = "com_lineType";
			this.com_lineType.Size = new System.Drawing.Size(115, 21);
			this.com_lineType.TabIndex = 6;
			this.com_lineType.SelectedIndexChanged += new System.EventHandler(this.com_lineType_SelectedIndexChanged);
			// 
			// lbl_lineType
			// 
			this.lbl_lineType.AutoSize = true;
			this.lbl_lineType.Location = new System.Drawing.Point(2, 49);
			this.lbl_lineType.Name = "lbl_lineType";
			this.lbl_lineType.Size = new System.Drawing.Size(34, 13);
			this.lbl_lineType.TabIndex = 5;
			this.lbl_lineType.Text = "Type:";
			// 
			// num_lineAlpha
			// 
			this.num_lineAlpha.Location = new System.Drawing.Point(112, 14);
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
			this.lbl_lineAlpha.Location = new System.Drawing.Point(69, 16);
			this.lbl_lineAlpha.Name = "lbl_lineAlpha";
			this.lbl_lineAlpha.Size = new System.Drawing.Size(37, 13);
			this.lbl_lineAlpha.TabIndex = 4;
			this.lbl_lineAlpha.Text = "Alpha:";
			// 
			// pic_lineColor
			// 
			this.pic_lineColor.BackColor = System.Drawing.Color.Black;
			this.pic_lineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pic_lineColor.Location = new System.Drawing.Point(44, 10);
			this.pic_lineColor.Name = "pic_lineColor";
			this.pic_lineColor.Size = new System.Drawing.Size(25, 25);
			this.pic_lineColor.TabIndex = 3;
			this.pic_lineColor.TabStop = false;
			this.pic_lineColor.Click += new System.EventHandler(this.pic_lineColor_Click);
			// 
			// lbl_lineColor
			// 
			this.lbl_lineColor.AutoSize = true;
			this.lbl_lineColor.Location = new System.Drawing.Point(4, 16);
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
			this.pnl_handleProps.Controls.Add(this.chk_handleVisible);
			this.pnl_handleProps.Controls.Add(this.num_handleAlpha);
			this.pnl_handleProps.Controls.Add(this.lbl_handleAlpha);
			this.pnl_handleProps.Controls.Add(this.pic_handleColor);
			this.pnl_handleProps.Controls.Add(this.lbl_handleColor);
			this.pnl_handleProps.Location = new System.Drawing.Point(-1, 21);
			this.pnl_handleProps.Name = "pnl_handleProps";
			this.pnl_handleProps.Size = new System.Drawing.Size(173, 73);
			this.pnl_handleProps.TabIndex = 1;
			// 
			// chk_handleVisible
			// 
			this.chk_handleVisible.AutoSize = true;
			this.chk_handleVisible.Location = new System.Drawing.Point(57, 43);
			this.chk_handleVisible.Name = "chk_handleVisible";
			this.chk_handleVisible.Size = new System.Drawing.Size(56, 17);
			this.chk_handleVisible.TabIndex = 4;
			this.chk_handleVisible.Text = "Visible";
			this.chk_handleVisible.UseVisualStyleBackColor = true;
			this.chk_handleVisible.CheckedChanged += new System.EventHandler(this.chk_handleVisible_CheckedChanged);
			// 
			// num_handleAlpha
			// 
			this.num_handleAlpha.Location = new System.Drawing.Point(124, 15);
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
			this.lbl_handleAlpha.Location = new System.Drawing.Point(81, 17);
			this.lbl_handleAlpha.Name = "lbl_handleAlpha";
			this.lbl_handleAlpha.Size = new System.Drawing.Size(37, 13);
			this.lbl_handleAlpha.TabIndex = 2;
			this.lbl_handleAlpha.Text = "Alpha:";
			// 
			// pic_handleColor
			// 
			this.pic_handleColor.BackColor = System.Drawing.Color.Black;
			this.pic_handleColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pic_handleColor.Location = new System.Drawing.Point(47, 11);
			this.pic_handleColor.Name = "pic_handleColor";
			this.pic_handleColor.Size = new System.Drawing.Size(25, 25);
			this.pic_handleColor.TabIndex = 1;
			this.pic_handleColor.TabStop = false;
			this.pic_handleColor.Click += new System.EventHandler(this.pic_handleColor_Click);
			// 
			// lbl_handleColor
			// 
			this.lbl_handleColor.AutoSize = true;
			this.lbl_handleColor.Location = new System.Drawing.Point(7, 17);
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
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Enabled = false;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.saveAsToolStripMenuItem.Text = "Save As..";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
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
			// lbl_lineLength
			// 
			this.lbl_lineLength.AutoSize = true;
			this.lbl_lineLength.Location = new System.Drawing.Point(606, 9);
			this.lbl_lineLength.Name = "lbl_lineLength";
			this.lbl_lineLength.Size = new System.Drawing.Size(66, 13);
			this.lbl_lineLength.TabIndex = 5;
			this.lbl_lineLength.Text = "Line Length:";
			// 
			// lbl_jointPosition
			// 
			this.lbl_jointPosition.AutoSize = true;
			this.lbl_jointPosition.Location = new System.Drawing.Point(455, 9);
			this.lbl_jointPosition.Name = "lbl_jointPosition";
			this.lbl_jointPosition.Size = new System.Drawing.Size(47, 13);
			this.lbl_jointPosition.TabIndex = 6;
			this.lbl_jointPosition.Text = "Position:";
			// 
			// dlg_openFile
			// 
			this.dlg_openFile.DefaultExt = "tzf";
			this.dlg_openFile.Filter = "TISFAT Stick Figure (*.tzf)|*.tzf";
			this.dlg_openFile.FileOk += new System.ComponentModel.CancelEventHandler(this.dlg_openFile_FileOk);
			// 
			// dlg_saveFile
			// 
			this.dlg_saveFile.DefaultExt = "tzf";
			this.dlg_saveFile.Filter = "TISFAT Stick Figure (*.tzf)|*.tzf";
			this.dlg_saveFile.FileOk += new System.ComponentModel.CancelEventHandler(this.dlg_saveFile_FileOk);
			// 
			// dlg_openBitmap
			// 
			this.dlg_openBitmap.DefaultExt = "png";
			this.dlg_openBitmap.FileOk += new System.ComponentModel.CancelEventHandler(this.dlg_openBitmap_FileOk);
			// 
			// GL_GRAPHICS
			// 
			this.GL_GRAPHICS.BackColor = System.Drawing.Color.Black;
			this.GL_GRAPHICS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GL_GRAPHICS.Location = new System.Drawing.Point(0, 24);
			this.GL_GRAPHICS.Name = "GL_GRAPHICS";
			this.GL_GRAPHICS.Size = new System.Drawing.Size(710, 487);
			this.GL_GRAPHICS.TabIndex = 7;
			this.GL_GRAPHICS.VSync = false;
			this.GL_GRAPHICS.Paint += new System.Windows.Forms.PaintEventHandler(this.GL_GRAPHICS_Paint);
			this.GL_GRAPHICS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GL_GRAPHICS_MouseDown);
			this.GL_GRAPHICS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GL_GRAPHICS_MouseMove);
			this.GL_GRAPHICS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GL_GRAPHICS_MouseUp);
			this.GL_GRAPHICS.Resize += new System.EventHandler(this.GL_GRAPHICS_Resize);
			// 
			// lbl_bitmapID
			// 
			this.lbl_bitmapID.AutoSize = true;
			this.lbl_bitmapID.Location = new System.Drawing.Point(375, 9);
			this.lbl_bitmapID.Name = "lbl_bitmapID";
			this.lbl_bitmapID.Size = new System.Drawing.Size(53, 13);
			this.lbl_bitmapID.TabIndex = 8;
			this.lbl_bitmapID.Text = "BitmapID:";
			// 
			// StickEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 511);
			this.Controls.Add(this.lbl_bitmapID);
			this.Controls.Add(this.GL_GRAPHICS);
			this.Controls.Add(this.lbl_jointPosition);
			this.Controls.Add(this.lbl_lineLength);
			this.Controls.Add(this.mnu_Main);
			this.Controls.Add(this.pnl_toolBox);
			this.MainMenuStrip = this.mnu_Main;
			this.MinimumSize = new System.Drawing.Size(900, 550);
			this.Name = "StickEditor";
			this.Text = "Stick Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StickEditor_FormClosing);
			this.Load += new System.EventHandler(this.StickEditor_Load);
			this.pnl_toolBox.ResumeLayout(false);
			this.pnl_Stats.ResumeLayout(false);
			this.pnl_Stats.PerformLayout();
			this.pnl_brushProps.ResumeLayout(false);
			this.pnl_brushProps.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_brushThickness)).EndInit();
			this.pnl_toolboxMain.ResumeLayout(false);
			this.pnl_toolboxMain.PerformLayout();
			this.pnl_toolPanel.ResumeLayout(false);
			this.pnl_lineProps.ResumeLayout(false);
			this.pnl_lineProps.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapYOffs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapXOffs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapRotation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tkb_Rotation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_drawOrder)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_lineThickness)).EndInit();
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
		private System.Windows.Forms.CheckBox chk_drawHandles;
		private System.Windows.Forms.CheckBox chk_obeyIK;
		private System.Windows.Forms.Panel pnl_toolPanel;
		private System.Windows.Forms.Label lbl_toolBox;
		private System.Windows.Forms.Panel pnl_lineProps;
		private System.Windows.Forms.Button btn_remBitmap;
		private System.Windows.Forms.Button btn_addBitmap;
		private System.Windows.Forms.Label lbl_lineBitmap;
		private System.Windows.Forms.ComboBox com_lineType;
		private System.Windows.Forms.Label lbl_lineType;
		private System.Windows.Forms.NumericUpDown num_lineAlpha;
		private System.Windows.Forms.Label lbl_lineAlpha;
		private System.Windows.Forms.PictureBox pic_lineColor;
		private System.Windows.Forms.Label lbl_lineColor;
		private System.Windows.Forms.Label lbl_lineProps;
		private System.Windows.Forms.Button btn_toolRemove;
		private System.Windows.Forms.Button btn_toolAdd;
		private System.Windows.Forms.Button btn_toolPointer;
		private System.Windows.Forms.Button btn_toolMove;
		private System.Windows.Forms.Panel pnl_brushProps;
		private System.Windows.Forms.NumericUpDown num_brushThickness;
		private System.Windows.Forms.Label lbl_brushThickness;
		private System.Windows.Forms.Label lbl_brushProps;
		private System.Windows.Forms.CheckBox chk_handleVisible;
		private System.Windows.Forms.NumericUpDown num_lineThickness;
		private System.Windows.Forms.Label lbl_lineThickness;
		private System.Windows.Forms.CheckBox chk_lineVisible;
		private System.Windows.Forms.NumericUpDown num_drawOrder;
		private System.Windows.Forms.Label lbl_drawOrder;
		private System.Windows.Forms.Label lbl_lineLength;
		private System.Windows.Forms.Label lbl_jointPosition;
		private System.Windows.Forms.OpenFileDialog dlg_openFile;
		private System.Windows.Forms.SaveFileDialog dlg_saveFile;
		private System.Windows.Forms.OpenFileDialog dlg_openBitmap;
		private OpenTK.GLControl GL_GRAPHICS;
		public System.Windows.Forms.ComboBox com_lineBitmap;
		private System.Windows.Forms.Label lbl_bitmapID;
		private System.Windows.Forms.NumericUpDown num_bitmapYOffs;
		private System.Windows.Forms.Label lbl_bitmapYOffs;
		private System.Windows.Forms.NumericUpDown num_bitmapXOffs;
		private System.Windows.Forms.Label lbl_bitmapXOffs;
		private System.Windows.Forms.NumericUpDown num_bitmapRotation;
		private System.Windows.Forms.Label lbl_bitmapRotation;
		private System.Windows.Forms.TrackBar tkb_Rotation;

	}
}