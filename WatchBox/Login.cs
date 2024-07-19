using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchBox
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            populateRecomendations();
            fetchPosters();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            if (username == "teste" && password == "123")
            {
                Movies home = new Movies();
                this.Hide();
                home.Show();
            }
        }

        private void populateRecomendations()
        {
            Random rnd  = new Random();
            Data.chosenMovies = Data.movieRecomendations
                                  .OrderBy(x => rnd.Next())
                                  .Take(5)
                                  .ToList();

            Data.chosenTvShows = Data.tvShowRecommendations
                                  .OrderBy(x => rnd.Next())
                                  .Take(5)
                                  .ToList();
        }

        private async void fetchPosters()
        {
            foreach (var movie in Data.chosenMovies)
            {
                string posterUrl = movie["Poster"].ToString();

                using (var webClient = new HttpClient())
                {
                    byte[] imageBytes = await webClient.GetByteArrayAsync(posterUrl);
                    Data.chosenMoviePosters.Add(imageBytes);
                }
            }

            foreach (var show in Data.chosenTvShows)
            {
                string posterUrl = show["Poster"].ToString();

                using (var webClient = new HttpClient())
                {
                    byte[] imageBytes = await webClient.GetByteArrayAsync(posterUrl);
                    Data.chosenTvShowPosters.Add(imageBytes);
                }
            }
        }
    }
}
