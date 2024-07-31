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
    public partial class FormMovies : Form
    {
        public FormMovies()
        {
            InitializeComponent();
            displayRecomendations();
            tbSearchTitle.KeyPress += new KeyPressEventHandler(tbSearchTitle_KeyPress);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void displayRecomendations()
        {
            List<string> favorites = Favorites.getFavorites();

            for (int i = 0; i < Data.chosenMovies.Count; i++)
            {
                var movie = Data.chosenMovies[i];

                MovieControl movieControl = new MovieControl();
                movieControl.Title  = movie["Title"].ToString();
                movieControl.Rating = movie["imdbRating"].ToString() + "/10";
                movieControl.Poster = Image.FromStream(new System.IO.MemoryStream(Data.chosenMoviePosters[i]));
                movieControl.IsFavorite = favorites.Contains(movie["Title"].ToString());

                Favorites.changeStar(movieControl);
                flowLayoutPanel.Controls.Add(movieControl);
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

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            Data.selectedMovieData = await Search.fetchMovie(tbSearchTitle.Text);

            if (Error.getError())
            {
                Error.showMessage(Error.getMessage());
            }

            if (Data.selectedMovieData == null || Data.selectedMovieData["Response"].ToString() == "False")
            {
                MessageBox.Show("Movie not found. Make sure to type the title correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormSearched moviePage = new FormSearched();
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

        private void btnShare_Click(object sender, EventArgs e)
        {
            FormShare sharePage = new FormShare();
            sharePage.Show();
            this.Hide();
        }
    }
}
