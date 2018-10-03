using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    /*
     Вопрос: делать структуру на каждый примитив, и для всех расписывать
         */
    public struct Primitive
    {
        public List<Point> points;

        public Primitive(List<Point> pts)
        {
            this.points = new List<Point>(pts);
        }

        // double or int ?
        public void translate(int x, int y)
        {
            // TODO
        }

        public void rotate(double angle)
        {
            // TODO
        }

        public void scaling(double coef)
        {
            // TODO
        }
    }


}
