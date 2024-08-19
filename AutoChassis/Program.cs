using System;
using System.Threading.Tasks;

using Utilities;
using AutoChassis;
using Suspension;

class Program
{
    public static string version_number = "v0.0.1";
    private static int sleep = 00;
    public static async Task Main(string[] args)
    {
        await OnStart();

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
            else if (input == "s" || input == "suspension") {
                await TestGroundSuspension();
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
            driver = Input.GetDriver(),
            interation_step = .01
        };

        Task t_firewall = fw.Start();
        await Task.WhenAll(t_firewall); // <-- add other parts here to run concurrently (t_firewall, t_other, t_another)
    }

    public static async Task TestGroundSuspension()
    {
        ControlArm lower = new ControlArm {
            front = new Point(9, 0, 0.25), // 6, 7.5, 13.25
            rear = new Point(0, 0, 0)     // 15, 7.5, 13.5
        };

        ControlArm upper = new ControlArm {
            // front = new Point(),
            // rear = new Point()
        };
    }

    public static async Task OnStart()
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