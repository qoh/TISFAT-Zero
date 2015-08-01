using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TISFAT.Util;

namespace TISFAT
{
    public partial class Timeline
    {
        private void DrawBackground(int frameCount, int layerHeight)
        {
            Drawing.Rectangle(new PointF(80, 0), new SizeF(frameCount * 9, 16), Color.FromArgb(220, 220, 220));
            Drawing.Rectangle(new PointF(80, 16), new SizeF(frameCount * 9, layerHeight), Color.FromArgb(200, 200, 200));
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

                    foreach (Keyframe keyframe in frameset.Keyframes)
                    {
                        Drawing.Rectangle(new PointF(FrameToPixels(keyframe.Time), y), new SizeF(9, 16), Color.Gold);
                        Drawing.Rectangle(new PointF(FrameToPixels(keyframe.Time) + 3, y + 6), new SizeF(3, 3), Color.Black);
                    }

                    Drawing.Line(
                        new PointF(FrameToPixels(frameset.StartTime + 0.5f), y + 8),
                        new PointF(FrameToPixels(frameset.EndTime + 0.5f), y + 8),
                        Color.Black
                    );
                }
            }
        }

        public void DrawMisc(List <Layer> Layers, int layerHeight, int frameWidth, int frameCount)
        {
            // Draw outline outlines
            Drawing.Line(new PointF(80, 16), new PointF(80 + frameWidth, 16), Color.Gray);
            for (int i = 0; i < Layers.Count; i++)
            {
                int y = 16 * (i + 2);
                Drawing.Line(new PointF(80, y), new PointF(80 + frameWidth, y), Color.Gray);
            }
            for (int frame = 0; frame < frameCount; frame++)
            {
                int x = 80 + 9 * (frame + 1);
                Drawing.Line(new PointF(x, 0), new PointF(x, 16 + layerHeight), Color.Gray);
                Drawing.TextRect("" + (frame + 1) % 10, new PointF(x - 9, 0), new Size(9, 16), new Font("Segoe UI", 8), Color.Black, StringAlignment.Center);
            }
        }

        public void DrawPlayhead()
        {
            int tx = 79 + (int)Math.Floor(GetCurrentFrame() * 9);
            Drawing.Rectangle(new PointF(tx + 3, 16), new SizeF(3, GLContext.Height - 16), Color.Red);
            Drawing.RectangleLine(new PointF(tx, 0), new SizeF(9, 16), Color.Red);
        }

        public void DrawLabels(List<Layer> Layers)
        {
            // Draw TIMELINE layer
            Drawing.Rectangle(new PointF(0, 0), new SizeF(80, 16), Color.FromArgb(70, 120, 255));
            Drawing.RectangleLine(new PointF(0, 1), new SizeF(79, 15), Color.Black);
            Drawing.TextRect("Timeline", new PointF(0, 1), new Size(80, 16), new Font("Segoe UI", 9, FontStyle.Bold), Color.Black, StringAlignment.Center);

            // Draw layers
            for (int i = 0; i < Layers.Count; i++)
            {
                Layer layer = Layers[i];

                // Draw layer name here
                Drawing.Rectangle(new PointF(0, 16 * (i + 1)), new SizeF(80, 16), layer.TimelineColor);
                Drawing.RectangleLine(new PointF(0, 16 * (i + 1)), new SizeF(79, 16), Color.Black);
                Drawing.TextRect(layer.Name, new PointF(1, 16 * (i + 1) - 1), new Size(79, 16), new Font("Segoe UI", 9), Color.White, StringAlignment.Near);
            }
        }
    }
}
