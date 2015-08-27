using System;
using System.Drawing;

namespace TISFAT.Util
{
	public enum EntityInterpolationMode
	{
		None,
		Linear,
		QuadInOut,
		ExpoInOut,
		BounceOut,
		BackOut
	}

    public static class Interpolation
	{
		#region None
		private static float None(float t, float a, float b)
		{
			if (t < 1)
				return a;
			return b;
		}

		private static PointF None(float t, PointF a, PointF b)
		{
			return new PointF(None(t, a.X, b.X), None(t, a.Y, b.Y));
		}

		private static RectangleF None(float t, RectangleF a, RectangleF b)
		{
			return new RectangleF(
				None(t, a.X, b.X),
				None(t, a.Y, b.Y),
				None(t, a.Width, b.Width),
				None(t, a.Height, b.Height));
		}
		#endregion

		#region Linear
		private static float Linear(float t, float a, float b)
		{
			return a + (b - a) * t;
		}

		private static PointF Linear(float t, PointF a, PointF b)
		{
			return new PointF(Linear(t, a.X, b.X), Linear(t, a.Y, b.Y));
		}

		private static RectangleF Linear(float t, RectangleF a, RectangleF b)
		{
			return new RectangleF(
				Linear(t, a.X, b.X),
				Linear(t, a.Y, b.Y),
				Linear(t, a.Width, b.Width),
				Linear(t, a.Height, b.Height));
		}
		#endregion

		#region QuadInOut
		private static float QuadInOut(float t, float a, float b)
		{
			b -= a;
			t /= 1.0f / 2.0f;

			if (t < 1.0f) return b / 2.0f * t * t + a;
			t--;
			return -b / 2.0f * (t * (t - 2.0f) - 1.0f) + a;
		}

		private static PointF QuadInOut(float t, PointF a, PointF b)
		{
			return new PointF(QuadInOut(t, a.X, b.X), QuadInOut(t, a.Y, b.Y));
		}


		private static RectangleF QuadInOut(float t, RectangleF a, RectangleF b)
		{
			return new RectangleF(
				QuadInOut(t, a.X, b.X),
				QuadInOut(t, a.Y, b.Y),
				QuadInOut(t, a.Width, b.Width),
				QuadInOut(t, a.Height, b.Height));
		}
		#endregion

		#region ExpoInOut
		private static float ExpoInOut(float t, float a, float b)
		{
			b -= a;
			t /= 1.0f / 2.0f;

			if (t < 1.0f) return b / 2.0f * (float)Math.Pow(2.0f, 10.0f * (t - 1.0f)) + a;
			t--;
			return b / 2.0f * (-(float)Math.Pow(2.0f, -10.0f * t) + 2.0f) + a;
		}

		private static PointF ExpoInOut(float t, PointF a, PointF b)
		{
			return new PointF(ExpoInOut(t, a.X, b.X), ExpoInOut(t, a.Y, b.Y));
		}


		private static RectangleF ExpoInOut(float t, RectangleF a, RectangleF b)
		{
			return new RectangleF(
				ExpoInOut(t, a.X, b.X),
				ExpoInOut(t, a.Y, b.Y),
				ExpoInOut(t, a.Width, b.Width),
				ExpoInOut(t, a.Height, b.Height));
		} 
		#endregion

		#region BounceOut
		private static float BounceOut(float t, float a, float b)
		{
			b -= a;

			if (t < 1 / 2.75)
			{
				return b * (7.5625f * t * t) + a;
			}
			else if (t < 2 / 2.75)
			{
				t -= 1.5f / 2.75f;
				return b * (7.5625f * t * t + 0.75f) + a;
			}
			else if (t < 2.5 / 2.75)
			{
				t -= 2.25f / 2.75f;
				return b * (7.5625f * t * t + 0.9375f) + a;
			}
			else
			{
				t -= 2.625f / 2.75f;
				return b * (7.5625f * t * t + 0.984375f) + a;
			}
		}

		private static PointF BounceOut(float t, PointF a, PointF b)
		{
			return new PointF(BounceOut(t, a.X, b.X), QuadInOut(t, a.Y, b.Y));
		}

		private static RectangleF BounceOut(float t, RectangleF a, RectangleF b)
		{
			return new RectangleF(
				BounceOut(t, a.X, b.X),
				BounceOut(t, a.Y, b.Y),
				BounceOut(t, a.Width, b.Width),
				BounceOut(t, a.Height, b.Height));
		}
		#endregion

		#region BackOut
		private static float BackOut(float t, float a, float b)
		{
			b -= a;

			return b * ((t = t - 1.0f) * t * ((1.70158f + 1.0f) * t + 1.70158f) + 1.0f) + a;
		}

		private static PointF BackOut(float t, PointF a, PointF b)
		{
			return new PointF(BackOut(t, a.X, b.X), QuadInOut(t, a.Y, b.Y));
		}

		private static RectangleF BackOut(float t, RectangleF a, RectangleF b)
		{
			return new RectangleF(
				BackOut(t, a.X, b.X),
				BackOut(t, a.Y, b.Y),
				BackOut(t, a.Width, b.Width),
				BackOut(t, a.Height, b.Height));
		} 
		#endregion

		public static float Interpolate(float t, float a, float b, EntityInterpolationMode mode)
		{
			switch(mode)
			{
				case EntityInterpolationMode.None:
					return None(t, a, b);
				case EntityInterpolationMode.Linear:
					return Linear(t, a, b);
				case EntityInterpolationMode.QuadInOut:
					return QuadInOut(t, a, b);
				case EntityInterpolationMode.ExpoInOut:
					return ExpoInOut(t, a, b);
				case EntityInterpolationMode.BounceOut:
					return BounceOut(t, a, b);
				case EntityInterpolationMode.BackOut:
					return BackOut(t, a, b);

				default:
					throw new System.ArgumentException("EntityInterpolationmode not valid.");
			}
		}

		public static PointF Interpolate(float t, PointF a, PointF b, EntityInterpolationMode mode)
		{
			return new PointF(Interpolate(t, a.X, b.X, mode), Interpolate(t, a.Y, b.Y, mode));
		}

		public static RectangleF Interpolate(float t, RectangleF a, RectangleF b, EntityInterpolationMode mode)
		{
			return new RectangleF(
				Interpolate(t, a.X, b.X, mode),
				Interpolate(t, a.Y, b.Y, mode),
				Interpolate(t, a.Width, b.Width, mode),
				Interpolate(t, a.Height, b.Height, mode));
		}
	}
}
