using System;
using System.Threading.Tasks;

using Utilities;

namespace AutoChassis
{
    public class Firewall
    {
        double HEAD_CLEARANCE = 6;
        double BODY_CLEARANCE = 3;
        /// <summary>
        /// an additive value that is added to the calculated values to account for manufacturing or measurement error
        /// </summary>
        /// <remarks> between 0 and 1, 0 denotes no tolerance and will produce exact dimensions that do not account for differences in drivers </remarks>
        public double tolerance { get; set; }
        /// <summary>
        /// the step size for the iterative process
        /// </summary>
        /// <remarks> defaults to 0.01, lower value will increase accuracy but also significantly increase computation time </remarks>
        public double interation_step { get; set; }

        // inputs
        public Driver driver { get; set; }
        public double seat_height { get; set; } // distance from the plane of tube A (ar <-> al) to the TOP of the seat
        public Point helmet_center { get; set; }

        // restraints
        public double head_clearance { get; set; }
        public double body_clearance { get; set; }


        public Point BR { get; set; }
        public Point BL { get; set; }

        public Point SR { get; set; }
        public Point SL { get; set; }

        public Point AR { get; set; }
        public Point AL { get; set; }

        public Firewall()
        {
            // tolerance = tolerance + 1;
            // interation_step = interation_step;
        }

        public async Task Start()
        {
            Console.WriteLine("beginning calculations for firewall");
            // get my tolerances
            // get my driver

            helmet_center = CaluclateHelmetCenter();
            Printer.PrintPoint(helmet_center);

            DetermineLineB();
            Printer.PrintSingleLineColor("COMPLETE", ConsoleColor.Green, true);
            Printer.PrintPoint(BR);

            Printer.PrintPoint(helmet_center);
        }

        public void CalculatePermeter()
        {
            Console.WriteLine("calculating perimeter of firewall");
        }

        public double BLength()
        {
            return Equations.Length(BL, BR);
        }

        public double SLength()
        {
            return Equations.Length(SL, SR);
        }

        public double ALength()
        {
            return Equations.Length(AL, AR);
        }

        // distance between the two points 27" above the seat must be minimum of 29" apart
        // seat top to the upper points must be at least 41" tall
        // tube S must be 8"-14" above the seat top

        // DRIVER CLEARANCE:
        // must have 6 inch clearance from any point on the driver's helmet
        // must have 3 inch clearance from any other point on the driver's body and the tubes

        // find x and y of the point at the height of the hips
        // find x and y of the point at the height of the shoulders
            // calculate the top based on the head use those points to get an angle
        // determine line S line

        public void DetermineLineB()
        {
            Point shoulder_point = new Point(
                (driver.shoulder_width + BODY_CLEARANCE) / 2 * tolerance,
                (seat_height + driver.back_height) * tolerance,
                0 // not implemented yet
            );

            Point top_point = new Point(
                0, // start in center
                (helmet_center.y + HEAD_CLEARANCE) * tolerance,
                0 // not implemented yet
            );

            bool clear = false;
            while (!clear)
            {
                clear = true;
                double totalPoints = Equations.Length(shoulder_point, top_point) / interation_step;

                for(int i = 0; i < totalPoints; i++)
                {
                    double t = i / totalPoints;
                    Point p = Equations.Interpolation(top_point, shoulder_point, t);
                    if (!CheckClearance(p))
                    {
                        //Console.WriteLine(CheckClearance(p));
                        top_point.x += interation_step;
                        clear = false;
                        break;
                    }

                    if (i % 500 == 0)
                    {
                        Printer.PrintPoint(top_point);
                    }
                }
                //Console.WriteLine("clear: " + clear);
            }

            BR = new Point(
                top_point.x,
                top_point.y,
                0
            );

            bool CheckClearance(Point a)
            {
                // Console.WriteLine("len " + Equations.Length(a, helmet_center));
                // Console.WriteLine("oth " + (HEAD_CLEARANCE + Equations.CircumferenceToRadius(driver.helmet_circumference)) * tolerance);
                // Console.WriteLine(Equations.Length(a, helmet_center) > (HEAD_CLEARANCE + Equations.CircumferenceToRadius(driver.helmet_circumference)) * tolerance);
                return Equations.Length(a, helmet_center) > (HEAD_CLEARANCE + Equations.CircumferenceToRadius(driver.helmet_circumference)) * tolerance;
            }
        }

        public Point CaluclateHelmetCenter()
        {
            double helmet_radius = Equations.CircumferenceToRadius(driver.helmet_circumference);
            double x = 0;
            double y;
            y = seat_height + driver.back_height + helmet_radius;

            helmet_center = new Point(x, y);
            return helmet_center;
        }
    }
}