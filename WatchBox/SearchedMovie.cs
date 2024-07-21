using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                byte[] imageBytes = await webClient.GetByteArrayAsync(posterUrl);
                pbPoster.Image    = Image.FromStream(new System.IO.MemoryStream(imageBytes));
            }

            lbTitle.Text  = Data.selectedMovieData["Title"].ToString();
            lbRating.Text = Data.selectedMovieData["imdbRating"].ToString();
            lbPlot.Text   = Data.selectedMovieData["Plot"].ToString();
            lbGenres.Text = Data.selectedMovieData["Genre"].ToString();
            lbReleaseYear.Text = Data.selectedMovieData["Year"].ToString();
            lbRuntime.Text  = Data.selectedMovieData["Runtime"].ToString();
            lbActors.Text   = Data.selectedMovieData["Actors"].ToString();
            lbWriter.Text   = Data.selectedMovieData["Writer"].ToString();
            lbDirector.Text = Data.selectedMovieData["Director"].ToString();
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
    }
}
