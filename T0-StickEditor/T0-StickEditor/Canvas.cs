using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T0_StickEditor
{
	public partial class Canvas : Form
	{
		public static Main mainForm;
		public static Graphics theCanvasGraphics;

		public int toolType = 0;


		public bool draggingTool = false, mouseDown = false;
		public Point mousePoint;
		


		public Canvas(Main f)
		{
			mainForm = f;
			InitializeComponent();
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			theCanvasGraphics = e.Graphics;
			theCanvasGraphics.Clear(Color.White);
			//Basically, for anything drawing related, you /have/ to have anything that does drawing to the canvas
			//be called through this function.

			drawGraphics(3, new Pen(Color.Green), mousePoint, 5, 5, mousePoint);
		}

		public static void drawGraphics(int type, Pen pen, Point one, int width, int height, Point two)
		{
			if (type == 0) //Line
			{
				theCanvasGraphics.SmoothingMode = SmoothingMode.AntiAlias;
				pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
				pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
				theCanvasGraphics.DrawLine(pen, two, one);
				pen.Dispose();
			}
			else if (type == 1) //Circle
			{
				theCanvasGraphics.SmoothingMode = SmoothingMode.AntiAlias;
				Brush brush = new SolidBrush(pen.Color);

				theCanvasGraphics.DrawEllipse(pen, new Rectangle(one.X - width / 2, one.Y - height / 2, width, height));

				brush.Dispose();
				pen.Dispose();
			}
			else if (type == 2) //Handle
			{

				theCanvasGraphics.SmoothingMode = SmoothingMode.HighSpeed;
				Rectangle rect = new Rectangle(one.X, one.Y, 5, 5);
				Brush brush = new SolidBrush(pen.Color);

				theCanvasGraphics.FillRectangle(brush, Functions.Center(rect).X, Functions.Center(rect).Y, 5, 5);

				brush.Dispose();
				pen.Dispose();
			}
			else if (type == 3) //Hollow Handle
			{
				theCanvasGraphics.SmoothingMode = SmoothingMode.HighSpeed;
				Rectangle rect = new Rectangle(one.X - 1, one.Y - 1, 5, 5);

				theCanvasGraphics.DrawRectangle(pen, Functions.Center(rect).X, Functions.Center(rect).Y, 6, 6);
				pen.Dispose();
			}
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			mousePoint = e.Location;
			Refresh();
		}

		private void Canvas_Load(object sender, EventArgs e)
		{
		}

		private void Canvas_MouseLeave(object sender, EventArgs e)
		{
		}

		private void Canvas_MouseEnter(object sender, EventArgs e)
		{
		}
	}
}
