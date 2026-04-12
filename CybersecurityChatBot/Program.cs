using System;

namespace CybersecurityChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.WindowWidth = 100;
            Console.WindowHeight = 40;

            // Display a simple welcome message
            Console.WriteLine("========================================");
            Console.WriteLine("   CYBERSECURITY AWARENESS CHATBOT");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Initializing system...");
            Console.WriteLine();

            // Play voice greeting (stub version - will work when AudioPlayer is ready)
            AudioPlayer.PlayGreeting("greeting.wav");

            // Display logo (stub version - will work when UiHelper is ready)
           // UiHelper.DisplayAsciiLogo();

            // Start chatbot (stub version - will work when Chatbot is ready)
           // Chatbot bot = new Chatbot();
           // bot.Start();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}