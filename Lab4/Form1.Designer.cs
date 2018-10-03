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
            this.button_check = new System.Windows.Forms.Button();
            this.label_check = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_trans_y = new System.Windows.Forms.TextBox();
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
            this.button_exec.Location = new System.Drawing.Point(12, 196);
            this.button_exec.Name = "button_exec";
            this.button_exec.Size = new System.Drawing.Size(75, 23);
            this.button_exec.TabIndex = 10;
            this.button_exec.Text = "Выполнить";
            this.button_exec.UseVisualStyleBackColor = true;
            this.button_exec.Click += new System.EventHandler(this.button_exec_Click);
            // 
            // button_check
            // 
            this.button_check.Location = new System.Drawing.Point(12, 265);
            this.button_check.Name = "button_check";
            this.button_check.Size = new System.Drawing.Size(141, 48);
            this.button_check.TabIndex = 11;
            this.button_check.Text = "Принадлежит ли точка";
            this.button_check.UseVisualStyleBackColor = true;
            // 
            // label_check
            // 
            this.label_check.AutoSize = true;
            this.label_check.Location = new System.Drawing.Point(13, 316);
            this.label_check.Name = "label_check";
            this.label_check.Size = new System.Drawing.Size(37, 13);
            this.label_check.TabIndex = 12;
            this.label_check.Text = "Ответ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 246);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 475);
            this.Controls.Add(this.textBox_trans_y);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_check);
            this.Controls.Add(this.button_check);
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
        private System.Windows.Forms.Button button_check;
        private System.Windows.Forms.Label label_check;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_trans_y;
    }
}

