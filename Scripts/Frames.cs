using System.Collections.Generic;
using System.Drawing;

namespace TISFAT_ZERO
{
    public abstract class KeyFrame
    {
		public uint pos;
		public byte type;
		public List<StickJoint> Joints = new List<StickJoint>();
    }

    public class StickFrame : KeyFrame
    {
        public StickFrame(List<StickJoint> ps, uint po)
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
                if (Joints[i].parent != null)
                    Joints[i].CalcLength(null);

            for (int i = 0; i < Joints.Count; i++)
                if (Joints[i].parent != null)
                    Joints[i].parent.children.Add(Joints[i]);
        }

        public StickFrame(StickFrame old)
		{
			pos = old.pos;
			Joints = old.Joints;
			type = 0;
		}

		public StickFrame(uint po)
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
			Joints.Add(new StickJoint("Head", new Point(222, 150), 13, Color.Black, Color.Yellow, 0, 1, true, Joints[0]));

			for (int i = 0; i < Joints.Count; i++)
				if (Joints[i].parent != null)
					Joints[i].CalcLength(null);

			for (int i = 0; i < Joints.Count; i++)
				if (Joints[i].parent != null)
					Joints[i].parent.children.Add(Joints[i]);

			pos = po;
			type = 0;
		}
    }

	public class LineFrame : KeyFrame
	{
		public LineFrame(List<StickJoint> ps, uint po)
		{
			type = 2; pos = po;
			Joints.Add(new StickJoint(ps[0], null));
			Joints.Add(new StickJoint(ps[1], Joints[0]));
		}

		public LineFrame(uint po)
		{
			type = 2; pos = po;
			Joints.Add(new StickJoint("Rock", new Point(30, 30), 12, Color.Black, Color.Green, 0, 0, false, null));
			Joints.Add(new StickJoint("Hard Place", new Point(45, 30), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[0]));
		}
	}

	public class RectFrame : KeyFrame
	{
		public RectFrame(List<StickJoint> ps, uint po)
		{
			type = 3; pos = po;

			Joints.Add(new StickJoint(ps[0], null));
			Joints.Add(new StickJoint(ps[1], Joints[0]));
			Joints.Add(new StickJoint(ps[2], Joints[1]));
			Joints.Add(new StickJoint(ps[3], Joints[2]));
		}

		public RectFrame(uint po)
		{
			type = 3; pos = po;

			Joints.Add(new StickJoint("CornerTL", new Point(30, 30), 12, Color.Black, Color.LimeGreen, 0, 0, false, null));
			Joints.Add(new StickJoint("CornerLL", new Point(30, 70), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[0]));
			Joints.Add(new StickJoint("CornerLR", new Point(150, 70), 12, Color.Black, Color.Red, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("CornerTR", new Point(150, 30), 12, Color.Black, Color.Blue, 0, 0, false, Joints[2]));
		}
	}
}
