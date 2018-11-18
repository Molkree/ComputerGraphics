using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace Lab9_task2
{
    public partial class Form1 : Form
    {
        Pen new_fig = Pens.Black;
        Graphics g;
        Axis line_mode = 0;
        Polyhedron figure = null;
        Bitmap bmp, texture;
        BitmapData bmpData, bmpDataTexture; // for picturebox and texture
        byte[] rgbValues, rgbValuesTexture; // for picturebox and texture
        IntPtr ptr; // pointer to the rgbValues
        int bytes; // length of rgbValues

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
            g.ScaleTransform(1, -1);
            comboBox2.SelectedIndex = 0;

            texture = Image.FromFile("../../crate-texture.jpg") as Bitmap;
            Rectangle rectTexture = new Rectangle(0, 0, texture.Width, texture.Height);
            bmpDataTexture = texture.LockBits(rectTexture, ImageLockMode.ReadWrite, texture.PixelFormat);
            int bytesTexture = Math.Abs(bmpDataTexture.Stride) * texture.Height;
            rgbValuesTexture = new byte[bytesTexture];
            System.Runtime.InteropServices.Marshal.Copy(bmpDataTexture.Scan0, rgbValuesTexture, 0, bytesTexture);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        // контроль вводимых символов
        private void textBox_KeyPress_int(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '-') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox_KeyPress_double(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == ',') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        void check_all_textboxes()
        {
            foreach (var c in Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = c as TextBox;
                    if (string.IsNullOrEmpty(t.Text))
                    {
                        if (t.Name == "scaling_x" || t.Name == "scaling_y" || t.Name == "scaling_z" || t.Name == "rot_line_x2" ||
                            t.Name == "rot_line_y2" || t.Name == "rot_line_z2")
                            t.Text = "1";
                        else t.Text = "0";
                    }
                }
            }
        }

        private void button_exec_Click(object sender, EventArgs e)
        {
            if (figure == null)
            {
                MessageBox.Show("Сначала создайте фигуру", "Нет фигуры", MessageBoxButtons.OK);
            }
            else
            {
                check_all_textboxes();
                // масштабируем и переносим относительно начала координат (сдвигом центра в начало)
                //
                if (scaling_x.Text != "1" || scaling_y.Text != "1" || scaling_z.Text != "1" ||
                    trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                {
                    // сначала переносим в начало
                    // left fig
                    float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                    figure.translate(-old_x, -old_y, -old_z);

                    // делаем, что нужно
                    if (scaling_x.Text != "1" || scaling_y.Text != "1" || scaling_z.Text != "1")
                    {
                        float x = float.Parse(scaling_x.Text, CultureInfo.CurrentCulture),
                            y = float.Parse(scaling_y.Text, CultureInfo.CurrentCulture),
                            z = float.Parse(scaling_z.Text, CultureInfo.CurrentCulture);
                        figure.scale(x, y, z);
                    }

                    if (trans_x.Text != "0" || trans_y.Text != "0" || trans_z.Text != "0")
                    {
                        int dx = int.Parse(trans_x.Text, CultureInfo.CurrentCulture),
                            dy = int.Parse(trans_y.Text, CultureInfo.CurrentCulture),
                            dz = int.Parse(trans_z.Text, CultureInfo.CurrentCulture);
                        figure.translate(dx, dy, dz);
                    }

                    // переносим обратно
                    figure.translate(old_x, old_y, old_z);
                }

                // поворачиваем относительно нужной прямой
                if (rot_angle.Text != "0")
                {
                    if (line_mode != Axis.OTHER)
                    {
                        float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                        figure.translate(-old_x, -old_y, -old_z);

                        double angle = double.Parse(rot_angle.Text, CultureInfo.CurrentCulture);
                        figure.rotate(angle, line_mode);

                        figure.translate(old_x, old_y, old_z);
                    }
                    else
                    {
                        Edge rot_line = new Edge(
                            new Point3d(
                                int.Parse(rot_line_x1.Text, CultureInfo.CurrentCulture),
                                int.Parse(rot_line_y1.Text, CultureInfo.CurrentCulture),
                                int.Parse(rot_line_z1.Text, CultureInfo.CurrentCulture)),
                            new Point3d(
                                int.Parse(rot_line_x2.Text, CultureInfo.CurrentCulture),
                                int.Parse(rot_line_y2.Text, CultureInfo.CurrentCulture),
                                int.Parse(rot_line_z2.Text, CultureInfo.CurrentCulture)));
                        float Ax = rot_line.P1.X, Ay = rot_line.P1.Y, Az = rot_line.P1.Z;
                        figure.translate(-Ax, -Ay, -Az);

                        double angle = double.Parse(rot_angle.Text, CultureInfo.CurrentCulture);
                        figure.rotate(angle, line_mode, rot_line);

                        figure.translate(Ax, Ay, Az);
                    }
                }

                bmp.Dispose();
                rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
                figure.ApplyTexture(bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                bmp.UnlockBits(bmpData);
                pictureBox1.Image = bmp;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            line_mode = (Axis)comboBox2.SelectedIndex;
            if (line_mode == Axis.OTHER)
            {
                rot_line_x1.Enabled = true;
                rot_line_y1.Enabled = true;
                rot_line_z1.Enabled = true;
                rot_line_x2.Enabled = true;
                rot_line_y2.Enabled = true;
                rot_line_z2.Enabled = true;
            }
            else
            {
                rot_line_x1.Enabled = false;
                rot_line_y1.Enabled = false;
                rot_line_z1.Enabled = false;
                rot_line_x2.Enabled = false;
                rot_line_y2.Enabled = false;
                rot_line_z2.Enabled = false;
            }
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            foreach (var c in Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = c as TextBox;
                    if (t.Name == "scaling_x" || t.Name == "scaling_y" || t.Name == "scaling_z" || t.Name == "rot_line_x2" ||
                            t.Name == "rot_line_y2" || t.Name == "rot_line_z2")
                        t.Text = "1";
                    else t.Text = "0";

                }
            }
        }

        private byte[] getRGBValues(out Bitmap bmp, out BitmapData bmpData,
    out IntPtr ptr, out int bytes)
        {
            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height, PixelFormat.Format24bppRgb);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            bmpData =
                bmp.LockBits(rect, ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgb_values = new byte[bytes];

            // Create rgb array with background color
            for (int i = 0; i < bytes - 3; i += 3)
            {
                rgb_values[i] = pictureBox1.BackColor.R;
                rgb_values[i + 1] = pictureBox1.BackColor.G;
                rgb_values[i + 2] = pictureBox1.BackColor.B;
            }

            return rgb_values;
        }

        // Create hexahedron
        private void button1_Click(object sender, EventArgs e)
        {
            figure = new Polyhedron();
            figure.make_hexahedron();

            if (bmp != null)
                bmp.Dispose();
            rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
            figure.ApplyTexture(bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            pictureBox1.Image = bmp;
        }

        // Create tetrahedron
        private void button2_Click(object sender, EventArgs e)
        {
            figure = new Polyhedron();
            figure.make_tetrahedron();

            if (bmp != null)
                bmp.Dispose();
            rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
            figure.ApplyTexture(bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            pictureBox1.Image = bmp;
        }

        // Create octahedron
        private void button3_Click(object sender, EventArgs e)
        {
            figure = new Polyhedron();
            figure.make_octahedron();

            if (bmp != null)
                bmp.Dispose();
            rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
            figure.ApplyTexture(bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            pictureBox1.Image = bmp;
        }

        // отражение по х
        private void button6_Click(object sender, EventArgs e)
        {
            if (figure != null)
            {
                figure.reflectX();

                bmp.Dispose();
                rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
                figure.ApplyTexture(bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                bmp.UnlockBits(bmpData);
                pictureBox1.Image = bmp;
            }
        }

        // отражение по y
        private void button7_Click(object sender, EventArgs e)
        {
            if (figure != null)
            {
                figure.reflectY();

                bmp.Dispose();
                rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
                figure.ApplyTexture(bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                bmp.UnlockBits(bmpData);
                pictureBox1.Image = bmp;
            }
        }

        // отражение по z
        private void button8_Click(object sender, EventArgs e)
        {
            if (figure != null)
            {
                figure.reflectZ();

                bmp.Dispose();
                rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
                figure.ApplyTexture(bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                bmp.UnlockBits(bmpData);
                pictureBox1.Image = bmp;
            }
        }

        // load texture
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                texture.UnlockBits(bmpDataTexture);
                texture.Dispose();

                texture = Image.FromFile(openFileDialog1.FileName) as Bitmap;
                Rectangle rectTexture = new Rectangle(0, 0, texture.Width, texture.Height);
                bmpDataTexture = texture.LockBits(rectTexture, ImageLockMode.ReadWrite, texture.PixelFormat);
                int bytesTexture = Math.Abs(bmpDataTexture.Stride) * texture.Height;
                rgbValuesTexture = new byte[bytesTexture];
                System.Runtime.InteropServices.Marshal.Copy(bmpDataTexture.Scan0, rgbValuesTexture, 0, bytesTexture);

                if (bmp != null)
                {
                    bmp.Dispose();
                    rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
                    figure.ApplyTexture(bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                    bmp.UnlockBits(bmpData);
                    pictureBox1.Image = bmp;
                }
            }
        }
    }
}
