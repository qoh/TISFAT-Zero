namespace TISFAT_ZERO.UserControls
{
	partial class SceneControlObject
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnl_whitePanel = new System.Windows.Forms.Panel();
			this.pnl_mainPanel = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pnl_whitePanel.SuspendLayout();
			this.pnl_mainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pnl_whitePanel
			// 
			this.pnl_whitePanel.BackColor = System.Drawing.SystemColors.Control;
			this.pnl_whitePanel.Controls.Add(this.pnl_mainPanel);
			this.pnl_whitePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_whitePanel.Location = new System.Drawing.Point(2, 2);
			this.pnl_whitePanel.Name = "pnl_whitePanel";
			this.pnl_whitePanel.Padding = new System.Windows.Forms.Padding(2);
			this.pnl_whitePanel.Size = new System.Drawing.Size(216, 76);
			this.pnl_whitePanel.TabIndex = 0;
			// 
			// pnl_mainPanel
			// 
			this.pnl_mainPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.pnl_mainPanel.Controls.Add(this.label1);
			this.pnl_mainPanel.Controls.Add(this.pictureBox1);
			this.pnl_mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_mainPanel.Location = new System.Drawing.Point(2, 2);
			this.pnl_mainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.pnl_mainPanel.Name = "pnl_mainPanel";
			this.pnl_mainPanel.Size = new System.Drawing.Size(212, 72);
			this.pnl_mainPanel.TabIndex = 0;
			this.pnl_mainPanel.MouseEnter += new System.EventHandler(this.pnl_mainPanel_MouseEnter);
			this.pnl_mainPanel.MouseLeave += new System.EventHandler(this.pnl_mainPanel_MouseLeave);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Segoe UI", 8.5F);
			this.label1.Location = new System.Drawing.Point(75, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Scene Name Here";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(66, 66);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
			// 
			// SceneControlObject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Highlight;
			this.Controls.Add(this.pnl_whitePanel);
			this.Name = "SceneControlObject";
			this.Padding = new System.Windows.Forms.Padding(2);
			this.Size = new System.Drawing.Size(220, 80);
			this.pnl_whitePanel.ResumeLayout(false);
			this.pnl_mainPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnl_whitePanel;
		private System.Windows.Forms.Panel pnl_mainPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}
