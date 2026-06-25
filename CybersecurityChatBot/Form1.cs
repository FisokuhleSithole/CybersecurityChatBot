using CybersecurityChatBot;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CybersecurityChatBot
{
    public partial class Form1 : Form  // ← MUST have : Form
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

        // Part 3: Database, Quiz, Activity Log
        private DatabaseHelper dbHelper;
        private QuizGame quizGame;
        private List<ActivityLogEntry> activityLog;

        // Constructor - THIS IS CRITICAL!
        public Form1()
        {
            InitializeComponent();  // ← MUST call this!
            InitializeResponses();
            InitializePart3();
            PlayVoiceGreeting();
            DisplayWelcomeMessage();

            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializePart3()
        {
            dbHelper = new DatabaseHelper();
            quizGame = new QuizGame();
            activityLog = new List<ActivityLogEntry>();

            // Test database connection
            if (dbHelper.TestConnection())
            {
                AddBotMessage(" Database connection successful!");
            }
            else
            {
                AddBotMessage(" Database connection failed. Check your connection string.");
            }

            LogActivity("Application Started", "Cybersecurity Chatbot launched.");
        }

        private void LogActivity(string actionType, string description)
        {
            var entry = new ActivityLogEntry
            {
                ActionType = actionType,
                Description = description,
                Timestamp = DateTime.Now
            };
            activityLog.Add(entry);
            if (activityLog.Count > 50) activityLog.RemoveAt(0);
            dbHelper?.AddActivityLog(actionType, description);
        }

        private void InitializeResponses()
        {
            responses = new Dictionary<string, List<string>>();

            responses.Add("password", new List<string>
            {
                " Use strong passwords with at least 12 characters, including uppercase, lowercase, numbers, and symbols!",
                " Never reuse passwords across different accounts. Each account needs its own unique password!",
                " Consider using a password manager like Bitwarden or LastPass!",
                " Avoid using personal information like birthdays or pet names in your passwords!"
            });

            responses.Add("phishing", new List<string>
            {
                " Phishing emails often create urgency. Always verify the sender's email address before clicking links!",
                " Never download attachments from unknown senders. They may contain malware!",
                " Look for spelling and grammar mistakes – common signs of phishing!",
                " Hover over links to see the real URL before clicking!"
            });

            responses.Add("privacy", new List<string>
            {
                " Review your privacy settings on social media regularly. Limit what the public can see!",
                " Be careful what personal information you share online – once it's out, it's hard to remove!",
                " Use two-factor authentication (2FA) on all accounts that offer it!",
                " Regularly check which apps have access to your data and remove ones you don't use!"
            });

            responses.Add("malware", new List<string>
            {
                " Malware is malicious software designed to harm your computer. Always download from official sources!",
                " Keep your antivirus software updated and run regular scans!",
                " Never click on pop-up ads claiming your computer is infected – they're often scams!",
                " Don't insert unknown USB drives into your computer – they can contain automatic malware!"
            });
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                AudioPlayer.PlayGreeting("greeting.wav");
            }
            catch
            {
                AddBotMessage("(Voice greeting could not be played)");
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
            AddBotMessage("  • malware - How to avoid malicious software");
            AddBotMessage("");
            AddBotMessage(" NEW FEATURES:");
            AddBotMessage("  • 'add task' - Manage cybersecurity tasks");
            AddBotMessage("  • 'show tasks' - View your tasks");
            AddBotMessage("  • 'start quiz' - Test your cybersecurity knowledge");
            AddBotMessage("  • 'show log' - View recent activity");
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
            return "I have information on that topic. Please ask me about: password, phishing, privacy, or malware.";
        }

        private string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("anxious") ||
                input.Contains("nervous") || input.Contains("concerned"))
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
                        return $"It's completely understandable to feel worried about {detectedTopic}. Let me share some tips:\n\n{GetRandomResponse(detectedTopic)}";
                    }
                    return "It's normal to feel worried about online threats. Would you like me to share some general safety tips?";
                case "curious":
                    if (!string.IsNullOrEmpty(detectedTopic))
                    {
                        return $"That's great that you're curious about {detectedTopic}! Here's what you should know:\n\n{GetRandomResponse(detectedTopic)}";
                    }
                    return "That's great! Curiosity helps you learn. What topic would you like to explore?";
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
            string lowerInput = input.ToLower().Trim();

            // Check for quit
            if (lowerInput == "quit" || lowerInput == "exit" || lowerInput == "goodbye")
            {
                Application.Exit();
                return "";
            }

            // PART 3: Activity Log
            if (lowerInput.Contains("show log") || lowerInput.Contains("what have you done") ||
                lowerInput.Contains("activity log") || lowerInput.Contains("recent actions"))
            {
                LogActivity("Activity Log Viewed", "User requested to view activity log.");
                return GetActivityLogDisplay();
            }

            // PART 3: Quiz
            if (lowerInput.Contains("start quiz") || lowerInput.Contains("play quiz") ||
                lowerInput.Contains("start game") || lowerInput.Contains("quiz"))
            {
                if (!quizGame.IsActive)
                {
                    LogActivity("Quiz Started", "User started the cybersecurity quiz.");
                    return quizGame.StartQuiz();
                }
                else
                {
                    return "You're already playing the quiz! Submit your answer.";
                }
            }

            // PART 3: Handle quiz answer
            if (quizGame.IsActive)
            {
                string result = quizGame.SubmitAnswer(input);
                LogActivity("Quiz Attempt", $"User answered a quiz question.");
                return result;
            }

            // PART 3: Add Task
            if (lowerInput.Contains("add task") || lowerInput.Contains("new task") ||
                lowerInput.Contains("create task") || lowerInput.Contains("add reminder"))
            {
                return HandleAddTask(input);
            }

            // PART 3: Show tasks
            if (lowerInput.Contains("show tasks") || lowerInput.Contains("view tasks") ||
                lowerInput.Contains("list tasks") || lowerInput.Contains("my tasks"))
            {
                return GetTasksDisplay();
            }

            // PART 3: Complete task
            if (lowerInput.Contains("complete task") || lowerInput.Contains("task done") ||
                lowerInput.Contains("finish task"))
            {
                return HandleCompleteTask(input);
            }

            // PART 3: Delete task
            if (lowerInput.Contains("delete task") || lowerInput.Contains("remove task"))
            {
                return HandleDeleteTask(input);
            }

            // PART 3: NLP - Reminder
            if (lowerInput.Contains("remind me") || lowerInput.Contains("remind to") ||
                lowerInput.Contains("set reminder") || lowerInput.Contains("remember to"))
            {
                return HandleAddTask(input);
            }

            // Check for name
            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                userMemory["name"] = userName;
                LogActivity("User Named", $"User set name to {userName}");
                return $"Nice to meet you, {userName}! What cybersecurity topic would you like to learn about?\n\nOr try the new features: add task, start quiz, or show tasks!";
            }

            // Sentiment detection
            string sentiment = DetectSentiment(lowerInput);
            if (sentiment != "neutral")
            {
                string sentimentResponse = HandleSentiment(sentiment, lowerInput);
                if (!string.IsNullOrEmpty(sentimentResponse))
                {
                    LogActivity("Sentiment Detected", $"User expressed {sentiment} sentiment.");
                    return sentimentResponse;
                }
            }

            // Conversation flow
            if (lowerInput.Contains("tell me more") || lowerInput.Contains("another tip") ||
                lowerInput.Contains("more info") || lowerInput.Contains("explain more"))
            {
                if (!string.IsNullOrEmpty(currentTopic) && responses.ContainsKey(currentTopic))
                {
                    LogActivity("More Info", $"User requested more info on {currentTopic}");
                    return $"Here's another tip about {currentTopic}:\n\n{GetRandomResponse(currentTopic)}";
                }
                return "Sure! What topic would you like to learn more about? (password, phishing, privacy, or malware)";
            }

            // Thank you
            if (lowerInput.Contains("thank"))
            {
                return $"You're welcome, {userName}! I'm glad I could help. Is there anything else you'd like to learn about?";
            }

            // Greeting
            if (lowerInput == "hello" || lowerInput == "hi" || lowerInput == "hey")
            {
                return $"Hello again, {userName}! How can I help you today?";
            }

            // Keyword recognition
            foreach (var keyword in responses.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    currentTopic = keyword;
                    LogActivity("Topic Discussed", $"User asked about {keyword}");
                    return GetRandomResponse(keyword);
                }
            }

            // Default response
            return "I'm not sure I understand. You can ask me about:\n• password - Password safety tips\n• phishing - How to spot scams\n• privacy - Protecting your personal information\n• malware - How to avoid malicious software\n\nOr try new features:\n• add task - Manage cybersecurity tasks\n• start quiz - Test your knowledge\n• show log - View recent activity";
        }

        // PART 3: Handle Add Task
        private string HandleAddTask(string input)
        {
            string taskContent = input;
            foreach (var prefix in new[] { "add task", "new task", "create task", "add reminder", "remind me to" })
            {
                if (input.ToLower().Contains(prefix))
                {
                    int index = input.ToLower().IndexOf(prefix) + prefix.Length;
                    taskContent = input.Substring(Math.Min(index, input.Length)).Trim();
                    break;
                }
            }

            if (string.IsNullOrEmpty(taskContent) || taskContent.Length < 3)
            {
                return "What task would you like to add? Please describe it, e.g., 'Enable two-factor authentication'";
            }

            string title = taskContent.Length > 50 ? taskContent.Substring(0, 47) + "..." : taskContent;
            string description = taskContent;

            DateTime? reminderDate = null;
            if (input.ToLower().Contains("remind") || input.ToLower().Contains("tomorrow") ||
                input.ToLower().Contains("days") || input.ToLower().Contains("weeks"))
            {
                var words = input.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].ToLower() == "in" && i + 2 < words.Length && words[i + 2].ToLower() == "days")
                    {
                        if (int.TryParse(words[i + 1], out int days))
                        {
                            reminderDate = DateTime.Now.AddDays(days);
                        }
                    }
                    else if (words[i].ToLower() == "tomorrow")
                    {
                        reminderDate = DateTime.Now.AddDays(1);
                    }
                }
            }

            bool success = dbHelper.AddTask(title, description, reminderDate);
            if (success)
            {
                LogActivity("Task Added", $"Task: {title}");
                string reminderMsg = reminderDate.HasValue ? $" I'll remind you on {reminderDate.Value:yyyy-MM-dd}." : "";
                return $" Task added: '{title}'{reminderMsg}\n\nType 'show tasks' to view your tasks.";
            }
            return " I couldn't add that task. Please try again or check if the database is connected.";
        }

        private string GetTasksDisplay()
        {
            var tasks = dbHelper.GetTasks(false);
            if (tasks.Count == 0)
                return " You have no pending tasks. Add one with 'add task'!";

            string result = " YOUR TASKS:\n\n";
            for (int i = 0; i < tasks.Count; i++)
            {
                result += $"{i + 1}. {tasks[i].Title}";
                if (!string.IsNullOrEmpty(tasks[i].Description))
                    result += $"\n   Description: {tasks[i].Description}";
                if (tasks[i].ReminderDate.HasValue)
                    result += $"\n   Reminder: {tasks[i].ReminderDate.Value:yyyy-MM-dd}";
                result += "\n\n";
            }
            result += "Type 'complete task 1' to mark as done, or 'delete task 1' to remove.";
            return result;
        }

        private string HandleCompleteTask(string input)
        {
            var tasks = dbHelper.GetTasks(false);
            if (tasks.Count == 0)
                return "You have no pending tasks to complete.";

            int taskId = ExtractTaskId(input);
            if (taskId < 1 || taskId > tasks.Count)
                return $"Please specify a valid task number (1-{tasks.Count}). Type 'show tasks' to see them.";

            var task = tasks[taskId - 1];
            bool success = dbHelper.CompleteTask(task.Id);
            if (success)
            {
                LogActivity("Task Completed", $"Task: {task.Title}");
                return $" Task '{task.Title}' marked as completed!";
            }
            return " Could not complete the task. Please try again.";
        }

        private string HandleDeleteTask(string input)
        {
            var tasks = dbHelper.GetTasks(true);
            if (tasks.Count == 0)
                return "You have no tasks to delete.";

            int taskId = ExtractTaskId(input);
            if (taskId < 1 || taskId > tasks.Count)
                return $"Please specify a valid task number (1-{tasks.Count}). Type 'show tasks' to see them.";

            var task = tasks[taskId - 1];
            bool success = dbHelper.DeleteTask(task.Id);
            if (success)
            {
                LogActivity("Task Deleted", $"Task: {task.Title}");
                return $" Task '{task.Title}' deleted!";
            }
            return " Could not delete the task. Please try again.";
        }

        private int ExtractTaskId(string input)
        {
            var words = input.Split(' ');
            foreach (var word in words)
            {
                if (int.TryParse(word, out int num))
                    return num;
            }
            return -1;
        }

        private string GetActivityLogDisplay()
        {
            var entries = activityLog.Count > 0 ? activityLog : dbHelper.GetActivityLog(10);

            if (entries.Count == 0)
                return " No activity logged yet.";

            string result = " RECENT ACTIVITY LOG:\n\n";
            int count = 0;
            for (int i = Math.Min(entries.Count - 1, 9); i >= 0 && count < 10; i--)
            {
                var entry = entries[i];
                result += $"{count + 1}. [{entry.Timestamp:HH:mm}] {entry.ActionType}: {entry.Description}\n";
                count++;
            }

            if (entries.Count > 10)
                result += $"\n(Showing last 10 of {entries.Count} entries)";

            LogActivity("Log Viewed", "User reviewed activity log.");
            return result;
        }

        // ======================================================
        // CHAT DISPLAY METHODS (These match the Designer controls)
        // ======================================================

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

        // ======================================================
        // EVENT HANDLERS (These match the Designer events)
        // ======================================================

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
            if (!string.IsNullOrEmpty(response))
                AddBotMessage(response);

            txtUserInput.Clear();
            txtUserInput.Focus();
            txtChat.SelectionStart = txtChat.Text.Length;
            txtChat.ScrollToCaret();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtChat.Clear();
            LogActivity("Chat Cleared", "User cleared the chat.");
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