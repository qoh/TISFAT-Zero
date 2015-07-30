using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TISFAT.Util
{
    public static class Interpolation
    {
        public static float None(float t, float a, float b)
        {
            if (t < 1)
                return a;
            return b;
        }

        public static float Linear(float t, float a, float b)
        {
            return a + (b - a) * t;
        }

        public static PointF Linear(float t, PointF a, PointF b)
        {
            return new PointF(Linear(t, a.X, b.X), Linear(t, a.Y, b.Y));
        }
    }
}
