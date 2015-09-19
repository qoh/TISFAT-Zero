using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT.Controls
{
	public partial class BitmapButtonControl : UserControl
	{
		#region Designer Properties
		private Bitmap _ImageDefault;
		[Description("Image displayed when the user isn't interacting with the control."), Category("Appearance")]
		public Bitmap ImageDefault
		{
			get { return _ImageDefault; }
			set { _ImageDefault = value; RefreshState(); }
		}

		private Bitmap _ImageHover;
		[Description("Image displayed when the user hovers over the control."), Category("Appearance")]
		public Bitmap ImageHover
		{
			get { return _ImageHover; }
			set { _ImageHover = value; RefreshState(); }
		}

		private Bitmap _ImageDown;
		[Description("Image displayed when the user is clicking down on the control."), Category("Appearance")]
		public Bitmap ImageDown
		{
			get { return _ImageDown; }
			set { _ImageDown = value; RefreshState(); }
		}

		private Bitmap _ImageOn;
		[Description("Image displayed when the button is toggled on (if applicable)."), Category("Appearance")]
		public Bitmap ImageOn
		{
			get { return _ImageOn; }
			set { _ImageOn = value; RefreshState(); }
		}

		private Bitmap _ImageOnHover;
		[Description("Image displayed when the button is toggled on (if applicable) and the user is hovering over the control."), Category("Appearance")]
		public Bitmap ImageOnHover
		{
			get { return _ImageOnHover; }
			set { _ImageOnHover = value; RefreshState(); }
		}

		private Bitmap _ImageOnDown;
		[Description("Image displayed when the button is toggled on (if applicable) and the user is clicking down on the control."), Category("Appearance")]
		public Bitmap ImageOnDown
		{
			get { return _ImageOnDown; }
			set { _ImageOnDown = value; RefreshState(); }
		}

		private bool _ToggleButton;
		[Description("States whether the button can be toggled"), Category("Behavior")]
		public bool ToggleButton
		{
			get { return _ToggleButton; }
			set { _ToggleButton = value; RefreshState(); }
		}

		private bool _Checked;
		[Description("Gets or sets the enabled state of the button control"), Category("Appearance")]
		public bool Checked
		{
			get { return _Checked; }
			set { _Checked = value; RefreshState(); }
		}
		#endregion

		[Description("Occurs when the button on the UserControl is clicked")]
		public new event EventHandler Click;

		public bool Hovered = false;
		public bool Down = false;

		public BitmapButtonControl()
		{
			InitializeComponent();

			btn_MainButton.BackgroundImageLayout = ImageLayout.Zoom;
		}

		public void RefreshState()
		{
			btn_MainButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
			btn_MainButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
			btn_MainButton.FlatAppearance.BorderSize = 0;
			btn_MainButton.BackColor = Color.FromKnownColor(KnownColor.Control);

			if (Down)
				btn_MainButton.BackgroundImage = Checked ? _ImageOnDown : _ImageDown;
			else if (Hovered)
				btn_MainButton.BackgroundImage = Checked ? _ImageOnHover : _ImageHover;
			else
				btn_MainButton.BackgroundImage = Checked ? _ImageOn : _ImageDefault;

			if (_ImageDown == null && _ImageHover == null)
			{
				btn_MainButton.FlatAppearance.BorderColor = Color.FromArgb(55, 155, 255);

				if (Hovered || Down || Checked)
					btn_MainButton.FlatAppearance.BorderSize = 1;

				btn_MainButton.BackgroundImage = _ImageDefault;
				btn_MainButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 233, 249);
				btn_MainButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(127, 191, 255);

				if (Checked)
					btn_MainButton.BackColor = Color.FromArgb(127, 191, 255);
			}
		}

		private void btn_MainButton_MouseEnter(object sender, EventArgs e)
		{
			Hovered = true;
			RefreshState();
		}

		private void btn_MainButton_MouseLeave(object sender, EventArgs e)
		{
			Hovered = false;
			RefreshState();
		}

		private void btn_MainButton_MouseDown(object sender, MouseEventArgs e)
		{
			Down = true;
			RefreshState();
		}

		private void btn_MainButton_MouseUp(object sender, MouseEventArgs e)
		{
			Down = false;
			RefreshState();
		}

		private void btn_MainButton_Click(object sender, EventArgs e)
		{
			if (ToggleButton)
			{
				Checked = !Checked;
				RefreshState();
			}

			if (Click != null)
				Click(sender, e);
		}
	}
}
