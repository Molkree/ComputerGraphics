using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace lab6
{
    public enum Axis { AXIS_X, AXIS_Y, AXIS_Z, OTHER };
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

        public string to_string()
        {   
            return X.ToString(CultureInfo.InvariantCulture) + " " +
                Y.ToString(CultureInfo.InvariantCulture) + " " +
                Z.ToString(CultureInfo.InvariantCulture);
        }

        public void reflectX()
        {
            X = -X;
        }
        public void reflectY()
        {
            Y = -Y;
        }
        public void reflectZ()
        {
            Z = -Z;
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
            double r_phi = Math.Asin(Math.Tan(Math.PI * 30 / 180));
            double r_psi = Math.PI * 45 / 180;
            float cos_phi = (float)Math.Cos(r_phi);
            float sin_phi = (float)Math.Sin(r_phi);
            float cos_psi = (float)Math.Cos(r_psi);
            float sin_psi = (float)Math.Sin(r_psi);

            List<float> M = new List<float> { cos_psi,  sin_phi * sin_psi,   0,  0,
                                                 0,          cos_phi,        0,  0,
                                              sin_psi,  -sin_phi * cos_psi,  0,  0,
                                                 0,              0,          0,  1 };

            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, M, 4, 4);

            return new PointF(c[0], c[1]);
        }

        // get point for orthographic projection
        public PointF make_orthographic(Axis a)
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
            if (a == Axis.AXIS_X)
                P[0] = 0;
            // y
            else if (a == Axis.AXIS_Y)
                P[5] = 0;
            // z
            else
                P[10] = 0;

            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, P, 4, 4);

            // x
            if (a == Axis.AXIS_X)
                return new PointF(c[1], c[2]); // (y, z)
            // y
            else if (a == Axis.AXIS_Y)
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
                    p = make_orthographic(Axis.AXIS_X);
                    break;
                case Projection.ORTHOGR_Y:
                    p = make_orthographic(Axis.AXIS_Y);
                    break;
                case Projection.ORTHOGR_Z:
                    p = make_orthographic(Axis.AXIS_Z);
                    break;
                default:
                    p = make_perspective();
                    break;
            }
            g.DrawRectangle(pen, p.X, p.Y, 2, 2);
        }

        /* ------ Affine transformations ------ */

        static public List<float> mul_matrix(List<float> matr1, int m1, int n1, List<float> matr2, int m2, int n2)
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
                        c[i * l + j] += matr1[i * m1 + r] * matr2[r * n2 + j];
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

        public void rotate(double angle, Axis a, Edge line = null)
        {
            double rangle = Math.PI * angle / 180; // угол в радианах

            List<float> R = null;

            float sin = (float)Math.Sin(rangle);
            float cos = (float)Math.Cos(rangle);
            switch (a)
            {
                case Axis.AXIS_X:
                    R = new List<float> { 1,   0,     0,   0,
                                          0,  cos,   sin,  0,
                                          0,  -sin,  cos,  0,
                                          0,   0,     0,   1 };
                    break;
                case Axis.AXIS_Y:
                    R = new List<float> { cos,  0,  -sin,  0,
                                           0,   1,   0,    0,
                                          sin,  0,  cos,   0,
                                           0,   0,   0,    1 };
                    break;
                case Axis.AXIS_Z:
                    R = new List<float> { cos,   sin,  0,  0,
                                          -sin,  cos,  0,  0,
                                           0,     0,   1,  0,
                                           0,     0,   0,  1 };
                    break;
                case Axis.OTHER:
                    float l = Math.Sign(line.P2.X - line.P1.X);
                    float m = Math.Sign(line.P2.Y - line.P1.Y);
                    float n = Math.Sign(line.P2.Z - line.P1.Z);
                    float length = (float)Math.Sqrt(l * l + m * m + n * n);
                    l /= length; m /= length; n /= length;

                    R = new List<float> {  l * l + cos * (1 - l * l),   l * (1 - cos) * m + n * sin,   l * (1 - cos) * n - m * sin,  0,
                                          l * (1 - cos) * m - n * sin,   m * m + cos * (1 - m * m),    m * (1 - cos) * n + l * sin,  0,
                                          l * (1 - cos) * n + m * sin,  m * (1 - cos) * n - l * sin,    n * n + cos * (1 - n * n),   0,
                                                       0,                            0,                             0,               1 };

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


    public class Edge
    {
        public Point3d P1 { get; set; }
        public Point3d P2 { get; set; }

        public Edge(Point3d pt1, Point3d pt2)
        {
            P1 = new Point3d(pt1);
            P2 = new Point3d(pt2);
        }

        public Edge(string s)
        {
            var arr = s.Split(' ');
            P1 = new Point3d(float.Parse(arr[0], CultureInfo.InvariantCulture),
                float.Parse(arr[1], CultureInfo.InvariantCulture),
                float.Parse(arr[2], CultureInfo.InvariantCulture));
            P2 = new Point3d(float.Parse(arr[3], CultureInfo.InvariantCulture),
                float.Parse(arr[4], CultureInfo.InvariantCulture),
                float.Parse(arr[5], CultureInfo.InvariantCulture));
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

        private List<PointF> make_orthographic(Axis a)
        {
            List<PointF> res = new List<PointF>
            {
                P1.make_orthographic(a),
                P2.make_orthographic(a)
            };
            return res;
        }

        private List<PointF> make_isometric()
        {
            List<PointF> res = new List<PointF>
            {
                P1.make_isometric(),
                P2.make_isometric()
            };
            return res;
        }


        private void show_perspective(Graphics g, Pen pen)
        {
            var pts = make_perspective();
            g.DrawLine(pen, pts[0], pts[1]);
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
                    pts = make_orthographic(Axis.AXIS_X);
                    break;
                case Projection.ORTHOGR_Y:
                    pts = make_orthographic(Axis.AXIS_Y);
                    break;
                case Projection.ORTHOGR_Z:
                    pts = make_orthographic(Axis.AXIS_Z);
                    break;
                default:
                    pts = make_perspective();
                    break;
            }

            g.DrawLine(pen, pts[0], pts[pts.Count - 1]);
        }

        public void translate(float x, float y, float z)
        {
            P1.translate(x, y, z);
            P2.translate(x, y, z);
        }

        public void rotate(double angle, Axis a, Edge line = null)
        {
            P1.rotate(angle, a, line);
            P2.rotate(angle, a, line);
        }

        //Edge scale(float kx, float ky, float kz);
    }

    // многоугольник (грань)
    public class Face
    {
        public List<Point3d> Points { get; }
        public Point3d Center { get; set; } = new Point3d(0, 0, 0);
        public List<float> Normal { get; set; }
        public bool IsVisible { get; set; }

        public Face(Face face)
        {
            Points = face.Points.Select(pt => new Point3d(pt.X, pt.Y, pt.Z)).ToList();
            find_center();
        }

        public Face(List<Point3d> pts = null)
        {
            if (pts != null)
            {
                Points = new List<Point3d>(pts);
                find_center();
            }
        }

        public Face(string s)
        {
            Points = new List<Point3d>();

            var arr = s.Split(' ');
            //int points_cnt = int.Parse(arr[0], CultureInfo.InvariantCulture);
            for (int i = 1; i < arr.Length; i += 3)
            {
                if (string.IsNullOrEmpty(arr[i]))
                    continue;
                float x = float.Parse(arr[i], CultureInfo.InvariantCulture);
                float y = float.Parse(arr[i + 1], CultureInfo.InvariantCulture);
                float z = float.Parse(arr[i + 2], CultureInfo.InvariantCulture);
                Point3d p = new Point3d(x, y, z);
                Points.Add(p);
            }
            find_center();
        }

        public string to_string()
        {
            string res = "";
            res += Points.Count.ToString(CultureInfo.InvariantCulture) + " ";
            foreach (var f in Points)
            {
                res += f.to_string() + " ";
            }

            return res;
        }

        private void find_center()
        {
            Center.X = 0;
            Center.Y = 0;
            Center.Z = 0;
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

        public void find_normal(Point3d p_center, Edge camera)
        {
            Point3d Q = Points[1], R = Points[2], S = Points[0];
            List<float> QR = new List<float> { R.X - Q.X, R.Y - Q.Y, R.Z - Q.Z };
            List<float> QS = new List<float> { S.X - Q.X, S.Y - Q.Y, S.Z - Q.Z };
            Normal = new List<float> { QR[1] * QS[2] - QR[2] * QS[1],
                                       -(QR[0] * QS[2] - QR[2] * QS[0]),
                                       QR[0] * QS[1] - QR[1] * QS[0] }; // cross product
            List<float> CQ = new List<float> { Q.X - p_center.X, Q.Y - p_center.Y, Q.Z - p_center.Z };
            if (Point3d.mul_matrix(Normal, 1, 3, CQ, 3, 1)[0] < 0)
            {
                Normal[0] *= -1;
                Normal[1] *= -1;
                Normal[2] *= -1;
            }

            // TODO allow point of view change
            Point3d E = new Point3d(0, 0, 1000); // point of view
            //List<float> EC = new List<float> { camera.P1.X - Center.X, camera.P1.Y - Center.Y, camera.P1.Z - Center.Z };
            List<float> EC = new List<float> { E.X - Center.X, E.Y - Center.Y, E.Z - Center.Z };
            //List<float> EC = new List<float> { camera.P1.X - camera.P2.X, camera.P1.Y - camera.P2.Y, camera.P1.Z - camera.P2.Z };
            float dot_product = Point3d.mul_matrix(Normal, 1, 3, EC, 3, 1)[0];
            IsVisible = 0 <= dot_product;
        }

        public void reflectX()
        {
            Center.X = -Center.X;
            if (Points != null)
                foreach (var p in Points)
                    p.reflectX();
        }
        public void reflectY()
        {
            Center.Y = -Center.Y;
            if (Points != null)
                foreach (var p in Points)
                    p.reflectY();
        }
        public void reflectZ()
        {
            Center.Z = -Center.Z;
            if (Points != null)
                foreach (var p in Points)
                    p.reflectZ();
        }

        /* ------ Projections ------ */

        // get points for central (perspective) projection
        public List<PointF> make_perspective(float k = 1000)
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3d p in Points)
                res.Add(p.make_perspective(k));
          
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
        public List<PointF> make_orthographic(Axis a)
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
                    pts = make_orthographic(Axis.AXIS_X);
                    break;
                case Projection.ORTHOGR_Y:
                    pts = make_orthographic(Axis.AXIS_Y);
                    break;
                case Projection.ORTHOGR_Z:
                    pts = make_orthographic(Axis.AXIS_Z);
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
            find_center();
        }

        public void rotate(double angle, Axis a, Edge line = null)
        {
            foreach (Point3d p in Points)
                p.rotate(angle, a, line);
            find_center();
        }

        public void scale(float kx, float ky, float kz)
        {
            foreach (Point3d p in Points)
                p.scale(kx, ky, kz);
            find_center();
        }
    }

    // многогранник
    public class Polyhedron
    {
        public const int MODE_POL = 0;
        public const int MODE_ROT = 1;
        public List<Face> Faces { get; set; } = null;
        public Point3d Center { get; set; } = new Point3d(0, 0, 0);
        public float Cube_size { get; set; }

        public Polyhedron(List<Face> fs = null)
        {
            if (fs != null)
            {
                Faces = fs.Select(face => new Face(face)).ToList();
                //Faces = new List<Face>(fs);
                find_center();
            }
        }

        public Polyhedron(string s, int mode = MODE_POL)
        {
            Faces = new List<Face>();
            switch (mode)
            {
                case MODE_POL:
                    var arr1 = s.Split('\n');
                    //int faces_cnt = int.Parse(arr1[0], CultureInfo.InvariantCulture);
                    for (int i = 1; i < arr1.Length; ++i)
                    {
                        if (string.IsNullOrEmpty(arr1[i]))
                            continue;
                        Face f = new Face(arr1[i]);
                        Faces.Add(f);
                    }
                    find_center();
                    break;
                case MODE_ROT:
                    var arr2 = s.Split('\n');
                    int cnt_breaks = int.Parse(arr2[0], CultureInfo.InvariantCulture);
                    Edge rot_line = new Edge(arr2[1]);
                    int cnt_points = int.Parse(arr2[2], CultureInfo.InvariantCulture);
                    var arr3 = arr2[3].Split(' ');
                    List<Point3d> pts = new List<Point3d>();
                    for (int i = 0; i < 3 * cnt_points; i += 3)
                        pts.Add(new Point3d(
                            float.Parse(arr3[i], CultureInfo.InvariantCulture),
                            float.Parse(arr3[i + 1], CultureInfo.InvariantCulture),
                            float.Parse(arr3[i + 2], CultureInfo.InvariantCulture)));
                    make_rotation_figure(cnt_breaks, rot_line, pts);
                    break;
                default: break;
            }
            
        }

        public string to_string()
        {
            string res = "";
            res += Faces.Count.ToString(CultureInfo.InvariantCulture) + "\n";
            foreach (var f in Faces)
            {
                res += f.to_string() + "\n";
            }

            return res;
        }

        private void find_center()
        {
            Center.X = 0;
            Center.Y = 0;
            Center.Z = 0;
            foreach (Face f in Faces)
            {
                Center.X += f.Center.X;
                Center.Y += f.Center.Y;
                Center.Z += f.Center.Z;
            }
            Center.X /= Faces.Count;
            Center.Y /= Faces.Count;
            Center.Z /= Faces.Count;
            //foreach (Face f in Faces)
            //{
            //    f.find_normal(Center);
            //}
        }

        public void show(Graphics g, Projection pr = 0, Pen pen = null)
        {
            foreach (Face f in Faces)
                //if (f.IsVisible)
                    f.show(g, pr, pen);
        }

        public void show_camera(Graphics g, Edge camera, Pen pen = null)
        {
            foreach (Face f in Faces)
            {
                f.find_normal(Center, camera);
                if (f.IsVisible)
                {
                    //float k = (float)Math.Sqrt(
                    //    (camera.P1.X - Center.X) * (camera.P1.X - Center.X) + 
                    //    (camera.P1.Y - Center.Y) * (camera.P1.Y - Center.Y) +
                    //    (camera.P1.Z - Center.Z) * (camera.P1.Z - Center.Z));
                    //List<PointF> pts = f.make_perspective(k/*1000*/);
                    //g.DrawLines(pen, pts.ToArray());
                    //g.DrawLine(pen, pts[0], pts[pts.Count - 1]);
                    f.show(g, Projection.PERSPECTIVE, pen);
                }
            }
        }

        /* ------ Affine transformation ------ */

        public void translate(float x, float y, float z)
        {
            foreach (Face f in Faces)
                f.translate(x, y, z);
            find_center();
        }

        public void rotate(double angle, Axis a, Edge line = null)
        {
            foreach (Face f in Faces)
                f.rotate(angle, a, line);
            find_center();
        }

        public void scale(float kx, float ky, float kz)
        {
            foreach (Face f in Faces)
                f.scale(kx, ky, kz);
            find_center();
        }
        
        public void reflectX()
        {
            if (Faces != null )
                foreach (var f in Faces)
                    f.reflectX();
            find_center();
        }

        public void reflectY()
        {
            if (Faces != null)
                foreach (var f in Faces)
                    f.reflectY();
            find_center();
        }

        public void reflectZ()
        {
            if (Faces != null)
                foreach (var f in Faces)
                    f.reflectZ();
            find_center();
        }

        /* ------ Figures ------- */

        public void make_hexahedron(Face f = null)
        {
            if (f == null)
            {
                f = new Face(
                    new List<Point3d>
                    {
                        new Point3d(-50, -50, 0),
                        new Point3d(50, -50, 0),
                        new Point3d(50, 50, 0),
                        new Point3d(-50, 50, 0)
                    }
               );
            }

            Faces = new List<Face>
            {
                // back face
                f
            };

            float cube_size = Math.Abs(f.Points[0].X - f.Points[1].X);
            List<Point3d> l1 = new List<Point3d>();
            // front face
            foreach (var point in f.Points)
            {
                l1.Add(new Point3d(point.X, point.Y, point.Z + cube_size));
            }
            Face f1 = new Face(l1);
            Faces.Add(f1);

            // down face
            List<Point3d> l2 = new List<Point3d>
            {
                new Point3d(f.Points[0]),   // left back
                new Point3d(f.Points[1]),   // right back
                new Point3d(f1.Points[1]),  // right front
                new Point3d(f1.Points[0]),  // left front
            };
            Face f2 = new Face(l2);
            Faces.Add(f2);

            // up face
            List<Point3d> l3 = new List<Point3d>
            {
                new Point3d(f.Points[3]),
                new Point3d(f.Points[2]),
                new Point3d(f1.Points[2]),
                new Point3d(f1.Points[3]),  
            };
            Face f3 = new Face(l3);
            Faces.Add(f3);

            // left face
            List<Point3d> l4 = new List<Point3d>
            {
                new Point3d(f.Points[0]),
                new Point3d(f1.Points[0]),
                new Point3d(f1.Points[3]),
                new Point3d(f.Points[3])
            };
            Face f4 = new Face(l4);
            Faces.Add(f4);

            // right face
            List<Point3d> l5 = new List<Point3d>
            {
                new Point3d(f1.Points[1]),
                new Point3d(f.Points[1]),
                new Point3d(f.Points[2]),
                new Point3d(f1.Points[2])
            };
            Face f5 = new Face(l5);
            Faces.Add(f5);

            Cube_size = cube_size;
            find_center();
        }

        public void make_tetrahedron(Polyhedron cube = null)
        {
            if (cube == null)
            {
                cube = new Polyhedron();
                cube.make_hexahedron();
            }
            Face f0 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[0].Points[0]),
                    new Point3d(cube.Faces[1].Points[1]),
                    new Point3d(cube.Faces[1].Points[3])                
                }
            );

            Face f1 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[1].Points[3]),
                    new Point3d(cube.Faces[1].Points[1]),
                    new Point3d(cube.Faces[0].Points[2])
                }
            );

            Face f2 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[0].Points[2]),
                    new Point3d(cube.Faces[1].Points[1]),
                    new Point3d(cube.Faces[0].Points[0])
                }
            );

            Face f3 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[0].Points[2]),
                    new Point3d(cube.Faces[0].Points[0]),
                    new Point3d(cube.Faces[1].Points[3])
                }
            );

            Faces = new List<Face> { f0, f1, f2, f3 };
            find_center();
        }

        public void make_octahedron(Polyhedron cube = null)
        {
            if (cube == null)
            {
                cube = new Polyhedron();
                cube.make_hexahedron();
            }

            // up
            Face f0 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[2].Center),
                    new Point3d(cube.Faces[1].Center),
                    new Point3d(cube.Faces[4].Center)
                }
            );

            Face f1 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[2].Center),
                    new Point3d(cube.Faces[1].Center),
                    new Point3d(cube.Faces[5].Center)
                }
            );

            Face f2 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[2].Center),
                    new Point3d(cube.Faces[5].Center),
                    new Point3d(cube.Faces[0].Center)
                }
            );

            Face f3 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[2].Center),
                    new Point3d(cube.Faces[0].Center),
                    new Point3d(cube.Faces[4].Center)
                }
            );

            // down
            Face f4 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[3].Center),
                    new Point3d(cube.Faces[1].Center),
                    new Point3d(cube.Faces[4].Center)
                }
            );

            Face f5 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[3].Center),
                    new Point3d(cube.Faces[1].Center),
                    new Point3d(cube.Faces[5].Center)
                }
            );

            Face f6 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[3].Center),
                    new Point3d(cube.Faces[5].Center),
                    new Point3d(cube.Faces[0].Center)
                }
            );

            Face f7 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[3].Center),
                    new Point3d(cube.Faces[0].Center),
                    new Point3d(cube.Faces[4].Center)
                }
            );

            Faces = new List<Face> { f0, f1, f2, f3, f4, f5, f6, f7 };
            find_center();
        }

        public void make_icosahedron()
        {
            Faces = new List<Face>();

            float size = 100;

            float r1 = size * (float)Math.Sqrt(3) / 4;   // половина высоты правильного треугольника - для высоты цилиндра
            float r = size * (3 + (float)Math.Sqrt(5)) / (4 * (float)Math.Sqrt(3)); // радиус вписанной сферы - для правильных пятиугольников

            Point3d up_center = new Point3d(0, -r1, 0);  // центр верхней окружности
            Point3d down_center = new Point3d(0, r1, 0); // центр нижней окружности

            // up
            double a = Math.PI / 2;
            List<Point3d> up_points = new List<Point3d>();
            for (int i = 0; i < 5; ++i)
            {
                up_points.Add( new Point3d(up_center.X + r * (float)Math.Cos(a), up_center.Y, up_center.Z - r * (float)Math.Sin(a)));
                a += 2 * Math.PI / 5;
            }

            // down
            a = Math.PI / 2 - Math.PI / 5;
            List<Point3d> down_points = new List<Point3d>();
            for (int i = 0; i < 5; ++i)
            {
                down_points.Add(new Point3d(down_center.X + r * (float)Math.Cos(a), down_center.Y, down_center.Z - r * (float)Math.Sin(a)));
                a += 2 * Math.PI / 5;
            }

            var R = Math.Sqrt(2*(5 + Math.Sqrt(5))) * size / 4; // радиус описанной сферы - для пирамидок над цилиндром

            Point3d p_up = new Point3d(up_center.X, (float)(-R), up_center.Z);
            Point3d p_down = new Point3d(down_center.X, (float)R, down_center.Z);

            // upper faces
            for (int i = 0; i < 5; ++i)
            {
                Faces.Add(
                    new Face(new List<Point3d>
                    {
                        new Point3d(p_up),
                        new Point3d(up_points[i]),
                        new Point3d(up_points[(i+1) % 5]),
                    })
                    );
            }

            // lower faces
            for (int i = 0; i < 5; ++i)
            {
                Faces.Add(
                    new Face(new List<Point3d>
                    {
                        new Point3d(p_down),
                        new Point3d(down_points[i]),
                        new Point3d(down_points[(i+1) % 5]),
                    })
                    );
            }
            
            // vertical
            for (int i = 0; i < 5; ++i)
            {
                // triangle \/
                Faces.Add(
                    new Face(new List<Point3d>
                    {
                        new Point3d(up_points[i]),
                        new Point3d(up_points[(i+1) % 5]),
                        new Point3d(down_points[(i+1) % 5])
                    })
                    );

                // triangle /\
                Faces.Add(
                    new Face(new List<Point3d>
                    {
                        new Point3d(up_points[i]),
                        new Point3d(down_points[i]),
                        new Point3d(down_points[(i+1) % 5])
                    })
                    );
            }
            
            find_center();
        }

        public void make_dodecahedron()
        {
            Faces = new List<Face>();
            Polyhedron ik = new Polyhedron();
            ik.make_icosahedron();

            List<Point3d> pts = new List<Point3d>();
            foreach (Face f in ik.Faces)
            {
                pts.Add(f.Center);
            }

            // up
            Faces.Add(new Face(new List<Point3d>
            {
                new Point3d(pts[0]),
                new Point3d(pts[1]),
                new Point3d(pts[2]),
                new Point3d(pts[3]),
                new Point3d(pts[4])
            }));

            // down
            Faces.Add(new Face(new List<Point3d>
            {
                new Point3d(pts[5]),
                new Point3d(pts[6]),
                new Point3d(pts[7]),
                new Point3d(pts[8]),
                new Point3d(pts[9])
            }));

            // side / up
            for (int i = 0; i < 5; ++i)
            {
                Faces.Add(new Face(new List<Point3d>
                {
                    new Point3d(pts[i]),
                    new Point3d(pts[(i + 1) % 5]),
                    new Point3d(pts[(i == 4) ? 10 : 2*i + 12]),
                    new Point3d(pts[(i == 4) ? 11 : 2*i + 13]),
                    new Point3d(pts[2*i + 10])
                }));
            }

           Faces.Add(new Face(new List<Point3d>
            {
                new Point3d(pts[5]),
                new Point3d(pts[6]),
                new Point3d(pts[13]),
                new Point3d(pts[10]),
                new Point3d(pts[11])
            }));
             Faces.Add(new Face(new List<Point3d>
            {
                new Point3d(pts[6]),
                new Point3d(pts[7]),
                new Point3d(pts[15]),
                new Point3d(pts[12]),
                new Point3d(pts[13])
            }));
            Faces.Add(new Face(new List<Point3d>
            {
                new Point3d(pts[7]),
                new Point3d(pts[8]),
                new Point3d(pts[17]),
                new Point3d(pts[14]),
                new Point3d(pts[15])
            }));
            Faces.Add(new Face(new List<Point3d>
            {
                new Point3d(pts[8]),
                new Point3d(pts[9]),
                new Point3d(pts[19]),
                new Point3d(pts[16]),
                new Point3d(pts[17])
            }));
            Faces.Add(new Face(new List<Point3d>
            {
                new Point3d(pts[9]),
                new Point3d(pts[5]),
                new Point3d(pts[11]),
                new Point3d(pts[18]),
                new Point3d(pts[19])
            }));

            find_center();
        }

        private void make_rotation_figure(int cnt_breaks, Edge rot_line, List<Point3d> pts)
        {
            double angle = 360.0 / cnt_breaks;
            float Ax = rot_line.P1.X, Ay = rot_line.P1.Y, Az = rot_line.P1.Z;
            foreach(var p in pts)
                p.translate(-Ax, -Ay, -Az);

            List<Point3d> new_pts = new List<Point3d>();
            foreach (var p in pts)
                new_pts.Add(new Point3d(p.X, p.Y, p.Z));


            for (int i = 0; i < cnt_breaks; ++i)
            {
                foreach (var np in new_pts)
                    np.rotate(angle, Axis.OTHER, rot_line);
                for (int j = 1; j < pts.Count; ++j)
                {
                    Face f = new Face(new List<Point3d>(){ new Point3d(pts[j - 1]), new Point3d(new_pts[j - 1]),
                        new Point3d(new_pts[j]), new Point3d(pts[j])});
                    Faces.Add(f);
                }
                foreach (var p in pts)
                    p.rotate(angle, Axis.OTHER, rot_line);
            }


            foreach (var f in Faces)
                f.translate(Ax, Ay, Az);
            find_center();
        }
    }
}
