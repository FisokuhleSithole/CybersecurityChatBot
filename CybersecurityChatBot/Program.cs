using System;
using System.Media;
using System.IO;

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

            // Welcome message with typewriter effect
            UiHelper.DrawDoubleDivider();
            UiHelper.TypeWriterEffect("WELCOME TO THE CYBERSECURITY AWARENESS BOT", 40, "yellow");
            UiHelper.DrawDoubleDivider();
            Console.WriteLine();

            // Ask for name with typewriter effect
            UiHelper.TypeWriterEffect("Please enter your name: ", 30, "green");
            string userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "Citizen";
            }

            Console.WriteLine();
            UiHelper.TypeWriterEffect($"Hello, {userName}! I'm here to help you stay safe online.", 35, "cyan");
            UiHelper.TypeWriterEffect("Let me teach you how to recognize and avoid cyber threats.", 35, "cyan");
            Console.WriteLine();

            // Display available commands in a box
            UiHelper.DisplayInBox("AVAILABLE COMMANDS", "yellow");
            UiHelper.TypeWriterEffect("  • password   - Learn about strong passwords", 25);
            UiHelper.TypeWriterEffect("  • phishing   - Recognize phishing scams", 25);
            UiHelper.TypeWriterEffect("  • safe       - Safe browsing tips", 25);
            UiHelper.TypeWriterEffect("  • help       - Show this menu again", 25);
            UiHelper.TypeWriterEffect("  • quit       - Exit the chatbot", 25);
            Console.WriteLine();

            UiHelper.DrawDivider();

            // Main conversation loop
            while (true)
            {
                Console.Write($"\n[{userName}] > ");
                string input = Console.ReadLine()?.ToLower().Trim();

                if (input == "quit" || input == "exit" || input == "goodbye")
                {
                    Console.WriteLine();
                    UiHelper.TypeWriterEffect($"Goodbye, {userName}! Stay safe online! 🔒", 40, "yellow");
                    UiHelper.TypeWriterEffect("Remember: Think before you click!", 40, "yellow");
                    break;
                }
                else if (string.IsNullOrWhiteSpace(input))
                {
                    UiHelper.TypeWriterEffect("I didn't catch that. Please say something.", 30, "red");
                }
                else if (input == "help")
                {
                    Console.WriteLine();
                    UiHelper.DisplayInBox("HELP MENU", "cyan");
                    UiHelper.TypeWriterEffect("  • password   - Tips for creating strong passwords", 25);
                    UiHelper.TypeWriterEffect("  • phishing   - How to spot phishing emails", 25);
                    UiHelper.TypeWriterEffect("  • safe       - Safe browsing habits", 25);
                    UiHelper.TypeWriterEffect("  • help       - Show this menu", 25);
                    UiHelper.TypeWriterEffect("  • quit       - Exit the chatbot", 25);
                }
                else if (input.Contains("password"))
                {
                    UiHelper.TypeWriterEffect("\n🔐 PASSWORD SAFETY TIPS:", 30, "cyan");
                    UiHelper.TypeWriterEffect("• Use at least 12 characters", 25);
                    UiHelper.TypeWriterEffect("• Include uppercase, lowercase, numbers, and symbols", 25);
                    UiHelper.TypeWriterEffect("• Never reuse passwords across different sites", 25);
                    UiHelper.TypeWriterEffect("• Use a password manager like Bitwarden or LastPass", 25);
                }
                else if (input.Contains("phishing"))
                {
                    UiHelper.TypeWriterEffect("\n🎣 PHISHING SCAM WARNING SIGNS:", 30, "cyan");
                    UiHelper.TypeWriterEffect("• Urgent or threatening language", 25);
                    UiHelper.TypeWriterEffect("• Requests for personal information", 25);
                    UiHelper.TypeWriterEffect("• Suspicious links or attachments", 25);
                    UiHelper.TypeWriterEffect("• Spelling and grammar mistakes", 25);
                    UiHelper.TypeWriterEffect("• Generic greetings like 'Dear Customer'", 25);
                }
                else if (input.Contains("safe") || input.Contains("browsing"))
                {
                    UiHelper.TypeWriterEffect("\n🌐 SAFE BROWSING HABITS:", 30, "cyan");
                    UiHelper.TypeWriterEffect("• Look for 'https://' and padlock icon", 25);
                    UiHelper.TypeWriterEffect("• Avoid public Wi-Fi for banking", 25);
                    UiHelper.TypeWriterEffect("• Keep your browser updated", 25);
                    UiHelper.TypeWriterEffect("• Use ad blockers and antivirus", 25);
                    UiHelper.TypeWriterEffect("• Don't save passwords in browsers", 25);
                }
                else
                {
                    UiHelper.TypeWriterEffect("\nI didn't quite understand that.", 30, "red");
                    UiHelper.TypeWriterEffect("Type 'help' to see what I can do!", 30, "yellow");
                }

                UiHelper.DrawDivider();
            }

            Console.WriteLine();
            UiHelper.TypeWriterEffect("Press any key to exit...", 30, "gray");
            Console.ReadKey();
        }
    }
}