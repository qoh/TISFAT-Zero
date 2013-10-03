using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TISFAT_ZERO
{
	public partial class Timeline : Form
	{

		#region Variables

		//This is the set of points used for drawing the black outline of the timeline layer
		private readonly Point[] p1 = new Point[] { new Point(79, 0), new Point(79, 15), new Point(0, 15) };
		private readonly Color layerColour = Color.FromArgb(70, 120, 255), tenthframe = Color.FromArgb(40, 230, 255);

		public static MainF mainForm;
		public Canvas theCanvas;

		//List of layers
		public static List<Layer> layers;
		public static int layer_cnt = 0;
		public static int layer_sel, frm_selPos, frm_selInd;

		public byte frm_selType;
		public KeyFrame frm_selected;

		private bool mouseDown = false, isPlaying = false;

		private List<string> layers_dispNames = new List<string>();
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Timeline"/> class.
		/// </summary>
		/// <param name="m">The main form object.</param>
		/// <param name="canvas">The canvas object.</param>
		public Timeline(MainF m, Canvas canvas)
		{
			InitializeComponent();
			mainForm = m;
			theCanvas = canvas;

			layers = new List<Layer>();

			addStickLayer("Stick Figure 1");

			frm_selPos = 0;
			layer_sel = 0;

			this.Refresh();
			setFrame();
		}

		#region Helper Methods

		/// <summary>
		/// Determines whether or not any of the layers at a given point in the timeline contain a frame.
		/// </summary>
		/// <param name="pos">The position to check.</param>
		/// <returns>Returns whether or not the given position has frames.</returns>
		private bool hasFrames(int pos)
		{
			//Check if it's within the keyset of any layer.
			//If it's within even one, then there is an active figure and it will return true.
			foreach (Layer k in layers)
			{
				if (pos <= k.lastKF && pos >= k.firstKF)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Determines whether or not any of the layers at the selected point on the timeline has a frame.
		/// </summary>
		/// <returns>Returns whether or not the selected position has frames.</returns>
		private bool hasFrames()
		{
			foreach (StickLayer k in layers)
			{
				if (frm_selPos <= k.lastKF && frm_selPos >= k.firstKF)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Sets the slected frame and updates all layers.
		/// </summary>
		/// <param name="pos">The position.</param>
		public void setFrame(int pos)
		{
			if(pos != frm_selPos)
				frm_selPos = pos;

			for (int a = 0; a < layers.Count; a++)
				if (a != layer_sel)
					layers[a].doDisplay(pos, false);

			if (layer_sel != -1)
			{
				layers[layer_sel].doDisplay(pos);
				Canvas.activeFigure = layers[layer_sel].fig;
			}

			if (frm_selected != null)
				mainForm.theToolbox.setColor(frm_selected.figColor);

			theCanvas.Refresh();
		}

		/// <summary>
		/// Updates all the layers based on the currently selected frame.
		/// </summary>
		public void setFrame()
		{
			setFrame(frm_selPos);
		}

		/// <summary>
		/// Returns the type of the selected frame.
		/// </summary>
		/// <returns>The type of the frame selected.
		/// 0: No frame
		/// 1: Middle Keyframe
		/// 2: First Keyframe of Frameset
		/// 3: Last Keyframe of Frameset
		/// 4: Tween frame
		/// 5: Timeline
		/// </returns>
		private byte selectedFrameType()
		{
			// The -1 layer is reserved for the Timeline.
			// If the user has selected the timeline, the selected layer will be -1.
			if (layer_sel == -1)
				return 5;

			//Get the currently selected layer (so that we can check it's frames)
			Layer selLayer = layers[layer_sel];

			//Check if the frame is on the first or last keyframe position
			// (Will have to be changed when framesets are implemented)
			if (frm_selPos == selLayer.firstKF)
			{
				frm_selected = selLayer.keyFrames[0];
				return 2;
			}
			else if (frm_selPos == selLayer.lastKF)
			{
				frm_selected = selLayer.keyFrames[selLayer.keyFrames.Count - 1];
				return 3;
			}

			//If the selected frame index has already been set (by the doDisplay method calls by setFrame) then it's a middle keyframe.
			if (frm_selInd != -1)
			{
				frm_selected = selLayer.keyFrames[frm_selInd];
				return 1;
			}

			//Now that all other possibilities are eliminated, it isn't a keyframe.
			//So because of this, we set the selected key frame to null.
			frm_selected = null;

			//This basically checks if the position is between the first and last keyframe positions.
			//This will also have to be changed when framesets are implemented.
			if (frm_selPos > selLayer.firstKF && frm_selPos < selLayer.lastKF)
				return 4;

			//Since it's failed all other tests, there is no frame at that spot.
			return 0;
		}

		/// <summary>
		/// Adds a stick figure layer to the project.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public StickLayer addStickLayer(string name)
		{
			StickFigure x = new StickFigure(false);

			StickLayer n = new StickLayer(name, x, theCanvas);
			layers.Add(n);

			layer_cnt++;

			setFrame(n.firstKF);
			return n;
		}

		/// <summary>
		/// Adds a stick line layer to the project.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public LineLayer addLineLayer(string name)
		{
			StickLine x = new StickLine(false);

			LineLayer n = new LineLayer(name, x, theCanvas);
			layers.Add(n);

			layer_cnt++;

			setFrame(n.firstKF);
			return n;
		}

		/// <summary>
		/// Adds a rectangle to the current project.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public RectLayer addRectLayer(string name)
		{
			StickRect x = new StickRect(false);

			RectLayer n = new RectLayer(name, x, theCanvas);
			layers.Add(n);

			layer_cnt++;

			setFrame(n.firstKF);
			return n;
		}

		/// <summary>
		/// Resets the timeline as if you restarted the program.
		/// </summary>
		/// <param name="keepDefaultLayer">if set to <c>true</c>, keep the default stick layer upon resetting.</param>
		public static void resetEverything(bool keepDefaultLayer)
		{
			layers = new List<Layer>();
			layer_cnt = layer_sel = 0; frm_selPos = 0;
			mainForm.resetTimeline(keepDefaultLayer);
		}


		/// <summary>
		/// Sets the layer display names, which accounts for overflow. (In which case it will make sure ... are the last 3 displayed characters)
		/// </summary>
		private void getLayerDisplayNames()
		{
			//layers_dispNames
		}

		#endregion Helper Methods

		/// <summary>
		/// Handles the Paint event of the Timeline control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
		private void Timeline_Paint(object sender, PaintEventArgs e)
		{

			#region Timeline Rendering

			//Create the graphics object then clear to the background colour of the majortiy of frames (light gray)
			Graphics g = e.Graphics;
			g.Clear(Color.FromArgb(220, 220, 220));

			//Create a black pen
			Pen gray = new Pen(Color.FromArgb(140, 140, 140)), blk = new Pen(Color.Black);

			//Calculate how many frames need to be drawn and what the offset is
			int frames = (mainForm.Width - 80) / 9;
			int scroll = mainForm.splitContainer1.Panel1.HorizontalScroll.Value;
			int offset = scroll / 9;

			//Grab the font we need to use to draw strings
			Font fo = DefaultFont;

			//draw the timeline layer
			g.FillRectangle(new SolidBrush(Color.FromArgb(70, 120, 255)), new Rectangle(0, 0, 79, layers.Count * 16 + 15));
			g.DrawLines(blk, p1);
			TextRenderer.DrawText(g, "T I M E L I N E", fo, new Point(1, 1), Color.Black);

			//Draw each layer
			for (int a = 1; a - 1 < layers.Count; a++)
			{
				g.DrawLines(blk, new Point[] { new Point(79, 16 * a - 1), new Point(79, 16 * a + 15), new Point(0, 16 * a + 15) });
				TextRenderer.DrawText(g, layers[a - 1].name, fo, new Point(1, 16 * a + 1), Color.Black);
			}

			g.FillRectangle(new SolidBrush(Color.FromArgb(200, 200, 200)), new Rectangle(80, 16 * layer_sel + 16, Width, 16));

			//Draw the timeline frames
			for (int a = offset; a - offset < frames; a++)
			{
				//Calculate where on the timeline we need to draw the frame
				int xx = (a - offset) * 9 + 80;

				//Default to cyan colour (10th frame colour)
				Color x = tenthframe;

				//If frame number is divisble by 100, set colour to red
				if ((a + 1) % 100 == 0)
				{
					x = Color.FromArgb(255, 200, 255);
				}

				//If the frame is not a special colour, don't fill it in (as it's already filled in with that colour)
				if ((a + 1) % 10 == 0)
					g.FillRectangle(new SolidBrush(x), xx, 0, 8, 16 * layers.Count + 15);

				//Write in the number
				TextRenderer.DrawText(g, "" + ((a + 1) % 10), fo, new Point(xx - 2, 1), Color.Black);
			}

			Height = layers.Count * 16 + 16;

			int sFrameLoc = ((int)frm_selPos - offset) * 9 + 80;

			//Draw all the frame outlines
			for (int a = 0, b = 88; a < frames; a++, b = 88 + 9 * a)
				g.DrawLine(gray, new Point(b, 0), new Point(b, Height));

			//This one has <= instead of < so that the line on the bottom of the last layer gets drawn
			for (int a = 0, b = 15; a <= layers.Count; a++, b = 16 * a + 15)
				g.DrawLine(gray, new Point(80, b), new Point(frames * 9 + 80, b));

			#endregion Timeline Rendering

			#region Layer Rendering

			//Fill in keyframes and such
			for (int a = 0; a < layers.Count; a++)
			{
				Layer l = layers[a];

				//Figure out the y axis of where we need to draw
				int y = a * 16 + 16;

				//Get the positions of the first and last keyframe
				int first = l.firstKF - offset;
				int last = l.lastKF - offset;


				int count = Math.Min(frames, last - first);

				int max = Math.Max(last, count + first);

				//Draw all the frames in the layer (I'll implement framesets later ok)
				for (int b = first, kind = 0; b <= max; b++)
				{
					Color x = Color.White;
					if (l.keyFrames[kind].pos == b + offset)
					{
						kind++;
						x = Color.Gold;
					}

					if (b < 0)
						continue;

					g.FillRectangle(new SolidBrush(x), b * 9 + 80, y, 8, 15);
				}
			}

			if (frm_selPos >= offset && frm_selPos - offset < frames)
			{
				if (layer_sel != -1)
					g.FillRectangle(new SolidBrush(Color.Red), sFrameLoc, layer_sel * 16 + 16, 8, 15);
				else
				{
					int x9 = frm_selPos * 9 + 79 - offset * 9;
					Pen pn = new Pen(new SolidBrush(Color.Red));
					g.DrawLines(pn, new Point[] { new Point(x9, 0), new Point(x9 + 9, 0), new Point(x9 + 9, 15), new Point(x9, 15), new Point(x9, 1) });
					g.DrawLines(pn, new Point[] { new Point(x9+1, 1), new Point(x9 + 8, 1), new Point(x9 + 8, 14), new Point(x9 + 1, 14), new Point(x9 + 1, 1) });
					g.DrawLine(pn, new Point(x9 + 4, 16), new Point(x9 + 4, layer_cnt * 16 + 32));
					g.DrawLine(pn, new Point(x9 + 5, 16), new Point(x9 + 5, layer_cnt * 16 + 32));
					pn.Dispose();
				}
			}

			#endregion Layer Rendering

			//Dispose of the pens (because apparently this is necessary)
			gray.Dispose(); blk.Dispose();
		}

		#region Callback Events

		/// <summary>
		/// Handles the MouseDown event of the Timeline control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void Timeline_MouseDown(object sender, MouseEventArgs e)
		{
			if (isPlaying)
				return;

			int x = e.X, y = e.Y;

			if (x > 80)
			{
				frm_selPos = (x - 80) / 9 + mainForm.splitContainer1.Panel1.HorizontalScroll.Value / 9;

				if (y < 16)
					layer_sel = -1;
				else if (y < layers.Count() * 16 + 16)
					layer_sel = (y - 16) / 16;

				setFrame();
				frm_selType = selectedFrameType();

				if (e.Button == MouseButtons.Left)
					mouseDown = true;
				if (frm_selected != null)
					mainForm.theToolbox.setColor(frm_selected.figColor);

				Refresh();
			}
			else
			{
				if (y < layers.Count() * 16 + 16)
				{
					RenameLayer f = new RenameLayer((y - 16) / 16);
					f.ShowDialog();
					Refresh();
				}

			}
		}

		/// <summary>
		/// Handles the MouseMove event of the Timeline control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
		private void Timeline_MouseMove(object sender, MouseEventArgs e)
		{
			//Don't do anything if we're playing back an animation or we don't have the mouse down.
			if (!mouseDown || isPlaying)
				return;

			int x = e.X;

			//Do the selected frame calculation twice, once in both int and uint.
			int newSelected = (x - 80) / 9 + mainForm.splitContainer1.Panel1.HorizontalScroll.Value / 9;

			//If the int calculation is below zero, then set the selected value to 0 (as we don't want anything going negative)
			if (newSelected < 0)
				newSelected = 0;

			//If the timeline is selected, simply update the selected frame and refresh everything.
			if (layer_sel == -1)
			{
				frm_selPos = newSelected;
				setFrame();
				Refresh();
				return;
			}

			//Get the difference in the selected frame before and after the mouse move update.
			int diff = newSelected - frm_selPos;

			//This function basically handles anything having to do with dragging things on the timeline.

			//Everything on from here relies on the mouse having moved at least 1 frame, so if it hasn't or there is
			//no keyframe selected, then we can just stop here.
			if (diff == 0 || frm_selType == 0)
				return;

			//Get the currently selected layer (Note to self: make this into a property of the timeline for easy access)
			Layer l = layers[layer_sel];

			//And get the list of keyframes from the layer.
			List<KeyFrame> kfs = l.keyFrames;

			//Behaviour for dragging keyframes
			if (frm_selType > 0 && frm_selType < 4)
			{
				// Note to Self: Remove constraints on dradding keyframes. Penis

				//This code makes sure that the selected frame doesn't go over/under the positions of it's neighbouring frames
				if (frm_selected.pos == l.lastKF)
				{
					newSelected = Math.Max(newSelected, kfs[kfs.Count - 2].pos + 1);
					l.lastKF = newSelected;
				}
				else if (frm_selected.pos == l.firstKF)
				{
					newSelected = Math.Min(newSelected, kfs[1].pos - 1);
					l.firstKF = newSelected;
				}
				else
				{
					int backPos = kfs[frm_selInd - 1].pos, frontPos = kfs[frm_selInd + 1].pos;

					newSelected = Math.Max(Math.Min(newSelected, frontPos - 1), backPos + 1);
				}

				frm_selected.pos = newSelected;
				frm_selPos = newSelected;
				Refresh();
				return;
			}
			else if (frm_selType == 4)
			{
				if (diff < 0 && l.firstKF == 0)
					return;

				if (diff < 0 && l.firstKF < -1 * diff)
				{
					diff = -1 * l.firstKF; //To make sure you can't make things go negative
				}

				if (diff == 0)
				{
					return;
				}
				else
				{
					l.firstKF += diff;
					l.lastKF += diff;

					for (int a = 0; a < l.keyFrames.Count; a++)
						l.keyFrames[a].pos += diff;
				}

				frm_selPos = newSelected;
				Refresh();
			}
		}

		/// <summary>
		/// Handles the MouseUp event of the Timeline control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void Timeline_MouseUp(object sender, MouseEventArgs e)
		{
			mouseDown = false;
		}

		/// <summary>
		/// Handles the Opening event of the timeline right click menu.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
		private void cxt_Menu_Opening(object sender, CancelEventArgs e)
		{
			//To be honest I don't even know if this is necessary, but I put it in just in case.
			//If the timeline is playing, then don't allow the right click menu.
			if (isPlaying)
			{
				e.Cancel = true;
				return;
			}

			//Get the selected keyframe type
			int frameType = selectedFrameType();

			tst_insertKeyframe.Enabled = frameType == 4;
			tst_insertKeyframeAtPose.Enabled = frameType == 4;
			tst_removeKeyframe.Enabled = frameType == 1;
			tst_setPosePrvKfrm.Enabled = frameType == 1 || frameType == 3;
			tst_setPosePrvKfrm.Enabled = frameType == 1 || frameType == 2;
			tst_onionSkinning.Enabled = frameType != 0 | frameType != 4;

			tst_insertFrameset.Enabled = frameType == 0;
			tst_removeFrameset.Enabled = frameType != 0;

			tst_moveLayerUp.Enabled = true;
			tst_moveLayerDown.Enabled = true;
			tst_insertLayer.Enabled = true;
			tst_removeLayer.Enabled = layer_cnt > 1;

			tst_keyFrameAction.Enabled = frameType != 0 | frameType != 4;

			tst_hideLayer.Enabled = true;
			tst_showLayer.Enabled = true;

			tst_gotoFrame.Enabled = true;
		}

		/// <summary>
		/// Handler for when an item in the timeline ricght click menu is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
		private void cxt_Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			//Get the name of the clicked item and the currently selected layer
			string name = e.ClickedItem.Name;
			Layer cLayer = layers[layer_sel];

			//Decide what to do based on the clicked menu item was.
			switch (name)
			{
				//Layer methods handle this part.
				case "tst_insertKeyframe":
					cLayer.insertKeyFrame(frm_selPos);
					selectedFrameType();
					break;

				case "tst_removeKeyframe":
					cLayer.removeKeyFrame(frm_selPos);
					break;

				//Insert a keyframe like before and then copy the positions of the
				//current tween fig into the new keyframe.
				case "tst_insertKeyframeAtPose":
					int p = cLayer.insertKeyFrame(frm_selPos);
					KeyFrame newFrame = cLayer.keyFrames[p];
					List<StickJoint> jointz = cLayer.tweenFig.Joints;

					for (int a = 0; a < jointz.Count; a++)
						newFrame.Joints[a].location = jointz[a].location;

					break;

				case "tst_setPosePrvKfrm":
					int pos = cLayer.keyFrames.IndexOf(frm_selected);

					for (int a = 0; a < cLayer.keyFrames[pos].Joints.Count; a++)
						cLayer.keyFrames[pos].Joints[a].location = new Point(cLayer.keyFrames[pos - 1].Joints[a].location.X, cLayer.keyFrames[pos - 1].Joints[a].location.Y);

					break;

				case "tst_setPoseNxtKfrm":
					pos = cLayer.keyFrames.IndexOf(frm_selected);

					for (int a = 0; a < cLayer.keyFrames[pos].Joints.Count; a++)
						cLayer.keyFrames[pos].Joints[a].location = new Point(cLayer.keyFrames[pos + 1].Joints[a].location.X, cLayer.keyFrames[pos + 1].Joints[a].location.Y);

					break;

				case "tst_removeLayer":
					Layer toRemove = layers[layer_sel];

					Canvas.figureList.Remove(toRemove.fig);
					Canvas.tweenFigs.Remove(toRemove.tweenFig);

					layers.RemoveAt(Timeline.layer_sel);
					layer_cnt--;

					//Prevent having a non-existant layer selected after deleting
					if (layer_sel == layer_cnt)
						layer_sel--;

					Refresh();

					break;

				default:
					return;

			}

			//After that, refresh the timeline and the canvas.
			Refresh();
			setFrame();
		}

		/// <summary>
		/// Handles the Tick event of the playTimer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void playTimer_Tick(object sender, EventArgs e)
		{
			if (hasFrames(frm_selPos + 1))
			{
				frm_selPos++;
				setFrame();
				Refresh();
			}
			else
			{
				playTimer.Stop();
				mainForm.theToolbox.btn_playPause.Text = "Play";
				mainForm.theToolbox.isPlaying = isPlaying = false;
			}

		}

		#endregion Callback Events

		#region Timer methods

		/// <summary>
		/// Starts the timer.
		/// </summary>
		/// <param name="fps">The FPS.</param>
		public void startTimer(byte fps)
		{
			int mspertick = 16;
			playTimer.Interval = mspertick;

			layer_sel = -1; //the -1 layer is the timeline 'layer'
			isPlaying = true;
			playTimer.Start();
		}

		/// <summary>
		/// Stops the timer.
		/// </summary>
		public void stopTimer()
		{
			isPlaying = false;
			playTimer.Stop();
		}

		#endregion Timer Methods

	}
}
