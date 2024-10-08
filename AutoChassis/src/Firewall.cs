using System;
using System.Threading.Tasks;

using Utilities;
using IO;

namespace AutoChassis
{
    // TODO: change side member height to not be a const
    // TODO: add checks
            // - the width exceeds 29" at 27" above the seat bottom
            // - lateral cross members exceed 8"
    // // TODO: add inputs for seat bottom, side member height, tolerance, interation
    // // TODO: clean up the code

    public class Firewall
    {


        // this should be a user given value
        public double sim_height { get; set; }
        /// <summary>
        /// an additive value that is added to the calculated values to account for manufacturing or measurement error
        /// </summary>
        /// <remarks> greater than 1, 1 denotes no tolerance and will produce exact dimensions that do not account for differences in drivers </remarks>
        public double tolerance { get; set; }
        /// <summary>
        /// the step size for the iterative process
        /// </summary>
        /// <remarks> defaults to 0.01, lower value will increase accuracy but also significantly increase computation time </remarks>

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

        public Firewall(double tolerance, double seat_height, double sim_height, Driver driver)
        {
            this.tolerance = tolerance;
            this.driver = driver;
            this.seat_height = seat_height;
            this.sim_height = sim_height;

            this.helmet_center = CalculateHelmetCenter();
            this.shoulder_point = CalculateShoulderPoint();
            this.elbow_point = CalculateElbowPoint();
            this.hip_point = CalculateHipPoint();

            BR = new Point();
            BL = new Point();
            SR = new Point();
            SL = new Point();
            AR = new Point();
            AL = new Point();
        }

        public async Task Start()
        {
            Console.WriteLine("beginning calculations for firewall");
            // get my tolerances
            // get my driver
            // get seat height

            DetermineLineB();
            
            Task calcLineA = Task.Run(() => DetermineLineA());
            // Task example = Task.Run(() => Example());

            await Task.WhenAll(calcLineA);

            AngleAdjustment();
            DebbieAdjustment();
            Printer.PrintPoint(BR);
            Y_IS_UP_DEV();
            Console.WriteLine("finished calculations for firewall");
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
            Point top_point = new Point(
                10.45,
                // MIN_LATERAL_LENGTH / 2, // start in center
                helmet_center.y + HEAD_CLEARANCE * tolerance,
                0 // accounted for in AngleAdjustments()
            );

            bool clear = false;
            while (!clear)
            {
                if (top_point.x > 40)
                {
                    break;
                }
                clear = true;
                double totalPoints = Equations.Length(shoulder_point, top_point) / ITERATION_STEP;

                for(int i = 0; i < totalPoints; i++)
                {
                    double t = i / totalPoints;
                    Point p = Equations.Interpolation(top_point, shoulder_point, t);
                    if (!CheckHelmetClearanceSide(p))
                    {
                        top_point.x += ITERATION_STEP;
                        clear = false;
                        break;
                    }
                }

                Point adjusted_top_point = new(Equations.YZPointAlongArcAtAngle(AR, top_point, top_point.y, FIREWALL_ANGLE));
                adjusted_top_point.y = -1 * adjusted_top_point.y;
                adjusted_top_point.FlipYZ();
                // Printer.PrintPoint(adjusted_top_point);
                // Printer.PrintPoint(top_point);

                Point adjusted_center = new(Equations.YZPointAlongArcAtAngle(AR, SR, sim_height + seat_height, FIREWALL_ANGLE));
                adjusted_center.y = -1 * adjusted_center.y;
                adjusted_center.FlipYZ();

                Point max_top_tube = new(adjusted_top_point);
                max_top_tube.z += 40;
                // Printer.PrintPoint(max_top_tube);
                // Printer.PrintPoint(adjusted_top_point);

                // calculate the cetner of head, move foward
                Point head_center = new(Equations.YZPointAlongArcAtAngle(AR, helmet_center, helmet_center.y, FIREWALL_ANGLE));
                head_center.y = head_center.y + Equations.CircumferenceToRadius(driver.helmet_circumference);

                double total_top_points = MAX_TUBE_LENGTH / ITERATION_STEP;
                double total_side_points = Equations.Length(adjusted_top_point, adjusted_center) / ITERATION_STEP;
                for(int i = 0; i <= total_top_points; i++)
                {
                    for(int j = 0; j <= total_side_points; j++)
                    {
                        double t = i / total_top_points;
                        double s = j / total_side_points;
                        // Console.WriteLine();
                        // Printer.PrintPoint(adjusted_top_point);
                        // Printer.PrintPoint(max_top_tube);
                        // Printer.PrintPoint(adjusted_center);
                        Point p = new(Equations.Interpolation(adjusted_top_point, max_top_tube, t));
                        // Printer.PrintPoint(p);
                        Point q = new(Equations.Interpolation(adjusted_top_point, adjusted_center, s));
                        
                        

                        // Console.WriteLine();
                        // Printer.PrintPoint(q);
                        // Printer.PrintPoint(p);
                        if (p != q && !CheckHelmetClearance3D(p, q, head_center))
                        {
                            Console.WriteLine("run");
                            Printer.PrintPoint(p);
                            Printer.PrintPoint(q);
                            Printer.PrintPoint(head_center);
                            Console.WriteLine();
                            Console.WriteLine(Equations.Distance3D(Equations.LineIntersection3D(p, q, head_center), head_center));
                            Console.WriteLine(HEAD_CLEARANCE * tolerance + Equations.CircumferenceToRadius(driver.helmet_circumference));
                            Console.WriteLine();
                            top_point.x += ITERATION_STEP;
                            clear = false;
                            break;
                        }
                    }
                }
                Printer.PrintPoint(top_point);
            }

            if (top_point.x < MIN_LATERAL_LENGTH / 2)
            {
                top_point.x = MIN_LATERAL_LENGTH * tolerance;
            }

            BR.x = top_point.x;
            BR.y = top_point.y;

            BL.x = -top_point.x;
            BL.y = top_point.y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Requires DetermineLineB() to finishing run</remarks>
        public void DetermineLineA()
        {
            SR = Equations.PointAlongLineAtYValue(BR, shoulder_point, sim_height + seat_height);
            SL = new Point(-SR.x, SR.y, SR.z); // mirror the point

            Point bottom_point = new Point(
                0,
                0,
                0 // not implemented yet
            );
            bool clear = false;
            while (!clear) 
            {
                clear = true;
                double totalPoints = Equations.Length(SR, bottom_point) / ITERATION_STEP;

                for (int i = 0; i < totalPoints; i++)
                {
                    double t = i / totalPoints;
                    Point p = Equations.Interpolation(SR, bottom_point, t);
                    if (!CheckElbowClearance(p))
                    {
                        bottom_point.x += ITERATION_STEP;
                        clear = false;
                        break;
                    }
                    else if (!CheckHipClearance(p))
                    {
                        bottom_point.x += ITERATION_STEP;
                        clear = false;
                        break;
                    }
                }
            }

            if (bottom_point.x < MIN_LATERAL_LENGTH / 2)
            {
                bottom_point.x = MIN_LATERAL_LENGTH * tolerance;
            }

            AR.x = bottom_point.x;
            AR.y = bottom_point.y;

            AL.x = -bottom_point.x;
            AL.y = bottom_point.y;
        }

        bool CheckHelmetClearanceSide(Point a)
        {
            return Equations.Length(a, helmet_center) > (HEAD_CLEARANCE * tolerance + Equations.CircumferenceToRadius(driver.helmet_circumference));
        }

        bool CheckHelmetClearance3D(Point side, Point top, Point center)
        {
            // // compare distance to radius
            // Console.WriteLine(Equations.Distance3D(Equations.LineIntersection3D(side, top, center), center));
            // Console.WriteLine(HEAD_CLEARANCE * tolerance + Equations.CircumferenceToRadius(driver.helmet_circumference));
            // Console.WriteLine();
            return Equations.Distance3D(Equations.LineIntersection3D(side, top, center), center) > (HEAD_CLEARANCE * tolerance + Equations.CircumferenceToRadius(driver.helmet_circumference));
        }

        bool CheckElbowClearance(Point a)
        {
            return Equations.Length(a, shoulder_point) > BODY_CLEARANCE * tolerance;
        }

        bool CheckHipClearance(Point a)
        {
            return Equations.Length(a, hip_point) > BODY_CLEARANCE * tolerance;
        }

        public Point CalculateHelmetCenter()
        {
            double helmet_radius = Equations.CircumferenceToRadius(driver.helmet_circumference);
            double x = 0;
            double y;
            y = seat_height + driver.hip_to_head - helmet_radius;

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

        private void AngleAdjustment()
        {
            SL = Equations.YZPointAlongArcAtAngle(AL, SL, sim_height + seat_height, FIREWALL_ANGLE);
            SL.y = -1 * SL.y;
            SR = Equations.YZPointAlongArcAtAngle(AR, SR, sim_height + seat_height, FIREWALL_ANGLE);
            SR.y = -1 * SR.y;

            BL = Equations.YZPointAlongArcAtAngle(AL, BL, BL.y, FIREWALL_ANGLE);
            BL.y = -1 * BL.y;
            BR = Equations.YZPointAlongArcAtAngle(AR, BR, BR.y, FIREWALL_ANGLE);
            BR.y = -1 * BR.y;
        }

        private void Y_IS_UP_DEV()
        {
            (BR.y, BR.z) = (BR.z, BR.y);
            (BL.y, BL.z) = (BL.z, BL.y);

            (SR.y, SR.z) = (SR.z, SR.y);
            (SL.y, SL.z) = (SL.z, SL.y);
        }

        private void DebbieAdjustment()
        {
            
        }
    }
}
