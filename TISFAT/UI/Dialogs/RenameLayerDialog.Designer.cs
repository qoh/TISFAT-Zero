namespace TISFAT
{
	partial class RenameLayerDialog
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
			this.btn_OK = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.lbl_layerName = new System.Windows.Forms.Label();
			this.txt_layerName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btn_OK
			// 
			this.btn_OK.Location = new System.Drawing.Point(13, 60);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(92, 23);
			this.btn_OK.TabIndex = 0;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Location = new System.Drawing.Point(153, 60);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(92, 23);
			this.btn_Cancel.TabIndex = 1;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// lbl_layerName
			// 
			this.lbl_layerName.AutoSize = true;
			this.lbl_layerName.Location = new System.Drawing.Point(10, 11);
			this.lbl_layerName.Name = "lbl_layerName";
			this.lbl_layerName.Size = new System.Drawing.Size(64, 13);
			this.lbl_layerName.TabIndex = 2;
			this.lbl_layerName.Text = "Layer Name";
			// 
			// txt_layerName
			// 
			this.txt_layerName.Location = new System.Drawing.Point(13, 27);
			this.txt_layerName.Name = "txt_layerName";
			this.txt_layerName.Size = new System.Drawing.Size(232, 20);
			this.txt_layerName.TabIndex = 3;
			this.txt_layerName.TextChanged += new System.EventHandler(this.txt_layerName_TextChanged);
			// 
			// RenameLayerDialog
			// 
			this.AcceptButton = this.btn_OK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(254, 91);
			this.ControlBox = false;
			this.Controls.Add(this.txt_layerName);
			this.Controls.Add(this.lbl_layerName);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_OK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RenameLayerDialog";
			this.Text = "Rename Layer";
			this.Load += new System.EventHandler(this.RenameLayerDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Label lbl_layerName;
		private System.Windows.Forms.TextBox txt_layerName;
	}
}