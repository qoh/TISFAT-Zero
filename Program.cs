using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace TISFAT_ZERO
{
    static class Program
    {
		public static readonly string Version = "1.1.0.0";
		public static string loadFile = "";

        [STAThread]
        static void Main(string[] args)
        {
			if (args.Length > 0)
			{
				string arg = args[0];
				if (arg == "update1")
				{
					if (File.Exists(args[1]))
						File.Delete(args[1]);
					File.Copy(Application.ExecutablePath, args[1]);
					Process x = new Process();
					x.StartInfo = new ProcessStartInfo(args[1], "update2 \"" + Path.GetFileName(Application.ExecutablePath) + "\"");
					x.Start();
					return;
				}
				else if (arg == "update2")
				{
					File.Delete(args[1]);
				}
				else
				{
					loadFile = arg;
				}
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainF());
        }
    }
}
