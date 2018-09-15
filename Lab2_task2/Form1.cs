using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Lab2_task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private byte[] getRGBValues(PictureBox pb, out Bitmap bmp, out System.Drawing.Imaging.BitmapData bmpData,
            out IntPtr ptr, out int bytes)
        {
            pb.Image = Image.FromFile(openFileDialog1.FileName);
            bmp = pb.Image as Bitmap;

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            return rgbValues;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Bitmap bmpR, bmpG, bmpB;
            System.Drawing.Imaging.BitmapData bmpDataR, bmpDataG, bmpDataB;
            IntPtr ptrR, ptrG, ptrB;
            int bytes;

            byte[] rgbValuesR = getRGBValues(pictureBox1, out bmpR, out bmpDataR, out ptrR, out bytes);
            byte[] rgbValuesG = getRGBValues(pictureBox2, out bmpG, out bmpDataG, out ptrG, out bytes);
            byte[] rgbValuesB = getRGBValues(pictureBox3, out bmpB, out bmpDataB, out ptrB, out bytes);

            // Color histogram
            int[] red_count = new int[256];
            int[] green_count = new int[256];
            int[] blue_count = new int[256];

            // Set every third value to 255. A 24bpp bitmap will look red.  
            for (int counter = 0; counter < rgbValuesR.Length; counter += 3)
            {
                ++red_count[rgbValuesR[counter + 2]];
                ++green_count[rgbValuesR[counter + 1]];
                ++blue_count[rgbValuesR[counter]];

                rgbValuesR[counter] = 0;
                rgbValuesR[counter + 1] = 0;
                rgbValuesG[counter] = 0;
                rgbValuesG[counter + 2] = 0;
                rgbValuesB[counter + 1] = 0;
                rgbValuesB[counter + 2] = 0;

            }
            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValuesR, 0, ptrR, bytes);
            System.Runtime.InteropServices.Marshal.Copy(rgbValuesG, 0, ptrG, bytes);
            System.Runtime.InteropServices.Marshal.Copy(rgbValuesB, 0, ptrB, bytes);

            // Unlock the bits.
            bmpR.UnlockBits(bmpDataR);
            bmpG.UnlockBits(bmpDataG);
            bmpB.UnlockBits(bmpDataB);

            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();

            // Show histogram
            chart1.Series[0].Points.DataBindY(red_count);
            chart2.Series[0].Points.DataBindY(green_count);
            chart3.Series[0].Points.DataBindY(blue_count);

            chart1.Series[0].Color = Color.Red;
            chart2.Series[0].Color = Color.Green;
            chart3.Series[0].Color = Color.Blue;
        }
    }
}
