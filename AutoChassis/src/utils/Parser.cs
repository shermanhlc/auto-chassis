using IO;

namespace Utilities
{
    public static class Parser
    {
        public static double ParseDouble(string str, bool zeroPermitted = false)
        {
            try {
                double d = double.Parse(str);
                if (zeroPermitted && d < 0) {
                    throw new ArgumentOutOfRangeException("Value must be greater than or equal to 0");
                }
                else if (!zeroPermitted && d <= 0) {
                    throw new ArgumentOutOfRangeException("Value must be greater than 0");
                }
                else {
                    return d;
                }
            } catch (FormatException) {
                throw;
            }
        }

        public static double ParseDoubleRange(string str, double min, double max)
        {   
            try {
                double d = ParseDouble(str, true);

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