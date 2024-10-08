using System;

namespace Utilities
{
    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public Point()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Point(double x_, double y_, double z_ = 0)
        {
            x = x_;
            y = y_;
            z = z_;
        }

        public Point(Point p)
        {
            x = p.x;
            y = p.y;
            z = p.z;
        }

        public void FlipYZ()
        {
            (z, y) = (y, z);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(Point a, Point b)
        {
            return a.x != b.x || a.y != b.y || a.z != b.z;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Point p)
            {
                return this == p;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }
    }
}