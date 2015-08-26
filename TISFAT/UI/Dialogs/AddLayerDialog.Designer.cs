namespace TISFAT
{
	partial class AddLayerDialog
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Default Figure", 1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Custom Figure", 2);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Bitmap", 0);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLayerDialog));
			this.grp_Properties = new System.Windows.Forms.GroupBox();
			this.pnl_PropertiesDescription = new System.Windows.Forms.Panel();
			this.lbl_PropertiesDescription = new System.Windows.Forms.Label();
			this.pnl_BitmapProperties = new System.Windows.Forms.Panel();
			this.btn_bitmapBrowse = new System.Windows.Forms.Button();
			this.txt_bitmapPath = new System.Windows.Forms.TextBox();
			this.lbl_bitmapLocation = new System.Windows.Forms.Label();
			this.pnl_DefaultFigureProperties = new System.Windows.Forms.Panel();
			this.lbl_DefaultFigureVariantDetail = new System.Windows.Forms.Label();
			this.cmb_DefaultFigureVariant = new System.Windows.Forms.ComboBox();
			this.lbl_DefaultFigureVariant = new System.Windows.Forms.Label();
			this.pnl_CustomFigureProperties = new System.Windows.Forms.Panel();
			this.btn_openStickEditor = new System.Windows.Forms.Button();
			this.lbl_openStickEditor = new System.Windows.Forms.Label();
			this.lbl_customFigPath = new System.Windows.Forms.Label();
			this.txt_customFigPath = new System.Windows.Forms.TextBox();
			this.btn_customFigBrowse = new System.Windows.Forms.Button();
			this.lbl_CustomFigureDescription = new System.Windows.Forms.Label();
			this.lsv_LayerTypes = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btn_Add = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.grp_Properties.SuspendLayout();
			this.pnl_PropertiesDescription.SuspendLayout();
			this.pnl_BitmapProperties.SuspendLayout();
			this.pnl_DefaultFigureProperties.SuspendLayout();
			this.pnl_CustomFigureProperties.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// grp_Properties
			// 
			this.grp_Properties.BackColor = System.Drawing.SystemColors.Control;
			this.grp_Properties.Controls.Add(this.pnl_PropertiesDescription);
			this.grp_Properties.Controls.Add(this.pnl_BitmapProperties);
			this.grp_Properties.Controls.Add(this.pnl_DefaultFigureProperties);
			this.grp_Properties.Controls.Add(this.pnl_CustomFigureProperties);
			this.grp_Properties.Location = new System.Drawing.Point(144, 0);
			this.grp_Properties.Name = "grp_Properties";
			this.grp_Properties.Size = new System.Drawing.Size(252, 221);
			this.grp_Properties.TabIndex = 1;
			this.grp_Properties.TabStop = false;
			this.grp_Properties.Text = "Layer Properties";
			// 
			// pnl_PropertiesDescription
			// 
			this.pnl_PropertiesDescription.Controls.Add(this.lbl_PropertiesDescription);
			this.pnl_PropertiesDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_PropertiesDescription.Location = new System.Drawing.Point(3, 16);
			this.pnl_PropertiesDescription.Name = "pnl_PropertiesDescription";
			this.pnl_PropertiesDescription.Padding = new System.Windows.Forms.Padding(1);
			this.pnl_PropertiesDescription.Size = new System.Drawing.Size(246, 202);
			this.pnl_PropertiesDescription.TabIndex = 0;
			// 
			// lbl_PropertiesDescription
			// 
			this.lbl_PropertiesDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl_PropertiesDescription.Location = new System.Drawing.Point(1, 1);
			this.lbl_PropertiesDescription.Name = "lbl_PropertiesDescription";
			this.lbl_PropertiesDescription.Size = new System.Drawing.Size(244, 200);
			this.lbl_PropertiesDescription.TabIndex = 0;
			this.lbl_PropertiesDescription.Text = "This area contains various editable properties for the layer you\'re about to add." +
    "\r\n\r\nSelect a layer to the left to choose the kind of layer you would like to cre" +
    "ate.";
			this.lbl_PropertiesDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnl_BitmapProperties
			// 
			this.pnl_BitmapProperties.Controls.Add(this.btn_bitmapBrowse);
			this.pnl_BitmapProperties.Controls.Add(this.txt_bitmapPath);
			this.pnl_BitmapProperties.Controls.Add(this.lbl_bitmapLocation);
			this.pnl_BitmapProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_BitmapProperties.Location = new System.Drawing.Point(3, 16);
			this.pnl_BitmapProperties.Name = "pnl_BitmapProperties";
			this.pnl_BitmapProperties.Size = new System.Drawing.Size(246, 202);
			this.pnl_BitmapProperties.TabIndex = 2;
			this.pnl_BitmapProperties.Visible = false;
			// 
			// btn_bitmapBrowse
			// 
			this.btn_bitmapBrowse.Location = new System.Drawing.Point(200, 98);
			this.btn_bitmapBrowse.Name = "btn_bitmapBrowse";
			this.btn_bitmapBrowse.Size = new System.Drawing.Size(22, 22);
			this.btn_bitmapBrowse.TabIndex = 2;
			this.btn_bitmapBrowse.Text = "..";
			this.btn_bitmapBrowse.UseVisualStyleBackColor = true;
			this.btn_bitmapBrowse.Click += new System.EventHandler(this.btn_bitmapBrowse_Click);
			// 
			// txt_bitmapPath
			// 
			this.txt_bitmapPath.Enabled = false;
			this.txt_bitmapPath.Location = new System.Drawing.Point(28, 99);
			this.txt_bitmapPath.Name = "txt_bitmapPath";
			this.txt_bitmapPath.Size = new System.Drawing.Size(172, 20);
			this.txt_bitmapPath.TabIndex = 1;
			// 
			// lbl_bitmapLocation
			// 
			this.lbl_bitmapLocation.AutoSize = true;
			this.lbl_bitmapLocation.Location = new System.Drawing.Point(25, 83);
			this.lbl_bitmapLocation.Name = "lbl_bitmapLocation";
			this.lbl_bitmapLocation.Size = new System.Drawing.Size(36, 13);
			this.lbl_bitmapLocation.TabIndex = 0;
			this.lbl_bitmapLocation.Text = "Image";
			// 
			// pnl_DefaultFigureProperties
			// 
			this.pnl_DefaultFigureProperties.Controls.Add(this.lbl_DefaultFigureVariantDetail);
			this.pnl_DefaultFigureProperties.Controls.Add(this.cmb_DefaultFigureVariant);
			this.pnl_DefaultFigureProperties.Controls.Add(this.lbl_DefaultFigureVariant);
			this.pnl_DefaultFigureProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_DefaultFigureProperties.Location = new System.Drawing.Point(3, 16);
			this.pnl_DefaultFigureProperties.Name = "pnl_DefaultFigureProperties";
			this.pnl_DefaultFigureProperties.Size = new System.Drawing.Size(246, 202);
			this.pnl_DefaultFigureProperties.TabIndex = 3;
			this.pnl_DefaultFigureProperties.Visible = false;
			// 
			// lbl_DefaultFigureVariantDetail
			// 
			this.lbl_DefaultFigureVariantDetail.Location = new System.Drawing.Point(32, 89);
			this.lbl_DefaultFigureVariantDetail.Name = "lbl_DefaultFigureVariantDetail";
			this.lbl_DefaultFigureVariantDetail.Size = new System.Drawing.Size(183, 65);
			this.lbl_DefaultFigureVariantDetail.TabIndex = 2;
			this.lbl_DefaultFigureVariantDetail.Text = "This variant is the same as the inital figure you see when you start TISFAT Zero";
			this.lbl_DefaultFigureVariantDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// cmb_DefaultFigureVariant
			// 
			this.cmb_DefaultFigureVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_DefaultFigureVariant.FormattingEnabled = true;
			this.cmb_DefaultFigureVariant.Items.AddRange(new object[] {
            "Default (Modern T0)",
            "Default (Previous T0)"});
			this.cmb_DefaultFigureVariant.Location = new System.Drawing.Point(33, 65);
			this.cmb_DefaultFigureVariant.Name = "cmb_DefaultFigureVariant";
			this.cmb_DefaultFigureVariant.Size = new System.Drawing.Size(180, 21);
			this.cmb_DefaultFigureVariant.TabIndex = 1;
			this.cmb_DefaultFigureVariant.SelectedIndexChanged += new System.EventHandler(this.cmb_DefaultFigureVariant_SelectedIndexChanged);
			// 
			// lbl_DefaultFigureVariant
			// 
			this.lbl_DefaultFigureVariant.AutoSize = true;
			this.lbl_DefaultFigureVariant.Location = new System.Drawing.Point(30, 49);
			this.lbl_DefaultFigureVariant.Name = "lbl_DefaultFigureVariant";
			this.lbl_DefaultFigureVariant.Size = new System.Drawing.Size(72, 13);
			this.lbl_DefaultFigureVariant.TabIndex = 0;
			this.lbl_DefaultFigureVariant.Text = "Figure Variant";
			// 
			// pnl_CustomFigureProperties
			// 
			this.pnl_CustomFigureProperties.Controls.Add(this.btn_openStickEditor);
			this.pnl_CustomFigureProperties.Controls.Add(this.lbl_openStickEditor);
			this.pnl_CustomFigureProperties.Controls.Add(this.lbl_customFigPath);
			this.pnl_CustomFigureProperties.Controls.Add(this.txt_customFigPath);
			this.pnl_CustomFigureProperties.Controls.Add(this.btn_customFigBrowse);
			this.pnl_CustomFigureProperties.Controls.Add(this.lbl_CustomFigureDescription);
			this.pnl_CustomFigureProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_CustomFigureProperties.Location = new System.Drawing.Point(3, 16);
			this.pnl_CustomFigureProperties.Name = "pnl_CustomFigureProperties";
			this.pnl_CustomFigureProperties.Size = new System.Drawing.Size(246, 202);
			this.pnl_CustomFigureProperties.TabIndex = 4;
			this.pnl_CustomFigureProperties.Visible = false;
			// 
			// btn_openStickEditor
			// 
			this.btn_openStickEditor.Location = new System.Drawing.Point(37, 157);
			this.btn_openStickEditor.Name = "btn_openStickEditor";
			this.btn_openStickEditor.Size = new System.Drawing.Size(172, 23);
			this.btn_openStickEditor.TabIndex = 7;
			this.btn_openStickEditor.Text = "Open Stick Editor";
			this.btn_openStickEditor.UseVisualStyleBackColor = true;
			this.btn_openStickEditor.Click += new System.EventHandler(this.btn_openStickEditor_Click);
			// 
			// lbl_openStickEditor
			// 
			this.lbl_openStickEditor.Location = new System.Drawing.Point(25, 115);
			this.lbl_openStickEditor.Name = "lbl_openStickEditor";
			this.lbl_openStickEditor.Size = new System.Drawing.Size(197, 39);
			this.lbl_openStickEditor.TabIndex = 6;
			this.lbl_openStickEditor.Text = "If you\'d like to create a new figure, click the button below";
			this.lbl_openStickEditor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lbl_customFigPath
			// 
			this.lbl_customFigPath.AutoSize = true;
			this.lbl_customFigPath.Location = new System.Drawing.Point(25, 64);
			this.lbl_customFigPath.Name = "lbl_customFigPath";
			this.lbl_customFigPath.Size = new System.Drawing.Size(93, 13);
			this.lbl_customFigPath.TabIndex = 5;
			this.lbl_customFigPath.Text = "Use existing figure";
			// 
			// txt_customFigPath
			// 
			this.txt_customFigPath.Enabled = false;
			this.txt_customFigPath.Location = new System.Drawing.Point(28, 80);
			this.txt_customFigPath.Name = "txt_customFigPath";
			this.txt_customFigPath.Size = new System.Drawing.Size(172, 20);
			this.txt_customFigPath.TabIndex = 4;
			// 
			// btn_customFigBrowse
			// 
			this.btn_customFigBrowse.Location = new System.Drawing.Point(200, 79);
			this.btn_customFigBrowse.Name = "btn_customFigBrowse";
			this.btn_customFigBrowse.Size = new System.Drawing.Size(22, 22);
			this.btn_customFigBrowse.TabIndex = 3;
			this.btn_customFigBrowse.Text = "..";
			this.btn_customFigBrowse.UseVisualStyleBackColor = true;
			this.btn_customFigBrowse.Click += new System.EventHandler(this.btn_customFigBrowse_Click);
			// 
			// lbl_CustomFigureDescription
			// 
			this.lbl_CustomFigureDescription.Location = new System.Drawing.Point(25, 10);
			this.lbl_CustomFigureDescription.Name = "lbl_CustomFigureDescription";
			this.lbl_CustomFigureDescription.Size = new System.Drawing.Size(197, 39);
			this.lbl_CustomFigureDescription.TabIndex = 0;
			this.lbl_CustomFigureDescription.Text = "Custom figures allow you to create new figures with custom properties";
			this.lbl_CustomFigureDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lsv_LayerTypes
			// 
			this.lsv_LayerTypes.BackColor = System.Drawing.SystemColors.Control;
			this.lsv_LayerTypes.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lsv_LayerTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsv_LayerTypes.Font = new System.Drawing.Font("Segoe UI", 8.5F);
			this.lsv_LayerTypes.FullRowSelect = true;
			this.lsv_LayerTypes.HideSelection = false;
			listViewItem1.Tag = "StickFigure";
			listViewItem2.Tag = "CustomFigure";
			listViewItem3.Tag = "BitmapObject";
			this.lsv_LayerTypes.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
			this.lsv_LayerTypes.Location = new System.Drawing.Point(3, 16);
			this.lsv_LayerTypes.Name = "lsv_LayerTypes";
			this.lsv_LayerTypes.Size = new System.Drawing.Size(126, 202);
			this.lsv_LayerTypes.SmallImageList = this.imageList1;
			this.lsv_LayerTypes.TabIndex = 2;
			this.lsv_LayerTypes.UseCompatibleStateImageBehavior = false;
			this.lsv_LayerTypes.View = System.Windows.Forms.View.SmallIcon;
			this.lsv_LayerTypes.SelectedIndexChanged += new System.EventHandler(this.lsv_LayerTypes_SelectedIndexChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "picture.png");
			this.imageList1.Images.SetKeyName(1, "stickman.png");
			this.imageList1.Images.SetKeyName(2, "customfig.png");
			// 
			// btn_Add
			// 
			this.btn_Add.Location = new System.Drawing.Point(83, 224);
			this.btn_Add.Name = "btn_Add";
			this.btn_Add.Size = new System.Drawing.Size(75, 23);
			this.btn_Add.TabIndex = 3;
			this.btn_Add.Text = "Add";
			this.btn_Add.UseVisualStyleBackColor = true;
			this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Location = new System.Drawing.Point(244, 224);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 4;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lsv_LayerTypes);
			this.groupBox1.Location = new System.Drawing.Point(6, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(132, 221);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Layer Type";
			// 
			// AddLayerDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(402, 251);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_Add);
			this.Controls.Add(this.grp_Properties);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddLayerDialog";
			this.Text = "Add Layer";
			this.Load += new System.EventHandler(this.AddLayerDialog_Load);
			this.grp_Properties.ResumeLayout(false);
			this.pnl_PropertiesDescription.ResumeLayout(false);
			this.pnl_BitmapProperties.ResumeLayout(false);
			this.pnl_BitmapProperties.PerformLayout();
			this.pnl_DefaultFigureProperties.ResumeLayout(false);
			this.pnl_DefaultFigureProperties.PerformLayout();
			this.pnl_CustomFigureProperties.ResumeLayout(false);
			this.pnl_CustomFigureProperties.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox grp_Properties;
		private System.Windows.Forms.ListView lsv_LayerTypes;
		private System.Windows.Forms.Button btn_Add;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label lbl_PropertiesDescription;
		private System.Windows.Forms.Panel pnl_PropertiesDescription;
		private System.Windows.Forms.Panel pnl_BitmapProperties;
		private System.Windows.Forms.Button btn_bitmapBrowse;
		private System.Windows.Forms.TextBox txt_bitmapPath;
		private System.Windows.Forms.Label lbl_bitmapLocation;
		private System.Windows.Forms.Panel pnl_DefaultFigureProperties;
		private System.Windows.Forms.ComboBox cmb_DefaultFigureVariant;
		private System.Windows.Forms.Label lbl_DefaultFigureVariant;
		private System.Windows.Forms.Label lbl_DefaultFigureVariantDetail;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel pnl_CustomFigureProperties;
		private System.Windows.Forms.Label lbl_CustomFigureDescription;
		private System.Windows.Forms.Button btn_openStickEditor;
		private System.Windows.Forms.Label lbl_openStickEditor;
		private System.Windows.Forms.Label lbl_customFigPath;
		private System.Windows.Forms.TextBox txt_customFigPath;
		private System.Windows.Forms.Button btn_customFigBrowse;
	}
}