using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WatchBox
{
    public static class Favorite
    {
        public static void addToFavorites(MovieControl movieControl)
        {
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                movieControl.Poster.Save(ms, movieControl.Poster.RawFormat);
                imageBytes = ms.ToArray();
            }

            string base64Image = Convert.ToBase64String(imageBytes);

            Dictionary<string, string> movieInfo = new Dictionary<string, string>
            {
                { "Title",  movieControl.Title },
                { "Poster", base64Image }
            };

            Data.favorites.Add(movieInfo);
            movieControl.IsFavorite = true;
            changeStar(movieControl);
        }

        public static void addToFavorites(PictureBox poster, string movieTitle)
        {
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                poster.Image.Save(ms, poster.Image.RawFormat);
                imageBytes = ms.ToArray();
            }

            string base64Image = Convert.ToBase64String(imageBytes);

            Dictionary<string, string> movieInfo = new Dictionary<string, string>
            {
                { "Title",  movieTitle },
                { "Poster", base64Image }
            };

            Data.favorites.Add(movieInfo);
        }

        public static void removeFromFavorites(MovieControl movieControl)
        {
            var movieToRemove = Data.favorites.FirstOrDefault(movie => movie["Title"] == movieControl.Title);

            if (movieToRemove != null)
            {
                Data.favorites.Remove(movieToRemove);
                movieControl.IsFavorite = false;
                changeStar(movieControl);
            }
        }

        public static void removeFromFavorites(string movieTitle)
        {
            var movieToRemove = Data.favorites.FirstOrDefault(movie => movie["Title"] == movieTitle);

            if (movieToRemove != null)
            {
                Data.favorites.Remove(movieToRemove);
            }
        }

        public static void changeStar(MovieControl movieControl)
        {
            string favoritePath    = @"C:\Users\Mauro\Desktop\WatchBox\imgs\favorite_icon.png";
            string notFavoritePath = @"C:\Users\Mauro\Desktop\WatchBox\imgs\not_favorite_icon.png";

            string imagePath = movieControl.IsFavorite ? favoritePath : notFavoritePath;

            try
            {
                if (System.IO.File.Exists(imagePath))
                {
                    using (Image image = Image.FromFile(imagePath))
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
            string favoritePath    = @"C:\Users\Mauro\Desktop\WatchBox\imgs\favorite_icon.png";
            string notFavoritePath = @"C:\Users\Mauro\Desktop\WatchBox\imgs\not_favorite_icon.png";

            bool isFavorite  = Data.favorites.Any(dict => dict["Title"] == movieTitle);
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
