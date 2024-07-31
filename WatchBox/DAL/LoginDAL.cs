using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WatchBox.DAL
{
    static class LoginDAL
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

                    if (result == null)
                    {
                        Error.setError("Unable to find user. Try again or create an account.");
                        return false;
                    }

                    return true;
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

        public static string getHash(string username)
        {
            Error.setError(false);
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "SELECT hash FROM users WHERE username = @username";

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

        public static string getSalt(string username)
        {
            Error.setError(false);
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "SELECT salt FROM users WHERE username = @username";

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
    }
}
