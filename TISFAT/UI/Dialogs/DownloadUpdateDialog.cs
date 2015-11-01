using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TISFAT
{
	public partial class DownloadUpdateDialog : Form
	{
		WebClient client;

		string url;
		string tempDir;
		Stopwatch watch = new Stopwatch();
		double lastBytesDownloaded = 0;

		public DownloadUpdateDialog(string download)
		{
			InitializeComponent();

			url = download;
		}

		private void DownloadUpdateDialog_Load(object sender, EventArgs e)
		{
			tempDir = Path.GetTempPath() + Path.GetRandomFileName();
			Directory.CreateDirectory(tempDir);

			client = new WebClient();
			client.DownloadProgressChanged += WebClient_DownloadProgressChanged;
			client.DownloadFileCompleted += WebClient_DownloadFileCompleted;

			watch.Start();
			client.DownloadFileAsync(new Uri(url), tempDir + "\\" + Program.TargetMsiName);
		}

		private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			watch.Stop();

			progressBar1.Value = e.ProgressPercentage;
		}

		private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				Process install = new Process();

				install.StartInfo = new ProcessStartInfo(tempDir + "\\" + Program.TargetMsiName);

				install.Start();

				Close();

				Application.Exit();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			client.CancelAsync();
			client.Dispose();

			Close();
		}
	}
}
