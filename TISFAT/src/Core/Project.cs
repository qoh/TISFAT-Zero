using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TISFAT.Entities;
using TISFAT.Util;

namespace TISFAT
{
	public class Project : ISaveable
	{
		public List<Layer> Layers;
		public Dictionary<Type, int> LayerCount;

		public float AnimSpeed = 10.0f;
		public float FPS = 60.0f;
		public int Width;
		public int Height;

		public Color BackColor;

		public Project()
		{
			Layers = new List<Layer>();
			LayerCount = new Dictionary<Type, int>();

			Camera camera = new Camera();
			Layers.Add(camera.CreateDefaultLayer(0, 20, null));

			Width = 460;
			Height = 360;

			BackColor = Color.White;
		}

		public void Draw(float time, bool render, bool lights)
		{
			EditMode editMode = Program.Form_Main.ActiveEditMode;
			Layer selectedLayer = Program.MainTimeline.SelectedLayer;

			foreach (Layer layer in Layers)
			{
				if (!lights && layer.Data.GetType() == typeof(PointLight))
					continue;

				if (!render && layer == selectedLayer && editMode != EditMode.Default)
				{
					Keyframe prev = layer.FindPrevKeyframe(time);

					if (prev != null)
					{
						if (editMode == EditMode.Onion)
						{
							layer.Draw(prev.Time);
						}
						else if (editMode == EditMode.Phase)
						{
							double t = (double)DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
							//float w = (float)(t % 1.0f) * (time - prev.Time);
							//float w = (float)(t * Program.ActiveProject.FPS % (time - prev.Time));
							//layer.Draw(prev.Time + w);
							float wrapped = (float)(t % 2.0);
							float frames = (wrapped - 1) * Program.ActiveProject.AnimSpeed;
							layer.Draw(time + frames);

							Program.MainTimeline.GLContext.Invalidate();
						}
					}

					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
					GL.Color4(Color.FromArgb(150, Color.White)); // 35
					GL.Begin(PrimitiveType.Quads);
					GL.Vertex2(0, 0);
					GL.Vertex2(0, Program.Form_Canvas.GLHeight);
					GL.Vertex2(Program.Form_Canvas.GLWidth, Program.Form_Canvas.GLHeight);
					GL.Vertex2(Program.Form_Canvas.GLWidth, 0);
					GL.End();
					GL.Disable(EnableCap.Blend);
				}

				layer.Draw(time);
			}

			if (!render && selectedLayer != null)
				selectedLayer.DrawEditable(time);
		}

		#region File Saving / Loading
		public void Write(BinaryWriter writer)
		{
			FileFormat.WriteList(writer, Layers);
			writer.Write((double)AnimSpeed);
			writer.Write((double)FPS);
			writer.Write(Width);
			writer.Write(Height);

			writer.Write(BackColor.A);
			writer.Write(BackColor.R);
			writer.Write(BackColor.G);
			writer.Write(BackColor.B);
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			Layers = FileFormat.ReadList<Layer>(reader, version);
			if (version < 6)
			{
				Camera camera = new Camera();
				Layers.Insert(0, camera.CreateDefaultLayer(0, (uint)Program.MainTimeline.GetLastTime(), null));
			}


			if (version >= 1)
			{
				AnimSpeed = (float)reader.ReadDouble();

				if (version >= 5)
				{
					FPS = (float)reader.ReadDouble();
				}

				if (version >= 2)
				{
					Width = reader.ReadInt32();
					Height = reader.ReadInt32();

					byte a = reader.ReadByte();
					byte r = reader.ReadByte();
					byte g = reader.ReadByte();
					byte b = reader.ReadByte();

					BackColor = Color.FromArgb(a, r, g, b);
				}
			}
		}
		#endregion
	}
}
