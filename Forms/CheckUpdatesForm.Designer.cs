namespace TISFAT_ZERO
{
	partial class CheckUpdateForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckUpdateForm));
			this.lbl_DlTitle = new System.Windows.Forms.Label();
			this.Btn_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lbl_DlTitle
			// 
			this.lbl_DlTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lbl_DlTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_DlTitle.Location = new System.Drawing.Point(8, 8);
			this.lbl_DlTitle.Name = "lbl_DlTitle";
			this.lbl_DlTitle.Size = new System.Drawing.Size(266, 20);
			this.lbl_DlTitle.TabIndex = 0;
			this.lbl_DlTitle.Tag = "";
			this.lbl_DlTitle.Text = "Checking For Updates...";
			this.lbl_DlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Btn_Cancel
			// 
			this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.Btn_Cancel.AutoSize = true;
			this.Btn_Cancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Btn_Cancel.Location = new System.Drawing.Point(116, 36);
			this.Btn_Cancel.Name = "Btn_Cancel";
			this.Btn_Cancel.Size = new System.Drawing.Size(50, 23);
			this.Btn_Cancel.TabIndex = 1;
			this.Btn_Cancel.Text = "Cancel";
			this.Btn_Cancel.UseVisualStyleBackColor = true;
			this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
			// 
			// CheckUpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 68);
			this.Controls.Add(this.Btn_Cancel);
			this.Controls.Add(this.lbl_DlTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CheckUpdateForm";
			this.Text = "Checking For Updates";
			this.Load += new System.EventHandler(this.CheckUpdatesForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_DlTitle;
		private System.Windows.Forms.Button Btn_Cancel;
	}
}