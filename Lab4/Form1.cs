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
            pictureBox1.BackColor = Color.White;
            mode = Mode.None;
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
            //из за смещения начала координат
            PointF tmp1 = new PointF(pr.points[0].X, pictureBox1.Height - pr.points[0].Y);
            for (int i = 1; i < pr.points.Count; ++i)
            {
                PointF tmp2 = new PointF(pr.points[i].X, pictureBox1.Height - pr.points[i].Y);
                g.DrawLine(pen, tmp1, tmp2);
                tmp1 = tmp2;
            }
            g.DrawLine(pen, tmp1, new PointF(pr.points[0].X, pictureBox1.Height - pr.points[0].Y));
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

        //принадлежность точки ребру
        private int point_belongs(PointF e1, PointF e2, PointF pt)
        {
            float a = e1.Y - e2.Y;
            float b = e2.X - e1.X;
            float c = e1.X * e2.Y - e2.X * e1.Y;

            if (Math.Abs(a * pt.X + b * pt.Y + c) > eps)
                return -1;

            bool toedge = l_eq(Math.Min(e1.X, e2.X), pt.X) && l_eq(pt.X, Math.Max(e1.X, e2.X))
                        && l_eq(Math.Min(e1.Y, e2.Y), pt.Y) && l_eq(pt.Y, Math.Max(e1.Y, e2.Y));
            if (toedge)
                return 1;
            return -1;
        }

        private bool point_belongs(PointF pt)
        {
            //пускаем луч || Ох и считаем количество пересечений. четное - не принадлежит, нечетное - принадлежит
            //вернем -1 если нет, 0 если на ребре, 1 если принадлежит
            //если у ребра x1 = x2, то не учитываем его
            //если попали в нижнюю точку ребра, то не учитываем его
            int cnt = 0;
            PointF ray = new PointF(pictureBox1.Width, pt.Y);

            for (int i = 1; i <= pr.points.Count; ++i)
            {
                PointF tmp1 = pr.points[i - 1];
                PointF tmp2 = pr.points[i % pr.points.Count];
                if (point_belongs(tmp1, tmp2, pt) == 1)
                    return true;
                if (eq(tmp1.Y, tmp2.Y))
                    continue;
                if (eq(pt.Y, Math.Min(tmp1.Y, tmp2.Y)))
                    continue;
                if (eq(pt.Y, Math.Max(tmp1.Y, tmp2.Y)) && less(pt.X, Math.Min(tmp1.X, tmp2.X)))
                    ++cnt;
                else if (is_crossed(tmp1, tmp2, pt, ray))
                    ++cnt;
            }

            return cnt % 2 == 0? false : true;
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
                label2.Visible = false;
            }
            else // mode == mode.Read
            {
                if (pts.Count == 0)
                {
                    MessageBox.Show("Выберите хотя бы одну точку на поле", "Не выбрано ни одной точки", MessageBoxButtons.OK);
                }
                else
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
        }



        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PointF tmp = new PointF(e.X, pictureBox1.Height - e.Y);
            if (mode == Mode.Read)
            {
                //pts.Add(e.Location);
                pts.Add(tmp);
                //g.FillRectangle(Brushes.Black, e.X, e.Y, 1, 1);
                g.FillRectangle(Brushes.Black, tmp.X, pictureBox1.Height - tmp.Y, 1, 1);
            }
            //ввод точек для 1) справа или слева от ребра 2) пересекается ли со вторым ребром
            else if (mode == Mode.Edge)
            {
                //g.FillRectangle(Brushes.Black, e.X, e.Y, 1, 1);
                g.FillRectangle(Brushes.Black, tmp.X, pictureBox1.Height - tmp.Y, 1, 1);
                //справа или слева
                //int lr = left_or_right(pr.points[0], pr.points[1], e.Location); 
                int lr = left_or_right(pr.points[0], pr.points[1], tmp);
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
                    //to_edge.Add(e.Location);
                    to_edge.Add(tmp);
                    label_check_answ2.Text = "Еще один!";
                    ++cnt;
                }
                else if (cnt == 1)
                {
                    //TODO
                    //to_edge.Add(e.Location);
                    to_edge.Add(tmp);
                    //g.DrawLine(Pens.BlueViolet, to_edge[0], to_edge[1]);
                    g.DrawLine(Pens.BlueViolet, new PointF(to_edge[0].X, pictureBox1.Height - to_edge[0].Y), new PointF(to_edge[1].X, pictureBox1.Height - to_edge[1].Y));
                    bool f = is_crossed(pr.points[0], pr.points[1], to_edge[0], to_edge[1]);
                    if (f)
                        label_check_answ2.Text = "Пересекаются";
                    else
                        label_check_answ2.Text = "Увы :С";
                    label_check_answ2.Text += "\n(можно нажимать снова)";
                    cnt = 0;
                }
            }
            //ввод точек для "принадлежит ли полигону"
            else if (mode == Mode.Polygon)
            {
                //if (point_belongs(e.Location))
                if (point_belongs(tmp))
                    label_check_answ1.Text = "Принадлежит";
                else
                    label_check_answ1.Text = "Не принадлежит";
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
                pr.translate(Int32.Parse(textBox_trans_x.Text),
                             Int32.Parse(textBox_trans_y.Text));
            }
            if (textBox_rotation.Text != "0")
            {
                double r = Double.Parse(textBox_rotation.Text);
                pr.rotate(r, rotation_point);
            }
            if (textBox_scaling_x.Text != "1")
            {          
                int x = Int32.Parse(textBox_x.Text);
                int y = Int32.Parse(textBox_y.Text);
                pr.scaling(x, y);
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
            if (textBox_rotation.Text == "")
                textBox_rotation.Text = "0";
            int a = Int32.Parse(textBox_rotation.Text);
            if (a < 0 || a > 359)
            {
                // TODO поругаться
            }
        }

        private void textBox_rot_TextChanged(object sender, EventArgs e)
        {
            // При каждом изменении рисует новую точку - не очищать же весь picturebox каждый раз? :/
            if (textBox_x.Text == "")
                textBox_x.Text = "0";
            if (textBox_y.Text == "")
                textBox_y.Text = "0";

            int x = Int32.Parse(textBox_x.Text);
            int y = Int32.Parse(textBox_y.Text);

            //g.FillRectangle(Brushes.White, rotation_point.X, rotation_point.Y, 2, 2);
            g.FillRectangle(Brushes.White, rotation_point.X, pictureBox1.Height + rotation_point.Y, 2, 2);
            rotation_point.X = x;
            rotation_point.Y = y;
            //g.FillRectangle(Brushes.Red, x, y, 2, 2);
            g.FillRectangle(Brushes.Red, x, pictureBox1.Height + y, 2, 2);
        }

        private void textBox_scaling_TextChanged(object sender, EventArgs e)
        {
            if (textBox_scaling_x.Text == "")
                textBox_scaling_x.Text = "1";
            if (textBox_scaling_y.Text == "")
                textBox_scaling_y.Text = "1";
            if (textBox_trans_x.Text == "")
                textBox_trans_x.Text = "0";
            if (textBox_trans_y.Text == "")
                textBox_trans_y.Text = "0";
           

        }
    }
}

