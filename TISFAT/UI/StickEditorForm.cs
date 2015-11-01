using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TISFAT.Entities;
using TISFAT.Util;

namespace TISFAT
{
	public enum EditorManipMode
	{
		Pointer, Move, Add, Delete
	}

	public partial class StickEditorForm : Form
	{
		#region Properties
		public StickFigure CreatedFigure { get; set; }
		public StickFigure.State CreatedFigureState { get; set; }

		GLControl GLContext;
		static int MSAASamples = 8;

		Point MouseLoc;

		StickFigure ActiveFigure;
		StickFigure.State ActiveFigureState;

		Tuple<StickFigure.Joint, StickFigure.Joint.State> _selectedState;
		Tuple<StickFigure.Joint, StickFigure.Joint.State> SelectedPair
		{
			get
			{
				return _selectedState;
			}
			set
			{
				_selectedState = value;
				UpdateSelection();
			}
		}

		Tuple<StickFigure.Joint, StickFigure.Joint.State> ActiveDragPair;
		IManipulatableParams ActiveDragParams;

		public bool FromProperties;

		private EditorManipMode _ActiveManipMode;
		public EditorManipMode ActiveManipMode
		{
			get { return _ActiveManipMode; }
			set
			{
				btn_editModePointer.Checked = value == EditorManipMode.Pointer;
				btn_editModeMove.Checked = value == EditorManipMode.Move;
				btn_editModeAdd.Checked = value == EditorManipMode.Add;
				btn_editModeDelete.Checked = value == EditorManipMode.Delete;

				_ActiveManipMode = value;
				GLContext.Invalidate();
			}
		}

		public bool IKEnabled => ckb_EnableIK.Checked; 
		public bool DrawHandles => ckb_DrawHandles.Checked; 
		#endregion

		public StickEditorForm()
		{
			InitContext();
		}

		public StickEditorForm(StickFigure Figure, StickFigure.State State)
		{
			InitContext();

			FromProperties = true;

			ActiveFigure = Figure;
			ActiveFigureState = State;
		}

		private void StickEditorForm_Load(object sender, EventArgs e)
		{
			GLContext_Init();

			if (!FromProperties)
			{
				ActiveFigure = new StickFigure();
				SelectedPair = null;

				ActiveFigure.Root = new StickFigure.Joint();
				ActiveFigure.Root.Location = new PointF(250, 300);
				var next = StickFigure.Joint.RelativeTo(ActiveFigure.Root, new PointF(0, -50));

				ActiveFigureState = (StickFigure.State)ActiveFigure.CreateRefState();
			}

			UpdateSelection();
		}

		public void InitContext()
		{
			InitializeComponent();

			GraphicsMode mode = new GraphicsMode(
				new ColorFormat(8, 8, 8, 8),
				8, 8, MSAASamples,
				new ColorFormat(8, 8, 8, 8), 2, false
			);
			GLContext = new GLControl(mode, 2, 0, GraphicsContextFlags.Default);
			GLContext.Dock = DockStyle.Fill;
			GLContext.VSync = true;

			GLContext.Paint += GLContext_Paint;
			GLContext.MouseMove += GLContext_MouseMove;
			GLContext.MouseDown += GLContext_MouseDown;
			GLContext.MouseUp += GLContext_MouseUp;
			GLContext.KeyDown += GLContext_KeyDown;

			pnl_GLArea.Controls.Add(GLContext);
		}

		public void GLContext_Init()
		{
			GLContext.MakeCurrent();

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Viewport(0, 0, GLContext.Width, GLContext.Height);
			GL.Ortho(0, GLContext.Width, GLContext.Height, 0, -1, 1);
			GL.Disable(EnableCap.DepthTest);
		}

		public void ProjectOpen(string filename)
		{
			ActiveFigure = new StickFigure();

			using (var reader = new BinaryReader(new FileStream(filename, FileMode.Open)))
			{
				UInt16 version = reader.ReadUInt16();
				ActiveFigure.Read(reader, version);
				reader.Close();
			}

			ActiveFigureState = (StickFigure.State)ActiveFigure.CreateRefState();

			UpdateSelection();

			GLContext.Invalidate();
		}

		public void ProjectSave(string filename)
		{
			using (var writer = new BinaryWriter(new FileStream(filename, FileMode.Create)))
			{
				writer.Write(FileFormat.Version);
				ActiveFigure.Write(writer);
				writer.Close();
			}
		}

		private void GLContext_MouseDown(object sender, MouseEventArgs e)
		{
			if (ActiveManipMode == EditorManipMode.Pointer || ActiveManipMode == EditorManipMode.Move)
			{
				var pair = StickFigure.FindJointStatePair(ActiveFigure.Root, ActiveFigureState.Root, e.Location);
				var result = ActiveFigure.TryManipulate(ActiveFigureState, e.Location, e.Button, ModifierKeys, !IKEnabled);

				if (result != null && pair != null)
				{
					if (e.Button != MouseButtons.Right)
						SelectedPair = pair;

					if (ActiveManipMode == EditorManipMode.Move)
					{
						ActiveDragPair = pair;
						ActiveDragParams = result.Params;
					}
				}
			}
			else if (ActiveManipMode == EditorManipMode.Add && SelectedPair != null)
			{
				StickFigure.Joint joint = StickFigure.Joint.RelativeTo(SelectedPair.Item1, new PointF(-(SelectedPair.Item2.Location.X - e.X), -(SelectedPair.Item2.Location.Y - e.Y)));
				StickFigure.Joint.State state = StickFigure.Joint.State.RelativeTo(SelectedPair.Item2, new PointF(-(SelectedPair.Item2.Location.X - e.X), -(SelectedPair.Item2.Location.Y - e.Y)));

				SelectedPair = new Tuple<StickFigure.Joint, StickFigure.Joint.State>(joint, state);
			}
			else if (ActiveManipMode == EditorManipMode.Delete)
			{
				var pair = StickFigure.FindJointStatePair(ActiveFigure.Root, ActiveFigureState.Root, e.Location);
				var result = ActiveFigure.TryManipulate(ActiveFigureState, e.Location, e.Button, ModifierKeys, !IKEnabled);

				if (result != null)
				{
					StickFigure.Joint.State state = ((StickFigure.Joint.State)result.Target);
					if (state.Parent == null) // No deleting everything allowed
						return;

                    pair.Item1.Delete();
					pair.Item2.Delete();

					SelectedPair = null;
				}
			}

			GLContext.Invalidate();
		}

		private void GLContext_MouseMove(object sender, MouseEventArgs e)
		{
			MouseLoc = e.Location;
			lbl_MousePosition.Text = String.Format("Mouse Position: {0}, {1}", e.X, e.Y);

			Cursor = ActiveFigure.TryManipulate(
				ActiveFigureState, e.Location, e.Button, ModifierKeys) != null ? Cursors.Hand : Cursors.Default;

			if (ActiveManipMode == EditorManipMode.Move && ActiveDragPair != null)
			{
				ActiveFigure.ManipulateUpdate(ActiveDragPair.Item2, ActiveDragParams, e.Location);
				ActiveDragPair.Item1.ResetFrom(ActiveDragPair.Item2);

				GLContext.Invalidate();
			}
			else if (ActiveManipMode == EditorManipMode.Add && SelectedPair != null)
			{
				GLContext.Invalidate();
			}
		}

		private void GLContext_MouseUp(object sender, MouseEventArgs e)
		{
			ActiveDragPair = null;
		}

		private void GLContext_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D1)
				ActiveManipMode = EditorManipMode.Pointer;
			else if (e.KeyCode == Keys.D2)
				ActiveManipMode = EditorManipMode.Move;
			else if (e.KeyCode == Keys.D3)
				ActiveManipMode = EditorManipMode.Add;
			else if (e.KeyCode == Keys.D4)
				ActiveManipMode = EditorManipMode.Delete;
			else if (e.KeyCode == Keys.M && SelectedPair != null && SelectedPair.Item2.Parent != null)
			{
				StickFigure.Joint current = SelectedPair.Item1;

				foreach (StickFigure.Joint.State state in SelectedPair.Item2.Children)
				{
					state.Parent = SelectedPair.Item2.Parent;
					SelectedPair.Item2.Parent.Children.Add(state);
				}

				foreach (StickFigure.Joint joint in current.Children)
				{
					joint.Parent = current.Parent;
					current.Parent.Children.Add(joint);
				}

				SelectedPair.Item2.Parent.Children.Remove(SelectedPair.Item2);
				current.Parent.Children.Remove(current);

				SelectedPair = new Tuple<StickFigure.Joint, StickFigure.Joint.State>(current, SelectedPair.Item2);
				GLContext.Invalidate();
			}
			else if (e.KeyCode == Keys.S && SelectedPair != null && SelectedPair.Item2.Parent != null)
			{
				StickFigure.Joint SelectedJoint = SelectedPair.Item1;

				float x = (SelectedPair.Item2.Location.X + SelectedPair.Item2.Parent.Location.X) / 2;
				float y = (SelectedPair.Item2.Location.Y + SelectedPair.Item2.Parent.Location.Y) / 2;

				int ID = ActiveFigure.GetJointCount();

				/*
				StickFigure.Joint.RelativeTo(ActiveFigureState.Root.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID), new PointF(-(SelectedObject.Location.X - e.X), -(SelectedObject.Location.Y - e.Y)), ref ID);
				SelectedObject = StickFigure.Joint.State.RelativeTo(SelectedObject, new PointF(-(SelectedObject.Location.X - e.X), -(SelectedObject.Location.Y - e.Y)), ref ID2);
				*/

				StickFigure.Joint newJoint = new StickFigure.Joint(SelectedJoint.Parent);
				newJoint.Location = new PointF(x, y);
				newJoint.HandleColor = SelectedJoint.Parent.HandleColor;
				newJoint.JointColor = SelectedJoint.Parent.JointColor;
				newJoint.Thickness = SelectedJoint.Parent.Thickness;
				newJoint.Children.Add(SelectedJoint);
				SelectedJoint.Parent.Children.Add(newJoint);

				StickFigure.Joint.State newState = new StickFigure.Joint.State(SelectedPair.Item2.Parent);
				newState.JointColor = SelectedPair.Item2.Parent.JointColor;
				newState.Thickness = SelectedPair.Item2.Parent.Thickness;
				newState.Location = new PointF(x, y);
				newState.Children.Add(SelectedPair.Item2);
				SelectedPair.Item2.Parent.Children.Add(newState);

				SelectedPair.Item2.Parent.Children.Remove(SelectedPair.Item2);
				SelectedPair.Item2.Parent = newState;

				SelectedJoint.Parent.Children.Remove(SelectedJoint);
				SelectedJoint.Parent = newJoint;

				GLContext.Invalidate();
			}
		}

		public void GLContext_Paint(object sender, PaintEventArgs e)
		{
			GLContext.MakeCurrent();

			GL.ClearColor(Color.FromArgb(81, 81, 145));

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit);

			ActiveFigure.Draw(ActiveFigureState);

			if (SelectedPair != null)
			{
				if (ActiveManipMode == EditorManipMode.Add)
				{
					GL.Enable(EnableCap.StencilTest);
					GL.StencilMask(0xFFFFFF);
					GL.StencilFunc(StencilFunction.Equal, 0, 0xFFFFFF);
					GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);

					Drawing.CappedLine(SelectedPair.Item2.Location, MouseLoc, SelectedPair.Item2.Thickness, Color.FromArgb(127, 0, 0, 0));

					GL.Disable(EnableCap.StencilTest);
				}

				Drawing.RectangleLine(new PointF(SelectedPair.Item2.Location.X - 4, SelectedPair.Item2.Location.Y - 4), new SizeF(8, 8), 1, Color.White);
			}

			if(DrawHandles)
				ActiveFigure.DrawEditable(ActiveFigureState);

			GLContext.SwapBuffers();
		}

		private void UpdateSelection()
		{
			if(SelectedPair == null)
			{
				grp_handleProperties.Enabled = false;
				grp_jointProperties.Enabled = false;
				grp_jointBitmaps.Enabled = false;

				ckb_handleVisible.Visible = false;

				pnl_handleColorImg.BackColor = Color.Black;
				pnl_jointColorImg.BackColor = Color.Black;

				lbl_handleColorNumbers.Text = "0, 0, 0";
				lbl_jointColorNumbers.Text = "0, 0, 0";

				cmb_drawMode.SelectedText = "";

				num_jointThickness.Value = 0;

				cmb_bitmaps.Items.Clear();
				num_bitmapXOffset.Value = 0;
				num_bitmapYOffset.Value = 0;
				num_bitmapRotation.Value = 0;
				tkb_bitmapRotation.Value = 0;
			}
			else
			{
				grp_handleProperties.Enabled = true;
				grp_jointProperties.Enabled = SelectedPair.Item2.Parent != null;
				grp_jointBitmaps.Enabled = SelectedPair.Item2.Parent != null;

				bool hasBitmap = SelectedPair.Item2.BitmapIndex != -1;
				btn_bitmapRemove.Enabled = hasBitmap;
				num_bitmapRotation.Enabled = hasBitmap;
				num_bitmapXOffset.Enabled = hasBitmap;
				num_bitmapYOffset.Enabled = hasBitmap;
				tkb_bitmapRotation.Enabled = hasBitmap;

				Color c1 = SelectedPair.Item1.HandleColor;
				Color c2 = SelectedPair.Item2.JointColor;

				ckb_handleVisible.Checked = SelectedPair.Item1.Visible;

				pnl_handleColorImg.BackColor = c1;
				pnl_jointColorImg.BackColor = c2;

				lbl_handleColorNumbers.Text = string.Format("{0}, {1}, {2}", c1.R, c1.G, c1.B);
				lbl_jointColorNumbers.Text = string.Format("{0}, {1}, {2}", c2.R, c2.G, c2.B);

				cmb_drawMode.SelectedIndex = (int)SelectedPair.Item1.DrawType;

				num_jointThickness.Value = (decimal)SelectedPair.Item2.Thickness;

				StickFigure.Joint Joint = SelectedPair.Item1;

				cmb_bitmaps.Items.Clear();

				cmb_bitmaps.Items.Add("(none)");

				for (int i = 0; i < Joint.Bitmaps.Count; i++)
					cmb_bitmaps.Items.Add(Joint.Bitmaps[i].Item2.Item1);

				cmb_bitmaps.SelectedIndex = SelectedPair.Item2.BitmapIndex + 1;

				if(SelectedPair.Item2.BitmapIndex != -1)
				{
					Bitmap bitmap = Joint.Bitmaps[SelectedPair.Item2.BitmapIndex].Item2.Item2;

					num_bitmapXOffset.Value = (decimal)Joint.BitmapOffsets[bitmap].Item2.X;
					num_bitmapYOffset.Value = (decimal)Joint.BitmapOffsets[bitmap].Item2.Y;
					num_bitmapRotation.Value = (decimal)Joint.BitmapOffsets[bitmap].Item1;
				}
			}
		}

		public void AddBitmap(StickFigure.Joint joint, string name, Bitmap image)
		{
			joint.Bitmaps.Add(
				new Tuple<int, Tuple<string, Bitmap>>(
					Drawing.GenerateTexID(image),
					new Tuple<string, Bitmap>(name, image)
					));

			joint.BitmapOffsets.Add(image, new Tuple<float, PointF>(0, new PointF(0, 0)));
		}
	}
}
