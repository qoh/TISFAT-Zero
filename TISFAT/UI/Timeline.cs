using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TISFAT.Util;

namespace TISFAT
{
	public partial class Timeline
	{
		#region Properties
		public GLControl GLContext;
		public bool LastHovered = false;
		public bool IsMouseDown = false;
		public bool IsDragging = false;

		public uint KeyframeDragStartTime;
		public uint FramesetDragStartTime;

		private float FrameNum;
		private DateTime? PlayStart;

		public Point MouseDragStart;
		public bool IsDraggingKeyframe = false;
		public bool IsDraggingFrameset = false;

		public int HoveredLayerIndex = -1;
		public bool HoveredLayerOverVis = false;

		public Layer SelectedLayer
		{
			get { return selectedItems.GetSelected(SelectionType.Layer) as Layer; }
		}
		public Frameset SelectedFrameset
		{
			get { return selectedItems.GetSelected(SelectionType.Frameset) as Frameset; }
		}
		public Keyframe SelectedKeyframe
		{
			get { return selectedItems.GetSelected(SelectionType.Keyframe) as Keyframe; }
		}

		public int selectedTime
		{
			get { return selectedItems.Time; }
		}

		public TimelineSelection selectedItems = new TimelineSelection();

		public int VisibilityBitmapOn;
		public int VisibilityBitmapOn_hover;
		public int VisibilityBitmapOff;
		public int VisibilityBitmapOff_hover;
		#endregion

		#region CTOR | OpenGL core functions
		public Timeline(GLControl context)
		{
			GLContext = context;

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

			if (VisibilityBitmapOn == 0)
			{
				// Create visibility button bitmaps
				VisibilityBitmapOn = Drawing.GenerateTexID(Properties.Resources.eye);
				VisibilityBitmapOn_hover = Drawing.GenerateTexID(Properties.Resources.eye_hover);
				VisibilityBitmapOff = Drawing.GenerateTexID(Properties.Resources.eye_off);
				VisibilityBitmapOff_hover = Drawing.GenerateTexID(Properties.Resources.eye_off_hover);
			}
		}

		public void Resize()
		{
			// Reinit OpenGL
			GLContext_Init();

			// Resize scrollbars
			int LastTime = GetLastTime();

			int HContentLength = (LastTime + 101) * 9;
			int VContentLength = (Program.ActiveProject.Layers.Count) * 16 + 16;

			int TotWidth = GLContext.Width - 80;
			int TotHeight = GLContext.Height;

			if (Program.Form_Main.Form_Timeline.VScrollVisible)
				TotWidth -= 17;

			if (Program.Form_Main.Form_Timeline.HScrollVisible)
				TotHeight -= 18;

			Program.Form_Main.Form_Timeline.CalcScrollBars(HContentLength, VContentLength, TotWidth, TotHeight);
		}
		#endregion

		public int GetLastTime()
		{
			List<Layer> Layers = Program.ActiveProject.Layers;
			int LastTime = 0;
			foreach (Layer layer in Layers)
				LastTime = (int)Math.Max(layer.Framesets[layer.Framesets.Count - 1].EndTime, LastTime);

			return LastTime;
		}

		#region Seeking / Playback Functions
		public void SeekStart()
		{
			FrameNum = 0.0f;
			ClearSelection();
			GLContext.Invalidate();
		}

		public void SeekFirstFrame()
		{
			Project project = Program.ActiveProject;

			foreach (Layer layer in project.Layers)
				FrameNum = Math.Min(FrameNum, layer.Framesets[0].StartTime);

			ClearSelection();
			GLContext.Invalidate();
		}

		public void SeekPrevFrame()
		{
			// Go to the previous frame goddamnit
		}

		public void SeekNextFrame()
		{
			// Go to the next frame goddamnit
		}

		public void SeekLastFrame()
		{
			Project project = Program.ActiveProject;

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
			if (selectedTime != -1)
				return selectedTime;

			float frame;

			if (PlayStart != null)
				frame = ((float)(DateTime.Now - (DateTime)PlayStart).TotalSeconds) * Program.ActiveProject.FPS;
			else
				frame = 0.0f;

			return FrameNum + frame;
		}

		public int GetFrameType()
		{
			if (selectedItems.Contains(SelectionType.Keyframe))
				return 2;

			if (selectedItems.Contains(SelectionType.BlankFrame))
				return 1;

			if (selectedItems.Contains(SelectionType.NullFrame))
				return 0;

			return -1;
		}

		public bool IsPlaying()
		{
			return PlayStart != null;
		}
		#endregion

		public void ClearSelection()
		{
			selectedItems.Clear();
		}

		public void SelectFrame(Point location)
		{
			// Select keyframes
			Project project = Program.ActiveProject;

			int frameIndex = (int)Math.Floor((location.X - 80) / 9.0);
			int layerIndex = (int)Math.Floor((location.Y - 16) / 16.0);
			FrameNum = (float)Math.Max(0, Math.Floor((location.X - 79.0f) / 9.0f));

			if (layerIndex < 0)
			{
				if (PlayStart != null)
					PlayStart = DateTime.Now;

				GLContext.Invalidate();

				return;
			}

			if (layerIndex >= project.Layers.Count)
				return;

			Layer layer = project.Layers[layerIndex];

			ClearSelection();

			foreach (Frameset frameset in layer.Framesets)
			{
				foreach (Keyframe keyframe in frameset.Keyframes)
				{
					if (keyframe.Time == frameIndex)
					{
						selectedItems.Select(layer, frameset, keyframe);

						GLContext.Invalidate();
						return;
					}
				}

				if (frameIndex > frameset.StartTime && frameIndex < frameset.EndTime)
				{
					selectedItems.Select(SelectionType.BlankFrame, frameIndex);
					selectedItems.Select(layer, frameset);

					GLContext.Invalidate();
					return;
				}
			}

			selectedItems.Select(SelectionType.NullFrame, frameIndex);
			selectedItems.Select(layer);

			GLContext.Invalidate();
		}

		public void GLContext_Paint(object sender, PaintEventArgs e)
		{
			List<Layer> Layers = Program.ActiveProject.Layers;

			float lastFrame = 0;

			foreach (Layer layer in Layers)
				lastFrame = Math.Max(lastFrame, layer.Framesets[layer.Framesets.Count - 1].EndTime);

			int frameCount = (int)Math.Ceiling(lastFrame + 101);
			int frameWidth = frameCount * 9;
			int layerHeight = Layers.Count * 16;

			int dist = GLContext.Height - 17;
			int TotalLayerHeight = Math.Min(Layers.Count * 16 + 16, dist);

			GLContext.MakeCurrent();

			GL.ClearColor(Color.FromArgb(220, 220, 220));

			GL.Clear(ClearBufferMask.ColorBufferBit);

			int scrollX = Program.Form_Main.Form_Timeline.HScrollVal;
			int scrollY = Program.Form_Main.Form_Timeline.VScrollVal > 0 ? Program.Form_Main.Form_Timeline.VScrollVal - 1 : 0;

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

			Program.Form_Main.Form_Canvas.GLContext_Paint(sender, e);

			if (IsPlaying())
			{
				Application.DoEvents();
				GLContext.Invalidate();
			}
		}

		#region Mouse Events
		public void MouseDown(Point location, MouseButtons button)
		{
			Point locationActual = location;
			location = new Point(location.X + Program.Form_Main.Form_Timeline.HScrollVal, location.Y + Program.Form_Main.Form_Timeline.VScrollVal);

			IsMouseDown = true;
			MouseDragStart = location;

			if (IsPlaying())
				return;

			if (location.X - Program.Form_Main.Form_Timeline.HScrollVal > 80)
			{
				ClearSelection();

				if (PlayStart != null)
					PlayStart = DateTime.Now;

				SelectFrame(location);

				if (button == MouseButtons.Right)
					return;

				if (SelectedKeyframe != null)
				{
					KeyframeDragStartTime = SelectedKeyframe.Time;
					IsDraggingKeyframe = true;
				}
				else if (selectedItems.Contains(SelectionType.BlankFrame) && selectedTime != -1)
				{
					FramesetDragStartTime = SelectedFrameset.Keyframes[0].Time;
					IsDraggingFrameset = true;
				}
			}
			else if (button == MouseButtons.Left)
			{
				if (HoveredLayerIndex >= 0 && HoveredLayerIndex < Program.ActiveProject.Layers.Count)
					if (HoveredLayerOverVis)
					{
						Program.ActiveProject.Layers[HoveredLayerIndex].Visible =
							!Program.ActiveProject.Layers[HoveredLayerIndex].Visible;

						GLContext.Invalidate();
					}
			}
		}

		public void MouseMoved(Point location)
		{
			Point locationActual = location;
			location = new Point(location.X + Program.Form_Main.Form_Timeline.HScrollVal, location.Y + Program.Form_Main.Form_Timeline.VScrollVal);

			if (HoveredLayerIndex > -1)
			{
				HoveredLayerIndex = -1;
				Program.Form_Main.Cursor = Cursors.Default;
				GLContext.Invalidate();
			}

			if (location.X - Program.Form_Main.Form_Timeline.HScrollVal > 80)
			{
				if (IsMouseDown)
					IsDragging = true;

				if (IsDraggingKeyframe) // Keyframe stuff
				{
					uint TargetTime = (uint)Math.Max(0, Math.Floor((location.X - 79.0f) / 9.0f));

					for (int i = SelectedLayer.Framesets.IndexOf(SelectedFrameset) - 1; i < SelectedLayer.Framesets.IndexOf(SelectedFrameset) + 2; i++)
					{
						if (i == -1 || i > SelectedLayer.Framesets.Count - 1)
							continue;
						if (SelectedLayer.Framesets[i] == SelectedFrameset)
							continue;

						if (i < SelectedLayer.Framesets.IndexOf(SelectedFrameset))
						{
							if (TargetTime <= SelectedLayer.Framesets[i].EndTime)
								return;
						}
						else
							if (TargetTime >= SelectedLayer.Framesets[i].StartTime)
								return;
					}

					if (TargetTime >= SelectedFrameset.StartTime)
					{
						foreach (Keyframe frame in SelectedFrameset.Keyframes)
						{
							if (frame != SelectedKeyframe)
								if (frame.Time == TargetTime)
									return;
						}
					}
					else if (SelectedKeyframe.Time != SelectedFrameset.StartTime && SelectedKeyframe.Time != SelectedFrameset.EndTime)
						return;

					SelectedKeyframe.Time = TargetTime;
					SelectedFrameset.Keyframes = SelectedFrameset.Keyframes.OrderBy(o => o.Time).ToList();

					// Recalc Scrollbars
					Resize();
					GLContext.Invalidate();
				}
				else if (IsDraggingFrameset) // Frameset stuff
				{
					int StartTime = (int)Math.Max(0, Math.Floor((MouseDragStart.X - 79.0f) / 9.0f));
					int TargetTime = (int)Math.Max(0, Math.Floor((location.X - 79.0f) / 9.0f));
					int NewTime = TargetTime - StartTime;

					float NewStartTime = SelectedFrameset.StartTime + NewTime;
					float NewEndTime = SelectedFrameset.EndTime + NewTime;

					//uhhh here you check if anything is overlapping but i dont quite remember gimme a minute
					// btw the amount of frames a frameset takes up can be figured out with frameset.duration
					// which is just frameset.starttime - frameset.endtime

					if (NewStartTime < 0)
						return;


					foreach (Frameset x in SelectedLayer.Framesets)
					{
						if (x == SelectedFrameset)
							continue;

						if (NewStartTime > x.EndTime)
							continue;
						else if (NewEndTime < x.StartTime)
							continue;

						return;
					}

					foreach (Keyframe frame in SelectedFrameset.Keyframes)
						frame.Time = (uint)(frame.Time + NewTime);

					SelectedLayer.Framesets = SelectedLayer.Framesets.OrderBy(o => o.EndTime).ToList();

					selectedItems.Select(SelectionType.BlankFrame, (int)TargetTime);

					MouseDragStart = location;

					// Recalc Scrollbars
					Resize();
					GLContext.Invalidate();
				}
				else if (IsDragging)
				{
					if (PlayStart != null)
						PlayStart = DateTime.Now;

					FrameNum = (float)Math.Max(0, Math.Floor((location.X - 79.0f) / 9.0f));
					GLContext.Invalidate();
				}
			}
			else // Do stuff for whether you're hovering over a visibility button
			{
				int y = locationActual.Y + Program.Form_Main.Form_Timeline.VScrollVal;

				HoveredLayerIndex = (int)Math.Floor((y - 16) / 16.0);
				GLContext.Invalidate();

				if (HoveredLayerIndex > Program.ActiveProject.Layers.Count - 1 || HoveredLayerIndex == -1)
					return;

				Rectangle VisButton = new Rectangle(new Point(65, 16 * (HoveredLayerIndex + 1) + 2), new Size(14, 14));
				if (MathUtil.PointInRect(new PointF(locationActual.X, y), VisButton))
				{
					HoveredLayerOverVis = true;
					GLContext.Invalidate();
				}
				else
				{
					HoveredLayerOverVis = false;
					GLContext.Invalidate();
				}
			}
		}

		public void MouseUp(Point Location, MouseButtons button)
		{
			if (button == MouseButtons.Left)
			{
				if (IsDraggingKeyframe && KeyframeDragStartTime != SelectedKeyframe.Time)
					Program.Form_Main.Do(new KeyframeMoveAction(SelectedLayer, SelectedFrameset, SelectedKeyframe, KeyframeDragStartTime));
				else if (IsDraggingFrameset && FramesetDragStartTime != SelectedFrameset.Keyframes[0].Time)
					Program.Form_Main.Do(new FramesetMoveAction(SelectedLayer, SelectedFrameset, (int)FramesetDragStartTime, (int)SelectedFrameset.Keyframes[0].Time));
			}

			if (Location.X > 80 && Location.Y < (Program.ActiveProject.Layers.Count * 16) + 16 &&
				Location.Y > 16 &&
				button == MouseButtons.Right &&
				!IsPlaying())
				Program.Form_Main.Form_Timeline.ShowCxtMenu(Location, GetFrameType(), (int)FrameNum);

			IsMouseDown = false;
			IsDragging = false;
			IsDraggingKeyframe = false;
			IsDraggingFrameset = false;

			GLContext.Invalidate();
		}

		public void MouseLeft()
		{
			IsDragging = false;
			GLContext.Invalidate();
		}
		#endregion

		public void InsertKeyframe()
		{
			Keyframe prev = null;
			uint TargetTime = (uint)FrameNum;

			for (int i = 0; i < SelectedFrameset.Keyframes.Count; i++)
			{
				if (SelectedFrameset.Keyframes[i].Time < TargetTime)
					if (SelectedFrameset.Keyframes[i + 1] != null)
						if (SelectedFrameset.Keyframes[i + 1].Time > TargetTime)
						{
							prev = SelectedFrameset.Keyframes[i];
							break;
						}
			}

			if (prev == null)
				return;

			Program.Form_Main.Do(new KeyframeAddAction(SelectedLayer, SelectedFrameset, TargetTime, prev.State.Copy()));
		}

		public void RemoveKeyframe()
		{
			Program.Form_Main.Do(new KeyframeRemoveAction(SelectedLayer, SelectedFrameset, SelectedKeyframe));

			GLContext.Invalidate();
		}

		public void SetPoseToPrevious()
		{
			if (SelectedFrameset.Keyframes.IndexOf(SelectedKeyframe) < 1)
				return;

			Program.Form_Main.Do(new ManipulatableUpdateAction(SelectedLayer, SelectedFrameset, SelectedKeyframe, SelectedKeyframe.State, SelectedFrameset.Keyframes[SelectedFrameset.Keyframes.IndexOf(SelectedKeyframe) - 1].State));
		}

		public void SetPoseToNext()
		{
			if (SelectedFrameset.Keyframes.IndexOf(SelectedKeyframe) == SelectedFrameset.Keyframes.Count - 1)
				return;

			Program.Form_Main.Do(new ManipulatableUpdateAction(SelectedLayer, SelectedFrameset, SelectedKeyframe, SelectedKeyframe.State, SelectedFrameset.Keyframes[SelectedFrameset.Keyframes.IndexOf(SelectedKeyframe) + 1].State));
		}

		public bool CanInsertFrameset()
		{
			if (!selectedItems.Contains(SelectionType.NullFrame))
				return false;

			if (selectedTime > SelectedLayer.Framesets[SelectedLayer.Framesets.Count - 1].EndTime)
				return true;
			else
			{
				float nextTime = -1;

				foreach (Frameset fs in SelectedLayer.Framesets)
				{
					if (fs.EndTime < selectedTime)
						continue;

					nextTime = fs.StartTime;
					break;
				}

				if (nextTime > -1 && nextTime >= selectedTime + 2)
					return true;
			}

			return false;
		}

		public void InsertFrameset()
		{
			if (!Program.MainTimeline.CanInsertFrameset())
				return;

			Program.Form_Main.Do(new FramesetAddAction(SelectedLayer));
		}

		public void RemoveFrameset()
		{
			Program.Form_Main.Do(new FramesetRemoveAction(SelectedLayer, SelectedFrameset));
		}

		public void MoveLayerUp()
		{
			Program.Form_Main.Do(new LayerMoveUpAction(SelectedLayer));
		}

		public void MoveLayerDown()
		{
			Program.Form_Main.Do(new LayerMoveDownAction(SelectedLayer));
		}

		public void RemoveLayer()
		{
			if (SelectedLayer != null)
				if (Program.ActiveProject.Layers.IndexOf(SelectedLayer) != -1)
					Program.Form_Main.Do(new LayerRemoveAction(SelectedLayer));
		}

		public void ChangeInterpolationMode(EntityInterpolationMode mode)
		{
			if (SelectedKeyframe != null)
				Program.Form_Main.Do(new KeyframeChangeInterpModeAction(SelectedLayer, SelectedFrameset, SelectedKeyframe, mode));
		}
	}
}
