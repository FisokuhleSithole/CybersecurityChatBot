using System;

namespace CybersecurityChatBot
{
    public static class AudioPlayer
    {
        public static void PlayGreeting(string filePath)
        {
            // STUB VERSION: This will be replaced with real sound later
            Console.WriteLine("[Audio] Would play greeting from: " + filePath);
            Console.WriteLine("[Audio] Hello! Welcome to the Cybersecurity Awareness Bot!");
            Console.WriteLine();

           
            System.Threading.Thread.Sleep(1000);
        }
    }
}