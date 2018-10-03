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
    enum RotateMode { Default, Change }

    public partial class Form1 : Form
    {
        Graphics g = null;
        Mode mode;
        RotateMode rmode;
        Point rotation_point = new Point(0, 0);
        List<PointF> pts;
        Primitive pr;
        //для пересечения ребра и ребра
        int cnt = 0;
        List<PointF> to_edge;
        float eps = 1E-9f;

        public Form1()
        { 
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            mode = Mode.None;
            rmode = RotateMode.Default;
            label_check1.Visible = false;
            label_check_answ1.Visible = false;
            label2.Visible = false;
            label_check2.Text = "Для проверки пересечения\n дважды нажмите на поле";
            label_check2.Visible = false;
            label_check_answ2.Visible = false;
            to_edge = new List<PointF>();
        }

        bool eq(float d1, float d2)
        {
            return Math.Abs(d1 - d2) < eps;
        }

        bool less(float d1, float d2)
        {
            return (d1 < d2) && (Math.Abs(d1 - d2) >= eps);
        }

        bool l_eq(float b1, float b2)
        {
            return less(b1, b2) || eq(b1, b2);
        }

       //ничего не знаю, код с емакса
        private bool is_crossed(PointF first1, PointF first2, PointF second1, PointF second2)
        {
            float a1 = first1.Y - first2.Y;
            float b1 = first2.X - first1.X;
            float c1 = first1.X * first2.Y - first2.X * first1.Y;

            float a2 = second1.Y - second2.Y;
            float b2 = second2.X - second1.X;
            float c2 = second1.X * second2.Y - second2.X * second1.Y;

            float zn = a1 * b2 - a2 * b1;
            if (Math.Abs(zn) < eps)
                return false;
            float x = (-1) * (c1 * b2 - c2 * b1) / zn;
            float y = (-1) * (a1 * c2 - a2 * c1) / zn;

            if (eq(x, 0))
                x = 0;
            if (eq(y, 0))
                y = 0;

            bool tofirst = l_eq(Math.Min(first1.X, first2.X), x) && l_eq(x, Math.Max(first1.X, first2.X)) && l_eq(Math.Min(first1.Y, first2.Y), y) && l_eq(y, Math.Max(first1.Y, first2.Y));
            bool tosecond = l_eq(Math.Min(second1.X, second2.X), x) && l_eq(x, Math.Max(second1.X, second2.X)) && l_eq(Math.Min(second1.Y, second2.Y), y) && l_eq(y, Math.Max(second1.Y, second2.Y));
            return tofirst && tosecond;
        }

        // нарисовать примитив по точкам
        private void show_primitive()
        {
                // TODO
                // запретить ставить точки так, чтобы линии пересекались*/
                Pen pen = Pens.Black;
                if (pr.p_type != 1)
                //эта функция кидает исключения, если один элемент
                    g.DrawLines(pen, pr.points.ToArray());
                g.DrawLine(pen, pr.points[0], pr.points[pr.points.Count-1]);
        }

        // > 0 => точка слева
        // < 0 => точка справа
        int left_or_right(PointF p1, PointF p2, PointF to_check)
        {
            float xa = p2.X - p1.X;
            float ya = p2.Y - p1.Y;
            float xb = to_check.X - p1.X;
            float yb = to_check.Y - p1.Y;
            float res = yb * xa - xb * ya;
            if (less(res, 0f))
                return -1;
            else if (eq(res, 0f))
                return 0;
            else return 1;
        }

        bool point_belongs(PointF pt)
        {
            // TODO
            return false;
        }

        private void button_make_Click(object sender, EventArgs e)
        {
            if (mode != Mode.Read)
            {
                button_make.Text = "Готово";
                mode = Mode.Read;
                g.Clear(Color.White);
                pts = new List<PointF>();
                label_check1.Visible = false;
                label_check_answ1.Visible = false;
                label_check2.Visible = false;
                label_check_answ2.Visible = false;
            }
            else // mode == mode.Read
            {
                button_make.Text = "Задать примитив";
                pr = new Primitive(pts);
                show_primitive();
                if (pr.p_type == 1)
                {
                    mode = Mode.Point;
                    label_check1.Visible = false;
                    label_check_answ1.Visible = false;
                    label2.Visible = false;
                }
                else if (pr.p_type == 2)
                {
                    mode = Mode.Edge;
                    label_check1.Text = "Точка справа \nили слева?";
                    label_check_answ1.Text = "Щелкните мышкой \nпо полю";
                    label_check1.Visible = true;
                    label_check_answ1.Visible = true;
                    label2.Visible = true;
                    label_check2.Visible = true;
                    label_check_answ2.Visible = true;
                    label_check_answ2.Text = "Еще два раза";
                }
                else
                {
                    mode = Mode.Polygon;
                    label_check1.Text = "Принадлежит ли точка?";
                    label_check_answ1.Text = "Щелкните мышкой \nпо полю";
                    label_check1.Visible = true;
                    label_check_answ1.Visible = true;
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
            //ввод точек для 1) справа или слева от ребра 2) пересекается ли со вторым ребром
            else if (mode == Mode.Edge)
            {
                g.FillRectangle(Brushes.Black, e.X, e.Y, 1, 1);
                //справа или слева
                // TODO (вроде по формуле, но считает странно)
                int lr = left_or_right(pr.points[0], pr.points[1], e.Location); 
                if (lr > 0) // слева
                    label_check_answ1.Text = "Точка слева";
                else if (lr < 0)
                    label_check_answ1.Text = "Точка справа";
                else label_check_answ1.Text = "Точка находится на прямой отрезка";
                //пересекается ли
                if (cnt == 0)
                {
                    to_edge.Clear();
                    g.Clear(Color.White);
                    show_primitive();
                    to_edge.Add(e.Location);
                    label_check_answ2.Text = "Еще один!";
                    ++cnt;
                }
                else if (cnt == 1)
                {
                    //TODO
                    to_edge.Add(e.Location);
                    g.DrawLine(Pens.BlueViolet, to_edge[0], to_edge[1]);
                    bool f = is_crossed(pr.points[0], pr.points[1], to_edge[0], to_edge[1]);
                    if (f)
                        label_check_answ2.Text = "Пересекаются";
                    else
                        label_check_answ2.Text = "Увы :С";
                    label_check_answ2.Text += "\n(можно нажимать снова)";
                    cnt = 0;
                }
            }
            //ввод точек для 1) принадлежит ли полигону 2) поворот полигона вокруг точки
            else if (mode == Mode.Polygon)
            {
                if (point_belongs(e.Location))
                    label_check_answ1.Text = "Принадлежит";
                else label_check_answ1.Text = "Не принадлежит";
            }
        }

        // очистка поля
        private void button_clear_Click(object sender, EventArgs e)
        {
            pts.Clear();
            g.Clear(Color.White);
            mode = Mode.Read;
            label_check1.Visible = false;
            label_check_answ1.Visible = false;
            label2.Visible = false;
            label_check2.Visible = false;
            label_check_answ2.Visible = false;
            button_make.Text = "Готово";
        }

        // сделать афинные преобразования
        private void button_exec_Click(object sender, EventArgs e)
        {
            if (textBox_trans_x.Text != "0" || textBox_trans_y.Text != "0")
            {
                // проблемы с parse
                pr.translate(Double.Parse(textBox_trans_x.Text),
                             Double.Parse(textBox_trans_y.Text));
            }
            if (textBox_rotation.Text != "0")
            {
                int x = Int32.Parse(textBox_x.Text);
                int y = Int32.Parse(textBox_y.Text);
                double r = Double.Parse(textBox_rotation.Text);
                pr.rotate(r, new Point(x,y));
            }
            if (textBox_scaling.Text != "1")
            {
                pr.scaling(Double.Parse(textBox_scaling.Text));
            }

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

        private void textBox_rotation_TextChanged(object sender, EventArgs e)
        {
            int a = Int32.Parse(textBox_rotation.Text);
            if (a < 0 || a > 359)
            {
                // TODO поругаться
            }
        }

        private void textBox_x_y_TextChanged(object sender, EventArgs e)
        {
            // При каждом изменении рисует новую точку - не очищать же весь picturebox каждый раз? :/
            int x = Int32.Parse(textBox_x.Text);
            int y = Int32.Parse(textBox_y.Text);

            g.FillRectangle(Brushes.Red, x, y, 1, 1);
        }
    }
}

