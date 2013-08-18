using System.Collections.Generic;
using System.Drawing;

namespace TISFAT_ZERO
{
    abstract class KeyFrame
    {
        //dummy class
    }

    class StickFrame : KeyFrame
    {
        public Point[] jointPos;
        static readonly byte type = 1;

        public StickFrame(Point[] pos)
        {
            
            jointPos = pos;
        }

        public StickFrame(StickJoint[] joints)
        {
            jointPos = new Point[joints.Length];

            for (int a = 0; a < joints.Length; a++)
                jointPos[a] = joints[a].location;
        }
    }
}
