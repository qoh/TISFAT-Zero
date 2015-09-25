using System;
using System.Drawing;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class EmitterObject
	{
		public class Particle
		{
			#region Old Code
			public ParticleSystem System;

			public Vector2F Position;
			public Vector2F Velocity;

			float MaxLifetime;
			float CurLifetime;

			private Random RandomGen;

			public Particle(ParticleSystem system, Random randomGen, Vector2F position)
			{
				System = system;
				RandomGen = randomGen;

				float angle = Interpolation.Interpolate((float)RandomGen.NextDouble(), System.EmissionAngleMin, System.EmissionAngleMax, EntityInterpolationMode.Linear);
				Vector2F direction = new Vector2F(1.0f, 0.0f).Rotate(angle);

				float speed = Interpolation.Interpolate((float)RandomGen.NextDouble(), System.EmissionSpeedMin, System.EmissionSpeedMax, EntityInterpolationMode.Linear);
				float offset = Interpolation.Interpolate((float)RandomGen.NextDouble(), System.EmissionOffsetMin, System.EmissionOffsetMax, EntityInterpolationMode.Linear);

				Position = position + direction * offset;
				Velocity = direction * speed;

				CurLifetime = 0.0f;
				MaxLifetime = Interpolation.Interpolate((float)RandomGen.NextDouble(), System.LifetimeMin, System.LifetimeMax, EntityInterpolationMode.Linear);
			}

			public bool Update(float dt)
			{
				CurLifetime += dt;

				if (CurLifetime >= MaxLifetime)
					return false;

				Velocity += System.ParticleDrag * Velocity * dt;
				Velocity += System.ParticleAcceleration * dt;
				Position += Velocity * dt;

				return true;
			}

			public void Draw()
			{
				float time = CurLifetime / MaxLifetime;

				float size = System.EvaluateParticleSize(time);
				Color color = System.EvaluateParticleColor(time);

				Drawing.Circle(new PointF(Position.X, Position.Y), size, color);
			}
			#endregion
		}
	}
}
