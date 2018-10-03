using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    enum Mode { None, Read, Point, Edge, Polygon };

    public partial class Form1 : Form
    {
        Graphics g = null;
        Mode mode;
        List<Point> pts;
        Primitive pr;

        public Form1()
        { 
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            mode = Mode.None;
            button_check.Enabled = false;
            button_check.Visible = false;
            label_check.Visible = false;
            label2.Visible = false;
        }

        // нарисовать примитив по точкам
        private void show_primitive()
        {
            if (pr.points.Count == 1)   // точка
            {
                g.FillRectangle(Brushes.Black, pr.points[0].X, pr.points[0].Y, 1, 1);
            }
            else if (pr.points.Count == 2)   // ребро
            {
                g.DrawLine(Pens.Black, pr.points[0], pr.points[1]);
            }
            else    // многоугольник
            {
                // TODO
                // запретить ставить точки так, чтобы линии пересекались
                Pen pen = Pens.Black;
                g.DrawLines(pen, pr.points.ToArray());
                g.DrawLine(pen, pr.points[0], pr.points[pr.points.Count-1]);
            }
        }

        private void button_make_Click(object sender, EventArgs e)
        {
            if (button_make.Text == "Задать примитив")
            {
                button_make.Text = "Готово";
                mode = Mode.Read;
                g.Clear(Color.White);
                pts = new List<Point>();

            }
            else // Готово
            {
                button_make.Text = "Задать примитив";
                pr = new Primitive(pts);
                show_primitive();
                if (pts.Count == 1)
                {
                    mode = Mode.Point;
                    button_check.Visible = false;
                    button_check.Enabled = false;
                    label_check.Visible = false;
                    label2.Visible = false;

                }
                else if (pts.Count == 2)
                {
                    mode = Mode.Edge;
                    button_check.Text = "Точка справа \nили слева?";
                    label_check.Text = "Щелкните мышкой \nпо графической области";
                    button_check.Visible = true;
                    button_check.Enabled = true;
                    label_check.Visible = true;
                    label2.Visible = true;
                }
                else
                {
                    mode = Mode.Polygon;
                    button_check.Text = "Принадлежит ли точка?";
                    label_check.Text = "Щелкните мышкой \nпо графической области";
                    button_check.Visible = true;
                    button_check.Enabled = true;
                    label_check.Visible = true;
                    label2.Visible = true;
                }   
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (mode == Mode.Read)
            {
                pts.Add(e.Location);
                g.FillRectangle(Brushes.Black, e.X, e.Y, 1, 1);

            }
            else { }
        }

        // очистка поля
        private void button_clear_Click(object sender, EventArgs e)
        {
            pts.Clear();
            g.Clear(Color.White);
            mode = Mode.None;
        }

        // сделать афинные преобразования
        private void button_exec_Click(object sender, EventArgs e)
        {
            if (textBox_trans_x.Text != "0" || textBox_trans_y.Text != "0")
                pr.translate(Int32.Parse(textBox_trans_x.Text),
                             Int32.Parse(textBox_trans_y.Text));
            if (textBox_rotation.Text != "0")
                pr.rotate(Double.Parse(textBox_rotation.Text));
            if (textBox_scaling.Text != "1")
                pr.scaling(Double.Parse(textBox_scaling.Text));

            show_primitive();
            
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
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == '.') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
    }
}

