using System.Threading.Tasks;

namespace Utilities
{
    public static class Input
    {
        private static string? line;
        private static string invalid_number = "Invalid input: value must be a number greater than 0";
        public static string? Put() { return Console.ReadLine(); }

        public static Point GetPoint()
        {   
            Console.WriteLine("Enter point coordinates <x y>: ");
            while ((line = Put()) != null )
            {
                if (line == null)
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }
            }

            #pragma warning disable CS8602
            string[] parts = line.Split(' '); // gives possible null reference warning, but line cannot be null (loop prevents it)
            #pragma warning restore CS8602
            return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        public static double GetTolerance()
        {
            double t = 0;
            Printer.MultipleColor(["Enter ", "tolerance", ": "], [ConsoleColor.White, ConsoleColor.Green, ConsoleColor.White], false);
            while ((line = Put()) != null)
            {
                t = Parser.ParseDoubleRange(line, 0, 1);
                if (t == -1)
                {
                    { // invalid input
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.MultipleColor(["Enter ", "tolerance", ": "], [ConsoleColor.White, ConsoleColor.Green, ConsoleColor.White], false);
                }
                else
                {
                    break;
                }
            }

            return t;
        }

        public static Driver GetDriver()
        {
            Driver driver = new Driver();

            Console.WriteLine("Enter driver information...");
            Console.WriteLine("These driver dimensions should be for the largest driver intending to use the vehicle.");

            Printer.SplitColor("Helment circumference ", ConsoleColor.Green, "(front facing): ", ConsoleColor.White);
            while ((line = Put()) != null)
            {
                driver.helmet_circumference = Parser.ParseDouble(line);
                if (driver.helmet_circumference == -1)
                {
                    { // invalid input
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.SplitColor("\rHelment circumference ", ConsoleColor.Green, "(front facing): ", ConsoleColor.White);
                }
                else
                {
                    break;
                }
            }

            Printer.PrintSingleLineColor("Shoulder width: ", ConsoleColor.Green, false);
            while ((line = Put()) != null)
            {
                driver.shoulder_width = Parser.ParseDouble(line);
                if (driver.shoulder_width == -1)
                {
                    { // invalid input
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.PrintSingleLineColor("\rShoulder width: ", ConsoleColor.Green, false);
                }
                else
                {
                    break;
                }
            }

            Printer.PrintSingleLineColor("Upper arm length: ", ConsoleColor.Green, false);
            while ((line = Put()) != null)
            {
                driver.upper_arm_length = Parser.ParseDouble(line);
                if (driver.upper_arm_length == -1)
                {
                    { // invalid input
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.PrintSingleLineColor("\rUpper arm length: ", ConsoleColor.Green, false);
                }
                else
                {
                    break;
                }
            }

            Printer.PrintSingleLineColor("Back height: ", ConsoleColor.Green, false);
            while ((line = Put()) != null)
            {
                driver.back_height = Parser.ParseDouble(line);
                if (driver.back_height == -1)
                {
                    { // invalid input
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.PrintSingleLineColor("\rBack height: ", ConsoleColor.Green, false);
                }
                else
                {
                    break;
                }
            }

            if (driver.helmet_circumference == 0 || driver.shoulder_width == 0 || driver.upper_arm_length == 0 || driver.back_height == 0) {
                Console.WriteLine("Something went wrong when buiding the druver. Restart the process.");
            }

            return driver;
        }
    }
}