using Utilities;

namespace IO
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
                try
                {
                    t = Parser.ParseDoubleRange(line, 0, 1);
                }
                catch (Exception)
                {
                    { // invalid input
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.MultipleColor(["Enter ", "tolerance", ": "], [ConsoleColor.White, ConsoleColor.Green, ConsoleColor.White], false);
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
                try {
                    driver.helmet_circumference = Parser.ParseDoubleRange(line, 0, 100);
                }
                catch (Exception)
                {
                    { // invalid input
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.SplitColor("\rHelment circumference ", ConsoleColor.Green, "(front facing): ", ConsoleColor.White);
                }
            }

            Printer.PrintSingleLineColor("Shoulder width: ", ConsoleColor.Green, false);
            while ((line = Put()) != null)
            {
                try{
                    driver.shoulder_width = Parser.ParseDoubleRange(line, 0, 100);
                }
                catch (Exception)
                {
                    { // invalid input
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.PrintSingleLineColor("\rShoulder width: ", ConsoleColor.Green, false);
                }
            }

            Printer.PrintSingleLineColor("Upper arm length: ", ConsoleColor.Green, false);
            while ((line = Put()) != null)
            {
                try {
                    driver.upper_arm_length = Parser.ParseDoubleRange(line, 0, 100);
                }
                catch (Exception)
                {
                    { // invalid input
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.PrintSingleLineColor("\rUpper arm length: ", ConsoleColor.Green, false);
                }
            }

            Printer.PrintSingleLineColor("Back height: ", ConsoleColor.Green, false);
            while ((line = Put()) != null)
            {
                try {
                    driver.back_height = Parser.ParseDoubleRange(line, 0, 100);
                }
                catch (Exception)
                {
                    { // invalid input
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(invalid_number);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Printer.PrintSingleLineColor("\rBack height: ", ConsoleColor.Green, false);
                }
            }

            if (driver.helmet_circumference == 0 || driver.shoulder_width == 0 || driver.upper_arm_length == 0 || driver.back_height == 0) {
                Printer.PrintSingleLineColor("Something went wrong when buiding the druver. Restart the process.", ConsoleColor.Red);
            }

            return driver;
        }
    }
}