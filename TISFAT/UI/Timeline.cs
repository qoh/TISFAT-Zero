using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TISFAT.Util;

namespace TISFAT
{
    public partial class Timeline
    {
        public GLControl GLContext;
        //public ScrollController HorizScrollBar;
        public bool LastHovered = false;
        public bool IsDragging = false;

        private float FrameNum;
        private DateTime? PlayStart;
        
        public Layer SelectedLayer;
        public Frameset SelectedFrameset;
        public Keyframe SelectedKeyframe;
        private int SelectedBlankFrame = -1;
        private int SelectedNullFrame = -1;

        #region CTOR | OpenGL core functions
        public Timeline(GLControl context)
        {
            GLContext = context;
            //HorizScrollBar = new ScrollController();

            FrameNum = 0.0f;
            PlayStart = null;
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

        public void Resize()
        {
            // Reinit OpenGL
            GLContext_Init();

            // Resize scrollbars
            int LastTime = GetLastTime();
            
            int HContentLength = (LastTime + 101) * 9;
            int VContentLength = (Program.Form.ActiveProject.Layers.Count) * 16 + 16;

            int TotWidth = GLContext.Width - 80;
            int TotHeight = GLContext.Height;

            if (Program.Form.VScrollVisible)
                TotWidth -= 17;

            if (Program.Form.HScrollVisible)
                TotHeight -= 18;

            Program.Form.CalcScrollBars(HContentLength, VContentLength, TotWidth, TotHeight);
        }
        #endregion

        public int GetLastTime()
        {
            List<Layer> Layers = Program.Form.ActiveProject.Layers;
            int LastTime = 0;
            foreach (Layer layer in Layers)
                LastTime = (int)Math.Max(layer.Framesets[layer.Framesets.Count - 1].EndTime, LastTime);

            return LastTime;
        }

        #region Seeking / Playback Functions
        public void SeekStart()
        {
            FrameNum = 0.0f;
            SelectedKeyframe = null;
            GLContext.Invalidate();
        }

        public void SeekFirstFrame()
        {
            Project project = Program.Form.ActiveProject;

            foreach (Layer layer in project.Layers)
                FrameNum = Math.Min(FrameNum, layer.Framesets[0].StartTime);

            SelectedKeyframe = null;
            GLContext.Invalidate();
        }

        public void SeekLastFrame()
        {
            Project project = Program.Form.ActiveProject;

            foreach (Layer layer in project.Layers)
                FrameNum = Math.Max(FrameNum, layer.Framesets[layer.Framesets.Count - 1].EndTime);

            ClearSelection();
            GLContext.Invalidate();
        }

        public void TogglePause()
        {
            ClearSelection();

            if (PlayStart != null)
            {
                FrameNum = GetCurrentFrame();
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
            if (SelectedKeyframe != null)
                return SelectedKeyframe.Time;

            if (SelectedBlankFrame != -1)
                return SelectedBlankFrame;

            if (SelectedNullFrame != -1)
                return SelectedNullFrame;

            float frame;

            if (PlayStart != null)
                frame = ((float)(DateTime.Now - (DateTime)PlayStart).TotalSeconds) * 10.0f;
            else
                frame = 0.0f;

            return FrameNum + frame;
        }

        public bool IsPlaying()
        {
            return PlayStart != null;
        }
        #endregion

        public void ClearSelection()
        {
            SelectedLayer = null;
            SelectedFrameset = null;
            SelectedKeyframe = null;
            SelectedBlankFrame = -1;
            SelectedNullFrame = -1;
        }

        public void SelectFrame(Point location)
        {
            // Select keyframes
            Project project = Program.Form.ActiveProject;

            int frameIndex = (int)Math.Floor((location.X - 80) / 9.0);
            int layerIndex = (int)Math.Floor((location.Y - 16) / 16.0);

            if (layerIndex < 0 || layerIndex >= project.Layers.Count)
                return;

            Layer layer = project.Layers[layerIndex];

            ClearSelection();

            foreach (Frameset frameset in layer.Framesets)
            {
                foreach (Keyframe keyframe in frameset.Keyframes)
                {
                    if (keyframe.Time == frameIndex)
                    {
                        SelectedLayer = layer;
                        SelectedFrameset = frameset;
                        SelectedKeyframe = keyframe;
                        GLContext.Invalidate();

                        return;
                    }
                }

                if (frameIndex > frameset.StartTime && frameIndex < frameset.EndTime)
                {
                    SelectedLayer = layer;
                    SelectedFrameset = frameset;
                    SelectedBlankFrame = frameIndex;
                    GLContext.Invalidate();

                    return;
                }
            }

            SelectedLayer = layer;
            SelectedNullFrame = frameIndex;
            GLContext.Invalidate();
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
            
            int dist = ((SplitContainer)GLContext.Parent.Parent).SplitterDistance - 17;
            int TotalLayerHeight = Math.Min(Layers.Count * 16 + 16, dist);

            GLContext.MakeCurrent();

            GL.ClearColor(Color.FromArgb(220, 220, 220));

            GL.Clear(ClearBufferMask.ColorBufferBit);

            //GL.Enable(EnableCap.LineSmooth);

            int scrollX = Program.Form.HScrollVal;
            int scrollY = Program.Form.VScrollVal > 0 ? Program.Form.VScrollVal - 1 : 0;

            GL.Translate(-scrollX, -scrollY, 0);

            DrawBackground(frameCount, layerHeight);

            DrawKeyframes(Layers);

            DrawMisc(Layers, layerHeight, frameWidth, frameCount);

            GL.Translate(0, scrollY, 0);
            DrawTimelineNumbers(frameCount, layerHeight);
            DrawPlayhead();
            GL.Translate(0, -scrollY, 0);

            DrawTimelineOutlines(frameCount, layerHeight);

            // Stop translating the drawing by x
            GL.Translate(scrollX, 0, 0);

            DrawLabels(Layers);

            // Stop translating the drawing by y
            GL.Translate(0, scrollY, 0);

            DrawTimelineLayer();

            // Draw rect below layers to hide bottom of playhead when
            // scrolling past the displayed layers.
            Drawing.Rectangle(new PointF(0, Layers.Count * 16 + 17), new SizeF(81, GLContext.Height - (Layers.Count * 16 + 16)), Color.FromArgb(220, 220, 220));

            GLContext.SwapBuffers();
            Program.Form.Canvas.GLContext_Paint(sender, e);
        }

        #region Mouse Events
        public void MouseDown(Point location)
        {
            if (IsPlaying())
                return;

            ClearSelection();

            IsDragging = true;
            if (PlayStart != null)
                PlayStart = DateTime.Now;

            FrameNum = (float)Math.Max(0, Math.Floor((location.X - 79.0f) / 9.0f));
            GLContext.Invalidate();

            SelectFrame(location);
        }

        public void MouseMoved(Point location)
        {
            if (IsDragging)
            {
                if (PlayStart != null)
                    PlayStart = DateTime.Now;

                FrameNum = (float)Math.Max(0, Math.Floor((location.X - 79.0f) / 9.0f));
                GLContext.Invalidate();
            }
        }

        public void MouseUp()
        {
            IsDragging = false;
            GLContext.Invalidate();
        }

        public void MouseLeft()
        {
            IsDragging = false;
            GLContext.Invalidate();
        } 
        #endregion
    }
}
