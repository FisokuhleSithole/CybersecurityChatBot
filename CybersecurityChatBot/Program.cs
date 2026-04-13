using System;
using System.IO;

namespace CybersecurityChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.WindowWidth = 100;
            Console.WindowHeight = 40;

            Console.WriteLine("========================================");
            Console.WriteLine("   CYBERSECURITY AWARENESS CHATBOT");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Initializing system...");
            Console.WriteLine();

            // Show where the program is running from
            Console.WriteLine($"[DEBUG] Running from: {Environment.CurrentDirectory}");

            // Try to play the greeting
            AudioPlayer.PlayGreeting("greeting");

            // Show ASCII logo (from your UiHelper)
            UiHelper.DisplayAsciiLogo();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}