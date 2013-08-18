using System.Collections.Generic;
using System.Drawing;

namespace TISFAT_ZERO
{
    public abstract class KeyFrame
    {
        //dummy class
		public uint pos;
    }

    public class StickFrame : KeyFrame
    {
        public Point[] jointPos;
		public uint pos;
        static readonly byte type = 1;

        public StickFrame(Point[] ps, uint po)
        {
			pos = po;
            jointPos = ps;
        }

        public StickFrame(StickJoint[] joints, uint po)
        {
			pos = po;

            jointPos = new Point[joints.Length];

            for (int a = 0; a < joints.Length; a++)
                jointPos[a] = joints[a].location;
        }

		public StickFrame(StickFrame old)
		{
			pos = old.pos;
			jointPos = old.jointPos;
		}
    }
}
