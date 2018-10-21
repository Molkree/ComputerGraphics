using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public enum axis { AXIS_X, AXIS_Y, AXIS_Z };

    interface Figure
    {
        /// <summary>
        /// Draw figure
        /// </summary>
        /// <param name="g">Where to draw</param>
        /// <param name="pen"></param>
        void show(Graphics g, Pen pen = null);
/*        Figure translate(float x, float y, float z);
        Figure rotate(double angle, axis a);
        Figure scale(float kx, float ky, float kz);
        */
    }


    public class Point3d: Figure
    {
        public float X, Y, Z;
        public Point3d(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3d(Point3d p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        // get point for central (perspective) projection
        public PointF make_perspective()
        {
            float z = Z;
            if (z == 0)
                z = 1; // ??? what if z = 0? We can't divide by zero

            return new PointF(X / z, Y / z);
        }

        // get point for parallel projection
        public PointF make_parallel()
        {
            return new PointF(X, Y);
        }

        // get point for orthographic projection
        // 0 - for x, 1 - for y, 2 - for z
  /*      public PointF make_orthographiс(axis a)
        {
            List<float> P = new List<float>(16);
            for (int i = 0; i < P.Count; ++i)
            {
                if (i % 5 == 0) // main diag
                    P[i] = 1;
                else
                    P[i] = 0;
            }

            // x
            if (axis == 0)
                P[0] = 0;
            // y
            else if (axis == 1)
                P[5] = 0;
            // z
            else
                P[10] = 0;


        }*/


        public void show(Graphics g, Pen pen = null)
        {
            if (pen == null)
                pen = Pens.Black;
            //  PointF p = make_perspective();
            PointF p = make_parallel();

            g.DrawRectangle(pen, p.X, p.Y, 2, 2);
        }

        // Affine transformation

        private List<float> mul_matrix(List<float> matr1, int m1, int n1, List<float> matr2, int m2, int n2)
        {
            if (n1 != m2)
                return new List<float>();
            int l = m1;
            int m = n1;
            int n = n2;

            List<float> c = new List<float>();
            for (int i = 0; i < l * n; ++i)
                c.Add(0f);

            for (int i = 0; i < l; ++i)
                for (int j = 0; j < n; ++j)
                {
                    //c[i * l + j] = 0;
                    for (int r = 0; r < m; ++r)
                        c[i * l + j] += matr1[i * m1 + r] * matr2[r * m2 + j];
                }
            return c;
        }

        public void translate(float x, float y, float z)
        {
            List<float> T = new List<float> { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, x, y, z, 1 };
            List<PointF> res = new List<PointF>();

            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, T, 4, 4);

            // or return new Point3d(c[0], c[1], c[2]); ???
            X = c[0];
            Y = c[1];
            Z = c[2];         
        }

        public void rotate(double angle, axis a)
        {
            double rangle = (Math.PI * angle) / 180; //угол в радианах

            List<float> R = null;
            
            switch (a)
            {
                case axis.AXIS_X:
                    R = new List<float> { 1, 0, 0, 0,
                                          0, (float)Math.Cos(rangle), (float)Math.Sin(rangle), 0,
                                          0, -1*(float)Math.Sin(rangle), (float)Math.Cos(rangle), 0,
                                          0, 0, 0, 1};
                    break;
                case axis.AXIS_Y:
                    R = new List<float> { (float)Math.Cos(rangle), 0, -1*(float)Math.Sin(rangle), 0,
                                          0, 1, 0, 0,
                                          (float)Math.Sin(rangle), 0, (float)Math.Cos(rangle), 0,
                                          0, 0, 0, 1};
                    break;
                case axis.AXIS_Z:
                    R = new List<float> { (float)Math.Cos(rangle), (float)Math.Sin(rangle), 0, 0,
                                          -1*(float)Math.Sin(rangle), (float)Math.Cos(rangle), 0, 0,
                                          0, 0, 1, 0,
                                          0, 0, 0, 1};
                    break;
            }
            
            
            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, R, 4, 4);
            // or return new Point3d(c[0], c[1], c[2]);
            X = c[0];
            Y = c[1];
            Z = c[2];

        }

        public void scale(float kx, float ky, float kz)
        {
            List<float> D = new List<float> { kx, 0, 0, 0,
                                               0, ky, 0, 0,
                                               0, 0, kz, 0,
                                               0, 0, 0, 1 };
            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, D, 4, 4);
            // or return new Point3d(c[0], c[1], c[2]);
            X = c[0];
            Y = c[1];
            Z = c[2];
        }


    }

    // прямая (ребро)
    public class Edge: Figure
    {
        public Point3d p1, p2;
        public Edge(Point3d pt1, Point3d pt2) 
        {
            p1 = new Point3d(pt1);
            p2 = new Point3d(pt2);
        }

        // get points for central (perspective) projection
        private List<PointF> make_perspective()
        {
            List<PointF> res = new List<PointF>();
            res.Add(p1.make_perspective());
            res.Add(p2.make_perspective());

            return res;

        }

        // get point for parallel projection
        public List<PointF> make_parallel()
        {
            List<PointF> res = new List<PointF>();
            res.Add(p1.make_parallel());
            res.Add(p2.make_parallel());

            return res;
        }

        private void show_perspective(Graphics g, Pen pen)
        {
            var pts = make_perspective();
            g.DrawLine(pen, pts[0], pts[1]);
        }

        public void show(Graphics g, Pen pen = null)
        {
            if (pen == null)
                pen = Pens.Black;
            show_perspective(g, pen);
        }

  /*      Edge translate(float x, float y, float z)
        {
            List<Point3d> l = new List<Point3d>();
            foreach (Point3d in )
        }
        Edge rotate(double angle, axis a);
        Edge scale(float kx, float ky, float kz);*/
    }

    // многоугольник (грань)
    public class Face: Figure
    {
        public List<Point3d> points;
        public Face(List<Point3d> pts)
        {
            points = new List<Point3d>(pts);
        }

        // get points for central (perspective) projection
        public List<PointF> make_perspective()
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3d p in points)
                res.Add(p.make_perspective());
           

            return res;
        }

        // get point for parallel projection
        public List<PointF> make_parallel()
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3d p in points)
                res.Add(p.make_parallel());

            return res;
        }

        public void show_perspective(Graphics g, Pen pen)
        {
            var pts = make_perspective();
            g.DrawLines(pen, pts.ToArray());
            g.DrawLine(pen, pts[0], pts[pts.Count - 1]);

        }

        public void show_parallel(Graphics g, Pen pen)
        {
            var pts = make_parallel();
            g.DrawLines(pen, pts.ToArray());
            g.DrawLine(pen, pts[0], pts[pts.Count - 1]);

        }

        public void show(Graphics g, Pen pen = null)
        {
            if (pen == null)
                pen = Pens.Black;
         //   show_parallel(g, pen);
            show_perspective(g, pen);


     //       g.DrawLines(pen, points.ToArray());
      //      g.DrawLine(pen, points[0], points[points.Count - 1]);

        }


        public void translate(float x, float y, float z)
        {
          //  List<Point3d> l = new List<Point3d>();
            foreach (Point3d p in points)
                p.translate(x, y, z);
          //  return new Face(l);
        }
        public void rotate(double angle, axis a)
        {
         //   List<Point3d> l = new List<Point3d>();
            foreach (Point3d p in points)
                p.rotate(angle, a);
         //   return new Face(l);
        }
        public void scale(float kx, float ky, float kz)
        {
         //   List<Point3d> l = new List<Point3d>();
            foreach (Point3d p in points)
                p.scale(kx, ky, kz);
        //    return new Face(l);
        }
    }

    // многогранник
    public class Polyhedron: Figure
    {
        public List<Face> faces; // ???
        // List<PointF> vertex; // ??? 
        public Polyhedron(List<Face> fs)
        {
            faces = new List<Face>(fs);
        }

        // get points for central (perspective) projection
/*       private List<List<PointF>> make_perspective()
        {
            List<List<PointF>> res = new List<List<PointF>>();

            foreach (Face f in faces)
            {
                var l = f.make_perspective();
                res.Add(l);
            }

            return res;
        }
        */

        public void show(Graphics g, Pen pen = null)
        {
            foreach (Face f in faces)
                f.show(g, pen);

        }

        public void translate(float x, float y, float z)
        {
//            List<Face> l = new List<Face>();
            foreach (Face f in faces)
                f.translate(x, y, z);
//            return new Polyhedron(l);
        }
        public void rotate(double angle, axis a)
        {
//            List<Face> l = new List<Face>();
            foreach (Face f in faces)
                f.rotate(angle, a);
//           return new Polyhedron(l);
        }
        public void scale(float kx, float ky, float kz)
        {
//            List<Face> l = new List<Face>();
            foreach (Face f in faces)
                f.scale(kx, ky, kz);
//            return new Polyhedron(l);
        }

    }

}
