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
        Color fill_color = Color.BlueViolet;
        List<Point> points = new List<Point>(); //точки после поиска границы,не отсортированы, чтобы ее нарисовать
        bool border_finded = false;
        Image curr = null;
        Color border_color = Color.Black;
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
                //if (bmp.GetPixel(x, act_y).ToArgb() != bg_color.ToArgb())
                //    return new Point(x, act_y);
                if (bmp.GetPixel(x, act_y).ToArgb() == border_color.ToArgb())
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

        int max_y = int.MinValue;
        int min_y = int.MaxValue;

        bool find_border(Point click)
        {
            max_y = int.MinValue;
            min_y = int.MaxValue;
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
                       
                        if (bmp.GetPixel(p.X, p.Y).ToArgb() == border_color.ToArgb())
                        {
                            pred_p = p;
                            break;
                        }
                        else
                            ++dir;
                        if (dir > 7)
                            dir -= 8;
                    }
                /*    if (dir == (pred_dir + 4) % 8)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;
                        result = MessageBox.Show("Не выделено ни одного пикселя", "problem", buttons);
                        points.Clear();
                        return false;
                    }*/

                    if (points[0].X == pred_p.X && points[0].Y == pred_p.Y)
                        break;
                    else
                    {
                        if (pred_p.Y == last_p.Y)
                        {
                            ++cnt_horizontal;
                        }
                        else
                        {
                            cnt_horizontal = 1;
                        }
                        if (cnt_horizontal > 2)
                        {
                            points.Remove(last_p);
                        }

                        last_p = pred_p;
                        if (pred_p.Y < min_y)
                            min_y = pred_p.Y;
                        if (pred_p.Y > max_y)
                            max_y = pred_p.Y;
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
            label2.BackColor = border_color;
            colorDialog1.FullOpen = true;
            colorDialog1.Color = fill_color;
            colorDialog2.Color = border_color;
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
            mode = Mode.Border;
            button2.Text = "Заливка";
        }

        private List<Point> calc_fill_border()
        {
            //модифицируем границу для заливки
            List<Point> res = new List<Point>();
            res.Add(points[0]);
            int pred_dir = 6;
            int dir = 6;
            for (int i = 1; i < points.Count; ++i)
            {
                Point first = points[i-1];
                Point second = points[i];

      
                if (first.X == second.X)
                {
                    dir = second.Y > first.Y ? 6 : 2;
                  //  if (second.Y != max_y && second.Y != min_y)
                        res.Add(second);
                }
                else if (first.Y == second.Y)
                {
                    // !!!
                    dir = second.X > first.X ? 0 : 4;
                 //   if (second.Y != max_y && second.Y != min_y)
                        if ((dir == 4 && pred_dir == 5) || (dir == 0 && pred_dir == 1))
                        {
                            res.Remove(first);
                            res.Add(second);
                        }

                       
                }
                else
                {
                    if (second.Y < first.Y)
                        dir = second.X > first.X ? 1 : 3;
                    else dir = second.X > first.X ? 7 : 5;

                 //   if (second.Y != max_y && second.Y != min_y)
                  //  {
                        if ((dir == 5 && pred_dir == 3) || (dir == 3 && pred_dir == 5) || (dir == 1 && pred_dir == 7) || (dir == 7 && pred_dir == 1))
                            res.Remove(res[res.Count - 1]);
                        res.Add(second);
                 //   }
                }

   
                pred_dir = dir;
              
            }
            if (pred_dir == 0)
            {
                Point first = res[0];
                Point last = res[res.Count - 1];
                if (first.Y > last.Y)
                    res.Remove(last);
            }


            foreach (var pt in points)
            {
                if (pt.Y == min_y || pt.Y == max_y)
                    if (res.Exists(p => (p.X == pt.X && p.Y == pt.Y)))
                    res.Remove(pt);
            }
            //поиск внутренних границ
        /*    res.Sort(new YXComparer());
            Point[] tmp = res.ToArray();
            Bitmap bmp = curr as Bitmap;
            // поиск внутренних областей
            for (int i = 0; i < tmp.Length; i += 2)
            {
                Point first = tmp[i];
                Point second = tmp[i + 1];
 
                if (first.X == second.X || first.X == second.X - 1)
                    continue;

                bool prev_is_border = false;
                //сканируем область внутри?
                int cnt = 0;
                for (int j = first.X + 1; j < second.X; ++j)
                {
                    cnt = 0;
                    if (bmp.GetPixel(j, first.Y).ToArgb() == border_color.ToArgb())
                    {
                        if (!prev_is_border)
                        {
                            if (!res.Exists(pt => (pt.X == j && pt.Y == first.Y)))
                            {
                                res.Add(new Point(j, first.Y));
                                ++cnt;
                            }
                            prev_is_border = true;
                            
                        }
                    }
                    else
                    {
                        if (prev_is_border)
                        {
                            if (!res.Exists(pt => (pt.X == j - 1 && pt.Y == first.Y)))
                            {
                                res.Add(new Point(j - 1, first.Y));
                                ++cnt;
                            }
                            prev_is_border = false;
                        }
                    }
                }
                if (cnt % 2 == 1)
                    res.Add(res[res.Count - 1]);
            }*/

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
                if (first.X == second.X)
                    (curr as Bitmap).SetPixel(first.X, first.Y, fill_color);
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
                    //костыль
                    pictureBox1.Image = curr;
                    List<Point> nborder = calc_fill_border();
                    Point[] t = nborder.ToArray();
                    if (t.Length % 2 == 1)
                    {
                        //TODO разобраться с ним наконец
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;
                        result = MessageBox.Show("Пора пилить костыль!", "Error", buttons);
                    }
                    else
                    {
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
            if (fill_color.ToArgb() == Color.Black.ToArgb())
                label1.ForeColor = Color.White;
            else label1.ForeColor = Color.Black;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            border_color = colorDialog2.Color;
            label2.BackColor = border_color;
            if (border_color.ToArgb() == Color.White.ToArgb())
                label2.ForeColor = Color.Black;
            else label2.ForeColor = Color.White;
        }
    }
}
