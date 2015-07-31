using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace TISFAT.Util
{
    public class ScrollController
    {
        private int Width = 0;
        private int LastWidth = 0;
        public int Height = 14;

        public int X, Y;
        public int xOffset;

        public bool Hovered = false;
        public bool Dragging;
        public int DragStartMouse;
        public int DragStartOffset;

        public bool CheckIsHovered(Point location)
        {
            Hovered = location.X >= X + xOffset && location.X < X + xOffset + Width && location.Y >= Y && location.Y < Y + Height;
            return Hovered;
        }

        public void StartDragging(Point location)
        {
            Dragging = true;
            DragStartMouse = location.X;
            DragStartOffset = xOffset;
        }

        public void Drag(int xLoc, int viewLength)
        {
            xOffset = Math.Max(0, Math.Min(viewLength, DragStartOffset + (xLoc - DragStartMouse)));
        }

        public void Resize(int contentLength, int viewLength)
        {
            if (xOffset + Width >= viewLength)
            {
                GetScrollbarLength(contentLength, viewLength);

                if (xOffset > 0)
                    xOffset -= (Width - LastWidth);
                else
                    xOffset = 0;
            }
        }

        public int GetScrollbarLength(int contentLength, int viewLength)
        {
            LastWidth = Width;
            Width = (int)(viewLength * Math.Min(1.0f, (float)viewLength / (float)contentLength));
            if (Width == viewLength)
                xOffset = 0;

            return Width;
        }

        public void Draw(int ContentLength, SizeF viewSize)
        {
            GetScrollbarLength(ContentLength, (int)viewSize.Width);
            Y = (int)viewSize.Height - 12;

            if (Width != viewSize.Width)
            {
                Drawing.Rectangle(new PointF(0, viewSize.Height - 14), new SizeF(viewSize.Width, Height), Color.DarkGray);
                Drawing.Rectangle(new PointF(X + xOffset, Y), new SizeF(Width, Height - 4), (Hovered || Dragging) ? Color.LightGray : Color.Gray);
            }
        }
    }
}
