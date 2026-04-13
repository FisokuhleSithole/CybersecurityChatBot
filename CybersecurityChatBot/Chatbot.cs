using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatBot
{
    public class Chatbot
    {
        // Automatic properties
        public string UserName { get; set; }
        public bool IsRunning { get; set; }
        public List<string> ConversationHistory { get; set; }

        // Dictionary to store responses (key = topic, value = response)
        private Dictionary<string, string> responses;

        // Constructor - runs when Chatbot is created
        public Chatbot()
        {
            responses = new Dictionary<string, string>();
            ConversationHistory = new List<string>();
            IsRunning = true;
            InitializeResponses();
        }

        // Initialize all the cybersecurity responses
        private void InitializeResponses()
        {
            // Basic conversation responses
            responses.Add("how are you", "I'm functioning perfectly! Thanks for asking. How can I help you stay safe online today?");
            responses.Add("purpose", "My purpose is to educate South African citizens about cybersecurity threats like phishing, malware, and social engineering. I'm here to help you stay safe online!");
            responses.Add("what can i ask", "You can ask me about:\n• Password safety\n• Phishing emails\n• Safe browsing habits\n• Recognizing suspicious links\n• Social engineering scams\n• Two-factor authentication\n• Data breaches\n\nType 'help' to see all topics!");
            responses.Add("help", @"╔══════════════════════════════════════════════════════════════╗
║                    AVAILABLE TOPICS                         ║
╠══════════════════════════════════════════════════════════════╣
║  • password     - Tips for strong passwords                 ║
║  • phishing     - How to spot phishing scams                ║
║  • safe browsing - Safe internet browsing habits            ║
║  • suspicious link - How to recognize dangerous links       ║
║  • social engineering - Manipulation tactics explained      ║
║  • 2fa          - Two-factor authentication benefits       ║
║  • data breach  - What to do if your data is stolen        ║
║  • malware      - How to avoid malicious software          ║
║  • wifi safety  - Secure Wi-Fi usage tips                  ║
║  • help         - Show this menu                           ║
║  • quit         - Exit the chatbot                         ║
╚══════════════════════════════════════════════════════════════╝");

            // Cybersecurity topic responses
            responses.Add("password", @"🔐 PASSWORD SAFETY TIPS:
• Use at least 12-16 characters
• Include uppercase, lowercase, numbers, AND symbols
• Never reuse passwords across different websites
• Use a password manager (Bitwarden, LastPass, or 1Password)
• Enable two-factor authentication whenever possible
• Avoid using personal info (birthdays, names, pet names)");

            responses.Add("phishing", @"🎣 PHISHING SCAM WARNING SIGNS:
• Urgent or threatening language ('Your account will be closed!')
• Requests for personal information (passwords, credit cards, ID numbers)
• Suspicious links or unexpected attachments
• Spelling and grammar mistakes
• Generic greetings like 'Dear Customer' or 'Dear User'
• Email addresses that don't match the official domain

⚠️ NEVER click links or download attachments from unknown senders!");

            responses.Add("safe browsing", @"🌐 SAFE BROWSING HABITS:
• Always look for 'https://' and the padlock icon in the address bar
• Avoid using public Wi-Fi for banking or sensitive transactions
• Keep your browser and extensions updated
• Use ad blockers and antivirus software
• Don't save passwords in your browser
• Clear your browsing data regularly
• Be careful what you download and install");

            responses.Add("suspicious link", @"🔗 HOW TO SPOT A SUSPICIOUS LINK:
• Hover over the link to see the real URL before clicking
• Look for misspelled domain names (like 'paypa1.com' instead of 'paypal.com')
• Be wary of shortened links (bit.ly, tinyurl.com, etc.)
• Check for extra words or numbers before the real domain
• When in doubt, type the website address manually in your browser

🚨 If something looks wrong, trust your instincts and DON'T click!");

            responses.Add("social engineering", @"🧠 SOCIAL ENGINEERING EXPLAINED:
Social engineering is when attackers manipulate people into giving up confidential information.

Common tactics include:
• Pretending to be IT support or a bank employee
• Creating fake emergencies to pressure you
• Building fake trust over time
• Impersonating someone you know

🛡️ Always verify who you're talking to before sharing sensitive information!");

            responses.Add("2fa", @"🔐 TWO-FACTOR AUTHENTICATION (2FA):
2FA adds an extra layer of security to your accounts.

How it works:
1. You enter your password (something you KNOW)
2. Then enter a code from your phone (something you HAVE)

✅ Always enable 2FA on your email, banking, and social media accounts!");

            responses.Add("data breach", @"⚠️ WHAT TO DO AFTER A DATA BREACH:
1. Change your password immediately on the affected site
2. Change passwords on any other sites where you used the same password
3. Check your bank statements for suspicious activity
4. Consider freezing your credit
5. Be extra careful of phishing attempts (scammers love data breaches!)

🔍 Check if your email has been breached at: haveibeenpwned.com");

            responses.Add("malware", @"🦠 HOW TO AVOID MALWARE:
• Only download software from official websites
• Don't open email attachments from unknown senders
• Keep your antivirus software updated
• Don't click on pop-up ads claiming your computer is infected
• Be careful with USB drives from unknown sources
• Update your operating system regularly");

            responses.Add("wifi safety", @"📶 SAFE WI-FI PRACTICES:
• Never access banking or sensitive accounts on public Wi-Fi
• Use a VPN (Virtual Private Network) on public networks
• Turn off automatic Wi-Fi connections
• Remove saved public Wi-Fi networks when you're done
• Create a strong password for your home Wi-Fi
• Change your router's default admin password");

            responses.Add("bye", @"Thank you for learning about cybersecurity today!

Remember the golden rules:
• Think before you click
• Use strong, unique passwords
• Enable 2FA whenever possible
• Stay skeptical of unexpected messages

Stay safe online! 👋");
        }

        // Start the conversation
        public void Start()
        {
            // Get user's name with validation
            GetUserName();

            // Personalized welcome
            UiHelper.SetColor("cyan");
            UiHelper.TypeWriterEffect($"\nWelcome, {UserName}!", 30);
            UiHelper.TypeWriterEffect($"I'm your Cybersecurity Awareness Assistant.", 30);
            UiHelper.TypeWriterEffect($"I'm here to help you recognize and avoid online threats.", 30);
            UiHelper.ResetColor();

            Console.WriteLine();
            UiHelper.DrawDivider();

            // Add welcome to conversation history
            AddToHistory($"Bot: Welcome, {UserName}!");

            // Show help menu
            ShowHelp();

            // Main conversation loop
            while (IsRunning)
            {
                UiHelper.SetColor("yellow");
                Console.Write($"\n[{UserName}] > ");
                UiHelper.ResetColor();

                string userInput = Console.ReadLine()?.Trim();

                // Input validation - empty input
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    UiHelper.SetColor("red");
                    UiHelper.TypeWriterEffect("I didn't catch that. Could you please say something?", 25);
                    UiHelper.ResetColor();
                    continue;
                }

                // Convert to lowercase for easier matching
                string lowerInput = userInput.ToLower();

                // Add to conversation history
                AddToHistory($"{UserName}: {userInput}");

                // Check for exit commands
                if (lowerInput == "quit" || lowerInput == "exit" || lowerInput == "goodbye" || lowerInput == "bye")
                {
                    SayGoodbye();
                    break;
                }

                // Process the input and get response
                string response = GetResponse(lowerInput);

                // Display response with typewriter effect
                UiHelper.SetColor("green");
                Console.Write("\n[Bot] > ");
                UiHelper.SetColor("white");
                UiHelper.TypeWriterEffect(response, 20);
                UiHelper.ResetColor();

                // Add bot response to history
                AddToHistory($"Bot: {response}");

                UiHelper.DrawDivider();
            }
        }

        // Get and validate user's name
        private void GetUserName()
        {
            UiHelper.SetColor("green");
            Console.Write("\nPlease enter your name: ");
            UiHelper.ResetColor();

            string input = Console.ReadLine()?.Trim();

            // Validate name input (not empty, not too long)
            while (string.IsNullOrWhiteSpace(input))
            {
                UiHelper.SetColor("red");
                Console.Write("Name cannot be empty. Please enter your name: ");
                UiHelper.ResetColor();
                input = Console.ReadLine()?.Trim();
            }

            if (input.Length > 50)
            {
                input = input.Substring(0, 50);
                UiHelper.TypeWriterEffect($"(Name shortened to {input})", 20, "gray");
            }

            UserName = input;
        }

        // Get response based on user input
        private string GetResponse(string input)
        {
            // Check each topic keyword
            foreach (var topic in responses)
            {
                if (input.Contains(topic.Key))
                {
                    return topic.Value;
                }
            }

            // Check for questions about specific topics
            if (input.Contains("what is") || input.Contains("tell me about") || input.Contains("explain"))
            {
                return "I'd love to help! Please ask me about specific topics like: password, phishing, safe browsing, suspicious links, social engineering, 2fa, or data breach. Type 'help' to see all topics.";
            }

            // Check for thank you messages
            if (input.Contains("thank") || input.Contains("thanks"))
            {
                return "You're welcome! I'm glad I could help. Is there anything else about cybersecurity you'd like to learn?";
            }

            // Check for greetings
            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
            {
                return $"Hello again, {UserName}! How can I help you with cybersecurity today? Type 'help' to see what I can do.";
            }

            // Default response for unrecognized input
            return $"I didn't quite understand that, {UserName}. Could you rephrase?\n\nTry asking about: password safety, phishing, safe browsing, suspicious links, or social engineering.\nType 'help' to see all available topics.";
        }

        // Display help menu
        private void ShowHelp()
        {
            Console.WriteLine();
            UiHelper.DisplayInBox("💡 GET STARTED", "cyan");
            UiHelper.TypeWriterEffect("Here are some things you can ask me:", 25);
            Console.WriteLine();
            UiHelper.TypeWriterEffect("  • 'Tell me about password safety'", 20);
            UiHelper.TypeWriterEffect("  • 'How to spot phishing emails?'", 20);
            UiHelper.TypeWriterEffect("  • 'What is safe browsing?'", 20);
            UiHelper.TypeWriterEffect("  • 'Explain social engineering'", 20);
            UiHelper.TypeWriterEffect("  • 'What is two-factor authentication?'", 20);
            Console.WriteLine();
            UiHelper.TypeWriterEffect("Or simply type keywords like:", 25);
            UiHelper.TypeWriterEffect("  • password", 20);
            UiHelper.TypeWriterEffect("  • phishing", 20);
            UiHelper.TypeWriterEffect("  • safe browsing", 20);
            UiHelper.TypeWriterEffect("  • help", 20);
            Console.WriteLine();
            UiHelper.DrawDivider();
        }

        // Say goodbye when user quits
        private void SayGoodbye()
        {
            Console.WriteLine();
            UiHelper.SetColor("yellow");
            UiHelper.TypeWriterEffect($"\n{responses["bye"]}", 35);
            UiHelper.TypeWriterEffect($"Stay safe online, {UserName}! 🔒", 35);
            UiHelper.ResetColor();
            IsRunning = false;
        }

        // Add message to conversation history (for future features)
        private void AddToHistory(string message)
        {
            ConversationHistory.Add($"{DateTime.Now:HH:mm:ss} - {message}");

            // Keep only last 100 messages to save memory
            if (ConversationHistory.Count > 100)
            {
                ConversationHistory.RemoveAt(0);
            }
        }

        // Show conversation history (optional feature)
        public void ShowHistory()
        {
            UiHelper.DisplayInBox("CONVERSATION HISTORY", "cyan");
            foreach (string entry in ConversationHistory)
            {
                Console.WriteLine(entry);
            }
            UiHelper.DrawDivider();
        }
    }
}