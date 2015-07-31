using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TISFAT.Util;

namespace TISFAT
{
    public class Timeline
    {
        public GLControl GLContext;
        public ScrollController HorizScrollBar;
        public bool LastHovered = false;

        public Timeline(GLControl context)
        {
            GLContext = context;
            HorizScrollBar = new ScrollController();
        }

        public void GLContext_Init()
        {
            GLContext.MakeCurrent();

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Viewport(0, 0, GLContext.Width, GLContext.Height);
            GL.Ortho(0, GLContext.Width, GLContext.Height, 0, -1, 1);
            GL.Disable(EnableCap.DepthTest);
        }

        public void GLContext_Paint()
        {
            bool cont = true;

            if (cont)
            {
                List<Layer> Layers = Program.Form.ActiveProject.Layers;
                int dist = ((SplitContainer)GLContext.Parent.Parent).SplitterDistance - HorizScrollBar.Height + 2;

                // Calculate number of visible frames
                int frames = (Program.Form.Width - 80) / 9;

                // Calculate height of visible frames
                int TotalLayerHeight = Math.Min(Layers.Count * 16 + 16, dist);

                GLContext.MakeCurrent();

                GL.ClearColor(Color.FromArgb(220, 220, 220));

                GL.Clear(ClearBufferMask.ColorBufferBit);

                #region Keyframes & Frames
                // Translate the drawing, draw keyframes
                int LastTime = 0;
                GL.Translate(-HorizScrollBar.xOffset, 0, 0);
                for (int i = 0; i < Layers.Count; i++)
                {
                    // Draw keyframes
                    foreach (Keyframe keyframe in Layers[i].Keyframes)
                    {
                        LastTime = (int)Math.Max(keyframe.Time, LastTime);
                        Drawing.Rectangle(new PointF(80 + (keyframe.Time * 9), 16 * (i + 1)), new SizeF(9, 16), Color.Yellow);
                    }
                }

                // Draw frame outlines
                Drawing.Line(new PointF(80, 1), new PointF((LastTime + 100) * 9 + 80, 1), Color.FromArgb(140, 140, 140));
                for (double a = 0, b = 88; a < (LastTime + 100); a++, b = 88 + 9 * a)
                    Drawing.Line(new PointF((float)Math.Floor(b), 0), new PointF((float)Math.Floor(b), TotalLayerHeight), Color.FromArgb(140, 140, 140));
                for (double a = 0, b = 16; a <= Layers.Count; a++, b = 16 * a + 16)
                    Drawing.Line(new PointF(80, (float)Math.Floor(b)), new PointF((LastTime + 100) * 9 + 80, (float)Math.Floor(b)), Color.FromArgb(140, 140, 140));

                // Stop translating the drawing
                GL.Translate(HorizScrollBar.xOffset, 0, 0); 
                #endregion

                // Draw TIMELINE layer
                Drawing.Rectangle(new PointF(0, 0), new SizeF(80, 16), Color.FromArgb(70, 120, 255));
                Drawing.RectangleLine(new PointF(0, 1), new SizeF(79, 15), Color.Black);

                // Draw layers
                for (int i = 0; i < Layers.Count; i++)
                {
                    Layer layer = Layers[i];

                    // Draw layer name here
                    Drawing.Rectangle(new PointF(0, 16 * (i + 1)), new SizeF(80, 16), Color.Blue);
                    Drawing.RectangleLine(new PointF(0, 16 * (i + 1)), new SizeF(79, 16), Color.Black);
                }

                // Draw scrollbar
                HorizScrollBar.Draw((LastTime + 100) * 9 + 80, GLContext.Size);

                GLContext.SwapBuffers();
            }
        }

        public void Resize()
        {
            List<Layer> Layers = Program.Form.ActiveProject.Layers;

            int LastTime = 0;

            foreach(Layer layer in Layers)
                foreach (Keyframe keyframe in layer.Keyframes)
                    LastTime = (int)Math.Max(keyframe.Time, LastTime);

            HorizScrollBar.Resize((LastTime + 100) * 9 + 80, GLContext.Width);
        }

        public void MouseDown(Point location)
        {
            if (HorizScrollBar.CheckIsHovered(location))
            {
                HorizScrollBar.StartDragging(location);
            }
        }

        public void MouseUp()
        {
            if (HorizScrollBar.Dragging)
            {
                HorizScrollBar.Dragging = false;
            }
        }

        public void MouseMoved(Point location)
        {
            if (HorizScrollBar.CheckIsHovered(location) != LastHovered && !HorizScrollBar.Dragging)
            {
                LastHovered = !LastHovered;
                GLContext.Invalidate();
            }
            if (HorizScrollBar.Dragging)
            {
                HorizScrollBar.Drag(location.X, GLContext.Width);
                GLContext.Invalidate();
            }
        }

        public void MouseLeft()
        {
            HorizScrollBar.Hovered = false;
            GLContext.Invalidate();
        }
    }
}
