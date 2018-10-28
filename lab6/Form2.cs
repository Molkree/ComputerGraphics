using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form2 : Form
    {
        public delegate float Function(float x, float y);
        public Function f;
        public float x0, x1, y0, y1;
        public int cnt_of_breaks;

        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            /*
x + y
x^2 + y^2
cos(x^2 + y^2) / (x^2 + y^2 + 1)
sin(x) * cos(y)
sin(x) + cos(y)

             */
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    f = (x, y) => x + y;
                    break;
                case 1:
                    f = (x, y) => x * x + y * y;
                    break;
                case 2:
                    f = (x, y) => (float)(Math.Cos(x * x + y * y + 1)) / (x * x + y * y + 1);
                    break;
                case 3:
                    f = (x, y) => (float)(Math.Sin(x) * Math.Cos(y));
                    break;
                case 4:
                    f = (x, y) => (float)(Math.Sin(x) + Math.Cos(y));
                    break;
                default:
                    f = (x, y) => 0;
                    break;
            }

            x0 = float.Parse(textBox_x0.Text);
            x1 = float.Parse(textBox_x1.Text);
            y0 = float.Parse(textBox_y0.Text);
            y1 = float.Parse(textBox_y1.Text);
            cnt_of_breaks = (int)breaks_cnt.Value;

            this.Close();
        }

        private void textBox_KeyPress_double(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == '-') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
    }
}
