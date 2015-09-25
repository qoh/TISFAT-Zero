using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TISFAT.Entities;
using TISFAT.Util;

namespace TISFAT
{
	public partial class ParticleEditorForm : Form
	{
		GLControl GLContext;
		static int MSAASamples = 8;

		EmitterObject.Emitter MainEmitter;

		public ParticleEditorForm()
		{
			InitializeComponent();

			GraphicsMode mode = new GraphicsMode(
				new ColorFormat(8, 8, 8, 8),
				8, 8, MSAASamples,
				new ColorFormat(8, 8, 8, 8), 2, false
			);
			GLContext = new GLControl(mode, 2, 0, GraphicsContextFlags.Default);
			GLContext.Dock = DockStyle.Fill;
			GLContext.VSync = true;

			GLContext.Paint += GLContext_Paint;
			GLContext.MouseMove += GLContext_MouseMove;
			GLContext.MouseDown += GLContext_MouseDown;

			pnl_GLArea.Controls.Add(GLContext);
		}

		private void ParticleEditorForm_Load(object sender, EventArgs e)
		{
			EmitterObject.ParticleSystem system = new EmitterObject.ParticleSystem();

			system.EmissionAngleMin = 270.0f;
			system.EmissionAngleMax = 275.0f;
			system.EmissionOffsetMax = 0.0f;
			system.EmissionOffsetMin = 0.0f;
			system.EmissionRate = 40.0f;
			system.EmissionSpeedMin = 35.0f;
			system.EmissionSpeedMax = 40.0f;
			system.LifetimeMin = 15.0f;
			system.LifetimeMax = 20.0f;
			system.ParticleAcceleration = new Vector2F(0, 3f);
			system.ParticleDrag = new Vector2F(0.0f, 0.0f);

			system.ParticleSize = new List<Tuple<float, float>>();
			system.ParticleSize.Add(new Tuple<float, float>(0.25f, 4));
			system.ParticleSize.Add(new Tuple<float, float>(0.5f, 6));
			system.ParticleSize.Add(new Tuple<float, float>(0.75f, 8));
			system.ParticleSize.Add(new Tuple<float, float>(1, 3));

			system.ParticleColor = new List<Tuple<float, Color>>();
			system.ParticleColor.Add(new Tuple<float, Color>(0, Color.FromArgb(255, 20, 60, 255)));
			system.ParticleColor.Add(new Tuple<float, Color>(1, Color.FromArgb(0, 199, 199, 255)));

			MainEmitter = new EmitterObject.Emitter(system);
			MainEmitter.Position = new Vector2F(200, 200);

			RefreshProperties();

			GLContext_Init();
		}

		private void RefreshProperties()
		{
			num_emissionRate.Value = (decimal)MainEmitter.System.EmissionRate;
			num_emissionAngleMin.Value = (decimal)MainEmitter.System.EmissionAngleMin;
			num_emissionAngleMax.Value = (decimal)MainEmitter.System.EmissionAngleMax;
			num_emissionSpeedMin.Value = (decimal)MainEmitter.System.EmissionSpeedMin;
			num_emissionSpeedMax.Value = (decimal)MainEmitter.System.EmissionSpeedMax;
			num_emissionOffsetMin.Value = (decimal)MainEmitter.System.EmissionOffsetMin;
			num_emissionOffsetMax.Value = (decimal)MainEmitter.System.EmissionOffsetMax;
			num_particleLifetimeMin.Value = (decimal)MainEmitter.System.LifetimeMin;
			num_particleLifetimeMax.Value = (decimal)MainEmitter.System.LifetimeMax;
			num_particleDragX.Value = (decimal)MainEmitter.System.ParticleDrag.X;
			num_particleDragY.Value = (decimal)MainEmitter.System.ParticleDrag.Y;
			num_particleGravityX.Value = (decimal)MainEmitter.System.ParticleAcceleration.X;
			num_particleGravityY.Value = (decimal)MainEmitter.System.ParticleAcceleration.Y;
		}

		public void GLContext_Init()
		{
			GLContext.MakeCurrent();

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Viewport(0, 0, GLContext.Width, GLContext.Height);
			GL.Ortho(0, GLContext.Width, GLContext.Height, 0, -1, 1);
			GL.Disable(EnableCap.DepthTest);
		}

		private void GLContext_Paint(object sender, PaintEventArgs e)
		{
			GLContext.MakeCurrent();

			GL.ClearColor(Color.FromArgb(81, 81, 145));

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit);
			
			MainEmitter.Update(1.0f / 4.0f);
			MainEmitter.Draw();

			GLContext.SwapBuffers();

			Application.DoEvents();

			GLContext.Invalidate();
		}

		private void GLContext_MouseDown(object sender, MouseEventArgs e)
		{
			MainEmitter.Position = new Vector2F(e.X, e.Y);
		}

		private void GLContext_MouseMove(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
				MainEmitter.Position = new Vector2F(e.X, e.Y);
		}

		#region numUpDown interaction 
		private void num_emissionRate_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.EmissionRate = (float)num_emissionRate.Value;
			MainEmitter.Reset();
		}

		private void num_emissionAngleMin_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.EmissionAngleMin = (float)num_emissionAngleMin.Value;
			MainEmitter.Reset();
		}

		private void num_emissionAngleMax_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.EmissionAngleMax = (float)num_emissionAngleMax.Value;
			MainEmitter.Reset();
		}

		private void num_emissionSpeedMin_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.EmissionSpeedMin = (float)num_emissionSpeedMin.Value;
			MainEmitter.Reset();
		}

		private void num_emissionSpeedMax_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.EmissionSpeedMax = (float)num_emissionSpeedMax.Value;
			MainEmitter.Reset();
		}

		private void num_emissionOffsetMin_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.EmissionOffsetMin = (float)num_emissionOffsetMin.Value;
			MainEmitter.Reset();
		}

		private void num_emissionOffsetMax_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.EmissionOffsetMax = (float)num_emissionOffsetMax.Value;
			MainEmitter.Reset();
		}

		private void num_particleLifetimeMin_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.LifetimeMin = (float)num_particleLifetimeMin.Value;
			MainEmitter.Reset();
		}

		private void num_particleLifetimeMax_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.LifetimeMax = (float)num_particleLifetimeMax.Value;
			MainEmitter.Reset();
		}

		private void num_particleDragX_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.ParticleDrag.X = -(float)num_particleDragX.Value;
			MainEmitter.Reset();
		}

		private void num_particleDragY_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.ParticleDrag.Y = -(float)num_particleDragY.Value;
			MainEmitter.Reset();
		}

		private void num_particleGravityX_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.ParticleAcceleration.X = (float)num_particleGravityX.Value;
			MainEmitter.Reset();
		}

		private void num_particleGravityY_ValueChanged(object sender, EventArgs e)
		{
			MainEmitter.System.ParticleAcceleration.Y = (float)num_particleGravityY.Value;
			MainEmitter.Reset();
		}
		#endregion


	}
}
