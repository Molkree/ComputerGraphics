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
            this.button_cube = new System.Windows.Forms.Button();
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
            this.rot_line_x1 = new System.Windows.Forms.TextBox();
            this.rot_line_y1 = new System.Windows.Forms.TextBox();
            this.rot_line_z1 = new System.Windows.Forms.TextBox();
            this.rot_line_z2 = new System.Windows.Forms.TextBox();
            this.rot_line_y2 = new System.Windows.Forms.TextBox();
            this.rot_line_x2 = new System.Windows.Forms.TextBox();
            this.button_tetraeder = new System.Windows.Forms.Button();
            this.button_octaeder = new System.Windows.Forms.Button();
            this.button_ikosaeder = new System.Windows.Forms.Button();
            this.button_dodecaeder = new System.Windows.Forms.Button();
            this.button_refl_x = new System.Windows.Forms.Button();
            this.button_refl_y = new System.Windows.Forms.Button();
            this.button_refl_z = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.camera_x = new System.Windows.Forms.Label();
            this.camera_y = new System.Windows.Forms.Label();
            this.camera_z = new System.Windows.Forms.Label();
            this.trans_z_camera = new System.Windows.Forms.TextBox();
            this.trans_y_camera = new System.Windows.Forms.TextBox();
            this.trans_x_camera = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.camera_axis_picker = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rot_angle_camera = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button_exec_camera = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(384, 55);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(799, 604);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_cube
            // 
            this.button_cube.Location = new System.Drawing.Point(19, 505);
            this.button_cube.Margin = new System.Windows.Forms.Padding(4);
            this.button_cube.Name = "button_cube";
            this.button_cube.Size = new System.Drawing.Size(101, 60);
            this.button_cube.TabIndex = 1;
            this.button_cube.Text = "Гексаэдр";
            this.button_cube.UseVisualStyleBackColor = true;
            this.button_cube.Click += new System.EventHandler(this.button1_Click);
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
            this.comboBox1.Location = new System.Drawing.Point(16, 34);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(180, 24);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите вид проекции";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Афинные преобразования";
            // 
            // label_translation
            // 
            this.label_translation.AutoSize = true;
            this.label_translation.Location = new System.Drawing.Point(16, 105);
            this.label_translation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_translation.Name = "label_translation";
            this.label_translation.Size = new System.Drawing.Size(127, 17);
            this.label_translation.TabIndex = 5;
            this.label_translation.Text = "Смещение (x, y, z)";
            // 
            // label_rotation
            // 
            this.label_rotation.AutoSize = true;
            this.label_rotation.Location = new System.Drawing.Point(16, 145);
            this.label_rotation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_rotation.Name = "label_rotation";
            this.label_rotation.Size = new System.Drawing.Size(113, 17);
            this.label_rotation.TabIndex = 6;
            this.label_rotation.Text = "Поворот (angle)";
            // 
            // trans_x
            // 
            this.trans_x.Location = new System.Drawing.Point(153, 101);
            this.trans_x.Margin = new System.Windows.Forms.Padding(4);
            this.trans_x.Name = "trans_x";
            this.trans_x.Size = new System.Drawing.Size(40, 22);
            this.trans_x.TabIndex = 8;
            this.trans_x.Text = "0";
            this.trans_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // trans_y
            // 
            this.trans_y.Location = new System.Drawing.Point(203, 101);
            this.trans_y.Margin = new System.Windows.Forms.Padding(4);
            this.trans_y.Name = "trans_y";
            this.trans_y.Size = new System.Drawing.Size(40, 22);
            this.trans_y.TabIndex = 9;
            this.trans_y.Text = "0";
            this.trans_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // trans_z
            // 
            this.trans_z.Location = new System.Drawing.Point(252, 101);
            this.trans_z.Margin = new System.Windows.Forms.Padding(4);
            this.trans_z.Name = "trans_z";
            this.trans_z.Size = new System.Drawing.Size(40, 22);
            this.trans_z.TabIndex = 10;
            this.trans_z.Text = "0";
            this.trans_z.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // rot_angle
            // 
            this.rot_angle.Location = new System.Drawing.Point(147, 142);
            this.rot_angle.Margin = new System.Windows.Forms.Padding(4);
            this.rot_angle.Name = "rot_angle";
            this.rot_angle.Size = new System.Drawing.Size(89, 22);
            this.rot_angle.TabIndex = 11;
            this.rot_angle.Text = "0";
            this.rot_angle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_int);
            // 
            // label_scaling
            // 
            this.label_scaling.AutoSize = true;
            this.label_scaling.Location = new System.Drawing.Point(16, 190);
            this.label_scaling.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_scaling.Name = "label_scaling";
            this.label_scaling.Size = new System.Drawing.Size(118, 17);
            this.label_scaling.TabIndex = 12;
            this.label_scaling.Text = "Масштаб (x, y, z)";
            // 
            // scaling_x
            // 
            this.scaling_x.Location = new System.Drawing.Point(153, 186);
            this.scaling_x.Margin = new System.Windows.Forms.Padding(4);
            this.scaling_x.Name = "scaling_x";
            this.scaling_x.Size = new System.Drawing.Size(40, 22);
            this.scaling_x.TabIndex = 25;
            this.scaling_x.Text = "1";
            this.scaling_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // scaling_y
            // 
            this.scaling_y.Location = new System.Drawing.Point(203, 186);
            this.scaling_y.Margin = new System.Windows.Forms.Padding(4);
            this.scaling_y.Name = "scaling_y";
            this.scaling_y.Size = new System.Drawing.Size(40, 22);
            this.scaling_y.TabIndex = 28;
            this.scaling_y.Text = "1";
            this.scaling_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // scaling_z
            // 
            this.scaling_z.Location = new System.Drawing.Point(252, 186);
            this.scaling_z.Margin = new System.Windows.Forms.Padding(4);
            this.scaling_z.Name = "scaling_z";
            this.scaling_z.Size = new System.Drawing.Size(40, 22);
            this.scaling_z.TabIndex = 29;
            this.scaling_z.Text = "1";
            this.scaling_z.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_double);
            // 
            // button_exec
            // 
            this.button_exec.Location = new System.Drawing.Point(16, 369);
            this.button_exec.Margin = new System.Windows.Forms.Padding(4);
            this.button_exec.Name = "button_exec";
            this.button_exec.Size = new System.Drawing.Size(100, 28);
            this.button_exec.TabIndex = 37;
            this.button_exec.Text = "Выполнить";
            this.button_exec.UseVisualStyleBackColor = true;
            this.button_exec.Click += new System.EventHandler(this.button_exec_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 241);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 17);
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
            this.comboBox2.Location = new System.Drawing.Point(16, 261);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(276, 24);
            this.comboBox2.TabIndex = 40;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(124, 369);
            this.clear_button.Margin = new System.Windows.Forms.Padding(4);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(100, 28);
            this.clear_button.TabIndex = 41;
            this.clear_button.Text = "Очистить";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // rot_line_x1
            // 
            this.rot_line_x1.Enabled = false;
            this.rot_line_x1.Location = new System.Drawing.Point(16, 293);
            this.rot_line_x1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_x1.Name = "rot_line_x1";
            this.rot_line_x1.Size = new System.Drawing.Size(68, 22);
            this.rot_line_x1.TabIndex = 42;
            this.rot_line_x1.Text = "0";
            // 
            // rot_line_y1
            // 
            this.rot_line_y1.Enabled = false;
            this.rot_line_y1.Location = new System.Drawing.Point(116, 293);
            this.rot_line_y1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_y1.Name = "rot_line_y1";
            this.rot_line_y1.Size = new System.Drawing.Size(68, 22);
            this.rot_line_y1.TabIndex = 43;
            this.rot_line_y1.Text = "0";
            // 
            // rot_line_z1
            // 
            this.rot_line_z1.Enabled = false;
            this.rot_line_z1.Location = new System.Drawing.Point(223, 293);
            this.rot_line_z1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_z1.Name = "rot_line_z1";
            this.rot_line_z1.Size = new System.Drawing.Size(69, 22);
            this.rot_line_z1.TabIndex = 44;
            this.rot_line_z1.Text = "0";
            // 
            // rot_line_z2
            // 
            this.rot_line_z2.Enabled = false;
            this.rot_line_z2.Location = new System.Drawing.Point(223, 325);
            this.rot_line_z2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_z2.Name = "rot_line_z2";
            this.rot_line_z2.Size = new System.Drawing.Size(69, 22);
            this.rot_line_z2.TabIndex = 47;
            this.rot_line_z2.Text = "1";
            // 
            // rot_line_y2
            // 
            this.rot_line_y2.Enabled = false;
            this.rot_line_y2.Location = new System.Drawing.Point(116, 325);
            this.rot_line_y2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_y2.Name = "rot_line_y2";
            this.rot_line_y2.Size = new System.Drawing.Size(68, 22);
            this.rot_line_y2.TabIndex = 46;
            this.rot_line_y2.Text = "1";
            // 
            // rot_line_x2
            // 
            this.rot_line_x2.Enabled = false;
            this.rot_line_x2.Location = new System.Drawing.Point(16, 325);
            this.rot_line_x2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_x2.Name = "rot_line_x2";
            this.rot_line_x2.Size = new System.Drawing.Size(68, 22);
            this.rot_line_x2.TabIndex = 45;
            this.rot_line_x2.Text = "1";
            // 
            // button_tetraeder
            // 
            this.button_tetraeder.Location = new System.Drawing.Point(128, 505);
            this.button_tetraeder.Margin = new System.Windows.Forms.Padding(4);
            this.button_tetraeder.Name = "button_tetraeder";
            this.button_tetraeder.Size = new System.Drawing.Size(101, 60);
            this.button_tetraeder.TabIndex = 48;
            this.button_tetraeder.Text = "Тетраэдр";
            this.button_tetraeder.UseVisualStyleBackColor = true;
            this.button_tetraeder.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_octaeder
            // 
            this.button_octaeder.Location = new System.Drawing.Point(237, 505);
            this.button_octaeder.Margin = new System.Windows.Forms.Padding(4);
            this.button_octaeder.Name = "button_octaeder";
            this.button_octaeder.Size = new System.Drawing.Size(101, 60);
            this.button_octaeder.TabIndex = 49;
            this.button_octaeder.Text = "Октаэдр";
            this.button_octaeder.UseVisualStyleBackColor = true;
            this.button_octaeder.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_ikosaeder
            // 
            this.button_ikosaeder.Location = new System.Drawing.Point(128, 572);
            this.button_ikosaeder.Margin = new System.Windows.Forms.Padding(4);
            this.button_ikosaeder.Name = "button_ikosaeder";
            this.button_ikosaeder.Size = new System.Drawing.Size(101, 60);
            this.button_ikosaeder.TabIndex = 50;
            this.button_ikosaeder.Text = "Икосаэдр";
            this.button_ikosaeder.UseVisualStyleBackColor = true;
            this.button_ikosaeder.Click += new System.EventHandler(this.button4_Click);
            // 
            // button_dodecaeder
            // 
            this.button_dodecaeder.Location = new System.Drawing.Point(19, 572);
            this.button_dodecaeder.Margin = new System.Windows.Forms.Padding(4);
            this.button_dodecaeder.Name = "button_dodecaeder";
            this.button_dodecaeder.Size = new System.Drawing.Size(100, 60);
            this.button_dodecaeder.TabIndex = 51;
            this.button_dodecaeder.Text = "Додекаэдр";
            this.button_dodecaeder.UseVisualStyleBackColor = true;
            this.button_dodecaeder.Click += new System.EventHandler(this.button5_Click);
            // 
            // button_refl_x
            // 
            this.button_refl_x.Location = new System.Drawing.Point(16, 452);
            this.button_refl_x.Margin = new System.Windows.Forms.Padding(4);
            this.button_refl_x.Name = "button_refl_x";
            this.button_refl_x.Size = new System.Drawing.Size(69, 28);
            this.button_refl_x.TabIndex = 52;
            this.button_refl_x.Text = "X";
            this.button_refl_x.UseVisualStyleBackColor = true;
            this.button_refl_x.Click += new System.EventHandler(this.button6_Click);
            // 
            // button_refl_y
            // 
            this.button_refl_y.Location = new System.Drawing.Point(93, 452);
            this.button_refl_y.Margin = new System.Windows.Forms.Padding(4);
            this.button_refl_y.Name = "button_refl_y";
            this.button_refl_y.Size = new System.Drawing.Size(69, 28);
            this.button_refl_y.TabIndex = 53;
            this.button_refl_y.Text = "Y";
            this.button_refl_y.UseVisualStyleBackColor = true;
            this.button_refl_y.Click += new System.EventHandler(this.button7_Click);
            // 
            // button_refl_z
            // 
            this.button_refl_z.Location = new System.Drawing.Point(171, 452);
            this.button_refl_z.Margin = new System.Windows.Forms.Padding(4);
            this.button_refl_z.Name = "button_refl_z";
            this.button_refl_z.Size = new System.Drawing.Size(69, 28);
            this.button_refl_z.TabIndex = 54;
            this.button_refl_z.Text = "Z";
            this.button_refl_z.UseVisualStyleBackColor = true;
            this.button_refl_z.Click += new System.EventHandler(this.button8_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 432);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 55;
            this.label4.Text = "Отразить по:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(69, 663);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 46);
            this.button1.TabIndex = 56;
            this.button1.Text = "Загрузить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(171, 663);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 46);
            this.button2.TabIndex = 57;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(236, 574);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 59);
            this.button3.TabIndex = 58;
            this.button3.Text = "Фигура вращения";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(384, 678);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(197, 28);
            this.button4.TabIndex = 59;
            this.button4.Text = "График двух переменных";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Location = new System.Drawing.Point(1191, 55);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(799, 604);
            this.pictureBox2.TabIndex = 60;
            this.pictureBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1220, 690);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 61;
            this.label5.Text = "Camera";
            // 
            // camera_x
            // 
            this.camera_x.AutoSize = true;
            this.camera_x.Location = new System.Drawing.Point(1223, 711);
            this.camera_x.Name = "camera_x";
            this.camera_x.Size = new System.Drawing.Size(16, 17);
            this.camera_x.TabIndex = 62;
            this.camera_x.Text = "0";
            // 
            // camera_y
            // 
            this.camera_y.AutoSize = true;
            this.camera_y.Location = new System.Drawing.Point(1261, 711);
            this.camera_y.Name = "camera_y";
            this.camera_y.Size = new System.Drawing.Size(16, 17);
            this.camera_y.TabIndex = 63;
            this.camera_y.Text = "0";
            // 
            // camera_z
            // 
            this.camera_z.AutoSize = true;
            this.camera_z.Location = new System.Drawing.Point(1299, 711);
            this.camera_z.Name = "camera_z";
            this.camera_z.Size = new System.Drawing.Size(32, 17);
            this.camera_z.TabIndex = 64;
            this.camera_z.Text = "500";
            // 
            // trans_z_camera
            // 
            this.trans_z_camera.Enabled = false;
            this.trans_z_camera.Location = new System.Drawing.Point(1456, 743);
            this.trans_z_camera.Margin = new System.Windows.Forms.Padding(4);
            this.trans_z_camera.Name = "trans_z_camera";
            this.trans_z_camera.Size = new System.Drawing.Size(40, 22);
            this.trans_z_camera.TabIndex = 68;
            this.trans_z_camera.Text = "0";
            // 
            // trans_y_camera
            // 
            this.trans_y_camera.Enabled = false;
            this.trans_y_camera.Location = new System.Drawing.Point(1407, 743);
            this.trans_y_camera.Margin = new System.Windows.Forms.Padding(4);
            this.trans_y_camera.Name = "trans_y_camera";
            this.trans_y_camera.Size = new System.Drawing.Size(40, 22);
            this.trans_y_camera.TabIndex = 67;
            this.trans_y_camera.Text = "0";
            // 
            // trans_x_camera
            // 
            this.trans_x_camera.Enabled = false;
            this.trans_x_camera.Location = new System.Drawing.Point(1357, 743);
            this.trans_x_camera.Margin = new System.Windows.Forms.Padding(4);
            this.trans_x_camera.Name = "trans_x_camera";
            this.trans_x_camera.Size = new System.Drawing.Size(40, 22);
            this.trans_x_camera.TabIndex = 66;
            this.trans_x_camera.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1220, 748);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 17);
            this.label6.TabIndex = 65;
            this.label6.Text = "Смещение (x, y, z)";
            // 
            // camera_axis_picker
            // 
            this.camera_axis_picker.FormattingEnabled = true;
            this.camera_axis_picker.Items.AddRange(new object[] {
            "Прямая, параллельная оси X",
            "Прямая, параллельная оси Y",
            "Прямая, параллельная оси Z"});
            this.camera_axis_picker.Location = new System.Drawing.Point(1220, 832);
            this.camera_axis_picker.Margin = new System.Windows.Forms.Padding(4);
            this.camera_axis_picker.Name = "camera_axis_picker";
            this.camera_axis_picker.Size = new System.Drawing.Size(276, 24);
            this.camera_axis_picker.TabIndex = 70;
            this.camera_axis_picker.SelectedIndexChanged += new System.EventHandler(this.camera_axis_picker_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1224, 812);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(213, 17);
            this.label7.TabIndex = 69;
            this.label7.Text = "Выбрать прямую для поворота";
            // 
            // rot_angle_camera
            // 
            this.rot_angle_camera.Location = new System.Drawing.Point(1357, 774);
            this.rot_angle_camera.Margin = new System.Windows.Forms.Padding(4);
            this.rot_angle_camera.Name = "rot_angle_camera";
            this.rot_angle_camera.Size = new System.Drawing.Size(89, 22);
            this.rot_angle_camera.TabIndex = 72;
            this.rot_angle_camera.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1227, 777);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 17);
            this.label8.TabIndex = 71;
            this.label8.Text = "Поворот (angle)";
            // 
            // button_exec_camera
            // 
            this.button_exec_camera.Location = new System.Drawing.Point(1220, 875);
            this.button_exec_camera.Margin = new System.Windows.Forms.Padding(4);
            this.button_exec_camera.Name = "button_exec_camera";
            this.button_exec_camera.Size = new System.Drawing.Size(100, 28);
            this.button_exec_camera.TabIndex = 73;
            this.button_exec_camera.Text = "Выполнить";
            this.button_exec_camera.UseVisualStyleBackColor = true;
            this.button_exec_camera.Click += new System.EventHandler(this.button_exec_camera_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(1554, 690);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(102, 21);
            this.radioButton1.TabIndex = 74;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Отсечения";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(1554, 717);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 21);
            this.radioButton2.TabIndex = 75;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Z-буфер";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.Location = new System.Drawing.Point(1191, 55);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(799, 604);
            this.pictureBox3.TabIndex = 76;
            this.pictureBox3.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1676, 902);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button_exec_camera);
            this.Controls.Add(this.rot_angle_camera);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.camera_axis_picker);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.trans_z_camera);
            this.Controls.Add(this.trans_y_camera);
            this.Controls.Add(this.trans_x_camera);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.camera_z);
            this.Controls.Add(this.camera_y);
            this.Controls.Add(this.camera_x);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_refl_z);
            this.Controls.Add(this.button_refl_y);
            this.Controls.Add(this.button_refl_x);
            this.Controls.Add(this.button_dodecaeder);
            this.Controls.Add(this.button_ikosaeder);
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
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button_cube);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_cube;
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
        private System.Windows.Forms.TextBox rot_line_x1;
        private System.Windows.Forms.TextBox rot_line_y1;
        private System.Windows.Forms.TextBox rot_line_z1;
        private System.Windows.Forms.TextBox rot_line_z2;
        private System.Windows.Forms.TextBox rot_line_y2;
        private System.Windows.Forms.TextBox rot_line_x2;
        private System.Windows.Forms.Button button_tetraeder;
        private System.Windows.Forms.Button button_octaeder;
        private System.Windows.Forms.Button button_ikosaeder;
        private System.Windows.Forms.Button button_dodecaeder;
        private System.Windows.Forms.Button button_refl_x;
        private System.Windows.Forms.Button button_refl_y;
        private System.Windows.Forms.Button button_refl_z;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label camera_x;
        private System.Windows.Forms.Label camera_y;
        private System.Windows.Forms.Label camera_z;
        private System.Windows.Forms.TextBox trans_z_camera;
        private System.Windows.Forms.TextBox trans_y_camera;
        private System.Windows.Forms.TextBox trans_x_camera;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox camera_axis_picker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox rot_angle_camera;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_exec_camera;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

