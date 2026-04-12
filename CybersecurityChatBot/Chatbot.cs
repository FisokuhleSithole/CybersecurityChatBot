using System;

namespace CybersecurityChatBot
{
    public class Chatbot
    {
        // Automatic properties
        public string UserName { get; set; }
        public bool IsRunning { get; set; }

        public Chatbot()
        {
            IsRunning = true;
        }

        public void Start()
        {
            // Simple conversation that works now
            Console.WriteLine();
            Console.Write("Please enter your name: ");
            UserName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserName = "Friend";
            }

            Console.WriteLine();
            Console.WriteLine($"Hello, {UserName}! Welcome to the Cybersecurity Awareness Bot!");
            Console.WriteLine();
            Console.WriteLine("This is the STUB VERSION. Full functionality coming soon!");
            Console.WriteLine();
            Console.WriteLine("In the final version, you'll be able to ask about:");
            Console.WriteLine("  • Password safety");
            Console.WriteLine("  • Phishing scams");
            Console.WriteLine("  • Safe browsing");
            Console.WriteLine("  • Suspicious links");
            Console.WriteLine();

            IsRunning = false; // exits after showing message
        }
    }
}