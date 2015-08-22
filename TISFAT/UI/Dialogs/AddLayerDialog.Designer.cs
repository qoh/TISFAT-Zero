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
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Bitmap", 0);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLayerDialog));
			this.grp_Properties = new System.Windows.Forms.GroupBox();
			this.lbl_PropertiesDescription = new System.Windows.Forms.Label();
			this.lsv_LayerTypes = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btn_Add = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.pnl_PropertiesDescription = new System.Windows.Forms.Panel();
			this.pnl_BitmapProperties = new System.Windows.Forms.Panel();
			this.btn_bitmapBrowse = new System.Windows.Forms.Button();
			this.txt_bitmapPath = new System.Windows.Forms.TextBox();
			this.lbl_bitmapLocation = new System.Windows.Forms.Label();
			this.grp_Properties.SuspendLayout();
			this.pnl_PropertiesDescription.SuspendLayout();
			this.pnl_BitmapProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// grp_Properties
			// 
			this.grp_Properties.BackColor = System.Drawing.SystemColors.Control;
			this.grp_Properties.Controls.Add(this.pnl_PropertiesDescription);
			this.grp_Properties.Controls.Add(this.pnl_BitmapProperties);
			this.grp_Properties.Location = new System.Drawing.Point(138, 0);
			this.grp_Properties.Name = "grp_Properties";
			this.grp_Properties.Size = new System.Drawing.Size(252, 221);
			this.grp_Properties.TabIndex = 1;
			this.grp_Properties.TabStop = false;
			this.grp_Properties.Text = "Layer Properties";
			// 
			// lbl_PropertiesDescription
			// 
			this.lbl_PropertiesDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl_PropertiesDescription.Location = new System.Drawing.Point(0, 0);
			this.lbl_PropertiesDescription.Name = "lbl_PropertiesDescription";
			this.lbl_PropertiesDescription.Size = new System.Drawing.Size(246, 202);
			this.lbl_PropertiesDescription.TabIndex = 0;
			this.lbl_PropertiesDescription.Text = "This area contains various editable properties for the layer you\'re about to add." +
    "\r\n\r\nSelect a layer to the left to choose the kind of layer you would like to cre" +
    "ate.";
			this.lbl_PropertiesDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lsv_LayerTypes
			// 
			this.lsv_LayerTypes.Font = new System.Drawing.Font("Segoe UI", 8.5F);
			this.lsv_LayerTypes.HideSelection = false;
			listViewItem1.Tag = "StickFigure";
			listViewItem2.Tag = "BitmapObject";
			this.lsv_LayerTypes.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.lsv_LayerTypes.Location = new System.Drawing.Point(0, 0);
			this.lsv_LayerTypes.Name = "lsv_LayerTypes";
			this.lsv_LayerTypes.Size = new System.Drawing.Size(132, 221);
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
			// pnl_PropertiesDescription
			// 
			this.pnl_PropertiesDescription.Controls.Add(this.lbl_PropertiesDescription);
			this.pnl_PropertiesDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_PropertiesDescription.Location = new System.Drawing.Point(3, 16);
			this.pnl_PropertiesDescription.Name = "pnl_PropertiesDescription";
			this.pnl_PropertiesDescription.Size = new System.Drawing.Size(246, 202);
			this.pnl_PropertiesDescription.TabIndex = 0;
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
			// AddLayerDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(402, 251);
			this.ControlBox = false;
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_Add);
			this.Controls.Add(this.lsv_LayerTypes);
			this.Controls.Add(this.grp_Properties);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddLayerDialog";
			this.Text = "Add Layer";
			this.grp_Properties.ResumeLayout(false);
			this.pnl_PropertiesDescription.ResumeLayout(false);
			this.pnl_BitmapProperties.ResumeLayout(false);
			this.pnl_BitmapProperties.PerformLayout();
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
	}
}