using System.Collections.Generic;
using System.Drawing;

namespace TISFAT_ZERO
{
    abstract class Frame
    {
        private byte type;
        /*
         * 0 = Blank frame
         * 1 = Keyframe
         * 2 = Sound frame (?)
         */
        public Layer layer;
        public StickJoint[] Joints;

        public Frame(byte type = 0)
        {

            //TODO: Stuff.
        }

        public void updatePosition()
        {
            if (type == 1)
            {
                layer.figure.Joints = Joints;
                layer.figure.Draw(true);
            }
        }


        /*public void drawFrame(Graphics g, Pen lp, Pen rp, int x, int y) //x, y makes up top corner of pixel
        {
            g.DrawLines(lp, new Point[] { new Point(x + 8, y), new Point(x + 8, y + 15), new Point(x, y + 15) });
            g.FillRectangle(new SolidBrush(Color.LightGray), x, y, 8, 15);
        }

        public void drawFrame(Graphics g, Pen lp, Pen rp, int x, int y, char c) //x, y makes up top corner of pixel
        {
            g.DrawLines(lp, new Point[] { new Point(x + 8, y), new Point(x + 8, y + 15), new Point(x, y + 15) });
            g.FillRectangle(new SolidBrush(Color.LightGray), x, y, 8, 15);
            Font fo = SystemFonts.DefaultFont;

            g.DrawString("" + c, fo, Brushes.Black, new PointF(x - 1, y + 1));
        }*/

        abstract public void drawFrame(Graphics g, int x, int y);
    }

    class Layer
    {
        private List<Frame> frameList = new List<Frame>();
        public StickFigure figure;

        public Layer(StickFigure stick)
        {
            figure = stick;
        }
    }
}
