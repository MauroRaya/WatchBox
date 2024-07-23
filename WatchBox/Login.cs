using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchBox
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            fetchMovieData(Data.movieRecommendations, "movie");
            fetchMovieData(Data.tvShowRecommendations, "show");
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

        private async void fetchMovieData(List<string> recommendations, string type)
        {
            Random rnd = new Random();
            string[] movies = recommendations
                             .OrderBy(x => rnd.Next())
                             .Take(5)
                             .ToArray();

            foreach (string title in movies)
            {
                JObject movieData = await Search.fetchMovie(title);

                if (movieData != null)
                {
                    byte[] imageBytes = await fetchImageData(movieData["Poster"].ToString());

                    if (type == "movie")
                    {
                        Data.chosenMovies.Add(movieData);
                        Data.chosenMoviePosters.Add(imageBytes);
                    }
                    else if (type == "show")
                    {
                        Data.chosenTvShows.Add(movieData);
                        Data.chosenTvShowPosters.Add(imageBytes);
                    }
                }
            }
        }

        private async Task<byte[]> fetchImageData(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    return await httpClient.GetByteArrayAsync(url);
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}");
            }
            return null;
        }
    }
}


