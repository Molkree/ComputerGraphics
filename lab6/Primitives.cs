using System;
using System.Collections.Generic;
using System.Drawing;

namespace lab6
{
    public enum axis { AXIS_X, AXIS_Y, AXIS_Z };
    public enum Projection { PERSPECTIVE = 0, ISOMETRIC, ORTHOGR_X, ORTHOGR_Y, ORTHOGR_Z };

    public class Point3d
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

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

        /* ------ Projections ------ */

        // get point for central (perspective) projection
        public PointF make_perspective(float k = 1000)
        {
            List<float> P = new List<float> { 1, 0, 0, 0,
                                              0, 1, 0, 0,
                                              0, 0, 0, -1/k,
                                              0, 0, 0, 1 };

            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, P, 4, 4);

            return new PointF(c[0] / c[3], c[1] / c[3]);
        }

        // get point for isometric projection
        public PointF make_isometric()
        {
            return new PointF(X, Y);
        }

        // get point for orthographic projection
        public PointF make_orthographic(axis a)
        {
            List<float> P = new List<float>();
            for (int i = 0; i < 16; ++i)
            {
                if (i % 5 == 0) // main diag
                    P.Add(1);
                else
                    P.Add(0);
            }

            // x
            if (a == axis.AXIS_X)
                P[0] = 0;
            // y
            else if (a == axis.AXIS_Y)
                P[5] = 0;
            // z
            else
                P[10] = 0;

            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, P, 4, 4);

            // x
            if (a == axis.AXIS_X)
                return new PointF(c[1], c[2]); // (y, z)
            // y
            else if (a == axis.AXIS_Y)
                return new PointF(c[0], c[2]); // (x, z)
            // z
            else
                return new PointF(c[0], c[1]); // (x, y)
        }

        public void show(Graphics g, Projection pr = 0, Pen pen = null)
        {
            if (pen == null)
                pen = Pens.Black;

            PointF p;
            switch (pr)
            {
                case Projection.ISOMETRIC:
                    p = make_isometric();
                    break;
                case Projection.ORTHOGR_X:
                    p = make_orthographic(axis.AXIS_X);
                    break;
                case Projection.ORTHOGR_Y:
                    p = make_orthographic(axis.AXIS_Y);
                    break;
                case Projection.ORTHOGR_Z:
                    p = make_orthographic(axis.AXIS_Z);
                    break;
                default:
                    p = make_perspective();
                    break;
            }
            g.DrawRectangle(pen, p.X, p.Y, 2, 2);
        }

        /* ------ Affine transformations ------ */

        static private List<float> mul_matrix(List<float> matr1, int m1, int n1, List<float> matr2, int m2, int n2)
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
                    for (int r = 0; r < m; ++r)
                        c[i * l + j] += matr1[i * m1 + r] * matr2[r * m2 + j];
                }
            return c;
        }

        public void translate(float x, float y, float z)
        {
            List<float> T = new List<float> { 1, 0, 0, 0,
                                              0, 1, 0, 0,
                                              0, 0, 1, 0,
                                              x, y, z, 1 };
            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, T, 4, 4);

            X = c[0];
            Y = c[1];
            Z = c[2];         
        }

        public void rotate(double angle, axis a)
        {
            double rangle = (Math.PI * angle) / 180; // угол в радианах

            List<float> R = null;

            float sin = (float)Math.Sin(rangle);
            float cos = (float)Math.Cos(rangle);
            switch (a)
            {
                case axis.AXIS_X:
                    R = new List<float> { 1,  0,    0,  0,
                                          0, cos,  sin, 0,
                                          0, -sin, cos, 0,
                                          0,  0,    0,  1 };
                    break;
                case axis.AXIS_Y:
                    R = new List<float> { cos, 0, -sin, 0,
                                           0,  1,  0,   0,
                                          sin, 0, cos,  0,
                                          0,   0,  0,   1 };
                    break;
                case axis.AXIS_Z:
                    R = new List<float> { cos,  sin, 0, 0,
                                          -sin, cos, 0, 0,
                                           0,    0,  1, 0,
                                           0,    0,  0, 1 };
                    break;
            }
            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, R, 4, 4);

            X = c[0];
            Y = c[1];
            Z = c[2];
        }

        public void scale(float kx, float ky, float kz)
        {
            List<float> D = new List<float> { kx, 0,  0,  0,
                                              0,  ky, 0,  0,
                                              0,  0,  kz, 0,
                                              0,  0,  0,  1 };
            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, D, 4, 4);

            X = c[0];
            Y = c[1];
            Z = c[2];
        }
    }

    // прямая (ребро) - он нужен вообще?
    public class Edge
    {
        public Point3d P1 { get; set; }
        public Point3d P2 { get; set; }

        public Edge(Point3d pt1, Point3d pt2) 
        {
            P1 = new Point3d(pt1);
            P2 = new Point3d(pt2);
        }

        // get points for central (perspective) projection
        private List<PointF> make_perspective()
        {
            List<PointF> res = new List<PointF>
            {
                P1.make_perspective(),
                P2.make_perspective()
            };

            return res;
        }

        // get point for parallel projection
        static public List<PointF> make_parallel()
        {
            List<PointF> res = new List<PointF>();
            //res.Add(p1.make_parallel());
            //res.Add(p2.make_parallel());

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
    public class Face
    {
        public List<Point3d> Points { get; }
        public Point3d Center { get; set; } = new Point3d(0, 0, 0);

        public Face(List<Point3d> pts)
        {
            Points = new List<Point3d>(pts);
            foreach (Point3d p in Points)
            {
                Center.X += p.X;
                Center.Y += p.Y;
                Center.Z += p.Z;
            }
            Center.X /= Points.Count;
            Center.Y /= Points.Count;
            Center.Z /= Points.Count;
        }

        /* ------ Projections ------ */

        // get points for central (perspective) projection
        public List<PointF> make_perspective()
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3d p in Points)
                res.Add(p.make_perspective());
          
            return res;
        }

        // get point for isometric projection
        public List<PointF> make_isometric()
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3d p in Points)
                res.Add(p.make_isometric());

            return res;
        }

        // get point for orthographic projection
        public List<PointF> make_orthographic(axis a)
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3d p in Points)
                res.Add(p.make_orthographic(a));

            return res;
        }

        public void show(Graphics g, Projection pr = 0, Pen pen = null)
        {
            if (pen == null)
                pen = Pens.Black;

            List<PointF> pts;

            switch (pr)
            {
                case Projection.ISOMETRIC:
                    pts = make_isometric();
                    break;
                case Projection.ORTHOGR_X:
                    pts = make_orthographic(axis.AXIS_X);
                    break;
                case Projection.ORTHOGR_Y:
                    pts = make_orthographic(axis.AXIS_Y);
                    break;
                case Projection.ORTHOGR_Z:
                    pts = make_orthographic(axis.AXIS_Z);
                    break;
                default:
                    pts = make_perspective();
                    break;
            }

            g.DrawLines(pen, pts.ToArray());
            g.DrawLine(pen, pts[0], pts[pts.Count - 1]);
        }

        /* ------ Affine transformations ------ */

        public void translate(float x, float y, float z)
        {
            foreach (Point3d p in Points)
                p.translate(x, y, z);
        }
        public void rotate(double angle, axis a)
        {
            foreach (Point3d p in Points)
                p.rotate(angle, a);
        }
        public void scale(float kx, float ky, float kz)
        {
            foreach (Point3d p in Points)
                p.scale(kx, ky, kz);
        }
    }

    // многогранник
    public class Polyhedron
    {
        public List<Face> Faces { get; }
        public Point3d Center { get; set; } = new Point3d(0, 0, 0);
        public float Cube_size { get; set; }

        public Polyhedron(List<Face> fs)
        {
            Faces = new List<Face>(fs);

            foreach (Face f in Faces)
            {
                Center.X = f.Center.X;
                Center.Y = f.Center.Y;
                Center.Z = f.Center.Z;
            }
            Center.X /= Faces.Count;
            Center.Y /= Faces.Count;
            Center.Z /= Faces.Count;
        }

        public void show(Graphics g, Projection pr = 0, Pen pen = null)
        {
            foreach (Face f in Faces)
                f.show(g, pr, pen);
        }

        /* ------ Affine transformation ------ */

        public void translate(float x, float y, float z)
        {
            foreach (Face f in Faces)
                f.translate(x, y, z);
        }
        public void rotate(double angle, axis a)
        {
            foreach (Face f in Faces)
                f.rotate(angle, a);
        }
        public void scale(float kx, float ky, float kz)
        {
            foreach (Face f in Faces)
                f.scale(kx, ky, kz);
        }
    }
}
