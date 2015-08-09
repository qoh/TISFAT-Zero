using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TISFAT.Util
{
    public static class MathUtil
    {
        public static bool PointInRect(Point Loc, Rectangle Area)
        {
            return Loc.X >= Area.X &&
                Loc.X < Area.X + Area.Width && 
                Loc.Y >= Area.Y && 
                Loc.Y < Area.Y + Area.Height;
        }

        public static bool PointInRect(PointF Loc, RectangleF Area)
        {
            return Loc.X >= Area.X &&
                Loc.X < Area.X + Area.Width &&
                Loc.Y >= Area.Y &&
                Loc.Y < Area.Y + Area.Height;
        }

        public static bool IsPointAroundPoint(Point A, Point B, int Tolerance)
        {
            return Math.Pow(B.X - A.X, 2) + Math.Pow(B.Y - A.Y, 2) <= Tolerance * Tolerance;
        }

        public static bool IsPointInPoint(Point A, Point B, int Tolerance)
        {
            return PointInRect(A, new Rectangle(B.X - Tolerance, B.Y - Tolerance, Tolerance * 2, Tolerance * 2));
        }

        public static bool IsPointInPoint(PointF A, PointF B, float Tolerance)
        {
            return PointInRect(A, new RectangleF(B.X - Tolerance, B.Y - Tolerance, Tolerance * 2, Tolerance * 2));
        }
    }
}
