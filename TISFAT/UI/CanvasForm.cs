using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using TISFAT.Util;

namespace TISFAT
{
    public partial class CanvasForm : Form
    {
        GLControl GLContext;
        bool Loaded;
        static int MSAASamples = 8;

        public CanvasForm(Control parent)
        {
            InitializeComponent();
            Loaded = false;

            GraphicsMode mode = new GraphicsMode(
                new ColorFormat(8, 8, 8, 8),
                8, 8, MSAASamples,
                new ColorFormat(8, 8, 8, 8), 2, false
            );
            GLContext = new GLControl(mode, 2, 0, GraphicsContextFlags.Default);
            GLContext.Dock = DockStyle.Fill;
            GLContext.VSync = true;
            GLContext.Paint += new PaintEventHandler(this.GLContext_Paint);
            GLContext.MouseMove += new MouseEventHandler(this.GLContext_MouseMove);
            Controls.Add(GLContext);

            // Setup stuff
            TopLevel = false;
            parent.Controls.Add(this);
        }

        private void Canvas_Load(object sender, EventArgs e)
        {
            GLContext_Init();
            Loaded = true;
        }

        #region GL Core Init
        private void GLContext_Init()
        {
            GLContext.MakeCurrent();

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Viewport(0, 0, GLContext.Width, GLContext.Height);
            GL.Ortho(0, GLContext.Width, GLContext.Height, 0, -1, 1);
            GL.Disable(EnableCap.DepthTest);
        }

        private void CanvasForm_Resize(object sender, EventArgs e)
        {
            if (Loaded)
                GLContext_Init();
        } 
        #endregion

        public void GLContext_Paint(object sender, PaintEventArgs e)
        {
            GLContext.MakeCurrent();

            GL.ClearColor(Color.White);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            Program.Form.ActiveProject.Draw(Program.Form.MainTimeline.GetCurrentFrame());

            GLContext.SwapBuffers();
        }

        public void GLContext_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Program.Form.ActiveProject.TryManipulate(Program.Form.MainTimeline.GetCurrentFrame(), e.Location) ? Cursors.Hand : Cursors.Default;
        }
    }
}
