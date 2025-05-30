#include <utils/equations.h>

#include <cmath>
namespace utils
{
    // FIREWALL NEEDS TO BE IN YZ PLANE NOT XY
    double lineLengthYZ(utils::point a, utils::point b)
    {
        return std::sqrt(std::pow(a.y - b.y, 2) + std::pow(a.z - b.z, 2));
    }

    double circumferenceToRadius(double cirumference)
    {
        return cirumference / (2 * M_PI);
    }

    utils::point interpolate(utils::point p1, utils::point p2, double t)
    {
        return utils::point{ 
            p1.y + t * (p2.y - p1.y),
            p1.z + t * (p2.z - p1.z)
        };
    }

    utils::point pointAloneLineAtZValue(utils::point p1, utils::point p2, double z)
    {
        double slope = (p2.z - p1.z) / (p2.y - p1.y);
        double intercept = p1.z - (slope * p1.y);
        double y = (z - intercept) / slope;

        return utils::point{ 0, y, z };
    }
}