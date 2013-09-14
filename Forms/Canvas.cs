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
		/// <summary>
		/// Initializes a new instance of the <see cref="Canvas"/> class.
		/// </summary>
		/// <param name="f">The main form object</param>
		/// <param name="t">The toolbox object</param>
		public Canvas(MainF f, Toolbox t)
		{
			mainForm = f;
			theCanvas = this;
			theToolbox = t;

			InitializeComponent();
		}

		#region Mouse Events
		//Debug stuff, and dragging joints.
		/// <summary>
		/// Handles the MouseMove event of the Canvas control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			//Set the cursor position in the toolbox
			theToolbox.lbl_xPos.Text = "X Pos: " + e.X.ToString();
			theToolbox.lbl_yPos.Text = "Y Pos: " + e.Y.ToString();

			//If the canvas is to be drawn, and the user isn't holding down the right mouse button
			//This is mostly so that you won't be dragging the entire figure at the same time
			//That you're dragging a joint
			if (draw & !(e.Button == MouseButtons.Right))
			{
				//To prevent exceptions being thrown.
				if (!(selectedJoint.name == "null"))
				{
					selectedJoint.SetPos(e.X, e.Y);
					Refresh();
				}
				//This prevents any other figures from becoming active as you are dragging a joint.
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
				//This prevents the context menu from popping up after you release the right
				//mouse button when you're dragging a figure.
				mousemoved = true;

				//This basically keeps the distance from the cursor and the figure constant
				//as the user drags it around.
				for (int i = 0; i < activeFigure.Joints.Count; i++)
				{
					activeFigure.Joints[i].location.X = fx[i] + (e.X - ox);
					activeFigure.Joints[i].location.Y = fy[i] + (e.Y - oy);
				}

				//Refresh the canvas or the user won't see any difference.
				Refresh();
			}

			//This is what sets the active figure to whatever figure owns the joint that you
			//moused over.
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

			//If the active figure exists (isn't null), and we aren't supposed to redraw, then..
			//This is what sets the cursor to the hand when you mouse over a joint.
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
		/// <summary>
		/// Handles the MouseDown event of the Canvas control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				//If the user isn't holding down the 'ctrl' key..
				if (!(ModifierKeys == Keys.Control))
				{
					//This prevents a null reference exception when a user left clicks
					//without an activeFigure being set yet.
					if (activeFigure == null)
						return;

					StickJoint f;

					//Selects the point at the location that the user clicked, with a
					//tolerance of about 4 pixels.
					f = activeFigure.selectPoint(new Point(e.X, e.Y), 4);

					//This sets the labels in the debug menu.
					theToolbox.lbl_selectedJoint.Text = "Selected Joint: " + f.name;
					theToolbox.lbl_jointLength.Text = "Joint Length: " + f.CalcLength(null).ToString();

					//Sets the selectedJoint variable to the joint that we just selected.
					selectedJoint = f;

					//This tells the form that the mouse button is being held down, and
					//that we should redraw the form when it's moved.
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
		/// <summary>
		/// Handles the MouseUp event of the Canvas control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
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

			foreach (StickObject o in figureList)
				o.drawFigHandles();
		}

		/// <summary>
		/// Draws the graphics.
		/// </summary>
		/// <param name="type">What we're drawing. 1 = Line, 1 = Circle, 2 = Handle, 3 = Hollow Handle</param>
		/// <param name="pen">The <see cref="Color">color</see> of what we're drawing.</param>
		/// <param name="one">The origin point.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="two">The end point. (only used in line type)</param>
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
		/// <summary>
		/// Adds the figure.
		/// </summary>
		/// <param name="figure">The figure.</param>
		public static void addFigure(StickObject figure)
		{
			figureList.Add(figure);
		}

		/// <summary>
		/// Adds the tween figure.
		/// </summary>
		/// <param name="figure">The figure.</param>
		public static void addTweenFigure(StickObject figure)
		{
			tweenFigs.Add(figure);
		}

		/// <summary>
		/// Removes the figure.
		/// </summary>
		/// <param name="figure">The figure.</param>
		public static void removeFigure(StickObject figure)
		{
			figureList.Remove(figure);
		}

		/// <summary>
		/// Removes the specified tween figure from the tween figures list.
		/// </summary>
		/// <param name="figure">The figure.</param>
		public static void removeTweenFigure(StickObject figure)
		{
			tweenFigs.Remove(figure);
		}

		/// <summary>
		/// Activates the figure.
		/// </summary>
		/// <param name="fig">The fig.</param>
		public static void activateFigure(StickObject fig)
		{
			for (int i = 0; i < figureList.Count; i++)
			{
				figureList[i].isActiveFig = false;
			}

			fig.isActiveFig = true;
		}
		#endregion

		#region Right Click Menu
		private void flipArmsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeFigure.type == 1)
				((StickFigure)activeFigure).flipArms();
			Refresh();
		}

		private void flipLegsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeFigure.type == 1)
				((StickFigure)activeFigure).flipLegs();
			Refresh();
		} 
		#endregion

		private void Canvas_Load(object sender, EventArgs e)
		{
		}

	}
}