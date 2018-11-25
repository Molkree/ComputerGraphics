using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace floating_horizon
{
    public partial class Form1 : Form
    {
        Polyhedron figure;
        Camera camera = new Camera();
        Graphics g;

        Color up_color = Color.Black;
        Color down_color = Color.LightGray;
        Bitmap bmp;
        //Projection pr = Projection.Z;

        Function f;
        Dictionary<double, double> Ymax = new Dictionary<double, double>();
        Dictionary<double, double> Ymin = new Dictionary<double, double>();

        //Axis line_mode = 0;

        public Form1()
        {
            InitializeComponent();

            //g = pictureBox1.CreateGraphics();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);


            g.ScaleTransform(1, -1);

            g.TranslateTransform(pictureBox1.ClientSize.Width / 2, -pictureBox1.ClientSize.Height / 2);
            
            comboBox1.SelectedIndex = 0;
            //comboBox2.SelectedIndex = 0;
            //comboBox3.SelectedIndex = 2;

            this.ActiveControl = button_gr;

            check_all_textboxes();
            label_coords.Text = camera.position.X + ", " + camera.position.Y + ", " + camera.position.Z;
            label_angles.Text = camera.angle_1 + ", " + camera.angle_2 + ", " + camera.angle_3;
        }


        // контроль вводимых символов
        private void textBox_KeyPress_int(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '-') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox_KeyPress_double(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == '-') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void button_gr_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    f = (x, y) => x + y;
                    break;
                case 1:
                    f = (x, y) => x * x + y * y;
                    break;
                case 2:
                    f = (x, y) => (float)Math.Cos(x * x + y * y + 1) / (x * x + y * y + 1);
                    break;
                case 3:
                    f = (x, y) => (float)(Math.Sin(x) * Math.Cos(y));
                    break;
                case 4:
                    f = (x, y) => (float)(Math.Sin(x) + Math.Cos(y));
                    break;
                default:
                    f = (x, y) => 0;
                    break;
            }

            int X0 = int.Parse(textBox_x0.Text);
            int X1 = int.Parse(textBox_x1.Text);
            int Y0 = int.Parse(textBox_y0.Text);
            int Y1 = int.Parse(textBox_y1.Text);
            int Cnt_of_breaks = (int)breaks_cnt.Value;

            float dx = (Math.Abs(X0) + Math.Abs(X1)) / (float)Cnt_of_breaks;
            float dy = (Math.Abs(Y0) + Math.Abs(Y1)) / (float)Cnt_of_breaks;


            List<Face> faces = new List<Face>();
            List<Point3d> pts0 = new List<Point3d>();
            List<Point3d> pts1 = new List<Point3d>();

            for (float x = X0; x < X1; x += dx)
            {
                for (float y = Y0; y < Y1; y += dy)
                {
                    float z = f(x, y);
                    //   graph_function.Add(z, new PointF(x, y));
                    pts1.Add(new Point3d(x, y, z));
                }
                // make faces
                if (pts0.Count != 0)
                    for (int i = 1; i < pts0.Count; ++i)
                    {
                        faces.Add(new Face(new List<Point3d>() {
                            new Point3d(pts0[i - 1]), new Point3d(pts1[i - 1]),
                            new Point3d(pts1[i]), new Point3d(pts0[i])
                        }));
                    }
                pts0.Clear();
                pts0 = pts1;
                pts1 = new List<Point3d>();
            }

            figure = new Polyhedron(faces);
            figure.is_graph = true;
            figure.graph_function = f;
            figure.graph_breaks = Cnt_of_breaks;

            show_plot();
        }
        /*
        private void button_exec_af_Click(object sender, EventArgs e)
        {
            if (figure == null)
            {
                MessageBox.Show("Сначала создайте фигуру", "Нет фигуры", MessageBoxButtons.OK);
            }
            else
            {
                check_all_textboxes();
                // масштабируем и переносим относительно начала координат (сдвигом центра в начало)
                //
                if (scaling.Text != "1" || trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                {
                    // сначала переносим в начало
                    float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                    figure.translate(-old_x, -old_y, -old_z);
                    
                    // scale
                    if (scaling.Text != "1")
                    {
                        float scale = float.Parse(scaling.Text, CultureInfo.CurrentCulture);
                        figure.scale(scale, scale, scale);   
                    }

                    // translate
                    if (trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                    {
                        int dx = int.Parse(trans_x.Text, CultureInfo.CurrentCulture),
                            dy = -int.Parse(trans_y.Text, CultureInfo.CurrentCulture),
                            dz = int.Parse(trans_z.Text, CultureInfo.CurrentCulture);
                        figure.translate(dx, dy, dz);

                    }

                    // переносим обратно
                    figure.translate(old_x, old_y, old_z);

                }

                // поворачиваем относительно нужной прямой
                if (rot_angle.Text != "0")
                {
                    float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                    figure.translate(-old_x, -old_y, -old_z);


                    double angle = double.Parse(rot_angle.Text, CultureInfo.CurrentCulture);
                    figure.rotate(angle, line_mode);
                       

                    figure.translate(old_x, old_y, old_z);
                       
                    
                }
               
            }
            show_plot();
        }
        */
        private void clear_button_Click(object sender, EventArgs e)
        {
            check_all_textboxes(true);
            textBox_x0.Text = "-10";
            textBox_x1.Text = "10";
            textBox_y0.Text = "-10";
            textBox_y1.Text = "10";

        }

        private void button_camera_Click(object sender, EventArgs e)
        {
            check_all_textboxes();
            
            // translate camera
            if (trans_x_camera.Text != "0" || trans_y_camera.Text != "0" || trans_z_camera.Text != "0")
            {
                int dx = int.Parse(trans_x_camera.Text, CultureInfo.CurrentCulture),
                    dy = int.Parse(trans_y_camera.Text, CultureInfo.CurrentCulture),
                    dz = int.Parse(trans_z_camera.Text, CultureInfo.CurrentCulture);
                

                camera.translate(dx, dy, dz);
            }

            // rotate
            if (!string.IsNullOrEmpty(angle_1.Text) ||
                !string.IsNullOrEmpty(angle_2.Text) ||
                !string.IsNullOrEmpty(angle_3.Text))
            {
                float a1 = 0, a2 = 0, a3 = 0;
                if (!string.IsNullOrEmpty(angle_1.Text))
                    a1 = float.Parse(angle_1.Text);
                if (!string.IsNullOrEmpty(angle_2.Text))
                    a2 = float.Parse(angle_2.Text);
                if (!string.IsNullOrEmpty(angle_3.Text))
                    a3 = float.Parse(angle_3.Text);

                camera.set_angles(a1, a2, a3);
                
            
            }
            show_plot();
            label_coords.Text = camera.position.X + ", " + camera.position.Y + ", " + camera.position.Z;
            label_angles.Text = camera.angle_1 + ", " + camera.angle_2 + ", " + camera.angle_3;
        }

        void check_all_textboxes(bool defalut = false)
        {
            if (string.IsNullOrEmpty(textBox_x0.Text) || defalut)
                textBox_x0.Text = "-10";
            if (string.IsNullOrEmpty(textBox_y0.Text) || defalut)
                textBox_x0.Text = "-10";
            if (string.IsNullOrEmpty(textBox_x1.Text) || defalut)
                textBox_x0.Text = "10";
            if (string.IsNullOrEmpty(textBox_y1.Text) || defalut)
                textBox_x0.Text = "10";
/*
            foreach (var c in panel2.Controls)
            {
                if (c is TextBox)
                {
                    var t = c as TextBox;
                    if (string.IsNullOrEmpty(t.Text) || defalut)
                    {
                        if (t.Name == "scaling" || t.Name == "rot_line_x2" || t.Name == "rot_line_y2" || t.Name == "rot_line_z2")
                            t.Text = "1";
                        else t.Text = "0";
                    }

                }
            }*/

            foreach (var c in panel3.Controls)
            {
                if (c is TextBox)
                {
                    var t = c as TextBox;
                    if (string.IsNullOrEmpty(t.Text) || defalut)
                    {
                        t.Text = "0";
                    }

                }
            }

        }

        public void show_plot()
        {

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = pictureBox1.Image as Bitmap;

            int x1 = int.Parse(textBox_x0.Text);
            int x2 = int.Parse(textBox_x1.Text);
            int y1 = int.Parse(textBox_y0.Text);
            int y2 = int.Parse(textBox_y1.Text);
            int cnt_of_breaks = (int)breaks_cnt.Value;

            PlotSurface(x1, y1, x2, y2, -1, 1, cnt_of_breaks, cnt_of_breaks);

            pictureBox1.Refresh();
        }

        //private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    pr = (Projection)comboBox3.SelectedIndex;
        //    show_plot();
        //}

        public float[,] Mul_matrix(float[,] m1, float[,] m2)
        {
            float[,] res = new float[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }

        private void DrawLine(Point p1, Point p2)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);

            int sx = p2.X >= p1.X ? 1 : -1;
            int sy = p2.Y >= p1.Y ? 1 : -1;


            int xs = p2.X > p1.X ? p1.X : p2.X;
            int xe = p2.X > p1.X ? p2.X : p1.X;
            for (int i = xs; i <= 2 * xe + dx; ++i)
                if (!Ymax.Keys.Contains(i))
                { Ymax[i] = Ymin[i] = Int32.MaxValue; }

            if (dy <= dx)
            {
                int d = -dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                for (int x = p1.X, y = p1.Y, i = 0; i <= dx; i++, x += sx)
                {
                    if (!Ymin.Keys.Contains(x))
                    {
                        Ymin.Add(x, Int32.MaxValue);
                        Ymax.Add(x, Int32.MaxValue);
                    }

                    if (Ymin[x] == Int32.MaxValue) // YMin, YMax not inited
                    {
                        if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height)
                            bmp.SetPixel(x, y, up_color);
                        Ymin[x] = Ymax[x] = y;
                    }
                    else
                    if (y < Ymin[x])
                    {
                        if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height)
                            bmp.SetPixel(x, y, up_color);
                        Ymin[x] = y;
                    }
                    else
                    if (y > Ymax[x])
                    {
                        if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height)
                            bmp.SetPixel(x, y, down_color);
                        Ymax[x] = y;
                    }

                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                }
            }
            else
            {
                int d = dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;

                if (!Ymin.Keys.Contains(p1.X))
                {
                    Ymin.Add(p1.X, Int32.MaxValue);
                    Ymax.Add(p1.X, Int32.MaxValue);
                }


                double m1 = Ymin[p1.X];
                double m2 = Ymax[p1.X];
                for (int x = p1.X, y = p1.Y, i = 0; i <= dy; i++, y += sy)
                {
                    if (!Ymin.Keys.Contains(x))
                    {
                        Ymax.Add(x, Int32.MaxValue);
                        Ymin.Add(x, Int32.MaxValue);
                    }

                    if (Ymin[x] == Int32.MaxValue) // YMin, YMax not inited
                    {
                        if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height)
                            bmp.SetPixel(x, y, up_color);
                        Ymin[x] = Ymax[x] = y;
                    }
                    else
                    if (y < m1)
                    {
                        if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height)
                            bmp.SetPixel(x, y, up_color);
                        if (y < Ymin[x])
                            Ymin[x] = y;

                    }
                    else
                    if (y > m2)
                    {
                        if (x >= 0 && x < pictureBox1.Width && y >= 0 && y < pictureBox1.Height)
                            bmp.SetPixel(x, y, down_color);
                        if (y > Ymax[x])
                            Ymax[x] = y;
                    }

                    if (d > 0)
                    {
                        d += d2;
                        x += sx;

                        if (!Ymin.Keys.Contains(x))
                        {
                            Ymax.Add(x, Int32.MaxValue);
                            Ymin.Add(x, Int32.MaxValue);
                        }

                        m1 = Ymin[x];
                        m2 = Ymax[x];
                    }
                    else
                        d += d1;
                }
            }

            pictureBox1.Image = bmp;
        }

        void PlotSurface(double x1, double y1, double x2, double y2, double fmin, double fmax, int n1, int n2)
        {
            Ymax.Clear();
            Ymin.Clear();

            List<Point> CurrLine = new List<Point>();
            List<Point> NextLine = new List<Point>();

            double phiz = camera.position.X * 3.1415926 / 180;
            double psiz = camera.position.Z * 3.1415926 / 180;

            double phi = 30 * 3.1415926 / 180 + phiz;
            double psi = 10 * 3.1415926 / 180 + psiz;
            double sphi = Math.Sin(phi);
            double cphi = Math.Cos(phi);
            double spsi = Math.Sin(psi);
            double cpsi = Math.Cos(psi);
            double[] e1 = { cphi, sphi, 0 };
            double[] e2 = { spsi * sphi, spsi * cphi, cpsi };

            double x, y, z;
            double hx = (x2 - x1) / n1;
            double hy = (y2 - y1) / n2;

            double xmin = (e1[0] >= 0 ? x1 : x2) * e1[0] + (e1[1] >= 0 ? y1 : y2) * e1[1];
            double xmax = (e1[0] >= 0 ? x2 : x1) * e1[0] + (e1[1] >= 0 ? y2 : y1) * e1[1];
            double ymin = (e2[0] >= 0 ? x1 : x2) * e2[0] + (e2[1] >= 0 ? y1 : y2) * e2[1];
            double ymax = (e2[0] >= 0 ? x2 : x1) * e2[0] + (e2[1] >= 0 ? y2 : y1) * e2[1];


            if (e2[2] >= 0)
            {
                ymin += fmin * e2[2];
                ymax += fmax * e2[2];
            }
            else
            {
                ymin += fmax * e2[2];
                ymax += fmin * e2[2];
            }
            double ax = 10 - pictureBox1.Width * xmin / (xmax - xmin);
            double bx = pictureBox1.Width / (xmax - xmin);
            double ay = 10 - pictureBox1.Height * ymin / (ymax - ymin);
            double by = pictureBox1.Height / (ymax - ymin);

            for (int i = 0; i < Math.Abs(x2 - x1); i++)
                Ymin[i] = Ymax[i] = Int32.MaxValue;

            for (int i = 0; i < n1; i++)
            {
                x = x1 + i * hx;
                y = y1 + (n2 - 1) * hy;
                z = f((float)x, (float)y);


                var t0 = Mul_matrix(camera.translateAtPosition(), new float[4, 1] { { (float)x }, { (float)y }, { (float)z }, { 1 } });
                var t1 = Mul_matrix(camera.translateAtAngles(),
                new float[3, 1] { { t0[0, 0] }, { t0[1, 0] }, { t0[2, 0] } });
                Point3d p = new Point3d(t1[0, 0], t1[1, 0], t1[2, 0]);

                int X = (int)(ax + bx * (p.X * e1[0] + p.Y * e1[1]));
                int Y = (int)(ay + by * (p.X * e2[0] + p.Y * e2[1] + p.Z * e2[2]));
                if (CurrLine.Count() > i)
                    CurrLine[i] = new Point(X, Y);
                else
                    CurrLine.Add(new Point(X, Y));
            }

            for (int i = n2 - 1; i > -1; i--)
            {
                for (int j = 0; j < n1 - 1; j++)
                    DrawLine(CurrLine[j], CurrLine[j + 1]);
                if (i > 0)
                    for (int j = 0; j < n1; j++)
                    {
                        x = x1 + j * hx;
                        y = y1 + (i - 1) * hy;
                        z = f((float)x, (float)y);


                        var t0 = Mul_matrix(camera.translateAtPosition(), new float[4, 1] { { (float)x }, { (float)y }, { (float)z }, { 1 } });
                        var t1 = Mul_matrix(camera.translateAtAngles(),
                        new float[3, 1] { { t0[0, 0] }, { t0[1, 0] }, { t0[2, 0] } });
                        Point3d p = new Point3d(t1[0, 0], t1[1, 0], t1[2, 0]);

                        int X = (int)(ax + bx * (p.X * e1[0] + p.Y * e1[1]));
                        int Y = (int)(ay + by * (p.X * e2[0] + p.Y * e2[1] + p.Z * e2[2]));
                        if (NextLine.Count() > j)
                            NextLine[j] = new Point(X, Y);
                        else
                            NextLine.Add(new Point(X, Y));
                        DrawLine(CurrLine[j], NextLine[j]);
                        CurrLine[j] = NextLine[j];
                    }
            }
            CurrLine.Clear();
            NextLine.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            camera.Reset();
            angle_1.Text = "0";
            angle_2.Text = "0";
            angle_3.Text = "0";
            trans_x_camera.Text = "0";
            trans_y_camera.Text = "0";
            trans_z_camera.Text = "0";

            label_coords.Text = camera.position.X + ", " + camera.position.Y + ", " + camera.position.Z;
            label_angles.Text = camera.angle_1 + ", " + camera.angle_2 + ", " + camera.angle_3;
            show_plot();
        }

        /*
        private void DrawLine(Point p1, Point p2)
        {
            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);

            int sx = p2.X >= p1.X ? 1 : -1;
            int sy = p2.Y >= p1.Y ? 1 : -1;

            int first_x = p2.X > p1.X ? p1.X : p2.X;
            int last_x = p2.X > p1.X ? p2.X : p1.X;

            for (int i = first_x; i <= 2 * last_x + dx; ++i)
                if (!Ymax.Keys.Contains(i))
                { Ymax[i] = Ymin[i] = Int32.MaxValue; }

            if (dy <= dx)
            {
                int d = -dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                for (int x = p1.X, y = p1.Y, i = 0; i <= dx; i++, x += sx)
                {
                    if (!Ymin.Keys.Contains(x))
                    {
                        Ymin.Add(x, Int32.MaxValue);
                        Ymax.Add(x, Int32.MaxValue);
                    }

                    if (Ymin[x] == Int32.MaxValue) // YMin, YMax not inited
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, up_color);
                        Ymin[x] = Ymax[x] = y;
                    }
                    else
                    if (y < Ymin[x])
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, up_color);
                        Ymin[x] = y;
                    }
                    else
                    if (y > Ymax[x])
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, down_color);
                        Ymax[x] = y;
                    }

                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                }
            }
            else
            {
                int d = -dy; // or just dy?
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;

                if (!Ymin.Keys.Contains(p1.X))
                {
                    Ymin.Add(p1.X, Int32.MaxValue);
                    Ymax.Add(p1.X, Int32.MaxValue);
                }

                double m1 = Ymin[p1.X];
                double m2 = Ymax[p1.X];

                for (int x = p1.X, y = p1.Y, i = 0; i <= dy; i++, y += sy)
                {
                    if (!Ymin.Keys.Contains(x))
                    {
                        Ymax.Add(x, Int32.MaxValue);
                        Ymin.Add(x, Int32.MaxValue);
                    }

                    if (Ymin[x] == Int32.MaxValue) // YMin, YMax not inited
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, up_color);
                        Ymin[x] = Ymax[x] = y;
                    }
                    else
                    if (y < m1)
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, up_color);
                        if (y < Ymin[x])
                            Ymin[x] = y;

                    }
                    else
                    if (y > m2)
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, down_color);
                        if (y > Ymax[x])
                            Ymax[x] = y;
                    }

                    if (d > 0)
                    {
                        d += d2;
                        x += sx;

                        if (!Ymin.Keys.Contains(x))
                        {
                            Ymax.Add(x, Int32.MaxValue);
                            Ymin.Add(x, Int32.MaxValue);
                        }

                        m1 = Ymin[x];
                        m2 = Ymax[x];
                    }
                    else
                        d += d1;
                }
            }

        }

        public void PlotSurface()
        {

            int x1 = int.Parse(textBox_x0.Text);
            int x2 = int.Parse(textBox_x1.Text);
            int y1 = int.Parse(textBox_y0.Text);
            int y2 = int.Parse(textBox_y1.Text);
            int cnt_of_breaks = (int)breaks_cnt.Value;

            HashSet<Point3d> pts = new HashSet<Point3d>(new Point3dEqComparer());

            
            int n1, n2;
            n1 = n2 = cnt_of_breaks;
            int fmin, fmax;

            fmin = -1;
            fmax = 1;
                       
            Ymax = new Dictionary<double, double>();
            Ymin = new Dictionary<double, double>();
            
            double phiz = camera.position.X * 3.1415926 / 180;
            double psiz = camera.position.Y * 3.1415926 / 180;


            double phi = 30 * Math.PI / 180 + phiz;
            double psi = 10 * Math.PI / 180 + psiz;

            double sphi = Math.Sin(phi);
            double cphi = Math.Cos(phi);
            double spsi = Math.Sin(psi);
            double cpsi = Math.Cos(psi);

            double[] e1 = { cphi, sphi, 0 };
            double[] e2 = { spsi * sphi, spsi * cphi, cpsi };

            double x, y, z;
            double hx = (x2 - x1) / n1;
            double hy = (y2 - y1) / n2;

            double xmin = (e1[0] >= 0 ? x1 : x2) * e1[0] + (e1[1] >= 0 ? y1 : y2) * e1[1];
            double xmax = (e1[0] >= 0 ? x2 : x1) * e1[0] + (e1[1] >= 0 ? y2 : y1) * e1[1];
            double ymin = (e2[0] >= 0 ? x1 : x2) * e2[0] + (e2[1] >= 0 ? y1 : y2) * e2[1];
            double ymax = (e2[0] >= 0 ? x2 : x1) * e2[0] + (e2[1] >= 0 ? y2 : y1) * e2[1];

            if (e2[2] >= 0)
            {
                ymin += fmin * e2[2];
                ymax += fmax * e2[2];
            }
            else
            {
                ymin += fmax * e2[2];
                ymax += fmin * e2[2];
            }

            double ax = 10 - pictureBox1.Width * xmin / (xmax - xmin);
            double bx = pictureBox1.Width / (xmax - xmin);
            double ay = 10 - pictureBox1.Height * ymin / (ymax - ymin);
            double by = pictureBox1.Height / (ymax - ymin);

            for (int i = 0; i < Math.Abs(x2 - x1); i++)
                Ymin[i] = Ymax[i] = Int32.MaxValue;

            for (int i = 0; i < Ymax.Count; ++i)
                Ymin[i] = Ymax[i] = Int32.MaxValue;

            Point[] CurLine = new Point[n1];
            Point[] NextLine = new Point[n1];

            for (int i = 0; i < n1; ++i)
            {
                x = x1 + i * hx;
                y = y1 + (n2 - 1) * hy;
                z = f((float)x, (float)y);

                var t0 = Mul_matrix(camera.translateAtPosition(), new float[4, 1] { { (float)x }, { (float)y }, { (float)z }, { 1 } });
                var t1 = Mul_matrix(camera.translateAtAngles(), new float[3, 1] { { t0[0, 0] }, { t0[1, 0] }, { t0[2, 0] } });

                Point3d p = new Point3d(t1[0, 0], t1[1, 0], t1[2, 0]);

                CurLine[i].X = (int)(ax + bx * (p.X * e1[0] + p.Y * e1[1]));
                CurLine[i].Y = (int)(ay + by * (p.X * e2[0] + p.Y * e2[1] + p.Z * e2[2]));
                //CurLine[i].X = (int)(ax + bx * (x * e1[0] + y * e1[1]));
                //CurLine[i].Y = (int)(ay + by * (x * e2[0] + y * e2[1] + z * e2[2]));
            }

            for (int i = n2 - 1; i > -1; --i)
            {
                for (int j = 0; j < n1 - 1; ++j)
                    DrawLine(CurLine[j], CurLine[j + 1]);

                if (i > 0)
                    for (int j = 0; j < n1; ++j)
                    {
                        x = x1 + j * hx;
                        y = y1 * (i - 1) + hy;
                        z = f((float)x, (float)y);

                        var t0 = Mul_matrix(camera.translateAtPosition(), new float[4, 1] { { (float)x }, { (float)y }, { (float)z }, { 1 } });
                        var t1 = Mul_matrix(camera.translateAtAngles(), new float[3, 1] { { t0[0, 0] }, { t0[1, 0] }, { t0[2, 0] } });

                        Point3d p = new Point3d(t1[0, 0], t1[1, 0], t1[2, 0]);

                        NextLine[j].X = (int)(ax + bx * (p.X * e1[0] + p.Y * e1[1]));
                        NextLine[j].Y = (int)(ay + by * (p.X * e2[0] + p.Y * e2[1] + p.Z * e2[2]));


                        //        NextLine[j].X = (int)(ax + bx * (x * e1[0] + y * e1[1]));
                        //        NextLine[j].Y = (int)(ay + by * (x * e2[0] + y * e2[1] + z * e2[2]));
                        DrawLine(CurLine[j], NextLine[j]);

                    }
            }

        }*/
    }
}
