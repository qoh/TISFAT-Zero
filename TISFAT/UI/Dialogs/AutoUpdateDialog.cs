using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TISFAT
{
	public partial class AutoUpdateDialog : Form
	{
		string version;
		string url;
		int downloadcount;
		string body;

		public AutoUpdateDialog(string newVersion, string downloadURL, int downloadCount, string Body)
		{
			InitializeComponent();

			version = newVersion;
			url = downloadURL;
			downloadcount = downloadCount;
			body = Body;
		}

		private void AutoUpdateDialog_Load(object sender, EventArgs e)
		{
			rtb_updateDesc.Text = body;
			lbl_Version.Text = string.Format("Your Version: {0}\r\nNew Version: {1}\r\n\r\nDownloads: {2}", Application.ProductVersion.Remove(Application.ProductVersion.Length - 2, 2), version.TrimStart('v'), downloadcount);
		}

		public static void CheckForUpdates(bool user)
		{
			string url = "https://api.github.com/repos/atomic-software/TISFAT-Zero/releases/latest";

			HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);

			Request.UserAgent = "TISFAT Zero";

			WebResponse Response;

			try
			{
				Response = Request.GetResponse();
			}
			catch (WebException ex)
			{
				var response = ex.Response;
				StreamReader r = new StreamReader(response.GetResponseStream());
				MessageBox.Show(r.ReadToEnd());
				return;
			}
			StreamReader reader = new StreamReader(Response.GetResponseStream());

			string result = reader.ReadToEnd();

			GithubReleaseDto latestRelease = JsonConvert.DeserializeObject<GithubReleaseDto>(result);

			string[] version = latestRelease.body.Split('\r');

			if (IsVersionNewer(version[0], Application.ProductVersion))
			{
				GithubAssetsDto asset = null;

				foreach(GithubAssetsDto set in latestRelease.assets)
				{
					if(set.name == Program.TargetMsiName)
					{
						asset = set;
						break;
					}
				}

				if(asset == null)
				{
					// Some error
					return;
				}

				AutoUpdateDialog dlg = new AutoUpdateDialog(version[0], asset.browser_download_url, asset.download_count, latestRelease.body);

				dlg.StartPosition = FormStartPosition.CenterParent;

				dlg.ShowDialog();
			}
			else if (user)
			{
				MessageBox.Show("You have the latest version of TISFAT Zero.", "No updates available");
			}
		}

		public static bool IsVersionNewer(string source, string check)
		{
			int[] ver1, ver2;

			source = source.TrimStart('v');
			check = check.TrimStart('v');

			ver1 = Array.ConvertAll(source.Split('.'), s => int.Parse(s));
			ver2 = Array.ConvertAll(check.Split('.'), s => int.Parse(s));

			if (ver1[0] == ver2[0] &&
				ver1[1] == ver2[1] &&
				ver1[2] == ver2[2])
				return false;

			if (ver1[0] > ver2[0])
				return true;
			if (ver1[1] > ver2[1])
				return true;
			if (ver1[2] > ver2[2])
				return true;

			return false;
		}

		private void btn_updateNow_Click(object sender, EventArgs e)
		{
			DownloadUpdateDialog dlg = new DownloadUpdateDialog(url);

			dlg.StartPosition = FormStartPosition.CenterParent;

			dlg.ShowDialog();

			Close();
		}

		private void btn_updateOnClose_Click(object sender, EventArgs e)
		{
			Program.updateScheduled = true;
			Program.updateUrl = url;

			Close();
		}

		private void btn_noThanks_Click(object sender, EventArgs e)
		{
			Close();
		}
	}

	public class GithubReleaseDto
	{
		public string url;
		public string assets_url;
		public string upload_url;
		public string html_url;
		public long id;
		public string tag_name;
		public string target_commitish;
		public string name;
		public bool draft;
		public GithubAuthorDto author;
		public bool prerelease;
		public string created_at;
		public string published_at;
		public List<GithubAssetsDto> assets;
		public string tarball_url;
		public string zipball_url;
		public string browser_download_url;
		public string body;
	}

	public class GithubAuthorDto
	{
		public string login;
		public long id;
		public string avatar_url;
		public string gravatar_id;
		public string url;
		public string html_url;
		public string followers_url;
		public string following_url;
		public string gists_url;
		public string starred_url;
		public string subscriptions_url;
		public string organizations_url;
		public string repos_url;
		public string events_url;
		public string received_events_url;
		public string type;
		public bool site_admin;
	}

	public class GithubAssetsDto
	{
		public string url;
		public long id;
		public string name;
		public string label;
		public GithubAuthorDto uploader;
		public string content_type;
		public string state;
		public long size;
		public int download_count;
		public string created_at;
		public string updated_at;
		public string browser_download_url;
	}
}
