using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Drawing2D;

namespace TISFAT.Controls
{
	public class ImageListDropDownControl : ComboBox
	{
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

		public ImageListDropDownControl()
		{
			SetStyle(ControlStyles.Opaque | ControlStyles.UserPaint, true);

			DrawMode = DrawMode.OwnerDrawVariable;
			DropDownStyle = ComboBoxStyle.DropDownList;

			MeasureItem += ImageListDropDownControl_MeasureItem;
			DrawItem += ImageListDropDownControl_DrawItem;
		}

		private void ImageListDropDownControl_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = 25;

			this.DropDownHeight = (25 * 5) + 2;
		}

		private void ImageListDropDownControl_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();
			var index = e.Index;
			if (index < 0 || index >= Items.Count) return;
			using (var brush = new SolidBrush(e.ForeColor))
			{
				Rectangle rec = new Rectangle(e.Bounds.Left, e.Bounds.Top + ((e.Bounds.Height - ItemHeight) / 2), e.Bounds.Width, ItemHeight);
				e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, new SolidBrush(this.ForeColor), rec);
			}
			e.DrawFocusRectangle();
		}

		public void RefreshState()
		{
			//if (Down)
			//	BackgroundImage = _ImageDown;
			//else if (Hovered)
			//	BackgroundImage = _ImageHover;
			//else
			BackgroundImage = _ImageDefault;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);
			
			if (_ImageDefault == null)
			{
				if (DroppedDown)
					ButtonRenderer.DrawButton(e.Graphics, new Rectangle(ClientRectangle.X - 1, ClientRectangle.Y - 1, ClientRectangle.Width + 2, ClientRectangle.Height + 2), PushButtonState.Pressed);
				else
					ButtonRenderer.DrawButton(e.Graphics, new Rectangle(ClientRectangle.X - 1, ClientRectangle.Y - 1, ClientRectangle.Width + 2, ClientRectangle.Height + 2), PushButtonState.Normal);
			}

			if (_ImageDefault != null)
			{
				_ImageDefault.MakeTransparent();
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				e.Graphics.DrawImage(_ImageDefault, 0, 0, ClientSize.Width, ClientSize.Height);
			}
		}
	}
}
