using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace TISFAT.Util
{
    public static class Drawing
    {
        private static Vector2 PointToVector(PointF point)
        {
            return new Vector2(point.X, point.Y);
        }
        
        public static void Line(PointF start, PointF end, Color color)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(color);
            GL.Vertex2(PointToVector(start));
            GL.Vertex2(PointToVector(end));
            GL.End();
        }

        public static void QuadLine(PointF start, PointF end, float thickness, Color color)
        {
            float dx = end.X - start.X;
            float dy = end.Y - start.Y;
            float dm = (float)Math.Sqrt(dx * dx + dy * dy);
            
            float px = (dy / dm) * thickness;
            float py = -(dx / dm) * thickness;

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            GL.Vertex2(new Vector2(start.X - px, start.Y - py));
            GL.Vertex2(new Vector2(end.X - px, end.Y - py));
            GL.Vertex2(new Vector2(end.X + px, end.Y + py));
            GL.Vertex2(new Vector2(start.X + px, start.Y + py));
            GL.End();
        }

        public static void CappedLine(PointF start, PointF end, float thickness, Color color)
        {
            QuadLine(start, end, thickness, color);
            Circle(start, thickness, color);
            Circle(end, thickness, color);
        }

        public static void Circle(PointF center, float radius, Color color)
        {
            Circle(center, radius, color, (int)Math.Ceiling(radius * 2));
        }

        public static void Circle(PointF center, float radius, Color color, int segments)
        {
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(color);
            GL.Vertex2(PointToVector(center));

            for (int i = 0; i < segments; i++)
            {
                float theta = ((float)i / ((float)segments - 1)) * ((float)Math.PI * 2);

                GL.Vertex2(new Vector2(
                    center.X + (float)Math.Cos(theta) * radius,
                    center.Y + (float)Math.Sin(theta) * radius
                ));
            }

            GL.End();
        }

        public static void Rectangle(PointF position, SizeF size, Color color)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            GL.Vertex2(PointToVector(position));
            GL.Vertex2(position.X + size.Width, position.Y);
            GL.Vertex2(position.X + size.Width, position.Y + size.Height);
            GL.Vertex2(position.X, position.Y + size.Height);
            GL.End();
        }

        public static void RectangleLine(PointF position, SizeF size, Color color)
        {
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(color);
            GL.Vertex2(PointToVector(position));
            GL.Vertex2(position.X + size.Width, position.Y);
            GL.Vertex2(position.X + size.Width, position.Y + size.Height);
            GL.Vertex2(position.X, position.Y + size.Height);
            GL.End();
        }
    }
}
