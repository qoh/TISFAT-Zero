using System;
using System.Windows.Forms;

namespace TISFAT
{
    static class Program
    {
		private static MainForm _MainForm;

        public static MainForm Form_Main { get { return _MainForm; } }
		public static TimelineForm Form_Timeline { get { return Form_Main.Form_Timeline; } }
		public static CanvasForm Form_Canvas { get { return Form_Main.Form_Canvas; } }

		public static Project ActiveProject { get { return Form_Main.ActiveProject; } }
		public static Timeline MainTimeline { get { return Form_Main.MainTimeline; } }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _MainForm = new MainForm();
            Application.Run(Form_Main);
        }
    }
}
