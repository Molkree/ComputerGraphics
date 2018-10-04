using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    /*
     p_type - тип полигона (по количеству точек) 1 - точка, 2 - линия, 3++ многоугольник
         */
    public struct Primitive
    {
        public List<PointF> points;
        public int p_type;
        public PointF left_bot;
        public Primitive(List<PointF> pts)
        {
            float eps = 1E-9f;
            left_bot = new PointF(float.MaxValue, float.MaxValue);
            this.points = new List<PointF>(pts);
            p_type = points.Count;
            foreach(var pt in points)
            {
                if (pt.X < left_bot.X && Math.Abs(pt.X - left_bot.X)>eps)
                    left_bot = pt;
                else if (Math.Abs(pt.X - left_bot.X) < eps && pt.Y < left_bot.Y && Math.Abs(pt.Y - left_bot.Y) > eps)
                    left_bot = pt;
            }
        }
        
        public void translate(float x, float y)
        {
            List<float> T = new List<float> { 1, 0, 0, 0, 1, 0, x, y, 1 };
            List<PointF> res = new List<PointF>();
            for (int i = 0; i < points.Count; ++i)
            {
                List<float> xy = new List<float> { points[i].X, points[i].Y, 1 };
                List<float> c = mul_matrix(xy, 1, 3, T, 3, 3);
                res.Add(new PointF(c[0], c[1]));
            }
            points = res;
        }

        public void rotate(double angle)
        {
            double rangle = (Math.PI * angle) / 180; //угол в радианах
            List<float> R = new List<float> { (float)Math.Cos(rangle), (float)Math.Sin(rangle), 0,
                                                -1* (float)Math.Sin(rangle), (float)Math.Cos(rangle), 0,
                                                0,                           0,                        1};

            List<PointF> res = new List<PointF>();
            for (int i = 0; i < points.Count; ++i)
            {
                List<float> xy = new List<float> { points[i].X, points[i].Y, 1 };
                List<float> c = mul_matrix(xy, 1, 3, R, 3, 3);
                res.Add(new PointF(c[0], c[1]));
            }
            points = res;
        }

        public void scaling(float kx, float ky)
        {
            //на точку плевать, она - материальная точка
            // TODO
            List<float> D = new List<float> { kx, 0, 0, 0, ky, 0, 0, 0, 1 };
            List<PointF> res = new List<PointF>();
            for (int i = 0; i < points.Count; ++i)
            {
                List<float> xy = new List<float> { points[i].X, points[i].Y, 1 };
                List<float> c = mul_matrix(xy, 1, 3, D, 3, 3);
                res.Add(new PointF(c[0], c[1]));
            }
            points = res;
        }

        private List<float> mul_matrix(List<float> matr1, int m1, int n1, List<float> matr2, int m2, int n2)
        {
            if (n1 != m2)
                return new List<float>();
            int l = m1;
            int m = n1;
            int n = n2;

            List<float> c = new List<float>(l * n);
            for (int i = 0; i<l; ++i)
                for (int j =0; j<n; ++j)
                {
                    c[i * l + j] = 0;
                    for (int r = 0; r < m; ++r)
                        c[i * l + j] += matr1[i * m1 + r] * matr2[r * m2 + j];
                }
            return c;
        }
    }


}
