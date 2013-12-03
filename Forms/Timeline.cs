using OpenTK.Graphics.OpenGL;
using OpenTK;
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
		private Color[] Colors;
		private Point[] Points = new Point[] { new Point(79, 0), new Point(79, 15), new Point(0, 15) };

		private bool GLLoaded = false;

		private double Scrollbar_eX = 0.0, Scrollbar_eY = 0.0;
		public int pxOffsetX = 0, pxOffsetY = 0;

		//We need a lot of rectangles...
		private Rectangle ScrollX = new Rectangle(), ScrollY = new Rectangle();
		private Rectangle StubX = new Rectangle(), StubY = new Rectangle();

		public int cursorDiff;

		//We also use a lot of bools >.>
		public bool isScrolling, isScrollingY;
		public bool mouseDown;

		//On the suggestion of Valcle, I'm using bit masks here so we don't flood this area with bools.
		//bits 1-4: scrollbars
		//1st bit:		is anything selected
		//2nd bit:		mouse over or mouse down (0,1)
		//3rd-4th bit:	which is selected (3rd: 0:x, 1:y 4th: 0:top/left, 1:bottom/right)
		//repeated for bits 5-8 for scrolling stubs
		public byte selectedScrollItems = 0;

		public int timelineFrameLength = 150;
		public int timelineRealLength;
		public int timelineHeight = 16 * 16;

		//We also use a lot of bitmaps.. ^ ^'
		private T0Bitmap[] zerotonine = new T0Bitmap[10];
		private List<T0Bitmap> layerNames = new List<T0Bitmap>();
		private T0Bitmap[] stubs = new T0Bitmap[12];

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
			Colors = new Color[] { Color.FromArgb(220, 220, 220), Color.FromArgb(140, 140, 140), Color.FromArgb(0, 0, 0), Color.FromArgb(70, 120, 255), Color.FromArgb(40, 230, 255) };

			//Render the 0-9 text lablels for use in rendering the timeline
			Point y = new Point(-2, -1);
			Font F = new Font("Arial", 12);
			
			for (int a = 0; a < 10; a++)
			{
				using(Bitmap raw = new Bitmap(9, 16))
				using(Graphics g = Graphics.FromImage(raw))
				{
					g.Clear(Color.Empty);
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
					g.TextContrast = 10;
					g.DrawString("" + a, F, Brushes.Black, y);

					zerotonine[a] = new T0Bitmap(raw);
				}
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
			timelineRealLength = 9 * timelineFrameLength;
		}

		public void Timeline_Resize(object sender, EventArgs e)
		{
			if (!GLLoaded)
				return;

			GL.MatrixMode(MatrixMode.Projection);

			GL.LoadIdentity();

			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(Colors[0]);
			GL.Viewport(0, Screen.PrimaryScreen.Bounds.Height - Height, Width, Height);
			GL.Ortho(0, Width, Height, 0, 0, 1);

			int width = timelineFrameLength * 9;

			int scrollAreaY = Height - 28, scrollAreaX = Width - 100;
			int containerX = scrollAreaX + 10;

			float viewRatioY = (float)scrollAreaY / timelineHeight, viewRatioX = (float)containerX / width;
			
			ScrollY.Height = viewRatioY < 1 ? Math.Max((int)(scrollAreaY * viewRatioY), 16) : -1;
			ScrollY.Location = ScrollY.Height != -1 ? new Point(Width - 10, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 16)) : new Point(-1, -1);

			ScrollX.Width = viewRatioX < 1 ? Math.Max((int)(scrollAreaX * viewRatioX), 16) : -1;
			ScrollX.Location = ScrollX.Width != -1 ? new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 80), Height - 10) : new Point(-1, -1);

			pxOffsetX = (int)(Scrollbar_eX * width);
			pxOffsetY = (int)(Scrollbar_eY * timelineHeight);

			StubX.Width = Width - 80;
			StubX.Location = new Point(70, Height - 12);

			StubY.Height = Height - 6;
			StubY.Location = new Point(Width - 12, 6);

			stubs[0].texPos = new Point(StubX.Left + 2, StubX.Top + 1);
			stubs[1].texPos = new Point(StubX.Right - 8, StubX.Top + 1);
			stubs[2].texPos = new Point(StubY.Left + 1, StubY.Top + 2);
			stubs[3].texPos = new Point(StubY.Left + 1, StubY.Bottom - 9);
			for(int a = 4; a < 12; a++)
				stubs[a].texPos = stubs[a % 4].texPos;

			this.Invalidate();
		}

		private void Timeline_Paint(object sender, PaintEventArgs e)
		{
			Timeline_Refresh();
		}

		public void Timeline_Refresh()
		{
			//if(!GLLoaded)
			//	return;
			
			//glgraphics.MakeCurrent();
			GL.Clear(ClearBufferMask.ColorBufferBit);

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
				stubs[4 + 2 * (stub & 2) + ((stub & 4) >> 1) + ((stub & 8) >> 3)].Draw(this);

			currentnum = (currentnum + 1) % 10;
			zerotonine[currentnum].Draw(this);

			glgraphics.SwapBuffers();
		}

		public void LoadGraphics()
		{
			glgraphics.Width = Screen.PrimaryScreen.Bounds.Width;
			glgraphics.Height = Screen.PrimaryScreen.Bounds.Height;

			glgraphics = new OpenTK.GLControl(new OpenTK.Graphics.GraphicsMode(32, 0, 1, 4), 3, 0, OpenTK.Graphics.GraphicsContextFlags.Default);
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

			return newLayer;
		}

		public void drawGraphics(int type, Color color, Point one, int width, int height, Point two)
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			if (type == 0) //Line
			{
				GL.Color4(color);
				GL.Begin(BeginMode.Lines);

				GL.Vertex2(one.X, one.Y);
				GL.Vertex2(two.X, two.Y);

				GL.End();
			}
			else if (type == 1 || type == 2) //Rectangle
			{
				GL.Color4(color);
				if (type == 1)
					GL.Begin(BeginMode.Quads);
				else
					GL.Begin(BeginMode.LineLoop);

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

		private void renderRectangle(Rectangle rect, Color color)
		{
			GL.Color4(color);
			GL.Begin(BeginMode.Quads);

			int x1 = rect.Left, x2 = rect.Right;
			int y1 = rect.Top, y2 = rect.Bottom;
			GL.Vertex2(x1, y1); GL.Vertex2(x1, y2);
			GL.Vertex2(x2, y2); GL.Vertex2(x2, y1);

			GL.End();
		}

		private void Timeline_MouseMove(object sender, MouseEventArgs e)
		{
			byte old = selectedScrollItems;
			Point old1 = ScrollY.Location, old2 = ScrollX.Location;
			bool updateScrollLocation = false;
			int x = e.X, y = e.Y;
			int dx = Width - x, dy = Height - y;

			int scrollAreaY = Height - 28, scrollAreaX = Width - 100;

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
				selectedScrollItems = (byte)(!mouseDown ? 0 : 2 | 1 << 5);
				if (dx <= 12)
				{
					if (y <= 6)
						selectedScrollItems = 0;
					else if (y >= 15)
					{
						if (dy >= 12)
						{
							Rectangle ScrollerAreaY = new Rectangle(new Point(ScrollY.X - 2, ScrollY.Y), new Size(ScrollY.Width + 4, ScrollY.Height));
							selectedScrollItems |= (byte)(ScrollerAreaY.Contains(e.Location) ? 1 : 0);
						}
						else
						{
							selectedScrollItems |= 1 << 4 | 1 << 6 | 1 << 7;
							if (mouseDown)
							{
								Scrollbar_eY = Math.Min(1, 16 * ((pxOffsetY / 16) + 1) / (double)timelineHeight);
								updateScrollLocation = true;
							}
						}
					}
					else
					{
						selectedScrollItems |= 1 << 4 | 1 << 6;
						if (mouseDown)
						{
							Scrollbar_eY = Math.Max(0, 16 * ((pxOffsetY / 16) - 1) / (double)timelineHeight);
							updateScrollLocation = true;
						}
					}
				}
				else
				{
					if (dx <= 22)
						selectedScrollItems |= (byte)(dy < 12 ? 1 << 4 | 1 << 7 : 0);
					else if (x >= 70)
					{
						if (x >= 80)
						{
							Rectangle ScrollerAreaX = new Rectangle(new Point(ScrollX.X, ScrollX.Y - 2), new Size(ScrollX.Width, ScrollX.Height + 4));
							selectedScrollItems |= (byte)(ScrollerAreaX.Contains(e.Location) ?  1 | 1 << 3: 0);
						}
						else
							selectedScrollItems |= 1 << 4;
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
						Scrollbar_eY = Math.Min(1, Math.Max(0, (double)((y - 16) - cursorDiff) / (scrollAreaY - ScrollY.Height)));
					else
						Scrollbar_eX = Math.Min(1, Math.Max(0, (double)((x - 80) - cursorDiff) / (scrollAreaX - ScrollX.Width)));
					updateScrollLocation = true;
				}
			}

			if (updateScrollLocation)
			{
				ScrollY.Location = new Point(Width - 10, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 16));
				ScrollX.Location = new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 80), Height - 10);
				pxOffsetX = (int)(Scrollbar_eX * timelineRealLength);
				pxOffsetY = (int)(Scrollbar_eY * timelineHeight);
			}

			//Only refresh if the state has changed
			if(old != selectedScrollItems || ScrollY.Location != old1 || ScrollX.Location != old2)
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
			int scrollAreaY = Height - 28, scrollAreaX = Width - 100;

			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift && ScrollX.Location.X != -1)
			{
				int old = pxOffsetX;
				pxOffsetX = Math.Max(0, Math.Min(pxOffsetX - e.Delta, timelineRealLength));

				if (old == pxOffsetX)
					return;

				Scrollbar_eX = Math.Min(1, Math.Max(0, pxOffsetX / (double)timelineRealLength));
				ScrollX.Location = new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 80), Height - 10);
			}
			else
			{
				int old = pxOffsetY;
				pxOffsetY = Math.Max(0, Math.Min(pxOffsetY - e.Delta, timelineHeight));

				if (old == pxOffsetY)
					return;

				Scrollbar_eY = Math.Min(1, Math.Max(0, pxOffsetY / (double)timelineHeight));
				ScrollY.Location = new Point(Width - 10, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 16));
			}

			Timeline_Refresh();
		}
	}
}