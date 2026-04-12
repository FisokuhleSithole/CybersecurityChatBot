using System;

namespace CybersecurityChatBot
{
    public static class UiHelper
    {
        public static void DisplayAsciiLogo()
        {
            //Simple ASCII art that works now
            Console.ForegroundColor = ConsoleColor.Cyan;

            string logo = @"
    ╔══════════════════════════════════════════════════════════════════════╗
    ║                                                                      ║
    ║         ██████╗██╗   ██╗██████╗ ███████╗██████╗                      ║
    ║        ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗                     ║
    ║        ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝                     ║
    ║        ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗                     ║
    ║        ╚██████╗   ██║   ██████╔╝███████╗██║  ██║                     ║
    ║         ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝                     ║
    ║                                                                      ║
    ║              [ CYBERSECURITY AWARENESS ASSISTANT ]                   ║
    ║                  Protecting South African Citizens                   ║
    ║                                                                      ║
    ╚══════════════════════════════════════════════════════════════════════╝
";
            Console.WriteLine(logo);
            Console.ResetColor();

            // Add a small delay for effect
            System.Threading.Thread.Sleep(1000);
        }

        public static void TypeWriterEffect(string message, int delayMs)
        {
            // Just prints normally for now
            Console.WriteLine(message);
        }

        public static void SetColor(string colorName)
        {
            //  Basic color setting
            switch (colorName.ToLower())
            {
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }

        public static void DrawDivider()
        {
            Console.WriteLine(new string('─', 80));
        }
    }
}