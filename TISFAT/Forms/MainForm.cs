using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;
using TISFAT.Entities;

namespace TISFAT
{
    public partial class MainForm : Form
    {
        public Canvas MdiCanvas;
        public Project ActiveProject;

        public MainForm()
        {
            this.DoubleBuffered = true;

            InitializeComponent();

            #region General Init
            // Create and show forms
            MdiCanvas = new Canvas(sc_MainContainer.Panel2);
            MdiCanvas.Show();

            // Do this the wrong place
            ActiveProject = new Project();
            StickFigure figure = new StickFigure();

            var hip = new StickFigure.Joint();
            figure.Root = hip;
            var neck = new StickFigure.Joint(hip);
            hip.Children.Add(neck);

            Layer layer = new Layer(figure);

            StickFigure.State state1 = new StickFigure.State();
            var ship = new StickFigure.Joint.State();
            ship.Location = new PointF(200f, 200f);
            state1.Root = ship;
            var sneck = new StickFigure.Joint.State(ship);
            sneck.Location = new PointF(200f, 147f);
            ship.Children.Add(sneck);
            layer.Keyframes.Add(new Keyframe(0, state1));

            StickFigure.State state2 = new StickFigure.State();
            var ship2 = new StickFigure.Joint.State();
            ship2.Location = new PointF(200f, 200f);
            state2.Root = ship2;
            var sneck2 = new StickFigure.Joint.State(ship2);
            sneck2.Location = new PointF(100f, 147f);
            ship2.Children.Add(sneck2);
            layer.Keyframes.Add(new Keyframe(1, state2));

            ActiveProject.Layers.Add(layer); 
            #endregion
        }

        private void MainForm_Load(object sender, EventArgs e)
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

            GL.ClearColor(Color.Black);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GLContext.SwapBuffers();
        }

        private void sc_MainContainer_Panel1_Scroll(object sender, ScrollEventArgs e)
        {
            GLContext.Invalidate();
        }
    }
}
