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

        // Create cube
        private void button1_Click(object sender, EventArgs e)
        {
            clear_button_Click(sender, e);
            figure = new Polyhedron();
            figure.make_cube();

            figure.show(g, pr);
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
                            new Point3d(int.Parse(rot_line_x1.Text), int.Parse(rot_line_y1.Text), int.Parse(rot_line_z1.Text)),
                            new Point3d(int.Parse(rot_line_x2.Text), int.Parse(rot_line_y2.Text), int.Parse(rot_line_z2.Text)));
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
           figure = null;
           g.Clear(Color.White);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_tetraeder();
            figure.show(g, pr);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_octaeder();
            figure.show(g, pr);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_ikosaeder();
            figure.show(g, pr);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_dodecaeder();
            figure.show(g, pr);
        }

        //отражение по х
        private void button6_Click(object sender, EventArgs e)
        {
            figure.show(g, pr, old_fig);
            figure.reflectX();
            figure.show(g, pr);
        }

        //y
        private void button7_Click(object sender, EventArgs e)
        {
            figure.show(g, pr, old_fig);
            figure.reflectY();
            figure.show(g, pr);
        }

        //z
        private void button8_Click(object sender, EventArgs e)
        {
            figure.show(g, pr, old_fig);
            figure.reflectZ();
            figure.show(g, pr);
        }


        //save_file_dialog
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

        //open_file_dialog
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
    }
}
