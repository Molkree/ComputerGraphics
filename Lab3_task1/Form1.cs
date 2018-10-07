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

        Bitmap floodImage = null;
        IntPtr ptrFlood;
        int bytesFlood;
        BitmapData bmp_dataFlood;
        byte[] rgb_valuesFlood;
        int half_width_flood, half_height_flood;
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                clearButton_Click(sender, e);

                floodImage = Image.FromFile(openFileDialog1.FileName) as Bitmap;
                Rectangle rectFlood = new Rectangle(0, 0, floodImage.Width, floodImage.Height);
                bmp_dataFlood =
                    floodImage.LockBits(rectFlood, ImageLockMode.ReadWrite,
                    floodImage.PixelFormat);
                ptrFlood = bmp_dataFlood.Scan0;
                bytesFlood = Math.Abs(bmp_dataFlood.Stride) * floodImage.Height;
                rgb_valuesFlood = new byte[bytesFlood];
                System.Runtime.InteropServices.Marshal.Copy(ptrFlood, rgb_valuesFlood, 0, bytesFlood);

                half_width_flood = floodImage.Width / 2;
                half_height_flood = floodImage.Height / 2;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = null;
                Refresh();
            }
            if (floodImage != null)
            {
                System.Runtime.InteropServices.Marshal.Copy(rgb_valuesFlood, 0, ptrFlood, bytesFlood);
                floodImage.UnlockBits(bmp_dataFlood);
            }
            floodImage = null;
        }

        private byte[] getRGBValues(out Bitmap bmp, out BitmapData bmp_data,
            out IntPtr ptr, out int bytes)
        {
            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height, PixelFormat.Format24bppRgb);
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
        byte[] rgb_valuesFloodSwap;
        int start_x, start_y;
        void floodFill(int ind)
        {
            if (rgbValues[ind] == pictureBox1.BackColor.B && rgbValues[ind + 1] == pictureBox1.BackColor.G &&
                rgbValues[ind + 2] == pictureBox1.BackColor.R) // start floodfill only if clicked on background
            {
                int left_edge = (ind / bmpData.Stride) * bmpData.Stride;
                int right_edge = left_edge + bmpData.Stride;

                int left_point = ind;
                int right_point = ind + 3;
                // left search
                while (left_edge <= left_point && rgbValues[left_point] == pictureBox1.BackColor.B &&
                    rgbValues[left_point + 1] == pictureBox1.BackColor.G && rgbValues[left_point + 2] == pictureBox1.BackColor.R)
                {
                    // paint solid color
                    for (int i = left_point; i < left_point + 3; ++i)
                        rgbValues[i] = 0;

                    // copy from flood image to swap image
                    if (floodImage != null)
                    {
                        int cur_x = left_point % bmpData.Stride / 3; // divide by 3 to go from channels to pixels number
                        int cur_y = left_point / bmpData.Stride;
                        int flood_x;
                        if (cur_x < start_x)
                            flood_x = floodImage.Width - (start_x - cur_x + half_width_flood) % floodImage.Width - 1;
                        else
                            flood_x = (cur_x - start_x + half_width_flood) % floodImage.Width;
                        int flood_y;
                        if (cur_y < start_y)
                            flood_y = floodImage.Height - (start_y - cur_y + half_height_flood) % floodImage.Height - 1;
                        else
                            flood_y = (cur_y - start_y + half_height_flood) % floodImage.Height;
                        int flood_ind = 3 * flood_x + flood_y * bmp_dataFlood.Stride; // back to channels

                        rgb_valuesFloodSwap[left_point] = rgb_valuesFlood[flood_ind];
                        rgb_valuesFloodSwap[left_point + 1] = rgb_valuesFlood[flood_ind + 1];
                        rgb_valuesFloodSwap[left_point + 2] = rgb_valuesFlood[flood_ind + 2];
                    }

                    left_point -= 3;
                }
                left_point += 3;

                // right search
                while (right_point < right_edge && rgbValues[right_point] == pictureBox1.BackColor.B &&
                    rgbValues[right_point + 1] == pictureBox1.BackColor.G && rgbValues[right_point + 2] == pictureBox1.BackColor.R)
                {
                    // paint solid color
                    for (int i = right_point; i < right_point + 3; ++i)
                        rgbValues[i] = 0;

                    // copy from flood image to swap image
                    if (floodImage != null)
                    {
                        int cur_x = right_point % bmpData.Stride / 3; // divide by 3 to go from channels to pixels number
                        int cur_y = right_point / bmpData.Stride;
                        int flood_x;
                        if (cur_x < start_x)
                            flood_x = floodImage.Width - (start_x - cur_x + half_width_flood) % floodImage.Width - 1;
                        else
                            flood_x = (cur_x - start_x + half_width_flood) % floodImage.Width;
                        int flood_y;
                        if (cur_y < start_y)
                            flood_y = floodImage.Height - (start_y - cur_y + half_height_flood) % floodImage.Height - 1;
                        else
                            flood_y = (cur_y - start_y + half_height_flood) % floodImage.Height;
                        int flood_ind = 3 * flood_x + flood_y * bmp_dataFlood.Stride; // back to channels

                        rgb_valuesFloodSwap[right_point] = rgb_valuesFlood[flood_ind];
                        rgb_valuesFloodSwap[right_point + 1] = rgb_valuesFlood[flood_ind + 1];
                        rgb_valuesFloodSwap[right_point + 2] = rgb_valuesFlood[flood_ind + 2];
                    }

                    right_point += 3;
                }
                right_point -= 3;

                // recursion
                for (int i = left_point; i <= right_point; i += 3)
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
            Bitmap bmp;
            IntPtr ptr;
            int bytes;

            rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
            rgb_valuesFloodSwap = new byte[rgbValues.Length];
            rgbValues.CopyTo(rgb_valuesFloodSwap, 0);

            start_x = e.X;
            start_y = e.Y;
            floodFill(3 * e.X + e.Y * bmpData.Stride);

            if (floodImage != null)
                rgbValues = rgb_valuesFloodSwap;

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();

            Bitmap image = pictureBox1.Image as Bitmap;
            image.Save("../../flooded_lines.png", ImageFormat.Png);
        }
    }
}
