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
    public partial class FormShows : Form
    {
        public FormShows()
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

            for (int i = 0; i < Data.chosenTvShows.Count; i++) 
            {
                var show = Data.chosenTvShows[i];

                MovieControl showControl = new MovieControl();
                showControl.Title  = show["Title"].ToString();
                showControl.Rating = show["imdbRating"].ToString() + "/10";
                showControl.Poster = Image.FromStream(new System.IO.MemoryStream(Data.chosenTvShowPosters[i]));
                showControl.IsFavorite = favorites.Contains(show["Title"].ToString());

                Favorites.changeStar(showControl);

                flowLayoutPanel.Controls.Add(showControl);
            }
        }

        private void btnMovies_Click_1(object sender, EventArgs e)
        {
            FormMovies moviesPage = new FormMovies();
            moviesPage.Show();
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
