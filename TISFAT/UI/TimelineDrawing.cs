using System;
using System.Collections.Generic;
using System.Drawing;
using TISFAT.Util;

namespace TISFAT
{
    public partial class Timeline
    {
        public float FrameToPixels(float frame)
        {
            return 80 + frame * 9;
        }

        private void DrawBackground(int frameCount, int layerHeight)
        {
            Drawing.Rectangle(new PointF(80, 0), new SizeF(frameCount * 9, 16), Color.FromArgb(220, 220, 220));
            Drawing.Rectangle(new PointF(80, 16), new SizeF(frameCount * 9, layerHeight), Color.FromArgb(200, 200, 200));

            // Selected layer highlighting
            if (SelectedLayer != null)
            {
                int selectedIndex = Program.Form.ActiveProject.Layers.IndexOf(SelectedLayer);

                if (selectedIndex != -1)
                    Drawing.Rectangle(new PointF(80, (selectedIndex + 1) * 16), new SizeF(frameCount * 9, 16), Color.FromArgb(220, 220, 220));
            }

            for (int frame = 0; frame < frameCount; frame++)
            {
                if ((frame + 1) % 100 == 0)
                {
                    int x = 80 + 9 * frame;
                    Drawing.Rectangle(new PointF(x, 0), new SizeF(9, 16 + layerHeight), Color.HotPink);
                }
                else if ((frame + 1) % 10 == 0)
                {
                    int x = 80 + 9 * frame;
                    Drawing.Rectangle(new PointF(x, 0), new SizeF(9, 16 + layerHeight), Color.FromArgb(40, 230, 255));
                }
            }
        }

        private void DrawKeyframes(List<Layer> Layers)
        {
            // Draw keyframes
            for (int i = 0; i < Layers.Count; i++)
            {
                int y = 16 * (i + 1);

                foreach (Frameset frameset in Layers[i].Framesets)
                {
                    Drawing.Rectangle(new PointF(FrameToPixels(frameset.StartTime), y), new SizeF(frameset.Duration * 9, 16), Color.White);

                    // (Selected) Keyframes
                    foreach (Keyframe keyframe in frameset.Keyframes)
                    {
                        Color color = Color.Gold;
                        Color color2 = Color.FromArgb(127, 106, 0);

                        if (keyframe == SelectedKeyframe)
                        {
                            color = Color.Red;
                            color2 = Color.FromArgb(127, 0, 0);
                        }

                        Drawing.Rectangle(new PointF(FrameToPixels(keyframe.Time), y), new SizeF(9, 16), color);
                        Drawing.Rectangle(new PointF(FrameToPixels(keyframe.Time), y), new SizeF(9, 2), color2);
                        Drawing.Rectangle(new PointF(FrameToPixels(keyframe.Time) + 3, y + 9), new SizeF(4, 4), Color.Black);
                        Drawing.Rectangle(new PointF(FrameToPixels(keyframe.Time) + 4, y + 10), new SizeF(2, 2), Color.White);
                    }

                    // Selected blank frame
                    if(SelectedFrameset == frameset && SelectedBlankFrame != -1)
                        Drawing.Rectangle(new PointF(FrameToPixels(SelectedBlankFrame), y), new SizeF(9, 16), Color.Red);

                    // Frameset line
                    Drawing.Line(
                        new PointF(FrameToPixels(frameset.StartTime) + 12, y + 8),
                        new PointF(FrameToPixels(frameset.EndTime) - 3, y + 8),
                        Color.FromArgb(127, 127, 127)
                    );
                }

                if (SelectedLayer == Layers[i] && SelectedNullFrame != -1)
                    Drawing.Rectangle(new PointF(FrameToPixels(SelectedNullFrame), y), new SizeF(9, 16), Color.Red);
            }
        }

        public void DrawMisc(List <Layer> Layers, int layerHeight, int frameWidth, int frameCount)
        {
            // Layer separators
            Drawing.Line(new PointF(80, 16), new PointF(80 + frameWidth, 16), Color.Gray);
            for (int i = 0; i < Layers.Count; i++)
            {
                int y = 16 * (i + 2);
                Drawing.Line(new PointF(80, y), new PointF(80 + frameWidth, y), Color.Gray);
            }
        }

        public void DrawPlayhead()
        {
            if (SelectedKeyframe != null || SelectedBlankFrame != -1 || SelectedNullFrame != -1)
                return;

            int tx = 79 + (int)Math.Floor(GetCurrentFrame() * 9);
            Drawing.Rectangle(new PointF(tx + 4, 16), new SizeF(3, GLContext.Height - 16), Color.Red);
            Drawing.RectangleLine(new PointF(tx + 2, 0), new SizeF(7, 15), Color.Red);
        }

        public void DrawTimelineLayer()
        {
            // Draw TIMELINE layer
            Drawing.Rectangle(new PointF(0, 0), new SizeF(80, 16), Color.FromArgb(70, 120, 255));
            Drawing.RectangleLine(new PointF(0, 0), new SizeF(80, 16), Color.Black);
            Drawing.TextRect("Timeline", new PointF(0, 1), new Size(80, 16), new Font("Segoe UI", 9, FontStyle.Bold), Color.Black, StringAlignment.Center);
        }

        public void DrawTimelineNumbers(int frameCount, int layerHeight)
        {
            // Number background
            Drawing.Rectangle(new PointF(80, 0), new SizeF(frameCount * 9, 16), Color.FromArgb(220, 220, 220));

            for (int frame = 0; frame < frameCount; frame++)
            {
                if ((frame + 1) % 100 == 0)
                {
                    int x = 80 + 9 * frame;
                    Drawing.Rectangle(new PointF(x, 0), new SizeF(9, 16), Color.HotPink);
                }
                else if ((frame + 1) % 10 == 0)
                {
                    int x = 80 + 9 * frame;
                    Drawing.Rectangle(new PointF(x, 0), new SizeF(9, 16), Color.FromArgb(40, 230, 255));
                }
            }

            // Frame numbers
            for (int frame = 0; frame < frameCount; frame++)
            {
                int x = 80 + 9 * (frame + 1);
                Drawing.TextRect("" + (frame + 1) % 10, new PointF(x - 9, 0), new Size(9, 16), new Font("Segoe UI", 8), Color.Black, StringAlignment.Center);
            }

            Drawing.Line(new PointF(80, 16), new PointF(frameCount * 9, 16), Color.Gray);
        }

        public void DrawTimelineOutlines(int frameCount, int layerHeight)
        {
            for (int frame = 0; frame < frameCount; frame++)
            {
                int x = 80 + 9 * (frame + 1);
                Drawing.Line(new PointF(x, 0), new PointF(x, 16 + layerHeight), Color.Gray);
            }
        }

        public void DrawLabels(List<Layer> Layers)
        {
            // Draw layers
            for (int i = 0; i < Layers.Count; i++)
            {
                Layer layer = Layers[i];

                Drawing.Rectangle(new PointF(0, 16 * (i + 1)), new SizeF(80, 16), layer.TimelineColor);
                Drawing.RectangleLine(new PointF(0, 16 * (i + 1)), new SizeF(80, 16), Color.Black);
                Drawing.TextRect(layer.Name, new PointF(1, 16 * (i + 1) - 1), new Size(79, 16), new Font("Segoe UI", 9), Color.White, StringAlignment.Near);
            }
        }
    }
}
