using System.Collections.Generic;
using System.Drawing;

namespace TISFAT_ZERO
{
    public abstract class KeyFrame
    {
        //dummy class
		public uint pos;
		public byte type;
    }

    public class StickFrame : KeyFrame
    {
		public StickJoint[] Joints = new StickJoint[12];

        public StickFrame(StickJoint[] ps, uint po)
        {
			pos = po; type = 0;

            Joints[0] = new StickJoint("Neck", ps[0].location, 60, Color.Black, Color.Blue, 0, 0, true, Joints[1], false);
            Joints[1] = new StickJoint("Shoulder", ps[1].location, 12, Color.Black, Color.Yellow, 0, 0, false, Joints[0]);
            Joints[2] = new StickJoint("RElbow", ps[2].location, 12, Color.Black, Color.Red, 0, 0, false, Joints[1]);
            Joints[3] = new StickJoint("RHand", ps[3].location, 12, Color.Black, Color.Red, 0, 0, false, Joints[2]);
            Joints[4] = new StickJoint("LElbow", ps[4].location, 12, Color.Black, Color.Blue, 0, 0, false, Joints[1]);
            Joints[5] = new StickJoint("LHand", ps[5].location, 12, Color.Black, Color.Blue, 0, 0, false, Joints[4]);
            Joints[6] = new StickJoint("Hip", ps[6].location, 12, Color.Black, Color.Yellow, 0, 0, false, Joints[1]);
            Joints[7] = new StickJoint("LKnee", ps[7].location, 12, Color.Black, Color.Blue, 0, 0, false, Joints[6]);
            Joints[8] = new StickJoint("LFoot", ps[8].location, 12, Color.Black, Color.Blue, 0, 0, false, Joints[7]);
            Joints[9] = new StickJoint("RKnee", ps[9].location, 12, Color.Black, Color.Red, 0, 0, false, Joints[6]);
            Joints[10] = new StickJoint("RFoot", ps[10].location, 12, Color.Black, Color.Red, 0, 0, false, Joints[9]);
            Joints[11] = new StickJoint("Head", ps[11].location, 13, Color.Black, Color.Yellow, 0, 1, true, Joints[0]);

            for (int i = 0; i < Joints.Length; i++)
                if (Joints[i].parent != null)
                    Joints[i].CalcLength(null);

            for (int i = 0; i < Joints.Length; i++)
                if (Joints[i].parent != null)
                    Joints[i].parent.children.Add(Joints[i]);
        }

        public StickFrame(StickFrame old)
		{
			pos = old.pos;
			Joints = old.Joints;
		}

		public StickFrame(uint po)
		{
			Joints[0] = new StickJoint("Neck", new Point(222, 158), 60, Color.Black, Color.Blue, 0, 0, true, Joints[1], false);
			Joints[1] = new StickJoint("Shoulder", new Point(222, 155), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[0]);
			Joints[2] = new StickJoint("RElbow", new Point(238, 166), 12, Color.Black, Color.Red, 0, 0, false, Joints[1]);
			Joints[3] = new StickJoint("RHand", new Point(246, 184), 12, Color.Black, Color.Red, 0, 0, false, Joints[2]);
			Joints[4] = new StickJoint("LElbow", new Point(206, 167), 12, Color.Black, Color.Blue, 0, 0, false, Joints[1]);
			Joints[5] = new StickJoint("LHand", new Point(199, 186), 12, Color.Black, Color.Blue, 0, 0, false, Joints[4]);
			Joints[6] = new StickJoint("Hip", new Point(222, 195), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[1]);
			Joints[7] = new StickJoint("LKnee", new Point(211, 218), 12, Color.Black, Color.Blue, 0, 0, false, Joints[6]);
			Joints[8] = new StickJoint("LFoot", new Point(202, 241), 12, Color.Black, Color.Blue, 0, 0, false, Joints[7]);
			Joints[9] = new StickJoint("RKnee", new Point(234, 217), 12, Color.Black, Color.Red, 0, 0, false, Joints[6]);
			Joints[10] = new StickJoint("RFoot", new Point(243, 240), 12, Color.Black, Color.Red, 0, 0, false, Joints[9]);
			Joints[11] = new StickJoint("Head", new Point(222, 150), 13, Color.Black, Color.Yellow, 0, 1, true, Joints[0]);

			for (int i = 0; i < Joints.Length; i++)
				if (Joints[i].parent != null)
					Joints[i].CalcLength(null);

			for (int i = 0; i < Joints.Length; i++)
				if (Joints[i].parent != null)
					Joints[i].parent.children.Add(Joints[i]);

			pos = po;
		}
    }
}
