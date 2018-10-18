using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{

    interface Figure
    {
        /// <summary>
        /// Draw figure
        /// </summary>
        /// <param name="g">Where to draw</param>
        /// <param name="pen"></param>
        void show(Graphics g, Pen pen = null);
    }

    // нам вообще нужен класс точки?
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
  /*      public PointF make_orthographiс(int axis)
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
            PointF p = make_perspective();
            g.DrawRectangle(pen, p.X, p.Y, 2, 2);
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
            show_parallel(g, pen);
          //  show_perspective(g, pen);


     //       g.DrawLines(pen, points.ToArray());
      //      g.DrawLine(pen, points[0], points[points.Count - 1]);

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

    }

}
