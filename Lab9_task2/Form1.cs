using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lab9_task2
{
    public partial class Form1 : Form
    {
        Pen new_fig = Pens.Black;
        Graphics g;
        Projection pr = 0;
        Axis line_mode = 0;
        Polyhedron figure = null;
        Bitmap bmp, texture;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
            g.ScaleTransform(1, -1);
            comboBox2.SelectedIndex = 0;
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
                    if (string.IsNullOrEmpty(t.Text))
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
                // масштабируем и переносим относительно начала координат (сдвигом центра в начало)
                //
                if (scaling_x.Text != "1" || scaling_y.Text != "1" || scaling_z.Text != "1" ||
                    trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                {
                    // сначала переносим в начало
                    // left fig
                    float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                    figure.translate(-old_x, -old_y, -old_z);

                    // делаем, что нужно
                    if (scaling_x.Text != "1" || scaling_y.Text != "1" || scaling_z.Text != "1")
                    {
                        float x = float.Parse(scaling_x.Text, CultureInfo.CurrentCulture),
                            y = float.Parse(scaling_y.Text, CultureInfo.CurrentCulture),
                            z = float.Parse(scaling_z.Text, CultureInfo.CurrentCulture);
                        figure.scale(x, y, z);
                    }

                    if (trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                    {
                        int dx = int.Parse(trans_x.Text, CultureInfo.CurrentCulture),
                            dy = int.Parse(trans_y.Text, CultureInfo.CurrentCulture),
                            dz = int.Parse(trans_z.Text, CultureInfo.CurrentCulture);
                        figure.translate(dx, dy, dz);
                    }

                    // переносим обратно
                    figure.translate(old_x, old_y, old_z);
                }

                // поворачиваем относительно нужной прямой
                if (rot_angle.Text != "0")
                {
                    if (line_mode != Axis.OTHER)
                    {
                        float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                        figure.translate(-old_x, -old_y, -old_z);

                        double angle = double.Parse(rot_angle.Text, CultureInfo.CurrentCulture);
                        figure.rotate(angle, line_mode);

                        figure.translate(old_x, old_y, old_z);
                    }
                    else
                    {
                        Edge rot_line = new Edge(
                            new Point3d(
                                int.Parse(rot_line_x1.Text, CultureInfo.CurrentCulture),
                                int.Parse(rot_line_y1.Text, CultureInfo.CurrentCulture),
                                int.Parse(rot_line_z1.Text, CultureInfo.CurrentCulture)),
                            new Point3d(
                                int.Parse(rot_line_x2.Text, CultureInfo.CurrentCulture),
                                int.Parse(rot_line_y2.Text, CultureInfo.CurrentCulture),
                                int.Parse(rot_line_z2.Text, CultureInfo.CurrentCulture)));
                        float Ax = rot_line.P1.X, Ay = rot_line.P1.Y, Az = rot_line.P1.Z;
                        figure.translate(-Ax, -Ay, -Az);

                        double angle = double.Parse(rot_angle.Text, CultureInfo.CurrentCulture);
                        figure.rotate(angle, line_mode, rot_line);

                        figure.translate(Ax, Ay, Az);
                    }
                }

                g.Clear(Color.White);
                bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                pictureBox1.Image = bmp;
                figure.ApplyTexture(bmp, pictureBox1.Width, pictureBox1.Height);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            line_mode = (Axis)comboBox2.SelectedIndex;
            if (line_mode == Axis.OTHER)
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
        }

        // Create hexahedron
        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_hexahedron();

            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.Image = bmp;
            figure.ApplyTexture(bmp, pictureBox1.Width, pictureBox1.Height);
        }

        // Create tetrahedron
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_tetrahedron();

            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.Image = bmp;
            figure.ApplyTexture(bmp, pictureBox1.Width, pictureBox1.Height);
        }

        // Create octahedron
        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_octahedron();

            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.Image = bmp;
            figure.ApplyTexture(bmp, pictureBox1.Width, pictureBox1.Height);
        }

        // отражение по х
        private void button6_Click(object sender, EventArgs e)
        {
            if (figure != null)
            {
                figure.reflectX();
                g.Clear(Color.White);

                bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                pictureBox1.Image = bmp;
                figure.ApplyTexture(bmp, pictureBox1.Width, pictureBox1.Height);
            }
        }

        // отражение по y
        private void button7_Click(object sender, EventArgs e)
        {
            if (figure != null)
            {
                figure.reflectY();
                g.Clear(Color.White);

                bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                pictureBox1.Image = bmp;
                figure.ApplyTexture(bmp, pictureBox1.Width, pictureBox1.Height);
            }
        }

        // отражение по z
        private void button8_Click(object sender, EventArgs e)
        {
            if (figure != null)
            {
                figure.reflectZ();
                g.Clear(Color.White);

                bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                pictureBox1.Image = bmp;
                figure.ApplyTexture(bmp, pictureBox1.Width, pictureBox1.Height);
            }
        }

        // load texture
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                texture = Image.FromFile(openFileDialog1.FileName) as Bitmap;
                //Rectangle rectFlood = new Rectangle(0, 0, floodImage.Width, floodImage.Height);
                //bmp_dataFlood =
                //    floodImage.LockBits(rectFlood, ImageLockMode.ReadWrite,
                //    floodImage.PixelFormat);
                //ptrFlood = bmp_dataFlood.Scan0;
                //bytesFlood = Math.Abs(bmp_dataFlood.Stride) * floodImage.Height;
                //rgb_valuesFlood = new byte[bytesFlood];
                //System.Runtime.InteropServices.Marshal.Copy(ptrFlood, rgb_valuesFlood, 0, bytesFlood);

                //half_width_flood = floodImage.Width / 2;
                //half_height_flood = floodImage.Height / 2;
            }
        }
    }
}
