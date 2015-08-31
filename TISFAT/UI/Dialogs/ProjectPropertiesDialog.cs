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
	public partial class ProjectPropertiesDialog : Form
	{
		public ProjectPropertiesDialog()
		{
			InitializeComponent();
		}

		private void ProjectPropertiesDialog_Load(object sender, EventArgs e)
		{
			num_CanvasWidth.Value = Program.ActiveProject.Width;
			num_CanvasHeight.Value = Program.ActiveProject.Height;
			num_AnimSpeed.Value = (int)Program.ActiveProject.FPS;

			Color color = Program.ActiveProject.BackColor;
            pnl_CanvasColor.BackColor = color;
			lbl_CanvasColorNumbers.Text = string.Format("{0}, {1}, {2}, {3}", color.R, color.G, color.B, color.A);
		}

		private void btn_OK_Click(object sender, EventArgs e)
		{
			Program.ActiveProject.Width = (int)num_CanvasWidth.Value;
			Program.ActiveProject.Height = (int)num_CanvasHeight.Value;
			Program.ActiveProject.FPS = (float)num_AnimSpeed.Value;
			Program.ActiveProject.BackColor = pnl_CanvasColor.BackColor;
		}
	}
}
