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
        public Primitive(List<PointF> pts)
        {
            this.points = new List<PointF>(pts);
            p_type = points.Count;
        }

        // double or int ?
        public void translate(double x, double y)
        {
            // TODO
        }

        public void rotate(double angle, Point center)
        {
            //на точку всем плевать
            //поворот ребра осуществляется вокруг центра, поворот многоугольника вокруг заданной точки
            // TODO
        }

        public void scaling(double coef)
        {
            //и тут тоже на точку плевать, она - материальная точка
            // TODO
        }
    }


}
