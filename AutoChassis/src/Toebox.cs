using Utilities;
using Suspension;

namespace AutoChassis
{
    public class Toebox
    {
        const double BODY_CLEARANCE = 3;

        double tolerance { get; set; }
        double interation_step { get; set; }

        public Driver driver { get; set; }
        
        // distance from the tip of the nose to the pedal surface (foot contant point)
        public double pedal_position { get; set; }

        // points
        // upper rear points
        public Point DR { get; set; }
        public Point DL { get; set; }
        // lower front points
        public Point ER { get; set; }
        public Point EL { get; set; }
        // lower rear points
        public Point FR { get; set; }
        public Point FL { get; set; }
        // upper front points
        public Point GR { get; set; }
        public Point GL { get; set; }

        FrontSuspension front_suspension { get; set; }
        Firewall firewall { get; set; }

        public Toebox(FrontSuspension fs, Firewall fw)
        {
            firewall = fw;
            front_suspension = fs;
        }

        public void FindLegAngle()
        {
            // ! not implemented yet
            // is this even possible?
        }

        public void CaluclateFootClearance()
        {
            // ! not implemented yet
            // ! the suspension points may already determine the width of the toebox, thus making this irrelevant. use CheckFootClearance() instead of thats the case

            /**
             * the front bows outwards from the top (bottom is thinner than bottom)
             * could we measure the distance from the outside of the top of the shoes, then the distance of the back of the shoes while standing? 
          // * yes
             * toe distance determines the top, heel distance determines the bottom (add this stuff to the Driver class)
             * scale with tolerance
             */
        }

        public bool CheckFootClearance()
        {
            // ! not implemented yet

            /**
             * check the length of the shoe is not longer than the height of the toebox (lower front to upper front)
             * check the width of both shoes is not wider than the width of the toebox, both upper and lower (refer to CaluclateFootClearance() for human measurements)
             */

             return false;
        }

        public bool CheckLegClearance()
        {
            // ! not implemented yet

            /**
             * this one is a doozy
             * 
             * should it require the sim length first (distance from bottom of the firewall to lower rear control arm point)?
             * if yes, will need function that can caluclate the angle of the leg, then find the height of the leg at the end of the sims
             * check any point on the upper leg (lower leg needs no clearance) is at least 3" from any member
             */

            /**
             * this uses distance of the rear axle from the firewall, then the distance between axles
             * check the 
             */

             return false;
        }

        public void BuildRearBar()
        {
            FR = front_suspension.lower_arm.rear;
            
            DR = Equations.PointAlongLineAtYValue(FR, front_suspension.upper_arm.rear, firewall.SR.y);

        }
    }
}

// * q's
/**
 * the x-y-z planes in relation to the car?
 * 
 */
