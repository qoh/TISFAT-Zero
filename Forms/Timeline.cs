using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace TISFAT_Zero
{
	partial class Timeline : Form
	{
		public MainF MainForm;
		public static List<Layer> Layers = new List<Layer>();
		private Color[] Colors;
		private Point[] Points = new Point[] { new Point(79, 0), new Point(79, 15), new Point(0, 15) };

		public double Scrollbar_eX, Scrollbar_eY;
		public int Scrollbar_lX, Scrollbar_lY;
		public int cursorStart;
		public bool isScrolling, isScrollingY;

		public Timeline(MainF f)
		{
			InitializeComponent();
			this.GLGraphics = new OpenTK.GLControl(new OpenTK.Graphics.GraphicsMode(32, 0, 1, 1), 3, 0, OpenTK.Graphics.GraphicsContextFlags.Default);
			MainForm = f;
			Colors = new Color[] { Color.FromArgb(220, 220, 220), Color.FromArgb(140, 140, 140), Color.FromArgb(0,0,0), Color.FromArgb(70, 120, 255), Color.FromArgb(40, 230, 255) };
			Layers.Add(new StickLayer("Layer 1"));
		}

		public void Timeline_Resize(object sender, EventArgs e)
		{
			GLGraphics.Width = Width;
			GLGraphics.Height = Height;
			GLGraphics.MakeCurrent();
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Viewport(0, 0, GLGraphics.Width, GLGraphics.Height);
			GL.Ortho(0, GLGraphics.Width, 0, GLGraphics.Height, -1, 1);
			GL.ClearColor(Color.Black);
			GL.Disable(EnableCap.DepthTest);
		}

		private void Timeline_Paint(object sender, PaintEventArgs e)
		{
			//GLGraphics.MakeCurrent();
			Timeline_Refresh();
		}

		public void Timeline_Refresh()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(Colors[2]);
			
			//Determine the number of frames (x) we need to draw
			int lengthX = (int)Math.Ceiling((double)(Width - 80) / 9);
			//Determine the number of layers (y) we need to draw
			int lengthY = (int)Math.Ceiling((double)(Height - 16) / 16);

			//Get the index to start drawing from for both x and y
			GLGraphics.SwapBuffers();
		}
	}
}
