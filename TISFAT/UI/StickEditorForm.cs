using System;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT
{
	public partial class StickEditorForm : Form
	{
		public StickEditorForm()
		{
			InitializeComponent();
		}

		private void StickEditorForm_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			ToolStripDropDown drop = new ToolStripDropDown();
			ToolStripControlHost host = new ToolStripControlHost(panel1);

			host.Margin = new Padding(0);
			drop.Padding = new Padding(0);
			drop.Items.Add(host);
			drop.Show(button1, new Point(button1.Left, button1.Bottom));
		}
	}
}
