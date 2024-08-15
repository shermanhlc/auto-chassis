using System.Drawing;
using System.Text;

namespace Utilities
{
    public class Printer
    {
        public static void PrintSingleLineColor(string str, ConsoleColor color, bool newline = true)
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            if (newline) {
                Console.Write("\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SplitColor(string first, ConsoleColor first_color, string second, ConsoleColor second_color)
        {
            Console.ForegroundColor = first_color;
            Console.Write(first);
            Console.ForegroundColor = second_color;
            Console.Write(second);
        }

        public static void MultipleColor(string[] strings, ConsoleColor[] colors, bool newline = true)
        {
            if (strings[0].Contains('\r')) {
                Console.SetCursorPosition(0, Console.CursorTop);
            // Clear the current line by writing spaces
            Console.Write(new string(' ', Console.WindowWidth - 1));
            // Reset the cursor position to the beginning of the line
            Console.SetCursorPosition(0, Console.CursorTop);
            }
            for (int i = 0; i < strings.Length; i++)
            {
                Console.ForegroundColor = colors[i];
                Console.Write(strings[i]);
            }

            if (newline) {
                Console.Write("\n");
            }
        }

        public static void PrintPoint(Point p)
        {
            MultipleColor([p.x.ToString(), " ", p.y.ToString(), " ", p.z.ToString()], [ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Green, ConsoleColor.White, ConsoleColor.Blue]);
        }

        public static void PrintPointWithLabel(Point p, string label)
        {
            MultipleColor([label, ": ", p.x.ToString(), " ", p.y.ToString(), " ", p.z.ToString()], [ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Green, ConsoleColor.White, ConsoleColor.Blue]);
        }
    }
}