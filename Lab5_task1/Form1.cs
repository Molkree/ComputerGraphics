using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        bool isRandom = false;

        Graphics g;
        public Form1()
        {
            InitializeComponent();
            Bitmap bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
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
                using (System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }

                string[] tokens = lines[0].Split(' ');
                axiom = tokens[0];
                angle = double.Parse(tokens[1], CultureInfo.InvariantCulture);
                start_angle = double.Parse(tokens[2], CultureInfo.InvariantCulture);
                rules = new Dictionary<char, string>();
                for (int i = 1; i < lines.Count(); ++i)
                {
                    tokens = lines[i].Split('→'); // U+2192
                    rules.Add(tokens[0][0], tokens[1]);
                }
            }
        }

        private class Line
        {
            public PointF P1, P2;
            public float thickness;
            public Color branch_color;

            public Line(PointF p1, PointF p2, float width, Color color = default(Color))
            {
                P1 = p1;
                P2 = p2;
                thickness = width;
                branch_color = color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (axiom == null)
                return;
            stdDev = (int)numericUpDown2.Value;
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

            List<Line> lines = calculate_lines(fractal);
            if (lines.Count < 1)
                return;
            draw_fractal(lines);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            label3.Text = (elapsedMs / 1000.0).ToString(CultureInfo.CurrentCulture);
        }

        static Random randomizer = new Random();
        private int stdDev;
        private List<Line> calculate_lines(string fractal)
        {
            int len = 50;
            PointF prev_point = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);
            List<Line> lines = new List<Line>();
            double direction = -start_angle;
            float width = 5;
            Color tree_color = Color.Green;

            Stack<Tuple<PointF, double, float, Color>> stack = new Stack<Tuple<PointF, double, float, Color>>();
            for (int i = 0; i < fractal.Length; ++i)
                switch (fractal[i])
                {
                    case 'F':
                        PointF p = prev_point;
                        p.X += (float)(len * Math.Cos(direction * Math.PI / 180.0));
                        p.Y += (float)(len * Math.Sin(direction * Math.PI / 180.0));
                        if (!isRandom)
                            lines.Add(new Line(prev_point, p, width));
                        else
                            lines.Add(new Line(prev_point, p, width, tree_color));
                        prev_point = p;
                        break;
                    case '+':
                        if (isRandom)
                        {
                            double u1 = 1.0 - randomizer.NextDouble(); //uniform(0,1] random doubles
                            double u2 = 1.0 - randomizer.NextDouble();
                            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                            double rand_angle =
                                         angle + stdDev * randStdNormal;
                            direction = (direction + rand_angle) % 360;
                        }
                        else
                        {
                            direction = (direction + angle) % 360;
                        }
                        break;
                    case '-':
                        if (isRandom)
                        {
                            double u1 = 1.0 - randomizer.NextDouble(); //uniform(0,1] random doubles
                            double u2 = 1.0 - randomizer.NextDouble();
                            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                            double rand_angle =
                                         angle + stdDev * randStdNormal;
                            direction = (direction - rand_angle) % 360;
                        }
                        else
                        {
                            direction = (direction - angle) % 360;
                        }
                        break;
                    case '[':
                        stack.Push(new Tuple<PointF, double, float, Color>(prev_point, direction, width, tree_color));
                        width -= 1;
                        tree_color = ControlPaint.Light(tree_color, 0.2f);
                        break;
                    case ']':
                        direction = stack.Peek().Item2;
                        prev_point = stack.Peek().Item1;
                        width = stack.Peek().Item3;
                        tree_color = stack.Peek().Item4;
                        stack.Pop();
                        break;
                    default:
                        break;
                }
            return lines;
        }

        private void draw_fractal(List<Line> lines)
        {
            float min_x = float.MaxValue, min_y = float.MaxValue, max_x = float.MinValue, max_y = float.MinValue;
            foreach (Line line in lines)
            {
                min_x = Math.Min(min_x, Math.Min(line.P1.X, line.P2.X));
                min_y = Math.Min(min_y, Math.Min(line.P1.Y, line.P2.Y));
                max_x = Math.Max(max_x, Math.Max(line.P1.X, line.P2.X));
                max_y = Math.Max(max_y, Math.Max(line.P1.Y, line.P2.Y));
            }

            lines = lines.Select(line =>
            {
                if (max_x != min_x && (pictureBox1.ClientSize.Width < max_x || min_x < 0))
                {
                    line.P1.X = (pictureBox1.Width - 1) * (line.P1.X - min_x) / (max_x - min_x);
                    line.P2.X = (pictureBox1.Width - 1) * (line.P2.X - min_x) / (max_x - min_x);
                }
                if (max_y != min_y && (pictureBox1.ClientSize.Height < max_y || min_y < 0))
                {
                    line.P1.Y = (pictureBox1.Height - 1) * (line.P1.Y - min_y) / (max_y - min_y);
                    line.P2.Y = (pictureBox1.Height - 1) * (line.P2.Y - min_y) / (max_y - min_y);
                }
                return line;
            }).ToList();

            Clear();
            g = Graphics.FromImage(pictureBox1.Image);
            if (isRandom)
                foreach (Line line in lines)
                    using (Pen pen = new Pen(line.branch_color, line.thickness))
                        g.DrawLine(pen, line.P1, line.P2);
            else
                using (Pen pen = new Pen(Color.Black, 1))
                    foreach (Line line in lines)
                        g.DrawLine(pen, line.P1, line.P2);
            pictureBox1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isRandom = !isRandom;
            if (isRandom)
                label4.Text = "On";
            else
                label4.Text = "Off";
        }
    }
}
