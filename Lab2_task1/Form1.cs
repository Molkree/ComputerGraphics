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
using System.Collections;

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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
            int[] gray1 = new int[256];
            int[] gray2 = new int[256];

            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
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
            System.Drawing.Imaging.BitmapData bmp2Data =
                bmp2.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp2.PixelFormat);
            System.Drawing.Imaging.BitmapData bmp3Data =
                bmp3.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp3.PixelFormat);
            System.Drawing.Imaging.BitmapData bmp4Data =
                bmp4.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp4.PixelFormat);

            var height = bmp1.Height;
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;
			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(bmpData.Stride) * height;
			byte[] rgbValues = new byte[bytes];
			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            IntPtr ptr2 = bmp2Data.Scan0;
            bytes = Math.Abs(bmp2Data.Stride) * height;
            byte[] gray1Values = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, gray1Values, 0, bytes);

            IntPtr ptr3 = bmp3Data.Scan0;
            bytes = Math.Abs(bmp3Data.Stride) * height;
            byte[] gray2Values = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, gray2Values, 0, bytes);

            IntPtr ptr4 = bmp4Data.Scan0;
            bytes = Math.Abs(bmp4Data.Stride) * height;
            byte[] gray3Values = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, gray3Values, 0, bytes);


            int i = 0;
            int width = bmp1.Width;
            int maxgray = 0;
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
                //исходный
                int R = rgbValues[counter + 2];
                int G = rgbValues[counter + 1];
                int B = rgbValues[counter];
                //серый1
                byte NC1 = Convert.ToByte(Convert.ToInt32(0.33 * R + 0.33 * G + 0.33 * B) % 256);
                gray1Values[counter] = gray1Values[counter + 1] = gray1Values[counter + 2] = NC1;
                //серый2
                byte NC2 = Convert.ToByte(Convert.ToInt32(0.2126 * R + 0.7152 * G + 0.0722 * B) % 256);
                gray2Values[counter] = gray2Values[counter + 1] = gray2Values[counter + 2] = NC2;
                //разность
                byte NC3 = Convert.ToByte(Math.Max(NC1, NC2) - Math.Min(NC1, NC2));
                gray3Values[counter] = gray3Values[counter + 1] = gray3Values[counter + 2] = NC3;
                if (NC3 > maxgray)
                    maxgray = NC3;
                i++;
                //гистограмма
                gray1[NC1]++;
                gray2[NC2]++;
            }

            i = 0;
            byte k = Convert.ToByte(255 / maxgray);
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                gray3Values[counter] = gray3Values[counter + 1] = gray3Values[counter + 2] = Convert.ToByte(gray3Values[counter + 2]*k);
                i++;
            }


            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            System.Runtime.InteropServices.Marshal.Copy(gray1Values, 0, ptr2, bytes);
            System.Runtime.InteropServices.Marshal.Copy(gray2Values, 0, ptr3, bytes);
            System.Runtime.InteropServices.Marshal.Copy(gray3Values, 0, ptr4, bytes);

            // Unlock the bits.
            bmp1.UnlockBits(bmpData);
            bmp2.UnlockBits(bmp2Data);
            bmp3.UnlockBits(bmp3Data);
            bmp4.UnlockBits(bmp4Data);

            chart1.Series[0].Points.DataBindY(gray1);
            chart2.Series[0].Points.DataBindY(gray2);


            // Set the PictureBox to display the image.
            pictureBox1.Refresh();
			pictureBox2.Refresh();
			pictureBox3.Refresh();
			pictureBox4.Refresh();
		}
	}
}
