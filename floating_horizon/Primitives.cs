using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace floating_horizon
{
    public enum Axis { AXIS_X, AXIS_Y, AXIS_Z, OTHER };
    public enum Projection { X = 0, Y, Z };
    public delegate float Function(float x, float y);

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
            // for safety - in order not to get infinity
            if (Math.Abs(Z - k) < 1e-10)
                k += 1;

            List<float> P = new List<float> { 1, 0, 0, 0,
                                              0, 1, 0, 0,
                                              0, 0, 0, -1/k,
                                              0, 0, 0, 1 };

            List<float> xyz = new List<float> { X, Y, Z, 1 };
            List<float> c = mul_matrix(xyz, 1, 4, P, 4, 4);

            return new PointF(c[0] / c[3], c[1] / c[3]);
        }


        public void show(Graphics g, Projection pr = 0, Pen pen = null)
        {
            if (pen == null)
                pen = Pens.Black;

            PointF p = make_perspective();
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
            Center = new Point3d(face.Center);
            if (Normal != null)
                Normal = new List<float>(face.Normal);
            IsVisible = face.IsVisible;
        }

        public Face(List<Point3d> pts = null)
        {
            if (pts != null)
            {
                Points = new List<Point3d>(pts);
                find_center();
            }
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

        public void find_normal(Point3d p_center, /*Do we need it?*/Edge camera)
        {
            Point3d Q = Points[1], R = Points[2], S = Points[0];
            List<float> QR = new List<float> { R.X - Q.X, R.Y - Q.Y, R.Z - Q.Z };
            List<float> QS = new List<float> { S.X - Q.X, S.Y - Q.Y, S.Z - Q.Z };


            Normal = new List<float> { QR[1] * QS[2] - QR[2] * QS[1],
                                       -(QR[0] * QS[2] - QR[2] * QS[0]),
                                       QR[0] * QS[1] - QR[1] * QS[0] }; // cross product

            List<float> CQ = new List<float> { Q.X - p_center.X, Q.Y - p_center.Y, Q.Z - p_center.Z };
            if (Point3d.mul_matrix(Normal, 1, 3, CQ, 3, 1)[0] > 1E-6)
            {
                Normal[0] *= -1;
                Normal[1] *= -1;
                Normal[2] *= -1;
            }
            
            Point3d E = camera.P1; // point of view
            List<float> CE = new List<float> { E.X - Center.X, E.Y - Center.Y, E.Z - Center.Z };
            float dot_product = Point3d.mul_matrix(Normal, 1, 3, CE, 3, 1)[0];
            IsVisible = Math.Abs(dot_product) < 1E-6 || dot_product < 0;
        }

       

        /* ------ Projections ------ */

        // get points for central (perspective) projection
        public List<PointF> make_perspective(float k = 1000, float z_camera = 1000)
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3d p in Points)
            {
                // not to show object behind the camera  -  bad idea
                //    if (p.Z > k || p.Z > z_camera)
                //      continue;
                // 
                res.Add(p.make_perspective(k));
            }
            return res;
        }

    

        public void show(Graphics g, Projection pr = 0, Pen pen = null, Edge camera = null, float k = 1000)
        {
            if (pen == null)
                pen = Pens.Black;

            List<PointF> pts = make_perspective(k);
        
            if (pts.Count > 1)
            {
                g.DrawLines(pen, pts.ToArray());
                g.DrawLine(pen, pts[0], pts[pts.Count - 1]);
            }
            else if (pts.Count == 1)
                g.DrawRectangle(pen, pts[0].X, pts[0].Y, 1, 1);
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

        // костыли
        public bool is_graph = false;
        public Function graph_function = null;
        public int graph_breaks = 0;
       

        public Polyhedron(List<Face> fs = null)
        {
            if (fs != null)
            {
                Faces = fs.Select(face => new Face(face)).ToList();
                find_center();
            }
        }

        public Polyhedron(Polyhedron polyhedron)
        {
            Faces = polyhedron.Faces.Select(face => new Face(face)).ToList();
            Center = new Point3d(polyhedron.Center);
            Cube_size = polyhedron.Cube_size;
            is_graph = polyhedron.is_graph;
            graph_function = polyhedron.graph_function;
            graph_breaks = polyhedron.graph_breaks;
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

        /* Floating horizon stuff */

        // ПОПЫТКА НОМЕР N

        public float[,] Mul_matrix1(float[,] m1, float[,] m2)
        {
            float[,] res = new float[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }


        private void DrawLine(Point p1, Point p2, ref Bitmap bmp, ref Dictionary<double, double> Ymax, ref Dictionary<double, double> Ymin, Pen up_pen = null, Pen down_pen = null)
        {
            Color up_color, down_color;
            if (up_pen == null)
                up_color = Color.Black;
            else up_color = up_pen.Color;
            if (down_pen == null)
                down_color = Color.LightGray;
            else down_color = down_pen.Color;



            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);

            int sx = p2.X >= p1.X ? 1 : -1;
            int sy = p2.Y >= p1.Y ? 1 : -1;

            int first_x = p2.X > p1.X ? p1.X : p2.X;
            int last_x = p2.X > p1.X ? p2.X : p1.X;

            for (int i = first_x; i <= 2 * last_x + dx; ++i)
                if (!Ymax.Keys.Contains(i))
                { Ymax[i] = Ymin[i] = Int32.MaxValue; }

            if (dy <= dx)
            {
                int d = -dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                for (int x = p1.X, y = p1.Y, i = 0; i <= dx; i++, x += sx)
                {
                    if (!Ymin.Keys.Contains(x))
                    {
                        Ymin.Add(x, Int32.MaxValue);
                        Ymax.Add(x, Int32.MaxValue);
                    }

                    if (Ymin[x] == Int32.MaxValue) // YMin, YMax not inited
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, up_color);
                        Ymin[x] = Ymax[x] = y;
                    }
                    else
                    if (y < Ymin[x])
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, up_color);
                        Ymin[x] = y;
                    }
                    else
                    if (y > Ymax[x])
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, down_color);
                        Ymax[x] = y;
                    }

                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                }
            }
            else
            {
                int d = -dy; // or just dy?
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;

                if (!Ymin.Keys.Contains(p1.X))
                {
                    Ymin.Add(p1.X, Int32.MaxValue);
                    Ymax.Add(p1.X, Int32.MaxValue);
                }

                double m1 = Ymin[p1.X];
                double m2 = Ymax[p1.X];

                for (int x = p1.X, y = p1.Y, i = 0; i <= dy; i++, y += sy)
                {
                    if (!Ymin.Keys.Contains(x))
                    {
                        Ymax.Add(x, Int32.MaxValue);
                        Ymin.Add(x, Int32.MaxValue);
                    }

                    if (Ymin[x] == Int32.MaxValue) // YMin, YMax not inited
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, up_color);
                        Ymin[x] = Ymax[x] = y;
                    }
                    else
                    if (y < m1)
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, up_color);
                        if (y < Ymin[x])
                            Ymin[x] = y;

                    }
                    else
                    if (y > m2)
                    {
                        if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                            bmp.SetPixel(x, y, down_color);
                        if (y > Ymax[x])
                            Ymax[x] = y;
                    }

                    if (d > 0)
                    {
                        d += d2;
                        x += sx;

                        if (!Ymin.Keys.Contains(x))
                        {
                            Ymax.Add(x, Int32.MaxValue);
                            Ymin.Add(x, Int32.MaxValue);
                        }

                        m1 = Ymin[x];
                        m2 = Ymax[x];
                    }
                    else
                        d += d1;
                }
            }

        }

        public void PlotSurface(ref Bitmap bmp, Camera camera)
        {
            HashSet<Point3d> pts = new HashSet<Point3d>(new Point3dEqComparer());

            double x1, x2, y1, y2, fmin, fmax;
            int n1, n2;
            n1 = n2 = graph_breaks;

            x1 = y1 = fmin = Int32.MaxValue;
            x2 = y2 = fmax = Int32.MinValue;

            foreach (var f in Faces)
                foreach (var p in f.Points)
                {
                    if (p.X < x1)
                        x1 = p.X;
                    if (p.X > x2)
                        x2 = p.X;
                    if (p.Y < y1)
                        y1 = p.Y;
                    if (p.Y > y2)
                        y2 = p.Y;
                    if (p.Z < fmin)
                        fmin = p.Z;
                    if (p.Z > fmax)
                        fmax = p.Z;

                }



            Dictionary<double, double> Ymax = new Dictionary<double, double>();
            Dictionary<double, double> Ymin = new Dictionary<double, double>();

            Pen up_pen = null;
            Pen down_pen = null;

            double phiz = camera.position.X * 3.1415926 / 180;
            double psiz = camera.position.Y * 3.1415926 / 180;
            

            double phi = 30 * Math.PI / 180 + phiz;
            double psi = 10 * Math.PI / 180 + psiz;

            double sphi = Math.Sin(phi);
            double cphi = Math.Cos(phi);
            double spsi = Math.Sin(psi);
            double cpsi = Math.Cos(psi);

            double[] e1 = { cphi, sphi, 0 };
            double[] e2 = { spsi * sphi, spsi * cphi, cpsi };

            double x, y, z;
            double hx = (x2 - x1) / n1;
            double hy = (y2 - y1) / n2;

            double xmin = (e1[0] >= 0 ? x1 : x2) * e1[0] + (e1[1] >= 0 ? y1 : y2) * e1[1];
            double xmax = (e1[0] >= 0 ? x2 : x1) * e1[0] + (e1[1] >= 0 ? y2 : y1) * e1[1];
            double ymin = (e2[0] >= 0 ? x1 : x2) * e2[0] + (e2[1] >= 0 ? y1 : y2) * e2[1];
            double ymax = (e2[0] >= 0 ? x2 : x1) * e2[0] + (e2[1] >= 0 ? y2 : y1) * e2[1];

            if (e2[2] >= 0)
            {
                ymin += fmin * e2[2];
                ymax += fmax * e2[2];
            }
            else
            {
                ymin += fmax * e2[2];
                ymax += fmin * e2[2];
            }

            double ax = 10 - bmp.Width * xmin / (xmax - xmin);
            double bx = bmp.Width / (xmax - xmin);
            double ay = 10 - bmp.Height * ymin / (ymax - ymin);
            double by = bmp.Height / (ymax - ymin);

            for (int i = 0; i < Math.Abs(x2 - x1); i++)
                Ymin[i] = Ymax[i] = Int32.MaxValue;

            for (int i = 0; i < Ymax.Count; ++i)
                Ymin[i] = Ymax[i] = Int32.MaxValue;

            Point[] CurLine = new Point[n1];
            Point[] NextLine = new Point[n1];

            for (int i = 0; i < n1; ++i)
            {
                x = x1 + i * hx;
                y = y1 + (n2 - 1) * hy;
                z = graph_function((float)x, (float)y);

                var t0 = Mul_matrix1(camera.translateAtPosition(), new float[4, 1] { { (float)x }, { (float)y }, { (float)z }, { 1 } });
                var t1 = Mul_matrix1(camera.translateAtAngles(), new float[3, 1] { { t0[0, 0] }, { t0[1, 0] }, { t0[2, 0] } });

                Point3d p = new Point3d(t1[0, 0], t1[1, 0], t1[2, 0]);

                CurLine[i].X = (int)(ax + bx * (p.X * e1[0] + p.Y * e1[1]));
                CurLine[i].Y = (int)(ay + by * (p.X * e2[0] + p.Y * e2[1] + p.Z * e2[2]));
                //CurLine[i].X = (int)(ax + bx * (x * e1[0] + y * e1[1]));
                //CurLine[i].Y = (int)(ay + by * (x * e2[0] + y * e2[1] + z * e2[2]));
            }

            for (int i = n2 - 1; i > -1; --i)
            {
                for (int j = 0; j < n1 - 1; ++j)
                    DrawLine(CurLine[j], CurLine[j + 1], ref bmp, ref Ymax, ref Ymin, up_pen, down_pen);

                if (i > 0)
                    for (int j = 0; j < n1; ++j)
                    {
                        x = x1 + j * hx;
                        y = y1 * (i - 1) + hy;
                        z = graph_function((float)x, (float)y);

                        var t0 = Mul_matrix1(camera.translateAtPosition(), new float[4, 1] { { (float)x }, { (float)y }, { (float)z }, { 1 } });
                        var t1 = Mul_matrix1(camera.translateAtAngles(), new float[3, 1] { { t0[0, 0] }, { t0[1, 0] }, { t0[2, 0] } });

                        Point3d p = new Point3d(t1[0, 0], t1[1, 0], t1[2, 0]);

                        NextLine[j].X = (int)(ax + bx * (p.X * e1[0] + p.Y * e1[1]));
                        NextLine[j].Y = (int)(ay + by * (p.X * e2[0] + p.Y * e2[1] + p.Z * e2[2]));
                           
                
                //        NextLine[j].X = (int)(ax + bx * (x * e1[0] + y * e1[1]));
                //        NextLine[j].Y = (int)(ay + by * (x * e2[0] + y * e2[1] + z * e2[2]));
                        DrawLine(CurLine[j], NextLine[j], ref bmp, ref Ymax, ref Ymin, up_pen, down_pen);

                    }
            }

        }

        /* End of floating horizon stuff */
    }

    public class Camera
    {
        public Point3d position;
        public double angle_1 = 0;
        public double angle_2 = 0;
        public double angle_3 = 0;

        public Camera()
        {
            position = new Point3d(0, 0, 10);
        }

        public void Reset()
        {
            position.X = 0;
            position.Y = 0;
            position.Z = 10;
            angle_1 = 0;
            angle_2 = 0;
            angle_3 = 0;
    }

        public void translate(float dx, float dy, float dz)
        {
            position.translate(dx, dy, dz);
        }

        public void set_angles(float a1, float a2, float a3)
        {
            angle_1 += a1;// * Math.PI / 180;
            angle_2 += a2; // * Math.PI / 180;
            angle_3 += a3; // * Math.PI / 180;
        }

        public float[,] translateAtPosition()
        {
            return new float[4, 4]{ {1,0,0,-(position.X)}, {0,1,0,-(position.Y)}, {0,0,1,-(position.Z)}, {0,0,0,1}};
        }

        public float[,] translateAtAngles()
        {
            return new float[3, 3] {
                { (float)(Math.Cos(angle_2) * Math.Cos(angle_3)),
                  (float)(-Math.Cos(angle_2) * Math.Sin(angle_3)),
                  (float)(Math.Sin(angle_2)) },

                {(float)(Math.Sin(angle_1)*Math.Sin(angle_2)*Math.Cos(angle_3) + Math.Sin(angle_3)*Math.Cos(angle_1)),
                 (float)(-Math.Sin(angle_1)*Math.Sin(angle_2)*Math.Sin(angle_3) + Math.Cos(angle_3)*Math.Cos(angle_1)),
                 (float)(-Math.Sin(angle_1)*Math.Cos(angle_2))},

                {(float)(-Math.Cos(angle_1)*Math.Sin(angle_2)*Math.Cos(angle_3) + Math.Sin(angle_1)*Math.Sin(angle_3)),
                 (float)(Math.Cos(angle_1)*Math.Sin(angle_2)*Math.Sin(angle_3) + Math.Sin(angle_1)*Math.Cos(angle_3)),
                 (float)(Math.Cos(angle_2)*Math.Cos(angle_1))} };
        }
    }

    public sealed class Point3dEqComparer : IEqualityComparer<Point3d>
    {
        public bool Equals(Point3d x, Point3d y)
        {
            return x.X.Equals(y.X) && x.Y.Equals(y.Y) && x.Z.Equals(y.Z);
        }

        public int GetHashCode(Point3d obj)
        {
            return obj.X.GetHashCode() + obj.Y.GetHashCode() + obj.Z.GetHashCode();
        }
    }

}