namespace TISFAT_ZERO.Forms.Dialogs
{
	partial class ScalePrompt
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
			this.tkb_Scale = new System.Windows.Forms.TrackBar();
			this.num_Scale = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.btn_OK = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.tkb_Scale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_Scale)).BeginInit();
			this.SuspendLayout();
			// 
			// tkb_Scale
			// 
			this.tkb_Scale.Location = new System.Drawing.Point(3, 4);
			this.tkb_Scale.Maximum = 500;
			this.tkb_Scale.Minimum = 1;
			this.tkb_Scale.Name = "tkb_Scale";
			this.tkb_Scale.Size = new System.Drawing.Size(269, 45);
			this.tkb_Scale.TabIndex = 0;
			this.tkb_Scale.Value = 100;
			this.tkb_Scale.Scroll += new System.EventHandler(this.tkb_Scale_Scroll);
			// 
			// num_Scale
			// 
			this.num_Scale.Location = new System.Drawing.Point(161, 45);
			this.num_Scale.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.num_Scale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.num_Scale.Name = "num_Scale";
			this.num_Scale.Size = new System.Drawing.Size(43, 20);
			this.num_Scale.TabIndex = 1;
			this.num_Scale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.num_Scale.ValueChanged += new System.EventHandler(this.num_Scale_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(81, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Current Scale:";
			// 
			// btn_OK
			// 
			this.btn_OK.Location = new System.Drawing.Point(30, 74);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(75, 23);
			this.btn_OK.TabIndex = 3;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(180, 74);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 4;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// ScalePrompt
			// 
			this.AcceptButton = this.btn_OK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(284, 101);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_OK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.num_Scale);
			this.Controls.Add(this.tkb_Scale);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ScalePrompt";
			this.Text = "Scale Figure";
			this.Load += new System.EventHandler(this.ScalePrompt_Load);
			((System.ComponentModel.ISupportInitialize)(this.tkb_Scale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_Scale)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TrackBar tkb_Scale;
		private System.Windows.Forms.NumericUpDown num_Scale;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Button btn_Cancel;
	}
}