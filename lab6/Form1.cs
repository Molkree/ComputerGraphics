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
        Graphics g, g_camera;
        Projection pr = 0;
        Axis line_mode = 0, camera_mode = 0;
        Polyhedron figure = null, figure_camera = null;
        Camera camera = new Camera();

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

            g_camera = pictureBox2.CreateGraphics();
            g_camera.TranslateTransform(pictureBox2.ClientSize.Width / 2, pictureBox2.ClientSize.Height / 2);
            g_camera.ScaleTransform(1, -1);
            camera_x.Text = ((int)camera.view.P1.X).ToString(CultureInfo.CurrentCulture);
            camera_y.Text = ((int)camera.view.P1.Y).ToString(CultureInfo.CurrentCulture);
            camera_z.Text = ((int)camera.view.P1.Z).ToString(CultureInfo.CurrentCulture);
            camera_axis_picker.SelectedIndex = 0;
            create_camera();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pr = (Projection)comboBox1.SelectedIndex;
            g.Clear(Color.White);
            if (figure != null)
                figure.show(g, pr);

            camera.show(g, pr);
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

                    // right fig
                    float old_x_camera = figure_camera.Center.X,
                        old_y_camera = figure_camera.Center.Y,
                        old_z_camera = figure_camera.Center.Z;
                    figure_camera.translate(-old_x_camera, -old_y_camera, -old_z_camera);

                    // camera
                    //float cam_x = camera.P1.X, cam_y = camera.P1.Y, cam_z = camera.P1.Z;
                    //camera.translate(-cam_x, -cam_y, -cam_z);
                    

                    // делаем, что нужно
                    if (scaling_x.Text != "1" || scaling_y.Text != "1" || scaling_z.Text != "1")
                    {
                        float x = float.Parse(scaling_x.Text, CultureInfo.CurrentCulture),
                            y = float.Parse(scaling_y.Text, CultureInfo.CurrentCulture),
                            z = float.Parse(scaling_z.Text, CultureInfo.CurrentCulture);
                        figure.scale(x, y, z);
                        figure_camera.scale(x, y, z);

                        // camera
                        //camera.scale(x, y, z);
                    }
                    if (trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                    {
                        int dx = int.Parse(trans_x.Text, CultureInfo.CurrentCulture),
                            dy = int.Parse(trans_y.Text, CultureInfo.CurrentCulture),
                            dz = int.Parse(trans_z.Text, CultureInfo.CurrentCulture);
                        figure.translate(dx, dy, dz);
                        figure_camera.translate(dx, dy, dz);

                        // camera
                    //    camera.translate(dx, dy, dz);
                    }
                    // переносим обратно
                    figure.translate(old_x, old_y, old_z);
                    figure_camera.translate(old_x_camera, old_y_camera, old_z_camera);

                    // camera
                    //camera.translate(cam_x, cam_y, cam_z);
                    
                }

                // поворачиваем относительно нужной прямой
                if (rot_angle.Text != "0")
                {
                    if (line_mode != Axis.OTHER)
                    {
                        float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                        figure.translate(-old_x, -old_y, -old_z);
                        float old_x_camera = figure_camera.Center.X,
                            old_y_camera = figure_camera.Center.Y,
                            old_z_camera = figure_camera.Center.Z;
                        figure_camera.translate(-old_x_camera, -old_y_camera, -old_z_camera);

                        // camera
                        //float cam_x = camera.P1.X, cam_y = camera.P1.Y, cam_z = camera.P1.Z;
                        //camera.translate(-cam_x, -cam_y, -cam_z);

                        double angle = double.Parse(rot_angle.Text, CultureInfo.CurrentCulture);
                        figure.rotate(angle, line_mode);
                        figure_camera.rotate(angle, line_mode);

                        // camera
                        //camera.rotate(angle, line_mode);

                        figure.translate(old_x, old_y, old_z);
                        figure_camera.translate(old_x_camera, old_y_camera, old_z_camera);

                        // camera
                        //camera.translate(cam_x, cam_y, cam_z);

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
                        figure_camera.translate(-Ax, -Ay, -Az);

                        // camera
                        //camera.translate(-Ax, -Ay, -Az);

                        double angle = double.Parse(rot_angle.Text, CultureInfo.CurrentCulture);
                        figure.rotate(angle, line_mode, rot_line);
                        figure_camera.rotate(angle, line_mode, rot_line);

                        // camera
                        //camera.rotate(angle, line_mode, rot_line);

                        figure.translate(Ax, Ay, Az);
                        figure_camera.translate(Ax, Ay, Az);

                        // camera
                        //camera.translate(Ax, Ay, Az);
                    }
                }
                //figure.show(g, pr, old_fig);
                g.Clear(Color.White);
                figure.show(g, pr, new_fig);

                // camera
                camera.show(g, pr, new_fig);
                
                g_camera.Clear(Color.White);
                figure_camera.show_camera(g_camera, camera.view, new_fig);
            }
        }

        private void button_exec_camera_Click(object sender, EventArgs e)
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
                if (trans_x_camera.Text != "0" || trans_y_camera.Text != "0" || trans_z_camera.Text != "0"
                    || rot_angle_camera.Text != "0")
                {
                    // сначала переносим в начало
                    float old_x = figure_camera.Center.X, old_y = figure_camera.Center.Y, old_z = figure_camera.Center.Z;
                    figure_camera.translate(-old_x, -old_y, -old_z);

                    // try to move camera
                    float cam_x = camera.view.P1.X, cam_y = camera.view.P1.Y, cam_z = camera.view.P1.Z;
                    camera.translate(-cam_x, -cam_y, -cam_z);

                    // делаем, что нужно
                    if (trans_x_camera.Text != "0" || trans_y_camera.Text != "0" || trans_z_camera.Text != "0")
                    {
                        int dx = int.Parse(trans_x_camera.Text, CultureInfo.CurrentCulture),
                            dy = int.Parse(trans_y_camera.Text, CultureInfo.CurrentCulture),
                            dz = int.Parse(trans_z_camera.Text, CultureInfo.CurrentCulture);
                        figure_camera.translate(-dx, -dy, -dz);

                        // try to move camera
                        camera.translate(dx, dy, dz);

                        camera_x.Text = (int.Parse(camera_x.Text, CultureInfo.CurrentCulture) + dx).ToString(CultureInfo.CurrentCulture);
                        camera_y.Text = (int.Parse(camera_y.Text, CultureInfo.CurrentCulture) + dy).ToString(CultureInfo.CurrentCulture);
                        camera_z.Text = (int.Parse(camera_z.Text, CultureInfo.CurrentCulture) + dz).ToString(CultureInfo.CurrentCulture);
                    }
                    // поворачиваем относительно нужной прямой
                    if (rot_angle_camera.Text != "0")
                    {

                        float Ax = camera.rot_line.P1.X, Ay = camera.rot_line.P1.Y, Az = camera.rot_line.P1.Z;
                        figure_camera.translate(-Ax, -Ay, -Az);
                        figure_camera.rotate(-double.Parse(rot_angle_camera.Text, CultureInfo.CurrentCulture), camera_mode, camera.rot_line);
                        figure_camera.translate(Ax, Ay, Az);

                        // try to move camera
                        camera.rotate(double.Parse(rot_angle_camera.Text, CultureInfo.CurrentCulture), camera_mode);
                    }
                    // переносим обратно
                    figure_camera.translate(old_x, old_y, old_z);
                    
                    // try to move camera
                    camera.translate(cam_x, cam_y, cam_z);

                }

                // draw camera, draw figure
                g.Clear(Color.White);
                camera.show(g, pr);
                figure.show(g, pr);


                g_camera.Clear(Color.White);
                figure_camera.show_camera(g_camera, camera.view, new_fig);
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

        private void camera_axis_picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            camera_mode = (Axis)camera_axis_picker.SelectedIndex;
            camera.set_rot_line((Axis)camera_axis_picker.SelectedIndex);
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
            camera.show(g, pr);
        }

        private void create_camera()
        {
            camera = new Camera();
            camera_x.Text = ((int)camera.view.P1.X).ToString(CultureInfo.CurrentCulture);
            camera_y.Text = ((int)camera.view.P1.Y).ToString(CultureInfo.CurrentCulture);
            camera_z.Text = ((int)camera.view.P1.Z).ToString(CultureInfo.CurrentCulture);
            g_camera.Clear(Color.White);

            if (figure != null)
            {
                figure_camera = new Polyhedron(figure);
                figure_camera.translate(-camera.view.P1.X, -camera.view.P1.Y, -camera.view.P1.Z);
                figure_camera.show_camera(g_camera, camera.view, new_fig);
            }

            camera.show(g, pr);
        }
        
        // Create hexahedron
        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_hexahedron();
            figure.show(g, pr);

            create_camera();
        }

        // Create tetrahedron
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_tetrahedron();
            figure.show(g, pr);

            create_camera();
        }

        // Create octahedron
        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_octahedron();
            figure.show(g, pr);

            create_camera();
        }

        // Create icosahedron
        private void button4_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_icosahedron();
            figure.show(g, pr);

            create_camera();
        }

        // Create dodecahedron
        private void button5_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_dodecahedron();
            figure.show(g, pr);

            create_camera();
        }

        // отражение по х
        private void button6_Click(object sender, EventArgs e)
        {
            //figure.show(g, pr, old_fig);
            figure.reflectX();
            g.Clear(Color.White);
            figure.show(g, pr);

            camera.show(g, pr);
            figure_camera.reflectX();
            g_camera.Clear(Color.White);
            figure_camera.show_camera(g_camera, camera.view, new_fig);
        }

        // отражение по y
        private void button7_Click(object sender, EventArgs e)
        {
            //figure.show(g, pr, old_fig);
            figure.reflectY();
            g.Clear(Color.White);
            figure.show(g, pr);

            camera.show(g, pr);
            figure_camera.reflectY();
            g_camera.Clear(Color.White);
            figure_camera.show_camera(g_camera, camera.view, new_fig);
        }

        // отражение по z
        private void button8_Click(object sender, EventArgs e)
        {
            //figure.show(g, pr, old_fig);
            figure.reflectZ();
            g.Clear(Color.White);
            figure.show(g, pr);

            camera.show(g, pr);
            figure_camera.reflectZ();
            g_camera.Clear(Color.White);
            figure_camera.show_camera(g_camera, camera.view, new_fig);
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

            figure = new Polyhedron(fileText);
            g.Clear(Color.White);
            figure.show(g, pr);

            create_camera();
        }

        // rotation_figure
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);

            figure = new Polyhedron(fileText, Polyhedron.MODE_ROT);
            g.Clear(Color.White);
            figure.show(g, pr);

            create_camera();
        }

        // graphic
        private void button4_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();

            var f = form2.f;
            float x0 = form2.X0;
            float x1 = form2.X1;
            float y0 = form2.Y0;
            float y1 = form2.Y1;
            int cnt_of_breaks = form2.Cnt_of_breaks;

            form2.Dispose();

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
