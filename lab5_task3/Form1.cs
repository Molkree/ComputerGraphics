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
        Label p0, p1, p2, p3;
        Point p;
        List<Label> labels = new List<Label>();

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            label1.Text = "ПКМ по точке,\nчтобы удалить её";

            // four initial points
            make_initial_labels();

            // why doesn't it work here?((
            bezier();
        }

        void make_initial_labels()
        {
            labels.Add(new_label(230, 230));
            labels.Add(new_label(270, 120));
            labels.Add(new_label(360, 100));
            labels.Add(new_label(420, 240));
        }

        // formula from lecture
        private PointF countB(List<Point> pts, float t)
        {
            float bx, by;
            float pow3 = (float)Math.Pow((1 - t), 3);
            float pow2 = (float)Math.Pow((1 - t), 2);

            bx = pts[0].X * pow3 + 3 * pts[1].X * pow2 * t + 3 * pts[2].X * (1 - t) * t * t + pts[3].X * t * t * t;
            by = pts[0].Y * pow3 + 3 * pts[1].Y * pow2 * t + 3 * pts[2].Y * (1 - t) * t * t + pts[3].Y * t * t * t;
           
            return new PointF(bx, by);
        }

        // just a middle point between p0 and p1 
        Point mid_point(Point p0, Point p1)
        {
            return new Point((p0.X + p1.X) / 2, (p0.Y + p1.Y) / 2);
        }

        private void bezier()
        {
            List<PointF> curve = new List<PointF>();

            g.Clear(Color.White);
            Pen pen = Pens.Gray;

            // I need all_points to add some middle points
            List<Point> all_points = new List<Point>();
            int count = labels.Count;
            for (int i = 0; i < count - 1; ++i)
                all_points.Add(labels[i].Location);

            // we need an even number of points
            if (count % 2 == 1)
            {
                all_points.Add(mid_point(labels[count - 2].Location, labels[count - 1].Location));
            }
            all_points.Add(labels[count - 1].Location);
            
            // four points to build a cubic bezier 
            List<Point> four_points = new List<Point>();
            four_points.Add(all_points[0]);
            four_points.Add(all_points[1]);
            four_points.Add(all_points[2]);

            for (int i = 3; i < all_points.Count;)
            {
                if (i >= all_points.Count - 2) // only two or less points left
                {
                    four_points.Add(all_points[i]);
                    ++i;                 
                }
                else // add new point between previous and current points
                { 
                    four_points.Add(mid_point(all_points[i - 1], all_points[i]));
                 
                    // built cubic curve
                    for (float t = 0; t < 1; t += step)
                        curve.Add(countB(four_points, t));

                    // remove first three points
                    four_points.Remove(four_points[0]);
                    four_points.Remove(four_points[0]);
                    four_points.Remove(four_points[0]);

                    // add next two points - now we again have three points
                    four_points.Add(all_points[i]);
                    four_points.Add(all_points[i+1]);
              
                    i += 2;
                }              
            }
            // built last cubic curve
            for (float t = 0; t < 1; t += step)
                curve.Add(countB(four_points, t));

            // add last point because we did't reach it before
            curve.Add(all_points[all_points.Count - 1]);

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

        Label new_label(int x, int y)
        {
            Label l = new Label();
            l.Location = new Point(x, y);
            int ind = 0;
            if (labels.Count > 0)
                ind = labels.Count - 1;
            l.Name = "point" + ind.ToString();
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

            return l;
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            int mid_x = pictureBox1.Width / 2;
            int mid_y = pictureBox1.Height / 2;
            labels.Add(new_label(mid_x, mid_y));
            bezier();
        }

        private void point_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (labels.Count == 4)
                {
                    MessageBox.Show("Нужно хотя бы 4 точки для построения кубической кривой!", "Нельзя удалить", MessageBoxButtons.OK);
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

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);

            foreach (Label l in labels)
                l.Dispose();
                
            labels.Clear();
            make_initial_labels();
        }

        private void point_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;
        }
    }
}
