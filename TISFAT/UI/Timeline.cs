using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TISFAT.Util;

namespace TISFAT
{
    public partial class Timeline
    {
        public GLControl GLContext;
        public ScrollController HorizScrollBar;
        public bool LastHovered = false;
        public bool IsDragging = false;

        private float Frame;
        private DateTime? PlayStart;

        public Timeline(GLControl context)
        {
            GLContext = context;
            HorizScrollBar = new ScrollController();

            Frame = 0.0f;
            PlayStart = null;
        }

        public void Rewind()
        {
            Frame = 0.0f;
        }

        public void TogglePause()
        {
            if (PlayStart != null)
            {
                Frame = GetCurrentFrame();
                PlayStart = null;
            }
            else
            {
                PlayStart = DateTime.Now;
                GLContext.Invalidate();
            }
        }

        public float GetCurrentFrame()
        {
            float frame;

            if (PlayStart != null)
                frame = ((float)(DateTime.Now - (DateTime)PlayStart).TotalSeconds) * 10.0f;
            else
                frame = 0.0f;

            return Frame + frame;
        }

        public bool IsRedrawing()
        {
            return PlayStart != null;
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

        public float FrameToPixels(float frame)
        {
            return 80 + frame * 9;
        }

        public void GLContext_Paint(object sender, PaintEventArgs e)
        {
            List<Layer> Layers = Program.Form.ActiveProject.Layers;

            float lastFrame = 0;

            foreach (Layer layer in Layers)
                lastFrame = Math.Max(lastFrame, layer.Framesets[layer.Framesets.Count - 1].EndTime);

            int frameCount = (int)Math.Ceiling(lastFrame + 101);
            int frameWidth = frameCount * 9;
            int layerHeight = Layers.Count * 16;
            int LastTime = 0;

            int dist = ((SplitContainer)GLContext.Parent.Parent).SplitterDistance - HorizScrollBar.Height + 2;

            // Calculate height of visible frames
            int TotalLayerHeight = Math.Min(Layers.Count * 16 + 16, dist);

            GLContext.MakeCurrent();

            GL.ClearColor(Color.FromArgb(220, 220, 220));

            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            // Translate the drawing, draw keyframes
            GL.Translate(-HorizScrollBar.xOffset, 0, 0);

            DrawBackground(frameCount, layerHeight);

            DrawKeyframes(Layers);

            DrawMisc(Layers, layerHeight, frameWidth, frameCount);

            DrawPlayhead();

            // Stop translating the drawing
            GL.Translate(HorizScrollBar.xOffset, 0, 0);

            DrawLabels(Layers);

            // Draw scrollbar
            HorizScrollBar.Draw((LastTime + 100) * 9 + 80, GLContext.Size);

            GLContext.SwapBuffers();
            Program.Form.Canvas.GLContext_Paint(sender, e);
        }

        public void Resize()
        {
            List<Layer> Layers = Program.Form.ActiveProject.Layers;

            int LastTime = 0;

            foreach (Layer layer in Layers)
                LastTime = (int)Math.Max(layer.Framesets[layer.Framesets.Count - 1].EndTime, LastTime);

            HorizScrollBar.Resize((LastTime + 100) * 9 + 80, GLContext.Width);
        }

        public void MouseDown(Point location)
        {
            if (HorizScrollBar.CheckIsHovered(location))
            {
                HorizScrollBar.StartDragging(location);
            }
            else if (location.Y < 16)
                IsDragging = true;
        }

        public void MouseUp()
        {
            HorizScrollBar.Dragging = false;
            IsDragging = false;
            GLContext.Invalidate();
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
                List<Layer> Layers = Program.Form.ActiveProject.Layers;

                int LastTime = 0;

                foreach (Layer layer in Layers)
                    LastTime = (int)Math.Max(layer.Framesets[layer.Framesets.Count - 1].EndTime, LastTime);

                HorizScrollBar.Drag(location.X, (LastTime + 100) * 9 + 80, GLContext.Width);
                GLContext.Invalidate();
            }
            if (IsDragging)
            {
                if (PlayStart != null)
                    PlayStart = DateTime.Now;

                Frame = (float)Math.Max(0, Math.Floor((location.X - 79.0f) / 9.0f));
                GLContext.Invalidate();
            }
        }

        public void MouseLeft()
        {
            HorizScrollBar.Hovered = false;
            IsDragging = false;
            GLContext.Invalidate();
        }
    }
}
