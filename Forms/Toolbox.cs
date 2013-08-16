using System.Windows.Forms;

namespace TISFAT_ZERO
{
	public partial class Toolbox : Form
	{
		public static MainF mainForm;

		public Toolbox(MainF f)
		{
			mainForm = f;
			InitializeComponent();
		}
	}
}