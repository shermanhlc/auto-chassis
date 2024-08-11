namespace Utilities
{
    public class Driver
    {
        public double helmet_circumference { get; set; }
        public double head_height { get; set; }
        public double shoulder_width { get; set; }
        public double hip_width { get; set; }
        public double upper_arm_length { get; set; }
        public double back_height { get; set; } // hips -> shoulders

        public Driver(double helmet_circumference_ = 0, double shoulder_width_ = 0, double upper_arm_length_ = 0, double back_height_ = 0, double head_height_ = 10, double hip_width_) // head_height should default to 0 not 10
        {
            helmet_circumference = helmet_circumference_;
            shoulder_width = shoulder_width_;
            upper_arm_length = upper_arm_length_;
            back_height = back_height_;
            head_height = head_height_;
        }
    }
}
