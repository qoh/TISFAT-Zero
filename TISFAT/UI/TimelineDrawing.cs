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
			return SplitterDistance + frame * 9;
		}

		private void DrawBackground(int frameCount, int layerHeight)
		{
			Drawing.Rectangle(new PointF(SplitterDistance, 0), new SizeF(frameCount * 9, 16), Color.FromArgb(220, 220, 220));
			Drawing.Rectangle(new PointF(SplitterDistance, 16), new SizeF(frameCount * 9, layerHeight), Color.FromArgb(200, 200, 200));

			// Selected layer highlighting
			if (SelectedLayer != null)
			{
				int selectedIndex = Program.ActiveProject.Layers.IndexOf(SelectedLayer);

				if (selectedIndex != -1)
					Drawing.Rectangle(new PointF(SplitterDistance, (selectedIndex + 1) * 16), new SizeF(frameCount * 9, 16), Color.FromArgb(220, 220, 220));
			}

			for (int frame = 0; frame < frameCount; frame++)
			{
				if ((frame + 1) % 100 == 0)
				{
					float x = SplitterDistance + 9 * frame;
					Drawing.Rectangle(new PointF(x, 0), new SizeF(9, 16 + layerHeight), Color.HotPink);
				}
				else if ((frame + 1) % 10 == 0)
				{
					float x = SplitterDistance + 9 * frame;
					Drawing.Rectangle(new PointF(x, 0), new SizeF(9, 16 + layerHeight), Color.FromArgb(40, 230, 255));
				}
			}
		}

		public void DrawLabels(List<Layer> Layers)
		{
			// Draw layers
			for (int i = 0; i < Layers.Count; i++)
			{
				Layer layer = Layers[i];

				layer.DrawLabel(i, SplitterDistance, HoveredLayerIndex == i, HoveredLayerOverVis);
			}
		}

		private int DrawKeyframes(List<Layer> Layers, int y = 16)
		{
			// Draw keyframes
			foreach (Layer layer in Layers)
			{
				if (layer.Type == LayerTypeEnum.Entity)
				{
					foreach (Frameset frameset in layer.Framesets)
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

							if (frameset.Keyframes.IndexOf(keyframe) < frameset.Keyframes.Count - 1)
							{
								Color lineColor;

								switch (keyframe.InterpMode)
								{
									case EntityInterpolationMode.None:
										lineColor = Color.DarkRed;
										break;
									case EntityInterpolationMode.Linear:
										lineColor = Color.FromArgb(127, 127, 127);
										break;
									case EntityInterpolationMode.QuadInOut:
										lineColor = Color.Cyan;
										break;
									case EntityInterpolationMode.ExpoInOut:
										lineColor = Color.Gold;
										break;
									case EntityInterpolationMode.BounceOut:
										lineColor = Color.LightPink;
										break;
									case EntityInterpolationMode.BackOut:
										lineColor = Color.Khaki;
										break;

									default:
										throw new ArgumentException("Keyframe has an unknown InterpolationMode");
								}

								Drawing.Line(new PointF(FrameToPixels(keyframe.Time) + 12, y + 8),
								new PointF(FrameToPixels(frameset.Keyframes[frameset.Keyframes.IndexOf(keyframe) + 1].Time) - 3, y + 8), 1, lineColor);
							}
						}

						// Selected blank frame
						if (SelectedFrameset == frameset && selectedItems.Contains(SelectionType.BlankFrame) && selectedTime != -1)
							Drawing.Rectangle(new PointF(FrameToPixels(selectedTime), y), new SizeF(9, 16), Color.Red);

						// Frameset line
						//Drawing.Line(
						//	new PointF(FrameToPixels(frameset.StartTime) + 12, y + 8),
						//	new PointF(FrameToPixels(frameset.EndTime) - 3, y + 8),
						//	Color.FromArgb(127, 127, 127)
						//);
					}
				}

				if (SelectedLayer == layer && selectedItems.Contains(SelectionType.NullFrame) && selectedTime != -1)
					Drawing.Rectangle(new PointF(FrameToPixels(selectedTime), y), new SizeF(9, 16), Color.Red);

				y += 16;

				if (layer.Children != null)
					DrawKeyframes(layer.Children, y);
			}

			return y;
		}

		public void DrawMisc(List<Layer> Layers, int layerHeight, int frameWidth, int frameCount)
		{
			// Layer separators
			Drawing.Line(new PointF(SplitterDistance, 16), new PointF(SplitterDistance + frameWidth, 16), 1, Color.Gray);
			for (int i = 0; i < Layers.Count; i++)
			{
				int y = 16 * (i + 2);
				Drawing.Line(new PointF(SplitterDistance, y), new PointF(SplitterDistance + frameWidth, y), 1, Color.Gray);
			}
		}

		public void DrawPlayhead()
		{
			if (selectedTime != -1)
				return;

			float tx = SplitterDistance - 1 + (float)Math.Floor(GetCurrentFrame() * 9);
			Drawing.Rectangle(new PointF(tx + 4, 16), new SizeF(3, GLContext.Height - 16), Color.Red);
			Drawing.RectangleLine(new PointF(tx + 2, 0), new SizeF(7, 15), 1, Color.Red);
		}

		public void DrawTimelineLayer()
		{
			// Draw TIMELINE layer
			Drawing.Rectangle(new PointF(0, 0), new SizeF(SplitterDistance, 16), Color.FromArgb(70, 120, 255));
			Drawing.RectangleLine(new PointF(0, 0), new SizeF(SplitterDistance, 16), 1, Color.Black);
			Drawing.TextRect("Timeline", new PointF(0, 1), new SizeF(SplitterDistance, 16), new Font("Segoe UI", 9, FontStyle.Bold), Color.Black, StringAlignment.Center);

		}

		public void DrawTimelineNumbers(int frameCount, int layerHeight)
		{
			// Number background
			Drawing.Rectangle(new PointF(SplitterDistance, 0), new SizeF(frameCount * 9, 16), Color.FromArgb(220, 220, 220));

			for (int frame = 0; frame < frameCount; frame++)
			{
				if ((frame + 1) % 100 == 0)
				{
					float x = SplitterDistance + 9 * frame;
					Drawing.Rectangle(new PointF(x, 0), new SizeF(9, 16), Color.HotPink);
				}
				else if ((frame + 1) % 10 == 0)
				{
					float x = SplitterDistance + 9 * frame;
					Drawing.Rectangle(new PointF(x, 0), new SizeF(9, 16), Color.FromArgb(40, 230, 255));
				}
			}

			// Frame numbers
			for (int frame = 0; frame < frameCount; frame++)
			{
				float x = SplitterDistance + 9 * (frame + 1);
				Drawing.TextRect("" + (frame + 1) % 10, new PointF(x - 9, 0), new Size(9, 16), new Font("Segoe UI", 8), Color.Black, StringAlignment.Center);
			}

			Drawing.Line(new PointF(SplitterDistance, 16), new PointF(frameCount * 9, 16), 1, Color.Gray);
		}

		public void DrawTimelineOutlines(int frameCount, int layerHeight)
		{
			for (int frame = 0; frame < frameCount; frame++)
			{
				float x = SplitterDistance + 9 * (frame + 1);
				Drawing.Line(new PointF(x, 0), new PointF(x, 16 + layerHeight), 1, Color.Gray);
			}
		}
	}
}
