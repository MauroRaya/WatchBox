using CrystalDecisions.Shared.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WatchBox
{
    public static class Favorites
    {
        public static List<string> getFavorites()
        {
            Error.setError(false);
            string connectionString = "Data Source=watchbox.db;Version=3;";
            List<string> favoritesList = new List<string>();

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "SELECT title FROM favorites WHERE user_id = @userId";

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@userId", Data.userId);

                    connection.Open();

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            favoritesList.Add(reader.GetString(reader.GetOrdinal("title")));
                        }
                    }
                }
            }
            catch (SQLiteException)
            {
                Error.setError("Unexpected error trying to retrieve favorites.");
                return null;
            }
            catch (Exception)
            {
                Error.setError("Unexpected error trying to retrieve favorites.");
                return null;
            }

            return favoritesList;
        }


        public static void addToFavorites(MovieControl movieControl)
        {
            Error.setError(false);
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "INSERT INTO favorites (title, user_id) VALUES (@title, @userId)";

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@title",  movieControl.Title);
                    command.Parameters.AddWithValue("@userId", Data.userId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException)
            {
                Error.setError("Unexpected error trying to add movie to favorites.");
                return;
            }
            catch (Exception)
            {
                Error.setError("Unexpected error trying to add movie to favorites.");
                return;
            }

            movieControl.IsFavorite = true;
            changeStar(movieControl);
        }

        public static void addToFavorites(string movieTitle)
        {
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "INSERT INTO favorites (title, user_id) VALUES (@title, @userId)";

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@title",  movieTitle);
                    command.Parameters.AddWithValue("@userId", Data.userId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException)
            {
                Error.setError("Unexpected error trying to add movie to favorites.");
                return;
            }
            catch (Exception)
            {
                Error.setError("Unexpected error trying to add movie to favorites.");
                return;
            }
        }

        public static void removeFromFavorites(MovieControl movieControl)
        {
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "DELETE FROM favorites WHERE title = @title AND user_id = @userId";

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@title",  movieControl.Title);
                    command.Parameters.AddWithValue("@userId", Data.userId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException)
            {
                Error.setError("Unexpected error trying to remove movie from favorites.");
                return;
            }
            catch (Exception)
            {
                Error.setError("Unexpected error trying to remove movie from favorites.");
                return;
            }

            movieControl.IsFavorite = false;
            changeStar(movieControl);
        }

        public static void removeFromFavorites(string movieTitle)
        {
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "DELETE FROM favorites WHERE title = @title AND user_id = @userId";

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@title",  movieTitle);
                    command.Parameters.AddWithValue("@userId", Data.userId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException)
            {
                Error.setError("Unexpected error trying to remove movie from favorites.");
                return;
            }
            catch (Exception)
            {
                Error.setError("Unexpected error trying to remove movie from favorites.");
                return;
            }
        }

        public static void changeStar(MovieControl movieControl)
        {
            string favoritePath    = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\favorite_icon.png");
            string notFavoritePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\not_favorite_icon.png");

            string imagePath = movieControl.IsFavorite ? favoritePath : notFavoritePath;

            try
            {
                if (System.IO.File.Exists(imagePath))
                {
                    using (System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath))
                    {
                        movieControl.FavoriteButton = new Bitmap(image);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found: " + imagePath);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("ArgumentException: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        public static Bitmap changeStar(string movieTitle, string operation)
        {
            string favoritePath    = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\favorite_icon.png");
            string notFavoritePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\not_favorite_icon.png");

            bool isFavorite = false;
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM favorites WHERE title = @title AND user_id = @userId";

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@title", movieTitle);
                    command.Parameters.AddWithValue("@userId", Data.userId);

                    connection.Open();
                    isFavorite = Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
            catch (SQLiteException)
            {
                MessageBox.Show("Unexpected error checking favorite status.");
                return null;
            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error checking favorite status.");
                return null;
            }

            string imagePath = null;

            if (operation == "load")
            {
                imagePath = isFavorite ? favoritePath : notFavoritePath;
            }
            else if (operation == "change")
            {
                imagePath = isFavorite ? notFavoritePath : favoritePath;
            }

            try
            {
                if (System.IO.File.Exists(imagePath))
                {
                    return new Bitmap(imagePath);
                }
                else
                {
                    MessageBox.Show("Image file not found: " + imagePath);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("ArgumentException: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }

            return null;
        }
    }
}
