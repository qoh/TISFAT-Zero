using System;
using System.Drawing;
using System.Windows.Forms;
using TISFAT.Entities;

namespace TISFAT
{
	public partial class PropertiesForm : Form
	{
		public PropertiesForm(Control parent)
		{
			InitializeComponent();

			// Setup stuff
			TopLevel = false;
			parent.Controls.Add(this);
		}

		private void ToolboxForm_Enter(object sender, EventArgs e)
		{
			BringToFront();
		}

		public void Clear()
		{
			pnl_PropertiesDescription.Visible = false;

			pnl_StickFigureProperties.Visible = false;
			pnl_LineProperties.Visible = false;
			pnl_RectangleProperties.Visible = false;
			pnl_CircleProperties.Visible = false;
			pnl_PolyProperties.Visible = false;
			pnl_BitmapProperties.Visible = false;
			pnl_TextProperties.Visible = false;
			pnl_PointLightProperties.Visible = false;
		}

		public void SelectionChanged(SelectionType type)
		{
			Clear();

			Color c;

			if ((type & SelectionType.Keyframe) != 0)
			{
				switch(Program.MainTimeline.SelectedLayer.Data.GetType().ToString().Replace("TISFAT.Entities.", ""))
                {
					case "StickFigure":
						c = ((StickFigure.State)Program.MainTimeline.SelectedKeyframe.State).Root.JointColor;

						pnl_stickFigureColorImg.BackColor = c;
						lbl_stickFigureColorNumbers.Text = string.Format("{0}, {1}, {2}", c.R, c.G, c.B);

						pnl_StickFigureProperties.Visible = true;
						break;
					case "CustomFigure":
						c = ((StickFigure.State)Program.MainTimeline.SelectedKeyframe.State).Root.JointColor;

						pnl_stickFigureColorImg.BackColor = c;
						lbl_stickFigureColorNumbers.Text = string.Format("{0}, {1}, {2}", c.R, c.G, c.B);

						pnl_StickFigureProperties.Visible = true;
						break;
					case "LineObject":
						c = ((LineObject.State)Program.MainTimeline.SelectedKeyframe.State).Color;
						float thickness = ((LineObject.State)Program.MainTimeline.SelectedKeyframe.State).Thickness;

						pnl_lineColorImg.BackColor = c;
						lbl_lineColorNumbers.Text = string.Format("{0}, {1}, {2}", c.R, c.G, c.B);

						num_lineThickness.Value = (decimal)thickness;

						pnl_LineProperties.Visible = true;
						break;
					case "RectObject":
						c = ((RectObject.State)Program.MainTimeline.SelectedKeyframe.State).Color;

						pnl_rectColorImg.BackColor = c;
						lbl_rectColorNumbers.Text = string.Format("{0}, {1}, {2}", c.R, c.G, c.B);

						pnl_RectangleProperties.Visible = true;
						break;
					case "CircleObject":
						c = ((CircleObject.State)Program.MainTimeline.SelectedKeyframe.State).Color;
						float radius = ((CircleObject.State)Program.MainTimeline.SelectedKeyframe.State).Size;

						pnl_circleColorImg.BackColor = c;
						lbl_circleColorNumbers.Text = string.Format("{0}, {1}, {2}", c.R, c.G, c.B);

						num_circleRadius.Value = (decimal)radius;

						pnl_CircleProperties.Visible = true;
						break;
					case "PolyObject":
						c = ((PolyObject.State)Program.MainTimeline.SelectedKeyframe.State).Color;

						pnl_polyColorImg.BackColor = c;
						lbl_polyColorNumbers.Text = string.Format("{0}, {1}, {2}", c.R, c.G, c.B);

						pnl_PolyProperties.Visible = true;
						break;
					case "BitmapObject":
						float rotation = ((BitmapObject.State)Program.MainTimeline.SelectedKeyframe.State).Rotation;
						int alpha = ((BitmapObject.State)Program.MainTimeline.SelectedKeyframe.State).BitmapAlpha;

						num_bitmapAngle.Value = (decimal)rotation;
						tkb_bitmapAngle.Value = (int)rotation;

						num_bitmapAlpha.Value = (decimal)alpha;
						tkb_bitmapAlpha.Value = alpha;

						pnl_BitmapProperties.Visible = true;
						break;
					case "TextObject":
						c = ((TextObject.State)Program.MainTimeline.SelectedKeyframe.State).TextColor;
						Font font = ((TextObject.State)Program.MainTimeline.SelectedKeyframe.State).TextFont;
						string txt = ((TextObject.State)Program.MainTimeline.SelectedKeyframe.State).Text;

						pnl_textColorImg.BackColor = c;
						lbl_textColorNumbers.Text = string.Format("{0}, {1}, {2}", c.R, c.G, c.B);

						rtb_textText.Text = txt;
						rtb_textText.Font = new Font(font.FontFamily, 8.25f, FontStyle.Regular);

						txt_textFont.Text = font.FontFamily.Name + ", " + font.Size + "pt";

						pnl_TextProperties.Visible = true;
						break;
				}
			}
			else
				pnl_PropertiesDescription.Visible = true;
		}

		// And so begins the most repetitive code in this entire project.
		// TODO: Fix this later
		// TODO: Undo / Redo for this stuff

		#region StickFigure Panel
		public void UpdateStickFigurePanel()
		{
			cmb_stickBitmaps.Items.Clear();
			cmb_stickBitmaps.Visible = false;
			lbl_stickBitmap.Visible = false;

			if (Program.Form_Canvas.StickFigurePair == null)
				return;

			StickFigure.Joint.State state = Program.Form_Canvas.StickFigurePair.Item2;

			var Bitmaps = Program.Form_Canvas.StickFigurePair.Item1.Bitmaps;

			if (Bitmaps.Count < 1)
				return;

			lbl_stickBitmap.Visible = true;
			cmb_stickBitmaps.Visible = true;
			cmb_stickBitmaps.Items.Add("(none)");

			for (int i = 0; i < Bitmaps.Count; i++)
				cmb_stickBitmaps.Items.Add(Bitmaps[i].Item2.Item1);

			cmb_stickBitmaps.SelectedIndex = state.BitmapIndex + 1;
		}

		private void pnl_stickFigureColorImg_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(StickFigure.State))
				return;

			StickFigure.State state = Program.MainTimeline.SelectedKeyframe.State as StickFigure.State;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				pnl_stickFigureColorImg.BackColor = dlg.Color;
				lbl_stickFigureColorNumbers.Text = string.Format("{0}, {1}, {2}", dlg.Color.R, dlg.Color.G, dlg.Color.B);
			}

			state.Root.SetColor(dlg.Color, null);

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_stickApplyToLayer_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedLayer == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(StickFigure))
				return;

			StickFigure fig = Program.MainTimeline.SelectedLayer.Data as StickFigure;

			foreach (Frameset set in Program.MainTimeline.SelectedLayer.Framesets)
			{
				foreach (Keyframe frame in set.Keyframes)
				{
					fig.SetColor(frame.State, pnl_stickFigureColorImg.BackColor);
				}
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_stickApplyToFrameset_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedFrameset == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(StickFigure))
				return;

			StickFigure fig = Program.MainTimeline.SelectedLayer.Data as StickFigure;

			foreach (Keyframe frame in Program.MainTimeline.SelectedFrameset.Keyframes)
			{
				fig.SetColor(frame.State, pnl_stickFigureColorImg.BackColor);
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void cmb_stickBitmaps_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedFrameset == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(CustomFigure))
				return;

			StickFigure.State state = Program.MainTimeline.SelectedKeyframe.State as StickFigure.State;
			StickFigure.Joint.State jointState = state.Root.FindState((StickFigure.Joint.State)Program.Form_Canvas.LastDragObject);

			jointState.BitmapIndex = cmb_stickBitmaps.SelectedIndex - 1;

			Program.MainTimeline.GLContext.Invalidate();
		}
		#endregion

		#region LineObject Panel
		private void pnl_lineColorImg_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(LineObject.State))
				return;

			LineObject.State state = Program.MainTimeline.SelectedKeyframe.State as LineObject.State;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				pnl_lineColorImg.BackColor = dlg.Color;
				lbl_lineColorNumbers.Text = string.Format("{0}, {1}, {2}", dlg.Color.R, dlg.Color.G, dlg.Color.B);
			}

			state.Color = dlg.Color;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_lineFrameset_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedFrameset == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(LineObject))
				return;

			foreach (Keyframe frame in Program.MainTimeline.SelectedFrameset.Keyframes)
			{
				LineObject.State state = frame.State as LineObject.State;

				state.Color = pnl_lineColorImg.BackColor;
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_lineLayer_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedLayer == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(LineObject))
				return;

			foreach (Frameset set in Program.MainTimeline.SelectedLayer.Framesets)
			{
				foreach (Keyframe frame in set.Keyframes)
				{
					LineObject.State state = frame.State as LineObject.State;

					state.Color = pnl_lineColorImg.BackColor;
				}
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void num_lineThickness_ValueChanged(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(LineObject.State))
				return;

			LineObject.State state = Program.MainTimeline.SelectedKeyframe.State as LineObject.State;

			state.Thickness = (float)num_lineThickness.Value;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_lineThicknessFrameset_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedFrameset == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(LineObject))
				return;

			foreach (Keyframe frame in Program.MainTimeline.SelectedFrameset.Keyframes)
			{
				LineObject.State state = frame.State as LineObject.State;

				state.Thickness = (float)num_lineThickness.Value;
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_lineThicknessLayer_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedLayer == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(LineObject))
				return;

			foreach (Frameset set in Program.MainTimeline.SelectedLayer.Framesets)
			{
				foreach (Keyframe frame in set.Keyframes)
				{
					LineObject.State state = frame.State as LineObject.State;

					state.Thickness = (float)num_lineThickness.Value;
				}
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		#endregion

		#region RectObject Panel
		private void pnl_rectColorImg_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(RectObject.State))
				return;

			RectObject.State state = Program.MainTimeline.SelectedKeyframe.State as RectObject.State;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				pnl_rectColorImg.BackColor = dlg.Color;
				lbl_rectColorNumbers.Text = string.Format("{0}, {1}, {2}", dlg.Color.R, dlg.Color.G, dlg.Color.B);
			}

			state.Color = dlg.Color;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_rectFrameset_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedFrameset == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(RectObject))
				return;

			foreach (Keyframe frame in Program.MainTimeline.SelectedFrameset.Keyframes)
			{
				RectObject.State state = frame.State as RectObject.State;

				state.Color = pnl_rectColorImg.BackColor;
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_rectLayer_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedLayer == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(RectObject))
				return;

			foreach (Frameset set in Program.MainTimeline.SelectedLayer.Framesets)
			{
				foreach (Keyframe frame in set.Keyframes)
				{
					RectObject.State state = frame.State as RectObject.State;

					state.Color = pnl_rectColorImg.BackColor;
				}
			}

			Program.MainTimeline.GLContext.Invalidate();
		}
		#endregion

		#region CircleObject Panel
		private void pnl_circleColorImg_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(CircleObject.State))
				return;

			CircleObject.State state = Program.MainTimeline.SelectedKeyframe.State as CircleObject.State;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				pnl_circleColorImg.BackColor = dlg.Color;
				lbl_circleColorNumbers.Text = string.Format("{0}, {1}, {2}", dlg.Color.R, dlg.Color.G, dlg.Color.B);
			}

			state.Color = dlg.Color;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_circleFrameset_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedFrameset == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(CircleObject))
				return;

			foreach (Keyframe frame in Program.MainTimeline.SelectedFrameset.Keyframes)
			{
				CircleObject.State state = frame.State as CircleObject.State;

				state.Color = pnl_circleColorImg.BackColor;
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_circleLayer_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedLayer == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(CircleObject))
				return;

			foreach (Frameset set in Program.MainTimeline.SelectedLayer.Framesets)
			{
				foreach (Keyframe frame in set.Keyframes)
				{
					CircleObject.State state = frame.State as CircleObject.State;

					state.Color = pnl_circleColorImg.BackColor;
				}
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void num_circleRadius_ValueChanged(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(CircleObject.State))
				return;

			CircleObject.State state = Program.MainTimeline.SelectedKeyframe.State as CircleObject.State;

			state.Size = (float)num_circleRadius.Value;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_circleRadiusFrameset_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedFrameset == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(CircleObject))
				return;

			foreach (Keyframe frame in Program.MainTimeline.SelectedFrameset.Keyframes)
			{
				CircleObject.State state = frame.State as CircleObject.State;

				state.Size = (float)num_circleRadius.Value;
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_circleRadiusLayer_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedLayer == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(CircleObject))
				return;

			foreach (Frameset set in Program.MainTimeline.SelectedLayer.Framesets)
			{
				foreach (Keyframe frame in set.Keyframes)
				{
					CircleObject.State state = frame.State as CircleObject.State;

					state.Size = (float)num_circleRadius.Value;
				}
			}

			Program.MainTimeline.GLContext.Invalidate();
		}
		#endregion

		#region PolyObject Panel
		private void pnl_polyColorImg_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(PolyObject.State))
				return;

			PolyObject.State state = Program.MainTimeline.SelectedKeyframe.State as PolyObject.State;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				pnl_polyColorImg.BackColor = dlg.Color;
				lbl_polyColorNumbers.Text = string.Format("{0}, {1}, {2}", dlg.Color.R, dlg.Color.G, dlg.Color.B);
			}

			state.Color = dlg.Color;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_polyFrameset_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedFrameset == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(PolyObject))
				return;

			foreach (Keyframe frame in Program.MainTimeline.SelectedFrameset.Keyframes)
			{
				PolyObject.State state = frame.State as PolyObject.State;

				state.Color = pnl_polyColorImg.BackColor;
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_polyLayer_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedLayer == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(PolyObject))
				return;

			foreach (Frameset set in Program.MainTimeline.SelectedLayer.Framesets)
			{
				foreach (Keyframe frame in set.Keyframes)
				{
					PolyObject.State state = frame.State as PolyObject.State;

					state.Color = pnl_polyColorImg.BackColor;
				}
			}

			Program.MainTimeline.GLContext.Invalidate();
		}
		#endregion

		#region TextObject Panel
		private void pnl_textColorImg_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(TextObject.State))
				return;

			TextObject.State state = Program.MainTimeline.SelectedKeyframe.State as TextObject.State;

			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				pnl_textColorImg.BackColor = dlg.Color;
				lbl_textColorNumbers.Text = string.Format("{0}, {1}, {2}", dlg.Color.R, dlg.Color.G, dlg.Color.B);
			}

			state.TextColor = dlg.Color;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_textFrameset_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedFrameset == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(TextObject))
				return;

			foreach (Keyframe frame in Program.MainTimeline.SelectedFrameset.Keyframes)
			{
				TextObject.State state = frame.State as TextObject.State;

				state.TextColor = pnl_textColorImg.BackColor;
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_textLayer_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedLayer == null)
				return;
			if (Program.MainTimeline.SelectedLayer.Data.GetType() != typeof(TextObject))
				return;

			foreach (Frameset set in Program.MainTimeline.SelectedLayer.Framesets)
			{
				foreach (Keyframe frame in set.Keyframes)
				{
					TextObject.State state = frame.State as TextObject.State;

					state.TextColor = pnl_textColorImg.BackColor;
				}
			}

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void btn_textOpenFontPicker_Click(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(TextObject.State))
				return;

			TextObject.State state = Program.MainTimeline.SelectedKeyframe.State as TextObject.State;

			FontDialog dlg = new FontDialog();

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				txt_textFont.Text = dlg.Font.FontFamily.Name + ", " + dlg.Font.Size + "pt";
			}

			state.TextFont = dlg.Font;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void rtb_textText_TextChanged(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(TextObject.State))
				return;

			TextObject.State state = Program.MainTimeline.SelectedKeyframe.State as TextObject.State;

			state.Text = rtb_textText.Text;

			Program.MainTimeline.GLContext.Invalidate();
		}
		#endregion

		private void num_bitmapAngle_ValueChanged(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(BitmapObject.State))
				return;

			tkb_bitmapAngle.Value = (int)num_bitmapAngle.Value;

			BitmapObject.State state = Program.MainTimeline.SelectedKeyframe.State as BitmapObject.State;

			state.Rotation = (float)num_bitmapAngle.Value;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void tkb_bitmapAngle_Scroll(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(BitmapObject.State))
				return;

			num_bitmapAngle.Value = (decimal)tkb_bitmapAngle.Value;
		}



		private void num_bitmapAlpha_ValueChanged(object sender, EventArgs e)
		{

			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(BitmapObject.State))
				return;

			tkb_bitmapAlpha.Value = (int)num_bitmapAlpha.Value;

			BitmapObject.State state = Program.MainTimeline.SelectedKeyframe.State as BitmapObject.State;

			state.BitmapAlpha = (int)num_bitmapAlpha.Value;

			Program.MainTimeline.GLContext.Invalidate();
		}

		private void tkb_bitmapAlpha_ValueChanged(object sender, EventArgs e)
		{
			if (Program.MainTimeline.SelectedKeyframe == null)
				return;
			if (Program.MainTimeline.SelectedKeyframe.State.GetType() != typeof(BitmapObject.State))
				return;

			num_bitmapAlpha.Value = (decimal)tkb_bitmapAlpha.Value;
		}

		private void btn_stickOpenInEditor_Click(object sender, EventArgs e)
		{
			StickEditorForm ed = new StickEditorForm(((StickFigure)Program.MainTimeline.SelectedLayer.Data).Copy(), (StickFigure.State)Program.MainTimeline.SelectedKeyframe.State.Copy());

			if (ed.ShowDialog() == DialogResult.OK)
			{
				Program.MainTimeline.SelectedLayer.Data = ed.CreatedFigure;
				Program.MainTimeline.SelectedKeyframe.State = ed.CreatedFigureState;
			}

			Program.MainTimeline.GLContext.Invalidate();
		}
	}
}
