using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchBox
{
    public partial class MovieControl : UserControl
    {
        private string favoriteState = "notFavorite";

        public MovieControl()
        {
            InitializeComponent();
        }

        public Image Poster
        {
            get { return pbPoster.Image;  }
            set { pbPoster.Image = value; }
        }

        public String Name
        {
            get { return lbName.Text;  }
            set { lbName.Text = value; }
        }

        public String Rating
        {
            get { return lbRating.Text;  }
            set { lbRating.Text = value; }
        }

        private void pbPoster_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lbName.Text);
        }

        private void pbFavorite_Click(object sender, EventArgs e)
        {
            changeStar();

            if (favoriteState == "favorite")
            {
                addToFavorites();
            }
            else
            {
                removeFromFavorites();
                ((Favorites)this.FindForm()).removeFavorite(lbName.Text);
            }

            //string message = favoriteState == "favorite" ? "Saved to your favorites!" : "Removed from your favorites";
            //MessageBox.Show(message);
        }

        public void changeStar()
        {
            string favoritePath    = @"C:\Users\Mauro\Desktop\WatchBox\imgs\favorite_icon.png";
            string notFavoritePath = @"C:\Users\Mauro\Desktop\WatchBox\imgs\not_favorite_icon.png";

            favoriteState = favoriteState    == "notFavorite" ? "favorite" : "notFavorite";
            string imagePath = favoriteState == "notFavorite" ? notFavoritePath : favoritePath;

            try
            {
                if (System.IO.File.Exists(imagePath))
                {
                    using (Image image = Image.FromFile(imagePath))
                    {
                        pbFavorite.Image    = new Bitmap(image);
                        pbFavorite.SizeMode = PictureBoxSizeMode.Zoom;
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

        private void addToFavorites()
        {
            string movieTitle = lbName.Text;
            Image posterImage = pbPoster.Image;

            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                posterImage.Save(ms, posterImage.RawFormat);
                imageBytes = ms.ToArray();
            }

            string base64Image = Convert.ToBase64String(imageBytes);

            Dictionary<string, string> movieInfo = new Dictionary<string, string>
            {
                { "Title", movieTitle },
                { "Poster", base64Image }
            };

            Data.favorites.Add(movieInfo);
        }

        private void removeFromFavorites()
        {
            string movieTitle = lbName.Text;

            var movieToRemove = Data.favorites.FirstOrDefault(movie => movie["Title"] == movieTitle);
            if (movieToRemove != null)
            {
                Data.favorites.Remove(movieToRemove);
            }
        }

    }
}
