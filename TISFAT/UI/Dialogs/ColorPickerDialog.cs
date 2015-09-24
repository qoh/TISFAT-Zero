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
	public partial class ColorPickerDialog : Form
	{
		public Color Color
		{
			get; set;
		}

		private bool expanded = false;

		public ColorPickerDialog()
		{
			InitializeComponent();
		}

		private void ColorPickerDialog_Load(object sender, EventArgs e)
		{
			colorEditorManager1.Color = Color.Black;
		}

		private void btn_ExpandWindow_Click(object sender, EventArgs e)
		{
			if(expanded)
			{
				Size = new Size(214, 271);
				btn_ExpandWindow.Text = "More >>";
				colorEditor1.Visible = false;
				colorGrid1.Height /= 3;
			}
			else
			{
				Size = new Size(472, 314);
				btn_ExpandWindow.Text = "<< Less";
				colorEditor1.Visible = true;
				colorGrid1.Height *= 3;
			}

			expanded = !expanded;
		}

		private void colorEditorManager1_ColorChanged(object sender, EventArgs e)
		{
			pnl_Color.BackColor = colorGrid1.Color;
		}

		private void btn_OK_Click(object sender, EventArgs e)
		{
			Color = colorEditorManager1.Color;
			Close();
		}
	}
}
