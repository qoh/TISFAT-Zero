using OpenTK;
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
		#endregion

		#region Linear
		private static float Linear(float t, float a, float b)
		{
			return a + (b - a) * t;
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
		#endregion

		#region BackOut
		private static float BackOut(float t, float a, float b)
		{
			b -= a;

			return b * ((t = t - 1.0f) * t * ((1.70158f + 1.0f) * t + 1.70158f) + 1.0f) + a;
		}
		#endregion

		public static float Interpolate(float t, float a, float b, EntityInterpolationMode mode)
		{
			switch (mode)
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

		public static Vector3 Interpolate(float t, Vector3 a, Vector3 b, EntityInterpolationMode mode)
		{
			return new Vector3(Interpolate(t, a.X, b.X, mode), Interpolate(t, a.Y, b.Y, mode), Interpolate(t, a.Z, b.Z, mode));
		}

		public static Color Interpolate(float t, Color a, Color b, EntityInterpolationMode mode)
		{
			return Color.FromArgb(
			(int)Math.Max(Interpolate(t, a.A, b.A, mode), 0),
			(int)Math.Min(Interpolate(t, a.R, b.R, mode), 255),
			(int)Math.Min(Interpolate(t, a.G, b.G, mode), 255),
			(int)Math.Min(Interpolate(t, a.B, b.B, mode), 255));
		}
	}
}
