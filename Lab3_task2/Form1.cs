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
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap with_border = null;
        Color bg_color;

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
            Bitmap bmp = pictureBox1.Image as Bitmap;
            

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

        List<Point> find_border(Point click)
        {
            List<Point> points = new List<Point>();
            Bitmap bmp = pictureBox1.Image as Bitmap;
            int cnt_horizontal = 0;
            int last_x = 0;
            Point pred_p = find_start_point(click);
            if (pred_p.X == -1 || pred_p.Y == -1)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Неверные координаты", "problem", buttons);
                points.Clear();
                return points;
            }
            else
            {
                points.Add(pred_p);
                /* label1.Text = pred_p.ToString();
                        bmp.SetPixel(pred_p.X, pred_p.Y, Color.Red);
                        pictureBox1.Refresh();
                  */


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
                        return points;
                    }

                    if (points.Exists(pt => (pt.X == pred_p.X && pt.Y == pred_p.Y)))
                        break;
                    else
                    {
                        points.Add(pred_p);
                        

                        pred_dir = dir;
                    }
                }
                return points;
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
            g = Graphics.FromImage(pictureBox1.Image);
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();    
            checkBox1.Checked = false;
            with_border = null;
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (with_border == null)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show("Выберите мышкой область внутри границы", "Не выбрана область", buttons);
                }

                pictureBox1.Image = with_border;

            }
            else
            {
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
            }
        }
        
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            with_border = null;
            List<Point> points = find_border(e.Location);
            with_border = pictureBox1.Image.Clone() as Bitmap;
            foreach (Point pt in points)
            {
                with_border.SetPixel(pt.X, pt.Y, Color.Red);
            }

        }
    }
}
