using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace TISFAT_ZERO
{
    static class Program
    {
		public static readonly string Version = "1.1.0.0";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainF());
        }
    }
}
