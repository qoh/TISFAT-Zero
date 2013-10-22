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
		public static byte currentBuild = 2; //current build version. 0 = stable, 1 = beta, 2 = nightly
		private string buildName;
		public static string[] buildNames = new string[] { "stable", "beta", "nightly" };

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
			buildName = buildNames[currentBuild];

			label1.Text = "Build Version: v" + Program.Version;

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

			string bld_Installed = Properties.User.Default.buildsInstalled;

			if (bld_Installed != buildName)
			{
				Properties.User.Default.buildsInstalled = Properties.User.Default.selectedBuilds = buildName;
				Properties.User.Default.buildLocations = Application.ExecutablePath;
			}

			comboBox1.SelectedIndex = currentBuild;
			checkBox1.Checked = Properties.User.Default.autoCheck;
		}

		private void pnl_colorButtonHitbox_MouseClick(object sender, MouseEventArgs e)
		{
			if(!(dlg_colorDialog.ShowDialog() == DialogResult.OK))
				return;

			pic_colorBox.BackColor = dlg_colorDialog.Color;
			lbl_backgroundColorPic.Text = dlg_colorDialog.Color.Name;
		}

		private void pnl_colorButtonHitbox_MouseClick(object sender, EventArgs e)
		{
			if(!(dlg_colorDialog.ShowDialog() == DialogResult.OK))
				return;
			pic_colorBox.BackColor = dlg_colorDialog.Color;
			lbl_backgroundColorPic.Text = dlg_colorDialog.Color.Name;
		}

		private void btn_defSavPathBrowse_Click(object sender, EventArgs e)
		{
			if(!(dlg_folderBrowser.ShowDialog() == DialogResult.OK))
				return;
			if(!(dlg_folderBrowser.SelectedPath == ""))
				txt_defaultSavePath.Text = dlg_folderBrowser.SelectedPath;
		}

		private void btn_submitButton_Click(object sender, EventArgs e)
		{
			Properties.User.Default.DefaultSavePath = txt_defaultSavePath.Text;
			Properties.User.Default.CanvasColor = pic_colorBox.BackColor;
			Properties.User.Default.CanvasSize = new System.Drawing.Size((int)num_Width.Value, (int)num_Height.Value);
			Properties.User.Default.Save();

			if (comboBox1.SelectedIndex != currentBuild)
			{
				Properties.User.Default.selectedBuilds = buildNames[comboBox1.SelectedIndex];
				CheckUpdateForm x = new CheckUpdateForm();
				x.ShowDialog();
			}

			this.Close();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.Items[0].Selected)
				pnl_General.BringToFront();
			if (listView1.Items[1].Selected)
			{
				pnl_Updates.BringToFront();
				checkBox1_CheckedChanged(new object(), new EventArgs());
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			label2.Text = "TISFAT:Zero will" + (checkBox1.Checked ? " " : " not ") + "automatically check for updates for checked builds upon opening the program.";
			Properties.User.Default.autoCheck = checkBox1.Checked;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
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