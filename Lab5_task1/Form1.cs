using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lab5_task1
{
    public partial class Form1 : Form
    {
        string axiom;
        double angle;
        double start_angle;
        Dictionary<char, string> rules;

        Graphics g;
        Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.Image = bmp;
        }

        private void Clear()
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<string> lines = new List<string>();
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName);
                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                file.Close();

                string[] tokens = lines[0].Split(' ');
                axiom = tokens[0];
                angle = double.Parse(tokens[1]);
                start_angle = double.Parse(tokens[2]);
                rules = new Dictionary<char, string>();
                for (int i = 1; i < lines.Count(); ++i)
                {
                    tokens = lines[i].Split('→'); // U+2192
                    rules.Add(tokens[0][0], tokens[1]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (axiom == null)
                return;
            string fractal = axiom;
            for (int i = 0; i < numericUpDown1.Value; ++i)
            {
                string next_level = "";
                for (int j = 0; j < fractal.Length; ++j)
                {
                    char next_char = fractal[j];
                    if (rules.ContainsKey(next_char))
                        next_level += rules[next_char];
                    else
                        next_level += next_char;
                }
                fractal = next_level;
            }

            List<Point> points = calculate_points(fractal);
            if (points.Count < 2)
                return;
            draw_fractal(points);
        }

        private List<Point> calculate_points(string fractal)
        {
            int len = 10000;
            List<Point> points = new List<Point> { new Point(pictureBox1.Width / 2, pictureBox1.Height / 2) };
            double direction = -start_angle;

            Stack<Tuple<Point, double>> stack = new Stack<Tuple<Point, double>>();
            for (int i = 0; i < fractal.Length; ++i)
                switch (fractal[i])
                {
                    case 'F':
                        Point p = points.Last();
                        p.Offset((int)(len * Math.Cos(direction * Math.PI / 180.0)), (int)(len * Math.Sin(direction * Math.PI / 180.0)));
                        points.Add(p);
                        points.Add(p);
                        break;
                    case '+':
                        direction = (direction + angle) % 360;
                        break;
                    case '-':
                        direction = (direction - angle) % 360;
                        break;
                    case '[':
                        stack.Push(new Tuple<Point, double>(points.Last(), direction));
                        break;
                    case ']':
                        direction = stack.Peek().Item2;
                        points.Add(points.Last());
                        points.Add(stack.Peek().Item1);
                        stack.Pop();
                        break;
                    default:
                        break;
                }
            return points;
        }

        private void draw_fractal(List<Point> points)
        {
            int min_x = points.Min(p => p.X);
            int min_y = points.Min(p => p.Y);
            int max_x = points.Max(p => p.X);
            int max_y = points.Max(p => p.Y);

            points = points.Select(p =>
            {
                if (max_x != min_x)
                    p.X = (pictureBox1.Width - 1) * (p.X - min_x) / (max_x - min_x);
                if (max_y != min_y)
                    p.Y = (pictureBox1.Height - 1) * (p.Y - min_y) / (max_y - min_y);
                return p;
            }).ToList();

            Clear();
            g = Graphics.FromImage(pictureBox1.Image);
            using (Pen pen = new Pen(Color.Black, 1))
                for (int i = 1; i < points.Count; i += 2)
                    g.DrawLine(pen, points[i - 1], points[i]);
            pictureBox1.Refresh();
        }
    }
}
