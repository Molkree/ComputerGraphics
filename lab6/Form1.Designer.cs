namespace lab6
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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_translation = new System.Windows.Forms.Label();
            this.label_rotation = new System.Windows.Forms.Label();
            this.trans_x = new System.Windows.Forms.TextBox();
            this.trans_y = new System.Windows.Forms.TextBox();
            this.trans_z = new System.Windows.Forms.TextBox();
            this.rot_angle = new System.Windows.Forms.TextBox();
            this.label_scaling = new System.Windows.Forms.Label();
            this.scaling_x = new System.Windows.Forms.TextBox();
            this.scaling_y = new System.Windows.Forms.TextBox();
            this.scaling_z = new System.Windows.Forms.TextBox();
            this.button_exec = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.clear_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(258, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(599, 491);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 450);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "Сделать кубик";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "перспективная",
            "изометрическая",
            "ортографическая (ось X)",
            "ортографическая (ось Y)",
            "ортографическая (ось Z)"});
            this.comboBox1.Location = new System.Drawing.Point(12, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(136, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите вид проекции";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Афинные преобразования";
            // 
            // label_translation
            // 
            this.label_translation.AutoSize = true;
            this.label_translation.Location = new System.Drawing.Point(12, 85);
            this.label_translation.Name = "label_translation";
            this.label_translation.Size = new System.Drawing.Size(97, 13);
            this.label_translation.TabIndex = 5;
            this.label_translation.Text = "Смещение (x, y, z)";
            // 
            // label_rotation
            // 
            this.label_rotation.AutoSize = true;
            this.label_rotation.Location = new System.Drawing.Point(12, 118);
            this.label_rotation.Name = "label_rotation";
            this.label_rotation.Size = new System.Drawing.Size(85, 13);
            this.label_rotation.TabIndex = 6;
            this.label_rotation.Text = "Поворот (angle)";
            // 
            // trans_x
            // 
            this.trans_x.Location = new System.Drawing.Point(115, 82);
            this.trans_x.Name = "trans_x";
            this.trans_x.Size = new System.Drawing.Size(31, 20);
            this.trans_x.TabIndex = 8;
            this.trans_x.Text = "0";
            this.trans_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // trans_y
            // 
            this.trans_y.Location = new System.Drawing.Point(152, 82);
            this.trans_y.Name = "trans_y";
            this.trans_y.Size = new System.Drawing.Size(31, 20);
            this.trans_y.TabIndex = 9;
            this.trans_y.Text = "0";
            this.trans_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // trans_z
            // 
            this.trans_z.Location = new System.Drawing.Point(189, 82);
            this.trans_z.Name = "trans_z";
            this.trans_z.Size = new System.Drawing.Size(31, 20);
            this.trans_z.TabIndex = 10;
            this.trans_z.Text = "0";
            this.trans_z.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // rot_angle
            // 
            this.rot_angle.Location = new System.Drawing.Point(110, 115);
            this.rot_angle.Name = "rot_angle";
            this.rot_angle.Size = new System.Drawing.Size(68, 20);
            this.rot_angle.TabIndex = 11;
            this.rot_angle.Text = "0";
            this.rot_angle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // label_scaling
            // 
            this.label_scaling.AutoSize = true;
            this.label_scaling.Location = new System.Drawing.Point(12, 154);
            this.label_scaling.Name = "label_scaling";
            this.label_scaling.Size = new System.Drawing.Size(89, 13);
            this.label_scaling.TabIndex = 12;
            this.label_scaling.Text = "Масштаб (x, y, z)";
            // 
            // scaling_x
            // 
            this.scaling_x.Location = new System.Drawing.Point(115, 151);
            this.scaling_x.Name = "scaling_x";
            this.scaling_x.Size = new System.Drawing.Size(31, 20);
            this.scaling_x.TabIndex = 25;
            this.scaling_x.Text = "1";
            this.scaling_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // scaling_y
            // 
            this.scaling_y.Location = new System.Drawing.Point(152, 151);
            this.scaling_y.Name = "scaling_y";
            this.scaling_y.Size = new System.Drawing.Size(31, 20);
            this.scaling_y.TabIndex = 28;
            this.scaling_y.Text = "1";
            this.scaling_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // scaling_z
            // 
            this.scaling_z.Location = new System.Drawing.Point(189, 151);
            this.scaling_z.Name = "scaling_z";
            this.scaling_z.Size = new System.Drawing.Size(31, 20);
            this.scaling_z.TabIndex = 29;
            this.scaling_z.Text = "1";
            this.scaling_z.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // button_exec
            // 
            this.button_exec.Location = new System.Drawing.Point(12, 288);
            this.button_exec.Name = "button_exec";
            this.button_exec.Size = new System.Drawing.Size(75, 23);
            this.button_exec.TabIndex = 37;
            this.button_exec.Text = "Выполнить";
            this.button_exec.UseVisualStyleBackColor = true;
            this.button_exec.Click += new System.EventHandler(this.button_exec_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Выбрать прямую для поворота";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Прямая, параллельная оси X",
            "Прямая, параллельная оси Y",
            "Прямая, параллельная оси Z",
            "Задать свою прямую"});
            this.comboBox2.Location = new System.Drawing.Point(12, 212);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(208, 21);
            this.comboBox2.TabIndex = 40;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(15, 413);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(75, 23);
            this.clear_button.TabIndex = 41;
            this.clear_button.Text = "Очистить";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 515);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_exec);
            this.Controls.Add(this.scaling_z);
            this.Controls.Add(this.scaling_y);
            this.Controls.Add(this.scaling_x);
            this.Controls.Add(this.label_scaling);
            this.Controls.Add(this.rot_angle);
            this.Controls.Add(this.trans_z);
            this.Controls.Add(this.trans_y);
            this.Controls.Add(this.trans_x);
            this.Controls.Add(this.label_rotation);
            this.Controls.Add(this.label_translation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_translation;
        private System.Windows.Forms.Label label_rotation;
        private System.Windows.Forms.TextBox trans_x;
        private System.Windows.Forms.TextBox trans_y;
        private System.Windows.Forms.TextBox trans_z;
        private System.Windows.Forms.TextBox rot_angle;
        private System.Windows.Forms.Label label_scaling;
        private System.Windows.Forms.TextBox scaling_x;
        private System.Windows.Forms.TextBox scaling_y;
        private System.Windows.Forms.TextBox scaling_z;
        private System.Windows.Forms.Button button_exec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button clear_button;
    }
}

