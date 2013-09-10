using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace T0_StickEditor
{
    public static class Functions
    {
        //Method for centering handles.
        //TODO: Make this method more accurate.
        public static Point Center(this Rectangle rect)
        {
            return new Point(rect.Left - rect.Width / 2,
                             rect.Top - rect.Height / 2);
        }

        public static double DegToRads(double d)
        {
            return (Math.PI * d) / 180;
        }
    }
}
