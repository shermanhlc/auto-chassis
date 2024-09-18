using System;
using System.Threading.Tasks;

using Utilities;
using AutoChassis;
using Suspension;

class Program
{
    public const string version_number = "v0.1.0";
    public static string APP_DIR { get { return AppContext.BaseDirectory; } }
    private static int sleep = 00;
    public static async Task Main(string[] args)
    {

        await BootupDisplay();

        string? input;
        while(true)
        {
            input = Input.Put();

            if (input == "q" || input == "quit") {
                Printer.PrintSingleLineColor("Exiting...", ConsoleColor.Red, true);
                break;
            }
            else if (input == "a" || input == "assist") {

            }
            else if (input == "h" || input == "help") {
                Console.WriteLine("This is the help menu, it will display the available commands and their functions.");
            }
            else if (input == "t" || input == "test") {
                await TestGround();
            }
            else {
                Printer.MultipleColor(["Invalid input: ", input, " is not a valid command"], [ConsoleColor.White, ConsoleColor.Red, ConsoleColor.White]);
            }
        }
    }

    public static async Task TestGround()
    {
        Firewall fw = new Firewall
        {
            BR = new Point(0, 0),
            BL = new Point(0, 1),
            tolerance = Input.GetTolerance() + 1,
            driver = Input.GetDriver()
        };

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

    public static async Task BootupDisplay()
    {
        Console.Clear();
        await Task.Delay(sleep * 3);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Auto Chassis {version_number}");

        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < 3; i++)
        {
            await Task.Delay(sleep);
            Console.Write(". ");
        }
        Console.Write("\n");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Developed by Sam Sherman");
        await Task.Delay(sleep);
        Console.WriteLine("Auto Chassis is a tool for designing and building custom chassis to meet the specifications set by SAE for the Baja compeition.");
        await Task.Delay(sleep);
        Console.WriteLine("This tool has been made to meet the requirements for the 2024-2025 season, and may need to be updated for future seasons.");
        Console.WriteLine("\n");
        await Task.Delay(sleep * 2);

        Printer.MultipleColor(["To view the available commands, type '", "h", "' or '", "help", "' and press enter.\n"], [ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.White, ConsoleColor.Cyan, ConsoleColor.White]);
    }
}