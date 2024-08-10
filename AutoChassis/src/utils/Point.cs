using System;

namespace Utilities
{
    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public Point(double x_, double y_, double z_ = 0)
        {
            x = x_;
            y = y_;
            z = z_;
        }
    }
}