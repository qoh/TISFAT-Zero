using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TISFAT_Zero
{
	partial class StickEditor : Form, ICanDraw
	{
		public StickCustom figure = null;

		public static StickEditor theSticked;

		private bool GLLoaded;
		private int GL_WIDTH, GL_HEIGHT;
		private OpenTK.GLControl glGraphics, oldGL;

		public GLControl GLGraphics
		{
			get
			{
				return glGraphics;	
			}
		}

		private StickJoint activeJoint = null;
		private StickJoint selectedJoint = null;

		private Point mouseLoc;
		private Point pointClicked;

		private bool mouseDown;
		private bool mouseHot;
		private bool obeyIK = false;
		private bool drawHandles = true;

		private int ox, oy;
		private List<int> fx, fy;

		private int toolType = 1;
		private int maxaa = 1;

		private bool loaded = false;

		public StickEditor()
		{
			int aa = 0;
			do
			{
				var mode = new GraphicsMode(32, 0, 0, aa);
				if (mode.Samples == aa)
					maxaa = aa;
				aa += 2;
			} while (aa <= 32);

			InitializeComponent();
		}

		public StickEditor(bool loading)
		{
			InitializeComponent();
			loaded = true;
		}

		private void StickEditor_Load(object sender, EventArgs e)
		{
			num_drawOrder.Maximum = int.MaxValue;
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

			if (!loaded)
			{
				figure = new StickCustom(true);
				figure.drawFig = true;
				figure.drawHandles = true;
				figure.isActiveFig = true;

				figure.FigureJoints.Add(new StickJoint("Base Joint 1", new Point(glGraphics.Width / 2, glGraphics.Height / 2), 12, Color.Black, Color.Yellow));
				figure.FigureJoints.Add(new StickJoint("Base Joint 2", new Point(glGraphics.Width / 2, glGraphics.Height / 2 - 20), 12, Color.Black, Color.Blue));
				figure.FigureJoints[1].parentJoint = figure.FigureJoints[0];
				figure.FigureJoints[0].drawOrder = 0;
				figure.FigureJoints[1].drawOrder = 1;

				recalcFigureJoints();
			}
		}

		public void recalcFigureJoints()
		{
			figure.reSortJoints();

			for (int i = 0; i < figure.FigureJoints.Count(); i++)
			{
				if (figure.FigureJoints[i].parentJoint != null)
				{
					figure.FigureJoints[i].CalcLength(null);
				}
			}

			for (int i = 0; i < figure.FigureJoints.Count(); i++)
			{
				if (figure.FigureJoints[i].parentJoint != null)
				{
					if(!(figure.FigureJoints[i].parentJoint.childJoints.IndexOf(figure.FigureJoints[i]) >= 0))
						figure.FigureJoints[i].parentJoint.childJoints.Add(figure.FigureJoints[i]);
				}
				figure.FigureJoints[i].parentFigure = figure;
			}
			glGraphics.Refresh();
		}

		public void loadFigure(StickCustom fig)
		{
			loaded = true;

			this.figure = new StickCustom(true);
			this.figure.drawFig = true;
			this.figure.drawHandles = true;
			this.figure.isActiveFig = true;

			for (int i = 0; i < fig.FigureJoints.Count; i++)
			{
				this.figure.FigureJoints.Add(fig.FigureJoints[i]);
				StickJoint parentJoint = null;

				if (!(fig.FigureJoints[i].parentJoint == null))
				{
					int parentIndex = fig.FigureJoints.IndexOf(fig.FigureJoints[i].parentJoint);
					
					parentJoint = fig.FigureJoints[i].parentJoint;
				}

				this.figure.FigureJoints[i].parentJoint = parentJoint;
			}

			recalcFigureJoints();
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

		#region GL_GRAPHICS Callbacks
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
			if (!(figure == null) & !mouseDown)
			{
				if (figure.getPointAt(new Point(e.X, e.Y), 6) != -1)
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

			if (toolType == 1)
			{
				if (mouseDown && !(activeJoint == null))
				{
					if (!obeyIK)
					{
						activeJoint.SetPosAbs(e.X, e.Y);
						if (!(activeJoint.parentJoint == null))
							activeJoint.CalcLength(null);
					}
					else
						activeJoint.setPos(e.X, e.Y);
				}
			}

			if (e.Button == MouseButtons.Right && !mouseDown)
			{
				for (int i = 0; i < figure.FigureJoints.Count; i++)
				{
					figure.FigureJoints[i].location.X = fx[i] + (e.X - ox);
					figure.FigureJoints[i].location.Y = fy[i] + (e.Y - oy);
				}
			}

			mouseLoc = e.Location;
			glGraphics.Invalidate();
		}

		private void GL_GRAPHICS_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (toolType == 0)
				{
					selectedJoint = figure.selectPoint(e.Location, 6);
					pointClicked = e.Location;
					mouseDown = e.Button == MouseButtons.Left;

					glGraphics.Invalidate();
					updateToolboxInfo();
				}

				if (toolType == 1)
				{
					activeJoint = figure.selectPoint(e.Location, 6);

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

					StickJoint j = new StickJoint("New Joint", e.Location, (int)num_brushThickness.Value, selectedJoint.jointColor, selectedJoint.handleColor, 0, 0, selectedJoint);
					figure.FigureJoints.Add(j);
					j.drawOrder = figure.FigureJoints.IndexOf(j);

					recalcFigureJoints();
					selectedJoint = j;
					glGraphics.Invalidate();
					updateToolboxInfo();
				}

				if (toolType == 3)
				{
					activeJoint = figure.selectPoint(e.Location, 6);
					if (!(activeJoint == null))
						selectedJoint = activeJoint;

					if(activeJoint == null || activeJoint.parentJoint == null)
						return;

					selectedJoint = activeJoint.parentJoint;
					StickJoint parentJoint = activeJoint;

					activeJoint.removeChildren();
					figure.FigureJoints.Remove(activeJoint);
					recalcFigureJoints();
					glGraphics.Invalidate();
					activeJoint = null;

					updateToolboxInfo();
				}
			}

			if (e.Button == MouseButtons.Right)
			{
				ox = e.X;
				oy = e.Y;
				fx = new List<int>();
				fy = new List<int>();

				for (int i = 0; i < figure.FigureJoints.Count; i++)
				{
					fx.Add(figure.FigureJoints[i].location.X);
					fy.Add(figure.FigureJoints[i].location.Y);
				}
			}
		}

		private void GL_GRAPHICS_Paint(object sender, PaintEventArgs e)
		{
			if (!(selectedJoint == null))
			{
				lbl_jointPosition.Text = "Position: (" + selectedJoint.location.X + ", " + selectedJoint.location.Y + ")";
				lbl_lineLength.Text = "Line Length: " + selectedJoint.length.ToString();
			}

			if (!GLLoaded)
			{
				return;
			}

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit);
			GL_GRAPHICS.MakeCurrent();

			if (!(figure == null))
				figure.drawFigure(this);

			if (!(selectedJoint == null) && drawHandles)
			{
				drawGraphics(2, Color.FromArgb(105, Color.SkyBlue), selectedJoint.location, 4, 4, new Point(selectedJoint.location.X + 7, selectedJoint.location.Y + 7));
				drawGraphics(3, Color.Red, selectedJoint.location, 5, 5, new Point(selectedJoint.location.X + 7, selectedJoint.location.Y + 7));
			}

			if (toolType == 0)
			{
				if (figure.selectPoint(mouseLoc, 4) != null)
				{
					drawGraphics(2, Color.Blue, figure.selectPoint(mouseLoc, 6).location, 1, 1, figure.selectPoint(mouseLoc, 6).location);
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
					drawGraphics(0, Color.FromArgb(100, selectedJoint.jointColor), selectedJoint.location, (int)num_brushThickness.Value, (int)num_brushThickness.Value, mouseLoc);
				}

				if (!mouseDown && !mouseHot)
					drawGraphics(3, Color.Green, mouseLoc, 1, 1, mouseLoc);
			}

			if (!(figure == null))
				if (drawHandles)
					figure.drawFigHandles(this);

			GL_GRAPHICS.SwapBuffers();
		}

		private void GL_GRAPHICS_MouseUp(object sender, MouseEventArgs e)
		{
			activeJoint = null;

			mouseDown = false;
			glGraphics.Invalidate();
		}
		#endregion

		#region Callbacks
		private void updateToolboxInfo()
		{
			if (selectedJoint == null)
				return;

			pic_handleColor.BackColor = selectedJoint.handleColor;
			pic_lineColor.BackColor = selectedJoint.jointColor;

			num_handleAlpha.Value = selectedJoint.handleColor.A;
			num_lineAlpha.Value = selectedJoint.jointColor.A;
			num_lineThickness.Value = selectedJoint.thickness;
			num_drawOrder.Value = selectedJoint.drawOrder;

			chk_handleVisible.Checked = selectedJoint.handleDrawn;
			chk_lineVisible.Checked = selectedJoint.isVisible;

			com_lineType.SelectedIndex = selectedJoint.drawType;
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
			selectedJoint.jointColor = Color.FromArgb((int)num_lineAlpha.Value, selectedJoint.jointColor);
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

			selectedJoint.drawType = com_lineType.SelectedIndex;
			glGraphics.Invalidate();
		}

		private void num_drawOrder_ValueChanged(object sender, EventArgs e)
		{
			selectedJoint.drawOrder = (int)num_drawOrder.Value;

			recalcFigureJoints();
			glGraphics.Invalidate();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dlg_openFile.ShowDialog();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dlg_saveFile.ShowDialog();
		}

		private void dlg_saveFile_FileOk(object sender, CancelEventArgs e)
		{
			//CustomFigSaver.saveFigure(dlg_saveFile.FileName, figure);
		}

		private void dlg_openFile_FileOk(object sender, CancelEventArgs e)
		{
			//CustomFigLoader.loadStickFile(dlg_openFile.FileName);
		}

		private void chk_handleVisible_CheckedChanged(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;

			selectedJoint.handleDrawn = chk_handleVisible.Checked;
			glGraphics.Invalidate();
		}

		private void pic_handleColor_Click(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;

			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			pic_handleColor.BackColor = dlg_Color.Color;

			selectedJoint.defaultHandleColor = dlg_Color.Color;
			selectedJoint.handleColor = dlg_Color.Color;
		}

		private void pic_lineColor_Click(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;

			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			pic_lineColor.BackColor = dlg_Color.Color;
			selectedJoint.jointColor = dlg_Color.Color;

			glGraphics.Invalidate();
		}

		private void StickEditor_FormClosing(object sender, FormClosingEventArgs e)
		{
			glGraphics.Dispose();

			Point oldLoc = figure.FigureJoints[0].location;

			figure.FigureJoints[0].location = new Point(222, 195);

			for (int i = 1; i < figure.FigureJoints.Count; i++)
			{
				figure.FigureJoints[i].location = new Point(figure.FigureJoints[0].location.X + Functions.calcFigureDiff(oldLoc, figure.FigureJoints[i]).X, figure.FigureJoints[0].location.Y + Functions.calcFigureDiff(oldLoc, figure.FigureJoints[i]).Y);
			}

			Program.TheCanvas.GL_GRAPHICS.MakeCurrent();
			if (!loaded)
				Program.TheCanvas.recieveStickFigure(figure);
			else
				Program.TheCanvas.recieveStickFigure(figure, true);
		}
		#endregion
	}
}