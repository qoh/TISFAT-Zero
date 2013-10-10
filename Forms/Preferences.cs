using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TISFAT_ZERO.Properties;

namespace TISFAT_ZERO
{
	public partial class Preferences : Form
	{
		private string folderPath = Environment.SpecialFolder.ApplicationData + "\\TISFAT\\";

		private string[] saveFile;

		public Preferences()
		{
			InitializeComponent();
		}

		/* -Conf.Prop-
			* Default Save Location
			* Default Canvas Size X,Y
			*
			*
			*/

		//TODO: Fix all this crap.

		private void Preferences_Load(object sender, EventArgs e)
		{
            listView1.Items[0].Selected = true;

			if (Properties.User.Default.DefaultSavePath == "")
			{
				txt_defaultSavePath.Text = Environment.SpecialFolder.MyDocuments + "\\TISFAT\\";
				Properties.User.Default.DefaultSavePath = txt_defaultSavePath.Text;
				Properties.User.Default.Save();
			}

			txt_defaultSavePath.Text = Properties.User.Default.DefaultSavePath;
			pic_colorBox.BackColor = Properties.User.Default.CanvasColor;
			lbl_backgroundColorPic.Text = Properties.User.Default.CanvasColor.Name;

			num_Width.Value = Properties.User.Default.CanvasSize.Width;
			num_Height.Value = Properties.User.Default.CanvasSize.Height;
		}

		private void pnl_colorButtonHitbox_MouseClick(object sender, MouseEventArgs e)
		{
			dlg_colorDialog.ShowDialog();
			pic_colorBox.BackColor = dlg_colorDialog.Color;
			lbl_backgroundColorPic.Text = dlg_colorDialog.Color.Name;
		}

		private void pnl_colorButtonHitbox_MouseClick(object sender, EventArgs e)
		{
			dlg_colorDialog.ShowDialog();
			pic_colorBox.BackColor = dlg_colorDialog.Color;
			lbl_backgroundColorPic.Text = dlg_colorDialog.Color.Name;
		}

		private void btn_defSavPathBrowse_Click(object sender, EventArgs e)
		{
			dlg_folderBrowser.ShowDialog();
			if(!(dlg_folderBrowser.SelectedPath == ""))
				txt_defaultSavePath.Text = dlg_folderBrowser.SelectedPath;
		}

		private void btn_submitButton_Click(object sender, EventArgs e)
		{
			Properties.User.Default.DefaultSavePath = txt_defaultSavePath.Text;
			Properties.User.Default.CanvasColor = pic_colorBox.BackColor;

			Properties.User.Default.CanvasSize = new System.Drawing.Size((int)num_Width.Value, (int)num_Height.Value);
			Properties.User.Default.Save();

			this.Close();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (listView1.Items[0].Selected)
                pnl_General.BringToFront();
            if (listView1.Items[1].Selected)
                pnl_Updates.BringToFront();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			label2.Text = "TISFAT:Zero will " + (checkBox1.Checked ? " " : "not ") + "automatically download updates for the selected builds upon closing the program.";
		}
	}

	namespace NumericEditBox
	{
		/// <summary>
		/// Summary description for Class1.
		/// </summary>
		public class NumericEditBox : System.Windows.Forms.NumericUpDown
		{
			public NumericEditBox()
			{
				Controls[0].Visible = false;
			}
		}
	}
}