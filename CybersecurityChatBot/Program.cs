using System;
using System.Threading;

namespace CybersecurityChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.WindowWidth = 120;
            Console.WindowHeight = 50;

            // Show loading animation
            UiHelper.ShowLoadingAnimation("Initializing Cybersecurity Bot", 1500);

            // Play voice greeting
            AudioPlayer.PlayGreeting("greeting.wav");

            // Clear and show logo
            UiHelper.ClearWithDelay(500);
            UiHelper.DisplayAsciiLogo();

            // Create and start the chatbot (this handles everything)
            Chatbot bot = new Chatbot();
            bot.Start();

            // After chatbot exits, show exit message
            Console.WriteLine();
            UiHelper.TypeWriterEffect("Press any key to exit...", 30, "gray");
            Console.ReadKey();
        }
    }
}