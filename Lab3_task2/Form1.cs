using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_task2
{
    enum Mode {Border, Fill };

    public class YXComparer : Comparer<Point>
    {
        public override int Compare(Point x, Point y)
        {
            if (object.Equals(x, y)) return 0;
            if (x.Y < y.Y)
                return -1;
            if (x.Y == y.Y && x.X < y.X) return -1;
            return 1;
        }
    }

    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap with_border = null;
        Color bg_color;
        Mode mode = Mode.Border;
        Color fill_color = Color.White;
        List<Point> points = new List<Point>(); //точки после поиска границы,не отсортированы, чтобы ее нарисовать
        bool border_finded = false;
        Image curr = null;

        /*    Point find_start_point()
            {
                Bitmap bmp = pictureBox1.Image as Bitmap;

                for (int x = bmp.Width-1; x >= 0; --x)
                    for (int y = 1; y < bmp.Height; ++y)
                    {
                        Color c = bmp.GetPixel(x, y);
                        if (c.ToArgb() != Color.White.ToArgb())
                        {
                            while (c.ToArgb() != Color.White.ToArgb())
                            {
                                --x;
                                c = bmp.GetPixel(x, y);
                            }
                            ++x;
                            return new Point(x, y);
                        }
                    }


                return new Point(0, 0);
            }*/

        Point find_start_point(Point click)
        {
            //Bitmap bmp = pictureBox1.Image as Bitmap;
            Bitmap bmp = curr as Bitmap;

            int act_x = click.X * bmp.Width / pictureBox1.Width;
            int act_y = click.Y * bmp.Height / pictureBox1.Height;

            bg_color = bmp.GetPixel(act_x, act_y);

            for (int x = act_x; x < bmp.Width; ++x)
            {
                if (bmp.GetPixel(x, act_y).ToArgb() != bg_color.ToArgb())
                    return new Point(x, act_y);
            }
            return new Point(-1, -1);
        }

        Point new_point(int dir, Point p)
        {
            if (dir == 7)
                return new Point(p.X + 1, p.Y + 1);
            else if (dir == 6)
                return new Point(p.X, p.Y + 1);
            else if (dir == 5)
                return new Point(p.X - 1, p.Y + 1);
            else if (dir == 4)
                return new Point(p.X - 1, p.Y);
            else if (dir == 3)
                return new Point(p.X - 1, p.Y - 1);
            else if (dir == 2)
                return new Point(p.X, p.Y - 1);
            else if (dir == 1)
                return new Point(p.X + 1, p.Y - 1);
            else
                return new Point(p.X + 1, p.Y);
        }

        bool find_border(Point click)
        {
            points.Clear();
            //Bitmap bmp = pictureBox1.Image as Bitmap;
            Bitmap bmp = curr as Bitmap;
            Point pred_p = find_start_point(click);
            if (pred_p.X == -1 || pred_p.Y == -1)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Неверные координаты", "problem", buttons);
                points.Clear();
                return false;
            }
            else
            {
                points.Add(pred_p);
                int cnt_horizontal = 1;
                Point last_last_p = pred_p;
                Point last_p = pred_p;

                int dir = 6;
                int pred_dir = dir;
                Point p = pred_p;
                while (true)
                {
                    dir -= 2;
                    if (dir < 0)
                        dir += 8;

                    while (true)
                    {
                        p = new_point(dir, pred_p);
                        if (bmp.GetPixel(p.X, p.Y).ToArgb() != Color.White.ToArgb())
                        {
                            pred_p = p;
                            break;
                        }
                        else
                            ++dir;
                        if (dir > 7)
                            dir -= 8;
                    }
                    if (dir == (pred_dir + 4) % 8)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;
                        result = MessageBox.Show("Не выделено ни одного пикселя", "problem", buttons);
                        points.Clear();
                        return false;
                    }

                    if (points.Exists(pt => (pt.X == pred_p.X && pt.Y == pred_p.Y)))
                        break;
                    else
                    {
                        if (pred_p.Y == last_p.Y)
                        {
                            ++cnt_horizontal;
                        }
                        else
                        {
                            if (cnt_horizontal > 1)
                            {
                                points.Remove(last_p);
                                points.Add(last_last_p);
                                points.Add(last_p);
                                points.Add(last_p);
                            }
                            cnt_horizontal = 1;
                            last_last_p = pred_p;
                        }
                        if (cnt_horizontal > 2)
                        {
                            points.Remove(last_p);
                        }
                        last_p = pred_p;
                        points.Add(pred_p);
                        

                        pred_dir = dir;
                    }
                }
                return true;
            }
        }


        public Form1()
        {
            InitializeComponent();
            label1.BackColor = fill_color;
            colorDialog1.FullOpen = true;
            colorDialog1.Color = fill_color;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            curr = Bitmap.FromFile(openFileDialog1.FileName);
            pictureBox1.Image = curr;
            g = Graphics.FromImage(curr);
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();    
            with_border = null;
            points.Clear();
            border_finded = false;
        }

        private List<Point> calc_fill_border()
        {
            List<Point> res = new List<Point>(points);
            //TODO добавить поиск внутренних областей


            res.Sort(new YXComparer());
            return res;
        }

        private void time_to_fill(Point[] border)
        {
            Pen pen = new Pen(fill_color);
            for (int i = 0; i < border.Length; i += 2)
            {
                Point first = border[i];
                Point second = border[i + 1];
                if (first.X == second.X || first.X == second.X - 1)
                    continue;
                first.X = first.X + 1;
                second.X = second.X - 1;
                g.DrawLine(pen, first, second);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //if (pictureBox1.Image == null) return;
            if (curr == null) return;
            //включен режим поиска границы
            if (mode == Mode.Border)
            {
                //pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = curr;
                with_border = null;
                border_finded = find_border(e.Location);
                //with_border = pictureBox1.Image.Clone() as Bitmap;
                with_border = curr.Clone() as Bitmap;

                if (border_finded)
                {
                    //draw
                    Point pt = points[0];
                    using (Graphics gg = Graphics.FromImage(with_border))
                    {
                        foreach (Point npt in points)
                        {
                            gg.DrawLine(Pens.Red, pt, npt);
                            pt = npt;
                        }
                        gg.DrawLine(Pens.Red, pt, points[0]);
                    } 
                    //with_border.SetPixel(pt.X, pt.Y, Color.Red);
                    pictureBox1.Image = with_border;
                }
                else
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show("Выберите мышкой область внутри границы", "Не выбрана область", buttons);
                }
            }
            else
            {
                //режим заливки
                if (border_finded)
                {
                    //там красная граница, она мне не нравится, ее надо убрать
                    //pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
                    Point[] t = points.ToArray();
                    //костыль
                    if (t.Length % 2 == 1)
                    {
                        //TODO разобраться с ним наконец
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;
                        result = MessageBox.Show("Пора пилить костыль!", "Error", buttons);
                    }
                    else
                    {
                        pictureBox1.Image = curr;
                        List<Point> nborder = calc_fill_border();
                        time_to_fill(nborder.ToArray());
                    }
                }
                else
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show("Не выбрана область. Вернитесь в режим поиска границы", "Error", buttons);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mode == Mode.Border)
            {
                mode = Mode.Fill;
                button2.Text = "Поиск границы";
            }
            else
            {
                mode = Mode.Border;
                button2.Text = "Заливка";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            fill_color = colorDialog1.Color;
            label1.BackColor = fill_color;
        }
    }
}
