namespace Utilities
{
    public class Driver
    {
        public double helmet_circumference { get; set; }
        public double shoulder_width { get; set; }
        public double upper_arm_length { get; set; }
        public double back_height { get; set; } // hips -> shoulders

        public Driver(double helmet_circumference_ = 0, double shoulder_width_ = 0, double upper_arm_length_ = 0, double back_height_ = 0)
        {
            helmet_circumference = helmet_circumference_;
            shoulder_width = shoulder_width_;
            upper_arm_length = upper_arm_length_;
            back_height = back_height_;
        }
    }
}
