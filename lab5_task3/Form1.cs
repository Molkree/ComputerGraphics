using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5_task3
{
    public partial class Form1 : Form
    {
        Graphics g;
        float step = 0.05f;
        Point p;
        bool isDown;

        // for coordinates
        int dx, dy;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();

            // for coordinates
            dx = pictureBox1.Location.X;
            dy = pictureBox1.Location.Y;
            bezier();
        }

        private Point get_point(Point p)
        {
            Point p_new = p;
            p_new.X -= dx;
            p_new.Y -= dy;
            return p_new;
        }

        private PointF countB(float t)
        {
            Point p0 = get_point(point0.Location);
            Point p1 = get_point(point1.Location);
            Point p2 = get_point(point2.Location);
            Point p3 = get_point(point3.Location);

            float pow3 = (float)Math.Pow((1 - t), 3);
            float pow2 = (float)Math.Pow((1 - t), 2);
            
            float bx = p0.X * pow3 + 3 * p1.X * pow2 * t + 3 * p2.X * (1-t)*t*t + p3.X * t*t*t;
            float by = p0.Y * pow3 + 3 * p1.Y * pow2 * t + 3 * p2.Y * (1 - t) * t * t + p3.Y * t * t * t;
            return new PointF(bx, by);
        }

        private void bezier()
        {
            g.Clear(Color.White);
            Pen pen = Pens.Gray;
            g.DrawLine(pen, get_point(point0.Location), get_point(point1.Location));
            g.DrawLine(pen, get_point(point1.Location), get_point(point2.Location));
            g.DrawLine(pen, get_point(point2.Location), get_point(point3.Location));

            List<PointF> curve = new List<PointF>();
            for (float t = 0; t < 1; t += step)
                curve.Add(countB(t));

            curve.Add(get_point(point3.Location));
            g.DrawLines(Pens.Red, curve.ToArray());          
        }

        private void point_MouseMove(object sender, MouseEventArgs e)
        {
            Label l = sender as Label;
            Size s0 = new Size(e.X - p.X, e.Y - p.Y);
            if (e.Button == MouseButtons.Left)
            {
                l.Location += s0;
                bezier();
            }    
        }

        private void point_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;
        }

 
    }
}
