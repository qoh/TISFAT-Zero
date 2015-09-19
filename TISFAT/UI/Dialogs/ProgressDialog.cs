using System;
using System.Windows.Forms;

namespace TISFAT
{
	public partial class ProgressDialog : Form
	{
		public string Title
		{
			get { return Text; }
			set { Text = value; }
		}

		public string DetailText
		{
			get { return lbl_Text.Text; }
			set { lbl_Text.Text = value; }
		}

		public ProgressBarStyle ProgressStyle
		{
			get { return prg_Display.Style; }
			set { prg_Display.Style = value; }
		}

		public int ProgressValue
		{
			get { return prg_Display.Value; }
			set { prg_Display.Value = value; }
		}

		public event EventHandler Canceled;
		public Action Work;

		public ProgressDialog()
		{
			InitializeComponent();
		}

		public void Finish(object sender, EventArgs e)
		{
			Invoke((MethodInvoker)(() =>
			{
				Close();
			}));
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			if (Canceled != null)
				Canceled(sender, e);

			Close();
		}

		private void ProgressDialog_Shown(object sender, EventArgs e)
		{
			if (Work != null)
				Work();
		}
	}
}
