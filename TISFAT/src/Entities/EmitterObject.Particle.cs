using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class EmitterObject
	{
		public class Particle
		{
			#region Old Code
			public PointF OriginalPos;
			public PointF Position;

			public float Gravity;
			public float GravityVariance;
			public PointF GravityDir;

			public PointF Drag;
			public float DragVariance;

			public bool Recolor;

			public List<PointF> Sizes;
			public List<Color> Colors;

			public PointF Velocity;
			public double Lifetime;

			public double Age;

			private int TexID;

			public Particle(PointF pos, PointF vel, float life, int textureID)
			{
				OriginalPos = pos;
				Position = pos;

				Velocity = vel;
				Lifetime = life;
				TexID = textureID;
				Drag = new PointF(0.5f, 0);
				Gravity = 10.0f;
				GravityDir = new PointF(0, 1);
			}

			public bool Update(float dt)
			{
				Age += dt;

				if (Age >= Lifetime)
					return false;

				Position = new PointF(Position.X + (Gravity * GravityDir.X * dt), Position.Y + (Gravity * GravityDir.Y * dt));
				Position = new PointF(Position.X + (Velocity.X * dt), Position.Y + (Velocity.Y * dt));

				Velocity = new PointF(Velocity.X - Drag.X * dt, Velocity.Y - Drag.Y * dt);

				return true;
			}

			public void Draw()
			{
				Drawing.Bitmap(Position, new SizeF(8, 8), 0, TexID);
			}
			#endregion
		}
	}
}
