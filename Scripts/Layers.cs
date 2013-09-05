using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TISFAT_ZERO
{
    public abstract class Layer
    {
		//Define variables
        public uint firstKF, lastKF;
        public List<KeyFrame> keyFrames;
		public string name;
		public byte type;

		public bool removeKeyFrame(uint pos)
		{
			if (pos == firstKF || pos == lastKF)
				return false;

			foreach (KeyFrame k in keyFrames)
				if (pos == k.pos)
				{
					keyFrames.Remove(k);
					return true;
				}

			return false;
		}
    }

	public class StickLayer : Layer
	{

		private StickFigure fig;
		public int selectedFrame = -1;
        private Canvas theCanvas;
        public StickFigure tweenFig;

		public StickLayer(string nom, StickFigure figure, Canvas aTheCanvas)
		{
			fig = figure;

			//These are the default positions for keyframes.
			firstKF = 0;
			lastKF = 19;
			type = 1;

			keyFrames = new List<KeyFrame>();
			keyFrames.Add(new StickFrame(firstKF));
			keyFrames.Add(new StickFrame(lastKF));

            theCanvas = aTheCanvas;

            tweenFig = new StickFigure(true, true);

			name = nom; //nomnomnom
		}

		public void doDisplay(uint pos, bool current = true)
		{
			bool render = false; int x = -1, start = -1, end = -1;


			if (selectedFrame >= 0)
			{
				StickFrame frm = (StickFrame)keyFrames[selectedFrame];
				for (int a = 0; a < frm.Joints.Length; a++)
				{
					frm.Joints[a].ParentFigure = null;
				}
			}
			

			for (int a = 0; a < keyFrames.Count; a++)
			{
				if (pos == keyFrames[a].pos)
				{
					x = a;
					render = true;
					fig.Joints = ((StickFrame)keyFrames[a]).Joints;
                    if (!(tweenFig == null))
                    {
                        tweenFig.isDrawn = false;
						tweenFig.drawFigure = false;
                    }
					break;
				}
				if (pos < keyFrames[a].pos)
				{
					end = a;
					break;
				}
				else if (pos > keyFrames[a].pos)
				{
					start = a;
				}
			}
			

			if (!render)
			{
                if (pos > firstKF && pos < lastKF)
                {
                    StickFrame s = (StickFrame)keyFrames[start], e = (StickFrame)keyFrames[end];
                    float percent = (float)(pos - s.pos) / (e.pos - s.pos);

					tweenFig.isDrawn = true;
                    if (!theCanvas.drawTweenFigures)
                        theCanvas.drawTweenFigures = true;

                    for (int a = 0; a < 12; a++)
                    {
                        tweenFig.Joints[a].location = s.Joints[a].location;
                        tweenFig.Joints[a].Tween(s.Joints[a], e.Joints[a], percent);
                    }
                }
                else
                {
                    tweenFig.isDrawn = false;
                }
			}

			fig.isDrawn = render;
			fig.drawFigure = render;
			fig.drawHandles = render & current;
			fig.isActiveFigure = true;
			selectedFrame = x;

			if (selectedFrame >= 0)
			{
				StickFrame frm = (StickFrame)keyFrames[selectedFrame];
				for (int a = 0; a < frm.Joints.Length; a++)
				{
					frm.Joints[a].ParentFigure = fig;
				}
			}
		}

		//Insert a keyframe at position pos in the timeline
		public int insertKeyFrame(uint pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				StickFrame x = new StickFrame(((StickFrame)keyFrames[0]).Joints,pos);

				keyFrames.Insert(0, x);

				return 0;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				KeyFrame x = keyFrames[keyFrames.Count - 1];
				x.pos = pos;

				keyFrames.Add(x);

				return keyFrames.Count - 1;
			}

			StickFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			for(int a = 0; a < keyFrames.Count; a++)
			{
                StickFrame k = (StickFrame)keyFrames[a];
				if (pos < k.pos)
                {
                    n = new StickFrame(((StickFrame)keyFrames[c - 1]).Joints, pos);
                    n.pos = pos;
                    break;
				}
                else if(pos > k.pos)
                {
                    c++;
                }
				else if (pos == k.pos) // We can't insert a frame in the same spot as another!
					return -1;

			}

			keyFrames.Insert(c, n);

			return c;
		}
	}
}
