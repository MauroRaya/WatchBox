using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WatchBox
{
    public partial class Favorites : Form
    {
        public Favorites()
        {
            InitializeComponent();
            displayFavorites();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void displayFavorites() //fix ratings and data
        {
            flowLayoutPanel.Controls.Clear();

            foreach (var movie in Data.favorites)
            {
                MovieControl movieControl = new MovieControl();

                movieControl.Title = movie["Title"];
                movieControl.IsFavorite = true;

                byte[] imageBytes = Convert.FromBase64String(movie["Poster"]);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    movieControl.Poster = Image.FromStream(ms);
                }

                Favorite.changeStar(movieControl);
                flowLayoutPanel.Controls.Add(movieControl);
            }
        }

        public static void removeFavoriteUI(MovieControl movieControl)
        {
            FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)movieControl.Parent;
            flowLayoutPanel.Controls.Remove(movieControl);
            movieControl.Dispose();
        }


        private void btnMovies_Click_1(object sender, EventArgs e)
        {
            Movies moviesPage = new Movies();
            moviesPage.Show();
            this.Hide();
        }

        private void btnShows_Click(object sender, EventArgs e)
        {
            TvShows showsPage = new TvShows();
            showsPage.Show();
            this.Hide();
        }
    }
}
