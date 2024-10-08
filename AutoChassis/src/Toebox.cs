using Utilities;
using Suspension;

namespace AutoChassis
{
    public class Toebox
    {
        double tolerance { get; set; }

        public Driver driver { get; set; }

        // distance from the tip of the nose to the pedal surface (foot contant point)
        // public double pedal_position { get; set; }

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

        // roll cage
        public Point CDR { get; set; }
        public Point CDL { get; set; }

        // front axle
        public Point front_axle { get; set; }

        FrontSuspension front_suspension { get; set; }
        Firewall firewall { get; set; }

        public Toebox(double tolerance, Driver driver, FrontSuspension fs, Firewall fw, Point front_axle)
        {
            this.tolerance = tolerance;
            this.driver = driver;

            firewall = fw;
            front_suspension = fs;

            DR = new Point();
            DL = new Point();
            ER = new Point();
            EL = new Point();
            FR = new Point();
            FL = new Point();
            GR = new Point();
            GL = new Point();
            CDR = new Point();
            CDL = new Point();
            this.front_axle = front_axle;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task Start()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            BuildRearBar();
            BuildFrontBar();
            ShockMount();
            AdjustForTabsAndTubes();
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

        private void BuildRearBar()
        {
            // FR = front_suspension.lower_arm.rear;
            // DR = Equations.PointAlong3DLineAtZValue(FR, front_suspension.upper_arm.rear, firewall.SR.y);

            // FL = new Point(FR.x, -FR.y, FR.z);
            // DL = new Point(DR.x, -DR.y, DR.z);


            // new
            FL = front_suspension.lower_arm.rear;
            DL = front_suspension.upper_arm.rear;
            
            FR = new Point(-FL.x, FL.y, FL.z);
            DR = new Point(-DL.x, DL.y, DL.z);
        }

        private void BuildFrontBar()
        {
            // ER = front_suspension.lower_arm.front;
            // GR = Equations.PointAlong3DLineAtZValue(ER, front_suspension.upper_arm.front, firewall.SR.y);

            // EL = new Point(ER.x, -ER.y, ER.z);
            // GL = new Point(GR.x, -GR.y, GR.z);

            // new
            EL = front_suspension.lower_arm.front;
            GL = Equations.PointAlong3DLineAtYValue(EL, front_suspension.upper_arm.front, firewall.SR.y);

            ER = new Point(-EL.x, EL.y, EL.z);
            GR = new Point(-GL.x, GL.y, GL.z);


        }

        private void ShockMount()
        {
            CDL = front_suspension.shock.upper;
            CDR = new Point(-1 * CDL.x, CDL.y, CDL.z);
        }

        public void AdjustForTabsAndTubes()
        {
            DR.x = AdjustInwards(DR.x, TAB_LENGTH + TUBE_RADIUS);
            DL.x = AdjustInwards(DL.x, TAB_LENGTH + TUBE_RADIUS);
            ER.x = AdjustInwards(ER.x, TAB_LENGTH + TUBE_RADIUS);
            EL.x = AdjustInwards(EL.x, TAB_LENGTH + TUBE_RADIUS);
            FR.x = AdjustInwards(FR.x, TAB_LENGTH + TUBE_RADIUS);
            FL.x = AdjustInwards(FL.x, TAB_LENGTH + TUBE_RADIUS);
            GR.x = AdjustInwards(GR.x, TAB_LENGTH + TUBE_RADIUS);
            GL.x = AdjustInwards(GL.x, TAB_LENGTH + TUBE_RADIUS);
            CDR.x = AdjustInwards(CDR.x, TAB_LENGTH + TUBE_RADIUS);
            CDL.x = AdjustInwards(CDL.x, TAB_LENGTH + TUBE_RADIUS);
        }

        public double AdjustInwards(double value, double amount)
        {
            if(value > 0)
            {
                return value - amount;
            }
            else
            {
                return value + amount;
            }
        }
    }
}

// * q's
/**
 * the x-y-z planes in relation to the car?
 * 
 */
