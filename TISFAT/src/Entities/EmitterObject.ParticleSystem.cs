using System;
using System.Collections.Generic;
using System.Drawing;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class EmitterObject
	{
		public class ParticleSystem
		{

			public float EmissionRate;
			public float EmissionAngleMin;
			public float EmissionAngleMax;
			public float EmissionSpeedMin;
			public float EmissionSpeedMax;
			public float EmissionOffsetMin;
			public float EmissionOffsetMax;
			public float LifetimeMin;
			public float LifetimeMax;

			public int ParticleTexture;
			public bool OrientParticles;

			public Vector2F ParticleDrag;
			public Vector2F ParticleAcceleration;
			public List<Tuple<float, float>> ParticleSize;
			public List<Tuple<float, Color>> ParticleColor;

			public float EvaluateParticleSize(float time)
			{
				int i;

				for (i = 0; i < ParticleSize.Count; i++)
				{
					if (ParticleSize[i].Item1 > time)
						break;
				}

				Tuple<float, float> next = ParticleSize[Math.Min(i, ParticleSize.Count - 1)];
				Tuple<float, float> curr = ParticleSize[Math.Max(i - 1, 0)];

				float t = Interpolation.Uninterpolate(time, curr.Item1, next.Item1);
				return Interpolation.Interpolate(t, next.Item2, curr.Item2, EntityInterpolationMode.Linear);
			}

			public Color EvaluateParticleColor(float time)
			{
				int i;

				for (i = 0; i < ParticleColor.Count; i++)
				{
					if (ParticleColor[i].Item1 > time)
						break;
				}

				Tuple<float, Color> next = ParticleColor[Math.Min(i, ParticleSize.Count - 1)];
				Tuple<float, Color> curr = ParticleColor[Math.Max(i - 1, 0)];

				float t = Interpolation.Uninterpolate(time, curr.Item1, next.Item1);
				return Interpolation.Interpolate(t, next.Item2, curr.Item2, EntityInterpolationMode.Linear);
			}
		}
	}
}
