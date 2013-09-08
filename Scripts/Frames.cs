using System.Collections.Generic;
using System.Drawing;

namespace TISFAT_ZERO
{
    public abstract class KeyFrame
    {
        //dummy class
		public uint pos;
		public byte type;
		public List<StickJoint> Joints = new List<StickJoint>();
    }

    public class StickFrame : KeyFrame
    {
        public StickFrame(List<StickJoint> ps, uint po)
        {
			pos = po; type = 0;

			Joints.Add(new StickJoint("Neck", ps[0].location, 12, Color.Black, Color.Blue, 0, 0, true, null, false));
			Joints.Add(new StickJoint("Shoulder", ps[1].location, 12, Color.Black, Color.Yellow, 0, 0, false, null));
			Joints[0].parent = Joints[1];
			Joints.Add(new StickJoint("RElbow", ps[2].location, 12, Color.Black, Color.Red, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("RHand", ps[3].location, 12, Color.Black, Color.Red, 0, 0, false, Joints[2]));
			Joints.Add(new StickJoint("LElbow", ps[4].location, 12, Color.Black, Color.Blue, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("LHand", ps[5].location, 12, Color.Black, Color.Blue, 0, 0, false, Joints[4]));
			Joints.Add(new StickJoint("Hip", ps[6].location, 12, Color.Black, Color.Yellow, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("LKnee", ps[7].location, 12, Color.Black, Color.Blue, 0, 0, false, Joints[6]));
			Joints.Add(new StickJoint("LFoot", ps[8].location, 12, Color.Black, Color.Blue, 0, 0, false, Joints[7]));
			Joints.Add(new StickJoint("RKnee", ps[9].location, 12, Color.Black, Color.Red, 0, 0, false, Joints[6]));
			Joints.Add(new StickJoint("RFoot", ps[10].location, 12, Color.Black, Color.Red, 0, 0, false, Joints[9]));
			Joints.Add(new StickJoint("Head", ps[11].location, 13, Color.Black, Color.Yellow, 0, 1, true, Joints[0]));

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
			type = 1; pos = po;
			Joints.Add(new StickJoint("Rock", ps[0].location, 12, Color.Black, Color.Green, 0, 0, false, null));
			Joints.Add(new StickJoint("Hard Place", ps[1].location, 12, Color.Black, Color.Yellow, 0, 0, false, Joints[0]));
		}

		public LineFrame(uint po)
		{
			type = 1; pos = po;
			Joints.Add(new StickJoint("Rock", new Point(30, 30), 12, Color.Black, Color.Green, 0, 0, false, null));
			Joints.Add(new StickJoint("Hard Place", new Point(45, 30), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[0]));
		}
	}
}
