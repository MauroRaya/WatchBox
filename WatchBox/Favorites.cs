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
using System.Security.Policy;
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
            tbSearchTitle.KeyPress += new KeyPressEventHandler(tbSearchTitle_KeyPress);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void displayFavorites()
        {
            flowLayoutPanel.Controls.Clear();

            foreach (var title in Data.favorites)
            {
                JObject favoriteData = await Search.fetchMovie(title);

                MovieControl movieControl = new MovieControl();
                movieControl.Title  = title;
                movieControl.Rating = favoriteData["imdbRating"].ToString() + "/10";
                movieControl.IsFavorite = true;

                string posterUrl = favoriteData["Poster"].ToString();
                if (posterUrl != null)
                {
                    using (WebClient wc = new WebClient())
                    {
                        try
                        {
                            byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(posterUrl));
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                movieControl.Poster = new Bitmap(ms);
                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            MessageBox.Show($"Request error: {ex.Message}");
                        }
                    }
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

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            Data.selectedMovieData = await Search.fetchMovie(tbSearchTitle.Text);

            if (Data.selectedMovieData == null)
            {
                return;
            }

            if (Data.selectedMovieData["Response"].ToString() == "False")
            {
                MessageBox.Show("Movie not found. Make sure to type the title correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SearchedMovie moviePage = new SearchedMovie();
            moviePage.Show();
            this.Hide();
        }

        private void tbSearchTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }
    }
}
