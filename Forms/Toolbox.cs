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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            lbl_selectionDummy.Focus();
        }
	}
}