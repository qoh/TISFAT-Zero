using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TISFAT.Entities;

namespace TISFAT
{
	public partial class AddLayerDialog : Form
	{
		public AddLayerDialog()
		{
			InitializeComponent();
		}

		private void btn_Add_Click(object sender, EventArgs e)
		{
			Layer layer;
			
			switch((string)lsv_LayerTypes.FocusedItem.Tag)
			{
				case "StickFigure":
					StickFigure fig = new StickFigure();
					layer = fig.CreateDefaultLayer(0, 20, new LayerCreationArgs(0, ""));
					break;
				case "BitmapObject":
					if (txt_bitmapPath.Text == "")
						return;

					BitmapObject bit = new BitmapObject();
					layer = bit.CreateDefaultLayer(0, 20, new LayerCreationArgs(0, txt_bitmapPath.Text));
					break;

				default:
					throw new ArgumentException("LayerType Tag is not a known EntityType");
			}

			Program.Form.ActiveProject.Layers.Add(layer);
			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();

			Close();
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btn_bitmapBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();

			if (dlg.ShowDialog() == DialogResult.OK)
				txt_bitmapPath.Text = dlg.FileName;
		}

		private void HidePropertyPanels()
		{
			pnl_PropertiesDescription.Visible = false;
			pnl_BitmapProperties.Visible = false;
		}

		private void lsv_LayerTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			HidePropertyPanels();

			switch ((string)lsv_LayerTypes.FocusedItem.Tag)
			{
				case "StickFigure":
					pnl_PropertiesDescription.Visible = true;
					break;
				case "BitmapObject":
					pnl_BitmapProperties.Visible = true;
					break;

				default:
					throw new ArgumentException("LayerType Tag is not a known EntityType");
			}
		}
	}
}
