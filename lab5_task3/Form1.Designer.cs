namespace lab5_task3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.point1 = new System.Windows.Forms.Label();
            this.point3 = new System.Windows.Forms.Label();
            this.point2 = new System.Windows.Forms.Label();
            this.point0 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(732, 432);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // point1
            // 
            this.point1.BackColor = System.Drawing.Color.White;
            this.point1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.point1.ForeColor = System.Drawing.Color.Black;
            this.point1.Image = ((System.Drawing.Image)(resources.GetObject("point1.Image")));
            this.point1.Location = new System.Drawing.Point(385, 132);
            this.point1.Name = "point1";
            this.point1.Size = new System.Drawing.Size(7, 7);
            this.point1.TabIndex = 7;
            this.point1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.point_MouseDown);
            this.point1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.point_MouseMove);
            // 
            // point3
            // 
            this.point3.BackColor = System.Drawing.Color.White;
            this.point3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.point3.ForeColor = System.Drawing.Color.Black;
            this.point3.Image = ((System.Drawing.Image)(resources.GetObject("point3.Image")));
            this.point3.Location = new System.Drawing.Point(532, 242);
            this.point3.Name = "point3";
            this.point3.Size = new System.Drawing.Size(7, 7);
            this.point3.TabIndex = 8;
            this.point3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.point_MouseDown);
            this.point3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.point_MouseMove);
            // 
            // point2
            // 
            this.point2.BackColor = System.Drawing.Color.White;
            this.point2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.point2.ForeColor = System.Drawing.Color.Black;
            this.point2.Image = ((System.Drawing.Image)(resources.GetObject("point2.Image")));
            this.point2.Location = new System.Drawing.Point(473, 116);
            this.point2.Name = "point2";
            this.point2.Size = new System.Drawing.Size(7, 7);
            this.point2.TabIndex = 9;
            this.point2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.point_MouseDown);
            this.point2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.point_MouseMove);
            // 
            // point0
            // 
            this.point0.BackColor = System.Drawing.Color.White;
            this.point0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.point0.ForeColor = System.Drawing.Color.Black;
            this.point0.Image = ((System.Drawing.Image)(resources.GetObject("point0.Image")));
            this.point0.Location = new System.Drawing.Point(345, 242);
            this.point0.Name = "point0";
            this.point0.Size = new System.Drawing.Size(7, 7);
            this.point0.TabIndex = 10;
            this.point0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.point_MouseDown);
            this.point0.MouseMove += new System.Windows.Forms.MouseEventHandler(this.point_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 456);
            this.Controls.Add(this.point0);
            this.Controls.Add(this.point2);
            this.Controls.Add(this.point3);
            this.Controls.Add(this.point1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label point1;
        private System.Windows.Forms.Label point3;
        private System.Windows.Forms.Label point2;
        private System.Windows.Forms.Label point0;
    }
}

