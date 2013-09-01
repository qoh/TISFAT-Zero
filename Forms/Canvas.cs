using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT_ZERO
{
	public partial class Canvas : Form
	{
        #region Variables
        public static MainF mainForm;
        public static Toolbox theToolbox;
        public static Canvas theCanvas;
        public static Graphics theCanvasGraphics;

        public static List<StickFigure> stickFigureList = new List<StickFigure>();
        public static StickFigure activeFigure;
        public static StickJoint selectedJoint = new StickJoint("null", new Point(0, 0), 0, Color.Transparent, Color.Transparent);
        public bool draw; 
        #endregion

		//Instantiate the class
		public Canvas(MainF f, Toolbox t)
		{
			mainForm = f;
			theCanvas = this;
			StickFigure.test = this;
			theToolbox = t;
            //theCanvasGraphics = this.CreateGraphics();
            
            //theCanvasGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			InitializeComponent();
		}

        #region Mouse Events
        //Debug stuff, and dragging joints.
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            theToolbox.lbl_xPos.Text = "X Pos: " + e.X.ToString();
            theToolbox.lbl_yPos.Text = "Y Pos: " + e.Y.ToString();

            if (draw)
            {
                if (!(selectedJoint.name == "null"))
                {
                    //if (selectedJoint.jointname == "Head")
                    //{
                    //    return;
                    //}

                    selectedJoint.SetPos(e.X, e.Y);
                    Refresh();
                }
                foreach (StickFigure fig in stickFigureList)
                {
                    if (!(fig == activeFigure))
                    {
                        fig.isActiveFigure = false;
                    }
                }
            }
            for (int i = 0; i < stickFigureList.Count; i++)
            {
				if (stickFigureList[i].getPointAt(new Point(e.X, e.Y), 4) != -1 && stickFigureList[i].drawHandles)
                {
                    if (activeFigure != null)
                        activeFigure.isActiveFigure = false;

                    activeFigure = stickFigureList[i];
                    activeFigure.isActiveFigure = true;

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

        //Debug stuff, and selection of joints. This also causes the canvas to be redrawn on mouse move.
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!(ModifierKeys == Keys.Control))
                {
                    try
                    {
                        StickJoint f = activeFigure.selectPoint(new Point(e.X, e.Y), 4);
                        theToolbox.lbl_selectedJoint.Text = "Selected Joint: " + f.name;
                        theToolbox.lbl_jointLength.Text = "Joint Length: " + f.CalcLength(null).ToString();

                        selectedJoint = f;
                    }
                    catch
                    {
                        return;
                    }
                    draw = true;
                }
                else if (ModifierKeys == Keys.Control)
                {
                    try
                    {
                        StickJoint f = activeFigure.selectPoint(new Point(e.X, e.Y), 4);
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
                draw = false;
            }
        } 
        #endregion

        #region Graphics
        //This is called whenever the form is invalidated.
        public void Canvas_Paint(object sender, PaintEventArgs e)
        {
            theCanvasGraphics = e.Graphics;
            theCanvasGraphics.Clear(this.BackColor);
            for (int i = 0; i < stickFigureList.Count; i++)
            {
                StickFigure x = stickFigureList[i];

                if (x.drawFigure)
                {
                    x.Draw(false);
					x.DrawHandles();
                }
            }
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
                theCanvasGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                theCanvasGraphics.DrawLine(pen, two, one);
                pen.Dispose();
            }
            else if (type == 1) //Circle
            {
                theCanvasGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Brush brush = new SolidBrush(pen.Color);

                theCanvasGraphics.DrawEllipse(pen, new Rectangle(one.X - width / 2, one.Y - height / 2, width, height));
                pen.Dispose();

            }
            else if (type == 2) //Handle
            {
                theCanvasGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                Rectangle rect = new Rectangle(one.X, one.Y, 5, 5);
                Brush brush = new SolidBrush(pen.Color);

                theCanvasGraphics.FillRectangle(brush, Functions.Center(rect).X, Functions.Center(rect).Y, 5, 5);
                pen.Dispose();
            }

            else if (type == 3) //Hollow Handle
            {

                theCanvasGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(one.X, one.Y, 5, 5);

                theCanvasGraphics.DrawRectangle(pen, Functions.Center(rect).X, Functions.Center(rect).Y, 6, 6);
                pen.Dispose();

            }
        } 
        #endregion

        #region Figures
        public static void addStickFigure(StickFigure figure)
        {
            for (int i = 0; i < stickFigureList.Count; i++)
            {
                stickFigureList[i].isActiveFigure = false;
            }
            stickFigureList.Add(figure);
            figure.isActiveFigure = true;
            theToolbox.lbl_stickFigures.Text = "StickFigure List: " + stickFigureList.Count;
            theCanvas.Refresh();
        }

        public static void removeStickFigure(StickFigure figure)
        {
            stickFigureList.Remove(figure);
        }

        public static void activateFigure(StickFigure fig)
        {
            foreach (StickFigure f in stickFigureList)
            {
                f.isActiveFigure = f == fig;
            }

            theCanvas.Refresh();
        }

        public StickFigure createFigure()
        {
			StickFigure figure = new StickFigure(false);

            return figure;
        } 
        #endregion

        #region Right Click Menu
        private void flipArmsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeFigure.flipArms();
        }

        private void flipLegsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeFigure.flipLegs();
        } 
        #endregion
	}
}