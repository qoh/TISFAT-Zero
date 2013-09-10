using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T0_StickEditor
{
	public partial class Main : Form
	{
		public static Canvas theCanvas;

		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			theCanvas = new Canvas(this);
			theCanvas.TopLevel = false;
			theCanvas.Parent = this.pnl_Main;
			theCanvas.Size = new Size(this.pnl_Main.Width, pnl_Main.Height);
			theCanvas.StartPosition = FormStartPosition.Manual;
			theCanvas.Location = new Point(0, 0);
			this.pnl_Main.Controls.Add(theCanvas);

			theCanvas.Show();
		}
	}
}
