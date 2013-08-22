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
        public StickJoint[] sjoints;
        static readonly byte type = 1;

        public StickFrame(StickJoint[] ps, uint po)
        {
			pos = po;
            sjoints = ps;
        }

		public StickFrame(StickFrame old)
		{
			pos = old.pos;
			sjoints = old.sjoints;
		}
    }
}
