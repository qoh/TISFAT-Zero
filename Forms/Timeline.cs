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
		private Point[] p1 = new Point[] { new Point(79, 0), new Point(79, 15), new Point(0, 15) };

		public static MainF mainForm;
		public Canvas theCanvas;

		//List of layers
		public static List<Layer> layers;
		public static int layercount = 0;
		public static uint selectedFrame;
		public static int selectedLayer;
		public byte selectedType;
		public KeyFrame selectedKeyFrame;

		private bool mouseDown = false, isPlaying = false;
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
			for (int a = 0; a < 1; a++)
				addStickLayer("Stick Figure " + (a + 1));
			selectedFrame = 0;
			selectedLayer = 0;
			this.Refresh();
			setFrame();
		}

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
			Pen blk = new Pen(Color.FromArgb(140, 140, 140)), bblk = new Pen(Color.Black);

			//Calculate how many frames need to be drawn and what the offset is
			int frames = (mainForm.Width - 80) / 9;
			int scroll = mainForm.splitContainer1.Panel1.HorizontalScroll.Value;
			int offset = scroll / 9;

			//Grab the font we need to use to draw strings
			Font fo = SystemFonts.DefaultFont;

			//draw the timeline layer
			g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(0, 0, 79, layers.Count * 16 + 15));
			g.DrawLines(bblk, p1);
			g.DrawString("T I M E L I N E", fo, new SolidBrush(Color.Black), 1, 1.5f);

			//Draw each layer
			for (int a = 1; a - 1 < layers.Count; a++)
			{
				g.DrawLines(bblk, new Point[] { new Point(79, 16 * a - 1), new Point(79, 16 * a + 15), new Point(0, 16 * a + 15) });
				g.DrawString(layers[a - 1].name, fo, new SolidBrush(Color.Black), 1, 16 * a + 0.4f);
			}

			g.FillRectangle(new SolidBrush(Color.FromArgb(200, 200, 200)), new Rectangle(80, 16 * selectedLayer + 16, Width, 16));

			//Draw the timeline frames
			for (int a = offset; a - offset < frames; a++)
			{
				//Calculate where on the timeline we need to draw the frame
				int xx = (a - offset) * 9 + 80;

				//Default to cyan colour (10th frame colour)
				Color x = Color.Cyan;

				//If frame number is divisble by 100, set colour to red
				if ((a + 1) % 100 == 0)
				{
					x = Color.FromArgb(255, 200, 255);
					if (a == selectedFrame && selectedLayer == -1)
						x = Color.Cyan;
				}

				//If the frame is not a special colour, don't fill it in (as it's already filled in with that colour)
				if ((a + 1) % 10 == 0 || (a == selectedFrame && selectedLayer == -1))
					g.FillRectangle(new SolidBrush(x), xx, 0, 8, 16 * layers.Count + 15);

				//Write in the number
				g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx - 1, 1));
			}

			Height = layers.Count * 16 + 16;

			int sFrameLoc = ((int)selectedFrame - offset) * 9 + 80;

			//Draw all the frame outlines
			for (int a = 0, b = 88; a < frames; a++, b = 88 + 9 * a)
				g.DrawLine(blk, new Point(b, 0), new Point(b, Height));

			//This one has <= instead of < so that the line on the bottom of the last layer gets drawn
			for (int a = 0, b = 15; a <= layers.Count; a++, b = 16 * a + 15)
				g.DrawLine(blk, new Point(80, b), new Point(frames * 9 + 80, b));

			#endregion Timeline Rendering

			#region Layer Rendering

			//Fill in keyframes and such
			for (int a = 0; a < layers.Count; a++)
			{
				Layer l = layers[a];

				//Figure out the y axis of where we need to draw
				int y = a * 16 + 16;

				//Get the positions of the first and last keyframe
				int first = (int)l.firstKF - offset;
				int last = (int)l.lastKF - offset;


				int count = (int)Math.Min(frames, last - first);

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

					if (b < 0 || (selectedLayer == -1 && b + offset == selectedFrame))
						continue;

					g.FillRectangle(new SolidBrush(x), b * 9 + 80, y, 8, 15);
				}
			}

			if (selectedFrame >= offset && selectedFrame - offset < frames && selectedLayer != -1)
			{
				g.FillRectangle(new SolidBrush(Color.Red), sFrameLoc, selectedLayer * 16 + 16, 8, 15);
			}

			#endregion Layer Rendering

			//Dispose of the pens (because apparently this is necessary)
			blk.Dispose(); bblk.Dispose();
		}

		/// <summary>
		/// Adds the stick layer.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public StickLayer addStickLayer(string name)
		{
			StickFigure x = new StickFigure(false);

			StickLayer n = new StickLayer(name, x, theCanvas);
			layers.Add(n);

			setFrame(n.firstKF);
			return n;
		}

		/// <summary>
		/// Adds the line layer.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public LineLayer addLineLayer(string name)
		{
			StickLine x = new StickLine(false);

			LineLayer n = new LineLayer(name, x, theCanvas);
			layers.Add(n);

			setFrame(n.firstKF);
			return n;
		}

		/// <summary>
		/// Adds the rect layer.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public RectLayer addRectLayer(string name)
		{
			StickRect x = new StickRect(false);

			RectLayer n = new RectLayer(name, x, theCanvas);
			layers.Add(n);

			setFrame(n.firstKF);
			return n;
		}

		/// <summary>
		/// Sets the frame.
		/// </summary>
		/// <param name="pos">The position.</param>
		public void setFrame(uint pos)
		{
			for (int a = 0; a < layers.Count; a++)
				if (a != selectedLayer)
					layers[a].doDisplay(pos, false);

			if (selectedLayer != -1)
				layers[selectedLayer].doDisplay(pos);

			theCanvas.Refresh();
		}

		/// <summary>
		/// Sets the frame.
		/// </summary>
		public void setFrame()
		{
			for (int a = 0; a < layers.Count; a++)
				if (a != selectedLayer)
					layers[a].doDisplay(selectedFrame, false);

			if (selectedLayer != -1)
				layers[selectedLayer].doDisplay(selectedFrame);

			if (selectedLayer >= 0)
				Canvas.activeFigure = layers[selectedLayer].fig;
			theCanvas.Refresh();
			if(selectedKeyFrame != null)
				mainForm.theToolbox.setColor(selectedKeyFrame.figColor);
		}

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
				selectedFrame = (uint)(x - 80) / 9 + (uint)mainForm.splitContainer1.Panel1.HorizontalScroll.Value / 9;
				if (y < 16)
					selectedLayer = -1;
				else if (y < layers.Count() * 16 + 16)
					selectedLayer = (y - 16) / 16;

				setFrame();
				selectedType = selectedFrameType();
				if (e.Button == MouseButtons.Left)
					mouseDown = true;
				if (selectedKeyFrame != null)
					mainForm.theToolbox.setColor(selectedKeyFrame.figColor);

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
			if (selectedLayer == -1)
				return 5;

			//Get the currently selected layer (so that we can check it's frames)
			Layer selLayer = layers[selectedLayer];

			//Check if the frame is on the first or last keyframe position
			// (Will have to be changed when framesets are implemented)
			if (selectedFrame == selLayer.firstKF)
			{
				selectedKeyFrame = selLayer.keyFrames[0];
				return 2;
			}
			else if (selectedFrame == selLayer.lastKF)
			{
				selectedKeyFrame = selLayer.keyFrames[selLayer.keyFrames.Count - 1];
				return 3;
			}

			//Check each individual keyframe in the layer and see if the positions match
			//I'm going to revise this into a binary search later for efficiency.
			foreach (KeyFrame x in selLayer.keyFrames)
			{
				if (x.pos == selectedFrame)
				{
					selectedKeyFrame = x;
					return 1;
				}
			}

			//Now that all other possibilities are eliminated, it isn't a keyframe.
			//So because of this, we set the selected key frame to null.
			selectedKeyFrame = null;

			//This basically checks if the position is between the first and last keyframe positions.
			//This will also have to be changed when framesets are implemented.
			if (selectedFrame > selLayer.firstKF && selectedFrame < selLayer.lastKF)
				return 4;

			//Since it's failed all other tests, there is no frame at that spot.
			return 0;
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

			//massive facking if statements, which I will later condense to save space
			tst_insertKeyframe.Enabled = frameType == 4;
			tst_insertKeyframeAtPose.Enabled = frameType == 4;
			tst_removeKeyframe.Enabled = frameType == 1;
			tst_setPosePrvKfrm.Enabled = frameType == 3;
			tst_setPoseNxtKfrm.Enabled = frameType == 2;
			tst_onionSkinning.Enabled = frameType != 0 | frameType != 4;

			tst_insertFrameset.Enabled = frameType == 0;
			tst_removeFrameset.Enabled = frameType != 0;

			tst_moveLayerUp.Enabled = true;
			tst_moveLayerDown.Enabled = true;
			tst_insertLayer.Enabled = true;
			tst_removeLayer.Enabled = true;

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
			Layer cLayer = layers[selectedLayer];

			//Decide what to do based on the clicked menu item was.
			switch (name)
			{
				//Layer methods handle this part.
				case "tst_insertKeyframe":
					cLayer.insertKeyFrame(selectedFrame);
					selectedFrameType();
					break;

				case "tst_removeKeyframe":
					cLayer.removeKeyFrame(selectedFrame);
					break;

				//Insert a keyframe like before and then copy the positions of the
				//current tween fig into the new keyframe.
				case "tst_insertKeyframeAtPose":
					int p = cLayer.insertKeyFrame(selectedFrame);
					KeyFrame newFrame = cLayer.keyFrames[p];
					List<StickJoint> jointz = cLayer.tweenFig.Joints;

					for (int a = 0; a < jointz.Count; a++)
						newFrame.Joints[a].location = jointz[a].location;

					break;

				case "tst_setPosePrvKfrm":
					int pos = cLayer.keyFrames.IndexOf(selectedKeyFrame);

					for (int a = 0; a < cLayer.keyFrames[pos].Joints.Count; a++)
						cLayer.keyFrames[pos].Joints[a].location = new Point(cLayer.keyFrames[pos - 1].Joints[a].location.X, cLayer.keyFrames[pos - 1].Joints[a].location.Y);

					break;

				case "tst_setPoseNxtKfrm":
					pos = cLayer.keyFrames.IndexOf(selectedKeyFrame);

					for (int a = 0; a < cLayer.keyFrames[pos].Joints.Count; a++)
						cLayer.keyFrames[pos].Joints[a].location = new Point(cLayer.keyFrames[pos + 1].Joints[a].location.X, cLayer.keyFrames[pos + 1].Joints[a].location.Y);

					break;

				default:
					return;

			}

			//After that, refresh the timeline and the canvas.
			Refresh();
			setFrame();
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
			uint newSelected = (uint)(x - 80) / 9 + (uint)mainForm.splitContainer1.Panel1.HorizontalScroll.Value / 9;
			decimal check = (decimal)(x - 80) / 9 + mainForm.splitContainer1.Panel1.HorizontalScroll.Value / 9;

			//If the int calculation is below zero, then set the selected value to 0 (as we don't want anything going negative)
			if (check < 0)
				newSelected = 0;

			//If the timeline is selected, simply update the selected frame and refresh everything.
			if (selectedLayer == -1)
			{
				selectedFrame = newSelected;
				setFrame();
				Refresh();
				return;
			}

			//Get the difference in the selected frame before and after the mouse move update.
			int diff = (int)newSelected - (int)selectedFrame;

			//This function basically handles anything having to do with dragging things on the timeline.

			//Everything on from here relies on the mouse having moved at least 1 frame, so if it hasn't or there is
			//no keyframe selected, then we can just stop here.
			if (diff == 0 || selectedType == 0)
				return;

			//Get the currently selected layer (Note to self: make this into a property of the timeline for easy access)
			Layer l = layers[selectedLayer];

			//And get the list of keyframes from the layer.
			List<KeyFrame> kfs = l.keyFrames;

			//Behaviour for dragging keyframes
			if (selectedType > 0 && selectedType < 4)
			{
				// Note to Self: Remove constraints on dradding keyframes. Penis

				//This code makes sure that the selected frame doesn't go over/under the positions of it's neighbouring frames
				if (selectedKeyFrame.pos == l.lastKF)
				{
					newSelected = Math.Max(newSelected, kfs[kfs.Count - 2].pos + 1);
					l.lastKF = newSelected;
				}
				else if (selectedKeyFrame.pos == l.firstKF)
				{
					newSelected = Math.Min(newSelected, kfs[1].pos - 1);
					l.firstKF = newSelected;
				}
				else
				{
					//Get the index off the keyframe inside the frames list (quite helpful)
					int indOf = l.keyFrames.IndexOf(selectedKeyFrame);

					uint backPos = kfs[indOf - 1].pos, frontPos = kfs[indOf + 1].pos;

					newSelected = Math.Max(Math.Min(newSelected, frontPos - 1), backPos + 1);
				}

				selectedKeyFrame.pos = newSelected;
				selectedFrame = newSelected;
				Refresh();
				return;
			}
			else if (selectedType == 4)
			{
				if (diff < 0 && l.firstKF == 0)
					return;

				if (diff < 0 && l.firstKF < -1 * diff)
				{
					diff = -1 * (int)l.firstKF; //To make sure you can't make things go negative
				}

				if (diff == 0)
				{
					return;
				}
				else if (diff > 0)
				{
					uint d = (uint)diff;
					l.firstKF += d;
					l.lastKF += d;

					for (int a = 0; a < l.keyFrames.Count; a++)
						l.keyFrames[a].pos += d;
				}
				else
				{
					uint d = (uint)(-1 * diff);
					l.firstKF -= d;
					l.lastKF -= d;

					for (int a = 0; a < l.keyFrames.Count; a++)
						l.keyFrames[a].pos -= d;
				}
				selectedFrame = newSelected;
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
		/// Handles the Tick event of the playTimer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void playTimer_Tick(object sender, EventArgs e)
		{
			if (hasFrames(selectedFrame + 1))
			{
				selectedFrame++;
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

		/// <summary>
		/// Starts the timer.
		/// </summary>
		/// <param name="fps">The FPS.</param>
		public void startTimer(byte fps)
		{
			int mspertick = 1000 / fps;
			playTimer.Interval = mspertick;

			selectedLayer = -1; //the -1 layer is the timeline 'layer'
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

		/// <summary>
		/// Determines whether or not any of the layers at a given point in the timeline contain a frame.
		/// </summary>
		/// <param name="pos">The position to check.</param>
		/// <returns>Returns whether or not the given position has frames.</returns>
		private bool hasFrames(uint pos)
		{
			//Check if it's within the keyset of any layer.
			//If it's within even one, then there is an active figure and it will return true.
			foreach (StickLayer k in layers)
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
				if (selectedFrame <= k.lastKF && selectedFrame >= k.firstKF)
					return true;
			}

			return false;
		}

		//Only used by loading so far.
		/// <summary>
		/// Resets the everything.
		/// </summary>
		/// <param name="keepDefault">if set to <c>true</c> [reset].</param>
		public static void resetEverything(bool keepDefault)
		{
			layers = new List<Layer>();
			layercount = selectedLayer = 0; selectedFrame = 0;
			mainForm.resetTimeline(keepDefault);
		}
	}
}
