using System;
using System.IO;
using System.Windows.Forms;

namespace TISFAT
{
	static class Program
	{
		private static MainForm _MainForm;

		public static MainForm Form_Main => _MainForm; 
		public static TimelineForm Form_Timeline => Form_Main.Form_Timeline; 
		public static CanvasForm Form_Canvas => Form_Main.Form_Canvas;
		public static PropertiesForm Form_Properties => Form_Main.Form_Toolbox;

		public static Project ActiveProject => Form_Main.ActiveProject;
		public static Timeline MainTimeline => Form_Main.MainTimeline;

		public static string LoadFile = "";

#if !WIN64
		public static string TargetMsiName = "Tisfat.Zero-x86.msi";
#else
		public static string TargetMsiName = "Tisfat.Zero-x64.msi";
#endif

		public static bool updateScheduled = false;
		public static string updateUrl;


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
