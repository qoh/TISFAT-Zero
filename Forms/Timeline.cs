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

		public double Scrollbar_eX, Scrollbar_eY = 0.3;
		public int Scrollbar_lX, Scrollbar_lY;
		public int cursorStart;
		public bool isScrolling, isScrollingY;

		public int timelineLength = 4096; //The number of frames that the timeline can hold, this number can be anything really but it defaults to this number.

		private T0Bitmap[] zerotonine = new T0Bitmap[10];
		private List<T0Bitmap> layerNames = new List<T0Bitmap>();

		public Rectangle workingArea; //For use in calculations for the scrollbar positioning and size

		public int currentnum = 0;

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
			Point y = new Point(-1, 1);
			for (int a = 0; a < 10; a++)
			{
				using(Bitmap raw = new Bitmap(9, 16))
				using(Graphics g = Graphics.FromImage(raw))
				{
					g.Clear(Color.Empty);
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
					g.DrawString("" + a, new Font(this.Font.FontFamily, 10), Brushes.Black, y);

					zerotonine[a] = new T0Bitmap(raw);
				}
			}

			Layers.Add(new StickLayer("Layer 1"));
		}

		public void Timeline_Resize(object sender, EventArgs e)
		{
			if (!GLLoaded)
				return;

			GL.MatrixMode(MatrixMode.Projection);

			GL.LoadIdentity();

			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(Color.White);
			GL.Viewport(0, Screen.PrimaryScreen.Bounds.Height - Height, Width, Height);
			GL.Ortho(0, Width, Height, 0, 0, 1);

			int height = 16 * 16;
			int width = timelineLength * 9;

			int scrollbarHeight = Height - 30;

			float viewRatioY = (float)(Height - 31) / height, viewRatioX = (float)(Width - 95) / width;

			int offsetY = 0, offsetX = 0;

			Scrollbar_lY = viewRatioY < 1 ? Math.Max((int)(scrollbarHeight * viewRatioY), 16) : -1;

			this.Invalidate();
		}

		private void Timeline_Paint(object sender, PaintEventArgs e)
		{
			Timeline_Refresh();
		}

		public void Timeline_Refresh()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			int scrollbarHeight = Height - 30;

			//float viewRatioY = (float)(Height - 31) / height, viewRatioX = (float)(Width - 95) / width;

			//int offsetY = 0, offsetX = 0;

			//if (viewRatioY < 1)
			//{
			//	Scrollbar_lY = Math.Max((int)(scrollbarHeight * viewRatioY), 16);
			//	GL.Begin(BeginMode.Quads);
			//	GL.Color3(0, 0, 0);
			//	offsetY = (int)((height * Scrollbar_eY * (scrollbarHeight - Scrollbar_lY)) / scrollbarHeight);
			//}

			int drawStartY = (int)((scrollbarHeight - Scrollbar_lY) * Scrollbar_eY);

			GL.Begin(BeginMode.Quads);
			GL.Vertex2(Width - 11, 15 + drawStartY);
			GL.Vertex2(Width - 11, 15 + drawStartY + Scrollbar_lY);
			GL.Vertex2(Width - 3, 15 + drawStartY + Scrollbar_lY);
			GL.Vertex2(Width - 3, 15 + drawStartY);
			GL.End();

			//Determine the number of frames (x) we need to draw
			int lengthX = (int)Math.Ceiling((double)(Width - 80) / 9);
			//Determine the number of layers (y) we need to draw
			int lengthY = (int)Math.Ceiling((double)(Height - 16) / 16);

			//Get the index to start drawing from for both x and y
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

			//Construct the name 

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
			else if (type == 1) //Rectangle
			{
				GL.Color4(color);
				GL.Begin(BeginMode.Quads);

				GL.Vertex2(one.X, one.Y);
				GL.Vertex2(one.X + width, one.Y);
				GL.Vertex2(one.X + width, one.Y + height);
				GL.Vertex2(one.X, one.Y + height);

				GL.End();
			}
			else if (type == 2) //Hollow Rectangle
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
		}
	}
}