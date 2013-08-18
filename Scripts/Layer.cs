using System;
using System.Collections.Generic;
using System.Text;

namespace TISFAT_ZERO
{
    abstract class Layer
    {
        uint firstKF, lastKF;
        List<KeyFrame> keyFrames;
    }
}
