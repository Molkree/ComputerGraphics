namespace Lab4
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_make = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_translation = new System.Windows.Forms.Label();
            this.label_rotation = new System.Windows.Forms.Label();
            this.label_scaling = new System.Windows.Forms.Label();
            this.textBox_trans_x = new System.Windows.Forms.TextBox();
            this.textBox_rotation = new System.Windows.Forms.TextBox();
            this.textBox_scaling = new System.Windows.Forms.TextBox();
            this.button_exec = new System.Windows.Forms.Button();
            this.label_check_answ1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_trans_y = new System.Windows.Forms.TextBox();
            this.label_check2 = new System.Windows.Forms.Label();
            this.label_check_answ2 = new System.Windows.Forms.Label();
            this.label_check1 = new System.Windows.Forms.Label();
            this.textBox_x = new System.Windows.Forms.TextBox();
            this.textBox_y = new System.Windows.Forms.TextBox();
            this.label_point = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(198, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(585, 451);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // button_make
            // 
            this.button_make.Location = new System.Drawing.Point(12, 12);
            this.button_make.Name = "button_make";
            this.button_make.Size = new System.Drawing.Size(112, 23);
            this.button_make.TabIndex = 1;
            this.button_make.Text = "Задать примитив";
            this.button_make.UseVisualStyleBackColor = true;
            this.button_make.Click += new System.EventHandler(this.button_make_Click);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(12, 41);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(112, 23);
            this.button_clear.TabIndex = 2;
            this.button_clear.Text = "Очистить";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Афинные преобразования";
            // 
            // label_translation
            // 
            this.label_translation.AutoSize = true;
            this.label_translation.Location = new System.Drawing.Point(13, 117);
            this.label_translation.Name = "label_translation";
            this.label_translation.Size = new System.Drawing.Size(86, 13);
            this.label_translation.TabIndex = 4;
            this.label_translation.Text = "Смещение (x, y)";
            // 
            // label_rotation
            // 
            this.label_rotation.AutoSize = true;
            this.label_rotation.Location = new System.Drawing.Point(12, 143);
            this.label_rotation.Name = "label_rotation";
            this.label_rotation.Size = new System.Drawing.Size(85, 13);
            this.label_rotation.TabIndex = 5;
            this.label_rotation.Text = "Поворот (angle)";
            // 
            // label_scaling
            // 
            this.label_scaling.AutoSize = true;
            this.label_scaling.Location = new System.Drawing.Point(12, 169);
            this.label_scaling.Name = "label_scaling";
            this.label_scaling.Size = new System.Drawing.Size(83, 13);
            this.label_scaling.TabIndex = 6;
            this.label_scaling.Text = "Масштаб (coef)";
            // 
            // textBox_trans_x
            // 
            this.textBox_trans_x.Location = new System.Drawing.Point(105, 114);
            this.textBox_trans_x.Name = "textBox_trans_x";
            this.textBox_trans_x.Size = new System.Drawing.Size(31, 20);
            this.textBox_trans_x.TabIndex = 7;
            this.textBox_trans_x.Text = "0";
            this.textBox_trans_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // textBox_rotation
            // 
            this.textBox_rotation.Location = new System.Drawing.Point(105, 140);
            this.textBox_rotation.Name = "textBox_rotation";
            this.textBox_rotation.Size = new System.Drawing.Size(68, 20);
            this.textBox_rotation.TabIndex = 8;
            this.textBox_rotation.Text = "0";
            this.textBox_rotation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // textBox_scaling
            // 
            this.textBox_scaling.Location = new System.Drawing.Point(105, 166);
            this.textBox_scaling.Name = "textBox_scaling";
            this.textBox_scaling.Size = new System.Drawing.Size(68, 20);
            this.textBox_scaling.TabIndex = 9;
            this.textBox_scaling.Text = "1";
            this.textBox_scaling.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // button_exec
            // 
            this.button_exec.Location = new System.Drawing.Point(14, 254);
            this.button_exec.Name = "button_exec";
            this.button_exec.Size = new System.Drawing.Size(75, 23);
            this.button_exec.TabIndex = 10;
            this.button_exec.Text = "Выполнить";
            this.button_exec.UseVisualStyleBackColor = true;
            this.button_exec.Click += new System.EventHandler(this.button_exec_Click);
            // 
            // label_check_answ1
            // 
            this.label_check_answ1.AutoSize = true;
            this.label_check_answ1.Location = new System.Drawing.Point(12, 356);
            this.label_check_answ1.Name = "label_check_answ1";
            this.label_check_answ1.Size = new System.Drawing.Size(37, 13);
            this.label_check_answ1.TabIndex = 12;
            this.label_check_answ1.Text = "Ответ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Проверка:";
            // 
            // textBox_trans_y
            // 
            this.textBox_trans_y.Location = new System.Drawing.Point(142, 114);
            this.textBox_trans_y.Name = "textBox_trans_y";
            this.textBox_trans_y.Size = new System.Drawing.Size(31, 20);
            this.textBox_trans_y.TabIndex = 14;
            this.textBox_trans_y.Text = "0";
            this.textBox_trans_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // label_check2
            // 
            this.label_check2.AutoSize = true;
            this.label_check2.Location = new System.Drawing.Point(10, 384);
            this.label_check2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_check2.Name = "label_check2";
            this.label_check2.Size = new System.Drawing.Size(147, 13);
            this.label_check2.TabIndex = 15;
            this.label_check2.Text = "Для проверки пересечения";
            // 
            // label_check_answ2
            // 
            this.label_check_answ2.AutoSize = true;
            this.label_check_answ2.Location = new System.Drawing.Point(11, 419);
            this.label_check_answ2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_check_answ2.Name = "label_check_answ2";
            this.label_check_answ2.Size = new System.Drawing.Size(37, 13);
            this.label_check_answ2.TabIndex = 16;
            this.label_check_answ2.Text = "Ответ";
            // 
            // label_check1
            // 
            this.label_check1.AutoSize = true;
            this.label_check1.Location = new System.Drawing.Point(9, 324);
            this.label_check1.Name = "label_check1";
            this.label_check1.Size = new System.Drawing.Size(136, 13);
            this.label_check1.TabIndex = 17;
            this.label_check1.Text = "Точка справа или слева?";
            // 
            // textBox_x
            // 
            this.textBox_x.Location = new System.Drawing.Point(134, 212);
            this.textBox_x.Name = "textBox_x";
            this.textBox_x.Size = new System.Drawing.Size(39, 20);
            this.textBox_x.TabIndex = 18;
            this.textBox_x.Text = "0";
            this.textBox_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // textBox_y
            // 
            this.textBox_y.Location = new System.Drawing.Point(134, 238);
            this.textBox_y.Name = "textBox_y";
            this.textBox_y.Size = new System.Drawing.Size(41, 20);
            this.textBox_y.TabIndex = 19;
            this.textBox_y.Text = "0";
            this.textBox_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // label_point
            // 
            this.label_point.AutoSize = true;
            this.label_point.Location = new System.Drawing.Point(12, 199);
            this.label_point.Name = "label_point";
            this.label_point.Size = new System.Drawing.Size(108, 13);
            this.label_point.TabIndex = 20;
            this.label_point.Text = "Точка для поворота";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "y";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 475);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_point);
            this.Controls.Add(this.textBox_y);
            this.Controls.Add(this.textBox_x);
            this.Controls.Add(this.label_check1);
            this.Controls.Add(this.label_check_answ2);
            this.Controls.Add(this.label_check2);
            this.Controls.Add(this.textBox_trans_y);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_check_answ1);
            this.Controls.Add(this.button_exec);
            this.Controls.Add(this.textBox_scaling);
            this.Controls.Add(this.textBox_rotation);
            this.Controls.Add(this.textBox_trans_x);
            this.Controls.Add(this.label_scaling);
            this.Controls.Add(this.label_rotation);
            this.Controls.Add(this.label_translation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.button_make);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_make;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_translation;
        private System.Windows.Forms.Label label_rotation;
        private System.Windows.Forms.Label label_scaling;
        private System.Windows.Forms.TextBox textBox_trans_x;
        private System.Windows.Forms.TextBox textBox_rotation;
        private System.Windows.Forms.TextBox textBox_scaling;
        private System.Windows.Forms.Button button_exec;
        private System.Windows.Forms.Label label_check_answ1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_trans_y;
        private System.Windows.Forms.Label label_check2;
        private System.Windows.Forms.Label label_check_answ2;
        private System.Windows.Forms.Label label_check1;
        private System.Windows.Forms.TextBox textBox_x;
        private System.Windows.Forms.TextBox textBox_y;
        private System.Windows.Forms.Label label_point;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

