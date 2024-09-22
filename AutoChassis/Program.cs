using System;
using System.Threading.Tasks;


using AutoChassis;
using IO;
using Suspension;
using Utilities;

class Program
{
    public static string version_number = GetVersionNumber();
    public static readonly string EMPTY_ERR = "This exception should be described manually when caught";
    public static async Task Main(string[] args)
    {
        try {
            version_number = GetVersionNumber();

            if(args.Length == 0) {
                throw new ArgumentException(EMPTY_ERR);
            }

            foreach(string arg in args) 
            {
                switch(arg.ToLower()) 
                {
                    case "--help":
                    case "-h":
                        ManPage.Help();
                        break;

                    case "--version":
                    case "-v":
                        Printer.PrintSingleLineColor(version_number, ConsoleColor.White);
                        break;

                    case "--test":
                    case "-t":
                        await TestGround();
                        break;

                    default:
                        Printer.MultipleColor(["Invalid argument: ", arg, "\nUse ", "--help", " to see available options" ], [ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.White, ConsoleColor.Cyan, ConsoleColor.White]);
                        break;
                }
            }
        }
        catch (ArgumentException) {
            Printer.MultipleColor(["No arguments provided. ",  "Use ", "--help", " to see available options" ], [ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Cyan, ConsoleColor.White]);
        }
    }

    private static string GetVersionNumber()
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream("AutoChassis.VERSION");

        if (stream == null) {
            return "VERSION-null";
        }

        using StreamReader reader = new(stream); {
            return reader.ReadToEnd();
        }
    }

    public static async Task TestGround()
    {
        Firewall fw = new(Input.GetTolerance(), 4, 13.6, Input.GetDriver());

        Task t_firewall = fw.Start();
        await Task.WhenAll(t_firewall); // <-- add other parts here to run concurrently (t_firewall, t_other, t_another)

        await TestGroundSuspension(fw);

        Printer.PrintPointWithLabel(fw.BR, "BR");
        Printer.PrintPointWithLabel(fw.AR, "AR");
        Printer.PrintPointWithLabel(fw.SR, "SR");
    }

    public static async Task TestGroundSuspension(Firewall fw)
    {
        // 9 inches apart, perfectly parallel to the center plane, and the front is .25 inches higher than the rear
        ControlArm lower = new ControlArm {
            front = new Point(9, 7.5, 0.25), // 6, 7.5, 13.25)
            rear = new Point(0, 7.5, 0)      // 15, 7.5, 13.5
        };

        ControlArm upper = new ControlArm {
            front = new Point(9, 9.4, 9.5),
            rear = new Point(0, 9.4, 8)
        };

        Shock shock = new Shock {
            lower = new Point(4, 20.75, 5.5), // < this point does not matter and probably is never used
            upper = new Point(-5.5, 8.75, 19)
        };


        FrontSuspension fs = new FrontSuspension {
            lower_arm = lower,
            upper_arm = upper,
            shock = shock
        };

        Toebox tb = new Toebox(fs, fw);
        await tb.Start();

        Printer.PrintPointWithLabel(tb.DR, "DR");
        Printer.PrintPointWithLabel(tb.DL, "DL");
        Printer.PrintPointWithLabel(tb.ER, "ER");
        Printer.PrintPointWithLabel(tb.EL, "EL");
        Printer.PrintPointWithLabel(tb.FR, "FR");
        Printer.PrintPointWithLabel(tb.FL, "FL");
        Printer.PrintPointWithLabel(tb.GR, "GR");
        Printer.PrintPointWithLabel(tb.GL, "GL");
        Printer.PrintPointWithLabel(tb.CDR, "CDR");
        Printer.PrintPointWithLabel(tb.CDL, "CDL");

        // upper_arm_shock_mount: (11, 20,75, 19) >> (4, 20.75, 5.5)
        // chassis_shock_mount: (20.5, 8.75, 32) >> (-5.5, 8.75, 19)
    }








    // public static async Task BootupDisplay()
    // {
    //     Console.Clear();
    //     await Task.Delay(SLEEP * 3);
    //     Console.ForegroundColor = ConsoleColor.Yellow;
    //     Console.WriteLine($"Auto Chassis {version_number}");

    //     Console.ForegroundColor = ConsoleColor.White;
    //     for (int i = 0; i < 3; i++)
    //     {
    //         await Task.Delay(SLEEP);
    //         Console.Write(". ");
    //     }
    //     Console.Write("\n");

    //     Console.ForegroundColor = ConsoleColor.White;
    //     Console.WriteLine("Developed by Sam Sherman");
    //     await Task.Delay(SLEEP);
    //     Console.WriteLine("Auto Chassis is a tool for designing and building custom chassis to meet the specifications set by SAE for the Baja compeition.");
    //     await Task.Delay(SLEEP);
    //     Console.WriteLine("This tool has been made to meet the requirements for the 2024-2025 season, and may need to be updated for future seasons.");
    //     Console.WriteLine("\n");
    //     await Task.Delay(SLEEP * 2);

    //     Printer.MultipleColor(["To view the available commands, type '", "h", "' or '", "help", "' and press enter.\n"], [ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.White, ConsoleColor.Cyan, ConsoleColor.White]);
    // }
}