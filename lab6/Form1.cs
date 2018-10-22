using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        Pen new_fig = Pens.Black;
        Pen old_fig = Pens.LightGray;
        Graphics g;
        Projection pr = 0;
        rot_line_mod line_mod = 0;
        Edge rot_line;
        Polyhedron figure = null;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        // Create cube
        private void button1_Click(object sender, EventArgs e)
        {
            clear_button_Click(sender, e);
            int center_x = 150;
            int center_y = 150;

            int size = 150 / 2;
            int z = -size;
            List<Point3d> pts = new List<Point3d>
            {
                new Point3d(center_x - size, center_y - size, z),
                new Point3d(center_x + size, center_y - size, z),
                new Point3d(center_x + size, center_y + size, z),
                new Point3d(center_x - size, center_y + size, z)
            };

            Face f = new Face(pts);

            figure = new Polyhedron();
            figure.make_cube(f);

//            figure = make_cube(f);
            figure.show(g, pr);
        }

        static public Polyhedron make_cube(Face f)
        {
            List<Face> faces = new List<Face>
            {
                // back face
                f
            };

            List<Point3d> l1 = new List<Point3d>();
                
            float cube_size = Math.Abs(f.Points[0].X - f.Points[1].X);

            // front face
            foreach (var point in f.Points)
            {
                l1.Add(new Point3d(point.X, point.Y, point.Z + cube_size));
            }
            Face f1 = new Face(l1);
            faces.Add(f1);

            // up face
            List<Point3d> l2 = new List<Point3d>
            {
                new Point3d(f1.Points[0]),
                new Point3d(f1.Points[1]),
                new Point3d(f.Points[1]),
                new Point3d(f.Points[0])
            };
            Face f2 = new Face(l2);
            faces.Add(f2);

            // down face
            List<Point3d> l3 = new List<Point3d>
            {
                new Point3d(f1.Points[3]),
                new Point3d(f1.Points[2]),
                new Point3d(f.Points[2]),
                new Point3d(f.Points[3])
            };
            Face f3 = new Face(l3);
            faces.Add(f3);

            // left face
            List<Point3d> l4 = new List<Point3d>
            {
                new Point3d(f.Points[0]),
                new Point3d(f.Points[3]),
                new Point3d(f1.Points[3]),
                new Point3d(f1.Points[0])
            };
            Face f4 = new Face(l4);
            faces.Add(f4);

            // right face
            List<Point3d> l5 = new List<Point3d>
            {
                new Point3d(f.Points[1]),
                new Point3d(f.Points[2]),
                new Point3d(f1.Points[2]),
                new Point3d(f1.Points[1])
            };
            Face f5 = new Face(l5);
            faces.Add(f5);

            Polyhedron cube = new Polyhedron(faces)
            {
                Cube_size = cube_size
            };
            return cube;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pr = (Projection)(comboBox1.SelectedIndex);
            g.Clear(Color.White);
            if (figure != null)
                figure.show(g, pr);
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
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == ',') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        void check_all_textboxes()
        {
            foreach (var c in Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = c as TextBox;
                    if (t.Text == "")
                    {
                        if (t.Name == "scaling_x" || t.Name == "scaling_y" || t.Name == "scaling_z" || t.Name == "rot_line_x2" ||
                            t.Name == "rot_line_y2" || t.Name == "rot_line_z2")
                            t.Text = "1";
                        else t.Text = "0";
                    }
                }
            }
        }

        private void button_exec_Click(object sender, EventArgs e)
        {
            if (figure == null)
            {
                MessageBox.Show("Сначала создайте фигуру", "Нет фигуры", MessageBoxButtons.OK);
            }
            else
            {
                check_all_textboxes();
                make_rot_line();
                figure.show(g, pr, old_fig);
                // масштабируем и переносим относительно начала координат (сдвигом центра в начало)
                //
                if (scaling_x.Text != "1" || scaling_y.Text != "1" || scaling_z.Text != "1" ||
                    trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                {
                    // сначала переносим в начало
                    figure.translate(-1 * figure.Center.X, -1 * figure.Center.Y, -1 * figure.Center.Z);
                    // делаем, что нужно
                    if (scaling_x.Text != "1" || scaling_y.Text != "1" || scaling_z.Text != "1")
                    {
                        float x = float.Parse(scaling_x.Text, CultureInfo.CurrentCulture);
                        float y = float.Parse(scaling_y.Text, CultureInfo.CurrentCulture);
                        float z = float.Parse(scaling_z.Text, CultureInfo.CurrentCulture);
                        figure.scale(x, y, z);

                    }
                    if (trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                    {
                        figure.translate(int.Parse(trans_x.Text, CultureInfo.CurrentCulture),
                                         int.Parse(trans_y.Text, CultureInfo.CurrentCulture),
                                         int.Parse(trans_z.Text, CultureInfo.CurrentCulture));
                    }
                    // переносим обратно
                    figure.translate(figure.Center.X, figure.Center.Y, figure.Center.Z);
                }

                // поворачиваем относительно нужной прямой
                if (rot_angle.Text != "0")
                {
                    if (line_mod != rot_line_mod.OTHER)
                        figure.rotate(double.Parse(rot_angle.Text, CultureInfo.CurrentCulture), (axis)line_mod);
                    else
                    {
                        float line_x = rot_line.P1.X, line_y = rot_line.P1.Y, line_z = rot_line.P1.Z;
                        figure.translate(-line_x, -line_y, -line_z);
                        rot_line.translate(-line_x, -line_y, -line_z);
                        figure.rotate(double.Parse(rot_angle.Text, CultureInfo.CurrentCulture), (axis)line_mod, rot_line);
                        rot_line.translate(line_x, line_y, line_z);
                        figure.translate(line_x, line_y, line_z);
                    }
                }

                figure.show(g, pr, new_fig);
            }
        }

        void make_rot_line()
        {
            switch (line_mod)
            {
                case rot_line_mod.LINE_X:
                    rot_line = new Edge(new Point3d(0, figure.Center.Y, figure.Center.Z),
                                        new Point3d(pictureBox1.ClientSize.Width, figure.Center.Y, figure.Center.Z));
                    break;
                case rot_line_mod.LINE_Y:
                    rot_line = new Edge(new Point3d(figure.Center.X, 0, figure.Center.Z),
                                        new Point3d(figure.Center.X, pictureBox1.ClientSize.Height, figure.Center.Z));
                    break;
                case rot_line_mod.LINE_Z:
                    rot_line = new Edge(new Point3d(figure.Center.X, figure.Center.Y, 0),
                                        new Point3d(figure.Center.X, figure.Center.Y, figure.Cube_size));
                    break;
                case rot_line_mod.OTHER:
                    rot_line = new Edge(
                        new Point3d(int.Parse(rot_line_x1.Text), int.Parse(rot_line_y1.Text), int.Parse(rot_line_z1.Text)),
                        new Point3d(int.Parse(rot_line_x2.Text), int.Parse(rot_line_y2.Text), int.Parse(rot_line_z2.Text)));
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            line_mod = (rot_line_mod)(comboBox2.SelectedIndex);
            if (line_mod == rot_line_mod.OTHER)
            {
                rot_line_x1.Enabled = true;
                rot_line_y1.Enabled = true;
                rot_line_z1.Enabled = true;
                rot_line_x2.Enabled = true;
                rot_line_y2.Enabled = true;
                rot_line_z2.Enabled = true;
            }
            else
            {
                rot_line_x1.Enabled = false;
                rot_line_y1.Enabled = false;
                rot_line_z1.Enabled = false;
                rot_line_x2.Enabled = false;
                rot_line_y2.Enabled = false;
                rot_line_z2.Enabled = false;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //if (line_mod == rot_line_mod.OTHER)
            //{
            //    // TODO
            //}
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            foreach (var c in Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = c as TextBox;
                    if (t.Name == "scaling_x" || t.Name == "scaling_y" || t.Name == "scaling_z" || t.Name == "rot_line_x2" ||
                            t.Name == "rot_line_y2" || t.Name == "rot_line_z2")
                            t.Text = "1";
                    else t.Text = "0";
                    
                }
            }
           g.Clear(Color.White);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_tetraedr();
            figure.show(g, pr);
        }
    }

    enum rot_line_mod { LINE_X = 0, LINE_Y, LINE_Z, OTHER };
}
