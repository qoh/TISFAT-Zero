namespace T0_StickEditor
{
	partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.pnl_toolBox = new System.Windows.Forms.Panel();
			this.pic_Circle = new System.Windows.Forms.PictureBox();
			this.pic_Line = new System.Windows.Forms.PictureBox();
			this.pic_Cursor = new System.Windows.Forms.PictureBox();
			this.pnl_Main = new System.Windows.Forms.Panel();
			this.pnl_toolBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pic_Circle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_Line)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_Cursor)).BeginInit();
			this.SuspendLayout();
			// 
			// pnl_toolBox
			// 
			this.pnl_toolBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_toolBox.Controls.Add(this.pic_Circle);
			this.pnl_toolBox.Controls.Add(this.pic_Line);
			this.pnl_toolBox.Controls.Add(this.pic_Cursor);
			this.pnl_toolBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl_toolBox.Location = new System.Drawing.Point(0, 0);
			this.pnl_toolBox.Name = "pnl_toolBox";
			this.pnl_toolBox.Size = new System.Drawing.Size(722, 40);
			this.pnl_toolBox.TabIndex = 0;
			// 
			// pic_Circle
			// 
			this.pic_Circle.BackColor = System.Drawing.Color.White;
			this.pic_Circle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Circle.BackgroundImage")));
			this.pic_Circle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_Circle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pic_Circle.Location = new System.Drawing.Point(342, 11);
			this.pic_Circle.Name = "pic_Circle";
			this.pic_Circle.Size = new System.Drawing.Size(18, 18);
			this.pic_Circle.TabIndex = 2;
			this.pic_Circle.TabStop = false;
			this.pic_Circle.MouseEnter += new System.EventHandler(this.pic_Circle_MouseEnter);
			this.pic_Circle.MouseLeave += new System.EventHandler(this.pic_Circle_MouseLeave);
			// 
			// pic_Line
			// 
			this.pic_Line.BackColor = System.Drawing.Color.White;
			this.pic_Line.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Line.BackgroundImage")));
			this.pic_Line.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_Line.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pic_Line.Location = new System.Drawing.Point(318, 11);
			this.pic_Line.Name = "pic_Line";
			this.pic_Line.Size = new System.Drawing.Size(18, 18);
			this.pic_Line.TabIndex = 1;
			this.pic_Line.TabStop = false;
			this.pic_Line.MouseEnter += new System.EventHandler(this.pic_Line_MouseEnter);
			this.pic_Line.MouseLeave += new System.EventHandler(this.pic_Line_MouseLeave);
			// 
			// pic_Cursor
			// 
			this.pic_Cursor.BackColor = System.Drawing.Color.Cyan;
			this.pic_Cursor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Cursor.BackgroundImage")));
			this.pic_Cursor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pic_Cursor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pic_Cursor.Location = new System.Drawing.Point(294, 11);
			this.pic_Cursor.Name = "pic_Cursor";
			this.pic_Cursor.Size = new System.Drawing.Size(18, 18);
			this.pic_Cursor.TabIndex = 0;
			this.pic_Cursor.TabStop = false;
			this.pic_Cursor.MouseEnter += new System.EventHandler(this.pic_Cursor_MouseEnter);
			this.pic_Cursor.MouseLeave += new System.EventHandler(this.pic_Cursor_MouseLeave);
			// 
			// pnl_Main
			// 
			this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_Main.Location = new System.Drawing.Point(0, 40);
			this.pnl_Main.Name = "pnl_Main";
			this.pnl_Main.Size = new System.Drawing.Size(722, 408);
			this.pnl_Main.TabIndex = 1;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(722, 448);
			this.Controls.Add(this.pnl_Main);
			this.Controls.Add(this.pnl_toolBox);
			this.Name = "Main";
			this.Text = "Stick Editor";
			this.Load += new System.EventHandler(this.Main_Load);
			this.Resize += new System.EventHandler(this.Main_Resize);
			this.pnl_toolBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pic_Circle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_Line)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_Cursor)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnl_toolBox;
		private System.Windows.Forms.Panel pnl_Main;
		private System.Windows.Forms.PictureBox pic_Cursor;
		private System.Windows.Forms.PictureBox pic_Circle;
		private System.Windows.Forms.PictureBox pic_Line;
	}
}