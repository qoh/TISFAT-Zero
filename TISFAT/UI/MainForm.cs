using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TISFAT.Entities;
using TISFAT.Util;

namespace TISFAT
{
    public partial class MainForm : Form
    {
        public Project ActiveProject;
        private string ProjectFileName;
        private bool ProjectDirty;

        public CanvasForm Canvas;
        public Timeline MainTimeline;

        private static Random Why = new Random();

        public MainForm()
        {
            this.DoubleBuffered = true;
            
            InitializeComponent();

            GLContext.KeyPress += MainForm_KeyPress;

            #region General Init
            // Create and show forms
            Canvas = new CanvasForm(sc_MainContainer.Panel2);
            Canvas.Show();
            MainTimeline = new Timeline(GLContext);

            ProjectNew();
            AddTestLayer();
            AddTestLayer();
            AddTestLayer();
            AddTestLayer();
            #endregion
        }

        private void AddTestLayer()
        {
            StickFigure figure = new StickFigure();

            var hip = new StickFigure.Joint();
            figure.Root = hip;
            var neck = new StickFigure.Joint(hip);
            hip.Children.Add(neck);

            Layer layer = new Layer(figure);
            layer.Framesets.Add(new Frameset());

            StickFigure.State state1 = new StickFigure.State();
            var ship = new StickFigure.Joint.State();
            ship.Location = new PointF(200f, 200f);
            state1.Root = ship;
            var sneck = new StickFigure.Joint.State(ship);
            sneck.Location = new PointF(200f, 147f);
            ship.Children.Add(sneck);
            layer.Framesets[0].Keyframes.Add(new Keyframe(0, state1));

            StickFigure.State state2 = new StickFigure.State();
            var ship2 = new StickFigure.Joint.State();
            ship2.Location = new PointF(200f, 200f);
            state2.Root = ship2;
            var sneck2 = new StickFigure.Joint.State(ship2);
            sneck2.Location = new PointF(100f, 147f);
            ship2.Children.Add(sneck2);
            layer.Framesets[0].Keyframes.Add(new Keyframe((uint)Why.Next(4, 12), state2));

            ActiveProject.Layers.Add(layer);
            MainTimeline.GLContext.Invalidate();
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
            MainTimeline.GLContext_Paint(sender, e);

            if (MainTimeline.IsRedrawing())
                MainTimeline.GLContext.Invalidate();
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

        public void SetDirty(bool dirty)
        {
            ProjectDirty = dirty;
            Text = "TISFAT Zero - " + (ProjectFileName ?? "Untitled") + (dirty ? " *" : "");
        }

        private void SetFileName(string filename)
        {
            ProjectFileName = filename;
            SetDirty(filename == null);
        }

        public void ProjectNew()
        {
            ActiveProject = new Project();
            SetFileName(null);
            MainTimeline.GLContext.Invalidate();
        }

        public void ProjectOpen(string filename)
        {
            ActiveProject = new Project();

            using (var reader = new BinaryReader(new FileStream(filename, FileMode.Open)))
            {
                UInt16 version = reader.ReadUInt16();
                ActiveProject.Read(reader, version);
            }

            SetFileName(filename);
            MainTimeline.GLContext.Invalidate();
        }

        public void ProjectSave(string filename)
        {
            using (var writer = new BinaryWriter(new FileStream(filename, FileMode.Create)))
            {
                writer.Write(FileFormat.Version);
                ActiveProject.Write(writer);
            }

            SetFileName(filename);
            ProjectFileName = filename;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectNew();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.AddExtension = true;
            dialog.Filter = "TISFAT Zero Project|*.tzp";
            
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                ProjectOpen(dialog.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectFileName != null)
                ProjectSave(ProjectFileName);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.Filter = "TISFAT Zero Project|*.tzp";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ProjectSave(dialog.FileName);
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("keypress");
            if (e.KeyChar == (char)Keys.Q)
                MainTimeline.Rewind();

            if (e.KeyChar == (char)Keys.Space)
                MainTimeline.TogglePause();
        }
    }
}
