using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TISFAT.Entities;
using TISFAT.Util;

namespace TISFAT
{
    public class Project : ISaveable
	{
		public List<Layer> Layers;
		public float FPS = 10.0f;
		public Dictionary<Type, int> LayerCount;

		public Project()
		{
			Layers = new List<Layer>();
			LayerCount = new Dictionary<Type, int>();
		}

		public void Draw(float time, bool render)
		{
			EditMode editMode = Program.Form.ActiveEditMode;
			Layer selectedLayer = Program.Form.MainTimeline.SelectedLayer;

            foreach (Layer layer in Layers)
			{
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
							//float w = (float)(t * Program.Form.ActiveProject.FPS % (time - prev.Time));
							//layer.Draw(prev.Time + w);
							float wrapped = (float)(t % 2.0);
							float frames = (wrapped - 1) * Program.Form.ActiveProject.FPS;
							layer.Draw(time + frames);

							Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
						}
					}

					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
					GL.Color4(Color.FromArgb(150, Color.White)); // 35
					GL.Begin(PrimitiveType.Quads);
					GL.Vertex2(0, 0);
					GL.Vertex2(0, Program.Form.Form_Canvas.GLHeight);
					GL.Vertex2(Program.Form.Form_Canvas.GLWidth, Program.Form.Form_Canvas.GLHeight);
					GL.Vertex2(Program.Form.Form_Canvas.GLWidth, 0);
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
			writer.Write((double)FPS);
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			Layers = FileFormat.ReadList<Layer>(reader, version);
			if (version >= 1)
			{
				FPS = (float)reader.ReadDouble();
			}
		} 
		#endregion
	}
}
