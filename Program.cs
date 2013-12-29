﻿using System;
using System.Windows.Forms;

namespace TISFAT_Zero
{
	internal static class Program
	{
		public static MainF TheMainForm;
		public static Timeline TheTimeline;
		public static Canvas TheCanvas;
		public static Toolbox TheToolbox;

		public static readonly string Version = "3.0.0.0";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			TheMainForm = new MainF();

			Application.Run(TheMainForm);
		}
	}
}