using System;
using System.Drawing;

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

		public static bool IsPointNearPoint(Point A, Point B, int Tolerance)
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

		public static SizeF fitToSize(float srcWidth, float srcHeight, float maxWidth, float maxHeight)
		{
			var ratio = Math.Min(maxWidth / srcWidth, maxHeight / srcHeight);

			return new SizeF(srcWidth * ratio, srcHeight * ratio);
		}

		public static PointF Rotate(PointF dir, float phi)
		{
			float c = (float)Math.Cos(phi);
			float s = (float)Math.Sin(phi);

			return new PointF(c * dir.X - s * dir.Y, s * dir.X + c * dir.Y);
		}

		public static PointF Difference(PointF a, PointF b)
		{
			return new PointF(a.X - b.X, a.Y - b.Y);
		}

		public static double Angle(PointF a)
		{
			return Math.Atan2(a.Y, a.X);
		}

		public static double Angle(PointF a, PointF b)
		{
			return Angle(Difference(a, b));
		}

		public static double Length(PointF a)
		{
			return Math.Sqrt(a.X * a.X + a.Y * a.Y);
		}

		public static double Length(PointF a, PointF b)
		{
			return Length(Difference(a, b));
		}

		public static PointF TranslatePoint(PointF a, PointF b)
		{
			return new PointF(a.X + b.X, a.Y + b.Y);
		}

		public static PointF ScalePoint(PointF a, float scale)
		{
			return new PointF(a.X * scale, a.Y * scale);
		}
	}
}
