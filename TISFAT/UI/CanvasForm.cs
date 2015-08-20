using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Windows.Media.Imaging;
using System.Windows;

namespace TISFAT
{
    public partial class CanvasForm : Form
	{
		GLControl GLContext;
		bool Loaded;
		static int MSAASamples = 8;

		private IManipulatable ActiveDragObject;
		private IManipulatableParams ActiveDragParams;

		public bool VSync
		{
			get { return GLContext.VSync; }
			set { GLContext.VSync = value; }
		}

		public CanvasForm(Control parent)
		{
			InitializeComponent();
			Loaded = false;

			GraphicsMode mode = new GraphicsMode(
				new ColorFormat(8, 8, 8, 8),
				8, 8, MSAASamples,
				new ColorFormat(8, 8, 8, 8), 2, false
			);
			GLContext = new GLControl(mode, 2, 0, GraphicsContextFlags.Default);
			GLContext.Dock = DockStyle.Fill;
			GLContext.VSync = true;
			GLContext.Paint += new PaintEventHandler(this.GLContext_Paint);
			GLContext.MouseDown += new MouseEventHandler(this.GLContext_MouseDown);
			GLContext.MouseMove += new MouseEventHandler(this.GLContext_MouseMove);
			GLContext.MouseUp += new MouseEventHandler(this.GLContext_MouseUp);
			Controls.Add(GLContext);

			// Setup stuff
			TopLevel = false;
			parent.Controls.Add(this);
		}

		private void Canvas_Load(object sender, EventArgs e)
		{
			GLContext_Init();
			Loaded = true;
		}

		#region GL Core Init
		private void GLContext_Init()
		{
			GLContext.MakeCurrent();

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Viewport(0, 0, GLContext.Width, GLContext.Height);
			GL.Ortho(0, GLContext.Width, GLContext.Height, 0, -1, 1);
			GL.Disable(EnableCap.DepthTest);
		}

		private void CanvasForm_Resize(object sender, EventArgs e)
		{
			if (Loaded)
				GLContext_Init();
		} 
		#endregion

		public void GLContext_Paint(object sender, PaintEventArgs e)
		{
			DrawFrame(Program.Form.MainTimeline.GetCurrentFrame(), false);
		}

		public void DrawFrame(float time, bool render)
		{
			GLContext.MakeCurrent();

			GL.ClearColor(Color.White);

			GL.Clear(ClearBufferMask.ColorBufferBit);
			
			Program.Form.ActiveProject.Draw(time);

			GLContext.SwapBuffers();
		}

		public void GLContext_MouseDown(object sender, MouseEventArgs e)
		{
			ActiveDragObject = null;
			Timeline timeline = Program.Form.MainTimeline;

			if (timeline.SelectedKeyframe == null)
				return;
			
			ManipulateResult result = timeline.SelectedLayer.Data.TryManipulate(
				timeline.SelectedKeyframe.State, e.Location, e.Button, ModifierKeys);

			if (result != null)
			{
				ActiveDragObject = result.Target;
				ActiveDragParams = result.Params;
			}
		}

		public void GLContext_MouseMove(object sender, MouseEventArgs e)
		{
			Timeline timeline = Program.Form.MainTimeline;

			if (timeline.SelectedKeyframe == null)
				return;

			if (ActiveDragObject == null)
				Cursor = timeline.SelectedLayer.Data.TryManipulate(timeline.SelectedKeyframe.State, e.Location, e.Button, ModifierKeys) != null ? Cursors.Hand : Cursors.Default;
			else
			{
				Cursor = Cursors.Hand;
				timeline.SelectedLayer.Data.ManipulateUpdate(ActiveDragObject, ActiveDragParams, e.Location);
				GLContext.Invalidate();
			}
		}

		public void GLContext_MouseUp(object sender, MouseEventArgs e)
		{
			ActiveDragObject = null;
		}

		private void CanvasForm_Enter(object sender, EventArgs e)
		{
			BringToFront();
		}

		public IntPtr TakeScreenshot()
		{
			if (GraphicsContext.CurrentContext == null)
				throw new GraphicsContextMissingException();

			Bitmap bmp = new Bitmap(GLContext.ClientSize.Width, GLContext.ClientSize.Height);
			System.Drawing.Imaging.BitmapData data = bmp.LockBits(GLContext.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			GL.ReadPixels(0, 0, GLContext.ClientSize.Width, GLContext.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
			bmp.UnlockBits(data);

			bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

			return bmp.GetHbitmap();
		}
	}
}
