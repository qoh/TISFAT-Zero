using System;
using System.Windows.Forms;
using TISFAT.Entities;

namespace TISFAT
{
	public partial class AddLayerDialog : Form
	{
		StickFigure CustomFigure;
		StickFigure.State CustomFigureState;

		public AddLayerDialog()
		{
			InitializeComponent();
		}

		private void AddLayerDialog_Load(object sender, EventArgs e)
		{
			lsv_LayerTypes.MultiSelect = false;
			cmb_DefaultFigureVariant.SelectedIndex = 0;
		}

		private void btn_Add_Click(object sender, EventArgs e)
		{
			if (lsv_LayerTypes.FocusedItem == null)
				return;

			switch ((string)lsv_LayerTypes.FocusedItem.Tag)
			{
				case "StickFigure":
					Program.Form_Main.Do(new LayerAddAction(typeof(StickFigure), 0, 20, new LayerCreationArgs(cmb_DefaultFigureVariant.SelectedIndex, "")));
					break;
				case "CustomFigure":
					if(CustomFigure != null)
					Program.Form_Main.Do(new LayerAddAction(typeof(CustomFigure), 0, 20, new LayerCreationArgs(2, new Tuple<StickFigure, StickFigure.State>(CustomFigure, CustomFigureState))));
					break;
				case "LineObject":
					Program.Form_Main.Do(new LayerAddAction(typeof(LineObject), 0, 20, new LayerCreationArgs(0, "")));
					break;
				case "RectObject":
					Program.Form_Main.Do(new LayerAddAction(typeof(RectObject), 0, 20, new LayerCreationArgs(0, "")));
					break;
				case "CircleObject":
					Program.Form_Main.Do(new LayerAddAction(typeof(CircleObject), 0, 20, new LayerCreationArgs(0, "")));
					break;
				case "PolyObject":
					Program.Form_Main.Do(new LayerAddAction(typeof(PolyObject), 0, 20, new LayerCreationArgs((int)num_polyNumPoints.Value, "")));
					break;
				case "BitmapObject":
					if (txt_bitmapPath.Text == "") return;
					Program.Form_Main.Do(new LayerAddAction(typeof(BitmapObject), 0, 20, new LayerCreationArgs(0, txt_bitmapPath.Text)));
					break;
				case "TextObject":
					Program.Form_Main.Do(new LayerAddAction(typeof(TextObject), 0, 20, new LayerCreationArgs(0, "")));
					break;
				case "PointLight":
					Program.Form_Main.Do(new LayerAddAction(typeof(PointLight), 0, 20, new LayerCreationArgs(0, "")));
					break;

				default:
					throw new ArgumentException("LayerType Tag is not a known EntityType");
			}

			Program.MainTimeline.GLContext.Invalidate();

			Close();
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btn_bitmapBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();

			dlg.Title = "Open an Image";
			dlg.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp";

			if (dlg.ShowDialog() == DialogResult.OK)
				txt_bitmapPath.Text = dlg.FileName;
		}

		private void HidePropertyPanels()
		{
			pnl_PropertiesDescription.Visible = false;
			pnl_DefaultFigureProperties.Visible = false;
			pnl_CustomFigureProperties.Visible = false;
			pnl_LineProperties.Visible = false;
			pnl_RectangleProperties.Visible = false;
			pnl_CircleProperties.Visible = false;
			pnl_PolygonProperties.Visible = false;
			pnl_BitmapProperties.Visible = false;
			pnl_TextProperties.Visible = false;
			pnl_PointLightProperties.Visible = false;
		}

		private void lsv_LayerTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			HidePropertyPanels();

			if (lsv_LayerTypes.FocusedItem == null)
				return;

			switch ((string)lsv_LayerTypes.FocusedItem.Tag)
			{
				case "StickFigure":
					pnl_DefaultFigureProperties.Visible = true;
					break;
				case "CustomFigure":
					pnl_CustomFigureProperties.Visible = true;
					break;
				case "LineObject":
					pnl_LineProperties.Visible = true;
					break;
				case "RectObject":
					pnl_RectangleProperties.Visible = true;
					break;
				case "CircleObject":
					pnl_CircleProperties.Visible = true;
					break;
				case "PolyObject":
					pnl_PolygonProperties.Visible = true;
					break;
				case "BitmapObject":
					pnl_BitmapProperties.Visible = true;
					break;
				case "TextObject":
					pnl_TextProperties.Visible = true;
					break;
				case "PointLight":
					pnl_PointLightProperties.Visible = true;
					break;

				default:
					throw new ArgumentException("LayerType Tag is not a known EntityType");
			}
		}

		private void cmb_DefaultFigureVariant_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmb_DefaultFigureVariant.SelectedIndex == 0)
			{
				lbl_DefaultFigureVariantDetail.Text = "This variant is the same as the inital figure you see when you start TISFAT Zero.";
			}
			else if (cmb_DefaultFigureVariant.SelectedIndex == 1)
			{
				lbl_DefaultFigureVariantDetail.Text = "This is the pre-rework TISFAT Zero default figure, which is a bit smaller and has different proportions than the current default.";
			}
		}

		private void btn_customFigBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();

			dlg.Title = "Open an Existing Custom Figure";
			dlg.Filter = "TISFAT Zero Figures|*.tzf";

			if (dlg.ShowDialog() == DialogResult.OK)
				txt_customFigPath.Text = dlg.FileName;
		}

		private void btn_openStickEditor_Click(object sender, EventArgs e)
		{
			StickEditorForm f = new StickEditorForm();

			if(f.ShowDialog() == DialogResult.OK)
			{
				txt_customFigPath.Text = "Custom figure created with Editor";
				CustomFigure = f.CreatedFigure;
				CustomFigureState = f.CreatedFigureState;
			}
		}
	}
}
