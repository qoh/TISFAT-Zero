using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TISFAT_ZERO.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                pnl_About.BringToFront();
            }
            else if (listBox1.SelectedIndex == 1)
            {
                pnl_Developers.BringToFront();
            }
            else if (listBox1.SelectedIndex == 2)
            {
                pnl_Thanks.BringToFront();
            }
        }

        private void About_Load(object sender, EventArgs e)
        {
            pnl_About.BringToFront();
            listBox1.SelectedIndex = 0;
        }
    }
}
