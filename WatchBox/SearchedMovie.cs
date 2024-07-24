using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class SearchedMovie : Form
    {
        public SearchedMovie()
        {
            InitializeComponent();
            displayMovie();
            tbSearchTitle.KeyPress += new KeyPressEventHandler(tbSearchTitle_KeyPress);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void displayMovie()
        {
            string posterUrl = Data.selectedMovieData["Poster"].ToString();

            using (var webClient = new HttpClient())
            {
                try
                {
                    byte[] imageBytes = await webClient.GetByteArrayAsync(posterUrl);
                    pbPoster.Image = Image.FromStream(new System.IO.MemoryStream(imageBytes));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Request error: {ex.Message}");
                }
            }

            Bitmap starImage = Favorite.changeStar(Data.selectedMovieData["Title"].ToString(), "load");
            pbFavorite.Image = starImage;

            lbTitle.Text  = Data.selectedMovieData["Title"].ToString();
            lbRating.Text = Data.selectedMovieData["imdbRating"].ToString() + "/10";
            lbPlot.Text   = Data.selectedMovieData["Plot"].ToString();
            lbGenres.Text = Data.selectedMovieData["Genre"].ToString();
            lbReleaseYear.Text = Data.selectedMovieData["Year"].ToString();
            lbRuntime.Text  = Data.selectedMovieData["Runtime"].ToString();
            lbActors.Text   = Data.selectedMovieData["Actors"].ToString();
            lbWriter.Text   = Data.selectedMovieData["Writer"].ToString();
            lbDirector.Text = Data.selectedMovieData["Director"].ToString();
        }

        private void pbFavorite_Click(object sender, EventArgs e)
        {
            Bitmap starImage = Favorite.changeStar(Data.selectedMovieData["Title"].ToString(), "change");
            pbFavorite.Image = starImage;

            bool isFavorite = Data.favorites.Any(title => title == Data.selectedMovieData["Title"].ToString());

            if (isFavorite)
            {
                Favorite.removeFromFavorites(Data.selectedMovieData["Title"].ToString());
            }
            else
            {
                Favorite.addToFavorites(Data.selectedMovieData["Title"].ToString());
            }
        }

        private void btnShows_Click(object sender, EventArgs e)
        {
            TvShows tvShowsPage = new TvShows();
            tvShowsPage.Show();
            this.Hide();
        }

        private void btnFavorites_Click(object sender, EventArgs e)
        {
            Favorites favoritesPage = new Favorites();
            favoritesPage.Show();
            this.Hide();
        }

        private void btnMovies_Click(object sender, EventArgs e)
        {
            Movies moviesPage = new Movies();
            moviesPage.Show();
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
