#pragma once

#include <utils/point.h>

namespace utils
{
    double lineLength(utils::point a, utils::point b);

    double circumferenceToRadius(double cirumference);

    utils::point interpolate(utils::point p1, utils::point p2, double t);

    utils::point pointAloneLineAtZValue(utils::point p1, utils::point p2, double z);
}

/*
x: front to rear
y: left to right
z: bottom to top
*/