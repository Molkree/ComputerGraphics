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
		double hue, sat, val;
		public Form1()
		{
			InitializeComponent();
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

		// copypaste ----
		public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
		{
			int max = Math.Max(color.R, Math.Max(color.G, color.B));
			int min = Math.Min(color.R, Math.Min(color.G, color.B));

			hue = color.GetHue();
			saturation = (max == 0) ? 0 : 1d - (1d * min / max);
			value = max / 255d;
		}

		public static Color ColorFromHSV(double hue, double saturation, double value)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value = value * 255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

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
		}
		// ------------

		private void showImage(double hue, double sat, double val)
		{
			pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
			Graphics g = Graphics.FromImage(pictureBox1.Image);
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

			// Set every third value to 255. A 24bpp bitmap will look red.  
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				//rgbValues[counter+1] = 255;                                
			}
			// Copy the RGB values back to the bitmap
			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);

			int i = 0;
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				/*				int red, green, blue;
								red = rgbValues[counter + 2];
								green = rgbValues[counter + 1];
								blue = rgbValues[counter];
								*/

				Color orig = Color.FromArgb(rgbValues[counter + 2], rgbValues[counter + 1], rgbValues[counter]);

				//		hue = orig.GetHue();
				//		sat = orig.GetSaturation();
				//		val = orig.GetBrightness();

				//		bmp.SetPixel(i % bmp.Width, i / bmp.Width, orig);

				//	ColorToHSV(orig, out hue, out sat, out val);



				Color newcolor = ColorFromHSV(hue, sat, val);

				bmp.SetPixel(i % bmp.Width, i / bmp.Width, newcolor);


				i++;

			}
			pictureBox1.Refresh();
		}


		private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
		{
			pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
			Graphics g = Graphics.FromImage(pictureBox1.Image);
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

			int i = 0;
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				Color orig = Color.FromArgb(rgbValues[counter + 2], rgbValues[counter + 1], rgbValues[counter]);

				//		hue = orig.GetHue();
				//		sat = orig.GetSaturation();
				//		val = orig.GetBrightness();

				//		bmp.SetPixel(i % bmp.Width, i / bmp.Width, orig);

				//	ColorToHSV(orig, out hue, out sat, out val);



				Color newcolor = ColorFromHSV(hue, sat, val);

				bmp.SetPixel(i % bmp.Width, i / bmp.Width, newcolor);


				i++;

			}
			pictureBox1.Refresh();
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			hue = trackBar1.Value;

		}
	}
}
