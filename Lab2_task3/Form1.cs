using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_task3
{
	public partial class Form1 : Form
	{

        double hue_shift, sat_shift, val_shift;
        Graphics g;
        Bitmap orig_pic;

        public Form1()
        {
            InitializeComponent();
           
            hue_shift = trackBar1.Value;
            sat_shift = trackBar2.Value;
            val_shift = trackBar3.Value;
        }

        private void button1_Click(object sender, EventArgs e)
		{
            openFileDialog1.ShowDialog();
		}

        public static void ColorToHSV(int red, int green, int blue, out double hue, out double sat, out double val)
        {
            
            int max = Math.Max(red, Math.Max(green, blue));
            int min = Math.Min(red, Math.Min(green, blue));

            hue = 0;

            if (max == min)
                hue = 0;
            else if (max == red)
            {
                hue = 60 * (green - blue) / (max - min);
                if (green < blue)
                    hue += 360;
            }
            else if (max == green)
                hue = 60 * (blue - red) / (max - min) + 120;
            else if (max == blue)
                hue = 60 * (red - green) / (max - min) + 240;

            if (max == 0)
                sat = 0;
            else sat = 1 - ((double)min / max);

            val = (double)max / 255;
        }
        
        
        public static void ColorFromHSV(double hue, double sat, double val, out int red, out int green, out int blue)
		{
            /* formulas from:
             *  https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSV
             *  https://www.rapidtables.com/convert/color/hsv-to-rgb.html
             */
            double c = val * sat;
            double h1 = hue / 60;
            double x = c * (1 - Math.Abs(h1 % 2 - 1));
            double m = val - c;

            if (0 <= h1 && h1 <= 1)
            {
                red = Convert.ToInt32((c + m) * 255);
                green = Convert.ToInt32((x + m) * 255);
                blue = Convert.ToInt32((0 + m) * 255);
            }
            else if (1 <= h1 && h1 <= 2)
            {
                red = Convert.ToInt32((x + m) * 255);
                green = Convert.ToInt32((c + m) * 255);
                blue = Convert.ToInt32((0 + m) * 255);
            }
            else if (2 <= h1 && h1 <= 3)
            {
                red = Convert.ToInt32((0 + m) * 255);
                green = Convert.ToInt32((c + m) * 255);
                blue = Convert.ToInt32((x + m) * 255);
           }
            else if (3 <= h1 && h1 <= 4)
            {
                red = Convert.ToInt32((0 + m) * 255);
                green = Convert.ToInt32((x + m) * 255);
                blue = Convert.ToInt32((c + m) * 255);
            }
            else if (4 <= h1 && h1 <= 5)
            {
                red = Convert.ToInt32((x + m) * 255);
                green = Convert.ToInt32((0 + m) * 255);
                blue = Convert.ToInt32((c + m) * 255);
            }
            else if (5 <= h1 && h1 <= 6)
            {
                red = Convert.ToInt32((c + m) * 255);
                green = Convert.ToInt32((0 + m) * 255);
                blue = Convert.ToInt32((x + m) * 255);
            }
            else
            {
                red = Convert.ToInt32(m * 255);
                green = Convert.ToInt32(m * 255);
                blue = Convert.ToInt32(m * 255);
             }
        }

        private void showImage()
		{
            
            Bitmap bmp = pictureBox1.Image as Bitmap;
            
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
           
            // get original pixels
            System.Drawing.Imaging.BitmapData origData =
				orig_pic.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
				orig_pic.PixelFormat);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);


            // Get the address of the first line.
            IntPtr ptr_orig = origData.Scan0;
            IntPtr ptr_new = bmpData.Scan0;
            
            // Declare an arrays to hold the bytes of the original pic and new bmp.
            int bytes = Math.Abs(origData.Stride) * orig_pic.Height;
            byte[] rgbValues_orig = new byte[bytes];
            byte[] rgbValues_new = new byte[bytes];

            // Copy the original RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr_orig, rgbValues_orig, 0, bytes);

			// Unlock the bits.
			orig_pic.UnlockBits(origData);        

			//int i = 0;
			for (int counter = 0; counter < rgbValues_orig.Length; counter += 3)
			{
                double hue, sat, val;

                int red = rgbValues_orig[counter + 2];
                int green = rgbValues_orig[counter + 1];
                int blue = rgbValues_orig[counter];
                ColorToHSV(red, green, blue, out hue, out sat, out val);

                // change h, s, v
                double new_hue = (hue + hue_shift) % 360;
                double new_sat = (sat*100 + sat_shift) * 0.01;
                if (new_sat > 1)
                    new_sat = 1;
                if (new_sat < 0)
                    new_sat = 0;

                double new_val = (val*100 + val_shift) * 0.01;
                if (new_val > 1)
                    new_val = 1;
                if (new_val < 0)
                    new_val = 0;

				ColorFromHSV(new_hue, new_sat, new_val, out red, out green, out blue);
                rgbValues_new[counter + 2] = Convert.ToByte(red);
                rgbValues_new[counter + 1] = Convert.ToByte(green);
                rgbValues_new[counter] = Convert.ToByte(blue);
			}

            // Copy new RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues_new, 0, ptr_new, bytes);
            bmp.UnlockBits(bmpData);
            pictureBox1.Refresh();
		}

      
        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
		{
            hue_shift = 0;
            sat_shift = 0;
            val_shift = 0;
			pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
            g = Graphics.FromImage(pictureBox1.Image);
            orig_pic = pictureBox1.Image.Clone() as Bitmap;
			pictureBox1.Refresh();
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			hue_shift = trackBar1.Value;
            showImage();
		}

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            sat_shift = trackBar2.Value;
            showImage();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            val_shift = trackBar3.Value;
            showImage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            hue_shift = 0;
            sat_shift = 0;
            val_shift = 0;
            showImage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image pic = pictureBox1.Image;
            pic.Save("../../newImage.png", System.Drawing.Imaging.ImageFormat.Png);


            string message = "Image is saved in the directory 'Lab2_task3'";
            string caption = "Saved";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);

        }

    }
}
