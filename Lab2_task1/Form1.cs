using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab2_task1
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
			pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
            pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
            pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);
            pictureBox4.Image = Image.FromFile(openFileDialog1.FileName);

            Graphics g = Graphics.FromImage(pictureBox1.Image);
			Bitmap bmp1 = pictureBox1.Image as Bitmap;
            Bitmap bmp2 = pictureBox2.Image as Bitmap;
            Bitmap bmp3 = pictureBox3.Image as Bitmap;
            Bitmap bmp4 = pictureBox4.Image as Bitmap;


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

			// Copy the RGB values back to the bitmap
			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			bmp1.UnlockBits(bmpData);

            
			int i = 0;
            int width = bmp1.Width;
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
                //исходный
                int R = rgbValues[counter];
                int G = rgbValues[counter + 1];
                int B = rgbValues[counter + 2];
                Color c = Color.FromArgb(B, G, R);
				bmp1.SetPixel(i % width, i / width, c);
                //серый1
                int NC1 = Convert.ToInt32(0.33 * R + 0.33 * G + 0.33 * B) % 256;
                c = Color.FromArgb(NC1, NC1, NC1);
                bmp2.SetPixel(i % width, i / width, c);
                //серый2
                int NC2 = Convert.ToInt32(0.2126 * R + 0.7152 * G + 0.0722 * B) % 256;
                c = Color.FromArgb(NC2, NC2, NC2);
                bmp3.SetPixel(i % width, i / width, c);
                //разность
                int NC3 = Math.Max(NC1, NC2) - Math.Min(NC1, NC2);
                c = Color.FromArgb(NC3, NC3, NC3);
                bmp4.SetPixel(i % width, i / width, c);
                //гистограмма
              
                chart1.Series[0].Points.Add(NC1);
                
                i++;

			}
            // Set the PictureBox to display the image.

            pictureBox1.Refresh();
			pictureBox2.Refresh();
			pictureBox3.Refresh();
			pictureBox4.Refresh();

		}
	}
}
