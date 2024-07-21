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

        private void displayFavorites()
        {
            flowLayoutPanel.Controls.Clear();

            foreach (var movie in Data.favorites)
            {
                MovieControl favoriteControl = new MovieControl();

                favoriteControl.Name = movie["Title"];

                byte[] imageBytes = Convert.FromBase64String(movie["Poster"]);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    favoriteControl.Poster = Image.FromStream(ms);
                }

                if (Data.favorites.Any(dict => dict["Title"] == movie["Title"].ToString()))
                {
                    favoriteControl.changeStar();
                }

                flowLayoutPanel.Controls.Add(favoriteControl);
            }
        }

        public void removeFavorite(string title)
        {
            foreach (Control control in flowLayoutPanel.Controls)
            {
                if (control is MovieControl movieControl && movieControl.Name == title)
                {
                    flowLayoutPanel.Controls.Remove(movieControl);
                    movieControl.Dispose();
                    break;
                }
            }
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
