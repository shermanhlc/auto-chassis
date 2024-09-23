namespace Utilities
{
    public static class Parser
    {
        public static double ParseDouble(string str, bool zeroPermitted = false)
        {
            try {
                double d = double.Parse(str);
                if (zeroPermitted && d < 0) {
                    return -1;
                }
                else if (!zeroPermitted && d <= 0) {
                    return -1;
                }
                else {
                    return d;
                }
            } catch (FormatException) {
                Printer.PrintSingleLineColor("Invalid input.", ConsoleColor.Red);
                return -1;
            }
        }

        public static double ParseDoubleRange(string str, double min, double max)
        {
            double d = ParseDouble(str, true);
            if (d >= min && d <= max) {
                return d;
            }
            else {
                return -1;
            }
        }
    }
}