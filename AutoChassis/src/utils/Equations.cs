using System;

namespace Utilities
{
    public class Equations
    {
        public static double Length(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }

        public static double CircumferenceToRadius(double circumference)
        {
            return circumference / (2 * Math.PI);
        }

        public static Point Interpolation(Point p1, Point p2, double t)
        {
            return new Point(
                p1.x + t * (p2.x - p1.x),
                p1.y + t * (p2.y - p1.y)
            );
        }

    }
}
