using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TISFAT_Zero
{
	static class Program
	{
		public static MainF TheMainForm;
		public static Timeline TheTimeline;
		public static Canvas TheCanvas;
		//public static Toolbox TheToolbox;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			TheMainForm = new MainF();

			Application.Run(TheMainForm);
		}
	}
}
