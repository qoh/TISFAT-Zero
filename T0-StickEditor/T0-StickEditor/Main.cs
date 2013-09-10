using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T0_StickEditor
{
	public partial class Main : Form
	{
		public static Canvas theCanvas;
		public int activeTool = 1;

		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			theCanvas = new Canvas(this);
			theCanvas.TopLevel = false;
			theCanvas.Parent = this.pnl_Main;
			theCanvas.Size = new Size(this.pnl_Main.Width, pnl_Main.Height);
			theCanvas.StartPosition = FormStartPosition.Manual;
			theCanvas.Location = new Point(0, 0);
			this.pnl_Main.Controls.Add(theCanvas);

			theCanvas.Show();
		}

		private void Main_Resize(object sender, EventArgs e)
		{
			theCanvas.Size = new Size(this.pnl_Main.Width, pnl_Main.Height);
		}


		#region Tool Buttons Selection
		private void pic_Cursor_MouseEnter(object sender, EventArgs e)
		{
			if (!(activeTool == 1))
				pic_Cursor.BackColor = Color.Cyan;
			else
				pic_Cursor.BackColor = Color.DarkCyan;
		}

		private void pic_Cursor_MouseLeave(object sender, EventArgs e)
		{
			if (!(activeTool == 1))
				pic_Cursor.BackColor = Color.White;
			else
				pic_Cursor.BackColor = Color.Cyan;
		}

		private void pic_Line_MouseEnter(object sender, EventArgs e)
		{
			if (!(activeTool == 2))
				pic_Line.BackColor = Color.Cyan;
			else
				pic_Line.BackColor = Color.DarkCyan;
		}

		private void pic_Line_MouseLeave(object sender, EventArgs e)
		{
			if (!(activeTool == 2))
				pic_Line.BackColor = Color.White;
			else
				pic_Line.BackColor = Color.Cyan;
		}

		private void pic_Circle_MouseEnter(object sender, EventArgs e)
		{
			if (!(activeTool == 3))
				pic_Circle.BackColor = Color.Cyan;
			else
				pic_Circle.BackColor = Color.DarkCyan;
		}

		private void pic_Circle_MouseLeave(object sender, EventArgs e)
		{
			if (!(activeTool == 3))
				pic_Circle.BackColor = Color.White;
			else
				pic_Circle.BackColor = Color.Cyan;
		}
		#endregion


	}
}
