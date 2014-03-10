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
using System.Drawing.Imaging;


namespace TISFAT_ZERO
{
	public partial class StickEditor : Form
	{
		public StickCustom figure = null;

		public static StickEditor theSticked;
		public string file;

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

		public StickEditor(string file)
		{
			int aa = 0;
			do
			{
				var mode = new GraphicsMode(32, 0, 0, aa);
				if (mode.Samples == aa)
					maxaa = aa;
				aa += 2;
			} while (aa <= 32);

			this.file = file;
			loaded = true;

			InitializeComponent();
		}

		public StickEditor(bool loading)
		{
			InitializeComponent();
			loaded = true;
		}

		private void StickEditor_Load(object sender, EventArgs e)
		{
			dlg_openBitmap.Filter = Functions.GetImageFilters();
			num_drawOrder.Maximum = int.MaxValue;

			num_bitmapXOffs.Maximum = int.MaxValue;
			num_bitmapXOffs.Minimum = -int.MaxValue;

			num_bitmapYOffs.Maximum = int.MaxValue;
			num_bitmapYOffs.Minimum = -int.MaxValue;

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
			else
			{
				loadFigure(file);
			}
		}

		public void recalcFigureJoints()
		{
			figure.reSortJoints();

			for (int i = 0;i < figure.Joints.Count();i++)
			{
				if (figure.Joints[i].parent != null)
				{
					figure.Joints[i].CalcLength(null);
					figure.Joints[i].recalcAngleToParent();
				}
			}

			for (int i = 0;i < figure.Joints.Count; i++)
			{
				foreach (StickJoint sj in figure.Joints[i].children)
				{
					Console.WriteLine(figure.Joints.Contains(sj));
					sj.parent = figure.Joints[i];
				}
			}

				for (int i = 0;i < figure.Joints.Count();i++)
				{
					if (figure.Joints[i].parent != null)
					{
						if (!(figure.Joints[i].parent.children.IndexOf(figure.Joints[i]) >= 0))
							figure.Joints[i].parent.children.Add(figure.Joints[i]);
					}
					figure.Joints[i].ParentFigure = figure;
				}
			glGraphics.Refresh();
		}

		public void loadFigure(string file)
		{
			CustomFigLoader.loadStickFile(file);
		}

		public void loadFigure(StickCustom fig)
		{
			loaded = true;

			this.figure = new StickCustom(1);
			this.figure.drawFig = true;
			this.figure.drawHandles = true;
			this.figure.isActiveFig = true;

			for (int i = 0;i < fig.Joints.Count;i++)
			{
				this.figure.Joints.Add(fig.Joints[i]);
				StickJoint parent = null;

				if (!(fig.Joints[i].parent == null))
				{
					int parentIndex = fig.Joints.IndexOf(fig.Joints[i].parent);

					parent = fig.Joints[i].parent;
				}

				this.figure.Joints[i].parent = parent;
			}

			recalcFigureJoints();
		}

		#region Drawing Grapics
		public void drawGraphics(int type, Color color, Point one, int width, int height, Point two, int textureID = 0, float rotation = 0)
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

			else if (type == 6) //Texture
			{
				GL.Color4(color);

				GL.Enable(EnableCap.Texture2D);
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

				GL.BindTexture(TextureTarget.Texture2D, textureID);

				GL.PushMatrix();

				GL.Translate(one.X, one.Y, 0);
				GL.Rotate(rotation, 0, 0, 1);

				GL.Begin(BeginMode.Quads);

				GL.TexCoord2(0.0, 1.0);
				GL.Vertex2(0, 0);

				GL.TexCoord2(0.0, 0.0);
				GL.Vertex2(0, -height);

				GL.TexCoord2(1.0, 0.0);
				GL.Vertex2(width, -height);

				GL.TexCoord2(1.0, 1.0);
				GL.Vertex2(width, 0);

				GL.End();

				GL.PopMatrix();

				GL.Disable(EnableCap.Blend);
				GL.Disable(EnableCap.Texture2D);
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

			for (int ii = 0;ii < num_segments;ii++)
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
						if (!(activeJoint.parent == null))
							activeJoint.CalcLength(null);
					}
					else
						activeJoint.SetPos(e.X, e.Y);
				}
			}

			if (e.Button == MouseButtons.Right && !mouseDown)
			{
				for (int i = 0;i < figure.Joints.Count;i++)
				{
					figure.Joints[i].location.X = fx[i] + (e.X - ox);
					figure.Joints[i].location.Y = fy[i] + (e.Y - oy);
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

					StickJoint j = new StickJoint("New Joint", e.Location, (int)num_brushThickness.Value, selectedJoint.color, selectedJoint.handleColor, 0, 0, false, selectedJoint, true);

					figure.Joints.Add(j);
					j.drawOrder = figure.Joints.IndexOf(j);

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

					if (activeJoint == null || activeJoint.parent == null)
						return;

					selectedJoint = activeJoint.parent;
					StickJoint parent = activeJoint;

					activeJoint.removeChildren();
					figure.Joints.Remove(activeJoint);
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

				for (int i = 0;i < figure.Joints.Count;i++)
				{
					fx.Add(figure.Joints[i].location.X);
					fy.Add(figure.Joints[i].location.Y);
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
					drawGraphics(0, Color.FromArgb(100, selectedJoint.color), selectedJoint.location, (int)num_brushThickness.Value, (int)num_brushThickness.Value, mouseLoc);
				}

				if (!mouseDown && !mouseHot)
					drawGraphics(3, Color.Green, mouseLoc, 1, 1, mouseLoc);
			}

			if (!(figure == null))
				if (drawHandles)
					figure.drawFigHandles(1, true);

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
			pic_lineColor.BackColor = selectedJoint.color;

			num_handleAlpha.Value = selectedJoint.handleColor.A;
			num_lineAlpha.Value = selectedJoint.color.A;
			num_lineThickness.Value = selectedJoint.thickness;
			num_drawOrder.Value = selectedJoint.drawOrder;

			chk_handleVisible.Checked = selectedJoint.handleDrawn;
			chk_lineVisible.Checked = selectedJoint.visible;

			com_lineType.SelectedIndex = selectedJoint.drawState;

			com_lineBitmap.Items.Clear();
			for (int i = 0;i < selectedJoint.Bitmap_names.Count;i++)
				com_lineBitmap.Items.Add(selectedJoint.Bitmap_names[i]);

			if (selectedJoint.Bitmap_CurrentID != -1)
				if (selectedJoint.bitmaps.Count != 0)
				{
					com_lineBitmap.SelectedItem = selectedJoint.Bitmap_names[selectedJoint.Bitmap_CurrentID];
					tkb_Rotation.Value = selectedJoint.Bitmap_Rotations[selectedJoint.Bitmap_CurrentID];
					//num_bitmapRotation.Value = tkb_Rotation.Value;
					//num_bitmapXOffs.Value = selectedJoint.Bitmap_Offsets[selectedJoint.Bitmap_CurrentID].X;
					//num_bitmapYOffs.Value = selectedJoint.Bitmap_Offsets[selectedJoint.Bitmap_CurrentID].Y;

					//lbl_bitmapID.Text = "Bitmap ID: " + selectedJoint.Bitmap_IDs[selectedJoint.Bitmap_CurrentID];
				}
				else
				{
					com_lineBitmap.SelectedItem = 0;
					tkb_Rotation.Value = 0;
					num_bitmapRotation.Value = 0;
					num_bitmapXOffs.Value = 0;
					num_bitmapYOffs.Value = 0;

					lbl_bitmapID.Text = "Bitmap ID: <no bitmaps>";
				}
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
			CustomFigSaver.saveFigure(dlg_saveFile.FileName, figure);
		}

		private void dlg_openFile_FileOk(object sender, CancelEventArgs e)
		{
			CustomFigLoader.loadStickFile(dlg_openFile.FileName);
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
			selectedJoint.color = dlg_Color.Color;

			glGraphics.Invalidate();
		}

		private void StickEditor_FormClosing(object sender, FormClosingEventArgs e)
		{
			glGraphics.Dispose();

			Point oldLoc = figure.Joints[0].location;

			figure.Joints[0].location = new Point(222, 195);

			for (int i = 1;i < figure.Joints.Count;i++)
			{
				figure.Joints[i].location = new Point(figure.Joints[0].location.X + Functions.calcFigureDiff(oldLoc, figure.Joints[i]).X, figure.Joints[0].location.Y + Functions.calcFigureDiff(oldLoc, figure.Joints[i]).Y);
			}

			Canvas.theCanvas.GL_GRAPHICS.MakeCurrent();
			if (!loaded)
				Canvas.theCanvas.recieveStickFigure(figure);
			else
				Canvas.theCanvas.recieveStickFigure(figure, true);
		}
		#endregion

		private void btn_addBitmap_Click(object sender, EventArgs e)
		{
			if (selectedJoint == null)
				return;

			dlg_openBitmap.ShowDialog();
		}

		private void dlg_openBitmap_FileOk(object sender, CancelEventArgs e)
		{
			selectedJoint.bitmaps.Add((Bitmap)Bitmap.FromFile(dlg_openBitmap.FileName));

			selectedJoint.Bitmap_IDs.Add(((StickCustom)selectedJoint.ParentFigure).getBitmapCount());
			selectedJoint.Bitmap_names.Add(dlg_openBitmap.FileName.Split('\\').Last());
			selectedJoint.Bitmap_CurrentID = selectedJoint.bitmaps.IndexOf(selectedJoint.bitmaps.Last());
			selectedJoint.Bitmap_Rotations.Add(0);
			selectedJoint.Bitmap_Offsets.Add(new Point(0, 0));
			selectedJoint.recalcAngleToParent();

			updateToolboxInfo();

			Functions.AssignGlid(selectedJoint, selectedJoint.bitmaps.IndexOf(selectedJoint.bitmaps.Last()));
			glGraphics.Refresh();
		}

		private void num_bitmapRotation_ValueChanged(object sender, EventArgs e)
		{
			if (selectedJoint.bitmaps.Count == 0)
				return;

			tkb_Rotation.Value = (int)num_bitmapRotation.Value;
			selectedJoint.Bitmap_Rotations[selectedJoint.Bitmap_CurrentID] = (int)num_bitmapRotation.Value;
			glGraphics.Refresh();
		}

		private void num_bitmapXOffs_ValueChanged(object sender, EventArgs e)
		{
			if (selectedJoint.bitmaps.Count == 0)
				return;

			selectedJoint.Bitmap_Offsets[selectedJoint.Bitmap_CurrentID] = new Point((int)num_bitmapXOffs.Value, selectedJoint.Bitmap_Offsets[selectedJoint.Bitmap_CurrentID].Y);
			glGraphics.Refresh();
		}

		private void num_bitmapYOffs_ValueChanged(object sender, EventArgs e)
		{
			if (selectedJoint.bitmaps.Count == 0)
				return;

			selectedJoint.Bitmap_Offsets[selectedJoint.Bitmap_CurrentID] = new Point(selectedJoint.Bitmap_Offsets[selectedJoint.Bitmap_CurrentID].X, (int)num_bitmapYOffs.Value);
			glGraphics.Refresh();
		}

		private void tkb_Rotation_Scroll(object sender, EventArgs e)
		{
			if (selectedJoint.bitmaps.Count == 0)
				return;

			num_bitmapRotation.Value = tkb_Rotation.Value;
			selectedJoint.Bitmap_Rotations[selectedJoint.Bitmap_CurrentID] = (int)num_bitmapRotation.Value;
			glGraphics.Refresh();
		}

		private void com_lineBitmap_SelectionChangeCommitted(object sender, EventArgs e)
		{
			selectedJoint.Bitmap_CurrentID = com_lineBitmap.SelectedIndex;
			updateToolboxInfo();
			glGraphics.Refresh();
		}

		private void originalTisfatFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dlg_openLegacyFile.Filter = "Legacy Stick Figure Files (*.sff)|*.sff";
			dlg_openLegacyFile.ShowDialog();
		}

		private void dlg_openLegacyFile_FileOk(object sender, CancelEventArgs e)
		{
			figure = new StickCustom(1);
			figure.Joints = LegacyTStickFileLoader.Read(dlg_openLegacyFile.FileName);
			foreach (StickJoint j in figure.Joints)
				j.ParentFigure = figure;

			recalcFigureJoints();
			figure.drawFig = true;
			figure.drawHandles = true;
			figure.isActiveFig = false;
		}

		private void btn_remBitmap_Click(object sender, EventArgs e)
		{
			if (selectedJoint.Bitmap_CurrentID == -1)
				return;

			selectedJoint.bitmaps.RemoveAt(selectedJoint.Bitmap_CurrentID);
			selectedJoint.Bitmap_Rotations.RemoveAt(selectedJoint.Bitmap_CurrentID);
			selectedJoint.Bitmap_Offsets.RemoveAt(selectedJoint.Bitmap_CurrentID);
			selectedJoint.Bitmap_names.RemoveAt(selectedJoint.Bitmap_CurrentID);
			selectedJoint.Bitmap_IDs.RemoveAt(selectedJoint.Bitmap_CurrentID);
			selectedJoint.textureIDs.RemoveAt(selectedJoint.Bitmap_CurrentID);

			selectedJoint.Bitmap_CurrentID -= 1;
			updateToolboxInfo();
			Refresh();
		}
	}
}