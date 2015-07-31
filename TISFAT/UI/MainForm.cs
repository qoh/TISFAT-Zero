using System;
using System.Drawing;
using System.Windows.Forms;
using TISFAT.Entities;

namespace TISFAT
{
    public partial class MainForm : Form
    {
        public Project ActiveProject;
        public CanvasForm Canvas;
        public Timeline MainTimeline;

        public MainForm()
        {
            this.DoubleBuffered = true;

            InitializeComponent();

            #region General Init
            // Create and show forms
            Canvas = new CanvasForm(sc_MainContainer.Panel2);
            Canvas.Show();
            MainTimeline = new Timeline(GLContext);

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
            MainTimeline.GLContext_Init();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            MainTimeline.GLContext_Init();
            MainTimeline.Resize();
        }

        #region GLContext <-> Timeline hooks
        private void sc_MainContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            MainTimeline.GLContext_Init();
        }

        private void GLContext_Paint(object sender, PaintEventArgs e)
        {
            MainTimeline.GLContext_Paint();
        }

        private void GLContext_MouseMove(object sender, MouseEventArgs e)
        {
            MainTimeline.MouseMoved(e.Location);
        }

        private void GLContext_MouseLeave(object sender, EventArgs e)
        {
            MainTimeline.MouseLeft();
        }

        private void GLContext_MouseDown(object sender, MouseEventArgs e)
        {
            MainTimeline.MouseDown(e.Location);
        }

        private void GLContext_MouseUp(object sender, MouseEventArgs e)
        {
            MainTimeline.MouseUp();
        }
        #endregion
    }
}
