using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CybersecurityChatBot
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            connectionString = "Server=localhost;Database=cybersecurity_chatbot;Uid=root;Pwd=Youlovedoingthis.0099;";
        }

        // Add a new task
        public bool AddTask(string title, string description, DateTime? reminderDate)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO tasks (title, description, reminder_date) VALUES (@title, @desc, @reminder)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@desc", description);
                        cmd.Parameters.AddWithValue("@reminder", (object)reminderDate ?? DBNull.Value);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
                return false;
            }
        }

        // Get all tasks
        public List<Task> GetTasks(bool includeCompleted = false)
        {
            List<Task> tasks = new List<Task>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM tasks";
                    if (!includeCompleted)
                        query += " WHERE is_completed = FALSE";
                    query += " ORDER BY reminder_date IS NULL, reminder_date ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new Task
                            {
                                Id = reader.GetInt32("id"),
                                Title = reader.GetString("title"),
                                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                                ReminderDate = reader.IsDBNull(reader.GetOrdinal("reminder_date")) ? null : reader.GetDateTime("reminder_date"),
                                IsCompleted = reader.GetBoolean("is_completed"),
                                CreatedAt = reader.GetDateTime("created_at")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
            }
            return tasks;
        }

        // Mark task as completed
        public bool CompleteTask(int taskId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE tasks SET is_completed = TRUE WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", taskId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
                return false;
            }
        }

        // Delete task
        public bool DeleteTask(int taskId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM tasks WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", taskId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
                return false;
            }
        }

        // Add activity log entry
        public bool AddActivityLog(string actionType, string description)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO activity_log (action_type, description) VALUES (@type, @desc)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@type", actionType);
                        cmd.Parameters.AddWithValue("@desc", description);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
                return false;
            }
        }

        // Get recent activity log entries
        public List<ActivityLogEntry> GetActivityLog(int limit = 10)
        {
            List<ActivityLogEntry> entries = new List<ActivityLogEntry>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM activity_log ORDER BY timestamp DESC LIMIT @limit";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@limit", limit);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                entries.Add(new ActivityLogEntry
                                {
                                    Id = reader.GetInt32("id"),
                                    ActionType = reader.GetString("action_type"),
                                    Description = reader.GetString("description"),
                                    Timestamp = reader.GetDateTime("timestamp")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
            }
            return entries;
        }

        public bool TestConnection()
{
    try
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            return true;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Connection Error: {ex.Message}");
        return false;
    }
}
    }

    // Task class
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "[✓] " : "[ ] ";
            string reminder = ReminderDate.HasValue ? $" (Reminder: {ReminderDate.Value:yyyy-MM-dd})" : "";
            return $"{status}{Title}{reminder}";
        }
    }

    // Activity Log Entry class
    public class ActivityLogEntry
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}