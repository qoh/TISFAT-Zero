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
		public static readonly string Version = "2.0.2.0";
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
