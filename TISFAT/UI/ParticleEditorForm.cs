using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			pnl_GLArea.Controls.Add(GLContext);
		}

		private void ParticleEditorForm_Load(object sender, EventArgs e)
		{
			MainEmitter = new EmitterObject.Emitter();

			MainEmitter.Position = new PointF(200, 200);
			MainEmitter.EmissionRate = 0.6f;
			MainEmitter.EmissionVelocity = 10.0f;
			MainEmitter.EmissionVelocityVariance = 2.0f;
			MainEmitter.ParticleLifetime = 10.0f;

			//MainEmitter.Configure();

			//MainEmitter.duration = 80000.0f;
			//MainEmitter.position = new PointF(125, 200);
			//MainEmitter.positionVariance = new PointF(0, 0);
			//MainEmitter.life = 40f;
			//MainEmitter.lifeVariance = 1.0f;
			//MainEmitter._totalParticles = 400;
			//MainEmitter.emissionRate = 20;
			//MainEmitter.startColor = Color.FromArgb(255, 20, 60, 255);
			//MainEmitter.startColorVariance = Color.FromArgb(76, 0, 0, 48);
			//MainEmitter.endColor = Color.FromArgb(0, 199, 199, 255);
			//MainEmitter.endColorVariance = Color.FromArgb(0, 0, 0, 0);
			//MainEmitter.speed = 10;
			//MainEmitter.speedVariance = 10;
			//MainEmitter.angle = 90;
			//MainEmitter.angleVariance = 10;
			//MainEmitter.gravity = new PointF(0, 350.0f);
			//MainEmitter.radialAccel = 0;
			//MainEmitter.radialAccelVariance = 0;
			//MainEmitter.tangentialAccel = 0;
			//MainEmitter.tangentialAccelVariance = 0;

			//MainEmitter.Restart();

			GLContext_Init();
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
	}
}
