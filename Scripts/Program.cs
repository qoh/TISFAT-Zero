using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TISFAT_ZERO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainF mf = new MainF();
            Application.Run(mf);
        }
    }
}
