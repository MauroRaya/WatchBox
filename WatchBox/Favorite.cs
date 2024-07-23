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
            Data.favorites.Add(movieControl.Title);
            movieControl.IsFavorite = true;
            changeStar(movieControl);
        }

        public static void addToFavorites(string movieTitle)
        {
            Data.favorites.Add(movieTitle);
        }

        public static void removeFromFavorites(MovieControl movieControl)
        {
            string movieToRemove = Data.favorites.FirstOrDefault(title => title == movieControl.Title);

            if (movieToRemove != null)
            {
                Data.favorites.Remove(movieToRemove);
                movieControl.IsFavorite = false;
                changeStar(movieControl);
            }
        }

        public static void removeFromFavorites(string movieTitle)
        {
            var movieToRemove = Data.favorites.FirstOrDefault(title => title == movieTitle);

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

            bool isFavorite  = Data.favorites.Any(title => title == movieTitle);
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
