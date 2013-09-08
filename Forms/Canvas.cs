using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TISFAT_ZERO
{
	public partial class Canvas : Form
	{
		#region Variables
		public static MainF mainForm;
		public static Toolbox theToolbox;
		public static Canvas theCanvas;
		public static Graphics theCanvasGraphics; //We need a list of objects to draw.

		public static List<StickObject> figureList = new List<StickObject>();
		public static List<StickObject> tweenFigs = new List<StickObject>();

		//Now we need a method to add figures to these lists.

		public static StickObject activeFigure;
		public static StickJoint selectedJoint = new StickJoint("null", new Point(0, 0), 0, Color.Transparent, Color.Transparent);
		public bool draw;

		public bool mousemoved;
		private int ox;
		private int oy;

		private int[] fx = new int[12];
		private int[] fy = new int[12];
		#endregion

		//Instantiate the class
		public Canvas(MainF f, Toolbox t)
		{
			mainForm = f;
			theCanvas = this;
			theToolbox = t;

			InitializeComponent();
		}

		#region Mouse Events
		//Debug stuff, and dragging joints.
		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			theToolbox.lbl_xPos.Text = "X Pos: " + e.X.ToString();
			theToolbox.lbl_yPos.Text = "Y Pos: " + e.Y.ToString();

			if (draw & !(e.Button == MouseButtons.Right))
			{
				if (!(selectedJoint.name == "null"))
				{
					selectedJoint.SetPos(e.X, e.Y);
					Refresh();
				}
				foreach (StickObject fig in figureList)
				{
					if (!(fig == activeFigure))
					{
						fig.isActiveFig = false;
					}
				}
			}
			else if (draw & e.Button == MouseButtons.Right)
			{
				mousemoved = true;

				for (int i = 0; i < activeFigure.Joints.Count; i++)
				{
					activeFigure.Joints[i].location.X = fx[i] + (e.X - ox);
					activeFigure.Joints[i].location.Y = fy[i] + (e.Y - oy);
				}

				Refresh();
			}

			for (int i = 0; i < figureList.Count; i++)
			{
				if (figureList[i].getPointAt(new Point(e.X, e.Y), 4) != -1 && figureList[i].drawHandles)
				{
					if (activeFigure != null)
						activeFigure.isActiveFig = false;

					activeFigure = figureList[i];
					activeFigure.isActiveFig = true;

					Refresh();
					break;
				}
			}

			if (!(activeFigure == null) & !draw)
			{
				if (activeFigure.getPointAt(new Point(e.X, e.Y), 4) != -1)
				{
					this.Cursor = Cursors.Hand;
				}
				else
				{
					this.Cursor = Cursors.Default;
				}
			}
		}

		//Debug stuff, and selection of joints. This also causes thde canvas to be redrawn on mouse move.
		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (!(ModifierKeys == Keys.Control))
				{
					if (activeFigure == null)
						return;

					StickJoint f = null;
					if(activeFigure != null)
						f = activeFigure.selectPoint(new Point(e.X, e.Y), 4);
					theToolbox.lbl_selectedJoint.Text = "Selected Joint: " + f.name;
					theToolbox.lbl_jointLength.Text = "Joint Length: " + f.CalcLength(null).ToString();

					selectedJoint = f;

					draw = true;
				}
				else if (ModifierKeys == Keys.Control)
				{
					try
					{
						StickJoint f = null;
						if(activeFigure != null)
							f = activeFigure.selectPoint(new Point(e.X, e.Y), 4);
						f.state = (f.state == 1) ? f.state = 0 : f.state = 1;
						Invalidate();
						selectedJoint = f;
					}
					catch
					{
						return;
					}
				}
			}
			if (e.Button == MouseButtons.Right & !(e.Button == MouseButtons.Left))
			{
				if (activeFigure == null)
					return;

				ox = e.X;
				oy = e.Y;
				for (int i = 0; i < activeFigure.Joints.Count; i++)
				{
					fx[i] = activeFigure.Joints[i].location.X;
					fy[i] = activeFigure.Joints[i].location.Y;
				}

				draw = true;
			}
		}

		//Deselect the joint, and stop redrawing the canvas.
		private void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				selectedJoint = new StickJoint("null", new Point(0, 0), 0, Color.Transparent, Color.Transparent);
				draw = false;
			}
			if (e.Button == MouseButtons.Right)
			{
				if (!mousemoved)
				{
					contextMenuStrip1.Show(new Point(e.X + contextMenuStrip1.Height, e.Y + contextMenuStrip1.Width));
				}
				mousemoved = false;
				draw = false;
			} 
		} 
		#endregion

		#region Graphics
		//This is called whenever the form is invalidated.
		public void Canvas_Paint(object sender, PaintEventArgs e)
		{
			theCanvasGraphics = e.Graphics;

			foreach (StickObject o in figureList)
				o.drawFigure();

			foreach (StickObject o in tweenFigs)
				o.drawFigure();
		}

		/// <summary>
		/// Draws the graphics.
		/// </summary>
		/// <param name="type">1 = Line, 1 = Circle, 2 = Handle, 3 = Hollow Handle</param>
		/// <param name="pen">Pen Color</param>
		/// <param name="one">Point one</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="two">Point two. (only used in line type)</param>
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
				pen.Dispose();
			}
			else if (type == 2) //Handle
			{
				
				theCanvasGraphics.SmoothingMode = SmoothingMode.HighSpeed;
				Rectangle rect = new Rectangle(one.X, one.Y, 5, 5);
				Brush brush = new SolidBrush(pen.Color);

				theCanvasGraphics.FillRectangle(brush, Functions.Center(rect).X, Functions.Center(rect).Y, 5, 5);
				pen.Dispose();
			}
			else if (type == 3) //Hollow Handle
			{
				theCanvasGraphics.SmoothingMode = SmoothingMode.HighSpeed;
				Rectangle rect = new Rectangle(one.X, one.Y, 5, 5);

				theCanvasGraphics.DrawRectangle(pen, Functions.Center(rect).X, Functions.Center(rect).Y, 6, 6);
				pen.Dispose();
			}
		} 
		#endregion

		#region Figures
		public static void addFigure(StickObject figure)
		{
			figureList.Add(figure);
		}

		public static void addTweenFigure(StickObject figure)
		{
			tweenFigs.Add(figure);
		}

		public static void removeFigure(StickObject figure)
		{
			figureList.Remove(figure);
		}

		public static void removeTweenFigure(StickObject figure)
		{
			tweenFigs.Remove(figure);
		}

		public static void activateFigure(StickObject fig)
		{
			for (int i = 0; i < figureList.Count; i++)
			{
				figureList[i].isActiveFig = false;
			}

			fig.isActiveFig = true;
		}

		public StickFigure createFigure()
		{
			StickFigure figure = new StickFigure(false);

			return figure;
		}

		public StickLine createLine()
		{
			StickLine line = new StickLine(false);

			return line;
		}

		public StickRect createRect()
		{
			StickRect rect = new StickRect(false);

			return rect;
		}
		#endregion

		#region Right Click Menu
		private void flipArmsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeFigure.type == 1)
				((StickFigure)activeFigure).flipArms();
		}

		private void flipLegsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeFigure.type == 1)
				((StickFigure)activeFigure).flipLegs();
		} 
		#endregion

		private void Canvas_Load(object sender, EventArgs e)
		{
		}

	}
}