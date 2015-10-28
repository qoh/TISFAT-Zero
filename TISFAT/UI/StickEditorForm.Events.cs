using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TISFAT.Entities;

namespace TISFAT
{
	public partial class StickEditorForm
	{
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
			if (SelectedPair == null)
				return;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				SelectedPair.Item1.HandleColor = dlg.Color;
				UpdateSelection();
			}

			GLContext.Invalidate();
		}

		private void pnl_jointColorImg_Click(object sender, EventArgs e)
		{
			if (SelectedPair == null)
				return;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				SelectedPair.Item1.JointColor = dlg.Color;
				SelectedPair.Item2.JointColor = dlg.Color;

				UpdateSelection();
			}

			GLContext.Invalidate();
		}

		private void ckb_handleVisible_VisibleChanged(object sender, EventArgs e)
		{
			if (SelectedPair == null)
				return;

			SelectedPair.Item1.Visible = ckb_handleVisible.Checked;
			UpdateSelection();

			GLContext.Invalidate();
		}

		private void num_jointThickness_ValueChanged(object sender, EventArgs e)
		{
			if (SelectedPair == null)
				return;

			SelectedPair.Item1.Thickness = (float)num_jointThickness.Value;
			SelectedPair.Item2.Thickness = (float)num_jointThickness.Value;
			UpdateSelection();

			GLContext.Invalidate();
		}

		private void cmb_drawMode_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (SelectedPair == null)
				return;

			DrawJointType type = (DrawJointType)Enum.Parse(typeof(DrawJointType), cmb_drawMode.SelectedIndex.ToString());
			SelectedPair.Item1.DrawType = type;

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

			dialog.Dispose();
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

		private void btn_bitmapAdd_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();

			dlg.Title = "Open an Image";
			dlg.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				AddBitmap(SelectedPair.Item1, dlg.SafeFileName, (Bitmap)Image.FromFile(dlg.FileName));

				SelectedPair.Item1.InitialBitmapIndex++;
				SelectedPair.Item2.BitmapIndex++;

				UpdateSelection();
				GLContext.Invalidate();
			}
		}

		private void btn_bitmapRemove_Click(object sender, EventArgs e)
		{
			StickFigure.Joint joint = SelectedPair.Item1;
			int ind = cmb_bitmaps.SelectedIndex - 1;

			Bitmap tmp = joint.Bitmaps[SelectedPair.Item2.BitmapIndex].Item2.Item2;
			joint.Bitmaps.RemoveAt(ind);
			joint.BitmapOffsets.Remove(tmp);
			joint.InitialBitmapIndex = -1;
			SelectedPair.Item2.BitmapIndex = -1;

			cmb_bitmaps.Items.RemoveAt(cmb_bitmaps.SelectedIndex);

			UpdateSelection();
			GLContext.Invalidate();
		}

		private void num_bitmapRotation_ValueChanged(object sender, EventArgs e)
		{
			if (SelectedPair == null)
				return;

			StickFigure.Joint joint = SelectedPair.Item1;
			Bitmap bitmap = joint.Bitmaps[SelectedPair.Item2.BitmapIndex].Item2.Item2;

			tkb_bitmapRotation.Value = (int)num_bitmapRotation.Value;

			joint.BitmapOffsets[bitmap] = new Tuple<float, PointF>((float)num_bitmapRotation.Value, joint.BitmapOffsets[bitmap].Item2);
			GLContext.Invalidate();
		}

		private void tkb_bitmapRotation_ValueChanged(object sender, EventArgs e)
		{
			num_bitmapRotation.Value = (decimal)tkb_bitmapRotation.Value;
		}

		private void num_bitmapXOffset_ValueChanged(object sender, EventArgs e)
		{
			if (SelectedPair == null)
				return;

			StickFigure.Joint joint = SelectedPair.Item1;
			Bitmap bitmap = joint.Bitmaps[SelectedPair.Item2.BitmapIndex].Item2.Item2;

			joint.BitmapOffsets[bitmap] = new Tuple<float, PointF>(joint.BitmapOffsets[bitmap].Item1, new PointF((float)num_bitmapXOffset.Value, joint.BitmapOffsets[bitmap].Item2.Y));

			GLContext.Invalidate();
		}

		private void num_bitmapYOffset_ValueChanged(object sender, EventArgs e)
		{
			if (SelectedPair == null)
				return;

			StickFigure.Joint joint = SelectedPair.Item1;
			Bitmap bitmap = joint.Bitmaps[SelectedPair.Item2.BitmapIndex].Item2.Item2;

			joint.BitmapOffsets[bitmap] = new Tuple<float, PointF>(joint.BitmapOffsets[bitmap].Item1, new PointF(joint.BitmapOffsets[bitmap].Item2.X, (float)num_bitmapYOffset.Value));

			GLContext.Invalidate();
		}

		private void cmb_bitmaps_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedPair == null)
				return;

			StickFigure.Joint joint = SelectedPair.Item1;

			SelectedPair.Item2.BitmapIndex = cmb_bitmaps.SelectedIndex - 1;
			joint.InitialBitmapIndex = cmb_bitmaps.SelectedIndex - 1;

			btn_bitmapRemove.Enabled = cmb_bitmaps.SelectedIndex > 0;

			GLContext.Invalidate();
		}

		private void btn_saveBitmap_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();

			dlg.Filter = "PNG Files|*.png";
			dlg.FileName = SelectedPair.Item1.Bitmaps[SelectedPair.Item2.BitmapIndex].Item2.Item1;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				Bitmap bitmap = SelectedPair.Item1.Bitmaps[SelectedPair.Item2.BitmapIndex].Item2.Item2;

				bitmap.Save(dlg.FileName);
			}

			dlg.Dispose();
		}
	}
}
