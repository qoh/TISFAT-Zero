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
	public partial class testEnter : Form
	{
		public Timeline t;
		public testEnter(Timeline tl)
		{
			t = tl;
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			t.setFrame(uint.Parse(textBox1.Text));
			Close();
		}
	}
}
