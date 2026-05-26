using System;
using System.Collections.Generic;
using System.Drawing;
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
        }

        private void InitializeResponses()
        {
            responses = new Dictionary<string, List<string>>();

            // Password responses
            responses.Add("password", new List<string>
            {
                "Use strong passwords with at least 12 characters, including uppercase, lowercase, numbers, and symbols!",
                "Never reuse passwords across different accounts. Each account needs its own unique password!",
                "Consider using a password manager like Bitwarden or LastPass!",
                "Avoid using personal information like birthdays or pet names in your passwords!"
            });

            // Phishing responses
            responses.Add("phishing", new List<string>
            {
                "Phishing emails often create urgency. Always verify the sender's email address before clicking links!",
                "Never download attachments from unknown senders. They may contain malware!",
                "Look for spelling and grammar mistakes – common signs of phishing!",
                "Hover over links to see the real URL before clicking!"
            });

            // Privacy responses
            responses.Add("privacy", new List<string>
            {
                "Review your privacy settings on social media regularly. Limit what the public can see!",
                "Be careful what personal information you share online – once it's out, it's hard to remove!",
                "Use two-factor authentication (2FA) on all accounts that offer it!",
                "Regularly check which apps have access to your data and remove ones you don't use!"
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
                AddBotMessage("(Voice greeting could not be played)");
            }
        }

        private void DisplayWelcomeMessage()
        {
            AddBotMessage("Welcome to the Cybersecurity Awareness Chatbot!");
            AddBotMessage("I'm here to help you learn about online safety.");
            AddBotMessage("");
            AddBotMessage("You can ask me about:");
            AddBotMessage("  • password - Password safety tips");
            AddBotMessage("  • phishing - How to spot scams");
            AddBotMessage("  • privacy - Protecting your personal information");
            AddBotMessage("");
            AddBotMessage("What's your name?");
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

        private string ProcessInput(string input)
        {
            string lowerInput = input.ToLower();

            // Get name if not set
            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                userMemory["name"] = userName;
                return $"Nice to meet you, {userName}! What cybersecurity topic would you like to learn about?";
            }

            // Greeting responses
            if (lowerInput.Contains("hello") || lowerInput.Contains("hi") || lowerInput.Contains("hey"))
            {
                return $"Hello again, {userName}! How can I help you today?";
            }

            // Thank you response
            if (lowerInput.Contains("thank"))
            {
                return $"You're welcome, {userName}! Is there anything else you'd like to learn?";
            }

            // Tell me more - conversation flow
            if (lowerInput.Contains("tell me more") || lowerInput.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(currentTopic) && responses.ContainsKey(currentTopic))
                {
                    return $"Here's another tip: {GetRandomResponse(currentTopic)}";
                }
                return "Sure! What topic would you like to learn more about?";
            }

            // Keyword recognition
            foreach (var keyword in responses.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    currentTopic = keyword;
                    return GetRandomResponse(keyword);
                }
            }

            // Default response
            return "I'm not sure I understand. You can ask me about: password safety, phishing scams, or privacy protection.";
        }

        private void AddUserMessage(string message)
        {
            lstChat.Items.Add($"[{DateTime.Now:HH:mm}] You: {message}");
            conversationHistory.Add($"You: {message}");
        }

        private void AddBotMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                lstChat.Items.Add($"[{DateTime.Now:HH:mm}] Bot: {message}");
                conversationHistory.Add($"Bot: {message}");
            }
            else
            {
                lstChat.Items.Add("");
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
            lstChat.TopIndex = lstChat.Items.Count - 1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstChat.Items.Clear();
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