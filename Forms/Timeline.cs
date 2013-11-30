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

		public int cursorStart;

		//We also use a lot of bools >.>
		public bool isScrolling, isScrollingY;
		public bool mouseDown;

		//On the suggestion of Valcle, I'm using bit masks here so we don't flood this area with bools.
		//bits 1-4: scrollbars
		//1st bit:		is anything selected
		//2nd bit:		mouse down or mouse over (0,1)
		//3rd-4th bit:	which is selected (3rd: 0:x, 1:y 4th: 0:top/left, 1:bottom/right)
		//repeated for bits 5-8 for scrolling stubs
		private byte selectedScrollItems = 0;

		public int timelineLength = 150;

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
			StubX.Location = new Point(70, 0);

			StubY.Width = 12;
			StubY.Location = new Point(0, 6);

			addNewLayer(typeof(StickLayer));

			selectedScrollItems = 1 << 4 | 1 << 5;
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

			int height = 16 * 16;
			int width = timelineLength * 9;

			int scrollAreaY = Height - 28, scrollAreaX = Width - 100;
			int containerX = scrollAreaX + 10;

			float viewRatioY = (float)scrollAreaY / height, viewRatioX = (float)containerX / width;
			
			ScrollY.Height = viewRatioY < 1 ? Math.Max((int)(scrollAreaY * viewRatioY), 16) : -1;
			ScrollY.Location = ScrollY.Height != -1 ? new Point(Width - 10, (int)((scrollAreaY - ScrollY.Height) * Scrollbar_eY + 16)) : new Point(-1, -1);

			ScrollX.Width = viewRatioX < 1 ? Math.Max((int)(scrollAreaX * viewRatioX), 16) : -1;
			ScrollX.Location = ScrollX.Width != -1 ? new Point((int)((scrollAreaX - ScrollX.Width) * Scrollbar_eX + 80), Height - 10) : new Point(-1, -1);

			pxOffsetX = (int)(Scrollbar_eX * width);
			pxOffsetY = (int)(Scrollbar_eY * height);

			StubX.Width = Width - 80;
			StubX.Location = new Point(70, Height - 12);

			StubY.Height = Height - 6;
			StubY.Location = new Point(Width - 12, 6);

			stubs[0].texPos = new Point(StubX.Left + 2, StubX.Top + 1);
			stubs[1].texPos = new Point(StubX.Right - 10, StubX.Top + 1);
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
			GL.Clear(ClearBufferMask.ColorBufferBit);

			renderRectangle(StubX, Color.DarkGray);
			renderRectangle(StubY, Color.DarkGray);
			renderRectangle(ScrollX, Color.Gray);
			renderRectangle(ScrollY, Color.Gray);

			byte stub = (byte)(selectedScrollItems & 0x0F);

			if (isBitSet(stub, 0))
			{
				Color x = isBitSet(stub, 1) ? Color.WhiteSmoke : Color.LightGray;
				
			}

			stubs[0].Draw(this);
			stubs[1].Draw(this);
			stubs[2].Draw(this);
			stubs[3].Draw(this);

			stub = (byte)((selectedScrollItems & 0xF0) >> 4);

			if (isBitSet(stub, 0))
			{
				//This formula is used to determine which stub to draw, since I ordered them so neatly in the array this is possible.
				int ind = 4 + 2 * (stub & 2) + ((stub & 4) >> 1) + ((stub & 8) >> 3);
				stubs[ind].Draw(this);
			}

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

		}

		private void Timeline_MouseDown(object sender, MouseEventArgs e)
		{

		}

		private void Timeline_MouseUp(object sender, MouseEventArgs e)
		{

		}

		private bool isBitSet(byte x, byte n)
		{
			return (x & (1 << n)) >> n == 1;
		}
	}
}