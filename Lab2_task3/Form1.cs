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
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            val = val * 255;
            int v = Convert.ToInt32(val);
            int p = Convert.ToInt32(val * (1 - sat));
            int q = Convert.ToInt32(val * (1 - f * sat));
            int t = Convert.ToInt32(val * (1 - (1 - f) * sat));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
            /*		int h_i = Convert.ToInt32(Math.Floor(hue / 60)) % 6;

                    int v_min = Convert.ToInt32((100 - sat) * val / 100);
                    int a = Convert.ToInt32((val - v_min) * (hue % 6) / 60);
                    int v_inc = v_min + a;
                    int v_dec = Convert.ToInt32(val - a);

                    int v = Convert.ToInt32(val); 

                    if (h_i == 0)
                        return Color.FromArgb(255, v, v_inc, v_min);
                    else if(h_i == 1)
                        return Color.FromArgb(255, v_dec, v, v_min);
                    else if (h_i == 2)
                        return Color.FromArgb(255, v_min, v, v_inc);
                    else if (h_i == 3)
                        return Color.FromArgb(255, v_min, v_dec, v);
                    else if (h_i == 4)
                        return Color.FromArgb(255, v_inc, v_min, v);
                    else
                        return Color.FromArgb(255, v, v_min, v_dec);        */
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

            // vsave the original picture
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
            showImage();
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

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            pic_sat = trackBar2.Value;
            showImage();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            pic_val = trackBar3.Value;
            showImage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* System.IO.Stream myStream;
             SaveFileDialog saveFileDialog1 = new SaveFileDialog();

             saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
             saveFileDialog1.FilterIndex = 2;
             saveFileDialog1.RestoreDirectory = true;

             if (saveFileDialog1.ShowDialog() == DialogResult.OK)
             {
                 if ((myStream = saveFileDialog1.OpenFile()) != null)
                 {
                     Image pic = pictureBox1.Image;*/
            /* ??? */     /*        pic.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                         myStream.Close();
                     }
                 }
        */
            Image pic = pictureBox1.Image;
            pic.Save("E://bluefruits.png", System.Drawing.Imaging.ImageFormat.Png);

        }

    }
}
