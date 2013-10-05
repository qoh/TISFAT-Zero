using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace TISFAT_ZERO
{
	public partial class Downloader : Form
	{
		private string fileIndexURI = "https://dl.dropboxusercontent.com/s/kbthzy7skh4hmkf/Versions.txt";
		private List<string> downloadQueue, fileNames;
		private ManualResetEvent doneDownload = new ManualResetEvent(false);
		private int bytesDownloaded, totalBytes = -1;
		private WebClient downloader;
		private DateTime prevTick;

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

			downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
			downloader.DownloadDataCompleted += new DownloadDataCompletedEventHandler(client_DownloadFileCompleted);

			lbl_DlTitle.Text = "Now Downloading: " + fileNames[0];
			prevTick = DateTime.Now;
			downloader.DownloadDataAsync(new Uri(downloadQueue[0]));
		}

		void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			int msElapsed = (int)((DateTime.Now - prevTick).TotalMilliseconds);

			int bytesRecieved = (int)e.BytesReceived - bytesDownloaded;

			bytesDownloaded = (int)e.BytesReceived;

			if (totalBytes == -1)
				totalBytes = (int)e.TotalBytesToReceive;

			int percentage = (bytesDownloaded * 100) / totalBytes;

			pgr_fileProgress.Value = 100;
			pgr_fileProgress.Value = Math.Min(percentage, 100);

			double downloadSpeed = (double)bytesRecieved / (Math.Max(msElapsed, 1) / 1000d);

			if (downloadSpeed < 1000)
			{
				lbl_DlSpeed.Text = "Download Speed: " + Math.Round(downloadSpeed) + " B/s";
			}
			else
			{
				downloadSpeed /= 1000;
				if (downloadSpeed < 1000)
				{
					lbl_DlSpeed.Text = "Download Speed: " + Math.Round(downloadSpeed, 1) + " KB/s";
				}
				else
				{
					downloadSpeed /= 1000;
					if (downloadSpeed < 1000)
					{
						lbl_DlSpeed.Text = "Download Speed: " + Math.Round(downloadSpeed, 2) + " MB/s";
					}
					else
					{
						downloadSpeed /= 1000;
						lbl_DlSpeed.Text = "Download Speed: " + Math.Round(downloadSpeed, 3) + " GB/s";
					}
				}
			}
			prevTick = DateTime.Now;
		}

		void client_DownloadFileCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			FileStream write = File.Create(fileNames[0]);

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

				prevTick = DateTime.Now;
				downloader.DownloadDataAsync(new Uri(downloadQueue[0]));
				lbl_DlTitle.Text = "Now Downloading: " + fileNames[0];
			}
		}

		private void doDownload()
		{
			//Create an object so that we can download shtuff
			WebClient downloader = new WebClient();

			byte[] result = downloader.DownloadData(fileIndexURI);
			MemoryStream txt = new MemoryStream();
			txt.Write(result, 0, result.Length);
			txt.Position = 0;
			TextReader x = new StreamReader(txt);

			string type = x.ReadLine();

			int filecount = int.Parse(x.ReadLine());

			downloadQueue = new List<string>(filecount);
			fileNames = new List<string>(filecount);

			for (int a = 0; a < filecount; a++)
			{
				downloadQueue.Add(x.ReadLine());
				fileNames.Add(x.ReadLine());
			}

			x.Close();
			x.Dispose();
			txt.Close();
			txt.Dispose();

			doneDownload.Set();
		}


	}
}