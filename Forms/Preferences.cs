using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/* -Conf.Prop-
		 * Default Save Location
		 * Default Canvas Size X,Y
		 *
		 *
		 */

		//TODO: Fix all this crap.
		private void initSettings()
		{
			if (Directory.Exists(folderPath))
			{
				if (File.Exists(folderPath + "conf.prop"))
				{
					string[] contents = File.ReadAllLines(folderPath + "conf.prop");

					txt_defaultSaveLoc.Text = contents[0];
					string[] canvasSize = contents[1].Split(',');

					txt_canvasX.Text = canvasSize[0];
					txt_canvasY.Text = canvasSize[1];
					return;
				}
				else
				{
					createNewConfig();
					initSettings();
				}
			}
			else
			{
				createNewConfig();
				initSettings();
			}
		}

		private void createNewConfig()
		{
			saveFile = new string[2];
			if (Directory.Exists(folderPath))
			{
				saveFile[0] = Environment.SpecialFolder.MyDocuments + "\\TISFAT\\";
				saveFile[1] = "460,360";

				File.WriteAllLines(folderPath + "conf.prop", saveFile);
			}
			else
			{
				Directory.CreateDirectory(folderPath);
				File.Create(folderPath + "conf.prop");

				saveFile[0] = Environment.SpecialFolder.MyDocuments + "\\TISFAT\\";
				saveFile[1] = "460,360";

				File.WriteAllLines(folderPath + "conf.prop", saveFile);
			}
		}

		private void btn_browseLocation_Click(object sender, EventArgs e)
		{
			dlg_folderBrowser.ShowDialog();
			txt_defaultSaveLoc.Text = dlg_folderBrowser.SelectedPath;
		}

		private void Preferences_Load(object sender, EventArgs e)
		{
			initSettings();
		}
	}
}