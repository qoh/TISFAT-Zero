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
	public partial class SaveChangesDialog : Form
	{
		public SaveChangesDialog()
		{
			InitializeComponent();
		}

		private void btn_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
