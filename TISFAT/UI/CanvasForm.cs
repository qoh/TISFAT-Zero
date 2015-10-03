using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;
using TISFAT.Entities;
using TISFAT.Util;

namespace TISFAT
{
	public partial class CanvasForm : Form
	{
		#region Properties
		Panel ContextContainer;
		GLControl GLContext;
		static int MSAASamples = 8;

		public Tuple<StickFigure.Joint, StickFigure.Joint.State> StickFigurePair;

		private IManipulatable ActiveDragObject;
		public IManipulatable LastDragObject;
		private IManipulatableParams ActiveDragParams;
		private IEntityState ActiveDragPrevState;

		public int GLWidth { get { return GLContext.Width; } }
		public int GLHeight { get { return GLContext.Height; } }

		public bool VSync
		{
			get { return GLContext.VSync; }
			set { GLContext.VSync = value; }
		}
		#endregion

		public CanvasForm(Control parent)
		{
			InitializeComponent();

			ContextContainer = new Panel();
			ContextContainer.Padding = new Padding(1);
			ContextContainer.BackColor = Color.Black;

			Console.WriteLine(ContextContainer.Anchor);

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

			ContextContainer.Controls.Add(GLContext);
			Controls.Add(ContextContainer);

			// Setup stuff
			TopLevel = false;
			parent.Controls.Add(this);
		}

		private void Canvas_Load(object sender, EventArgs e)
		{
			GLContext_Init();
		}

		#region GL Core Init
		public void GLContext_Init()
		{
			GLContext.MakeCurrent();

			ContextContainer.Size = new System.Drawing.Size(Program.ActiveProject.Width + 1, Program.ActiveProject.Height + 1);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Viewport(0, 0, GLContext.Width, GLContext.Height);
			GL.Ortho(0, GLContext.Width, GLContext.Height, 0, -1, 1);
			GL.Disable(EnableCap.DepthTest);
		}

		public void CanvasForm_Resize(object sender, EventArgs e)
		{
			if (Width < ContextContainer.Width)
			{
				ContextContainer.Anchor |= AnchorStyles.Left;
				ContextContainer.Location = new System.Drawing.Point(0, ContextContainer.Location.Y);
			}
			else
				ContextContainer.Anchor &= ~AnchorStyles.Left;

			if (Height < ContextContainer.Height)
			{
				ContextContainer.Anchor |= AnchorStyles.Top;
				ContextContainer.Location = new System.Drawing.Point(ContextContainer.Location.X, 0);
			}
			else
				ContextContainer.Anchor &= ~AnchorStyles.Top;
		}
		#endregion

		public void GLContext_Paint(object sender, PaintEventArgs e)
		{
			DrawFrame(Program.MainTimeline.GetCurrentFrame(), false, sender != null);
		}

		public void DrawFrame(float time, bool render, bool lights)
		{
			if (lights)
				GLContext.MakeCurrent();

			GL.PushMatrix();
			
			GL.ClearColor(Program.ActiveProject.BackColor);

			GL.Clear(ClearBufferMask.ColorBufferBit);

			if (Program.Form_Main.PreviewCamera || render)
			{
				Camera.State state = (Camera.State)Program.ActiveProject.Layers[0].FindCurrentState(time);

				if (state == null)
				{
					GLContext.SwapBuffers();
					return;
				}

				GL.Translate(-state.Location.X * 1 / state.Scale, -state.Location.Y * 1 / state.Scale, 0.0f);
				GL.Scale(1 / state.Scale, 1 / state.Scale, 1.0);
				GL.Rotate(state.Angle, 1.0, 0.0, 0.0);
			}

			Program.ActiveProject.Draw(time, render, lights);

			GL.PopMatrix();

			GLContext.SwapBuffers();
		}
		
		PointF UnprojectMousePos(PointF location)
		{
			Camera.State state = (Camera.State)Program.ActiveProject.Layers[0].FindCurrentState(Program.MainTimeline.SelectedKeyframe.Time);

			PointF translate = Program.Form_Main.PreviewCamera ? state.Location : new PointF(0, 0);
			float scale = Program.Form_Main.PreviewCamera ? state.Scale : 1;
			float angle = Program.Form_Main.PreviewCamera ? state.Angle : 0;

			return MathUtil.TranslatePoint(MathUtil.ScalePoint(MathUtil.Rotate(location, angle), scale), translate);
		}
		
		Point RoundMousePos(PointF location)
		{
			return new Point((int)location.X, (int)location.Y);
		}

		public void GLContext_MouseDown(object sender, MouseEventArgs e)
		{
			ActiveDragObject = null;
			Timeline timeline = Program.MainTimeline;

			if (timeline.SelectedLayer == null)
				return;
			if (timeline.SelectedKeyframe == null)
				return;

			Point location = RoundMousePos(UnprojectMousePos(e.Location));

			if (timeline.SelectedLayer.Data.GetType() == typeof(StickFigure) || timeline.SelectedLayer.Data.GetType() == typeof(CustomFigure))
				StickFigurePair = StickFigure.FindJointStatePair(((StickFigure)timeline.SelectedLayer.Data).Root, ((StickFigure.State)timeline.SelectedKeyframe.State).Root, location);

			ManipulateResult result = timeline.SelectedLayer.Data.TryManipulate(
				timeline.SelectedKeyframe.State, location, e.Button, ModifierKeys);

			if (result != null)
			{
				ActiveDragObject = result.Target;
				ActiveDragParams = result.Params;
				LastDragObject = result.Target;
				ActiveDragPrevState = timeline.SelectedKeyframe.State.Copy();

				Program.Form_Properties.UpdateStickFigurePanel();
			}
		}

		public void GLContext_MouseMove(object sender, MouseEventArgs e)
		{
			Timeline timeline = Program.MainTimeline;

			if (timeline.SelectedKeyframe == null || timeline.SelectedLayer == null)
				return;


			Point location = RoundMousePos(UnprojectMousePos(e.Location));

			if (ActiveDragObject == null)
				Cursor = timeline.SelectedLayer.Data.TryManipulate(timeline.SelectedKeyframe.State, location, e.Button, ModifierKeys) != null ? Cursors.Hand : Cursors.Default;
			else
			{
				Cursor = Cursors.Hand;
				timeline.SelectedLayer.Data.ManipulateUpdate(ActiveDragObject, ActiveDragParams, location);
				GLContext.Invalidate();
			}
		}

		public void GLContext_MouseUp(object sender, MouseEventArgs e)
		{
			Timeline timeline = Program.MainTimeline;
			// Point location = RoundMousePos(UnprojectMousePos(e.Location));

			if (ActiveDragObject != null)
			{
				Program.Form_Main.Do(new ManipulatableUpdateAction(timeline.SelectedLayer, timeline.SelectedFrameset, timeline.SelectedKeyframe,
				ActiveDragPrevState, timeline.SelectedKeyframe.State));
			}

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

		private void CanvasForm_Scroll(object sender, ScrollEventArgs e)
		{
			GLContext.Invalidate();
		}
	}
}
