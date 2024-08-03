using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchBox.DAL
{
    internal class RegisterDAL
    {
        public static bool userExists(string username)
        {
            Error.setError(false);
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "SELECT id FROM users WHERE username = @username";

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        Error.setError("Username already in use. Try again.");
                        return true;
                    }

                    return false;
                }
            }
            catch (SQLiteException)
            {
                Error.setError("Unexpected error trying to find user.");
                return false;
            }
            catch (Exception)
            {
                Error.setError("Unexpected error trying to find user.");
                return false;
            }
        }

        public static string getUserId(string username)
        {
            Error.setError(false);
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "SELECT id FROM users WHERE username = @username";

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        Error.setError("Unable to find user. Try again or create an account.");
                        return null;
                    }

                    return result.ToString();
                }
            }
            catch (SQLiteException)
            {
                Error.setError("Unexpected error trying to find user.");
                return null;
            }
            catch (Exception)
            {
                Error.setError("Unexpected error trying to find user.");
                return null;
            }
        }

        public static void createUser(string username, string hash, string salt)
        {
            Error.setError(false);
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "INSERT INTO users (username, hash, salt) VALUES (@username, @hash, @salt)";

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@hash", hash);
                    command.Parameters.AddWithValue("@salt", salt);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                Error.setError("SQLite error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Error.setError("Unexpected error: " + ex.Message);
            }
        }
    }
}
