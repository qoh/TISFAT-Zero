using System.Collections.Generic;
using System.Drawing;

namespace TISFAT_ZERO
{
	//The base keyframe class that all other frame types are derived from.
	public abstract class KeyFrame
	{
		//Just the basics, a colour, position, type, and a list of joints.
		//This really helps so we don't need to have a bunch of silly casts.
		public Color figColor = Color.Black;
		public int pos;
		public byte type;
		public List<StickJoint> Joints = new List<StickJoint>();
	}

	//This point on really just defines a bunch of different types of keyframes. The only real purpose for having different types
	//is having the joint-set set in properly by default on creation.
	public class StickFrame : KeyFrame
	{
		public StickFrame(List<StickJoint> ps, int po)
		{
			pos = po; type = 0;

			Joints.Add(new StickJoint(ps[0], null));
			Joints.Add(new StickJoint(ps[1], null));
			Joints[0].parent = Joints[1];
			Joints.Add(new StickJoint(ps[2], Joints[1]));
			Joints.Add(new StickJoint(ps[3], Joints[2]));
			Joints.Add(new StickJoint(ps[4], Joints[1]));
			Joints.Add(new StickJoint(ps[5], Joints[4]));
			Joints.Add(new StickJoint(ps[6], Joints[1]));
			Joints.Add(new StickJoint(ps[7], Joints[6]));
			Joints.Add(new StickJoint(ps[8], Joints[7]));
			Joints.Add(new StickJoint(ps[9], Joints[6]));
			Joints.Add(new StickJoint(ps[10], Joints[9]));
			Joints.Add(new StickJoint(ps[11], Joints[0]));

			for (int i = 0; i < Joints.Count; i++)
			{
				if (Joints[i].parent != null)
				{
					Joints[i].CalcLength(null);
					Joints[i].parent.children.Add(Joints[i]);
				}
			}

			if(ps[0].ParentFigure != null)
				figColor = ps[0].ParentFigure.figColor;
		}

		public StickFrame(StickFrame old)
		{
			pos = old.pos;
			Joints = old.Joints;
			type = 0;
		}

		public StickFrame(int po)
		{
			Joints.Add(new StickJoint("Neck", new Point(222, 158), 12, Color.Black, Color.Blue, 0, 0, true, null, false));
			Joints.Add(new StickJoint("Shoulder", new Point(222, 155), 12, Color.Black, Color.Yellow, 0, 0, false, null));
			Joints[0].parent = Joints[1];
			Joints.Add(new StickJoint("RElbow", new Point(238, 166), 12, Color.Black, Color.Red, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("RHand", new Point(246, 184), 12, Color.Black, Color.Red, 0, 0, false, Joints[2]));
			Joints.Add(new StickJoint("LElbow", new Point(206, 167), 12, Color.Black, Color.Blue, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("LHand", new Point(199, 186), 12, Color.Black, Color.Blue, 0, 0, false, Joints[4]));
			Joints.Add(new StickJoint("Hip", new Point(222, 195), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("LKnee", new Point(211, 218), 12, Color.Black, Color.Blue, 0, 0, false, Joints[6]));
			Joints.Add(new StickJoint("LFoot", new Point(202, 241), 12, Color.Black, Color.Blue, 0, 0, false, Joints[7]));
			Joints.Add(new StickJoint("RKnee", new Point(234, 217), 12, Color.Black, Color.Red, 0, 0, false, Joints[6]));
			Joints.Add(new StickJoint("RFoot", new Point(243, 240), 12, Color.Black, Color.Red, 0, 0, false, Joints[9]));
			Joints.Add(new StickJoint("Head", new Point(222, 147), 13, Color.Black, Color.Yellow, 0, 1, true, Joints[0]));

			for (int i = 0; i < Joints.Count; i++)
			{
				if (Joints[i].parent != null)
				{
					Joints[i].CalcLength(null);
					Joints[i].parent.children.Add(Joints[i]);
				}
			}

			pos = po;
			type = 0;
		}
	}

	public class LineFrame : KeyFrame
	{
		public LineFrame(List<StickJoint> ps, int po)
		{
			type = 2; pos = po;
			Joints.Add(new StickJoint(ps[0], null));
			Joints.Add(new StickJoint(ps[1], Joints[0]));
		}

		public LineFrame(int po)
		{
			type = 2; pos = po;
			Joints.Add(new StickJoint("Rock", new Point(30, 30), 12, Color.Black, Color.Green, 0, 0, false, null));
			Joints.Add(new StickJoint("Hard Place", new Point(45, 30), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[0]));
		}
	}

	public class RectFrame : KeyFrame
	{
		public bool filled = true;

		public RectFrame(List<StickJoint> ps, int po)
		{
			type = 3; pos = po;

			Joints.Add(new StickJoint(ps[0], null));
			Joints.Add(new StickJoint(ps[1], Joints[0]));
			Joints.Add(new StickJoint(ps[2], Joints[1]));
			Joints.Add(new StickJoint(ps[3], Joints[2]));
			Joints[0].parent = Joints[3];
		}

		public RectFrame(int po)
		{
			type = 3; pos = po;

			Joints.Add(new StickJoint("CornerTL", new Point(30, 30), 3, Color.Black, Color.LimeGreen, 0, 0, false, null));
			Joints.Add(new StickJoint("CornerLL", new Point(30, 70), 3, Color.Black, Color.Yellow, 0, 0, false, Joints[0]));
			Joints.Add(new StickJoint("CornerLR", new Point(150, 70), 3, Color.Black, Color.Red, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("CornerTR", new Point(150, 30), 3, Color.Black, Color.Blue, 0, 0, false, Joints[2]));
			Joints[0].parent = Joints[3];
		}
	}

	public class LightFrame : KeyFrame
	{
		public LightFrame(List<StickJoint> ps, int po)
		{
			type = 5; pos = po;

			Joints = ps;
		}

		public LightFrame(int po)
		{
			type = 4; pos = po;

			Joints.Add(new StickJoint("Light Source", new Point(30, 30), 1, Color.Black, Color.Green));
		}
	}

	public class custObjectFrame : KeyFrame
	{
		public custObjectFrame(List<StickJoint> ps, int po)
		{
			type = 4; pos = po;
			int[] positions = new int[ps.Count];
			for (int a = 0; a < ps.Count; a++)
			{
				StickJoint p = ps[a].parent;
				if (p != null)
				{
					ps[a].CalcLength(p);
					positions[a] = ps.IndexOf(p);
				}
				else
				{
					positions[a] = -1;
					ps[a].CalcLength(ps[a]);
				}
			}
			Joints = createClone(ps);
		}

		public custObjectFrame(int po)
		{
			pos = po;
			Joints = new List<StickJoint>();
		}

		public static List<StickJoint> createClone(List<StickJoint> old)
		{
			List<StickJoint> x = new List<StickJoint>();
			for(int i = 0; i < old.Count; i++)
				x.Add(new StickJoint(old[i]));
			for (int i = 0;i < old.Count;i++)
			{
				int index = old.IndexOf(old[i].parent);
				if(index != -1)
					x[i].parent = x[index];
			}
			for (int i = 0;i < x.Count;i++)
				if (x[i].parent != null)
				{
					x[i].parent.children.Add(x[i]);
					x[i].CalcLength(null);
				}

				/*x.AddRange(new StickJoint[old.Count]);
				for (int a = 0; a < old.Count; a++)
					writeJoint(x, old, a, positions);
				for (int a = 0; a < old.Count; a++)
					x[a].CalcLength(null);
				for (int i = 0; i < x.Count; i++)
				{
					if (x[i].parent != null)
					{
						x[i].parent.children.Add(x[i]);
					}
				} */

				return x;
		}

		private static void writeJoint(List<StickJoint> jnts, List<StickJoint> olds, int p, int[] positions)
		{
			if (positions[p] != -1 && jnts[positions[p]] == null)
				writeJoint(jnts, olds, positions[p], positions);

			if (jnts[p] != null && jnts[p].location == olds[p].location)
				return;

			jnts[p] = new StickJoint(olds[p], positions[p] != -1 ? jnts[positions[p]] : null);
		}

	}
}
