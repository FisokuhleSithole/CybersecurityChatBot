using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class QuizGame
    {
        private List<QuizQuestion> questions;
        private int currentQuestionIndex;
        private int score;
        private bool isActive;
        private int totalQuestions;

        public bool IsActive => isActive;

        public QuizGame()
        {
            InitializeQuestions();
            currentQuestionIndex = 0;
            score = 0;
            isActive = false;
            totalQuestions = 10; // We'll ask 10 random questions
        }

        private void InitializeQuestions()
        {
            questions = new List<QuizQuestion>
            {
                // Question 1
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report it as phishing", "Ignore it" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Reporting phishing emails helps prevent scams and protects others from falling victim."
                },
                // Question 2
                new QuizQuestion
                {
                    Question = "Which of these is a strong password?",
                    Options = new List<string> { "password123", "MyBirthday1990", "K9#mP2$vL8@qR", "12345678" },
                    CorrectAnswerIndex = 2,
                    Explanation = "A strong password uses uppercase, lowercase, numbers, and symbols - like K9#mP2$vL8@qR."
                },
                // Question 3
                new QuizQuestion
                {
                    Question = "What does 'https' in a website URL indicate?",
                    Options = new List<string> { "The site is secure", "The site is slow", "The site is free", "The site is foreign" },
                    CorrectAnswerIndex = 0,
                    Explanation = "HTTPS indicates the connection is encrypted and secure."
                },
                // Question 4
                new QuizQuestion
                {
                    Question = "What is social engineering?",
                    Options = new List<string> { "Building apps", "Manipulating people to reveal information", "Engineering social media", "A type of software" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Social engineering manipulates people into giving away confidential information."
                },
                // Question 5
                new QuizQuestion
                {
                    Question = "True or False: It's safe to use the same password for multiple accounts.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Using the same password across accounts puts all your accounts at risk if one is compromised."
                },
                // Question 6
                new QuizQuestion
                {
                    Question = "What is two-factor authentication (2FA)?",
                    Options = new List<string> { "A type of password", "An extra layer of security", "A virus", "A social media feature" },
                    CorrectAnswerIndex = 1,
                    Explanation = "2FA adds an extra layer of security by requiring a second verification method."
                },
                // Question 7
                new QuizQuestion
                {
                    Question = "True or False: Public Wi-Fi is completely safe for banking.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Public Wi-Fi is unsecured and can be intercepted - never use it for banking."
                },
                // Question 8
                new QuizQuestion
                {
                    Question = "What type of software is designed to harm your computer?",
                    Options = new List<string> { "Antivirus", "Malware", "Firewall", "Encryption" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Malware is malicious software designed to damage or disable computers."
                },
                // Question 9
                new QuizQuestion
                {
                    Question = "What should you do if you receive a suspicious link?",
                    Options = new List<string> { "Click it to see what happens", "Forward it to friends", "Ignore and delete it", "Download the attachment" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Never click suspicious links - they may lead to phishing sites or malware downloads."
                },
                // Question 10
                new QuizQuestion
                {
                    Question = "True or False: You should keep your software updated.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 0,
                    Explanation = "Software updates often include security patches that fix known vulnerabilities."
                },
                // Question 11
                new QuizQuestion
                {
                    Question = "Which is a sign of a phishing email?",
                    Options = new List<string> { "Personalized greeting", "Urgent request for information", "Known sender", "Clear subject line" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Phishing emails often create urgency to pressure you into acting quickly without thinking."
                },
                // Question 12
                new QuizQuestion
                {
                    Question = "What is a firewall?",
                    Options = new List<string> { "A physical wall", "A network security device", "A type of virus", "A social media tool" },
                    CorrectAnswerIndex = 1,
                    Explanation = "A firewall monitors and controls network traffic to prevent unauthorized access."
                }
            };
        }

        public string StartQuiz()
        {
            isActive = true;
            currentQuestionIndex = 0;
            score = 0;
            return " Welcome to the Cybersecurity Quiz! You'll be asked 10 random questions. Type A, B, C, or D to answer.\n\n" + GetCurrentQuestion();
        }

        public string GetCurrentQuestion()
        {
            if (!isActive || currentQuestionIndex >= totalQuestions)
                return null;

            var q = questions[currentQuestionIndex];
            string options = "";
            for (int i = 0; i < q.Options.Count; i++)
            {
                options += $"\n{(char)('A' + i)}) {q.Options[i]}";
            }
            return $"Question {currentQuestionIndex + 1}/{totalQuestions}: {q.Question}{options}";
        }

        public string SubmitAnswer(string answer)
        {
            if (!isActive || currentQuestionIndex >= totalQuestions)
                return "The quiz has ended. Type 'start quiz' to try again!";

            var q = questions[currentQuestionIndex];
            int selectedIndex = -1;

            // Parse answer input
            string cleanAnswer = answer.Trim().ToUpper();
            if (cleanAnswer.Length == 1 && cleanAnswer[0] >= 'A' && cleanAnswer[0] <= 'D')
            {
                selectedIndex = cleanAnswer[0] - 'A';
            }
            else if (int.TryParse(cleanAnswer, out int num) && num >= 1 && num <= q.Options.Count)
            {
                selectedIndex = num - 1;
            }
            else
            {
                return "Please answer with A, B, C, or D (or 1, 2, 3, 4)";
            }

            if (selectedIndex >= q.Options.Count)
                return "Invalid option. Please choose A, B, C, or D.";

            bool correct = selectedIndex == q.CorrectAnswerIndex;
            if (correct) score++;

            string response = correct ? " Correct! " : " Incorrect. ";
            response += q.Explanation + "\n\n";

            currentQuestionIndex++;

            if (currentQuestionIndex >= totalQuestions)
            {
                response += EndQuiz();
            }
            else
            {
                response += $"Your score: {score}/{totalQuestions}\n\n" + GetCurrentQuestion();
            }

            return response;
        }

        private string EndQuiz()
        {
            isActive = false;
            string feedback;
            if (score >= 9)
                feedback = " Excellent! You're a cybersecurity pro!";
            else if (score >= 7)
                feedback = " Good job! Keep learning to stay safe!";
            else if (score >= 5)
                feedback = " Not bad! Review the topics you missed to improve.";
            else
                feedback = " Keep practicing! Cybersecurity is important for everyone.";

            return $"Quiz complete! Your final score: {score}/{totalQuestions}\n{feedback}\n\nType 'start quiz' to play again!";
        }

        public void ForceEnd()
        {
            isActive = false;
        }

        public string GetScore()
        {
            return isActive ? $"Current score: {score}/{totalQuestions}" : $"Final score: {score}/{totalQuestions}";
        }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }
    }
}