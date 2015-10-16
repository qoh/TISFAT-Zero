namespace TISFAT
{
	partial class SaveChangesDialog
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
			this.lbl_saveChanges = new System.Windows.Forms.Label();
			this.btn_Yes = new System.Windows.Forms.Button();
			this.btn_No = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lbl_saveChanges
			// 
			this.lbl_saveChanges.Location = new System.Drawing.Point(34, 9);
			this.lbl_saveChanges.Name = "lbl_saveChanges";
			this.lbl_saveChanges.Size = new System.Drawing.Size(237, 58);
			this.lbl_saveChanges.TabIndex = 0;
			this.lbl_saveChanges.Text = "You have unsaved changes.\r\n\r\nWould you like to save your changes before exiting?";
			this.lbl_saveChanges.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btn_Yes
			// 
			this.btn_Yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btn_Yes.Location = new System.Drawing.Point(34, 75);
			this.btn_Yes.Name = "btn_Yes";
			this.btn_Yes.Size = new System.Drawing.Size(75, 23);
			this.btn_Yes.TabIndex = 1;
			this.btn_Yes.Text = "Yes";
			this.btn_Yes.UseVisualStyleBackColor = true;
			this.btn_Yes.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_No
			// 
			this.btn_No.DialogResult = System.Windows.Forms.DialogResult.No;
			this.btn_No.Location = new System.Drawing.Point(115, 75);
			this.btn_No.Name = "btn_No";
			this.btn_No.Size = new System.Drawing.Size(75, 23);
			this.btn_No.TabIndex = 2;
			this.btn_No.Text = "No";
			this.btn_No.UseVisualStyleBackColor = true;
			this.btn_No.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(196, 75);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 3;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Click);
			// 
			// SaveChangesDialog
			// 
			this.AcceptButton = this.btn_Yes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(305, 105);
			this.ControlBox = false;
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_No);
			this.Controls.Add(this.btn_Yes);
			this.Controls.Add(this.lbl_saveChanges);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveChangesDialog";
			this.Text = "Save Changes?";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbl_saveChanges;
		private System.Windows.Forms.Button btn_Yes;
		private System.Windows.Forms.Button btn_No;
		private System.Windows.Forms.Button btn_Cancel;
	}
}