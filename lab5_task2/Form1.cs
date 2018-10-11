using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace lab5_task2
{
    public class XYComparer : Comparer<Point>
    {
        public override int Compare(Point x, Point y)
        {
            if (x.X == y.X && x.Y == y.Y) return 0;
            if (x.X < y.X)
                return -1;
            if (x.X == y.X && x.Y < y.Y) return -1;
            return 1;
        }
    }

    public partial class Form1 : Form
    {
        const int DEF_ROUGHNESS = 5;
        const int MAX_ROUGHNESS = 15;
        const int DEF_DELTA = 5;
        const int MAX_DELTA = 100;
        int right, left;
        Graphics g = null;
        Color background = Color.MidnightBlue;
        int Roughness;
        int delta;
        SortedSet<Point> m_d_points = null;
        const float eps = 1e-9f;
        public Form1()
        {
            InitializeComponent();

            trackBar1.Maximum = pictureBox1.Height - 1;
            trackBar2.Maximum = pictureBox1.Height - 1;
            trackBar1.Value = trackBar2.Value = (int)(trackBar1.Maximum / 2);
            right = left = trackBar1.Value;
            g = pictureBox1.CreateGraphics();
            textBox1.Text = DEF_ROUGHNESS.ToString();
            Roughness = DEF_ROUGHNESS;
            delta = DEF_DELTA;
            textBox2.Text = DEF_DELTA.ToString();
            m_d_points = new SortedSet<Point>(new XYComparer());
            drawStartPosition();
        }
        
        private void drawStartPosition()
        {
            g.Clear(background);
            Pen p = new Pen(Color.Black);
            g.DrawLine(p, 0, left, pictureBox1.Width, right);
        }

        //reset button
        private void button1_Click(object sender, EventArgs e)
        {
            drawStartPosition();
        }

        private void draw_mountain()
        {
            g.Clear(background);
            Point fict_left = new Point(0, pictureBox1.Height);
            Point fict_right = new Point(pictureBox1.Width, pictureBox1.Height);
            var arr = m_d_points.ToList();
            arr.Add(fict_right);
            arr.Add(fict_left);
            g.FillPolygon(new SolidBrush(Color.Black), arr.ToArray());
            //g.DrawLines(new Pen(Color.Black), arr.ToArray());
            Thread.Sleep(100);
        }

        private void midpoint_displacement(Point left, Point right)
        {
            Queue<Tuple<Point, Point>> q = new Queue<Tuple<Point, Point>>();
            Tuple<Point, Point> start = new Tuple<Point, Point>(left, right);
            q.Enqueue(start);

            Random random = new Random();
            while (q.Count != 0)
            {
                var el = q.Dequeue();
                Point l = el.Item1;
                Point r = el.Item2;
                if (Math.Abs(r.X - l.X) <= delta)
                    continue;
                float length = (float)Math.Sqrt((r.X - l.X) * (r.X - l.X) + (r.Y - l.Y) * (r.Y - l.Y));
                int mid_x = (r.X + l.X) / 2;
                float mid_y = (r.Y + l.Y) / 2;
                float rand = (float)random.NextDouble() * 2 - 1;
                mid_y += Roughness * 0.1f * length * rand;
                if (mid_y < eps)
                    mid_y = 0;
                else if (Math.Abs(mid_y - trackBar1.Maximum) < eps)
                    mid_y = trackBar1.Maximum;

                m_d_points.Add(new Point(mid_x, (int)mid_y));
                draw_mountain();
               // await 
                Tuple<Point, Point> el1 = new Tuple<Point, Point>(l, new Point(mid_x, (int)mid_y));
                Tuple<Point, Point> el2 = new Tuple<Point, Point>(new Point(mid_x, (int)mid_y), r);
                q.Enqueue(el1);
                q.Enqueue(el2);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            left = trackBar1.Maximum - trackBar1.Value;
            drawStartPosition();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            right = trackBar2.Maximum - trackBar2.Value;
            drawStartPosition();
        }

        //play button
        private void button2_Click(object sender, EventArgs e)
        {
            m_d_points.Clear();
            Point left_p = new Point(0, left);
            Point right_p = new Point(pictureBox1.Width, right);
            m_d_points.Add(left_p);
            m_d_points.Add(right_p);
            draw_mountain();
            midpoint_displacement(left_p, right_p);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        //delta textbox
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                delta = DEF_DELTA;
            else delta = Int32.Parse(textBox2.Text);
            if (delta > MAX_DELTA)
            {
                delta = MAX_DELTA;
                textBox2.Text = delta.ToString();
            }
        }

        //roughness
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                Roughness = DEF_ROUGHNESS;
            else Roughness = Int32.Parse(textBox1.Text);
            if (Roughness > MAX_ROUGHNESS)
            {
                Roughness = MAX_ROUGHNESS;
                textBox1.Text = Roughness.ToString();
            }
        }
    }
}
