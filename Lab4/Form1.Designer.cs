﻿namespace Lab4
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
            this.textBox_scaling_x = new System.Windows.Forms.TextBox();
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
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_scaling_y = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(264, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(780, 555);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // button_make
            // 
            this.button_make.Location = new System.Drawing.Point(16, 15);
            this.button_make.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_make.Name = "button_make";
            this.button_make.Size = new System.Drawing.Size(149, 28);
            this.button_make.TabIndex = 1;
            this.button_make.Text = "Задать примитив";
            this.button_make.UseVisualStyleBackColor = true;
            this.button_make.Click += new System.EventHandler(this.button_make_Click);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(16, 50);
            this.button_clear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(149, 28);
            this.button_clear.TabIndex = 2;
            this.button_clear.Text = "Очистить";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 111);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Афинные преобразования";
            // 
            // label_translation
            // 
            this.label_translation.AutoSize = true;
            this.label_translation.Location = new System.Drawing.Point(17, 144);
            this.label_translation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_translation.Name = "label_translation";
            this.label_translation.Size = new System.Drawing.Size(112, 17);
            this.label_translation.TabIndex = 4;
            this.label_translation.Text = "Смещение (x, y)";
            // 
            // label_rotation
            // 
            this.label_rotation.AutoSize = true;
            this.label_rotation.Location = new System.Drawing.Point(16, 176);
            this.label_rotation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_rotation.Name = "label_rotation";
            this.label_rotation.Size = new System.Drawing.Size(113, 17);
            this.label_rotation.TabIndex = 5;
            this.label_rotation.Text = "Поворот (angle)";
            // 
            // label_scaling
            // 
            this.label_scaling.AutoSize = true;
            this.label_scaling.Location = new System.Drawing.Point(21, 213);
            this.label_scaling.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_scaling.Name = "label_scaling";
            this.label_scaling.Size = new System.Drawing.Size(109, 17);
            this.label_scaling.TabIndex = 6;
            this.label_scaling.Text = "Масштаб (coef)";
            // 
            // textBox_trans_x
            // 
            this.textBox_trans_x.Location = new System.Drawing.Point(140, 140);
            this.textBox_trans_x.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_trans_x.Name = "textBox_trans_x";
            this.textBox_trans_x.Size = new System.Drawing.Size(40, 22);
            this.textBox_trans_x.TabIndex = 7;
            this.textBox_trans_x.Text = "0";
            this.textBox_trans_x.TextChanged += new System.EventHandler(this.textBox_scaling_TextChanged);
            this.textBox_trans_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // textBox_rotation
            // 
            this.textBox_rotation.Location = new System.Drawing.Point(140, 172);
            this.textBox_rotation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_rotation.Name = "textBox_rotation";
            this.textBox_rotation.Size = new System.Drawing.Size(89, 22);
            this.textBox_rotation.TabIndex = 8;
            this.textBox_rotation.Text = "0";
            this.textBox_rotation.TextChanged += new System.EventHandler(this.textBox_rotation_TextChanged);
            this.textBox_rotation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // textBox_scaling_x
            // 
            this.textBox_scaling_x.Location = new System.Drawing.Point(189, 204);
            this.textBox_scaling_x.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_scaling_x.Name = "textBox_scaling_x";
            this.textBox_scaling_x.Size = new System.Drawing.Size(40, 22);
            this.textBox_scaling_x.TabIndex = 9;
            this.textBox_scaling_x.Text = "1";
            this.textBox_scaling_x.TextChanged += new System.EventHandler(this.textBox_scaling_TextChanged);
            this.textBox_scaling_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // button_exec
            // 
            this.button_exec.Location = new System.Drawing.Point(16, 351);
            this.button_exec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_exec.Name = "button_exec";
            this.button_exec.Size = new System.Drawing.Size(100, 28);
            this.button_exec.TabIndex = 10;
            this.button_exec.Text = "Выполнить";
            this.button_exec.UseVisualStyleBackColor = true;
            this.button_exec.Click += new System.EventHandler(this.button_exec_Click);
            // 
            // label_check_answ1
            // 
            this.label_check_answ1.AutoSize = true;
            this.label_check_answ1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_check_answ1.Location = new System.Drawing.Point(68, 460);
            this.label_check_answ1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_check_answ1.Name = "label_check_answ1";
            this.label_check_answ1.Size = new System.Drawing.Size(48, 17);
            this.label_check_answ1.TabIndex = 12;
            this.label_check_answ1.Text = "Ответ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 394);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Проверки:";
            // 
            // textBox_trans_y
            // 
            this.textBox_trans_y.Location = new System.Drawing.Point(189, 140);
            this.textBox_trans_y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_trans_y.Name = "textBox_trans_y";
            this.textBox_trans_y.Size = new System.Drawing.Size(40, 22);
            this.textBox_trans_y.TabIndex = 14;
            this.textBox_trans_y.Text = "0";
            this.textBox_trans_y.TextChanged += new System.EventHandler(this.textBox_scaling_TextChanged);
            this.textBox_trans_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // label_check2
            // 
            this.label_check2.AutoSize = true;
            this.label_check2.Location = new System.Drawing.Point(12, 495);
            this.label_check2.Name = "label_check2";
            this.label_check2.Size = new System.Drawing.Size(192, 17);
            this.label_check2.TabIndex = 15;
            this.label_check2.Text = "Для проверки пересечения";
            // 
            // label_check_answ2
            // 
            this.label_check_answ2.AutoSize = true;
            this.label_check_answ2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_check_answ2.Location = new System.Drawing.Point(68, 535);
            this.label_check_answ2.Name = "label_check_answ2";
            this.label_check_answ2.Size = new System.Drawing.Size(48, 17);
            this.label_check_answ2.TabIndex = 16;
            this.label_check_answ2.Text = "Ответ";
            // 
            // label_check1
            // 
            this.label_check1.AutoSize = true;
            this.label_check1.Location = new System.Drawing.Point(12, 422);
            this.label_check1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_check1.Name = "label_check1";
            this.label_check1.Size = new System.Drawing.Size(176, 17);
            this.label_check1.TabIndex = 17;
            this.label_check1.Text = "Точка справа или слева?";
            // 
            // textBox_x
            // 
            this.textBox_x.Location = new System.Drawing.Point(188, 276);
            this.textBox_x.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_x.Name = "textBox_x";
            this.textBox_x.Size = new System.Drawing.Size(40, 22);
            this.textBox_x.TabIndex = 18;
            this.textBox_x.Text = "0";
            this.textBox_x.TextChanged += new System.EventHandler(this.textBox_rot_TextChanged);
            this.textBox_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // textBox_y
            // 
            this.textBox_y.Location = new System.Drawing.Point(188, 308);
            this.textBox_y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_y.Name = "textBox_y";
            this.textBox_y.Size = new System.Drawing.Size(40, 22);
            this.textBox_y.TabIndex = 19;
            this.textBox_y.Text = "0";
            this.textBox_y.TextChanged += new System.EventHandler(this.textBox_rot_TextChanged);
            this.textBox_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // label_point
            // 
            this.label_point.AutoSize = true;
            this.label_point.Location = new System.Drawing.Point(16, 284);
            this.label_point.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_point.Name = "label_point";
            this.label_point.Size = new System.Drawing.Size(142, 17);
            this.label_point.TabIndex = 20;
            this.label_point.Text = "Точка для поворота";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 279);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 17);
            this.label3.TabIndex = 21;
            this.label3.Text = "x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 311);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 208);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "x";
            // 
            // textBox_scaling_y
            // 
            this.textBox_scaling_y.Location = new System.Drawing.Point(188, 236);
            this.textBox_scaling_y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_scaling_y.Name = "textBox_scaling_y";
            this.textBox_scaling_y.Size = new System.Drawing.Size(40, 22);
            this.textBox_scaling_y.TabIndex = 24;
            this.textBox_scaling_y.Text = "1";
            this.textBox_scaling_y.TextChanged += new System.EventHandler(this.textBox_scaling_TextChanged);
            this.textBox_scaling_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 240);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 17);
            this.label6.TabIndex = 25;
            this.label6.Text = "y";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 585);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_scaling_y);
            this.Controls.Add(this.label5);
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
            this.Controls.Add(this.textBox_scaling_x);
            this.Controls.Add(this.textBox_rotation);
            this.Controls.Add(this.textBox_trans_x);
            this.Controls.Add(this.label_scaling);
            this.Controls.Add(this.label_rotation);
            this.Controls.Add(this.label_translation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.button_make);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.TextBox textBox_scaling_x;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_scaling_y;
        private System.Windows.Forms.Label label6;
    }
}

