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

		public bool removeKeyFrame(uint pos)
		{
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

		public StickLayer(string nom, StickFigure figure, Canvas aTheCanvas)
		{
			fig = figure;

			firstKF = 3;
			lastKF = 10;
			keyFrames = new List<KeyFrame>();
			keyFrames.Add(new StickFrame(3));
			keyFrames.Add(new StickFrame(7));
			keyFrames.Add(new StickFrame(10));

            theCanvas = aTheCanvas;

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
					float percent = (float)(pos - firstKF) / (lastKF - firstKF);
					StickFrame s = (StickFrame)keyFrames[start], e = (StickFrame)keyFrames[end];

					for (int a = 0; a < 12; a++)
					{
						fig.Joints[a].Tween(s.Joints[a], e.Joints[a], percent);
					}
					render = true;
				}
			}

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
		public bool insertKeyFrame(uint pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				StickFrame x = new StickFrame(((StickFrame)keyFrames[0]).Joints, keyFrames[0].pos - 1);

				keyFrames.Insert(0, x);

				return true;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				KeyFrame x = keyFrames[keyFrames.Count - 1];
				x.pos = keyFrames[keyFrames.Count - 1].pos + 1;

				keyFrames.Add(x);

				return true;
			}

			StickFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			foreach (StickFrame k in keyFrames)
			{
				if (pos < k.pos)
				{
					n = new StickFrame(k);
					n.pos = pos;
				}
				else if (pos == k.pos) // We can't insert a frame in the same spot as another!
					return false;

				c++;
			}

			keyFrames.Insert(c - 1, n);

			return true;
		}

	}
}
