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
			//Create the thread to fetch all the files to download
			Thread downloaderThread = new Thread(doDownload);
			downloaderThread.Start();
			lbl_DlTitle.Text = "Now Downloading: File List";

			//Wait for it to finish
			doneDownload.WaitOne();
			
			//Reset so we can use it for the other downloads
			doneDownload.Reset();

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
				Process x = new Process();
				x.StartInfo = new ProcessStartInfo("T0_TMPFILE.exe", "update1 \"" + toExecute + "\"");
				x.Start();
				Application.Exit();
			}
		}

		private void doDownload()
		{
			//Create an object so that we can download shtuff
			WebClient downloader = new WebClient();
			downloader.Proxy = new WebProxy();

			byte[] result = downloader.DownloadData(fileIndexURI);
			MemoryStream txt = new MemoryStream();
			txt.Write(result, 0, result.Length);
			txt.Position = 0;
			TextReader x = new StreamReader(txt);
			string type = x.ReadLine();
			while (type != "stable")
				type = x.ReadLine();

			string dltype = x.ReadLine();
			while (dltype != "exe")
				dltype = x.ReadLine();

			//int filecount = int.Parse(x.ReadLine());
			int filecount = 1;

			downloadQueue = new List<string>(filecount);
			fileNames = new List<string>(filecount);

			for (int a = 0; a < filecount; a++)
			{
				downloadQueue.Add(x.ReadLine());
				fileNames.Add(x.ReadLine());
			}

			toExecute = fileNames[0];

			x.Close();
			x.Dispose();
			txt.Close();
			txt.Dispose();

			doneDownload.Set();
		}


	}
}