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

        byte[] rgbValues;
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
            pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);
            Bitmap bmp1 = pictureBox1.Image as Bitmap;
            Bitmap bmp2 = pictureBox2.Image as Bitmap;
            Bitmap bmp3 = pictureBox3.Image as Bitmap;

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp1.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp1.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp1.Height;
            rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            //// Set every third value to 255. A 24bpp bitmap will look red.  
            //for (int counter = 0; counter < rgbValues.Length; counter += 3)
            //{
            //    //rgbValues[counter + 1] = 255;                                
            //}
            //// Copy the RGB values back to the bitmap
            //System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp1.UnlockBits(bmpData);

            int i = 0;
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                bmp1.SetPixel(i % bmp1.Width, i / bmp1.Width,
                    Color.FromArgb(rgbValues[counter + 2], 0, 0));
                bmp2.SetPixel(i % bmp1.Width, i / bmp1.Width,
                    Color.FromArgb(0, rgbValues[counter + 1], 0));
                bmp3.SetPixel(i % bmp1.Width, i / bmp1.Width,
                    Color.FromArgb(0, 0, rgbValues[counter]));
                i++;
            }
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();
        }
    }
}
