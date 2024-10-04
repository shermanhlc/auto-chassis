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

        public static Point PointAlongLineAtYValue(Point p1, Point p2, double y)
        {
            double slope = (p2.y - p1.y) / (p2.x - p1.x);
            double y_intercept = p1.y - (slope * p1.x);

            double x = (y - y_intercept) / slope;

            Point point_on_line = new Point (
                x,
                y,
                0
            );

            return point_on_line;
        }

        public static Point PointAlong3DLineAtZValue(Point a, Point b, double given_z)
        {
            double t = (given_z - a.z) / (b.z - a.z);

            double x = a.x + t * (b.x - a.x);
            double y = a.y + t * (b.y - a.y);

            return new Point(x, y, given_z);
        }

        public static Point YZPointAlongArcAtAngle(Point center, Point moved, double radius, double angle)
        {
            // Math.Cos and Math.Sin take radians, not degrees (<-- f!#$ you Microsoft)
            double z = radius * Math.Cos(angle * Math.PI / 180);
            double y = radius * Math.Sin(angle * Math.PI / 180);

            return new Point(moved.x, y, z);
        }

        public static double YPointAlongArc(Point center, Point moved, double radius, double angle)
        {
            return radius * Math.Sin(angle * Math.PI / 180);
        }
    }
}
