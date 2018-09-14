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
	//	String path;
		byte[] rgbValues;
		double pic_hue, pic_sat, pic_val;
        Graphics g;
        Bitmap orig_pic;

        public Form1()
        {
            InitializeComponent();
           
            pic_hue = trackBar1.Value;
            pic_sat = trackBar2.Value;
            pic_val = trackBar3.Value;



        }

        private void button1_Click(object sender, EventArgs e)
		{
				if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
				//	System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
				//	sr.Close();
				//	path = openFileDialog1.FileName;
				//	pictureBox1.Image(
				}
		}

        public static void ColorToHSV(Color color, out double hue, out double sat, out double val)
        {
            
            byte red = color.R;
            byte green = color.G;
            byte blue = color.B;

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
        
        
        public static Color ColorFromHSV(double hue, double sat, double val)
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
                return Color.FromArgb(Convert.ToInt32((c + m) * 255), Convert.ToInt32((x + m) * 255), Convert.ToInt32((0 + m) * 255));
            else if (1 <= h1 && h1 <= 2)
                return Color.FromArgb(Convert.ToInt32((x + m) * 255), Convert.ToInt32((c + m) * 255), Convert.ToInt32((0 + m) * 255));
            else if (2 <= h1 && h1 <= 3)
                return Color.FromArgb(Convert.ToInt32((0 + m) * 255), Convert.ToInt32((c + m) * 255), Convert.ToInt32((x + m) * 255));
            else if (3 <= h1 && h1 <= 4)
                return Color.FromArgb(Convert.ToInt32((0 + m) * 255), Convert.ToInt32((x + m) * 255), Convert.ToInt32((c + m) * 255));
            else if (4 <= h1 && h1 <= 5)
                return Color.FromArgb(Convert.ToInt32((x + m) * 255), Convert.ToInt32((0 + m) * 255), Convert.ToInt32((c + m) * 255));
            else if (5 <= h1 && h1 <= 6)
                return Color.FromArgb(Convert.ToInt32((c + m) * 255), Convert.ToInt32((0 + m) * 255), Convert.ToInt32((x + m) * 255));
            else return Color.FromArgb(Convert.ToInt32(m), Convert.ToInt32(m), Convert.ToInt32(m));
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

			// Get the address of the first line.
			IntPtr ptr = origData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(origData.Stride) * orig_pic.Height;
			rgbValues = new byte[bytes];

			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

			// Copy the RGB values back to the bitmap
			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			orig_pic.UnlockBits(origData);

			int i = 0;
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
                double hue, sat, val;

				Color orig = Color.FromArgb(rgbValues[counter + 2], rgbValues[counter + 1], rgbValues[counter]);
                // get h, s, v
                ColorToHSV(orig, out hue, out sat, out val);

                // change h, s, v
                double new_hue = (hue + pic_hue) % 360;
                double new_sat = (sat*100 + pic_sat) * 0.01;
                if (new_sat > 1)
                    new_sat = 1;
                if (new_sat < 0)
                    new_sat = 0;

                double new_val = (val*100 + pic_val) * 0.01;
                if (new_val > 1)
                    new_val = 1;
                if (new_val < 0)
                    new_val = 0;

				Color newcolor = ColorFromHSV(new_hue, new_sat, new_val);
                bmp.SetPixel(i % bmp.Width, i / bmp.Width, newcolor);
				i++;
			}            
            pictureBox1.Refresh();
		}

      
        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
		{
            pic_hue = 0;
            pic_sat = 0;
            pic_val = 0;
			pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
            g = Graphics.FromImage(pictureBox1.Image);
            Bitmap bmp = pictureBox1.Image as Bitmap;
            
			// Lock the bitmap's bits.  
			Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

			System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
			rgbValues = new byte[bytes];

			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

			// Copy the RGB values back to the bitmap
			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            // save the original picture
            orig_pic = bmp.Clone() as Bitmap;

            int i = 0;
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				Color orig = Color.FromArgb(rgbValues[counter + 2], rgbValues[counter + 1], rgbValues[counter]);

                double hue, sat, val;
                hue = orig.GetHue();
                sat = orig.GetSaturation();
                val = orig.GetBrightness();

                ColorToHSV(orig, out hue, out sat, out val);
          		Color newcolor = ColorFromHSV(hue, sat, val);
                bmp.SetPixel(i % bmp.Width, i / bmp.Width, newcolor);
                
				i++;
			}
			pictureBox1.Refresh();
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			pic_hue = trackBar1.Value;
		}

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            pic_sat = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            pic_val = trackBar3.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            pic_hue = 0;
            pic_sat = 0;
            pic_val = 0;
            showImage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showImage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          /*   System.IO.Stream myStream;
             SaveFileDialog saveFileDialog1 = new SaveFileDialog();

             saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
             saveFileDialog1.FilterIndex = 2;
             saveFileDialog1.RestoreDirectory = true;

             if (saveFileDialog1.ShowDialog() == DialogResult.OK)
             {
                 if ((myStream = saveFileDialog1.OpenFile()) != null)
                 {
                    Image pic = pictureBox1.Image;

                    String path = saveFileDialog1.FileName;
                    String p1 = path.Substring(0, 3);
                    p1 = p1.Replace("\\", "//");
                    String p2 = path.Substring(3);
                    p2 = p2.Replace("\\", "/");
                    path = p1 + p2;

                    pic.Save("E://Images/bluefruits.png", System.Drawing.Imaging.ImageFormat.Png);
                    myStream.Close();
                 }
             }
            */ 
            Image pic = pictureBox1.Image;
            pic.Save("../../newImage.png", System.Drawing.Imaging.ImageFormat.Png);


            string message = "Image is saved in the folder 'Lab2_task3'";
            string caption = "Saved";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);

        }

    }
}
