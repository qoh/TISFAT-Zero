using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TISFAT_Zero
{
	partial class Timeline : Form, ICanDraw
	{
		#region Buttload of variables

		public MainF MainForm;
		public static List<Layer> Layers = new List<Layer>();
		private static Color[] Colors;

		private static double Scrollbar_eX = 0.0, Scrollbar_eY = 0.0;
		public int pxOffsetX, pxOffsetY;
		private static int scrollAreaY, scrollAreaX, maxFrames, maxLayers;

		private static Rectangle ScrollX = new Rectangle(), ScrollY = new Rectangle();
		private Rectangle StubX = new Rectangle(), StubY = new Rectangle();

		private Point relativePos;

		public int cursorDiff, originalPos;

		//Yeah, there are a lot of booleans. I'm far too lazy to make this into a bitmask.
		public bool isScrolling, isScrollingY;

		public bool mouseDown;
		private bool GLLoaded = false, forceRefresh = false;
		private bool cancelTimerOnMouseUp = false, cancelevent = false;

		//I'm using bit masks here so we don't flood this area with (8 more) bools. I said I was too lazy to make the OTHER ones a bitmask!
		//bits 1-4: scrollbars
		//1st bit:		is anything selected
		//2nd bit:		mouse over or mouse down (0,1)
		//3rd-4th bit:	which is selected (3rd: 0:x, 1:y 4th: 0:top/left, 1:bottom/right)
		//repeated for bits 5-8 for scrolling stubs
		public byte selectedScrollItems = 0;

		public static int timelineFrameLength = 512, timelineRealLength, timelineHeight;

		//If this value is -1 that means the timeline is selected
		public static int selectedLayer_Ind = 0, selectedFrame_Ind = 0;

		private byte selectedFrame_Type = 1;
		private byte selectedFrame_RType = 1; //0: plain  1: keyframe  2: tween

		public KeyFrame selectedKeyFrame;
		public Layer selectedLayer;
		public Frameset selectedFrameset;

		//We also use a lot of bitmaps.. ^ ^'
		private T0Bitmap[] zerotonine = new T0Bitmap[10];

		private static List<T0Bitmap> layerNames = new List<T0Bitmap>();
		private T0Bitmap[] stubs = new T0Bitmap[12];
		private T0Bitmap TIMELINE;

		public GLControl GLGraphics
		{
			get { return glgraphics; }
		}

		//For use with the timer
		public delegate void timerDel();

		private timerDel timerDelegate;

		#endregion Buttload of variables

		public Timeline(MainF f)
		{
			InitializeComponent();

			LoadGraphics();

			MainForm = f;
			Colors = new Color[] { Color.FromArgb(220, 220, 220), Color.FromArgb(140, 140, 140), Color.FromArgb(200, 200, 200), Color.FromArgb(70, 120, 255), Color.FromArgb(40, 230, 255), Color.FromArgb(30, 100, 255) };

			zerotonine[0] = new T0Bitmap(Properties.Resources._0); zerotonine[1] = new T0Bitmap(Properties.Resources._1);
			zerotonine[2] = new T0Bitmap(Properties.Resources._2); zerotonine[3] = new T0Bitmap(Properties.Resources._3);
			zerotonine[4] = new T0Bitmap(Properties.Resources._4); zerotonine[5] = new T0Bitmap(Properties.Resources._5);
			zerotonine[6] = new T0Bitmap(Properties.Resources._6); zerotonine[7] = new T0Bitmap(Properties.Resources._7);
			zerotonine[8] = new T0Bitmap(Properties.Resources._8); zerotonine[9] = new T0Bitmap(Properties.Resources._9);

			Point y = new Point(7, -1);
			Font F = new Font("Arial", 10);

			using (Bitmap raw = new Bitmap(79, 15))
			using (Graphics g = Graphics.FromImage(raw))
			{
				g.Clear(Colors[5]);
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				g.DrawString("TIMELINE", F, Brushes.Black, y);

				TIMELINE = new T0Bitmap(raw, new Point(0, 0));
			}

			//Fetch the scrollbar stub bitmaps
			stubs[0] = new T0Bitmap(Properties.Resources.stub_x_l); stubs[1] = new T0Bitmap(Properties.Resources.stub_x_r);
			stubs[2] = new T0Bitmap(Properties.Resources.stub_y_t); stubs[3] = new T0Bitmap(Properties.Resources.stub_y_b);
			stubs[4] = new T0Bitmap(Properties.Resources.stub_x_l_a); stubs[5] = new T0Bitmap(Properties.Resources.stub_x_r_a);
			stubs[6] = new T0Bitmap(Properties.Resources.stub_y_t_a); stubs[7] = new T0Bitmap(Properties.Resources.stub_y_b_a);
			stubs[8] = new T0Bitmap(Properties.Resources.stub_x_l_c); stubs[9] = new T0Bitmap(Properties.Resources.stub_x_r_c);
			stubs[10] = new T0Bitmap(Properties.Resources.stub_y_t_c); stubs[11] = new T0Bitmap(Properties.Resources.stub_y_b_c);

			ScrollY.Width = 6;
			ScrollX.Height = 6;

			StubX.Height = 12;
			StubY.Width = 12;

			addNewLayer(typeof(StickLayer));

			timelineRealLength = 9 * timelineFrameLength;
		}

		public void Timeline_Resize(object sender, EventArgs e)
		{
			glgraphics.MakeCurrent();

			if (!GLLoaded)
				return;

			GL.Viewport(0, Screen.PrimaryScreen.Bounds.Height - Height, Width, Height);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();

			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(Colors[0]);

			GL.Ortho(0, Width, Height, 0, 0, 1);

			//Hax to make sure that everything renders where it should
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.Translate(0.375, 0.375, 0);

			scrollAreaY = Height - 49; scrollAreaX = Width - 114;

			float viewRatioY = (float)scrollAreaY / timelineHeight, viewRatioX = (float)scrollAreaX / timelineRealLength;

			ScrollY.Height = viewRatioY < 1 ? Math.Max((int)(scrollAreaY * viewRatioY), 16) : -1;
			ScrollY.Location = ScrollY.Height != -1 ? new Point(Width - 9, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 26)) : new Point(-1, -1);

			ScrollX.Width = viewRatioX < 1 ? Math.Max((int)(scrollAreaX * viewRatioX), 16) : -1;
			ScrollX.Location = ScrollX.Width != -1 ? new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 91), Height - 9) : new Point(-1, -1);

			pxOffsetX = (int)(Scrollbar_eX * timelineRealLength);
			pxOffsetY = (int)(Scrollbar_eY * timelineHeight);

			StubX.Width = Width - 92;
			StubX.Location = new Point(80, Height - 12);

			StubY.Height = Height - 16;
			StubY.Location = new Point(Width - 12, 16);

			maxFrames = (int)Math.Ceiling((Width - 80) / 9d + 1);
			maxLayers = (int)Math.Ceiling((Height - 12) / 16d + 1);

			stubs[0].texPos = new Point(StubX.Left + 3, StubX.Top + 2);
			stubs[1].texPos = new Point(StubX.Right - 8, StubX.Top + 2);
			stubs[2].texPos = new Point(StubY.Left + 2, StubY.Top + 3);
			stubs[3].texPos = new Point(StubY.Left + 2, StubY.Bottom - 20);
			stubs[4].texPos = stubs[0].texPos; stubs[5].texPos = stubs[1].texPos;
			stubs[6].texPos = stubs[2].texPos; stubs[7].texPos = stubs[3].texPos;
			stubs[8].texPos = stubs[0].texPos; stubs[9].texPos = stubs[1].texPos;
			stubs[10].texPos = stubs[2].texPos; stubs[11].texPos = stubs[3].texPos;

			this.Invalidate();
		}

		private void Timeline_Paint(object sender, PaintEventArgs e)
		{
			Timeline_Refresh();
		}

		public void Timeline_Refresh()
		{
			#region Init. stuff

			glgraphics.MakeCurrent();

			GL.Disable(EnableCap.Multisample);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			int ind_L = (int)Math.Ceiling((pxOffsetY + 1) / 16d - 1), ind_F = (int)Math.Ceiling((pxOffsetX + 1) / 9d - 1);
			int start_L = 16 - (pxOffsetY % 16), start_F = 80 - (pxOffsetX % 9);

			if (ind_L < 0)
				ind_L = 0;
			if (ind_F < 0)
				ind_F = 0;

			int endL_ind = Math.Min(Layers.Count, ind_L + maxLayers), endF_ind = ind_F + maxFrames;

			//Getting the draw order for this exactly right was very difficult...

			#endregion Init. stuff

			#region Selected layer hilighting

			if (selectedLayer_Ind >= ind_L && selectedLayer_Ind < endL_ind)
			{
				GL.Color3(Colors[2]);
				GL.Begin(PrimitiveType.Quads);

				int y = 16 * (selectedLayer_Ind - ind_L) + start_L;

				GL.Vertex2(80, y); GL.Vertex2(80, y + 16);
				GL.Vertex2(Width, y + 16); GL.Vertex2(Width, y);

				GL.End();
			}

			#endregion Selected layer hilighting

			#region Colored bars and such

			GL.Color4(Colors[1]);
			GL.Begin(PrimitiveType.Lines);

			GL.Vertex2(80, 15); GL.Vertex2(Width, 15);

			GL.End();

			for (int p = start_F, a = ind_F; a < endF_ind; p += 9, a++)
			{
				Point x = new Point(p, 0);

				Color c = Color.Empty;

				if ((a + 1) % 100 == 0)
					c = Color.Pink;
				else if ((a + 1) % 10 == 0)
					c = Color.FromArgb(40, 230, 255);

				if (c != Color.Empty)
				{
					GL.Color3(c);
					GL.Begin(PrimitiveType.Quads);

					GL.Vertex2(x.X, 16); GL.Vertex2(x.X, Height);
					GL.Vertex2(x.X + 8, Height); GL.Vertex2(x.X + 8, 16);

					GL.End();

					drawFrame(x, c, true);
				}

				GL.Color3(Colors[1]);
				GL.Begin(PrimitiveType.Lines);

				GL.Vertex2(p - 1, 0); GL.Vertex2(p - 1, Height);

				GL.End();
			}

			#endregion Colored bars and such

			#region Layer Frames rendering

			bool renderSelected = selectedFrame_Ind >= ind_F && selectedFrame_Ind < endF_ind && selectedLayer_Ind >= ind_L && selectedLayer_Ind < endL_ind;
			bool rendered = false, renderinloop = renderSelected && selectedFrame_RType == 2;
			Point rendpoint = new Point(9 * (selectedFrame_Ind - ind_F) + start_F, 16 * (selectedLayer_Ind - ind_L) + start_L);

			for (int p1 = start_F, p2 = start_L, a = ind_L; a < endL_ind; p2 += 16, a++)
			{
				Layer current = Layers[a];

				//Get the first frameset we need to render
				int[] pos = current.BinarySearchDeep(ind_F);

				int framesetpos = -1, framepos = -1;

				if (pos[0] < 0)
				{
					framesetpos = -pos[0] - 1;
					framepos = 0;
				}
				else
				{
					framesetpos = pos[0];

					if (pos[1] < 0)
						framepos = -pos[1] - 1;
					else
						framepos = pos[1];
				}

				if (current[framesetpos].FrameCount - framepos <= 1)
					framepos--;

				bool renderselectedthislayer = renderinloop && !rendered && a == selectedLayer_Ind;

				for (Frameset fs = current[framesetpos]; fs.StartingPosition < endF_ind && framesetpos < current.Framesets.Count; )
				{
					int max = fs.FrameCount - 1;

					if (max == 1 && framepos == 1)
						framepos = 0;

					int renderingpos1 = 9 * (fs[framepos].Position - ind_F) + p1, renderingpos2 = 9 * (fs[framepos + 1].Position - ind_F) + p1;

					bool rendernow = renderselectedthislayer && !rendered && fs.EndingPosition >= selectedFrame_Ind;

					for (KeyFrame f1 = fs[framepos], f2 = fs[framepos + 1]; framepos < max; )
					{
						Color c = Color.DarkGray;

						if (framepos == 0)
							c = Color.FromArgb(255, 200, 190, 245);

						drawFrame(renderingpos1, p2, c);
						renderingpos1 += 9;

						if (renderingpos1 != renderingpos2)
						{
							GL.Color3(Color.White);

							GL.Begin(PrimitiveType.Quads);

							GL.Vertex2(renderingpos1, p2); GL.Vertex2(renderingpos1, p2 + 15);
							GL.Vertex2(renderingpos2, p2 + 15); GL.Vertex2(renderingpos2, p2);

							GL.End();

							if (rendernow && selectedFrame_Ind > f1.Position && selectedFrame_Ind < f2.Position)
							{
								drawFrame(rendpoint, Color.Red, true);

								rendered = true;
							}

							p2 += 11;

							GL.Color4(Color.Black);
							GL.Begin(PrimitiveType.LineStrip);

							GL.Vertex2(renderingpos1 + 2, p2); GL.Vertex2(renderingpos2 - 3, p2);
							GL.Vertex2(renderingpos2 - 6, p2 - 3);

							GL.End();

							GL.Begin(PrimitiveType.Lines);

							GL.Vertex2(renderingpos2 - 3, p2); GL.Vertex2(renderingpos2 - 5, p2 + 3);
							p2 -= 11;

							GL.Color4(Colors[1]);
							GL.Vertex2(renderingpos2 - 1, p2); GL.Vertex2(renderingpos2 - 1, p2 + 16);

							GL.End();
						}

						renderingpos1 = renderingpos2;

						if (++framepos >= max)
							break;

						f1 = f2;
						f2 = fs[framepos + 1];

						renderingpos2 = 9 * (fs[framepos + 1].Position - ind_F) + p1;
					}

					drawFrame(renderingpos2, p2, Color.FromArgb(200, 190, 245));

					framesetpos++;
					if (framesetpos < current.Framesets.Count)
						fs = current[framesetpos];

					framepos = 0;
				}
			}

			if (!renderinloop)
				drawFrame(rendpoint, Color.Red, selectedFrame_RType == 0);

			#endregion Layer Frames rendering

			#region Timeline numbers section

			for (int p = start_F, a = ind_F; a < endF_ind; p += 9, a++)
			{
				Point x = new Point(p, 0);

				Color c = Color.Empty;

				if ((a + 1) % 100 == 0)
					c = Color.Pink;
				else if ((a + 1) % 10 == 0)
					c = Color.FromArgb(40, 230, 255);

				if (c == Color.Empty)
					if (selectedLayer_Ind != -1)
						drawFrame(x, Colors[0], true);
					else
						drawFrame(x, Colors[2], true);
				else
					drawFrame(x, c, true);

				zerotonine[(a + 1) % 10].Draw(this, x);

				GL.Color3(Colors[1]);
				GL.Begin(PrimitiveType.Lines);

				GL.Vertex2(p - 1, 0);
				GL.Vertex2(p - 1, 15);

				GL.End();
			}

			#endregion Timeline numbers section

			#region Horiz. Lines

			//I don't have to do color3 here again because it was already done at the end of the previous loop

			GL.Begin(PrimitiveType.Lines);

			GL.Vertex2(79, 15);
			GL.Vertex2(Width, 15);

			for (int p = start_L + 15, a = ind_L; p < Height; p += 16, a++)
			{
				GL.Vertex2(80, p);
				GL.Vertex2(Width, p);
			}
			GL.End();

			int endll = 16 * endL_ind + start_L + 15;

			if (endll < Height)
			{
				//Draw a blue rectangle through the remaining area of the layers display
				GL.Begin(PrimitiveType.Quads);
				GL.Color3(Colors[5]);

				GL.Vertex2(0, endll-16);
				GL.Vertex2(0, Height);
				GL.Vertex2(79, Height);
				GL.Vertex2(79, endll-16);

				GL.End();
			}

			//If the selected layer is the timeline then we need to do some fancy stuff to make a nice little seeker line
			if (selectedLayer_Ind == -1)
			{
				int x = 9 * (selectedFrame_Ind - ind_F) + start_F;

				GL.Color3(Color.Red);
				GL.Begin(PrimitiveType.LineStrip);

				GL.Vertex2(x + 7, 1); GL.Vertex2(x + 7, 15);
				GL.Vertex2(x, 14); GL.Vertex2(x, 0);
				GL.Vertex2(x + 8, 0); GL.Vertex2(x + 8, 16);

				x--;

				GL.Vertex2(x, 15); GL.Vertex2(x, 0);

				GL.End();
				GL.Begin(PrimitiveType.Lines);

				x += 4;

				GL.Vertex2(x, 16); GL.Vertex2(x, Height);
				GL.Vertex2(x + 1, 16); GL.Vertex2(x + 1, Height);

				GL.End();
			}

			for (int p = start_L + 15, a = ind_L; a < endL_ind; p += 16, a++)
			{
				GL.Begin(PrimitiveType.Lines);
				GL.Color3(Color.Black);
				GL.Vertex2(0, p); GL.Vertex2(79, p);
				GL.End();

				layerNames[a].Draw(this, new Point(0, p - 15));
			}

			#endregion Horiz. Lines

			#region Misc other stuff

			TIMELINE.Draw(this);

			GL.Color3(Color.Black);
			GL.Begin(PrimitiveType.Lines);

			GL.Vertex2(0, 15); GL.Vertex2(79, 15);

			GL.Vertex2(79, 0); GL.Vertex2(79, Height);

			GL.End();

			GL.Begin(PrimitiveType.Quads);

			GL.Vertex2(0, Height - 12); GL.Vertex2(79, Height - 12);
			GL.Vertex2(79, Height); GL.Vertex2(0, Height);

			GL.End();

			renderRectangle(StubX, Color.DarkGray);
			renderRectangle(StubY, Color.DarkGray);
			renderRectangle(ScrollX, Color.Gray);
			renderRectangle(ScrollY, Color.Gray);

			byte stub = selectedScrollItems;

			if (isBitSet(stub, 0))
			{
				Color x = isBitSet(stub, 1) ? Color.LightGray : Color.DimGray;

				Rectangle toDraw = !isBitSet(stub, 3) ? ScrollY : ScrollX;
				renderRectangle(toDraw, x);
			}

			for (int a = 0; a < 4; a++)
				stubs[a].Draw(this);

			stub >>= 4;

			//This formula is used to determine which stub to draw, since I ordered them so neatly in the array this is possible.
			if (isBitSet(stub, 0))
				stubs[4 + ((stub & 2) << 1) + ((stub & 4) >> 1) + ((stub & 8) >> 3)].Draw(this);

			#endregion Misc other stuff

			glgraphics.SwapBuffers();
		}

		public void LoadGraphics()
		{
			glgraphics.MakeCurrent();

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();

			GL.Viewport(0, Screen.PrimaryScreen.Bounds.Height - Height, Width, Height);
			GL.Ortho(0, Width, Height, 0, 0, 1);

			GL.Disable(EnableCap.DepthTest);

			GLLoaded = true;
		}

		private void Timeline_Load(object sender, EventArgs e)
		{
			Timeline_Resize(null, null);
		}

		public static Layer addNewLayer(Type layerType, string name = null)
		{
			//Make sure the given type is a type derived from Layer
			if (layerType.BaseType != typeof(Layer))
				return null;

			//Construct the name of the layer and use reflection to call the constructor of the type and get the result
			if (name == null)
				name = "Layer " + (Layers.Count + 1);

			Layer newLayer = (Layer)layerType.GetConstructor(new Type[] { typeof(string), typeof(int) }).Invoke(new object[] { name, 0 });

			//Add the layer to the list
			Layers.Add(newLayer);

			//Construct the name bitmap
			Point y = new Point(0, -1);
			Font F = new Font("Arial", 10);

			using (Bitmap raw = new Bitmap(79, 15))
			using (Graphics g = Graphics.FromImage(raw))
			{
				g.Clear(Colors[3]);
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				g.DrawString(name, F, Brushes.Black, y);

				layerNames.Add(new T0Bitmap(raw));
			}

			timelineHeight = clamp(Layers.Count * 16 - (Program.TheMainForm.splitContainer1.Panel1.Height - 28), 0, int.MaxValue);

			return newLayer;
		}

		public void drawGraphics(int type, Color color, Point one, int width, int height, Point two)
		{ /* Never used, so hence it's blank */ }

		private void renderRectangle(Rectangle rect, Color color)
		{
			GL.Color4(color);
			GL.Begin(PrimitiveType.Quads);

			int x1 = rect.Left, x2 = rect.Right;
			int y1 = rect.Top, y2 = rect.Bottom;
			GL.Vertex2(x1, y1); GL.Vertex2(x1, y2);
			GL.Vertex2(x2, y2); GL.Vertex2(x2, y1);

			GL.End();
		}

		private void drawFrame(Point p, Color i, bool nofancy = false)
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			if (i != Color.Transparent)
			{
				int a = p.Y + 16, b = p.X + 8;

				GL.Color4(i);
				GL.Begin(PrimitiveType.Quads);

				GL.Vertex2(p.X, p.Y);
				GL.Vertex2(p.X, a - 1);
				GL.Vertex2(b, a - 1);
				GL.Vertex2(b, p.Y);

				if (!nofancy)
				{
					GL.Color4(Color.FromArgb(127, 0, 0, 0));

					GL.Vertex2(p.X, p.Y);
					GL.Vertex2(p.X, p.Y + 2);
					GL.Vertex2(b, p.Y + 2);
					GL.Vertex2(b, p.Y);

					GL.Color4(Color.Black);

					a -= 7;
					p.X += 2;
					GL.Vertex2(p.X, a);
					GL.Vertex2(p.X, a + 4);
					GL.Vertex2(p.X + 4, a + 4);
					GL.Vertex2(p.X + 4, a);

					GL.Color4(Color.White);

					a++;
					p.X++;
					GL.Vertex2(p.X, a);
					GL.Vertex2(p.X, a + 2);
					GL.Vertex2(p.X + 2, a + 2);
					GL.Vertex2(p.X + 2, a);
				}

				GL.End();
			}
		}

		private void drawFrame(int x, int y, Color i, bool nofancy = false)
		{
			drawFrame(new Point(x, y), i, nofancy);
		}

		private void Timeline_MouseMove(object sender, MouseEventArgs e)
		{
			relativePos = e.Location;
			byte old = selectedScrollItems;
			Point old1 = ScrollY.Location, old2 = ScrollX.Location;
			bool updateScrollLocation = false;
			int x = e.X, y = e.Y;
			int dx = Width - x, dy = Height - y;

			if (!mouseDown)
			{
				cursorDiff = -1;
				isScrolling = false;
				isScrollingY = false;
				selectedScrollItems &= 0xfd; //Get rid of the 2nd bit
			}

			#region Clicker hilighting

			//Only start checking the massive monolithic conditional tree if dx or dy is <= 12. This is because all the scrollers
			//are 12 pixels out from their respective sides. It also checks if the first bit of selectedScrollItems is 0,
			//this is so that it doesn't update the selected scroll items while the mouse is clicked down.

			if (!isBitSet(selectedScrollItems, 1))
			{
				if (dx <= 12 || dy <= 12)
				{
					selectedScrollItems = (byte)(!mouseDown ? 0 : 34);
					if (dx <= 12)
					{
						if (y < 16)
							selectedScrollItems = 0;
						else if (y >= 26)
						{
							if (dy <= 12)
								selectedScrollItems = 0;
							else if (dy >= 24)
							{
								updateScrollLocation = mouseDown;

								if (mouseDown)
								{
									if (y < ScrollY.Top)
										Scrollbar_eY = Math.Max(0, (double)(ScrollY.Top - 26 - ScrollY.Height) / (scrollAreaY - ScrollY.Height));
									else if (y >= ScrollY.Bottom)
										Scrollbar_eY = Math.Min(1, (double)(ScrollY.Top - 26 + ScrollY.Height) / (scrollAreaY - ScrollY.Height));
								}

								if (y >= ScrollY.Top && y < ScrollY.Bottom)
									selectedScrollItems |= 1;
							}
							else
							{
								selectedScrollItems |= 208;
								if (mouseDown && selectedScrollItems != old)
									startScrolling(false, false);
							}
						}
						else
						{
							selectedScrollItems |= 80;

							if (mouseDown && selectedScrollItems != old)
								startScrolling(false, true);
						}
					}
					else
					{
						if (dx <= 23)
						{
							selectedScrollItems |= (byte)(dy < 12 ? 144 : 0);
							if (mouseDown && selectedScrollItems != old)
								startScrolling(true, true);
						}
						else if (x >= 80)
						{
							if (x >= 91)
							{
								updateScrollLocation = mouseDown;

								if (mouseDown)
								{
									if (x < ScrollX.Left)
										Scrollbar_eX = Math.Max(0, (double)(ScrollX.Left - 90 - ScrollX.Width) / (scrollAreaX - ScrollX.Width));
									else if (x >= ScrollX.Right)
										Scrollbar_eX = Math.Min(1, (double)(ScrollX.Left - 90 + ScrollX.Width) / (scrollAreaX - ScrollX.Width));
								}

								if (x >= ScrollX.Left && x < ScrollX.Right)
									selectedScrollItems |= 9;
							}
							else
							{
								selectedScrollItems |= 16;

								if (mouseDown && selectedScrollItems != old)
									startScrolling(true, false);
							}
						}
					}
				}
				else
					selectedScrollItems = 0;
			}

			#endregion Clicker hilighting

			#region Scrollbar updating

			if (mouseDown)
			{
				if ((selectedScrollItems & 1) == 1 && !isScrolling)
				{
					isScrolling = true;
					isScrollingY = !isBitSet(selectedScrollItems, 3);
					selectedScrollItems |= 2;

					cursorDiff = isScrollingY ? y - ScrollY.Top : x - ScrollX.Left;
				}
				else if (isScrolling)
				{
					if (isScrollingY)
						Scrollbar_eY = Math.Min(1, Math.Max(0, (double)((y - 26) - cursorDiff) / (scrollAreaY - ScrollY.Height)));
					else
						Scrollbar_eX = Math.Min(1, Math.Max(0, (double)((x - 90) - cursorDiff) / (scrollAreaX - ScrollX.Width)));
					updateScrollLocation = true;
				}

				if (!isScrolling && x != originalPos)
				{
					int newSelected = clamp((pxOffsetX + (x - 80)) / 9, 0, timelineFrameLength);
					if (newSelected < 0)
						newSelected = 0;

					if (selectedLayer_Ind == -1)
					{
						selectedFrame_Ind = newSelected;
						setFrame();
						doRender();
					}
					else if (selectedFrame_Type != 0)
					{
						if (selectedFrame_RType == 1)
						{
							if (selectedLayer.moveKeyframeAtTo(selectedFrame_Ind, newSelected))
								selectedFrame_Ind = newSelected;
						}
						else if (selectedFrame_RType == 2)
						{
							int tmp1 = newSelected - (selectedFrame_Ind - selectedFrameset.StartingPosition);
							int tmp2 = clamp(tmp1, 0, timelineRealLength);
							if (tmp1 == tmp2 && selectedLayer.moveFramesetTo(selectedFrameset, tmp2))
								selectedFrame_Ind = newSelected;
						}
					}
				}
			}

			if (updateScrollLocation)
			{
				ScrollY.Location = new Point(Width - 9, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 26));
				ScrollX.Location = new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 91), Height - 9);
				pxOffsetX = (int)(Scrollbar_eX * timelineRealLength);
				pxOffsetY = (int)(Scrollbar_eY * timelineHeight);
			}

			#endregion Scrollbar updating

			//Only refresh if we need to! Saves precious processing power, and you money on your electrical bill!
			if (forceRefresh || old != selectedScrollItems || updateScrollLocation)
				Timeline_Refresh();
		}

		private void Timeline_MouseDown(object sender, MouseEventArgs e)
		{
			int x = e.X, y = e.Y;
			int dx = Width - x, dy = Height - y;

			if (!(dx <= 12 || dy <= 12))
			{
				if (x > 79)
				{
					//User is clicking inside the keyframes area
					if (y > 15)
					{
						//Set selected layer index and frame index
						selectedLayer_Ind = clamp((pxOffsetY + (y - 16)) / 16, 0, Layers.Count - 1); selectedFrame_Ind = (pxOffsetX + (x - 80)) / 9;

						forceRefresh = true;

						if (selectedLayer_Ind >= Layers.Count)
						{
							forceRefresh = false;
							return;
						}
						else
						{
							selectedLayer = Layers[selectedLayer_Ind];
							foreach (Layer l in Layers)
								l.layerIsSelected = false;
							Layers[selectedLayer_Ind].layerIsSelected = true;
						}

						selectedFrame_Type = Layers[selectedLayer_Ind].getFrameTypeAt(selectedFrame_Ind);
						selectedFrame_RType = (byte)(selectedFrame_Type == 4 ? 2 : selectedFrame_Type == 0 ? 0 : 1);

						if (selectedFrame_RType != 0)
						{
							selectedFrameset = selectedLayer.GetFramesetAt(selectedFrame_Ind);
							if (selectedFrame_RType == 1)
								selectedKeyFrame = selectedFrameset.GetKeyFrameAt(selectedFrame_Ind);
						}

						setFrame();
						doRender();
					}
					else //User clicked on the timeline
					{
						selectedLayer_Ind = -1;
						selectedFrame_Ind = (pxOffsetX + (x - 80)) / 9;

						Program.TheCanvas.GLGraphics.SwapBuffers();

						forceRefresh = true;

						setFrame();
						doRender();
					}
				}
				else //User clicked in layers
				{
					//Nothing here at the moment.
				}
			}

			if (e.Button == MouseButtons.Left)
			{
				mouseDown = true;
				forceRefresh = true;
				originalPos = x;
			}
			else if (e.Button == MouseButtons.Right)
			{
				//Figure out what area we're clicking so we know what context menu items to display
				//-1: ???
				//0: frame area
				//1: layers area
				//2: scrollbars (what do we use here?)
				//3: timeline
				//4: timeline controls area (what do we use here?)

				sbyte selectedArea = -1;

				if (x < 80 && y >= 16)
				{
					selectedArea = 1;
					selectedLayer_Ind = clamp((pxOffsetY + (y - 16)) / 16, 0, Layers.Count - 1);

					foreach (Layer l in Layers)
						l.layerIsSelected = false;
					Layers[selectedLayer_Ind].layerIsSelected = true;

					selectedFrame_Ind = 0;
					selectedFrame_Type = Layers[selectedLayer_Ind].getFrameTypeAt(0);
					selectedFrame_RType = (byte)(selectedFrame_Type == 4 ? 2 : selectedFrame_Type == 0 ? 0 : 1);

					setFrame();
					doRender();
				}
				else if (x < 80 && y < 16) ; //Not mistaken empty statement; it prevents the right click menu from popping up when you right click on "TIMELINE"
				else if (dx <= 12 ^ dy <= 12)
					selectedArea = -1; //Scrollbars
				else if (y >= 16)
					selectedArea = 0;
				else if (y < 16)
					selectedArea = 3;

				if (selectedArea == -1)
					cancelevent = true;
				else
				{
					cancelevent = false;

					//Loop through all the items in the context menu, and if their name contains the selected area then set it to visible.
					foreach (ToolStripItem item in contextMenuStrip1.Items)
						item.Visible = ((string)item.Tag).IndexOf("" + selectedArea) >= 0;
				}
			}

			Timeline_MouseMove(sender, e);
		}

		public void Timeline_MouseUp(object sender, MouseEventArgs e)
		{
			if (cancelTimerOnMouseUp)
				TimelineTimer.Stop();

			selectedScrollItems &= 0xfd;

			if (e.Button == MouseButtons.Left)
			{
				mouseDown = cancelTimerOnMouseUp = false;
				Timeline_MouseMove(sender, e);
			}
		}

		private bool isBitSet(byte x, byte n)
		{
			return (x & (1 << n)) >> n != 0;
		}

		private void OnMouseWheel(object sender, MouseEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift && ScrollX.Location.X != -1)
			{
				int old = pxOffsetX;
				pxOffsetX = Math.Max(0, Math.Min(pxOffsetX - e.Delta, timelineRealLength));

				if (old == pxOffsetX)
					return;

				Scrollbar_eX = Math.Min(1, Math.Max(0, pxOffsetX / (double)timelineRealLength));
				ScrollX.Location = new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 91), Height - 9);
			}
			else
			{
				int old = pxOffsetY;
				pxOffsetY = Math.Max(0, Math.Min(pxOffsetY - e.Delta, timelineHeight));

				if (old == pxOffsetY)
					return;

				Scrollbar_eY = Math.Min(1, Math.Max(0, pxOffsetY / (double)timelineHeight));
				ScrollY.Location = new Point(Width - 9, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 26));
			}

			Timeline_Refresh();
		}

		private void TimelineTimer_Tick(object sender, EventArgs e)
		{
			//Call the method in the timer delegate (That way we aren't restricted to one task per timer!)
			timerDelegate();
		}

		private void startScrolling(bool x, bool up)
		{
			if (!x)
			{
				pxOffsetY = clamp(16 * (((pxOffsetY + 0) / 16) + (up ? -1 : 1)), 0, timelineHeight);
				Scrollbar_eY = clamp(pxOffsetY / (double)timelineHeight, 0, 1);
				ScrollY.Location = new Point(Width - 9, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 26));
			}
			else
			{
				pxOffsetX = clamp(9 * ((pxOffsetX / 9) + (up ? 1 : -1)), 0, timelineRealLength);
				Scrollbar_eX = clamp(pxOffsetX / (double)timelineRealLength, 0, 1);
				ScrollX.Location = new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 91), Height - 9);
			}

			cancelTimerOnMouseUp = true;

			//This basically changes what happens when the timer gets ticked.
			timerDelegate = () =>
			{
				if (!cancelTimerOnMouseUp)
					return;

				TimelineTimer.Interval = 50;

				//This works because C# is smart enough to save in-scope variables until the delegate is out of use
				timerDelegate = () =>
				{
					if (!x)
					{
						pxOffsetY = clamp(16 * (((pxOffsetY + 0) / 16) + (up ? -1 : 1)), 0, timelineHeight);
						Scrollbar_eY = clamp(pxOffsetY / (double)timelineHeight, 0, 1);
						ScrollY.Location = new Point(Width - 9, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 26));
					}
					else
					{
						pxOffsetX = clamp(9 * ((pxOffsetX / 9) + (up ? 1 : -1)), 0, timelineRealLength);
						Scrollbar_eX = clamp(pxOffsetX / (double)timelineRealLength, 0, 1);
						ScrollX.Location = new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 91), Height - 9);
					}

					Timeline_Refresh();
				};

				TimelineTimer.Start();
			};

			TimelineTimer.Interval = 300;
			TimelineTimer.Start();
		}

		public static int clamp(int num, int min, int max)
		{
			return Math.Min(max, Math.Max(min, num));
		}

		public static double clamp(double num, double min, double max)
		{
			return Math.Min(max, Math.Max(min, num));
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			if (cancelevent)
				e.Cancel = true;
		}

		private void contextMenuStrip1_Opened(object sender, EventArgs e)
		{
			//Get all the visible, non-seperator toolstrip items in a list
			List<ToolStripItem> items = (from ToolStripItem item in contextMenuStrip1.Items where !item.Name.Contains("Separator") && item.Visible select item).ToList();

			if (items[0].Name.Contains("insert"))
			{
				//We've clicked on the keyframe area.
				items[0].Enabled = selectedFrame_RType == 2;
				items[1].Enabled = items[0].Enabled;
				items[2].Enabled = selectedFrame_RType == 1 && selectedFrameset.FrameCount > 2;
				int space = selectedLayer.getEmptyFramesCount(selectedFrame_Ind);
				items[3].Enabled = space == -2 || space >= 2;
				items[4].Enabled = selectedFrame_RType != 0;
			}
		}

		private void YayyyToolStripItem_ClickEvent(object sender, EventArgs e)
		{
			ToolStripItem theSender = ((ToolStripItem)sender);

			switch (theSender.Tag.ToString())
			{
				case "0;insertnorm":
					selectedLayer.insertNewKeyFrameAt(selectedFrame_Ind); break;
				case "0;insertpose":
					//Insert Keyframe with Current Pose goes here.
					break;

				case "0;remove":
					selectedFrameset.RemoveKeyFrameAt(selectedFrame_Ind); break;
				case "0;insertset":
					selectedLayer.insertNewFramesetAt(selectedFrame_Ind);
					break;

				case "0;removeset":
					selectedLayer.removeFramesetAt(selectedFrame_Ind); break;
				case "0;moveall":
					//Moving all framesets
					break;

				case "0;poseprevious":
					//Set to previous pose
					break;

				case "0;posenext":
					//Set to next pose
					break;

				case "01;deletelayer":
					Layers.Remove(selectedLayer);
					break;

				case "01;rename":
					//Layer renaming
					break;

				case "01;moveup":
					//Move layer up
					break;

				case "01;movedown":
					//Move layer down
					break;

				case "013;onion":
					//Onion skinning
					break;

				case "3;goto":
					//goto frame
					break;
			}
		}

		public static void setFrame(int position = -1)
		{
			if (position == -1)
				position = selectedFrame_Ind;

			foreach (Layer l in Layers)
				l.updateFigure(position);
		}

		public static void doRender()
		{
			Program.TheCanvas.GLGraphics.MakeCurrent();

			GL.Clear(ClearBufferMask.ColorBufferBit);

			foreach (Layer l in Layers)
				l.renderLayer(Program.TheCanvas);
			if (selectedLayer_Ind != -1 && selectedLayer_Ind < Layers.Count)
			{
				Layers[selectedLayer_Ind].LayerFigure.drawFigHandles(Program.TheCanvas);
			}
			Program.TheCanvas.GLGraphics.SwapBuffers();
		}
	}
}