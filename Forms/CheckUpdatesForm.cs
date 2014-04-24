using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.Drawing;

namespace TISFAT_ZERO
{
	public partial class CheckUpdateForm : Form
	{
		private string fileIndexURI = "https://dl.dropboxusercontent.com/s/kbthzy7skh4hmkf/Versions.txt";
		private string newVersion;

		private WebClient downloader;

		private bool closeWhenUpToDate;
		private bool downloadingStage = false; //There are 2 downloads that need to be done; the versions text download and the changelog.
		//False: versions  True: changelog

		public CheckUpdateForm(bool x = false)
		{
			InitializeComponent();

			closeWhenUpToDate = x;
		}

		public void doLoadEvent()
		{
			CheckUpdatesForm_Load(new object(), new EventArgs());
		}

		private void CheckUpdatesForm_Load(object sender, EventArgs e)
		{
			//Create a webclient to download the file
			downloader = new WebClient();

			//This is so it doesn't take ages to start up the connection for the first time because of proxy detection. I might change this later. *might*
			downloader.Proxy = new WebProxy();

			downloader.DownloadDataCompleted += new DownloadDataCompletedEventHandler(versionChecker_Done);

			downloader.DownloadDataAsync(new Uri(fileIndexURI));
		}

		void versionChecker_Done(object sender, DownloadDataCompletedEventArgs e)
		{
			if (!downloadingStage)
			{
				//Write the result to a memorystream
				MemoryStream txt = new MemoryStream();
				txt.Write(e.Result, 0, e.Result.Length);
				txt.Position = 0;

				//Put it into a textreader so we can easily read through the result sequentially
				TextReader x = new StreamReader(txt);

				//Read through it until we reach the version we want to fetch
				string type = x.ReadLine();
				while (type != Properties.User.Default.selectedBuilds)
					type = x.ReadLine();

				x.ReadLine();

				//Put both version into int arrays so we can compare them
				newVersion = x.ReadLine(); 
				string currentVersion = Program.Version;

				string[] v1 = newVersion.Split('.'), v2 = currentVersion.Split('.');

				int[] V1 = new int[v1.Length], V2 = new int[v2.Length];
				int z = 0;

				foreach (string v in v1)
				{
					V1[z] = Int32.Parse(v1[z]);
					V2[z] = Int32.Parse(v2[z++]);
				}

				//Compare the two arrays to see if we need to update
				bool needsUpdating = false;
				for (int a = 0; a < V1.Length; a++)
				{
					if (V2[a] < V1[a])
					{
						needsUpdating = true;
						break;
					}
					else if (V2[a] > V1[a])
						break;
				}

				if (!needsUpdating && !closeWhenUpToDate)
				{
					//Let the user know they're up to date and return
					lbl_DlTitle.Text = "TISFAT : Zero is up to date.";
					Btn_Cancel.Text = "Back";
					Btn_Cancel.Location = new Point(120, 36);

					x.Close();

					txt.Close();
					return;
				}
				else if (!needsUpdating && closeWhenUpToDate)
				{
					this.Close();
					return;
				}

				//Find the link to the changelog (and download it so we can display it)
				while (x.ReadLine() != "changelog") ;

				Uri changelogLink = null;

				try
				{
					changelogLink = new Uri(x.ReadLine());
				}
				catch
				{
					return; //I'll make it show an error message later.
				}

				downloadingStage = true;

				if (changelogLink != null)
				{
					lbl_DlTitle.Text = "Downloading Changelog";

					downloader.DownloadDataAsync(changelogLink);
				}
				else
				{
					//Put up the update notification with no changelog
					UpdateNotification f = new UpdateNotification(new string[] { "", "The changelog couldn't be fetched." }, newVersion.Substring(1));
					f.Show();
				}

				//Close and dispose the streams
				x.Close();

				txt.Close();
				txt.Dispose();
			}
			else
			{
				//Write the result to a memorystream
				MemoryStream txt = new MemoryStream();
				txt.Write(e.Result, 0, e.Result.Length);
				txt.Position = 0;

				//Put it into a textreader so we can easily read through the result sequentially
				TextReader x = new StreamReader(txt);

				//Make a list to hold the lines of the changelog
				List<string> lines = new List<string>();

				lines.Add(newVersion);

				bool canGetChangelog = true;

				//Keep reading until we get to the version we want (I dunno how it wouldn't be the lastest one right away but whatever)
				for (string line = x.ReadLine(); line != newVersion; line = x.ReadLine())
				{
					//If the line is null then that means that we've reached the end of the stream.
					//Note there's a difference between a blank string ("") and a null string.
					if (line == null)
					{
						canGetChangelog = false;
						break;
					}
				}

				if (canGetChangelog)
				{
					//Add all the lines we read until we reach one that says "end"
					for (string line = x.ReadLine(); line != "end"; line = x.ReadLine())
						lines.Add(line);
				}
				else
				{
					lines[0] = "Unable to fetch changelog for the new version.";
				}

				//Transfer it into an array
				string[] changelog = lines.ToArray();

				lines.Clear();

				//Pop up an update notification
				UpdateNotification f = new UpdateNotification(changelog, newVersion); //I do the substring from position 1 to get rid of the 'v' as it's kinda useless.
				f.Show();

				//Close and dispose the streams
				x.Close();

				txt.Close();
			}

			//If we're all done downloading things, close the form
			if (downloadingStage)
				this.Close();
		}

		private void Btn_Cancel_Click(object sender, EventArgs e)
		{
			//Cancel any downloads and close the form
			downloader.CancelAsync();
			this.Close();
		}
	}
}