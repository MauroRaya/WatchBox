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
    public partial class FormSearched : Form
    {
        public FormSearched()
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

            Bitmap starImage = Favorites.changeStar(Data.selectedMovieData["Title"].ToString(), "load");
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
            Bitmap starImage = Favorites.changeStar(Data.selectedMovieData["Title"].ToString(), "change");
            pbFavorite.Image = starImage;

            List<string> favorites = Favorites.getFavorites();

            if (favorites.Contains(Data.selectedMovieData["Title"].ToString()))
            {
                Favorites.removeFromFavorites(Data.selectedMovieData["Title"].ToString());
            }
            else
            {
                Favorites.addToFavorites(Data.selectedMovieData["Title"].ToString());
            }
        }

        private void btnShows_Click(object sender, EventArgs e)
        {
            FormShows tvShowsPage = new FormShows();
            tvShowsPage.Show();
            this.Hide();
        }

        private void btnFavorites_Click(object sender, EventArgs e)
        {
            FormFavorites favoritesPage = new FormFavorites();
            favoritesPage.Show();
            this.Hide();
        }

        private void btnMovies_Click(object sender, EventArgs e)
        {
            FormMovies moviesPage = new FormMovies();
            moviesPage.Show();
            this.Hide();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Data.selectedMovieData = await Search.fetchMovie(tbSearchTitle.Text);

                if (Data.selectedMovieData == null || Data.selectedMovieData["Response"].ToString() == "False")
                {
                    MessageBox.Show("Movie not found. Make sure to type the title correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FormSearched moviePage = new FormSearched();
                moviePage.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbSearchTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void btnShare_Click(object sender, EventArgs e)
        {
            FormShare sharePage = new FormShare();
            sharePage.Show();
            this.Hide();
        }
    }
}
