using System.Drawing;

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

		public static RectangleF Linear(float t, RectangleF a, RectangleF b)
		{
			return new RectangleF(
				Linear(t, a.X, b.X),
				Linear(t, a.Y, b.Y),
				Linear(t, a.Width, b.Width),
				Linear(t, a.Height, b.Height));
		}
	}
}
