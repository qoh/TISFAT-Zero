namespace TISFAT_ZERO.Forms.Dialogs
{
	partial class PolyPrompt
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
			this.lbl_numSides = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// btn_OK
			// 
			this.btn_OK.Location = new System.Drawing.Point(12, 60);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(75, 23);
			this.btn_OK.TabIndex = 0;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(197, 60);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 1;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// lbl_numSides
			// 
			this.lbl_numSides.AutoSize = true;
			this.lbl_numSides.Location = new System.Drawing.Point(12, 18);
			this.lbl_numSides.Name = "lbl_numSides";
			this.lbl_numSides.Size = new System.Drawing.Size(58, 13);
			this.lbl_numSides.TabIndex = 3;
			this.lbl_numSides.Text = "# of Sides:";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(12, 34);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(260, 20);
			this.numericUpDown1.TabIndex = 4;
			// 
			// PolyPrompt
			// 
			this.AcceptButton = this.btn_OK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(284, 101);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.lbl_numSides);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_OK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PolyPrompt";
			this.Text = "Polygon Creator";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Label lbl_numSides;
		public System.Windows.Forms.NumericUpDown numericUpDown1;
	}
}