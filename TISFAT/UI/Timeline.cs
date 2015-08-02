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
        public ScrollController HorizScrollBar;
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
            HorizScrollBar = new ScrollController();

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

            // Resize horizontal scrollbar
            List<Layer> Layers = Program.Form.ActiveProject.Layers;
            int LastTime = 0;
            foreach (Layer layer in Layers)
                LastTime = (int)Math.Max(layer.Framesets[layer.Framesets.Count - 1].EndTime, LastTime);
            HorizScrollBar.Resize((LastTime + 100) * 9 + 80, GLContext.Width);
        } 
        #endregion

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
            int LastTime = 0;

            int dist = ((SplitContainer)GLContext.Parent.Parent).SplitterDistance - HorizScrollBar.Height + 2;

            // Calculate height of visible frames
            int TotalLayerHeight = Math.Min(Layers.Count * 16 + 16, dist);

            GLContext.MakeCurrent();

            GL.ClearColor(Color.FromArgb(220, 220, 220));

            GL.Clear(ClearBufferMask.ColorBufferBit);

            //GL.Enable(EnableCap.LineSmooth);
            
            // Translate the drawing
            GL.Translate(-HorizScrollBar.xOffset, 0, 0);

            DrawBackground(frameCount, layerHeight);

            DrawKeyframes(Layers);

            DrawMisc(Layers, layerHeight, frameWidth, frameCount);

            DrawPlayhead();

            // Stop translating the drawing
            GL.Translate(HorizScrollBar.xOffset, 0, 0);

            DrawLabels(Layers);

            // Draw rect below layers to hide bottom of playhead when
            // scrolling past the displayed layers.
            Drawing.Rectangle(new PointF(0, Layers.Count * 16 + 17), new SizeF(81, GLContext.Height - (Layers.Count * 16 + 16)), Color.FromArgb(220, 220, 220));

            HorizScrollBar.Draw((LastTime + 100) * 9 + 80, GLContext.Size);

            //GL.Disable(EnableCap.LineSmooth);

            GLContext.SwapBuffers();
            Program.Form.Canvas.GLContext_Paint(sender, e);
        }

        #region Mouse Events
        public void MouseDown(Point location)
        {
            if (HorizScrollBar.CheckIsHovered(location))
            {
                HorizScrollBar.StartDragging(location);

                return;
            }
            else if (location.Y < 16)
            {
                ClearSelection();

                IsDragging = true;
                if (PlayStart != null)
                    PlayStart = DateTime.Now;

                FrameNum = (float)Math.Max(0, Math.Floor((location.X - 79.0f) / 9.0f));
                GLContext.Invalidate();

                return;
            }

            if (IsPlaying())
                return;

            SelectFrame(location);
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

                FrameNum = (float)Math.Max(0, Math.Floor((location.X - 79.0f) / 9.0f));
                GLContext.Invalidate();
            }
        }

        public void MouseUp()
        {
            HorizScrollBar.Dragging = false;
            IsDragging = false;
            GLContext.Invalidate();
        }

        public void MouseLeft()
        {
            HorizScrollBar.Hovered = false;
            IsDragging = false;
            GLContext.Invalidate();
        } 
        #endregion
    }
}
