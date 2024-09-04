namespace AutoChassis
{
    public class SIM // Side Impact Member
    {
        double rear_axle_distance { get; set; }
        double wheelbase { get; set; }

        public SIM(double rear_axle_distance, double wheelbase)
        {
            this.rear_axle_distance = rear_axle_distance;
            this.wheelbase = wheelbase;
        }

        public void Start()
        {

        }

        private void FindSimLength()
        {
            // ! Not implemented

            // Not possible right now, need to know where the fron axle is
        }
    }
}