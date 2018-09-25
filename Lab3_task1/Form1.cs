using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab3_task1
{
    public partial class Form1 : Form

    {

        Point lastPoint = Point.Empty;

        bool isMouseDown = new bool();

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
            isMouseDown = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (lastPoint != null)
                {
                    if (pictureBox1.Image == null) //if no available bitmap exists on the picturebox to draw on
                    {
                        Bitmap bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                        pictureBox1.Image = bmp;
                    }

                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        g.DrawLine(new Pen(Color.Black, 1), lastPoint, e.Location);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                    pictureBox1.Refresh();
                    lastPoint = e.Location;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            lastPoint = Point.Empty;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = null;
                Refresh();
            }
        }

        Bitmap bmp;
        Graphics graphics;
        void floodFill(int x, int y)
        {
            Color col = bmp.GetPixel(x, y);
            Color back = pictureBox1.BackColor;
            if (bmp.GetPixel(x, y).ToArgb() == pictureBox1.BackColor.ToArgb())
            {
                int left = x;
                int right = x;
                while (0 < left && bmp.GetPixel(left - 1, y).ToArgb() == pictureBox1.BackColor.ToArgb())
                    left -= 1;
                while (right + 1 < pictureBox1.ClientSize.Width && bmp.GetPixel(right + 1, y).ToArgb() == pictureBox1.BackColor.ToArgb())
                    right += 1;
                graphics.DrawLine(new Pen(Color.Black, 1), left, y, right, y);

                for (int i = left; i <= right; ++i)
                {
                    if (0 < y)
                        floodFill(i, y - 1);
                    if (y + 1 < pictureBox1.ClientSize.Height)
                        floodFill(i, y + 1);
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.X < pictureBox1.ClientSize.Width && e.Y >= 0 && e.Y < pictureBox1.ClientSize.Height)
            {
                bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
                graphics = Graphics.FromImage(bmp);
                floodFill(e.X, e.Y);
                pictureBox1.Image = bmp;
                pictureBox1.Refresh();
            }
        }
    }
}
