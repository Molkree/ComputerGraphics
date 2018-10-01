using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        Bitmap floodImage = null;
        IntPtr ptrFlood;
        int bytesFlood;
        BitmapData bmp_dataFlood;
        private void button1_Click(object sender, EventArgs e)
        {
            if (floodImage != null)
            {
                System.Runtime.InteropServices.Marshal.Copy(rgb_valuesFlood, 0, ptrFlood, bytesFlood);
                floodImage.UnlockBits(bmp_dataFlood);
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                floodImage = ResizeImage(Image.FromFile(openFileDialog1.FileName), pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                Rectangle rectFlood = new Rectangle(0, 0, floodImage.Width, floodImage.Height);
                bmp_dataFlood =
                    floodImage.LockBits(rectFlood, ImageLockMode.ReadWrite,
                    floodImage.PixelFormat);
                ptrFlood = bmp_dataFlood.Scan0;
                bytesFlood = Math.Abs(bmp_dataFlood.Stride) * floodImage.Height;
                rgb_valuesFlood = new byte[bytesFlood];
                System.Runtime.InteropServices.Marshal.Copy(ptrFlood, rgb_valuesFlood, 0, bytesFlood);
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

        Bitmap bmp;
        BitmapData bmpData;
        byte[] rgbValues;
        byte[] rgb_valuesFlood;
        Queue<Tuple<int, int>> lines = new Queue<Tuple<int, int>>();
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
                    for (int i = left_point; i < left_point + 3; ++i)
                        rgbValues[i] = 0;

                    left_point -= 3;
                }
                left_point += 3;

                // right search
                int right_point = ind + 3;
                while (right_point < right_edge && rgbValues[right_point] == 255 && rgbValues[right_point + 1] == 255 && rgbValues[right_point + 2] == 255)
                {
                    for (int i = right_point; i < right_point + 3; ++i)
                        rgbValues[i] = 0;

                    right_point += 3;
                }
                right_point -= 3;
                if (floodImage != null)
                    lines.Enqueue(Tuple.Create(left_point, right_point));

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
            IntPtr ptr;
            int bytes;

            rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);

            floodFill(3 * e.X + e.Y * bmpData.Stride);

            if (floodImage != null)
                while (lines.Count != 0)
                {
                    var line = lines.Dequeue();
                    for (int i = line.Item1; i <= line.Item2; i += 3)
                        for (int j = i; j < i + 3; ++j)
                            rgbValues[j] = rgb_valuesFlood[j];
                }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
        }
    }
}
