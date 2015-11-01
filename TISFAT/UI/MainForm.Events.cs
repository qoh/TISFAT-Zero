using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TISFAT
{
	public partial class MainForm
	{
		#region Menu Items
		private void newToolStripMenuItem_Click(object sender, EventArgs e) => ProjectNew();

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.AddExtension = true;
			dialog.Filter = "TISFAT Zero Project|*.tzp";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				ProjectOpen(dialog.FileName);
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ProjectFileName != null)
				ProjectSave(ProjectFileName);
			else
				ProjectSaveAs();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) => ProjectSaveAs();

		private void exportToolStripMenuItem_Click(object sender, EventArgs e) => ProjectExport();

		private void importToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.AddExtension = true;
			dialog.Filter = "TISFAT Project|*.sif";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				TISFAT.Util.Legacy.FileFormat.Load(dialog.FileName);
			}
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e) => Undo();

		private void redoToolStripMenuItem_Click(object sender, EventArgs e) => Redo();

		private void skipToStartToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline.SeekFirstFrame();

		private void seekToEndToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline.SeekLastFrame();

		private void nextFrameToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline.SeekNextFrame();

		private void previousFrameToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline.SeekPrevFrame();

		private void insertKeyframeToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline.InsertKeyframe();

		private void removeKeyframeToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline.RemoveKeyframe();

		private void nextKeyframeToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline.NextKeyframe();

		private void previousKeyframeToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline.PrevKeyframe();

		private void renameLayerToolStripMenuItem_Click(object sender, EventArgs e) => Form_Timeline.renameToolStripMenuItem_Click(sender, e);

		private void moveUpToolStripMenuItem_Click(object sender, EventArgs e) => Form_Timeline.moveLayerUpToolStripMenuItem_Click(sender, e);

		private void moveLayerDownToolStripMenuItem_Click(object sender, EventArgs e) => Form_Timeline.moveLayerDownToolStripMenuItem_Click(sender, e);

		private void playAnimationToolStripMenuItem_Click(object sender, EventArgs e) => MainTimeline.TogglePause();

		private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e) => AutoUpdateDialog.CheckForUpdates(true);

		private void projectPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProjectPropertiesDialog dlg = new ProjectPropertiesDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			dlg.ShowDialog();
		}
		#endregion

		#region Top Menu
		private void btn_EditModeDefault_Click(object sender, EventArgs e) => ActiveEditMode = EditMode.Default;

		private void btn_EditModeOnion_Click(object sender, EventArgs e) => ActiveEditMode = EditMode.Onion;

		private void btn_EditModePhase_Click(object sender, EventArgs e) => ActiveEditMode = EditMode.Phase;

		private void btn_RemoveLayer_Click(object sender, EventArgs e) => Form_Timeline.MainTimeline.RemoveLayer();

		private void btn_AddLayer_Click(object sender, EventArgs e)
		{
			AddLayerDialog dlg = new AddLayerDialog();

			dlg.StartPosition = FormStartPosition.CenterParent;

			dlg.ShowDialog();
		}

		private void btn_Undo_Click(object sender, EventArgs e) => Undo();

		private void btn_Redo_Click(object sender, EventArgs e) => Redo();

		private void ckb_PreviewCamera_CheckedChanged(object sender, EventArgs e) => MainTimeline.GLContext.Invalidate();
		#endregion
	}
}
