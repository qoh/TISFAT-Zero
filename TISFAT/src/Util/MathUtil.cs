using System;
using System.Drawing;

namespace TISFAT.Util
{
	public class Vector2F
	{
		public float X, Y;

		public Vector2F()
		{
			X = 0;
			Y = 0;
		}

		public Vector2F(float n)
		{
			X = n;
			Y = n;
		}

		public Vector2F(float x, float y)
		{
			X = x;
			Y = y;
		}

		public Vector2F(Vector2F vector)
		{
			X = vector.X;
			Y = vector.Y;
		}

		public Vector2F Rotate(float phi)
		{
			float c = (float)Math.Cos(MathUtil.DegToRad(phi));
			float s = (float)Math.Sin(MathUtil.DegToRad(phi));
			return new Vector2F(c * X - s * Y, s * X + c * Y);
		}

		public static Vector2F operator +(Vector2F a, Vector2F b)
		{
			return new Vector2F(a.X + b.X, a.Y + b.Y);
		}

		public static Vector2F operator -(Vector2F a, Vector2F b)
		{
			return new Vector2F(a.X - b.X, a.Y - b.Y);
		}

		public static Vector2F operator *(Vector2F a, Vector2F b)
		{
			return new Vector2F(a.X * b.X, a.Y * b.Y);
		}

		public static Vector2F operator *(Vector2F a, float b)
		{
			return new Vector2F(a.X * b, a.Y * b);
		}

		public static Vector2F operator /(Vector2F a, float b)
		{
			return new Vector2F(a.X / b, a.Y / b);
		}
	}

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

		public static float DegToRad(float degrees)
		{
			return (float)Math.PI * degrees / 180;
		}

		public static float NextFloat(Random random)
		{
			double mantissa = (random.NextDouble() * 2.0) - 1.0;
			double exponent = Math.Pow(2.0, random.Next(-126, 128));
			return (float)(mantissa * exponent);
		}
	}
}
