using Utilities;
using IO;

namespace AutoChassis.Tests
{
    public class EquationsTests
    {
        [Fact]
        public void DistanceBetweenPoints()
        {
            Point a = new();
            Point b = new(4, 0, 0);

            Assert.Equal(4.0, Equations.Distance3D(a, b));
        }

        [Fact]
        public void DistanceBetweenPoints2()
        {
            Point a = new(1, 1, 1);
            Point b = new(4, 5, 6);

            Assert.Equal(7.071067812, Equations.Distance3D(a, b), 9);
        }

        [Fact]
        public void DistanceBetweenSamePoint()
        {
            Point a = new(4, 2, 9);
            Point b = new(4, 2, 9);

            Assert.Equal(0.0, Equations.Distance3D(a, b));
        }

        [Fact]
        public void LineIntersection()
        {
            Point a = new Point(12, 16, 7);
            Point b = new Point(-4, -5, -6);
            Point org = new();

            Point t = Equations.LineIntersection3D(a, b, org);
            Assert.Equal(0.5635, t.x, 4);
            Assert.Equal(0.9896, t.y, 4);
            Assert.Equal(-2.292, t.z, 3);
        }

        [Fact]
        public void LineIntersection2()
        {
            Point a = new Point(160, 40, 0);
            Point b = new Point(160, 40, 0);
        }
    }
}
