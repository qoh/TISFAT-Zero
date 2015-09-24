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
		public class Emitter
		{
			#region Old Code
			public PointF Position;

			public float EmissionRate;

			public float EmissionVelocity;
			public float EmissionVelocityVariance;
			public float EmissionAngle;
			public float EmissionAngleVariance;

			public float ParticleLifetime;
			public float ParticleLifetimeVariance;

			public bool OrientParticles;

			public List<Particle> Particles;
			public int ParticleTex;

			private float lastEmitTime;

			public Emitter()
			{
				Particles = new List<Particle>();
			}

			public void Emit(PointF Loc)
			{
				Random r = new Random();

				Particles.Add(new Particle(
					new PointF(Loc.X + r.Next(-(int)EmissionAngleVariance, (int)EmissionAngleVariance), Loc.Y + r.Next(-(int)EmissionAngleVariance, (int)EmissionAngleVariance)), 
					new PointF(EmissionVelocity, 0),
					ParticleLifetime,
					ParticleTex));


				lastEmitTime = 0;
			}

			public void Update(float dt)
			{
				List<Particle> ScheduledDelete = new List<Particle>();

				lastEmitTime += dt;

				if (lastEmitTime > EmissionRate)
					Emit(Position);

				foreach(Particle p in Particles)
				{
					if (!p.Update(dt))
						ScheduledDelete.Add(p);
				}

				foreach(Particle p in ScheduledDelete)
				{
					Particles.Remove(p);
				}
			}

			public void Draw()
			{
				foreach (Particle p in Particles)
				{
					p.Draw();
				}
			}
			#endregion
		}
	}
}
