using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            int center_x = pictureBox1.ClientSize.Width / 2;
            int center_y = pictureBox1.ClientSize.Height / 2;

            int size = 150;
            int z = 0;
            List<Point3d> pts = new List<Point3d>();
            pts.Add(new Point3d(center_x, center_y, z));
            pts.Add(new Point3d(center_x + size, center_y, z));
            pts.Add(new Point3d(center_x + size, center_y + size, z));
            pts.Add(new Point3d(center_x, center_y + size, z));

            Face f = new Face(pts);

            figure = make_cube(f);
            figure.show(g, pr);
            
        }


        public Polyhedron make_cube(Face f)
        {
            List<Face> faces = new List<Face>();
            // front face
            faces.Add(f);

            List<Point3d> l1 = new List<Point3d>();
                
            float cube_size = Math.Abs(f.points[0].X - f.points[1].X);

            // back face
            for (int i = 0; i < f.points.Count; ++i)
            {
                l1.Add(new Point3d(f.points[i].X, f.points[i].Y, f.points[i].Z + cube_size));
            } 
            Face f1 = new Face(l1);
            faces.Add(f1);

            // up face
            List<Point3d> l2 = new List<Point3d>();
            l2.Add(f1.points[0]);
            l2.Add(f1.points[1]);
            l2.Add(f.points[1]);
            l2.Add(f.points[0]);        
            Face f2 = new Face(l2);
            faces.Add(f2);

            // down face
            List<Point3d> l3 = new List<Point3d>();
            l3.Add(f1.points[3]);
            l3.Add(f1.points[2]);
            l3.Add(f.points[2]);
            l3.Add(f.points[3]);
            Face f3 = new Face(l3);
            faces.Add(f3);

            Polyhedron cube = new Polyhedron(faces);
            cube.cube_size = cube_size;
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
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == '-') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox_KeyPress_double(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == ',') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        void check_all_textboxes()
        {
            foreach (var c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = c as TextBox;
                    if (t.Text == "")
                    {
                        if (t.Name == "scaling_x" || t.Name == "scaling_y" || t.Name == "scaling_z")
                            t.Text = "1";
                        else t.Text = "0";
                    }
                }
            }
        }

        private void button_exec_Click(object sender, EventArgs e)
        {
            check_all_textboxes();
            make_rot_line();
            figure.show(g, pr, old_fig);
            //масштабируем и переносим относительно начала координат (сдвигом левой нижней точки в начало)
            //
            if (scaling_x.Text != "1" || scaling_y.Text != "1" || scaling_z.Text != "1" || 
                trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
            {
                //сначала переносим в начало
                figure.translate(-1 * figure.center.X, -1 * figure.center.Y, -1 * figure.center.Z);
                //делаем, что нужно
                if (scaling_x.Text != "1" || scaling_y.Text != "1")
                {
                    float x = float.Parse(scaling_x.Text);
                    float y = float.Parse(scaling_y.Text);
                    float z = float.Parse(scaling_z.Text);
                    figure.scale(x, y, x);

                }
                if (trans_x.Text != "0" || trans_y.Text != "0")
                {
                    figure.translate(Int32.Parse(trans_x.Text),
                                     Int32.Parse(trans_y.Text),
                                     Int32.Parse(trans_z.Text));
                }
                //переносим обратно
                figure.translate(figure.center.X, figure.center.Y, figure.center.Z);
            }

            //поворачиваем относительно введенной точки rotation_point
            if (rot_angle.Text != "0")
            {
 /*               Point3d rot_point 
                double r = Double.Parse(rot_angle.Text);
                figure.translate(-1 * rot_x, -1 * rotation_point.Y);
                pr.rotate(r);
                pr.translate(rotation_point.X, rotation_point.Y);
   */         }

            figure.show(g, pr, new_fig);
        }

        void make_rot_line()
        {
            switch (line_mod)
            {
                case rot_line_mod.LINE_X:
                    rot_line = new Edge(new Point3d(0, figure.center.Y, figure.center.Z),
                                        new Point3d(pictureBox1.ClientSize.Width, figure.center.Y, figure.center.Z));
                    break;
                case rot_line_mod.LINE_Y:
                    rot_line = new Edge(new Point3d(figure.center.X, 0, figure.center.Z),
                                        new Point3d(figure.center.X, pictureBox1.ClientSize.Height, figure.center.Z));
                    break;
                case rot_line_mod.LINE_Z:
                    rot_line = new Edge(new Point3d(figure.center.X, figure.center.Y, 0),
                                        new Point3d(figure.center.X, figure.center.Y, figure.cube_size));
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            line_mod = (rot_line_mod)(comboBox2.SelectedIndex);

//            make_line();

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (line_mod == rot_line_mod.OTHER)
            {
                // TODO
            }
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }
    }

    enum rot_line_mod { LINE_X = 0, LINE_Y, LINE_Z, OTHER };

}
