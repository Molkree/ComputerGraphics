using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
                        using (Pen black_pen = new Pen(Color.Black, 1))
                            g.DrawLine(black_pen, lastPoint, e.Location);
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

        private byte[] getRGBValues(out Bitmap bmp, out BitmapData bmp_data,
            out IntPtr ptr, out int bytes)
        {
            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            bmp_data =
                bmp.LockBits(rect, ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            ptr = bmp_data.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            bytes = Math.Abs(bmp_data.Stride) * bmp.Height;
            byte[] rgb_values = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgb_values, 0, bytes);

            return rgb_values;
        }

        BitmapData bmpData;
        byte[] rgbValues;
        void floodFill(int ind)
        {
            if (rgbValues[ind] == 255 && rgbValues[ind + 1] == 255 && rgbValues[ind + 2] == 255) // check if white
            {
                int left_edge = (ind / bmpData.Stride) * bmpData.Stride;
                int right_edge = left_edge + bmpData.Stride;

                // left search
                int left_point = ind;
                while (left_edge <= left_point && rgbValues[left_point] == 255 && rgbValues[left_point + 1] == 255 && rgbValues[left_point + 2] == 255)
                {
                    rgbValues[left_point] = 0;
                    rgbValues[left_point + 1] = 0;
                    rgbValues[left_point + 2] = 0;
                    rgbValues[left_point + 3] = 255;

                    left_point -= 4;
                }
                left_point += 4;

                // right search
                int right_point = ind + 4;
                while (right_point < right_edge && rgbValues[right_point] == 255 && rgbValues[right_point + 1] == 255 && rgbValues[right_point + 2] == 255)
                {
                    rgbValues[right_point] = 0;
                    rgbValues[right_point + 1] = 0;
                    rgbValues[right_point + 2] = 0;
                    rgbValues[right_point + 3] = 255;

                    right_point += 4;
                }
                right_point -= 4;

                // recursion
                for (int i = left_point; i <= right_point; i += 4)
                {
                    if (0 <= i - bmpData.Stride)
                        floodFill(i - bmpData.Stride);
                    if (i + bmpData.Stride < rgbValues.Length)
                        floodFill(i + bmpData.Stride);
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            rgbValues = getRGBValues(out Bitmap bmp, out bmpData, out IntPtr ptr, out int bytes);

            floodFill(4 * e.X + e.Y * bmpData.Stride);

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
        }
    }
}
