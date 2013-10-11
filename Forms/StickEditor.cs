using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace TISFAT_ZERO
{
	public partial class StickEditor : Form
	{
		private StickCustom figure = null;

		public static StickEditor theSticked;

		private bool GLLoaded;
		private int GL_WIDTH, GL_HEIGHT;
		private OpenTK.GLControl glGraphics, oldGL;

		private StickJoint activeJoint = null;
		private StickJoint selectedJoint = null;

		private Point mouseLoc;
		private Point pointClicked;

		private bool mouseDown;
		private bool mouseHot;
		private bool obeyIK = false;
		private bool drawHandles = true;

		private int toolType = 1;

		public StickEditor()
		{
			InitializeComponent();
		}

		private void StickEditor_Load(object sender, EventArgs e)
		{
			glGraphics = GL_GRAPHICS;
			glGraphics.MakeCurrent();

			GLLoaded = true;

			//If you are going to be resizing the canvas later or changing the background color,
			//make sure to re-do these so the GLControl will work properly
			GL_HEIGHT = GL_GRAPHICS.Height;
			GL_WIDTH = GL_GRAPHICS.Width;
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Viewport(0, 0, GL_WIDTH, GL_HEIGHT);
			GL.Ortho(0, GL_WIDTH, 0, GL_HEIGHT, -1, 1);
			GL.ClearColor(Color.White);

			//Since we are 2d, we don't need the depth test
			GL.Disable(EnableCap.DepthTest);

			com_lineType.SelectedIndex = 0;

			theSticked = this;

			figure = new StickCustom(1);
			figure.drawFig = true;
			figure.drawHandles = true;
			figure.isActiveFig = true;

			figure.Joints.Add(new StickJoint("Base Joint 1", new Point(glGraphics.Width / 2, glGraphics.Height / 2), 12, Color.Black, Color.Yellow, 0, 0, false, null, true));
			figure.Joints.Add(new StickJoint("Base Joint 2", new Point(glGraphics.Width / 2, glGraphics.Height / 2 - 20), 12, Color.Black, Color.Blue, 0, 0, false, null, true));
			figure.Joints[1].parent = figure.Joints[0];
			figure.Joints[0].drawOrder = 0;
			figure.Joints[1].drawOrder = 1;

			recalcFigureJoints();
		}

		private void recalcFigureJoints()
		{
			////Clear all children from joints.
			//for (int i = 0; i < figure.Joints.Count; i++)
			//{
			//	for (int j = 0; j < figure.Joints[i].children.Count; j++)
			//		figure.Joints[i].children[j] = null;
			//}

			for (int i = 0; i < figure.Joints.Count(); i++)
			{
				if (figure.Joints[i].parent != null)
				{
					figure.Joints[i].CalcLength(null);
				}
			}

			for (int i = 0; i < figure.Joints.Count(); i++)
			{
				if (figure.Joints[i].parent != null)
				{
					if(!(figure.Joints[i].parent.children.IndexOf(figure.Joints[i]) >= 0))
						figure.Joints[i].parent.children.Add(figure.Joints[i]);
				}
				figure.Joints[i].ParentFigure = figure;
			}
			figure.reSortJoints();
		}

		private void GL_GRAPHICS_Paint(object sender, PaintEventArgs e)
		{
            if (!(selectedJoint == null))
            {
                lbl_jointPosition.Text = "Position: (" + selectedJoint.location.X + ", " + selectedJoint.location.Y + ")";
                lbl_lineLength.Text = "Line Length: " + selectedJoint.length.ToString();
            }

			num_drawOrder.Maximum = figure.Joints.Count;
			if (!GLLoaded)
			{
				return;
			}

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit);

			if (!(figure == null))
				figure.drawFigure(1, true);

			if (!(selectedJoint == null) && drawHandles)
			{
				drawGraphics(2, Color.FromArgb(105, Color.SkyBlue), selectedJoint.location, 4, 4, new Point(selectedJoint.location.X + 7, selectedJoint.location.Y + 7));
				drawGraphics(3, Color.Red, selectedJoint.location, 5, 5, new Point(selectedJoint.location.X + 7, selectedJoint.location.Y + 7));
			}

			if (toolType == 0)
			{
				if (figure.selectPoint(mouseLoc, 4) != null)
				{
					drawGraphics(2, Color.Blue, figure.selectPoint(mouseLoc, 4).location, 1, 1, figure.selectPoint(mouseLoc, 4).location);
					this.Cursor = Cursors.Hand;
				}
				else
					this.Cursor = Cursors.Default;

				if (!mouseDown && !mouseHot)
					drawGraphics(3, Color.Green, mouseLoc, 1, 1, mouseLoc);
				else if (!mouseHot)
					drawGraphics(4, Color.SkyBlue, pointClicked, 1, 1, mouseLoc);
			}

			if (toolType == 2)
			{
				if (!(selectedJoint == null))
				{
					drawGraphics(0, Color.FromArgb(100, selectedJoint.color), selectedJoint.location, (int)num_brushThickness.Value, (int)num_brushThickness.Value, mouseLoc);
				}

				if (!mouseDown && !mouseHot)
					drawGraphics(3, Color.Green, mouseLoc, 1, 1, mouseLoc);
			}

			if(!(figure == null))
				if (drawHandles)
					figure.drawFigHandles(1, true);

			GL_GRAPHICS.SwapBuffers();
		}

		#region Drawing Grapics
		public void drawGraphics(int type, Color color, Point one, int width, int height, Point two)
		{
			if (!GLLoaded)
			{
				return;
			}

			//Invert the y so OpenGL can draw it right-side up
			one.Y = GL_HEIGHT - one.Y;
			two.Y = GL_HEIGHT - two.Y;

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			if (type == 0) //Line
			{
				//since some opengl cards don't support line widths past 1.0, we need to draw quads
				GL.Color4(color);

				//step 1: spam floats
				float x1 = one.X;
				float x2 = two.X;
				float y1 = one.Y;
				float y2 = two.Y;

				//step 2: get slope/delta
				float vecX = x1 - x2;
				float vecY = y1 - y2;

				//step 3: calculate distance
				float dist = (float)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

				//step 4: normalize
				float norm1X = (vecX / dist);
				float norm1Y = (vecY / dist);

				GL.Begin(BeginMode.Quads);

				//step 5: get the perpindicular line to norm1, and scale it based on our width
				float normX = norm1Y * width / 2;
				float normY = -norm1X * width / 2;

				//step 6: draw the quad from the points using the normal as the offset
				GL.Vertex2((one.X - normX), (one.Y - normY));
				GL.Vertex2((one.X + normX), (one.Y + normY));

				GL.Vertex2((two.X + normX), (two.Y + normY));
				GL.Vertex2((two.X - normX), (two.Y - normY));

				GL.End();

				DrawCircle(one.X, one.Y, width / 2);
				DrawCircle(two.X, two.Y, width / 2);
			}
			else if (type == 1) //Circle
			{
				GL.Color4(color);
				DrawCircle(one.X, one.Y, width);
			}
			else if (type == 2) //Handle
			{
				GL.Disable(EnableCap.Multisample);

				GL.Color4(color);
				GL.Begin(BeginMode.Quads);

				GL.Vertex2(one.X - 2.5, one.Y - 2.5);
				GL.Vertex2(one.X + 2.5, one.Y - 2.5);
				GL.Vertex2(one.X + 2.5, one.Y + 2.5);
				GL.Vertex2(one.X - 2.5, one.Y + 2.5);

				GL.End();

				GL.Enable(EnableCap.Multisample);
			}
			else if (type == 3) //Hollow Handle
			{
				GL.Disable(EnableCap.Multisample);

				GL.Color4(color);
				GL.Begin(BeginMode.LineLoop);

				GL.Vertex2(one.X - 2.5, one.Y - 2.5);
				GL.Vertex2(one.X + 2.5, one.Y - 2.5);
				GL.Vertex2(one.X + 2.5, one.Y + 2.5);
				GL.Vertex2(one.X - 2.5, one.Y + 2.5);

				GL.End();

				GL.Enable(EnableCap.Multisample);
			}
			else if (type == 4) //Selection Rect
			{
				GL.Disable(EnableCap.Multisample);

				GL.Color4(color);
				GL.Begin(BeginMode.LineLoop);

				GL.Vertex2(one.X, one.Y);
				GL.Vertex2(two.X, one.Y);
				GL.Vertex2(two.X, two.Y);
				GL.Vertex2(one.X, two.Y);

				GL.End();

				color = Color.FromArgb(color.A - 200, color);
				GL.Color4(color);

				GL.Begin(BeginMode.Quads);

				GL.Vertex2(one.X, one.Y);
				GL.Vertex2(two.X, one.Y);
				GL.Vertex2(two.X, two.Y);
				GL.Vertex2(one.X, two.Y);

				GL.End();

				GL.Enable(EnableCap.Multisample);
			}
			GL.Disable(EnableCap.Blend);
		}

		private static void DrawCircle(float cx, float cy, float r)
		{
			int num_segments = 5 * (int)Math.Sqrt(r);

			float theta = 6.28271f / num_segments;
			float tangetial_factor = (float)Math.Tan(theta);

			float radial_factor = (float)Math.Cos(theta);

			float y = 0;

			GL.Begin(BeginMode.TriangleFan);

			for (int ii = 0; ii < num_segments; ii++)
			{
				GL.Vertex2(r + cx, y + cy);

				float ty = r;

				r = (r + -y * tangetial_factor) * radial_factor;
				y = (y + ty * tangetial_factor) * radial_factor;
			}

			GL.End();
		}
		#endregion

		#region Callbacks
		private void GL_GRAPHICS_Resize(object sender, EventArgs e)
		{
			GL_HEIGHT = GL_GRAPHICS.Height;
			GL_WIDTH = GL_GRAPHICS.Width;
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Viewport(0, 0, GL_WIDTH, GL_HEIGHT);
			GL.Ortho(0, GL_WIDTH, 0, GL_HEIGHT, -1, 1);
			GL.ClearColor(Color.White);
		}

		private void GL_GRAPHICS_MouseMove(object sender, MouseEventArgs e)
		{
			if (toolType == 1)
			{
				if (!(figure == null) & !mouseDown)
				{
					if (figure.getPointAt(new Point(e.X, e.Y), 4) != -1)
					{
						this.Cursor = Cursors.Hand;
						mouseHot = true;
						glGraphics.Invalidate();
					}
					else
					{
						this.Cursor = Cursors.Default;
						mouseHot = false;
						glGraphics.Invalidate();
					}
				}
				else if (mouseDown && !(activeJoint == null))
				{
					if (!obeyIK)
					{
						activeJoint.SetPosAbs(e.X, e.Y);
						if (!(activeJoint.parent == null))
							activeJoint.CalcLength(null);
					}
					else
						activeJoint.SetPos(e.X, e.Y);
				}
			}

			mouseLoc = e.Location;
			glGraphics.Invalidate();
		}

		private void pic_handleColor_Click(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;

			dlg_Color.ShowDialog();
			pic_handleColor.BackColor = dlg_Color.Color;

			selectedJoint.defaultHandleColor = dlg_Color.Color;
			selectedJoint.handleColor = dlg_Color.Color;
		}

		private void pic_lineColor_Click(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;

			dlg_Color.ShowDialog();
			pic_lineColor.BackColor = dlg_Color.Color;
			selectedJoint.color = dlg_Color.Color;

			glGraphics.Invalidate();
		}

		private void GL_GRAPHICS_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (toolType == 0)
				{
					selectedJoint = figure.selectPoint(e.Location, 4);
					pointClicked = e.Location;
					mouseDown = e.Button == MouseButtons.Left;

					glGraphics.Invalidate();
					updateToolboxInfo();
				}

				if (toolType == 1)
				{
					activeJoint = figure.selectPoint(e.Location, 4);

					if (!(activeJoint == null))
						selectedJoint = activeJoint;

					pointClicked = e.Location;
					mouseDown = e.Button == MouseButtons.Left;
					glGraphics.Invalidate();
					updateToolboxInfo();
				}

				if (toolType == 2)
				{
					if (selectedJoint == null)
						return;

					StickJoint j = new StickJoint("New Joint", e.Location, (int)num_brushThickness.Value, selectedJoint.color, selectedJoint.handleColor, 0, 0, false, selectedJoint, true);
					figure.Joints.Add(j);
					j.drawOrder = figure.Joints.IndexOf(j);

					recalcFigureJoints();
					selectedJoint = j;
					glGraphics.Invalidate();
					updateToolboxInfo();
				}
			}
		}

		private void GL_GRAPHICS_MouseUp(object sender, MouseEventArgs e)
		{
			activeJoint = null;

			mouseDown = false;
			glGraphics.Invalidate();
		}
		#endregion

		private void updateToolboxInfo()
		{
			if (selectedJoint == null)
				return;

			pic_handleColor.BackColor = selectedJoint.handleColor;
			pic_lineColor.BackColor = selectedJoint.color;

			num_handleAlpha.Value = selectedJoint.handleColor.A;
			num_lineAlpha.Value = selectedJoint.color.A;
			num_lineThickness.Value = selectedJoint.thickness;
			num_drawOrder.Value = selectedJoint.drawOrder;

			chk_handleVisible.Checked = selectedJoint.handleDrawn;
			chk_lineVisible.Checked = selectedJoint.visible;

			com_lineType.SelectedIndex = selectedJoint.drawState;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			obeyIK = chk_obeyIK.Checked;
		}

		private void num_handleAlpha_ValueChanged(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;

			selectedJoint.defaultHandleColor = Color.FromArgb((int)num_handleAlpha.Value, selectedJoint.defaultHandleColor);
			selectedJoint.handleColor = Color.FromArgb((int)num_handleAlpha.Value, selectedJoint.handleColor);
		}

		private void num_lineAlpha_ValueChanged(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;
			selectedJoint.color = Color.FromArgb((int)num_lineAlpha.Value, selectedJoint.color);
		}

		private void exitStickEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			drawHandles = chk_drawHandles.Checked;
			glGraphics.Invalidate();
		}

		private void btn_toolPointer_Click(object sender, EventArgs e)
		{
			toolType = 0;
			glGraphics.Invalidate();
		}

		private void btn_toolMove_Click(object sender, EventArgs e)
		{
			toolType = 1;
			glGraphics.Invalidate();
		}

		private void btn_toolAdd_Click(object sender, EventArgs e)
		{
			toolType = 2;
			glGraphics.Invalidate();
		}

		private void btn_toolRemove_Click(object sender, EventArgs e)
		{
			toolType = 3;
			glGraphics.Invalidate();
		}

		private void num_lineThickness_ValueChanged(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;

			selectedJoint.thickness = (int)num_lineThickness.Value;
			glGraphics.Invalidate();
		}

		private void com_lineType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;

			selectedJoint.drawState = com_lineType.SelectedIndex;
			glGraphics.Invalidate();
		}

		private void num_drawOrder_ValueChanged(object sender, EventArgs e)
		{
			figure.Joints[(int)num_drawOrder.Value].drawOrder = selectedJoint.drawOrder;
			selectedJoint.drawOrder = (int)num_drawOrder.Value;

			recalcFigureJoints();
			glGraphics.Invalidate();
		}

        private void StickEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            glGraphics.Dispose();

			Point oldLoc = figure.Joints[0].location;

			figure.Joints[0].location = new Point(222, 195);

			for (int i = 1; i < figure.Joints.Count; i++)
			{
				figure.Joints[i].location = new Point(figure.Joints[0].location.X + Functions.calcFigureDiff(oldLoc, figure.Joints[i]).X, figure.Joints[0].location.Y + Functions.calcFigureDiff(oldLoc, figure.Joints[i]).Y);
			}

            Canvas.theCanvas.GL_GRAPHICS.MakeCurrent();
			Canvas.theCanvas.recieveStickFigure(figure);
        }
	}
}