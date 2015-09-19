using System;
using System.Windows.Forms;

namespace TISFAT
{
	public partial class ToolboxForm : Form
	{
		public ToolboxForm(Control parent)
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
	}
}
