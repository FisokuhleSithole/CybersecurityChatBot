using System;
using System.Media;
using System.IO;

namespace CybersecurityChatBot
{
    public static class AudioPlayer
    {
        public static void PlayGreeting(string fileName)
        {
            try
            {
                string foundPath = FindFile(fileName);

                if (foundPath != null)
                {
                    Console.WriteLine($"[Audio] Found greeting at: {foundPath}");
                    Console.WriteLine("[Audio] Playing...");

                    using (SoundPlayer player = new SoundPlayer(foundPath))
                    {
                        player.PlaySync();
                    }

                    Console.WriteLine("[Audio] Done!");
                }
                else
                {
                    Console.WriteLine("[Audio] Could not find greeting.wav file.");
                    Console.WriteLine("[Audio] Playing text greeting instead.");
                    PlayTextGreeting();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Audio Error] {ex.Message}");
                PlayTextGreeting();
            }
        }

        private static string FindFile(string fileName)
        {
            // List of places to search
            string[] searchLocations = new string[]
            {
                // Where the .exe is running
                AppDomain.CurrentDomain.BaseDirectory,
                
                // Add .wav extension if not there
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName + ".wav"),
                
                // One folder up
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", fileName),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", fileName + ".wav"),
                
                // Two folders up (project folder)
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", fileName),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", fileName + ".wav"),
                
                // Three folders up
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", fileName),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", fileName + ".wav"),
                
                // Your specific project path
                @"C:\Users\fisok\source\repos\CybersecurityChatBot\CybersecurityChatBot\",
                @"C:\Users\fisok\source\repos\CybersecurityChatBot\CybersecurityChatBot\greeting.wav",
                
                // Desktop
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName + ".wav"),
                
                // Downloads
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", fileName + ".wav"),
            };

            foreach (string path in searchLocations)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return null;
        }

        private static void PlayTextGreeting()
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("   WELCOME TO CYBERSECURITY AWARENESS");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Hello! Welcome to the Cybersecurity Awareness Bot.");
            Console.WriteLine("I'm here to help you stay safe online.");
            Console.WriteLine();
            System.Threading.Thread.Sleep(2000);
        }
    }
}