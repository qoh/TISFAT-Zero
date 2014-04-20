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
	public partial class Scenes : Form
	{
		public Scenes()
		{
			InitializeComponent();
		}

		private void Scenes_Load(object sender, EventArgs e)
		{
			this.Size = new Size(240, 375);
		}

		private void sceneObject1_MouseEnter(object sender, EventArgs e)
		{
			sceneObject1.BackColor = Color.FromKnownColor(KnownColor.Highlight);
		}

		private void sceneObject1_MouseLeave(object sender, EventArgs e)
		{
			sceneObject1.BackColor = Color.FromKnownColor(KnownColor.ControlDarkDark);
		}
	}
}
