using IO;

namespace Utilities
{
    public static class Parser
    {
        public static double ParseDouble(string str)
        {
            try {
                double d = double.Parse(str);
                return d;
            } catch (FormatException) {
                throw;
            }
        }

        public static double ParseDoubleRange(string str, double min, double max)
        {   
            try {
                double d = ParseDouble(str);

                if (d < min || d > max) {
                    throw new ArgumentOutOfRangeException($"Value must be between {min} and {max}");
                }
                else {
                    return d;
                }
            }
            catch (FormatException) {
                throw;
            }
            catch (ArgumentOutOfRangeException) {
                throw;
            }
        }
    }
}