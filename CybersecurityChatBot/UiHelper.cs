using System;
using System.Threading;

namespace CybersecurityChatBot
{
    public static class UiHelper
    {
        // Display the ASCII logo with colors
        public static void DisplayAsciiLogo()
        {
            SetColor("cyan");

            string logo = @"
    ╔══════════════════════════════════════════════════════════════════════════════════════╗
    ║                                                                                      ║
    ║         ██████╗██╗   ██╗██████╗ ███████╗██████╗                                     ║
    ║        ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗                                    ║
    ║        ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝                                    ║
    ║        ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗                                    ║
    ║        ╚██████╗   ██║   ██████╔╝███████╗██║  ██║                                    ║
    ║         ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝                                    ║
    ║                                                                                      ║
    ║         █████╗ ██╗    ██╗ █████╗ ██████╗ ███████╗███╗   ██╗███████╗ ██████╗ ███████╗ ║
    ║        ██╔══██╗██║    ██║██╔══██╗██╔══██╗██╔════╝████╗  ██║██╔════╝██╔═══██╗██╔════╝ ║
    ║        ███████║██║ █╗ ██║███████║██████╔╝█████╗  ██╔██╗ ██║███████╗██║   ██║███████╗ ║
    ║        ██╔══██║██║███╗██║██╔══██║██╔══██╗██╔══╝  ██║╚██╗██║╚════██║██║   ██║╚════██║ ║
    ║        ██║  ██║╚███╔███╔╝██║  ██║██║  ██║███████╗██║ ╚████║███████║╚██████╔╝███████║ ║
    ║        ╚═╝  ╚═╝ ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝ ║
    ║                                                                                      ║
    ║              ╔════════════════════════════════════════════════════════╗              ║
    ║              ║     CYBERSECURITY AWARENESS ASSISTANT                  ║              ║
    ║              ║     Protecting South African Citizens Online           ║              ║
    ║              ╚════════════════════════════════════════════════════════╝              ║
    ║                                                                                      ║
    ║          [ Type 'help' for available commands | 'quit' to exit ]                     ║
    ║                                                                                      ║
    ╚══════════════════════════════════════════════════════════════════════════════════════╝
";
            Console.WriteLine(logo);
            ResetColor();

            // Pause so user can see the logo
            Thread.Sleep(1500);
        }

        // Typewriter effect - prints text one character at a time
        public static void TypeWriterEffect(string message, int delayMs)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
        }

        // Typewriter effect with color
        public static void TypeWriterEffect(string message, int delayMs, string colorName)
        {
            SetColor(colorName);
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
            ResetColor();
        }

        // Set console text color
        public static void SetColor(string colorName)
        {
            switch (colorName.ToLower())
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "white":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "gray":
                case "grey":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "darkgray":
                case "darkgrey":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        // Reset to default console color
        public static void ResetColor()
        {
            Console.ResetColor();
        }

        // Draw a divider line
        public static void DrawDivider()
        {
            SetColor("darkgray");
            Console.WriteLine(new string('─', 80));
            ResetColor();
        }

        // Draw a double divider line
        public static void DrawDoubleDivider()
        {
            SetColor("cyan");
            Console.WriteLine(new string('═', 80));
            ResetColor();
        }

        // Display a colored box around text
        public static void DisplayInBox(string message, string borderColor)
        {
            SetColor(borderColor);
            int width = message.Length + 4;
            Console.WriteLine("┌" + new string('─', width) + "┐");
            Console.WriteLine("│  " + message + "  │");
            Console.WriteLine("└" + new string('─', width) + "┘");
            ResetColor();
        }

        // Clear console with a pause effect
        public static void ClearWithDelay(int delayMs)
        {
            Thread.Sleep(delayMs);
            Console.Clear();
        }

        // Display a loading animation
        public static void ShowLoadingAnimation(string message, int durationMs)
        {
            string[] frames = { "|", "/", "-", "\\" };
            int frameCount = durationMs / 100;

            for (int i = 0; i < frameCount; i++)
            {
                Console.Write($"\r{message} {frames[i % frames.Length]}");
                Thread.Sleep(100);
            }
            Console.Write("\r" + new string(' ', message.Length + 2) + "\r");
        }
    }
}