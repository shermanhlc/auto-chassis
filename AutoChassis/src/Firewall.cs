using System;
using System.Threading.Tasks;

using Utilities;

namespace AutoChassis
{
    public class Firewall
    {
        // these should be moved to a easily modifiable configuration file, so new versions of the rules can be easily implemented
        const double HEAD_CLEARANCE = 6;
        const double BODY_CLEARANCE = 3;
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
        public Point shoulder_point { get; set; }

        // restraints
        public double head_clearance { get; set; }
        public double body_clearance { get; set; }


        public Point BR { get; set; } // x > 4 requried by the rules
        public Point BL { get; set; }

        public Point SR { get; set; }
        public Point SL { get; set; }

        public Point AR { get; set; } // x > 4 requried by the rules
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
            // get seat height
            seat_height = 4;

            helmet_center = CaluclateHelmetCenter();
            Console.WriteLine("helmet center: ");
            Printer.PrintPoint(helmet_center);

            double d = (HEAD_CLEARANCE + Equations.CircumferenceToRadius(driver.helmet_circumference));
            Console.WriteLine("d: " + d);

            DetermineLineB();
            DetermineLineA();
            Printer.PrintSingleLineColor("COMPLETE", ConsoleColor.Green, true);
            Printer.PrintPoint(BR);
            Printer.PrintPoint(shoulder_point);
            Printer.PrintPoint(helmet_center);

            // Printer.PrintPoint(helmet_center);

            
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
            shoulder_point = new Point(
                (driver.shoulder_width + BODY_CLEARANCE) / 2 * tolerance,
                (seat_height + driver.back_height) * tolerance,
                0 // not implemented yet
            );

            Point top_point = new Point(
                0, // start in center
                (helmet_center.y + HEAD_CLEARANCE) * tolerance,
                0 // not implemented yet
            );
            Printer.PrintPoint(top_point);
            bool clear = false;
            while (!clear)
            {
                clear = true;
                double totalPoints = Equations.Length(shoulder_point, top_point) / interation_step;

                for(int i = 0; i < totalPoints; i++)
                {
                    double t = i / totalPoints;
                    Point p = Equations.Interpolation(top_point, shoulder_point, t);
                    if (!CheckClearance(p, HEAD_CLEARANCE))
                    {
                        top_point.x += interation_step;
                        clear = false;
                        break;
                    }
                }
            }

            BR = new Point(
                top_point.x,
                top_point.y,
                0
            );
        }

        public void DetermineLineA()
        {
            
        }

        bool CheckClearance(Point a, double clearance)
        {
            return Equations.Length(a, helmet_center) > (clearance + Equations.CircumferenceToRadius(driver.helmet_circumference)) * tolerance;
        }

        public Point CaluclateHelmetCenter()
        {
            double helmet_radius = Equations.CircumferenceToRadius(driver.helmet_circumference);
            double x = 0;
            double y;
            y = seat_height + driver.back_height + (driver.head_height - helmet_radius);

            helmet_center = new Point(x, y);
            return helmet_center;
        }

        public Point ShoulderPoint()
        {
            double x = (driver.shoulder_width + BODY_CLEARANCE) / 2;
            double y = seat_height + driver.back_height;

            shoulder_point = new Point(x, y);
            return shoulder_point;
        }
    }
}