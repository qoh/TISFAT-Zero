using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TISFAT_ZERO
{
	public partial class RenameLayer : Form
	{
		int ind;
		public RenameLayer(int index)
		{
			ind = index;
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Timeline.layers[ind].name = textBox1.Text;
			Close(); //lol yes, it's that simple
		}
	}
}
