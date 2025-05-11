namespace IO
{
    public class ManPage
    {
        public static void Help()
        {
            Console.WriteLine("AutoChassis is a tool for designing and building custom car chassis.");
            Console.WriteLine("Usage: AutoChassis [options]");
            Console.WriteLine("Options:");
            Console.WriteLine("  -h, --help     Display this help message");
            Console.WriteLine("  -v, --version  Display version of AutoChassis");
            Console.WriteLine("  -c, --config   Specify a configuration file");
            Console.WriteLine("  -o, --output   Specify an output file");
            Console.WriteLine("  -i, --input    Specify an input file");
            Console.WriteLine("  -b,  --build   Build a chassis");
        }
    }
}