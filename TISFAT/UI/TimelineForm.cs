using System;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT
{
	public partial class TimelineForm : Form
	{
		#region Properties
		public Timeline MainTimeline;

		public int HScrollVal => scrl_HTimeline.Value;

		public bool HScrollVisible => scrl_HTimeline.Visible; 

		public int VScrollVal => scrl_VTimeline.Value; 

		public bool VScrollVisible => scrl_VTimeline.Visible; 
		#endregion

		public TimelineForm(Control parent)
		{
			InitializeComponent();

			GLContext.VSync = true;
			GLContext.KeyDown += TimelineForm_KeyDown;
			GLContext.MouseWheel += GLContext_MouseWheel;

			MainTimeline = new Timeline(GLContext);

			GLContext.Paint += MainTimeline.GLContext_Paint;

			TopLevel = false;
			parent.Controls.Add(this);
		}

		public void ShowFrameCxtMenu(Point Location, int FrameType, int FrameIndex)
		{
			// FrameTypes
			// 0 - Null Frame
			// 1 - Blank Frame
			// 2 - Key Frame

			// TODO: Fix this mess eventually

			insertKeyframeToolStripMenuItem.Visible = FrameType == 1 || FrameType == 2;
			insertKeyframeToolStripMenuItem.Enabled = FrameType == 1;

			removeKeyframeToolStripMenuItem.Visible = FrameType == 1 || FrameType == 2;
			removeKeyframeToolStripMenuItem.Enabled = !insertKeyframeToolStripMenuItem.Enabled;

			toolStripSeparator1.Visible = FrameType == 2;
			changeInterpolationModeToolStripMenuItem.Visible = FrameType == 2;

			if (FrameType == 2)
			{
				noneToolStripMenuItem.Checked = MainTimeline.SelectedKeyframe.InterpMode == Util.EntityInterpolationMode.None;
				linearToolStripMenuItem.Checked = MainTimeline.SelectedKeyframe.InterpMode == Util.EntityInterpolationMode.Linear;
				quadInOutToolStripMenuItem.Checked = MainTimeline.SelectedKeyframe.InterpMode == Util.EntityInterpolationMode.QuadInOut;
				expoInOutToolStripMenuItem.Checked = MainTimeline.SelectedKeyframe.InterpMode == Util.EntityInterpolationMode.ExpoInOut;
				bounceOutToolStripMenuItem.Checked = MainTimeline.SelectedKeyframe.InterpMode == Util.EntityInterpolationMode.BounceOut;
				backOutToolStripMenuItem.Checked = MainTimeline.SelectedKeyframe.InterpMode == Util.EntityInterpolationMode.BackOut;
			}

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
			insertFramesetToolStripMenuItem.Enabled = Program.MainTimeline.CanInsertFrameset();

			removeFramesetToolStripMenuItem.Visible = FrameType == 1 || FrameType == 2;
			removeFramesetToolStripMenuItem.Enabled = MainTimeline.SelectedLayer.Framesets.Count > 1;

			toolStripSeparator4.Visible = FrameType != 0;

			moveLayerUpToolStripMenuItem.Enabled = Program.ActiveProject.Layers.IndexOf(MainTimeline.SelectedLayer) > 1;
			moveLayerDownToolStripMenuItem.Enabled = Program.ActiveProject.Layers.IndexOf(MainTimeline.SelectedLayer) < Program.ActiveProject.Layers.Count - 1;
			moveLayerDownToolStripMenuItem.Enabled = MainTimeline.SelectedLayer.Data.GetType() != typeof(Camera);

			removeLayerToolStripMenuItem.Enabled = MainTimeline.SelectedLayer.Data.GetType() != typeof(Camera);
            removeLayerToolStripMenuItem.Visible = MainTimeline.SelectedLayer.Data.GetType() != typeof(Camera);

			cxtm_Timeline.Show(GLContext, Location);
		}

		public void ShowLayerCxtMenu(Point Location, int LayerIndex)
		{
			cxtm_Labels.Show(GLContext, Location);
		}

		#region GLContext <-> Timeline Hooks
		private void GLContext_MouseMove(object sender, MouseEventArgs e) => MainTimeline?.MouseMoved(e.Location);

		private void GLContext_MouseLeave(object sender, EventArgs e) => MainTimeline?.MouseLeft();

		private void GLContext_MouseDown(object sender, MouseEventArgs e) => MainTimeline?.MouseDown(e.Location, e.Button);

		private void GLContext_MouseUp(object sender, MouseEventArgs e) => MainTimeline?.MouseUp(e.Location, e.Button);

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

		private void scrl_Timeline_Scroll(object sender, ScrollEventArgs e) => MainTimeline?.GLContext.Invalidate();
		#endregion

		#region Playback Control Hooks
		public void btn_PlayPause_Click(object sender, EventArgs e) => MainTimeline?.TogglePause();

		private void btn_FastForward_Click(object sender, EventArgs e) => MainTimeline?.NextKeyframe();

		private void btn_Rewind_Click(object sender, EventArgs e) => MainTimeline?.PrevKeyframe();

		private void btn_SeekStart_Click(object sender, EventArgs e) => MainTimeline?.SeekFirstFrame();

		private void btn_SeekEnd_Click(object sender, EventArgs e) => MainTimeline?.SeekLastFrame();
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

		private void TimelineForm_Resize(object sender, EventArgs e) => MainTimeline?.Resize();

		private void TimelineForm_Enter(object sender, EventArgs e) => BringToFront();
		#endregion

		#region Context Menu Hooks
		private void insertKeyframeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline != null)
				MainTimeline.InsertKeyframe();

			cxtm_Timeline.Close();
		}

		private void removeKeyframeToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.RemoveKeyframe();

		private void insertFramesetToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.InsertFrameset();

		private void removeFramesetToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.RemoveFrameset();

		public void moveLayerUpToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.MoveLayerUp();

		public void moveLayerDownToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.MoveLayerDown();

		private void removeLayerToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.RemoveLayer();

		private void setPoseToPreviousToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.SetPoseToPrevious();

		private void setPoseToNextToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.SetPoseToNext();

		private void noneToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.ChangeInterpolationMode(Util.EntityInterpolationMode.None);

		private void linearToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.ChangeInterpolationMode(Util.EntityInterpolationMode.Linear);

		private void quadInOutToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.ChangeInterpolationMode(Util.EntityInterpolationMode.QuadInOut);

		private void bounceOutToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.ChangeInterpolationMode(Util.EntityInterpolationMode.BounceOut);

		private void backOutToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.ChangeInterpolationMode(Util.EntityInterpolationMode.BackOut);

		private void expoInOutToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.ChangeInterpolationMode(Util.EntityInterpolationMode.ExpoInOut);

		public void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MainTimeline.SelectedLayer != null)
			{
				RenameLayerDialog dlg = new RenameLayerDialog();

				dlg.StartPosition = FormStartPosition.CenterParent;

				if (dlg.ShowDialog() == DialogResult.OK)
					MainTimeline.RenameLayer(dlg.ReturnText);
			}
		}

		private void addToGroupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddLayerGroupDialog dlg = new AddLayerGroupDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			if (dlg.ShowDialog() == DialogResult.OK)
				MainTimeline.AddLayerGroup(dlg.ReturnText);
		}

		private void withInterpolatedStateToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline?.InsertKeyframe(true);
		#endregion

		private void TimelineForm_KeyDown(object sender, KeyEventArgs e)
		{
			// Doesn't really do much anymore. Not removing it just yet though
		}
	}
}
