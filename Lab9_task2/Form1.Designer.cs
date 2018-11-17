namespace Lab9_task2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_refl_z = new System.Windows.Forms.Button();
            this.button_refl_y = new System.Windows.Forms.Button();
            this.button_refl_x = new System.Windows.Forms.Button();
            this.button_octaeder = new System.Windows.Forms.Button();
            this.button_tetraeder = new System.Windows.Forms.Button();
            this.rot_line_z2 = new System.Windows.Forms.TextBox();
            this.rot_line_y2 = new System.Windows.Forms.TextBox();
            this.rot_line_x2 = new System.Windows.Forms.TextBox();
            this.rot_line_z1 = new System.Windows.Forms.TextBox();
            this.rot_line_y1 = new System.Windows.Forms.TextBox();
            this.rot_line_x1 = new System.Windows.Forms.TextBox();
            this.clear_button = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_exec = new System.Windows.Forms.Button();
            this.scaling_z = new System.Windows.Forms.TextBox();
            this.scaling_y = new System.Windows.Forms.TextBox();
            this.scaling_x = new System.Windows.Forms.TextBox();
            this.label_scaling = new System.Windows.Forms.Label();
            this.rot_angle = new System.Windows.Forms.TextBox();
            this.trans_z = new System.Windows.Forms.TextBox();
            this.trans_y = new System.Windows.Forms.TextBox();
            this.trans_x = new System.Windows.Forms.TextBox();
            this.label_rotation = new System.Windows.Forms.Label();
            this.label_translation = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_cube = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(363, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(878, 671);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 365);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 83;
            this.label4.Text = "Отразить по:";
            // 
            // button_refl_z
            // 
            this.button_refl_z.Location = new System.Drawing.Point(175, 385);
            this.button_refl_z.Margin = new System.Windows.Forms.Padding(4);
            this.button_refl_z.Name = "button_refl_z";
            this.button_refl_z.Size = new System.Drawing.Size(69, 28);
            this.button_refl_z.TabIndex = 82;
            this.button_refl_z.Text = "Z";
            this.button_refl_z.UseVisualStyleBackColor = true;
            this.button_refl_z.Click += new System.EventHandler(this.button8_Click);
            // 
            // button_refl_y
            // 
            this.button_refl_y.Location = new System.Drawing.Point(97, 385);
            this.button_refl_y.Margin = new System.Windows.Forms.Padding(4);
            this.button_refl_y.Name = "button_refl_y";
            this.button_refl_y.Size = new System.Drawing.Size(69, 28);
            this.button_refl_y.TabIndex = 81;
            this.button_refl_y.Text = "Y";
            this.button_refl_y.UseVisualStyleBackColor = true;
            this.button_refl_y.Click += new System.EventHandler(this.button7_Click);
            // 
            // button_refl_x
            // 
            this.button_refl_x.Location = new System.Drawing.Point(20, 385);
            this.button_refl_x.Margin = new System.Windows.Forms.Padding(4);
            this.button_refl_x.Name = "button_refl_x";
            this.button_refl_x.Size = new System.Drawing.Size(69, 28);
            this.button_refl_x.TabIndex = 80;
            this.button_refl_x.Text = "X";
            this.button_refl_x.UseVisualStyleBackColor = true;
            this.button_refl_x.Click += new System.EventHandler(this.button6_Click);
            // 
            // button_octaeder
            // 
            this.button_octaeder.Location = new System.Drawing.Point(241, 438);
            this.button_octaeder.Margin = new System.Windows.Forms.Padding(4);
            this.button_octaeder.Name = "button_octaeder";
            this.button_octaeder.Size = new System.Drawing.Size(101, 60);
            this.button_octaeder.TabIndex = 79;
            this.button_octaeder.Text = "Октаэдр";
            this.button_octaeder.UseVisualStyleBackColor = true;
            this.button_octaeder.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_tetraeder
            // 
            this.button_tetraeder.Location = new System.Drawing.Point(132, 438);
            this.button_tetraeder.Margin = new System.Windows.Forms.Padding(4);
            this.button_tetraeder.Name = "button_tetraeder";
            this.button_tetraeder.Size = new System.Drawing.Size(101, 60);
            this.button_tetraeder.TabIndex = 78;
            this.button_tetraeder.Text = "Тетраэдр";
            this.button_tetraeder.UseVisualStyleBackColor = true;
            this.button_tetraeder.Click += new System.EventHandler(this.button2_Click);
            // 
            // rot_line_z2
            // 
            this.rot_line_z2.Enabled = false;
            this.rot_line_z2.Location = new System.Drawing.Point(227, 258);
            this.rot_line_z2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_z2.Name = "rot_line_z2";
            this.rot_line_z2.Size = new System.Drawing.Size(69, 22);
            this.rot_line_z2.TabIndex = 77;
            this.rot_line_z2.Text = "1";
            // 
            // rot_line_y2
            // 
            this.rot_line_y2.Enabled = false;
            this.rot_line_y2.Location = new System.Drawing.Point(120, 258);
            this.rot_line_y2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_y2.Name = "rot_line_y2";
            this.rot_line_y2.Size = new System.Drawing.Size(68, 22);
            this.rot_line_y2.TabIndex = 76;
            this.rot_line_y2.Text = "1";
            // 
            // rot_line_x2
            // 
            this.rot_line_x2.Enabled = false;
            this.rot_line_x2.Location = new System.Drawing.Point(20, 258);
            this.rot_line_x2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_x2.Name = "rot_line_x2";
            this.rot_line_x2.Size = new System.Drawing.Size(68, 22);
            this.rot_line_x2.TabIndex = 75;
            this.rot_line_x2.Text = "1";
            // 
            // rot_line_z1
            // 
            this.rot_line_z1.Enabled = false;
            this.rot_line_z1.Location = new System.Drawing.Point(227, 226);
            this.rot_line_z1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_z1.Name = "rot_line_z1";
            this.rot_line_z1.Size = new System.Drawing.Size(69, 22);
            this.rot_line_z1.TabIndex = 74;
            this.rot_line_z1.Text = "0";
            // 
            // rot_line_y1
            // 
            this.rot_line_y1.Enabled = false;
            this.rot_line_y1.Location = new System.Drawing.Point(120, 226);
            this.rot_line_y1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_y1.Name = "rot_line_y1";
            this.rot_line_y1.Size = new System.Drawing.Size(68, 22);
            this.rot_line_y1.TabIndex = 73;
            this.rot_line_y1.Text = "0";
            // 
            // rot_line_x1
            // 
            this.rot_line_x1.Enabled = false;
            this.rot_line_x1.Location = new System.Drawing.Point(20, 226);
            this.rot_line_x1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_x1.Name = "rot_line_x1";
            this.rot_line_x1.Size = new System.Drawing.Size(68, 22);
            this.rot_line_x1.TabIndex = 72;
            this.rot_line_x1.Text = "0";
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(128, 302);
            this.clear_button.Margin = new System.Windows.Forms.Padding(4);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(100, 28);
            this.clear_button.TabIndex = 71;
            this.clear_button.Text = "Очистить";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Прямая, параллельная оси X",
            "Прямая, параллельная оси Y",
            "Прямая, параллельная оси Z",
            "Задать свою прямую"});
            this.comboBox2.Location = new System.Drawing.Point(20, 194);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(276, 24);
            this.comboBox2.TabIndex = 70;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 174);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 17);
            this.label3.TabIndex = 69;
            this.label3.Text = "Выбрать прямую для поворота";
            // 
            // button_exec
            // 
            this.button_exec.Location = new System.Drawing.Point(20, 302);
            this.button_exec.Margin = new System.Windows.Forms.Padding(4);
            this.button_exec.Name = "button_exec";
            this.button_exec.Size = new System.Drawing.Size(100, 28);
            this.button_exec.TabIndex = 68;
            this.button_exec.Text = "Выполнить";
            this.button_exec.UseVisualStyleBackColor = true;
            this.button_exec.Click += new System.EventHandler(this.button_exec_Click);
            // 
            // scaling_z
            // 
            this.scaling_z.Location = new System.Drawing.Point(256, 119);
            this.scaling_z.Margin = new System.Windows.Forms.Padding(4);
            this.scaling_z.Name = "scaling_z";
            this.scaling_z.Size = new System.Drawing.Size(40, 22);
            this.scaling_z.TabIndex = 67;
            this.scaling_z.Text = "1";
            // 
            // scaling_y
            // 
            this.scaling_y.Location = new System.Drawing.Point(207, 119);
            this.scaling_y.Margin = new System.Windows.Forms.Padding(4);
            this.scaling_y.Name = "scaling_y";
            this.scaling_y.Size = new System.Drawing.Size(40, 22);
            this.scaling_y.TabIndex = 66;
            this.scaling_y.Text = "1";
            // 
            // scaling_x
            // 
            this.scaling_x.Location = new System.Drawing.Point(157, 119);
            this.scaling_x.Margin = new System.Windows.Forms.Padding(4);
            this.scaling_x.Name = "scaling_x";
            this.scaling_x.Size = new System.Drawing.Size(40, 22);
            this.scaling_x.TabIndex = 65;
            this.scaling_x.Text = "1";
            // 
            // label_scaling
            // 
            this.label_scaling.AutoSize = true;
            this.label_scaling.Location = new System.Drawing.Point(20, 123);
            this.label_scaling.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_scaling.Name = "label_scaling";
            this.label_scaling.Size = new System.Drawing.Size(118, 17);
            this.label_scaling.TabIndex = 64;
            this.label_scaling.Text = "Масштаб (x, y, z)";
            // 
            // rot_angle
            // 
            this.rot_angle.Location = new System.Drawing.Point(151, 75);
            this.rot_angle.Margin = new System.Windows.Forms.Padding(4);
            this.rot_angle.Name = "rot_angle";
            this.rot_angle.Size = new System.Drawing.Size(89, 22);
            this.rot_angle.TabIndex = 63;
            this.rot_angle.Text = "0";
            // 
            // trans_z
            // 
            this.trans_z.Location = new System.Drawing.Point(256, 34);
            this.trans_z.Margin = new System.Windows.Forms.Padding(4);
            this.trans_z.Name = "trans_z";
            this.trans_z.Size = new System.Drawing.Size(40, 22);
            this.trans_z.TabIndex = 62;
            this.trans_z.Text = "0";
            // 
            // trans_y
            // 
            this.trans_y.Location = new System.Drawing.Point(207, 34);
            this.trans_y.Margin = new System.Windows.Forms.Padding(4);
            this.trans_y.Name = "trans_y";
            this.trans_y.Size = new System.Drawing.Size(40, 22);
            this.trans_y.TabIndex = 61;
            this.trans_y.Text = "0";
            // 
            // trans_x
            // 
            this.trans_x.Location = new System.Drawing.Point(157, 34);
            this.trans_x.Margin = new System.Windows.Forms.Padding(4);
            this.trans_x.Name = "trans_x";
            this.trans_x.Size = new System.Drawing.Size(40, 22);
            this.trans_x.TabIndex = 60;
            this.trans_x.Text = "0";
            // 
            // label_rotation
            // 
            this.label_rotation.AutoSize = true;
            this.label_rotation.Location = new System.Drawing.Point(20, 78);
            this.label_rotation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_rotation.Name = "label_rotation";
            this.label_rotation.Size = new System.Drawing.Size(113, 17);
            this.label_rotation.TabIndex = 59;
            this.label_rotation.Text = "Поворот (angle)";
            // 
            // label_translation
            // 
            this.label_translation.AutoSize = true;
            this.label_translation.Location = new System.Drawing.Point(20, 38);
            this.label_translation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_translation.Name = "label_translation";
            this.label_translation.Size = new System.Drawing.Size(127, 17);
            this.label_translation.TabIndex = 58;
            this.label_translation.Text = "Смещение (x, y, z)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 17);
            this.label2.TabIndex = 57;
            this.label2.Text = "Афинные преобразования";
            // 
            // button_cube
            // 
            this.button_cube.Location = new System.Drawing.Point(23, 438);
            this.button_cube.Margin = new System.Windows.Forms.Padding(4);
            this.button_cube.Name = "button_cube";
            this.button_cube.Size = new System.Drawing.Size(101, 60);
            this.button_cube.TabIndex = 56;
            this.button_cube.Text = "Гексаэдр";
            this.button_cube.UseVisualStyleBackColor = true;
            this.button_cube.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(120, 586);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 43);
            this.button1.TabIndex = 84;
            this.button1.Text = "Open file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 737);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_refl_z);
            this.Controls.Add(this.button_refl_y);
            this.Controls.Add(this.button_refl_x);
            this.Controls.Add(this.button_octaeder);
            this.Controls.Add(this.button_tetraeder);
            this.Controls.Add(this.rot_line_z2);
            this.Controls.Add(this.rot_line_y2);
            this.Controls.Add(this.rot_line_x2);
            this.Controls.Add(this.rot_line_z1);
            this.Controls.Add(this.rot_line_y1);
            this.Controls.Add(this.rot_line_x1);
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
            this.Controls.Add(this.button_cube);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_refl_z;
        private System.Windows.Forms.Button button_refl_y;
        private System.Windows.Forms.Button button_refl_x;
        private System.Windows.Forms.Button button_octaeder;
        private System.Windows.Forms.Button button_tetraeder;
        private System.Windows.Forms.TextBox rot_line_z2;
        private System.Windows.Forms.TextBox rot_line_y2;
        private System.Windows.Forms.TextBox rot_line_x2;
        private System.Windows.Forms.TextBox rot_line_z1;
        private System.Windows.Forms.TextBox rot_line_y1;
        private System.Windows.Forms.TextBox rot_line_x1;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_exec;
        private System.Windows.Forms.TextBox scaling_z;
        private System.Windows.Forms.TextBox scaling_y;
        private System.Windows.Forms.TextBox scaling_x;
        private System.Windows.Forms.Label label_scaling;
        private System.Windows.Forms.TextBox rot_angle;
        private System.Windows.Forms.TextBox trans_z;
        private System.Windows.Forms.TextBox trans_y;
        private System.Windows.Forms.TextBox trans_x;
        private System.Windows.Forms.Label label_rotation;
        private System.Windows.Forms.Label label_translation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_cube;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

