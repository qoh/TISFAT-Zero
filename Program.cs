using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using TISFAT_ZERO.Forms;

namespace TISFAT_ZERO
{
	static class Program
	{
		public static MainF MainformForm;
		public static Toolbox ToolboxForm;
		public static Canvas CanvasForm;
		public static Timeline TimelineForm;
		public static Scenes ScenesForm;

		public static readonly string Version = "2.5.0.2";
		public static string loadFile = "";

		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length > 0)
				loadFile = args[0];

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainF());
		}
	}
}
