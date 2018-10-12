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

        // for coordinates
        int dx, dy;
        List<Label> labels = new List<Label>();

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();

            // for coordinates
            dx = pictureBox1.Location.X;
            dy = pictureBox1.Location.Y;
            label1.Text = "ПКМ по точке,\nчтобы удалить её";
            labels.Add(point0);
            labels.Add(point1);
            labels.Add(point2);
            labels.Add(point3);

            bezier();
        }

        private Point get_point(Point p)
        {
            Point p_new = p;
            p_new.X -= dx;
            p_new.Y -= dy;
            return p_new;
        }

        private static long GetBinCoeff(long N, long K)
        {
            // This function gets the total number of unique combinations based upon N and K.
            // N is the total number of items.
            // K is the size of the group.
            // Total number of unique combinations = N! / ( K! (N - K)! ).
            // This function is less efficient, but is more likely to not overflow when N and K are large.
            // Taken from:  http://blog.plover.com/math/choose.html
            //
            long r = 1;
            long d;
            if (K > N) return 0;
            for (d = 1; d <= K; d++)
            {
                r *= N--;
                r /= d;
            }
            return r;
        }

     /*   private float count_C(int i, int n)
        {
            int n_fact = 1;
            int i_fact = 1;
            int n_i_fact = 1;
            for (int k = 1; k < Math.Max(n, i); ++k)
            {
                if (k <= n)
                    n_fact *= k;
                if (k <= i)
                    i_fact *= k;
                if (k <= n - 1)
                    n_i_fact *= k;
            }
            return n_fact / (i_fact * n_i_fact);

        }*/

        private PointF countB(List<Point> pts, float t)
        {
            float bx = 0, by = 0;
            for (int i = 0; i < pts.Count; ++i)
            {
                float C = (float)GetBinCoeff(pts.Count, i);
                float pow1 = (float)Math.Pow(t, i);
                float pow2 = (float)Math.Pow(1 - t, pts.Count - i);
                bx += pts[i].X * C * pow1 * pow2;
                by += pts[i].Y * C * pow1 * pow2;
            }

            return new PointF(bx, by);
        }

  /*      private PointF countB(List<Point> pts, float t)
        {
            float bx, by;
            if (pts.Count == 2)
            {
                bx = pts[0].X * (1 - t) + pts[1].X * t;
                by = pts[0].Y * (1 - t) + pts[1].Y * t;
            }
            else if (pts.Count == 3)
            {
                bx = pts[0].X * (1 - t) * (1 - t) + 2 * pts[1].X * (1 - t) * t + pts[2].X * t * t;
                by = pts[0].Y * (1 - t) * (1 - t) + 2 * pts[1].Y * (1 - t) * t + pts[2].Y * t * t;
            }
            else
            {
                float pow3 = (float)Math.Pow((1 - t), 3);
                float pow2 = (float)Math.Pow((1 - t), 2);

                bx = pts[0].X * pow3 + 3 * pts[1].X * pow2 * t + 3 * pts[2].X * (1 - t) * t * t + pts[3].X * t * t * t;
                by = pts[0].Y * pow3 + 3 * pts[1].Y * pow2 * t + 3 * pts[2].Y * (1 - t) * t * t + pts[3].Y * t * t * t;
            }

            return new PointF(bx, by);
        }
*/
        bool on_one_line(Point p1, Point p2, Point p3)
        {
            // substitute p2 in the equation for line (p1, p3)
            return ((p1.Y - p3.Y)*p2.X + (p3.X - p1.X)*p2.Y + (p1.X*p3.Y - p3.X*p1.Y) == 0);
        }

        List<Point> prepare_points()
        {
            List<Point> tmp = new List<Point>();
            // add first two points
            tmp.Add(get_point(labels[0].Location));
            tmp.Add(get_point(labels[1].Location));

            for (int i = 2; i < labels.Count;)
            {
                // if there are three more points
                if (i + 2 < labels.Count)
                {
                    Point p0 = get_point(labels[i].Location);
                    Point p1 = get_point(labels[i + 1].Location);
                    Point p2 = get_point(labels[i + 2].Location);

                    // if this three points aren't in one line => create new point between 1st and 2nd
                    if (!on_one_line(p0, p1, p2))
                    {
                        Point mid = new Point((p0.X + p1.X) / 2, (p0.Y + p1.Y) / 2);
                        tmp.Add(p0);
                        tmp.Add(mid);
                        tmp.Add(p1);
                        i += 2;
                    }
                    else
                    {
                        tmp.Add(p0);
                        tmp.Add(p1);
                        tmp.Add(p2);
                        i += 3;
                    }
                }
                else // if there are less then 3 points, just add them
                {
                    tmp.Add(get_point(labels[i].Location));
                    ++i;
                }
            }

            return tmp;
        }

        private void bezier()
        {
            g.Clear(Color.White);
            Pen pen = Pens.Gray;
            List<Point> points = prepare_points();
            

//            g.DrawLine(pen, get_point(point0.Location), get_point(point1.Location));
 //           g.DrawLine(pen, get_point(point1.Location), get_point(point2.Location));
  //          g.DrawLine(pen, get_point(point2.Location), get_point(point3.Location));

            List<PointF> curve = new List<PointF>();
            for (float t = 0; t < 1; t += step)
                curve.Add(countB(points, t));
            // what is happening to the last point??? (points[19] for 4 points)
            curve.Add(points[points.Count - 1]);
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

        private void add_button_Click(object sender, EventArgs e)
        {
            Label l = new Label();

            int mid_x = pictureBox1.Width / 2;
            int mid_y = pictureBox1.Height / 2;

            l.Location = new Point(mid_x, mid_y);
            l.Name = "point" + (labels.Count - 1).ToString();
            l.Parent = pictureBox1;
            Image im = Image.FromFile("../../point.png");

            l.AutoSize = false;
            l.Image = im;
            l.Size = im.Size;
            l.BackColor = Color.White;
            l.BringToFront();
            l.MouseClick += point_MouseClick;
            l.MouseDown += point_MouseDown;
            l.MouseMove += point_MouseMove;
            pictureBox1.Controls.Add(l);
            labels.Add(l);
            bezier();
        }

        private void point_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (labels.Count == 2)
                {
                    MessageBox.Show("Нужно хотя бы две точки для построения кривой!", "Нельзя удалить", MessageBoxButtons.OK);
                }
                else
                {
                    Label l = sender as Label;
                    labels.Remove(l);
                    l.Dispose();
                    bezier();
                }
            }
        }

        private void point_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;
        }

 
    }
}
