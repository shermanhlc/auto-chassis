namespace AutoChassis
{
    public class SIM // Side Impact Member
    {
        double rear_axle_distance { get; set; }
        double front_axle_distance { get; set; }
        double wheelbase { get; set; }
        Toebox toebox { get; set; }

        public SIM(double rear_axle_distance, double front_axle_distance, double wheelbase, Toebox toebox)
        {
            this.rear_axle_distance = rear_axle_distance;
            this.wheelbase = wheelbase;

            this.toebox = toebox;
            this.front_axle_distance = front_axle_distance;
        }

        public void Start()
        {

        }

        public Toebox FindSimLength()
        {
            // ! Not implemented
            // F is rear, E is front
            // Not possible right now, need to know where the fron axle is
            toebox.FL.z = front_axle_distance - toebox.front_axle.z;
            toebox.FR.z = front_axle_distance - toebox.front_axle.z;

            toebox.EL.z = toebox.FL.z + toebox.EL.z;
            toebox.ER.z = toebox.FR.z + toebox.ER.z;
            toebox.GL.z = toebox.FL.z + toebox.GL.z;
            toebox.GR.z = toebox.FR.z + toebox.GR.z;
            toebox.CDL.z = toebox.FL.z + toebox.CDL.z;
            toebox.CDR.z = toebox.FR.z + toebox.CDR.z;
            toebox.DL.z = toebox.FL.z + toebox.DL.z;
            toebox.DR.z = toebox.FR.z + toebox.DR.z;

            return toebox;
        }
    }
}