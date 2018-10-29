using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        delegate float Function(float x, float y);

        Pen new_fig = Pens.Black;
        Pen old_fig = Pens.LightGray;
        Graphics g;
        Projection pr = 0;
        Axis line_mod = 0;
        Polyhedron figure = null;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
            g.ScaleTransform(1, -1);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
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
                figure.show(g, pr, old_fig);
                // масштабируем и переносим относительно начала координат (сдвигом центра в начало)
                //
                if (scaling_x.Text != "1" || scaling_y.Text != "1" || scaling_z.Text != "1" ||
                    trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                {
                    // сначала переносим в начало
                    float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                    figure.translate(-old_x, -old_y, -old_z);
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
                    figure.translate(old_x, old_y, old_z);
                }

                // поворачиваем относительно нужной прямой
                if (rot_angle.Text != "0")
                {
                    if (line_mod != Axis.OTHER)
                    {
                        float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                        figure.translate(-old_x, -old_y, -old_z);
                        figure.rotate(double.Parse(rot_angle.Text, CultureInfo.CurrentCulture), line_mod);
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
                        figure.rotate(double.Parse(rot_angle.Text, CultureInfo.CurrentCulture), line_mod, rot_line);
                        figure.translate(Ax, Ay, Az);
                    }
                }

                figure.show(g, pr, new_fig);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            line_mod = (Axis)(comboBox2.SelectedIndex);
            if (line_mod == Axis.OTHER)
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
            //figure = null;
            g.Clear(Color.White);
            figure.show(g, pr, new_fig);
        }

        // Create hexahedron
        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_hexahedron();
            figure.show(g, pr);
        }

        // Create tetrahedron
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_tetrahedron();
            figure.show(g, pr);
        }

        // Create octahedron
        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_octahedron();
            figure.show(g, pr);
        }

        // Create icosahedron
        private void button4_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_icosahedron();
            figure.show(g, pr);
        }

        // Create dodecahedron
        private void button5_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_dodecahedron();
            figure.show(g, pr);
        }

        // отражение по х
        private void button6_Click(object sender, EventArgs e)
        {
            figure.show(g, pr, old_fig);
            figure.reflectX();
            figure.show(g, pr);
        }

        // отражение по y
        private void button7_Click(object sender, EventArgs e)
        {
            figure.show(g, pr, old_fig);
            figure.reflectY();
            figure.show(g, pr);
        }

        // отражение по z
        private void button8_Click(object sender, EventArgs e)
        {
            figure.show(g, pr, old_fig);
            figure.reflectZ();
            figure.show(g, pr);
        }


        // save_file_dialog
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            string text = "";
            if (figure != null)
                text = figure.to_string();
            System.IO.File.WriteAllText(filename, text);
        }

        // open_file_dialog
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);

            g.Clear(Color.White);
            figure = new Polyhedron(fileText);
            figure.show(g, pr);
        }

        // rotation_figure
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);

            g.Clear(Color.White);
            figure = new Polyhedron(fileText, Polyhedron.MODE_ROT);
            figure.show(g, pr);
        }

        // graphic
        private void button4_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            //MessageBox.Show(form2.x.ToString());
            //MessageBox.Show(form2.y);

            var f = form2.f;
            float x0 = form2.X0;
            float x1 = form2.X1;
            float y0 = form2.Y0;
            float y1 = form2.Y1;
            int cnt_of_breaks = form2.Cnt_of_breaks;

            form2.Dispose();

            // TODO

            float dx = (Math.Abs(x0) + Math.Abs(x1)) / cnt_of_breaks;
            float dy = (Math.Abs(y0) + Math.Abs(y1)) / cnt_of_breaks;

            List<Face> faces = new List<Face>();
            List<Point3d> pts0 = new List<Point3d>();
            List<Point3d> pts1 = new List<Point3d>();

            for (float x = x0; x < x1; x += dx)
            {
                for (float y = y0; y < y1; y += dy)
                {
                    float z = f(x, y);
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

            g.Clear(Color.White);
            figure = new Polyhedron(faces);
            figure.show(g, pr, new_fig);
        }
    }
}
