using System;
using System.Collections.Generic;
using System.Drawing;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class EmitterObject
	{
		public class Emitter
		{
			#region Old Code
			public Vector2F Position;
			public ParticleSystem System;

			public List<Particle> Particles;
			
			private float emissionTimeAccum;
			private Random RandomGen;

			public Emitter(ParticleSystem system, Random randomGen = null)
			{
				System = system;
				RandomGen = randomGen ?? new Random();
				Particles = new List<Particle>();
				Reset();
			}

			public void Reset()
			{
				emissionTimeAccum = 0.0f;
			}

			public void ClearParticles()
			{
				Particles.Clear();
			}

			public void EmitParticle()
			{
				Particles.Add(new Particle(System, RandomGen, Position));
			}

			public void Update(float dt)
			{
				int index = 0;

				while (index < Particles.Count)
				{
					if (!Particles[index].Update(dt))
						Particles.RemoveAt(index);
					else
						index++;
				}

				emissionTimeAccum += dt;

				int count = (int)Math.Floor(emissionTimeAccum * System.EmissionRate);
				for (int i = 0; i < count; i++)
					EmitParticle();

				emissionTimeAccum -= (float)count / System.EmissionRate;
			}

			public void Draw()
			{
				foreach (Particle p in Particles)
					p.Draw();

				Drawing.Rectangle(new PointF(Position.X, Position.Y), new SizeF(4, 4), Color.Red);
			}
			#endregion
		}
	}
}
