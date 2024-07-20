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
            foreach (var movie in Data.favorites)
            {
                FavoriteControl favoriteControl = new FavoriteControl();
                favoriteControl.Name = movie["Title"];

                byte[] imageBytes = Convert.FromBase64String(movie["Poster"]);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    favoriteControl.Poster = Image.FromStream(ms);
                }

                flowLayoutPanel.Controls.Add(favoriteControl);
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
