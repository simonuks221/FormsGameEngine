using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FormsGameEngine
{
    public static class PointHelper
    {
        public static Point Add(Point _point1, Point _point2)
        {
            return new Point(_point1.X + _point2.X, _point1.Y + _point2.Y);
        }

        public static Point Subtract(Point _point1, Point _point2)
        {
            return new Point(_point1.X - _point2.X, _point1.Y - _point2.Y);
        }
    }
}
