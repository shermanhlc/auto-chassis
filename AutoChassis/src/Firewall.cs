using System;
using System.Threading.Tasks;

using Utilities;

namespace AutoChassis
{
    public class Firewall
    {
        // TODO: add checks
            // - the width exceeds 29" at 27" above the seat bottom
            // - lateral cross members exceed 8"
        // TODO: add inputs for seat bottom, side member height, tolerance, interation
        // TODO: clean up the code

        // these should be moved to a easily modifiable configuration file, so new versions of the rules can be easily implemented
        const double HEAD_CLEARANCE = 6;
        const double BODY_CLEARANCE = 3;
        const double SIDE_MEMBER_HEIGHT = 13.6;
        const double MIN_LATERAL_LENGTH = 8;
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
        public Point elbow_point { get; set; }
        public Point hip_point { get; set; }



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

            DetermineLineB();
            
            Task calcLineA = Task.Run(() => DetermineLineA());
            // Task example = Task.Run(() => Example());

            await Task.WhenAll(calcLineA);
            Console.WriteLine("finished calculations for firewall");
            Printer.PrintPointWithLabel(BR, "BR");
            Printer.PrintPointWithLabel(SR, "SR");
            Printer.PrintPointWithLabel(AR, "AR");
            Console.Write('\n');
            Printer.PrintPointWithLabel(shoulder_point, "shoulder point");
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
            CaluclateHelmetCenter();
            CalculateShoulderPoint();

            Point top_point = new Point(
                0, // start in center
                helmet_center.y + HEAD_CLEARANCE * tolerance,
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
                    if (!CheckHelmetClearance(p))
                    {
                        top_point.x += interation_step;
                        clear = false;
                        break;
                    }
                }
            }

            if (top_point.x < MIN_LATERAL_LENGTH / 2)
            {
                top_point.x = MIN_LATERAL_LENGTH * tolerance;
            }

            BR = new Point(
                top_point.x,
                top_point.y,
                0
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Requires DetermineLineB() to finishing run</remarks>
        public void DetermineLineA()
        {
            CalculateElbowPoint();
            CalculateHipPoint();

            SR = Equations.PointAlongLineAtYValue(BR, shoulder_point, SIDE_MEMBER_HEIGHT + seat_height);

            Point bottom_point = new Point(
                0,
                0,
                0 // not implemented yet
            );

            bool clear = false;
            while (!clear) 
            {
                clear = true;
                double totalPoints = Equations.Length(SR, bottom_point) / interation_step;

                for (int i = 0; i < totalPoints; i++)
                {
                    double t = i / totalPoints;
                    Point p = Equations.Interpolation(SR, bottom_point, t);
                    if (!CheckElbowClearance(p))
                    {
                        bottom_point.x += interation_step;
                        clear = false;
                        break;
                    }
                    else if (!CheckHipClearance(p))
                    {
                        bottom_point.x += interation_step;
                        clear = false;
                        break;
                    }
                }
            }

            if (bottom_point.x < MIN_LATERAL_LENGTH / 2)
            {
                bottom_point.x = MIN_LATERAL_LENGTH * tolerance;
            }

            AR = new Point(
                bottom_point.x,
                bottom_point.y,
                0
            );
        }

        bool CheckHelmetClearance(Point a)
        {
            return Equations.Length(a, helmet_center) > (HEAD_CLEARANCE * tolerance + Equations.CircumferenceToRadius(driver.helmet_circumference));
        }

        bool CheckElbowClearance(Point a)
        {
            return Equations.Length(a, shoulder_point) > BODY_CLEARANCE * tolerance;
        }

        bool CheckHipClearance(Point a)
        {
            return Equations.Length(a, hip_point) > BODY_CLEARANCE * tolerance;
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

        public Point CalculateShoulderPoint()
        {
            double x = (driver.shoulder_width / 2) + BODY_CLEARANCE * tolerance; // this differs from the other Calculate___ methods since this is a point on the CHASSIS not the driver
            double y = seat_height + driver.back_height;

            shoulder_point = new Point(x, y);
            return shoulder_point;
        }

        private Point CalculateElbowPoint()
        {
            double x = driver.shoulder_width / 2;
            double y = (seat_height + driver.back_height) - driver.upper_arm_length;

            elbow_point = new Point(x, y);
            return elbow_point;
        }

        private Point CalculateHipPoint()
        {
            double x = driver.hip_width / 2;
            double y = seat_height;

            hip_point = new Point(x, y);
            return hip_point;
        }
    }
}