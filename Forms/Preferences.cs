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

		/* -Conf.Prop-
		 * Default Save Location
		 * Default Canvas Size X,Y
		 *
		 *
		 */

		//TODO: Fix all this crap.

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

		private void Preferences_Load(object sender, EventArgs e)
		{
		}
	}
}