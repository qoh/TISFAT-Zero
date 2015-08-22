using System;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT
{
    public partial class TimelineForm : Form
	{
		#region Properties
		public Timeline MainTimeline;

		public int HScrollVal
		{
			get { return scrl_HTimeline.Value; }
		}

		public bool HScrollVisible { get { return scrl_HTimeline.Visible; } }

		public int VScrollVal
		{
			get { return scrl_VTimeline.Value; }
		}
		public bool VScrollVisible { get { return scrl_VTimeline.Visible; } } 
		#endregion

		public TimelineForm(Control parent)
		{
			InitializeComponent();

			GLContext.VSync = true;
			GLContext.KeyPress += TimelineForm_KeyPress;
			GLContext.MouseWheel += GLContext_MouseWheel;

			MainTimeline = new Timeline(GLContext);

			GLContext.Paint += MainTimeline.GLContext_Paint;

			TopLevel = false;
			parent.Controls.Add(this);
		}

		public void ShowCxtMenu(Point Location, int FrameType, int FrameIndex)
		{
			// FrameTypes
			// 0 - Null Frame
			// 1 - Blank Frame
			// 2 - Key Frame

			insertKeyframeToolStripMenuItem.Visible = FrameType == 1 || FrameType == 2;
			insertKeyframeToolStripMenuItem.Enabled = FrameType == 1;

			removeKeyframeToolStripMenuItem.Visible = FrameType == 1 || FrameType == 2;
			removeKeyframeToolStripMenuItem.Enabled = !insertKeyframeToolStripMenuItem.Enabled;
			if (MainTimeline.SelectedFrameset != null && FrameType == 2)
				removeKeyframeToolStripMenuItem.Enabled = FrameIndex != MainTimeline.SelectedFrameset.StartTime && FrameIndex != MainTimeline.SelectedFrameset.EndTime;

			setPoseToNextToolStripMenuItem.Visible = FrameType == 2;
			setPoseToPreviousToolStripMenuItem.Visible = FrameType == 2;
			if (MainTimeline.SelectedFrameset != null && FrameType == 2)
			{
				setPoseToNextToolStripMenuItem.Enabled = FrameIndex != MainTimeline.SelectedFrameset.EndTime;
				setPoseToPreviousToolStripMenuItem.Enabled = FrameIndex != MainTimeline.SelectedFrameset.StartTime;
			}


			insertFramesetToolStripMenuItem.Visible = FrameType == 0;
			// TODO: Disable insert frameset if there's not enough space to create a new frameset

			removeFramesetToolStripMenuItem.Visible = FrameType == 1 || FrameType == 2;
			removeFramesetToolStripMenuItem.Enabled = MainTimeline.SelectedLayer.Framesets.Count > 1;

			toolStripSeparator4.Visible = FrameType != 0;

			moveLayerUpToolStripMenuItem.Enabled = Program.Form.ActiveProject.Layers.IndexOf(MainTimeline.SelectedLayer) > 0;
			moveLayerDownToolStripMenuItem.Enabled = Program.Form.ActiveProject.Layers.IndexOf(MainTimeline.SelectedLayer) < Program.Form.ActiveProject.Layers.Count;

			cxtm_Timeline.Show(GLContext, Location);
		}

		#region GLContext <-> Timeline Hooks

		private void TimelineForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Q)
				MainTimeline.SeekStart();

			if (e.KeyChar == (char)Keys.Space)
			{
				btn_PlayPause.Checked = !btn_PlayPause.Checked;
				btn_PlayPause_Click(null, null);
			}
		}

		private void GLContext_MouseMove(object sender, MouseEventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.MouseMoved(e.Location);
		}

		private void GLContext_MouseLeave(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.MouseLeft();
		}

		private void GLContext_MouseDown(object sender, MouseEventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.MouseDown(e.Location, e.Button);
		}

		private void GLContext_MouseUp(object sender, MouseEventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.MouseUp(e.Location, e.Button);
		}

		private void GLContext_MouseWheel(object sender, MouseEventArgs e)
		{
			ScrollBar bar = scrl_VTimeline;

			if (ModifierKeys == Keys.Shift)
				bar = scrl_HTimeline;

			if (!bar.Visible)
				return;

			int scrollAmount = bar.SmallChange * -(e.Delta / 100);

			if (bar.Value + scrollAmount > 1 + bar.Maximum - bar.LargeChange)
				bar.Value = 1 + bar.Maximum - bar.LargeChange;
			else if (bar.Value + scrollAmount < bar.Minimum)
				bar.Value = bar.Minimum;
			else
				bar.Value += scrollAmount;

			MainTimeline.GLContext.Invalidate();
		}
		#endregion

		#region Timeline Scroll Logic
		public void CalcScrollBars(int HContentSize, int VContentSize, int HViewSize, int VViewSize)
		{
			scrl_HTimeline.Visible = HViewSize < HContentSize;
			scrl_VTimeline.Visible = VViewSize < VContentSize;

			if (scrl_HTimeline.Visible)
			{
				scrl_HTimeline.Minimum = 0;

				scrl_HTimeline.SmallChange = HViewSize / 10;
				scrl_HTimeline.LargeChange = HViewSize / 5;

				scrl_HTimeline.Maximum = HContentSize - HViewSize;
				scrl_HTimeline.Maximum += scrl_HTimeline.LargeChange;
			}
			if (scrl_VTimeline.Visible)
			{
				scrl_VTimeline.Minimum = 0;

				scrl_VTimeline.SmallChange = VViewSize / 10;
				scrl_VTimeline.LargeChange = VViewSize / 5;

				scrl_VTimeline.Maximum = VContentSize - VViewSize;
				scrl_VTimeline.Maximum += scrl_VTimeline.LargeChange;
			}

			if (!scrl_HTimeline.Visible)
				scrl_HTimeline.Value = 0;
			if (!scrl_VTimeline.Visible)
				scrl_VTimeline.Value = 0;

			pnl_ScrollSquare.Visible = scrl_HTimeline.Visible || scrl_VTimeline.Visible;
		}

		private void scrl_Timeline_Scroll(object sender, ScrollEventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.GLContext.Invalidate();
		}
		#endregion

		#region Playback Control Hooks
		private void btn_PlayPause_Click(object sender, EventArgs e)
		{
			MainTimeline.TogglePause();
		}

		private void btn_SeekStart_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.SeekFirstFrame();
		}

		private void btn_SeekEnd_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.SeekLastFrame();
		}

		#endregion

			#region Form Events
		private void TimelineForm_Load(object sender, EventArgs e)
		{
			if (MainTimeline != null)
			{
				MainTimeline.GLContext_Init();
				MainTimeline.Resize();
			}
		}

		private void TimelineForm_Resize(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.Resize();
		}

		private void TimelineForm_Enter(object sender, EventArgs e)
		{
			BringToFront();
		}
		#endregion

		#region Context Menu Hooks
		private void insertKeyframeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.InsertKeyframe();
		}

		private void removeKeyframeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.RemoveKeyframe();
		}

		private void insertFramesetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.InsertFrameset();
		}

		private void removeFramesetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.RemoveFrameset();
		}

		private void moveLayerUpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.MoveLayerUp();
		}

		private void moveLayerDownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.MoveLayerDown();
		}

		private void removeLayerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.RemoveLayer();
		}
		#endregion

		private void setPoseToPreviousToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.SetPoseToPrevious();
		}

		private void setPoseToNextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.SetPoseToNext();
		}
	}
}
