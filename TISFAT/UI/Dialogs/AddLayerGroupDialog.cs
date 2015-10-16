using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TISFAT
{
	public partial class AddLayerGroupDialog : Form
	{
		public string ReturnText { get; set; }

		public AddLayerGroupDialog()
		{
			InitializeComponent();
		}

		private void RenameLayerDialog_Load(object sender, EventArgs e)
		{
			txt_layerName.Text = Program.ActiveProject.Layers[Program.MainTimeline.HoveredLayerIndex].Name;
		}

		private void txt_layerName_TextChanged(object sender, EventArgs e)
		{
			ReturnText = txt_layerName.Text;
		}

		private void btn_OK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Abort;
			Close();
		}
	}
}
