namespace TISFAT
{
	partial class StickEditorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StickEditorForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pnl_Toolbox = new System.Windows.Forms.Panel();
			this.ckb_DrawHandles = new System.Windows.Forms.CheckBox();
			this.ckb_EnableIK = new System.Windows.Forms.CheckBox();
			this.lbl_MousePosition = new System.Windows.Forms.Label();
			this.btn_editModePointer = new TISFAT.Controls.BitmapButtonControl();
			this.btn_editModeAdd = new TISFAT.Controls.BitmapButtonControl();
			this.btn_editModeMove = new TISFAT.Controls.BitmapButtonControl();
			this.btn_editModeDelete = new TISFAT.Controls.BitmapButtonControl();
			this.btn_Redo = new TISFAT.Controls.BitmapButtonControl();
			this.btn_Undo = new TISFAT.Controls.BitmapButtonControl();
			this.separatorControl1 = new TISFAT.UI.Controls.SeparatorControl();
			this.btn_SaveProject = new TISFAT.Controls.BitmapButtonControl();
			this.btn_OpenProject = new TISFAT.Controls.BitmapButtonControl();
			this.btn_NewProject = new TISFAT.Controls.BitmapButtonControl();
			this.pnl_GLArea = new System.Windows.Forms.Panel();
			this.pnl_SideBar = new System.Windows.Forms.Panel();
			this.grp_jointProperties = new System.Windows.Forms.GroupBox();
			this.pnl_drawMode = new System.Windows.Forms.Panel();
			this.cmb_drawMode = new System.Windows.Forms.ComboBox();
			this.lbl_drawMode = new System.Windows.Forms.Label();
			this.panel9 = new System.Windows.Forms.Panel();
			this.num_jointThickness = new System.Windows.Forms.NumericUpDown();
			this.lbl_jointThickness = new System.Windows.Forms.Label();
			this.pnl_jointColor = new System.Windows.Forms.Panel();
			this.pnl_jointColorImgBorder = new System.Windows.Forms.Panel();
			this.pnl_jointColorImg = new System.Windows.Forms.Panel();
			this.lbl_jointColorNumbers = new System.Windows.Forms.Label();
			this.lbl_jointColor = new System.Windows.Forms.Label();
			this.grp_jointBitmaps = new System.Windows.Forms.GroupBox();
			this.pnl_Bitmaps = new System.Windows.Forms.Panel();
			this.num_bitmapYOffset = new System.Windows.Forms.NumericUpDown();
			this.btn_bitmapRemove = new System.Windows.Forms.Button();
			this.num_bitmapXOffset = new System.Windows.Forms.NumericUpDown();
			this.btn_bitmapAdd = new System.Windows.Forms.Button();
			this.lbl_bitmapYOffset = new System.Windows.Forms.Label();
			this.lbl_bitmapXOffset = new System.Windows.Forms.Label();
			this.lbl_bitmaps = new System.Windows.Forms.Label();
			this.cmb_bitmaps = new System.Windows.Forms.ComboBox();
			this.grp_handleProperties = new System.Windows.Forms.GroupBox();
			this.ckb_handleVisible = new System.Windows.Forms.CheckBox();
			this.pnl_handleColor = new System.Windows.Forms.Panel();
			this.pnl_handleColorImgBorder = new System.Windows.Forms.Panel();
			this.pnl_handleColorImg = new System.Windows.Forms.Panel();
			this.lbl_handleColorNumbers = new System.Windows.Forms.Label();
			this.lbl_handleColor = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btn_OK = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.pnl_Toolbox.SuspendLayout();
			this.pnl_SideBar.SuspendLayout();
			this.grp_jointProperties.SuspendLayout();
			this.pnl_drawMode.SuspendLayout();
			this.panel9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_jointThickness)).BeginInit();
			this.pnl_jointColor.SuspendLayout();
			this.pnl_jointColorImgBorder.SuspendLayout();
			this.grp_jointBitmaps.SuspendLayout();
			this.pnl_Bitmaps.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapYOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapXOffset)).BeginInit();
			this.grp_handleProperties.SuspendLayout();
			this.pnl_handleColor.SuspendLayout();
			this.pnl_handleColorImgBorder.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(844, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Enabled = false;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.saveAsToolStripMenuItem.Text = "Save As..";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// pnl_Toolbox
			// 
			this.pnl_Toolbox.Controls.Add(this.ckb_DrawHandles);
			this.pnl_Toolbox.Controls.Add(this.ckb_EnableIK);
			this.pnl_Toolbox.Controls.Add(this.lbl_MousePosition);
			this.pnl_Toolbox.Controls.Add(this.btn_editModePointer);
			this.pnl_Toolbox.Controls.Add(this.btn_editModeAdd);
			this.pnl_Toolbox.Controls.Add(this.btn_editModeMove);
			this.pnl_Toolbox.Controls.Add(this.btn_editModeDelete);
			this.pnl_Toolbox.Controls.Add(this.btn_Redo);
			this.pnl_Toolbox.Controls.Add(this.btn_Undo);
			this.pnl_Toolbox.Controls.Add(this.separatorControl1);
			this.pnl_Toolbox.Controls.Add(this.btn_SaveProject);
			this.pnl_Toolbox.Controls.Add(this.btn_OpenProject);
			this.pnl_Toolbox.Controls.Add(this.btn_NewProject);
			this.pnl_Toolbox.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl_Toolbox.Location = new System.Drawing.Point(0, 24);
			this.pnl_Toolbox.Margin = new System.Windows.Forms.Padding(0);
			this.pnl_Toolbox.Name = "pnl_Toolbox";
			this.pnl_Toolbox.Size = new System.Drawing.Size(844, 30);
			this.pnl_Toolbox.TabIndex = 2;
			// 
			// ckb_DrawHandles
			// 
			this.ckb_DrawHandles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ckb_DrawHandles.AutoSize = true;
			this.ckb_DrawHandles.Checked = true;
			this.ckb_DrawHandles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckb_DrawHandles.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.ckb_DrawHandles.Location = new System.Drawing.Point(623, 7);
			this.ckb_DrawHandles.Name = "ckb_DrawHandles";
			this.ckb_DrawHandles.Size = new System.Drawing.Size(98, 17);
			this.ckb_DrawHandles.TabIndex = 27;
			this.ckb_DrawHandles.Text = "Draw Handles";
			this.ckb_DrawHandles.UseVisualStyleBackColor = true;
			this.ckb_DrawHandles.CheckedChanged += new System.EventHandler(this.ckb_DrawHandles_CheckedChanged);
			// 
			// ckb_EnableIK
			// 
			this.ckb_EnableIK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ckb_EnableIK.AutoSize = true;
			this.ckb_EnableIK.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.ckb_EnableIK.Location = new System.Drawing.Point(544, 7);
			this.ckb_EnableIK.Name = "ckb_EnableIK";
			this.ckb_EnableIK.Size = new System.Drawing.Size(73, 17);
			this.ckb_EnableIK.TabIndex = 26;
			this.ckb_EnableIK.Text = "Enable IK";
			this.ckb_EnableIK.UseVisualStyleBackColor = true;
			// 
			// lbl_MousePosition
			// 
			this.lbl_MousePosition.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.lbl_MousePosition.Location = new System.Drawing.Point(181, 9);
			this.lbl_MousePosition.Name = "lbl_MousePosition";
			this.lbl_MousePosition.Size = new System.Drawing.Size(145, 13);
			this.lbl_MousePosition.TabIndex = 25;
			this.lbl_MousePosition.Text = "Mouse Position: 0, 0";
			this.lbl_MousePosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btn_editModePointer
			// 
			this.btn_editModePointer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_editModePointer.Checked = true;
			this.btn_editModePointer.ImageDefault = global::TISFAT.Properties.Resources.cursor;
			this.btn_editModePointer.ImageDown = null;
			this.btn_editModePointer.ImageHover = null;
			this.btn_editModePointer.ImageOn = null;
			this.btn_editModePointer.ImageOnDown = null;
			this.btn_editModePointer.ImageOnHover = null;
			this.btn_editModePointer.Location = new System.Drawing.Point(727, 3);
			this.btn_editModePointer.Name = "btn_editModePointer";
			this.btn_editModePointer.Size = new System.Drawing.Size(24, 24);
			this.btn_editModePointer.TabIndex = 24;
			this.btn_editModePointer.ToggleButton = false;
			this.btn_editModePointer.Click += new System.EventHandler(this.btn_editModePointer_Click);
			// 
			// btn_editModeAdd
			// 
			this.btn_editModeAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_editModeAdd.Checked = false;
			this.btn_editModeAdd.ImageDefault = global::TISFAT.Properties.Resources.bullet_add;
			this.btn_editModeAdd.ImageDown = null;
			this.btn_editModeAdd.ImageHover = null;
			this.btn_editModeAdd.ImageOn = null;
			this.btn_editModeAdd.ImageOnDown = null;
			this.btn_editModeAdd.ImageOnHover = null;
			this.btn_editModeAdd.Location = new System.Drawing.Point(787, 3);
			this.btn_editModeAdd.Name = "btn_editModeAdd";
			this.btn_editModeAdd.Size = new System.Drawing.Size(24, 24);
			this.btn_editModeAdd.TabIndex = 23;
			this.btn_editModeAdd.ToggleButton = false;
			this.btn_editModeAdd.Click += new System.EventHandler(this.btn_editModeAdd_Click);
			// 
			// btn_editModeMove
			// 
			this.btn_editModeMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_editModeMove.Checked = false;
			this.btn_editModeMove.ImageDefault = global::TISFAT.Properties.Resources.transform_move;
			this.btn_editModeMove.ImageDown = null;
			this.btn_editModeMove.ImageHover = null;
			this.btn_editModeMove.ImageOn = null;
			this.btn_editModeMove.ImageOnDown = null;
			this.btn_editModeMove.ImageOnHover = null;
			this.btn_editModeMove.Location = new System.Drawing.Point(757, 3);
			this.btn_editModeMove.Name = "btn_editModeMove";
			this.btn_editModeMove.Size = new System.Drawing.Size(24, 24);
			this.btn_editModeMove.TabIndex = 22;
			this.btn_editModeMove.ToggleButton = false;
			this.btn_editModeMove.Click += new System.EventHandler(this.btn_editModeMove_Click);
			// 
			// btn_editModeDelete
			// 
			this.btn_editModeDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_editModeDelete.Checked = false;
			this.btn_editModeDelete.ImageDefault = global::TISFAT.Properties.Resources.bullet_delete;
			this.btn_editModeDelete.ImageDown = null;
			this.btn_editModeDelete.ImageHover = null;
			this.btn_editModeDelete.ImageOn = null;
			this.btn_editModeDelete.ImageOnDown = null;
			this.btn_editModeDelete.ImageOnHover = null;
			this.btn_editModeDelete.Location = new System.Drawing.Point(817, 3);
			this.btn_editModeDelete.Name = "btn_editModeDelete";
			this.btn_editModeDelete.Size = new System.Drawing.Size(24, 24);
			this.btn_editModeDelete.TabIndex = 21;
			this.btn_editModeDelete.ToggleButton = false;
			this.btn_editModeDelete.Click += new System.EventHandler(this.btn_editModeDelete_Click);
			// 
			// btn_Redo
			// 
			this.btn_Redo.Checked = false;
			this.btn_Redo.ImageDefault = global::TISFAT.Properties.Resources.redo_gray;
			this.btn_Redo.ImageDown = null;
			this.btn_Redo.ImageHover = null;
			this.btn_Redo.ImageOn = null;
			this.btn_Redo.ImageOnDown = null;
			this.btn_Redo.ImageOnHover = null;
			this.btn_Redo.Location = new System.Drawing.Point(133, 3);
			this.btn_Redo.Name = "btn_Redo";
			this.btn_Redo.Size = new System.Drawing.Size(24, 24);
			this.btn_Redo.TabIndex = 19;
			this.btn_Redo.ToggleButton = false;
			// 
			// btn_Undo
			// 
			this.btn_Undo.Checked = false;
			this.btn_Undo.ImageDefault = global::TISFAT.Properties.Resources.undo_gray;
			this.btn_Undo.ImageDown = null;
			this.btn_Undo.ImageHover = null;
			this.btn_Undo.ImageOn = null;
			this.btn_Undo.ImageOnDown = null;
			this.btn_Undo.ImageOnHover = null;
			this.btn_Undo.Location = new System.Drawing.Point(103, 3);
			this.btn_Undo.Name = "btn_Undo";
			this.btn_Undo.Size = new System.Drawing.Size(24, 24);
			this.btn_Undo.TabIndex = 18;
			this.btn_Undo.ToggleButton = false;
			// 
			// separatorControl1
			// 
			this.separatorControl1.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.separatorControl1.Location = new System.Drawing.Point(90, 3);
			this.separatorControl1.Margin = new System.Windows.Forms.Padding(0);
			this.separatorControl1.Name = "separatorControl1";
			this.separatorControl1.Size = new System.Drawing.Size(10, 22);
			this.separatorControl1.TabIndex = 17;
			// 
			// btn_SaveProject
			// 
			this.btn_SaveProject.Checked = false;
			this.btn_SaveProject.ImageDefault = global::TISFAT.Properties.Resources.diskette;
			this.btn_SaveProject.ImageDown = null;
			this.btn_SaveProject.ImageHover = null;
			this.btn_SaveProject.ImageOn = null;
			this.btn_SaveProject.ImageOnDown = null;
			this.btn_SaveProject.ImageOnHover = null;
			this.btn_SaveProject.Location = new System.Drawing.Point(63, 3);
			this.btn_SaveProject.Name = "btn_SaveProject";
			this.btn_SaveProject.Size = new System.Drawing.Size(24, 24);
			this.btn_SaveProject.TabIndex = 16;
			this.btn_SaveProject.ToggleButton = false;
			this.btn_SaveProject.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// btn_OpenProject
			// 
			this.btn_OpenProject.Checked = false;
			this.btn_OpenProject.ImageDefault = global::TISFAT.Properties.Resources.folder;
			this.btn_OpenProject.ImageDown = null;
			this.btn_OpenProject.ImageHover = null;
			this.btn_OpenProject.ImageOn = null;
			this.btn_OpenProject.ImageOnDown = null;
			this.btn_OpenProject.ImageOnHover = null;
			this.btn_OpenProject.Location = new System.Drawing.Point(33, 3);
			this.btn_OpenProject.Name = "btn_OpenProject";
			this.btn_OpenProject.Size = new System.Drawing.Size(24, 24);
			this.btn_OpenProject.TabIndex = 15;
			this.btn_OpenProject.ToggleButton = false;
			this.btn_OpenProject.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// btn_NewProject
			// 
			this.btn_NewProject.Checked = false;
			this.btn_NewProject.ImageDefault = global::TISFAT.Properties.Resources.page_white;
			this.btn_NewProject.ImageDown = null;
			this.btn_NewProject.ImageHover = null;
			this.btn_NewProject.ImageOn = null;
			this.btn_NewProject.ImageOnDown = null;
			this.btn_NewProject.ImageOnHover = null;
			this.btn_NewProject.Location = new System.Drawing.Point(3, 3);
			this.btn_NewProject.Name = "btn_NewProject";
			this.btn_NewProject.Size = new System.Drawing.Size(24, 24);
			this.btn_NewProject.TabIndex = 14;
			this.btn_NewProject.ToggleButton = false;
			this.btn_NewProject.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// pnl_GLArea
			// 
			this.pnl_GLArea.BackColor = System.Drawing.Color.Black;
			this.pnl_GLArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_GLArea.Location = new System.Drawing.Point(0, 54);
			this.pnl_GLArea.Name = "pnl_GLArea";
			this.pnl_GLArea.Size = new System.Drawing.Size(666, 557);
			this.pnl_GLArea.TabIndex = 3;
			// 
			// pnl_SideBar
			// 
			this.pnl_SideBar.BackColor = System.Drawing.SystemColors.Control;
			this.pnl_SideBar.Controls.Add(this.grp_jointProperties);
			this.pnl_SideBar.Controls.Add(this.grp_jointBitmaps);
			this.pnl_SideBar.Controls.Add(this.grp_handleProperties);
			this.pnl_SideBar.Controls.Add(this.panel1);
			this.pnl_SideBar.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnl_SideBar.Location = new System.Drawing.Point(666, 54);
			this.pnl_SideBar.Name = "pnl_SideBar";
			this.pnl_SideBar.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
			this.pnl_SideBar.Size = new System.Drawing.Size(178, 557);
			this.pnl_SideBar.TabIndex = 4;
			// 
			// grp_jointProperties
			// 
			this.grp_jointProperties.Controls.Add(this.pnl_drawMode);
			this.grp_jointProperties.Controls.Add(this.lbl_drawMode);
			this.grp_jointProperties.Controls.Add(this.panel9);
			this.grp_jointProperties.Controls.Add(this.lbl_jointThickness);
			this.grp_jointProperties.Controls.Add(this.pnl_jointColor);
			this.grp_jointProperties.Controls.Add(this.lbl_jointColor);
			this.grp_jointProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grp_jointProperties.Location = new System.Drawing.Point(4, 103);
			this.grp_jointProperties.Name = "grp_jointProperties";
			this.grp_jointProperties.Padding = new System.Windows.Forms.Padding(1);
			this.grp_jointProperties.Size = new System.Drawing.Size(170, 255);
			this.grp_jointProperties.TabIndex = 1;
			this.grp_jointProperties.TabStop = false;
			this.grp_jointProperties.Text = "Joint Properties";
			// 
			// pnl_drawMode
			// 
			this.pnl_drawMode.Controls.Add(this.cmb_drawMode);
			this.pnl_drawMode.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl_drawMode.Location = new System.Drawing.Point(1, 143);
			this.pnl_drawMode.Name = "pnl_drawMode";
			this.pnl_drawMode.Size = new System.Drawing.Size(168, 29);
			this.pnl_drawMode.TabIndex = 13;
			// 
			// cmb_drawMode
			// 
			this.cmb_drawMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_drawMode.FormattingEnabled = true;
			this.cmb_drawMode.Items.AddRange(new object[] {
            "Line",
            "Circle",
            "RadiusCircle"});
			this.cmb_drawMode.Location = new System.Drawing.Point(24, 4);
			this.cmb_drawMode.Name = "cmb_drawMode";
			this.cmb_drawMode.Size = new System.Drawing.Size(121, 21);
			this.cmb_drawMode.TabIndex = 0;
			this.cmb_drawMode.SelectionChangeCommitted += new System.EventHandler(this.cmb_drawMode_SelectionChangeCommitted);
			// 
			// lbl_drawMode
			// 
			this.lbl_drawMode.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_drawMode.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.lbl_drawMode.Location = new System.Drawing.Point(1, 123);
			this.lbl_drawMode.Name = "lbl_drawMode";
			this.lbl_drawMode.Size = new System.Drawing.Size(168, 20);
			this.lbl_drawMode.TabIndex = 12;
			this.lbl_drawMode.Text = "Draw Mode";
			this.lbl_drawMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.num_jointThickness);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel9.Location = new System.Drawing.Point(1, 94);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(168, 29);
			this.panel9.TabIndex = 11;
			// 
			// num_jointThickness
			// 
			this.num_jointThickness.Location = new System.Drawing.Point(24, 4);
			this.num_jointThickness.Name = "num_jointThickness";
			this.num_jointThickness.Size = new System.Drawing.Size(121, 20);
			this.num_jointThickness.TabIndex = 0;
			this.num_jointThickness.ValueChanged += new System.EventHandler(this.num_jointThickness_ValueChanged);
			// 
			// lbl_jointThickness
			// 
			this.lbl_jointThickness.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_jointThickness.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.lbl_jointThickness.Location = new System.Drawing.Point(1, 74);
			this.lbl_jointThickness.Name = "lbl_jointThickness";
			this.lbl_jointThickness.Size = new System.Drawing.Size(168, 20);
			this.lbl_jointThickness.TabIndex = 10;
			this.lbl_jointThickness.Text = "Thickness";
			this.lbl_jointThickness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnl_jointColor
			// 
			this.pnl_jointColor.Controls.Add(this.pnl_jointColorImgBorder);
			this.pnl_jointColor.Controls.Add(this.lbl_jointColorNumbers);
			this.pnl_jointColor.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl_jointColor.Location = new System.Drawing.Point(1, 34);
			this.pnl_jointColor.Name = "pnl_jointColor";
			this.pnl_jointColor.Size = new System.Drawing.Size(168, 40);
			this.pnl_jointColor.TabIndex = 4;
			// 
			// pnl_jointColorImgBorder
			// 
			this.pnl_jointColorImgBorder.BackColor = System.Drawing.Color.White;
			this.pnl_jointColorImgBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_jointColorImgBorder.Controls.Add(this.pnl_jointColorImg);
			this.pnl_jointColorImgBorder.Location = new System.Drawing.Point(12, 4);
			this.pnl_jointColorImgBorder.Name = "pnl_jointColorImgBorder";
			this.pnl_jointColorImgBorder.Padding = new System.Windows.Forms.Padding(2);
			this.pnl_jointColorImgBorder.Size = new System.Drawing.Size(32, 32);
			this.pnl_jointColorImgBorder.TabIndex = 0;
			// 
			// pnl_jointColorImg
			// 
			this.pnl_jointColorImg.BackColor = System.Drawing.Color.Black;
			this.pnl_jointColorImg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_jointColorImg.Location = new System.Drawing.Point(2, 2);
			this.pnl_jointColorImg.Name = "pnl_jointColorImg";
			this.pnl_jointColorImg.Size = new System.Drawing.Size(26, 26);
			this.pnl_jointColorImg.TabIndex = 0;
			this.pnl_jointColorImg.Click += new System.EventHandler(this.pnl_jointColorImg_Click);
			// 
			// lbl_jointColorNumbers
			// 
			this.lbl_jointColorNumbers.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.lbl_jointColorNumbers.Location = new System.Drawing.Point(50, 4);
			this.lbl_jointColorNumbers.Name = "lbl_jointColorNumbers";
			this.lbl_jointColorNumbers.Size = new System.Drawing.Size(115, 32);
			this.lbl_jointColorNumbers.TabIndex = 1;
			this.lbl_jointColorNumbers.Text = "0, 0, 0";
			this.lbl_jointColorNumbers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_jointColor
			// 
			this.lbl_jointColor.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_jointColor.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.lbl_jointColor.Location = new System.Drawing.Point(1, 14);
			this.lbl_jointColor.Name = "lbl_jointColor";
			this.lbl_jointColor.Size = new System.Drawing.Size(168, 20);
			this.lbl_jointColor.TabIndex = 5;
			this.lbl_jointColor.Text = "Joint Color";
			this.lbl_jointColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grp_jointBitmaps
			// 
			this.grp_jointBitmaps.Controls.Add(this.pnl_Bitmaps);
			this.grp_jointBitmaps.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grp_jointBitmaps.Location = new System.Drawing.Point(4, 358);
			this.grp_jointBitmaps.Name = "grp_jointBitmaps";
			this.grp_jointBitmaps.Size = new System.Drawing.Size(170, 162);
			this.grp_jointBitmaps.TabIndex = 3;
			this.grp_jointBitmaps.TabStop = false;
			this.grp_jointBitmaps.Text = "Joint Bitmaps";
			// 
			// pnl_Bitmaps
			// 
			this.pnl_Bitmaps.Controls.Add(this.num_bitmapYOffset);
			this.pnl_Bitmaps.Controls.Add(this.btn_bitmapRemove);
			this.pnl_Bitmaps.Controls.Add(this.num_bitmapXOffset);
			this.pnl_Bitmaps.Controls.Add(this.btn_bitmapAdd);
			this.pnl_Bitmaps.Controls.Add(this.lbl_bitmapYOffset);
			this.pnl_Bitmaps.Controls.Add(this.lbl_bitmapXOffset);
			this.pnl_Bitmaps.Controls.Add(this.lbl_bitmaps);
			this.pnl_Bitmaps.Controls.Add(this.cmb_bitmaps);
			this.pnl_Bitmaps.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl_Bitmaps.Location = new System.Drawing.Point(3, 16);
			this.pnl_Bitmaps.Name = "pnl_Bitmaps";
			this.pnl_Bitmaps.Size = new System.Drawing.Size(164, 141);
			this.pnl_Bitmaps.TabIndex = 11;
			// 
			// num_bitmapYOffset
			// 
			this.num_bitmapYOffset.Location = new System.Drawing.Point(59, 107);
			this.num_bitmapYOffset.Name = "num_bitmapYOffset";
			this.num_bitmapYOffset.Size = new System.Drawing.Size(102, 20);
			this.num_bitmapYOffset.TabIndex = 12;
			// 
			// btn_bitmapRemove
			// 
			this.btn_bitmapRemove.Location = new System.Drawing.Point(84, 50);
			this.btn_bitmapRemove.Name = "btn_bitmapRemove";
			this.btn_bitmapRemove.Size = new System.Drawing.Size(73, 23);
			this.btn_bitmapRemove.TabIndex = 11;
			this.btn_bitmapRemove.Text = "Remove";
			this.btn_bitmapRemove.UseVisualStyleBackColor = true;
			// 
			// num_bitmapXOffset
			// 
			this.num_bitmapXOffset.Location = new System.Drawing.Point(59, 81);
			this.num_bitmapXOffset.Name = "num_bitmapXOffset";
			this.num_bitmapXOffset.Size = new System.Drawing.Size(102, 20);
			this.num_bitmapXOffset.TabIndex = 4;
			// 
			// btn_bitmapAdd
			// 
			this.btn_bitmapAdd.Location = new System.Drawing.Point(7, 50);
			this.btn_bitmapAdd.Name = "btn_bitmapAdd";
			this.btn_bitmapAdd.Size = new System.Drawing.Size(71, 23);
			this.btn_bitmapAdd.TabIndex = 10;
			this.btn_bitmapAdd.Text = "Add";
			this.btn_bitmapAdd.UseVisualStyleBackColor = true;
			// 
			// lbl_bitmapYOffset
			// 
			this.lbl_bitmapYOffset.AutoSize = true;
			this.lbl_bitmapYOffset.Location = new System.Drawing.Point(4, 110);
			this.lbl_bitmapYOffset.Name = "lbl_bitmapYOffset";
			this.lbl_bitmapYOffset.Size = new System.Drawing.Size(45, 13);
			this.lbl_bitmapYOffset.TabIndex = 3;
			this.lbl_bitmapYOffset.Text = "Y Offset";
			// 
			// lbl_bitmapXOffset
			// 
			this.lbl_bitmapXOffset.AutoSize = true;
			this.lbl_bitmapXOffset.Location = new System.Drawing.Point(5, 84);
			this.lbl_bitmapXOffset.Name = "lbl_bitmapXOffset";
			this.lbl_bitmapXOffset.Size = new System.Drawing.Size(45, 13);
			this.lbl_bitmapXOffset.TabIndex = 2;
			this.lbl_bitmapXOffset.Text = "X Offset";
			// 
			// lbl_bitmaps
			// 
			this.lbl_bitmaps.AutoSize = true;
			this.lbl_bitmaps.Location = new System.Drawing.Point(58, 7);
			this.lbl_bitmaps.Name = "lbl_bitmaps";
			this.lbl_bitmaps.Size = new System.Drawing.Size(44, 13);
			this.lbl_bitmaps.TabIndex = 1;
			this.lbl_bitmaps.Text = "Bitmaps";
			// 
			// cmb_bitmaps
			// 
			this.cmb_bitmaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_bitmaps.FormattingEnabled = true;
			this.cmb_bitmaps.Location = new System.Drawing.Point(7, 23);
			this.cmb_bitmaps.Name = "cmb_bitmaps";
			this.cmb_bitmaps.Size = new System.Drawing.Size(150, 21);
			this.cmb_bitmaps.TabIndex = 0;
			// 
			// grp_handleProperties
			// 
			this.grp_handleProperties.Controls.Add(this.ckb_handleVisible);
			this.grp_handleProperties.Controls.Add(this.pnl_handleColor);
			this.grp_handleProperties.Controls.Add(this.lbl_handleColor);
			this.grp_handleProperties.Dock = System.Windows.Forms.DockStyle.Top;
			this.grp_handleProperties.Location = new System.Drawing.Point(4, 0);
			this.grp_handleProperties.Name = "grp_handleProperties";
			this.grp_handleProperties.Padding = new System.Windows.Forms.Padding(1);
			this.grp_handleProperties.Size = new System.Drawing.Size(170, 103);
			this.grp_handleProperties.TabIndex = 2;
			this.grp_handleProperties.TabStop = false;
			this.grp_handleProperties.Text = "Handle Properties";
			// 
			// ckb_handleVisible
			// 
			this.ckb_handleVisible.AutoSize = true;
			this.ckb_handleVisible.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.ckb_handleVisible.Location = new System.Drawing.Point(55, 80);
			this.ckb_handleVisible.Name = "ckb_handleVisible";
			this.ckb_handleVisible.Size = new System.Drawing.Size(60, 17);
			this.ckb_handleVisible.TabIndex = 4;
			this.ckb_handleVisible.Text = "Visible";
			this.ckb_handleVisible.UseVisualStyleBackColor = true;
			this.ckb_handleVisible.VisibleChanged += new System.EventHandler(this.ckb_handleVisible_VisibleChanged);
			// 
			// pnl_handleColor
			// 
			this.pnl_handleColor.Controls.Add(this.pnl_handleColorImgBorder);
			this.pnl_handleColor.Controls.Add(this.lbl_handleColorNumbers);
			this.pnl_handleColor.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl_handleColor.Location = new System.Drawing.Point(1, 34);
			this.pnl_handleColor.Name = "pnl_handleColor";
			this.pnl_handleColor.Size = new System.Drawing.Size(168, 40);
			this.pnl_handleColor.TabIndex = 2;
			// 
			// pnl_handleColorImgBorder
			// 
			this.pnl_handleColorImgBorder.BackColor = System.Drawing.Color.White;
			this.pnl_handleColorImgBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_handleColorImgBorder.Controls.Add(this.pnl_handleColorImg);
			this.pnl_handleColorImgBorder.Location = new System.Drawing.Point(12, 4);
			this.pnl_handleColorImgBorder.Name = "pnl_handleColorImgBorder";
			this.pnl_handleColorImgBorder.Padding = new System.Windows.Forms.Padding(2);
			this.pnl_handleColorImgBorder.Size = new System.Drawing.Size(32, 32);
			this.pnl_handleColorImgBorder.TabIndex = 0;
			// 
			// pnl_handleColorImg
			// 
			this.pnl_handleColorImg.BackColor = System.Drawing.Color.Black;
			this.pnl_handleColorImg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_handleColorImg.Location = new System.Drawing.Point(2, 2);
			this.pnl_handleColorImg.Name = "pnl_handleColorImg";
			this.pnl_handleColorImg.Size = new System.Drawing.Size(26, 26);
			this.pnl_handleColorImg.TabIndex = 0;
			this.pnl_handleColorImg.Click += new System.EventHandler(this.pnl_handleColorImg_Click);
			// 
			// lbl_handleColorNumbers
			// 
			this.lbl_handleColorNumbers.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.lbl_handleColorNumbers.Location = new System.Drawing.Point(50, 4);
			this.lbl_handleColorNumbers.Name = "lbl_handleColorNumbers";
			this.lbl_handleColorNumbers.Size = new System.Drawing.Size(115, 32);
			this.lbl_handleColorNumbers.TabIndex = 1;
			this.lbl_handleColorNumbers.Text = "0, 0, 0";
			this.lbl_handleColorNumbers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_handleColor
			// 
			this.lbl_handleColor.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_handleColor.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.lbl_handleColor.Location = new System.Drawing.Point(1, 14);
			this.lbl_handleColor.Name = "lbl_handleColor";
			this.lbl_handleColor.Size = new System.Drawing.Size(168, 20);
			this.lbl_handleColor.TabIndex = 3;
			this.lbl_handleColor.Text = "Handle Color";
			this.lbl_handleColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btn_OK);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(4, 520);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(170, 33);
			this.panel1.TabIndex = 4;
			// 
			// btn_OK
			// 
			this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_OK.Location = new System.Drawing.Point(48, 5);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(75, 23);
			this.btn_OK.TabIndex = 0;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// StickEditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(844, 611);
			this.Controls.Add(this.pnl_GLArea);
			this.Controls.Add(this.pnl_SideBar);
			this.Controls.Add(this.pnl_Toolbox);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "StickEditorForm";
			this.ShowIcon = false;
			this.Text = "TISFAT Zero Stick Editor";
			this.Load += new System.EventHandler(this.StickEditorForm_Load);
			this.Resize += new System.EventHandler(this.StickEditorForm_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.pnl_Toolbox.ResumeLayout(false);
			this.pnl_Toolbox.PerformLayout();
			this.pnl_SideBar.ResumeLayout(false);
			this.grp_jointProperties.ResumeLayout(false);
			this.pnl_drawMode.ResumeLayout(false);
			this.panel9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.num_jointThickness)).EndInit();
			this.pnl_jointColor.ResumeLayout(false);
			this.pnl_jointColorImgBorder.ResumeLayout(false);
			this.grp_jointBitmaps.ResumeLayout(false);
			this.pnl_Bitmaps.ResumeLayout(false);
			this.pnl_Bitmaps.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapYOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_bitmapXOffset)).EndInit();
			this.grp_handleProperties.ResumeLayout(false);
			this.grp_handleProperties.PerformLayout();
			this.pnl_handleColor.ResumeLayout(false);
			this.pnl_handleColorImgBorder.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.Panel pnl_Toolbox;
		private System.Windows.Forms.Panel pnl_GLArea;
		private System.Windows.Forms.Panel pnl_SideBar;
		private Controls.BitmapButtonControl btn_Redo;
		private Controls.BitmapButtonControl btn_Undo;
		private UI.Controls.SeparatorControl separatorControl1;
		private Controls.BitmapButtonControl btn_SaveProject;
		private Controls.BitmapButtonControl btn_OpenProject;
		private Controls.BitmapButtonControl btn_NewProject;
		private Controls.BitmapButtonControl btn_editModePointer;
		private Controls.BitmapButtonControl btn_editModeAdd;
		private Controls.BitmapButtonControl btn_editModeMove;
		private Controls.BitmapButtonControl btn_editModeDelete;
		private System.Windows.Forms.Label lbl_MousePosition;
		private System.Windows.Forms.CheckBox ckb_EnableIK;
		private System.Windows.Forms.GroupBox grp_jointProperties;
		private System.Windows.Forms.GroupBox grp_handleProperties;
		private System.Windows.Forms.Panel pnl_handleColor;
		private System.Windows.Forms.Panel pnl_handleColorImgBorder;
		private System.Windows.Forms.Panel pnl_handleColorImg;
		private System.Windows.Forms.Label lbl_handleColorNumbers;
		private System.Windows.Forms.Label lbl_handleColor;
		private System.Windows.Forms.CheckBox ckb_handleVisible;
		private System.Windows.Forms.CheckBox ckb_DrawHandles;
		private System.Windows.Forms.Panel pnl_jointColor;
		private System.Windows.Forms.Panel pnl_jointColorImgBorder;
		private System.Windows.Forms.Panel pnl_jointColorImg;
		private System.Windows.Forms.Label lbl_jointColorNumbers;
		private System.Windows.Forms.Label lbl_jointColor;
		private System.Windows.Forms.GroupBox grp_jointBitmaps;
		private System.Windows.Forms.Panel pnl_Bitmaps;
		private System.Windows.Forms.NumericUpDown num_bitmapYOffset;
		private System.Windows.Forms.Button btn_bitmapRemove;
		private System.Windows.Forms.NumericUpDown num_bitmapXOffset;
		private System.Windows.Forms.Button btn_bitmapAdd;
		private System.Windows.Forms.Label lbl_bitmapYOffset;
		private System.Windows.Forms.Label lbl_bitmapXOffset;
		private System.Windows.Forms.Label lbl_bitmaps;
		private System.Windows.Forms.ComboBox cmb_bitmaps;
		private System.Windows.Forms.Panel pnl_drawMode;
		private System.Windows.Forms.ComboBox cmb_drawMode;
		private System.Windows.Forms.Label lbl_drawMode;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.NumericUpDown num_jointThickness;
		private System.Windows.Forms.Label lbl_jointThickness;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
	}
}