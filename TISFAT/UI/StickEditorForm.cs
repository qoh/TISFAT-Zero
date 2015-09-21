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
		public StickFigure CreatedFigure { get; set; }
		public StickFigure.State CreatedFigureState { get; set; }

		GLControl GLContext;
		static int MSAASamples = 8;

		Point MouseLoc;

		StickFigure ActiveFigure;
		StickFigure.State ActiveFigureState;

		StickFigure.Joint.State _SelectedObject;
		StickFigure.Joint.State SelectedObject
		{
			get
			{
				return _SelectedObject;
			}
			set
			{
				_SelectedObject = value;
				UpdateSelection();
			}
		}
		StickFigure.Joint.State ActiveDragObject;
		IManipulatableParams ActiveDragParams;

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

		public bool IKEnabled { get { return ckb_EnableIK.Checked; } }
		public bool DrawHandles { get { return ckb_DrawHandles.Checked; } }

		public StickEditorForm()
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

			pnl_GLArea.Controls.Add(GLContext);
		}

		private void StickEditorForm_Load(object sender, EventArgs e)
		{
			GLContext_Init();

			ActiveFigure = new StickFigure();
			SelectedObject = null;

			int ID = 0;
			
			ActiveFigure.Root = new StickFigure.Joint();
			ActiveFigure.Root.Location = new PointF(250, 250);
			ActiveFigure.Root.ID = 0;
			var next = StickFigure.Joint.RelativeTo(ActiveFigure.Root, new PointF(0, 50), ref ID);

			ActiveFigureState = (StickFigure.State)ActiveFigure.CreateRefState();

			UpdateSelection();
        }

		public void ProjectOpen(string filename)
		{
			ActiveFigure = new StickFigure();

			using (var reader = new BinaryReader(new FileStream(filename, FileMode.Open)))
			{
				UInt16 version = reader.ReadUInt16();
				ActiveFigure.Read(reader, version);
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
			}
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

		private void GLContext_MouseMove(object sender, MouseEventArgs e)
		{
			MouseLoc = e.Location;
			lbl_MousePosition.Text = String.Format("Mouse Position: {0}, {1}", e.X, e.Y);

			Cursor = ActiveFigure.TryManipulate(
				ActiveFigureState, e.Location, e.Button, ModifierKeys) != null ? Cursors.Hand : Cursors.Default;

			if(ActiveManipMode == EditorManipMode.Move && ActiveDragObject != null)
			{
				ActiveFigure.ManipulateUpdate(ActiveDragObject, ActiveDragParams, e.Location);
				ActiveDragObject.GetEquivalentJoint(ActiveFigure.Root, ActiveDragObject.ID).Location = ActiveDragObject.Location;
				GLContext.Invalidate();
			} 
			else if(ActiveManipMode == EditorManipMode.Add && SelectedObject != null)
			{
				GLContext.Invalidate();
			}
		}

		private void GLContext_MouseDown(object sender, MouseEventArgs e)
		{
			if (ActiveManipMode == EditorManipMode.Pointer || ActiveManipMode == EditorManipMode.Move)
			{
				ManipulateResult result = ActiveFigure.TryManipulate(
					ActiveFigureState, e.Location, e.Button, ModifierKeys, !IKEnabled);

				if (result != null)
				{
					if (e.Button != MouseButtons.Right)
						SelectedObject = (StickFigure.Joint.State)result.Target;

					if (ActiveManipMode == EditorManipMode.Move)
					{
						ActiveDragObject = SelectedObject;
						ActiveDragParams = result.Params;
					}
				}
			}
			else if (ActiveManipMode == EditorManipMode.Add && SelectedObject != null)
			{
				int ID = ActiveFigure.GetJointCount();
				int ID2 = ID;

				StickFigure.Joint.RelativeTo(ActiveFigureState.Root.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID), new PointF(-(SelectedObject.Location.X - e.X), -(SelectedObject.Location.Y - e.Y)), ref ID);
				SelectedObject = StickFigure.Joint.State.RelativeTo(SelectedObject, new PointF(-(SelectedObject.Location.X - e.X), -(SelectedObject.Location.Y - e.Y)), ref ID2);
			}
			else if (ActiveManipMode == EditorManipMode.Delete)
			{
				ManipulateResult result = ActiveFigure.TryManipulate(
					ActiveFigureState, e.Location, e.Button, ModifierKeys, !IKEnabled);

				if (result != null)
				{
					StickFigure.Joint.State state = ((StickFigure.Joint.State)result.Target);
					if (state.Parent == null) // No deleting everything allowed
						return;

                    ActiveFigureState.Root.GetEquivalentJoint(ActiveFigure.Root, state.ID).Delete();
					state.Delete();

					SelectedObject = null;
				}
			}

			GLContext.Invalidate();
		}

		private void GLContext_MouseUp(object sender, MouseEventArgs e)
		{
			ActiveDragObject = null;
		}

		public void GLContext_Paint(object sender, PaintEventArgs e)
		{
			GLContext.MakeCurrent();

			GL.ClearColor(Color.FromArgb(81, 81, 145));

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit);

			ActiveFigure.Draw(ActiveFigureState);

			if (SelectedObject != null)
			{
				if (ActiveManipMode == EditorManipMode.Add)
				{
					GL.Enable(EnableCap.StencilTest);
					GL.StencilMask(0xFFFFFF);
					GL.StencilFunc(StencilFunction.Equal, 0, 0xFFFFFF);
					GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);

					Drawing.CappedLine(SelectedObject.Location, MouseLoc, SelectedObject.Thickness, Color.FromArgb(127, 0, 0, 0));

					GL.Disable(EnableCap.StencilTest);
				}

				Drawing.RectangleLine(new PointF(SelectedObject.Location.X - 4, SelectedObject.Location.Y - 4), new SizeF(8, 8), Color.White);
			}

			if(DrawHandles)
				ActiveFigure.DrawEditable(ActiveFigureState);

			GLContext.SwapBuffers();
		}

		private void UpdateSelection()
		{
			if(SelectedObject == null)
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
			}
			else
			{
				grp_handleProperties.Enabled = true;
				grp_jointProperties.Enabled = SelectedObject.Parent != null;
				grp_jointBitmaps.Enabled = false;

				Color c1 = SelectedObject.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID).HandleColor;
				Color c2 = SelectedObject.JointColor;

				ckb_handleVisible.Checked = SelectedObject.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID).Visible;

				pnl_handleColorImg.BackColor = c1;
				pnl_jointColorImg.BackColor = c2;

				lbl_handleColorNumbers.Text = string.Format("{0}, {1}, {2}", c1.R, c1.G, c1.B);
				lbl_jointColorNumbers.Text = string.Format("{0}, {1}, {2}", c2.R, c2.G, c2.B);

				cmb_drawMode.SelectedIndex = (int)SelectedObject.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID).DrawType;

				num_jointThickness.Value = (decimal)SelectedObject.Thickness;
			}
		}

		private void btn_editModePointer_Click(object sender, EventArgs e)
		{
			ActiveManipMode = EditorManipMode.Pointer;
		}

		private void btn_editModeMove_Click(object sender, EventArgs e)
		{
			ActiveManipMode = EditorManipMode.Move;
		}

		private void btn_editModeAdd_Click(object sender, EventArgs e)
		{
			ActiveManipMode = EditorManipMode.Add;
		}

		private void btn_editModeDelete_Click(object sender, EventArgs e)
		{
			ActiveManipMode = EditorManipMode.Delete;
		}

		private void StickEditorForm_Resize(object sender, EventArgs e)
		{
			GLContext_Init();
		}

		private void ckb_DrawHandles_CheckedChanged(object sender, EventArgs e)
		{
			GLContext.Invalidate();
		}

		private void pnl_handleColorImg_Click(object sender, EventArgs e)
		{
			if (SelectedObject == null)
				return;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if(dlg.ShowDialog() == DialogResult.OK)
			{
				SelectedObject.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID).HandleColor = dlg.Color;
				UpdateSelection();
			}

			GLContext.Invalidate();
		}

		private void pnl_jointColorImg_Click(object sender, EventArgs e)
		{
			if (SelectedObject == null)
				return;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				SelectedObject.JointColor = dlg.Color;
				SelectedObject.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID).JointColor = dlg.Color;

				UpdateSelection();
			}

			GLContext.Invalidate();
		}

		private void ckb_handleVisible_VisibleChanged(object sender, EventArgs e)
		{
			if (SelectedObject == null)
				return;

			SelectedObject.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID).Visible = ckb_handleVisible.Checked;
			UpdateSelection();

			GLContext.Invalidate();
		}

		private void num_jointThickness_ValueChanged(object sender, EventArgs e)
		{
			if (SelectedObject == null)
				return;

			SelectedObject.Thickness = (float)num_jointThickness.Value;
			SelectedObject.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID).Thickness = (float)num_jointThickness.Value;
			UpdateSelection();

			GLContext.Invalidate();
		}

		private void cmb_drawMode_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (SelectedObject == null)
				return;

			DrawJointType type = (DrawJointType)Enum.Parse(typeof(DrawJointType), cmb_drawMode.SelectedIndex.ToString());
			SelectedObject.GetEquivalentJoint(ActiveFigure.Root, SelectedObject.ID).DrawType = type;

			GLContext.Invalidate();
		}

		private void btn_OK_Click(object sender, EventArgs e)
		{
			CreatedFigure = ActiveFigure;
			CreatedFigureState = ActiveFigureState;

			Close();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StickEditorForm_Load(null, null);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.AddExtension = true;
			dialog.Filter = "TISFAT Zero Figure|*.tzf";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				ProjectOpen(dialog.FileName);
			}
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.AddExtension = true;
			dialog.Filter = "TISFAT Zero Figure|*.tzf";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				ProjectSave(dialog.FileName);
			}
		}
	}
}
