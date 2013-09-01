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

        public MainF mainForm;
        public Canvas theCanvas;

        //List of layers
        public static List<Layer> layers;
		public int layercount = 0;
		public uint selectedFrame;
		public int selectedLayer;
        public byte selectedType;
        public KeyFrame selectedKeyFrame;

		private bool mouseDown = false;
        #endregion

		public Timeline(MainF m, Canvas canvas)
		{
			InitializeComponent(); 
			mainForm = m;
            theCanvas = canvas;

			layers = new List<Layer>();
			for (int a = 0; a < 1; a++)
				addStickLayer("Layer " + a);
			selectedFrame = 3;
			selectedLayer = 0;
			this.Refresh();
			setFrame(3);
		}

		private void Timeline_Paint(object sender, PaintEventArgs e)
		{
			#region Timeline Rendering

			//Create the graphics object then clear to the background colour of the majortiy of frames (light gray)
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(220, 220, 220));

			//Create a black pen
			Pen blk = new Pen(Color.FromArgb(140, 140, 140)), bblk = new Pen(Color.Black);

			//Calculate how many frames need to be drawn and what the offset is
			int frames = (mainForm.Width-80) / 9;
            int scroll = mainForm.splitContainer1.Panel1.HorizontalScroll.Value;
			int offset = scroll / 9;

			//Grab the font we need to use to draw strings
			Font fo = SystemFonts.DefaultFont;

			//draw the timeline layer
			g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(0, 0, 79, layers.Count * 16 + 15));
			g.DrawLines(bblk, p1);
            g.DrawString("T I M E L I N E", fo, new SolidBrush(Color.Black), 1, 1.5f);

			//Draw each layer
			for (int a = 1; a-1 < layers.Count; a++)
			{
				g.DrawLines(bblk, new Point[] { new Point(79, 16 * a - 1), new Point(79, 16 * a + 15), new Point(0, 16 * a + 15) });
				g.DrawString(layers[a-1].name, fo, new SolidBrush(Color.Black), 1, 16 * a + 0.4f);
			}

			g.FillRectangle(new SolidBrush(Color.FromArgb(200, 200, 200)), new Rectangle(80, 16 * selectedLayer + 16, Width, 16));

			//Draw the timeline frames
			for (int a = offset; a - offset < frames; a++)
			{
				//Calculate where on the timeline we need to draw the frame
				int xx = (a-offset) * 9 + 80;

				//Default to cyan colour (10th frame colour)
				Color x = Color.Cyan;

				//If frame number is divisble by 100, set colour to red
				if ((a + 1) % 100 == 0)
					x = Color.FromArgb(255, 200, 255);

				//If the frame is not a special colour, don't fill it in (as it's already filled in with that colour)
				if ((a + 1) % 10 == 0)
					g.FillRectangle(new SolidBrush(x), xx, 0, 8, 16 * layers.Count + 15);

				//Write in the number
				g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx - 1, 1));
			}

			Height = layers.Count * 16 + 16;

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

				//Draw all the frames in the layer (I'll implement framesets later ok)
				for (int b = first, kind = 0; b <= count + first; b++)
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

			int sFrameLoc = ((int)selectedFrame - offset) * 9 + 80;

			if (selectedFrame >= offset && selectedFrame - offset < frames)
			{
				g.FillRectangle(new SolidBrush(Color.Red), sFrameLoc, selectedLayer * 16 + 16, 8, 15);
			}

			#endregion Layer Rendering

			//Dispose of the pens (because apparently this is necessary)
			blk.Dispose(); bblk.Dispose();
		}

		public StickLayer addStickLayer(string name)
		{
			StickFigure x = theCanvas.createFigure();
			x.int1 = layercount++;

			StickLayer n = new StickLayer(name, x, theCanvas);
			layers.Add(n);

            setFrame(n.firstKF);
			return n;
		}
        //back to sleep with me lolk
		public void setFrame(uint pos)
		{
			for (int a = 0; a < layers.Count; a++)
				if(a != selectedLayer)
					((StickLayer)layers[a]).doDisplay(pos, false);
			((StickLayer)layers[selectedLayer]).doDisplay(pos);
			theCanvas.Refresh();
		}

		public void setFrame()
		{
			for (int a = 0; a < layers.Count; a++)
				if (a != selectedLayer)
					((StickLayer)layers[a]).doDisplay(selectedFrame, false);
			((StickLayer)layers[selectedLayer]).doDisplay(selectedFrame);
			theCanvas.Refresh();
		}

		private void Timeline_MouseDown(object sender, MouseEventArgs e)
		{
			int x = e.X, y = e.Y;
			if (x > 80)
			{
				if (y > 16)
				{
                    selectedFrame = (uint)(x - 80) / 9 + (uint)mainForm.splitContainer1.Panel1.HorizontalScroll.Value / 9;
					if(y < layers.Count() * 16 + 16)
						selectedLayer = (y - 16) / 16;

					setFrame();
                    selectedType = selectedFrameType();
                    if (e.Button == MouseButtons.Left)
                        mouseDown = true;
					Refresh();
				}
			}
		}

		private byte selectedFrameType()
		{
            StickLayer selLayer = (StickLayer)layers[selectedLayer];

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
            
            foreach (KeyFrame x in selLayer.keyFrames)
            {
                if (((StickFrame)x).pos == selectedFrame)
                {
                    selectedKeyFrame = x;
                    return 1;
                }
            }

            selectedKeyFrame = null;
            if (selectedFrame > selLayer.firstKF && selectedFrame < selLayer.lastKF)
                return 4;

            // 0: blank
            // 1: Keyframe
            // 2: first keyframe
            // 3: last keyframe
            // 4: Tween frame
            return 0;

		}

        private void cxt_Menu_Opening(object sender, CancelEventArgs e)
        {
            int frameType = selectedFrameType();

            if (frameType == 0)
            {
                tst_insertKeyframe.Enabled = false;
                tst_removeKeyframe.Enabled = false;
                tst_setPosePrvKfrm.Enabled = false;
                tst_setPoseNxtKfrm.Enabled = false;
                tst_onionSkinning.Enabled = false;

                tst_insertFrameset.Enabled = true;
                tst_removeFrameset.Enabled = false;

                tst_moveLayerUp.Enabled = true;
                tst_moveLayerDown.Enabled = true;
                tst_insertLayer.Enabled = true;
                tst_removeLayer.Enabled = true;

                tst_keyFrameAction.Enabled = false;

                tst_hideLayer.Enabled = true;
                tst_showLayer.Enabled = true;

                tst_gotoFrame.Enabled = true;
            }

            else if (frameType == 1)
            {
                tst_insertKeyframe.Enabled = false;
                tst_removeKeyframe.Enabled = true;
                tst_setPosePrvKfrm.Enabled = true;
                tst_setPoseNxtKfrm.Enabled = true;
                tst_onionSkinning.Enabled = true;

                tst_insertFrameset.Enabled = false;
                tst_removeFrameset.Enabled = true;

                tst_moveLayerUp.Enabled = true;
                tst_moveLayerDown.Enabled = true;
                tst_insertLayer.Enabled = true;
                tst_removeLayer.Enabled = true;

                tst_keyFrameAction.Enabled = true;

                tst_hideLayer.Enabled = true;
                tst_showLayer.Enabled = true;

                tst_gotoFrame.Enabled = true;
            }
            else if (frameType == 2 | frameType == 3)
            {
                tst_insertKeyframe.Enabled = false;
                tst_removeKeyframe.Enabled = false;
                tst_setPosePrvKfrm.Enabled = true;
                tst_setPoseNxtKfrm.Enabled = true;
                tst_onionSkinning.Enabled = true;

                tst_insertFrameset.Enabled = false;
                tst_removeFrameset.Enabled = true;

                tst_moveLayerUp.Enabled = true;
                tst_moveLayerDown.Enabled = true;
                tst_insertLayer.Enabled = true;
                tst_removeLayer.Enabled = true;

                tst_keyFrameAction.Enabled = true;

                tst_hideLayer.Enabled = true;
                tst_showLayer.Enabled = true;

                tst_gotoFrame.Enabled = true;
            }

            else if (frameType == 4)
            {
                tst_insertKeyframe.Enabled = true;
                tst_removeKeyframe.Enabled = false;
                tst_setPosePrvKfrm.Enabled = false;
                tst_setPoseNxtKfrm.Enabled = false;
                tst_onionSkinning.Enabled = false;

                tst_insertFrameset.Enabled = false;
                tst_removeFrameset.Enabled = true;

                tst_moveLayerUp.Enabled = true;
                tst_moveLayerDown.Enabled = true;
                tst_insertLayer.Enabled = true;
                tst_removeLayer.Enabled = true;

                tst_keyFrameAction.Enabled = false;

                tst_hideLayer.Enabled = true;
                tst_showLayer.Enabled = true;

                tst_gotoFrame.Enabled = true;
            }
        }

        private void cxt_Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.Name;

            switch (name)
            {
                case "tst_insertKeyframe":
                    ((StickLayer)layers[selectedLayer]).insertKeyFrame(selectedFrame);
                    Refresh();
                    break;

                default:
                    break;
                    //implement other shiz here
            }
        }

        private void Timeline_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
                return;

            int x = e.X;

            uint newSelected = (uint)(x - 80) / 9 + (uint)mainForm.splitContainer1.Panel1.HorizontalScroll.Value / 9;
            int diff = (int)newSelected - (int)selectedFrame;

            if (selectedFrame - newSelected == 0 || selectedType == 0)
                return;

            StickLayer l = (StickLayer)layers[selectedLayer];

            if (selectedType > 0 && selectedType < 4)
            {

                if (selectedKeyFrame.pos == l.lastKF)
                    l.lastKF = newSelected;
                else if (selectedKeyFrame.pos == l.firstKF)
                    l.firstKF = newSelected;
                    
                selectedKeyFrame.pos = newSelected;
                selectedFrame = newSelected;
                Refresh();
                return;
            }
            else if (selectedType == 4)
            {
                if(diff < 0 && l.firstKF < -1 * diff)
                {
                    diff = -1 * (int)l.firstKF; //To make sure you can't make things go negative
                }
                if (diff > 0)
                {
                    uint d = (uint)diff;
                    l.firstKF += d;
                    l.lastKF += d;
                    for (int a = 0; a < l.keyFrames.Count; a++)
                        ((StickFrame)l.keyFrames[a]).pos += d;
                }
                else
                {
                    uint d = (uint)(-1 * diff);
                    l.firstKF -= d;
                    l.lastKF -= d;
                    for (int a = 0; a < l.keyFrames.Count; a++)
                        ((StickFrame)l.keyFrames[a]).pos -= d;
                }
                selectedFrame = newSelected;
                Refresh();
            }
        }

        private void Timeline_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
	}
}
