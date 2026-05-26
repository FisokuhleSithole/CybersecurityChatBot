using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CybersecurityChatBot
{
    public partial class Form1 : Form
    {
        // Memory variables
        private string userName = "";
        private string userInterest = "";
        private string currentTopic = "";
        private Dictionary<string, string> userMemory = new Dictionary<string, string>();

        // Response system
        private Dictionary<string, List<string>> responses;
        private Random random = new Random();

        // Conversation history
        private List<string> conversationHistory = new List<string>();

        public Form1()
        {
            InitializeComponent();
            InitializeResponses();
            PlayVoiceGreeting();
            DisplayWelcomeMessage();

            // Enable full screen mode
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeResponses()
        {
            responses = new Dictionary<string, List<string>>();

            // Password responses
            responses.Add("password", new List<string>
            {
                "🔐 Use strong passwords with at least 12 characters, including uppercase, lowercase, numbers, and symbols!",
                "🔐 Never reuse passwords across different accounts. Each account needs its own unique password!",
                "🔐 Consider using a password manager like Bitwarden or LastPass to generate and store strong passwords!",
                "🔐 Avoid using personal information like birthdays or pet names in your passwords!"
            });

            // Phishing responses
            responses.Add("phishing", new List<string>
            {
                "🎣 Phishing emails often create urgency. Always verify the sender's email address before clicking any links!",
                "🎣 Never download attachments from unknown senders. They may contain malware that steals your information!",
                "🎣 Look for spelling and grammar mistakes – these are common signs of phishing attempts!",
                "🎣 Hover over links to see the real URL before clicking. Scammers use fake domains like 'paypa1.com'!"
            });

            // Privacy responses
            responses.Add("privacy", new List<string>
            {
                "🔒 Review your privacy settings on social media regularly. Limit what the public can see!",
                "🔒 Be careful what personal information you share online – once it's out there, it's hard to remove!",
                "🔒 Use two-factor authentication (2FA) on all accounts that offer it for extra security!",
                "🔒 Regularly check which apps have access to your data and remove ones you don't use!"
            });
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                AudioPlayer.PlayGreeting("greeting.wav");
            }
            catch (Exception ex)
            {
                AddBotMessage("(Voice greeting could not be played: " + ex.Message + ")");
            }
        }

        private void DisplayWelcomeMessage()
        {
            AddBotMessage("═══════════════════════════════════════════════════════════════════");
            AddBotMessage("Welcome to the Cybersecurity Awareness Chatbot!");
            AddBotMessage("I'm here to help you learn about online safety.");
            AddBotMessage("");
            AddBotMessage("You can ask me about:");
            AddBotMessage("  • password - Password safety tips");
            AddBotMessage("  • phishing - How to spot scams");
            AddBotMessage("  • privacy - Protecting your personal information");
            AddBotMessage("");
            AddBotMessage("What's your name?");
            AddBotMessage("═══════════════════════════════════════════════════════════════════");
            AddBotMessage("");
        }

        private string GetRandomResponse(string keyword)
        {
            if (responses.ContainsKey(keyword) && responses[keyword].Count > 0)
            {
                int index = random.Next(responses[keyword].Count);
                return responses[keyword][index];
            }
            return "I have information on that topic. Please ask me about: password, phishing, or privacy.";
        }

        private string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("anxious") || input.Contains("nervous") || input.Contains("concerned"))
                return "worried";
            if (input.Contains("curious") || input.Contains("interested") || input.Contains("want to learn"))
                return "curious";
            if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed"))
                return "frustrated";
            if (input.Contains("confused") || input.Contains("don't understand"))
                return "confused";
            return "neutral";
        }

        private string HandleSentiment(string sentiment, string input)
        {
            // First, check if there's a topic in the input
            string detectedTopic = "";
            foreach (var keyword in responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    detectedTopic = keyword;
                    break;
                }
            }

            switch (sentiment)
            {
                case "worried":
                    if (!string.IsNullOrEmpty(detectedTopic))
                    {
                        currentTopic = detectedTopic;
                        return $"It's completely understandable to feel worried about {detectedTopic}. Let me share some tips to help you stay safe:\n\n{GetRandomResponse(detectedTopic)}";
                    }
                    return "It's normal to feel worried about online threats. Would you like me to share some general safety tips about password, phishing, or privacy?";

                case "curious":
                    if (!string.IsNullOrEmpty(detectedTopic))
                    {
                        return $"That's great that you're curious about {detectedTopic}! Here's what you should know:\n\n{GetRandomResponse(detectedTopic)}";
                    }
                    return "That's great! Curiosity helps you learn and stay safe. What specific topic would you like to explore? (password, phishing, or privacy)";

                case "frustrated":
                    return "I understand cybersecurity can be frustrating. Let's take it step by step. What specific issue are you dealing with?";

                case "confused":
                    return "I apologize if I wasn't clear. Let me try again. What would you like to know about cybersecurity?";

                default:
                    return "";
            }
        }

        private string ProcessInput(string input)
        {
            string lowerInput = input.ToLower();

            // STEP 1: Check for name input first
            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                userMemory["name"] = userName;
                return $"Nice to meet you, {userName}! What cybersecurity topic would you like to learn about? (Try: password, phishing, or privacy)";
            }

            // STEP 2: Check for sentiment BEFORE keyword recognition
            string sentiment = DetectSentiment(lowerInput);
            if (sentiment != "neutral")
            {
                string sentimentResponse = HandleSentiment(sentiment, lowerInput);
                if (!string.IsNullOrEmpty(sentimentResponse))
                {
                    return sentimentResponse;
                }
            }

            // STEP 3: Check for conversation flow - "tell me more"
            if (lowerInput.Contains("tell me more") || lowerInput.Contains("another tip") || lowerInput.Contains("more info"))
            {
                if (!string.IsNullOrEmpty(currentTopic) && responses.ContainsKey(currentTopic))
                {
                    return $"Here's another tip about {currentTopic}:\n\n{GetRandomResponse(currentTopic)}";
                }
                return "Sure! What topic would you like to learn more about? (password, phishing, or privacy)";
            }

            // STEP 4: Check for thank you
            if (lowerInput.Contains("thank"))
            {
                return $"You're welcome, {userName}! I'm glad I could help. Is there anything else you'd like to learn about?";
            }

            // STEP 5: Check for greeting (but only if not already handled)
            if (lowerInput == "hello" || lowerInput == "hi" || lowerInput == "hey")
            {
                return $"Hello again, {userName}! How can I help you today?";
            }

            // STEP 6: Check for interest memory
            if (lowerInput.Contains("interested in"))
            {
                if (lowerInput.Contains("password")) userInterest = "password";
                else if (lowerInput.Contains("phishing")) userInterest = "phishing";
                else if (lowerInput.Contains("privacy")) userInterest = "privacy";

                userMemory["interest"] = userInterest;
                if (!string.IsNullOrEmpty(userInterest) && responses.ContainsKey(userInterest))
                {
                    return $"Great! I'll remember that you're interested in {userInterest}. Here's a tip:\n\n{GetRandomResponse(userInterest)}";
                }
            }

            // STEP 7: Check for keyword recognition
            foreach (var keyword in responses.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    currentTopic = keyword;
                    return GetRandomResponse(keyword);
                }
            }

            // STEP 8: Default response
            return "I'm not sure I understand. You can ask me about:\n• password - Password safety tips\n• phishing - How to spot scams\n• privacy - Protecting your personal information\n\nOr tell me if you're worried or curious about something!";
        }

        private void AddUserMessage(string message)
        {
            txtChat.AppendText($"[{DateTime.Now:HH:mm}] You: {message}\n");
            conversationHistory.Add($"You: {message}");
        }

        private void AddBotMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                txtChat.AppendText($"[{DateTime.Now:HH:mm}] Bot: {message}\n\n");
                conversationHistory.Add($"Bot: {message}");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                AddBotMessage("Please enter a message. I'm here to help!");
                return;
            }

            AddUserMessage(userInput);
            string response = ProcessInput(userInput);
            AddBotMessage(response);

            txtUserInput.Clear();
            txtUserInput.Focus();

            // Auto-scroll to bottom
            txtChat.SelectionStart = txtChat.Text.Length;
            txtChat.ScrollToCaret();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtChat.Clear();
            AddBotMessage("Conversation cleared. How can I help you today?");
        }

        private void txtUserInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSend_Click(sender, null);
                e.Handled = true;
            }
        }
    }
}