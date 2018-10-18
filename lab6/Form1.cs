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
    public partial class Form1 : Form
    {
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int center_x = pictureBox1.ClientSize.Width / 2;
            int center_y = pictureBox1.ClientSize.Height / 2;

            int size = 10;
            List<Point3d> pts = new List<Point3d>();
            pts.Add(new Point3d(center_x, center_y, 1));
            pts.Add(new Point3d(center_x + size, center_y, 1));
            pts.Add(new Point3d(center_x + size, center_y + size, 1));
            pts.Add(new Point3d(center_x, center_y + size, 1));

            Face f = new Face(pts);

            Polyhedron cube = make_cube(f);
            cube.show(g);

            //            Polyhedron cube 
        }


        public Polyhedron make_cube(Face f)
        {
            List<Face> faces = new List<Face>();
            // front face
            faces.Add(f);

            List<Point3d> points = f.points;
            float cube_size = Math.Abs(points[0].X - points[1].X);

            // back face
            for (int i = 0; i < points.Count; ++i)
            {
                points[i].Z += cube_size;
            } 
            Face f1 = new Face(points);
            faces.Add(f1);

            // up face
            List<Point3d> l2 = new List<Point3d>();
            l2.Add(f1.points[0]);
            l2.Add(f1.points[1]);
            l2.Add(f.points[1]);
            l2.Add(f.points[0]);        
            Face f2 = new Face(l2);
            faces.Add(f2);

            // down face
            List<Point3d> l3 = new List<Point3d>();
            l3.Add(f1.points[3]);
            l3.Add(f1.points[2]);
            l3.Add(f.points[2]);
            l3.Add(f.points[3]);
            Face f3 = new Face(l3);
            faces.Add(f3);

            return new Polyhedron(faces);
        }
    }
}
