namespace floating_horizon
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_y1 = new System.Windows.Forms.TextBox();
            this.textBox_y0 = new System.Windows.Forms.TextBox();
            this.textBox_x1 = new System.Windows.Forms.TextBox();
            this.breaks_cnt = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_x0 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_gr = new System.Windows.Forms.Button();
            this.clear_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.angle_1 = new System.Windows.Forms.TextBox();
            this.angle_2 = new System.Windows.Forms.TextBox();
            this.angle_3 = new System.Windows.Forms.TextBox();
            this.button_camera = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.trans_x_camera = new System.Windows.Forms.TextBox();
            this.trans_y_camera = new System.Windows.Forms.TextBox();
            this.trans_z_camera = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_coords = new System.Windows.Forms.Label();
            this.label_angles = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.breaks_cnt)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "x + y",
            "x^2 + y^2",
            "cos(x^2 + y^2) / (x^2 + y^2 + 1)",
            "sin(x) * cos(y)",
            "sin(x) + cos(y)"});
            this.comboBox1.Location = new System.Drawing.Point(68, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(124, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Функция";
            // 
            // textBox_y1
            // 
            this.textBox_y1.Location = new System.Drawing.Point(158, 87);
            this.textBox_y1.Name = "textBox_y1";
            this.textBox_y1.Size = new System.Drawing.Size(44, 20);
            this.textBox_y1.TabIndex = 21;
            this.textBox_y1.Text = "10";
            this.textBox_y1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // textBox_y0
            // 
            this.textBox_y0.Location = new System.Drawing.Point(106, 87);
            this.textBox_y0.Name = "textBox_y0";
            this.textBox_y0.Size = new System.Drawing.Size(44, 20);
            this.textBox_y0.TabIndex = 20;
            this.textBox_y0.Text = "-10";
            this.textBox_y0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // textBox_x1
            // 
            this.textBox_x1.Location = new System.Drawing.Point(158, 58);
            this.textBox_x1.Name = "textBox_x1";
            this.textBox_x1.Size = new System.Drawing.Size(44, 20);
            this.textBox_x1.TabIndex = 19;
            this.textBox_x1.Text = "10";
            this.textBox_x1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // breaks_cnt
            // 
            this.breaks_cnt.Location = new System.Drawing.Point(106, 115);
            this.breaks_cnt.Name = "breaks_cnt";
            this.breaks_cnt.Size = new System.Drawing.Size(41, 20);
            this.breaks_cnt.TabIndex = 18;
            this.breaks_cnt.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Число разбиений";
            // 
            // textBox_x0
            // 
            this.textBox_x0.Location = new System.Drawing.Point(106, 58);
            this.textBox_x0.Name = "textBox_x0";
            this.textBox_x0.Size = new System.Drawing.Size(41, 20);
            this.textBox_x0.TabIndex = 16;
            this.textBox_x0.Text = "-10";
            this.textBox_x0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Диапазон по Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Диапазон по Х";
            // 
            // button_gr
            // 
            this.button_gr.Location = new System.Drawing.Point(106, 150);
            this.button_gr.Name = "button_gr";
            this.button_gr.Size = new System.Drawing.Size(75, 23);
            this.button_gr.TabIndex = 13;
            this.button_gr.Text = "Готово";
            this.button_gr.UseVisualStyleBackColor = true;
            this.button_gr.Click += new System.EventHandler(this.button_gr_Click);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(12, 461);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(75, 50);
            this.clear_button.TabIndex = 62;
            this.clear_button.Text = "Поставить все поля по умолчанию";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.button_gr);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox_x0);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.breaks_cnt);
            this.panel1.Controls.Add(this.textBox_x1);
            this.panel1.Controls.Add(this.textBox_y0);
            this.panel1.Controls.Add(this.textBox_y1);
            this.panel1.Location = new System.Drawing.Point(9, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 187);
            this.panel1.TabIndex = 71;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(9, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 69;
            this.label7.Text = "Задать график";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label_angles);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label_coords);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.angle_1);
            this.panel3.Controls.Add(this.angle_2);
            this.panel3.Controls.Add(this.angle_3);
            this.panel3.Controls.Add(this.button_camera);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.trans_x_camera);
            this.panel3.Controls.Add(this.trans_y_camera);
            this.panel3.Controls.Add(this.trans_z_camera);
            this.panel3.Location = new System.Drawing.Point(9, 205);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(228, 222);
            this.panel3.TabIndex = 73;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 49);
            this.button1.TabIndex = 88;
            this.button1.Text = "Вернуть камеру в начальное положение";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 84;
            this.label9.Text = "Поворот (x, y, z)";
            // 
            // angle_1
            // 
            this.angle_1.Location = new System.Drawing.Point(7, 103);
            this.angle_1.Name = "angle_1";
            this.angle_1.Size = new System.Drawing.Size(38, 20);
            this.angle_1.TabIndex = 85;
            this.angle_1.Text = "0";
            this.angle_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // angle_2
            // 
            this.angle_2.Location = new System.Drawing.Point(51, 103);
            this.angle_2.Name = "angle_2";
            this.angle_2.Size = new System.Drawing.Size(38, 20);
            this.angle_2.TabIndex = 86;
            this.angle_2.Text = "0";
            this.angle_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // angle_3
            // 
            this.angle_3.Location = new System.Drawing.Point(95, 103);
            this.angle_3.Name = "angle_3";
            this.angle_3.Size = new System.Drawing.Size(38, 20);
            this.angle_3.TabIndex = 87;
            this.angle_3.Text = "0";
            this.angle_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // button_camera
            // 
            this.button_camera.Location = new System.Drawing.Point(35, 129);
            this.button_camera.Name = "button_camera";
            this.button_camera.Size = new System.Drawing.Size(75, 23);
            this.button_camera.TabIndex = 83;
            this.button_camera.Text = "Выполнить";
            this.button_camera.UseVisualStyleBackColor = true;
            this.button_camera.Click += new System.EventHandler(this.button_camera_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(4, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Камера";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.label11.TabIndex = 73;
            this.label11.Text = "Смещение (x, y, z)";
            // 
            // trans_x_camera
            // 
            this.trans_x_camera.Location = new System.Drawing.Point(7, 45);
            this.trans_x_camera.Name = "trans_x_camera";
            this.trans_x_camera.Size = new System.Drawing.Size(38, 20);
            this.trans_x_camera.TabIndex = 74;
            this.trans_x_camera.Text = "0";
            this.trans_x_camera.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // trans_y_camera
            // 
            this.trans_y_camera.Location = new System.Drawing.Point(51, 45);
            this.trans_y_camera.Name = "trans_y_camera";
            this.trans_y_camera.Size = new System.Drawing.Size(38, 20);
            this.trans_y_camera.TabIndex = 75;
            this.trans_y_camera.Text = "0";
            this.trans_y_camera.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // trans_z_camera
            // 
            this.trans_z_camera.Location = new System.Drawing.Point(95, 45);
            this.trans_z_camera.Name = "trans_z_camera";
            this.trans_z_camera.Size = new System.Drawing.Size(38, 20);
            this.trans_z_camera.TabIndex = 76;
            this.trans_z_camera.Text = "0";
            this.trans_z_camera.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(243, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(822, 499);
            this.pictureBox1.TabIndex = 69;
            this.pictureBox1.TabStop = false;
            // 
            // label_coords
            // 
            this.label_coords.AutoSize = true;
            this.label_coords.Location = new System.Drawing.Point(139, 48);
            this.label_coords.Name = "label_coords";
            this.label_coords.Size = new System.Drawing.Size(54, 13);
            this.label_coords.TabIndex = 74;
            this.label_coords.Text = "cur coods";
            // 
            // label_angles
            // 
            this.label_angles.AutoSize = true;
            this.label_angles.Location = new System.Drawing.Point(139, 106);
            this.label_angles.Name = "label_angles";
            this.label_angles.Size = new System.Drawing.Size(56, 13);
            this.label_angles.TabIndex = 75;
            this.label_angles.Text = "cur angles";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 523);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.clear_button);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.breaks_cnt)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_y1;
        private System.Windows.Forms.TextBox textBox_y0;
        private System.Windows.Forms.TextBox textBox_x1;
        private System.Windows.Forms.NumericUpDown breaks_cnt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_x0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_gr;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox trans_x_camera;
        private System.Windows.Forms.TextBox trans_y_camera;
        private System.Windows.Forms.TextBox trans_z_camera;
        private System.Windows.Forms.Button button_camera;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox angle_1;
        private System.Windows.Forms.TextBox angle_2;
        private System.Windows.Forms.TextBox angle_3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_coords;
        private System.Windows.Forms.Label label_angles;
    }
}

