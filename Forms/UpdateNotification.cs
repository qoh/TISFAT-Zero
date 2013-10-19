using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace TISFAT_ZERO
{
	public partial class UpdateNotification : Form
	{
		private string fileIndexURI = "https://dl.dropboxusercontent.com/s/31h1ysf1k32ssue/T0Updater.exe";

		private WebClient downloader;

		public UpdateNotification(string[] lines, string newVersion)
		{
			InitializeComponent();
			rtxt_changelog.Lines = lines;
			lbl_version.Text = "Current Version: " + Program.Version + "   New Version: " + newVersion;
		}

		private void btn_cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_downloadButton_Click(object sender, EventArgs e)
		{
			//Create a webclient to download the file
			downloader = new WebClient();

			//This is so it doesn't take ages to start up the connection for the first time because of proxy detection. I might change this later. *might*
			downloader.Proxy = new WebProxy();

			//Add an event so we know when the download finishes and then start the file download
			downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(DDownloader_Done);
			downloader.DownloadFileAsync(new Uri(fileIndexURI), "T0Updater.exe");
		}

		void DDownloader_Done(object sender, AsyncCompletedEventArgs e)
		{
			Process x = new Process();
			x.StartInfo = new ProcessStartInfo("T0Updater.exe", "\"" + Path.GetFileName(Application.ExecutablePath) + "\" " + Properties.User.Default.selectedBuilds + " " + Program.Version);
			x.Start();
			Application.Exit();
		}
	}
}
