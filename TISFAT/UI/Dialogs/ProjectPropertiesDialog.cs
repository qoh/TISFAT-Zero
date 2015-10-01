using System;
using System.Drawing;
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
			num_AnimSpeed.Value = (int)Program.ActiveProject.AnimSpeed;
			num_FPS.Value = (int)Program.ActiveProject.FPS;

			Color color = Program.ActiveProject.BackColor;
			pnl_CanvasColor.BackColor = color;
			lbl_CanvasColorNumbers.Text = string.Format("{0}, {1}, {2}, {3}", color.R, color.G, color.B, color.A);
		}

		private void btn_OK_Click(object sender, EventArgs e)
		{
			Program.ActiveProject.Width = (int)num_CanvasWidth.Value;
			Program.ActiveProject.Height = (int)num_CanvasHeight.Value;
			Program.ActiveProject.AnimSpeed = (float)num_AnimSpeed.Value;
			Program.ActiveProject.FPS = (float)num_FPS.Value;
			Program.ActiveProject.BackColor = pnl_CanvasColor.BackColor;

			Program.Form_Canvas.GLContext_Init();
			Program.Form_Canvas.CanvasForm_Resize(null, null);
			Program.MainTimeline.GLContext.Invalidate();

			Close();
		}

		private void pnl_CanvasColor_Click(object sender, EventArgs e)
		{
			ColorPickerDialog dlg = new ColorPickerDialog();
			
			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				pnl_CanvasColor.BackColor = dlg.Color;
				lbl_CanvasColorNumbers.Text = string.Format("{0}, {1}, {2}, {3}", dlg.Color.R, dlg.Color.G, dlg.Color.B, dlg.Color.A);
			}
		}
	}
}
