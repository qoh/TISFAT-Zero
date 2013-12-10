using OpenTK.Graphics;
using OpenTK.Platform;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using Config = OpenTK.Configuration;
using Utilities = OpenTK.Platform.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection;

namespace TISFAT_Zero
{
	partial class Timeline : Form, ICanDraw
	{
		public MainF MainForm;
		public static List<Layer> Layers = new List<Layer>();
		private static Color[] Colors;

		private bool GLLoaded = false;

		private double Scrollbar_eX = 0.0, Scrollbar_eY = 0.0;
		public int pxOffsetX, pxOffsetY;
		int scrollAreaY, scrollAreaX, maxFrames, maxLayers;

		//We need a lot of rectangles...
		private Rectangle ScrollX = new Rectangle(), ScrollY = new Rectangle();
		private Rectangle StubX = new Rectangle(), StubY = new Rectangle();

		public int cursorDiff;

		public bool isScrolling, isScrollingY;
		public bool mouseDown;

		//On the suggestion of Valcle, I'm using bit masks here so we don't flood this area with bools.
		//bits 1-4: scrollbars
		//1st bit:		is anything selected
		//2nd bit:		mouse over or mouse down (0,1)
		//3rd-4th bit:	which is selected (3rd: 0:x, 1:y 4th: 0:top/left, 1:bottom/right)
		//repeated for bits 5-8 for scrolling stubs
		public byte selectedScrollItems = 0;

		public int timelineFrameLength = 1024;
		public int timelineRealLength;
		public int timelineHeight = 16 * 16;

		//We also use a lot of bitmaps.. ^ ^'
		private T0Bitmap[] zerotonine = new T0Bitmap[10];
		private static List<T0Bitmap> layerNames = new List<T0Bitmap>();
		private T0Bitmap[] stubs = new T0Bitmap[12];
		private T0Bitmap TIMELINE;

		public int currentnum = -1;

		public GLControl GLGraphics
		{
			get { return glgraphics; }
		}

		public Timeline(MainF f)
		{
			InitializeComponent();
			LoadGraphics();
			
			MainForm = f;
			Colors = new Color[] { Color.FromArgb(220, 220, 220), Color.FromArgb(140, 140, 140), Color.FromArgb(0, 0, 0), Color.FromArgb(70, 120, 255), Color.FromArgb(40, 230, 255), Color.FromArgb(30, 100, 255) };

			zerotonine[0] = new T0Bitmap(Properties.Resources._0); zerotonine[1] = new T0Bitmap(Properties.Resources._1);
			zerotonine[2] = new T0Bitmap(Properties.Resources._2); zerotonine[3] = new T0Bitmap(Properties.Resources._3);
			zerotonine[4] = new T0Bitmap(Properties.Resources._4); zerotonine[5] = new T0Bitmap(Properties.Resources._5);
			zerotonine[6] = new T0Bitmap(Properties.Resources._6); zerotonine[7] = new T0Bitmap(Properties.Resources._7);
			zerotonine[8] = new T0Bitmap(Properties.Resources._8); zerotonine[9] = new T0Bitmap(Properties.Resources._9);

			Point y = new Point(7, -1);
			Font F = new Font("Arial", 10);
			
			using (Bitmap raw = new Bitmap(78, 16))
			using (Graphics g = Graphics.FromImage(raw))
			{
				g.Clear(Colors[5]);
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				g.DrawString("TIMELINE", F, Brushes.Black, y);

				TIMELINE = new T0Bitmap(raw, new Point(1, 0));
			}

			//Fetch the scrollbar stub bitmaps
			stubs[0] = new T0Bitmap(Properties.Resources.stub_x_l); stubs[1] = new T0Bitmap(Properties.Resources.stub_x_r);
			stubs[2] = new T0Bitmap(Properties.Resources.stub_y_t); stubs[3] = new T0Bitmap(Properties.Resources.stub_y_b);
			stubs[4] = new T0Bitmap(Properties.Resources.stub_x_l_a); stubs[5] = new T0Bitmap(Properties.Resources.stub_x_r_a);
			stubs[6] = new T0Bitmap(Properties.Resources.stub_y_t_a); stubs[7] = new T0Bitmap(Properties.Resources.stub_y_b_a);
			stubs[8]  = new T0Bitmap(Properties.Resources.stub_x_l_c); stubs[9]  = new T0Bitmap(Properties.Resources.stub_x_r_c);
			stubs[10] = new T0Bitmap(Properties.Resources.stub_y_t_c); stubs[11] = new T0Bitmap(Properties.Resources.stub_y_b_c);

			ScrollY.Width = 8;
			ScrollX.Height = 8;

			StubX.Height = 12;
			StubY.Width = 12;

			addNewLayer(typeof(StickLayer));
			addNewLayer(typeof(BitmapLayer), "sdfasdggae");
			timelineRealLength = 9 * timelineFrameLength;
		}

		public void Timeline_Resize(object sender, EventArgs e)
		{
			if (!GLLoaded)
				return;

			GL.Viewport(0, Screen.PrimaryScreen.Bounds.Height - Height, Width, Height);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();

			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(Color.White);

			GL.Ortho(0, Width, Height, 0, 0, 1);

			//Hax to make sure that everything renders where it should
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.Translate(0.375, 0, 0);

			int width = timelineFrameLength * 9;

			scrollAreaY = Height - 49; scrollAreaX = Width - 113;

			float viewRatioY = (float)scrollAreaY / timelineHeight, viewRatioX = (float)scrollAreaX / width;
			
			ScrollY.Height = viewRatioY < 1 ? Math.Max((int)(scrollAreaY * viewRatioY), 16) : -1;
			ScrollY.Location = ScrollY.Height != -1 ? new Point(Width - 10, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 26)) : new Point(-1, -1);

			ScrollX.Width = viewRatioX < 1 ? Math.Max((int)(scrollAreaX * viewRatioX), 16) : -1;
			ScrollX.Location = ScrollX.Width != -1 ? new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 90), Height - 10) : new Point(-1, -1);

			pxOffsetX = (int)(Scrollbar_eX * width);
			pxOffsetY = (int)(Scrollbar_eY * timelineHeight);

			StubX.Width = Width - 91;
			StubX.Location = new Point(79, Height - 12);

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
			glgraphics.MakeCurrent();

            GL.Disable(EnableCap.Multisample);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.Color3(Color.Black);
			GL.Begin(PrimitiveType.LineStrip);

			GL.Vertex2(0, StubX.Top);
			GL.Vertex2(0, -1);
			GL.Vertex2(79, -1);
			GL.Vertex2(79, StubX.Top);

			GL.End();

			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(0, Height - 12);
			GL.Vertex2(79, Height - 12);
			GL.Vertex2(79, Height);
			GL.Vertex2(0, Height);
			GL.End();

			int ind_L = (int)Math.Ceiling((pxOffsetY + 1) / 16d - 1), ind_F = (int)Math.Ceiling((pxOffsetX + 1) / 9d - 1);
			int start_L = 32 - (pxOffsetY % 16), start_F = 80 - (pxOffsetX % 9);

			if (ind_L == -1)
				ind_L = 0;
			if (ind_F == -1)
				ind_F = 0;

			int endL_ind = Math.Min(Layers.Count, ind_L + maxLayers), endF_ind = ind_F + maxFrames;

			for (int p = start_F, a = ind_F; a < endF_ind; p += 9, a++)
			{
				Point x = new Point(p, 0);

				Color c = Color.Empty;
				if ((a+1) % 100 == 0)
					c = Color.Pink;
				else if ((a+1) % 10 == 0)
					c = Color.FromArgb(40, 230, 255);

				if (c != Color.Empty)
				{
					x.X++;
					GL.Color3(c);
					GL.Begin(PrimitiveType.Quads);
					GL.Vertex2(x.X, 0);
					GL.Vertex2(x.X, Height);
					GL.Vertex2(x.X + 8, Height);
					GL.Vertex2(x.X + 8, 0);
					GL.End();
					x.X--;
				}

				drawFrame(x, Color.Transparent);

				x.X++;

				zerotonine[(a+1) % 10].Draw(this, x);
			}

			for (int p = start_L, a = ind_L; p < StubX.Top && a < endL_ind; p += 16, a++)
			{
				GL.Color4(Color.Black);
				GL.Begin(PrimitiveType.Lines);

				GL.Vertex2(79, p);
				GL.Vertex2(0, p);

				GL.End();

				layerNames[a].Draw(this, new Point(1, p - 15));
			}

			TIMELINE.Draw(this);

			GL.Color4(Color.Black);
			GL.Begin(PrimitiveType.Lines);

			GL.Vertex2(1, 16);
			GL.Vertex2(Width - 12, 16);
			GL.Vertex2(1, 15);
			GL.Vertex2(79, 15);

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

			for(int a = 0; a < 4; a++)
				stubs[a].Draw(this);

			stub >>= 4;

			//This formula is used to determine which stub to draw, since I ordered them so neatly in the array this is possible.
			if (isBitSet(stub, 0))
				stubs[4 + ((stub & 2) << 1) + ((stub & 4) >> 1) + ((stub & 8) >> 3)].Draw(this);
			
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
			if(name == null)
				name = "Layer " + (Layers.Count + 1);

			Layer newLayer = (Layer)layerType.GetConstructor(new Type[] { typeof(string), typeof(int) } ).Invoke(new object[] { name, 0 } );

			//Add the layer
			Layers.Add(newLayer);

			//Construct the name (will do later because lazy)
			
			Point y = new Point(0, -1);
			Font F = new Font("Arial", 10);

			using (Bitmap raw = new Bitmap(78, 15))
			using (Graphics g = Graphics.FromImage(raw))
			{
				g.Clear(Colors[3]);
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				g.DrawString(name, F, Brushes.Black, y);

				layerNames.Add(new T0Bitmap(raw));
			}

			return newLayer;
		}

		public void drawGraphics(int type, Color color, Point one, int width, int height, Point two)
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			if (type == 0) //Line
			{
                //renderQuadLine(one, two, width, color);
			}
			else if (type == 1 || type == 2) //Rectangle
			{
				GL.Color4(color);
				if (type == 1)
					GL.Begin(PrimitiveType.Quads);
				else
					GL.Begin(PrimitiveType.LineLoop);

				if (width != 0 && height != 0)
				{
					GL.Vertex2(one.X, one.Y);
					GL.Vertex2(one.X + width, one.Y);
					GL.Vertex2(one.X + width, one.Y + height);
					GL.Vertex2(one.X, one.Y + height);
				}
				else
				{
					GL.Vertex2(one.X, one.Y);
					GL.Vertex2(two.X, one.Y);
					GL.Vertex2(two.X, two.Y);
					GL.Vertex2(one.X, two.Y);
				}

				GL.End();
			}

			GL.Disable(EnableCap.Blend);
		}

       /* Not needed for the timeline (at the moment)
	    * private void renderQuadLine(Point one, Point two, float thickness, Color color)
        {
            GL.Color4(color);

            float dist = (float)Math.Sqrt(Math.Pow((two.X - one.X), 2) + Math.Pow((two.Y - one.Y), 2));

            GL.Begin(PrimitiveType.Quads);

            float normX = ((one.Y - two.Y) / dist) * thickness / 2;
            float normY = -((one.X - two.X) / dist) * thickness / 2;

            GL.Vertex2((one.X - normX), (one.Y - normY));
            GL.Vertex2((one.X + normX), (one.Y + normY));

            GL.Vertex2((two.X + normX), (two.Y + normY));
            GL.Vertex2((two.X - normX), (two.Y - normY));

            GL.End();
        } */ 

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

		private void drawFrame(Point p, Color i)
		{
			GL.Color3(Color.Black);
			GL.Begin(PrimitiveType.LineStrip);

			int a = p.Y + 16, b = p.X + 9;
			GL.Vertex2(p.X, a - 1);
			GL.Vertex2(b, a);
			GL.Vertex2(b, p.Y);
			GL.End();

			if (i != Color.Transparent)
			{
				GL.Color3(i);
				GL.Begin(PrimitiveType.Quads);

				GL.Vertex2(p.X, p.Y);
				GL.Vertex2(p.X, --a);
				GL.Vertex2(--b, a);
				GL.Vertex2(b, p.Y);

				GL.End();
			}
		}

		private void drawFrame(int x, int y, Color i)
		{
			drawFrame(new Point(x, y), i);
		}

		private void Timeline_MouseMove(object sender, MouseEventArgs e)
		{
			#region Scrollbar Checks

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
                selectedScrollItems &= 0xfd;
			}

			//Only start checking the massive monolithic conditional tree if dx or dy is <= 12. This is because all the scrollers
			//are 12 pixels out from their respective sides. It also checks if the first bit of selectedScrollItems is 0,
			//this is so that it doesn't update the selected scroll items while the mouse is clicked down.
			if ((dx <= 12 || dy <= 12) && !isBitSet(selectedScrollItems, 1))
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
						else if(dy >= 24)
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
							if (mouseDown)
							{
								Scrollbar_eY = Math.Min(1, (pxOffsetY + 1) / (double)timelineHeight);
								updateScrollLocation = true;
							}
						}
					}
					else
					{
						selectedScrollItems |= 80;

						if (mouseDown)
						{
							Scrollbar_eY = Math.Max(0, (pxOffsetY - 1) / (double)timelineHeight);
							updateScrollLocation = true;
						}
					}
				}
				else
				{
					if (dx <= 23)
					{
						selectedScrollItems |= (byte)(dy < 12 ? 144 : 0);
						if (mouseDown && selectedScrollItems != old)
						{
							Scrollbar_eX = Math.Min(1, 12 * ((pxOffsetX / 12) + 1) / (double)timelineRealLength);
							updateScrollLocation = true;
						}
					}
					else if (x >= 79)
					{
						if (x >= 90)
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

							if (mouseDown)
							{
								Scrollbar_eX = Math.Max(0, 12 * ((pxOffsetX / 12) - 1) / (double)timelineRealLength);
								updateScrollLocation = true;
							}
						}
					}
				}
			}
			else if(!isBitSet(selectedScrollItems, 1))
				selectedScrollItems = 0;

			if(mouseDown)
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
			}

			if (updateScrollLocation)
			{
				ScrollY.Location = new Point(Width - 10, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 26));
				ScrollX.Location = new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 90), Height - 10);
				pxOffsetX = (int)(Scrollbar_eX * timelineRealLength);
				pxOffsetY = (int)(Scrollbar_eY * timelineHeight);
			}

			#endregion Scrollbar Checks

			//Only refresh if the state has changed
			if(old != selectedScrollItems || updateScrollLocation)
				Timeline_Refresh();
		}

		private void Timeline_MouseDown(object sender, MouseEventArgs e)
		{
			mouseDown = true;
			Timeline_MouseMove(sender, e);
		}

		public void Timeline_MouseUp(object sender, MouseEventArgs e)
		{
			mouseDown = false;
			Timeline_MouseMove(sender, e);
		}

		private bool isBitSet(byte x, byte n)
		{
			return (x & (1 << n)) >> n == 1;
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
				ScrollX.Location = new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 90), Height - 10);
			}
			else
			{
				int old = pxOffsetY;
				pxOffsetY = Math.Max(0, Math.Min(pxOffsetY - e.Delta, timelineHeight));

				if (old == pxOffsetY)
					return;

				Scrollbar_eY = Math.Min(1, Math.Max(0, pxOffsetY / (double)timelineHeight));
				ScrollY.Location = new Point(Width - 10, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 26));
			}

			Timeline_Refresh();
		}
	}
}