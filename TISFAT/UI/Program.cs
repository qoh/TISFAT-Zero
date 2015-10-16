using System;
using System.IO;
using System.Windows.Forms;

namespace TISFAT
{
	static class Program
	{
		private static MainForm _MainForm;

		public static MainForm Form_Main { get { return _MainForm; } }
		public static TimelineForm Form_Timeline { get { return Form_Main.Form_Timeline; } }
		public static CanvasForm Form_Canvas { get { return Form_Main.Form_Canvas; } }
		public static PropertiesForm Form_Properties { get { return Form_Main.Form_Toolbox; } }

		public static Project ActiveProject { get { return Form_Main.ActiveProject; } }
		public static Timeline MainTimeline { get { return Form_Main.MainTimeline; } }

		public static string LoadFile = "";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			_MainForm = new MainForm();

			if (args.Length > 0)
			{
				string file = args[0];

				if (File.Exists(file) && (Path.GetExtension(file) == ".tzp")) // or ".sif"
					LoadFile = file;
			}

			Application.Run(Form_Main);
		}
	}
}
