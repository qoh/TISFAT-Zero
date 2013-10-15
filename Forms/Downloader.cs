using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;

namespace TISFAT_ZERO
{
	public partial class Downloader : Form
	{
		private string fileIndexURI = "https://dl.dropboxusercontent.com/s/kbthzy7skh4hmkf/Versions.txt";
		private List<string> downloadQueue, fileNames;
		private string toExecute;
		private ManualResetEvent doneDownload = new ManualResetEvent(false);
		private int bytesDownloaded, totalBytes = -1, lastBytesDownloaded = 0;
		private WebClient downloader;
		private Stopwatch watch = new Stopwatch();
		private int measurements = 0, maxDataPoints = 10;
		private double[] dataPoints;

		public Downloader()
		{
			InitializeComponent();
		}

		private void Downloader_Load(object sender, EventArgs e)
		{
			if (File.Exists("T0Updater.exe"))
			{
				Process x = new Process();
				x.StartInfo = new ProcessStartInfo("T0Updater.exe", "\"" + Path.GetFileName(Application.ExecutablePath) + "\" " + Properties.User.Default.selectedBuilds + " " + Program.Version);
				x.Start();
				Application.Exit();
			}
			else
			{
				downloadQueue = new List<string>(); fileNames = new List<string>();
				downloadQueue.Add("https://dl.dropboxusercontent.com/s/31h1ysf1k32ssue/T0Updater.exe");
				fileNames.Add("T0Updater.exe");
			}
			//Create a downloader object
			downloader = new WebClient();
			downloader.Proxy = new WebProxy();
			downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
			downloader.DownloadDataCompleted += new DownloadDataCompletedEventHandler(client_DownloadFileCompleted);

			lbl_DlTitle.Text = "Now Downloading: " + fileNames[0];
			dataPoints = new double[maxDataPoints];
			watch.Start();
			downloader.DownloadDataAsync(new Uri(downloadQueue[0]));
		}

		void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			watch.Stop();
			double msElapsed = watch.Elapsed.TotalMilliseconds;
			msElapsed = Math.Max(10d, msElapsed);
			bytesDownloaded = (int)e.BytesReceived - lastBytesDownloaded;
			lastBytesDownloaded = (int)e.BytesReceived;
			double dataPoint = bytesDownloaded / (msElapsed / 1000);
			
			dataPoints[measurements++ % maxDataPoints] = dataPoint;

			double downloadSpeed = dataPoints.Average();

			if (totalBytes == -1)
				totalBytes = (int)e.TotalBytesToReceive;

			pgr_fileProgress.Value = e.ProgressPercentage;

			double secondsRemaining = (totalBytes - lastBytesDownloaded) / downloadSpeed;

			if (downloadSpeed < 1000)
				lbl_DlSpeed.Text = "Download Speed: " + Math.Round(downloadSpeed) + " B/s";
			else
			{
				downloadSpeed /= 1000;
				if (downloadSpeed < 1000)
					lbl_DlSpeed.Text = "Download Speed: " + Math.Round(downloadSpeed, 1) + " KB/s";
				else
				{
					downloadSpeed /= 1000;
					if (downloadSpeed < 1000)
						lbl_DlSpeed.Text = "Download Speed: " + Math.Round(downloadSpeed, 2) + " MB/s";
					else
					{
						downloadSpeed /= 1000;
						lbl_DlSpeed.Text = "Download Speed: " + Math.Round(downloadSpeed, 3) + " GB/s";
					}
				}
			}

			if (secondsRemaining < 60)
				lbl_TimeRemaining.Text = "Approximate DL Time: " + Math.Round(secondsRemaining) + " Seconds";
			else
			{
				secondsRemaining /= 60;
				if (secondsRemaining < 60)
					lbl_TimeRemaining.Text = "Approximate DL Time: " + Math.Round(secondsRemaining, 1) + " Minutes";
				else
				{
					secondsRemaining /= 60;
					if (secondsRemaining < 24)
						lbl_TimeRemaining.Text = "Approximate DL Time: " + Math.Round(secondsRemaining, 1) + " Hours";
					else
					{
						secondsRemaining /= 24;
						lbl_TimeRemaining.Text = "Approximate DL Time: " + Math.Round(secondsRemaining, 2) + " Days";
					}
				}
			}
			watch.Restart();
		}

		void client_DownloadFileCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			FileStream write;
			if (fileNames[0] != toExecute)
				write = File.Create(fileNames[0]);
			else
				write = File.Create("T0_TMPFILE.exe");
			watch.Reset();
			byte[] downloadResult = e.Result;

			write.Write(downloadResult, 0, downloadResult.Length);

			write.Close();
			write.Dispose();

			downloadQueue.RemoveAt(0);
			fileNames.RemoveAt(0);

			if (downloadQueue.Count > 0)
			{
				totalBytes = -1;
				bytesDownloaded = 0;

				watch.Start();
				downloader.DownloadDataAsync(new Uri(downloadQueue[0]));
				lbl_DlTitle.Text = "Now Downloading: " + fileNames[0];
			}
			else
			{
				while (!File.Exists("T0Updater.exe"))
					Thread.Sleep(100);

				Process x = new Process();
				x.StartInfo = new ProcessStartInfo("T0Updater.exe", "\"" + Path.GetFileName(Application.ExecutablePath) + "\" " + Properties.User.Default.selectedBuilds + " " + Program.Version);
				x.Start();
				Application.Exit();
			}
		}
	}
}