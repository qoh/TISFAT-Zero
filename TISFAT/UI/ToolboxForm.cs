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
    public partial class ToolboxForm : Form
    {
        public ToolboxForm(Control parent)
        {
            InitializeComponent();

            // Setup stuff
            TopLevel = false;
            parent.Controls.Add(this);
        }
    }
}
