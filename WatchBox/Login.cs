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
        private bool fetching = true;
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

            if (fetching)
            {
                MessageBox.Show("Data is still being fetched. Please wait a moment and try again.", "Data Fetching", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (username == "teste" && password == "123")
            {
                Movies home = new Movies();
                this.Hide();
                home.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please check your credentials and try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private async void fetchMovieData(List<string> recommendations, string type)
        {
            Random rnd = new Random();
            string[] movies = recommendations
                             .OrderBy(x => rnd.Next())
                             .Take(5)
                             .ToArray();

            fetching = true;

            foreach (string title in movies)
            {
                try
                {
                    JObject movieData = await Search.fetchMovie(title);

                    if (movieData != null && movieData["Poster"] != null)
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
                catch (Exception ex)
                {
                    showErrorMessage(ex.Message);
                    break;
                }
            }

            fetching = false;
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
            catch (HttpRequestException)
            {
                throw new Exception("Unable to download the poster image. Please check your internet connection or try again later.");
            }
        }

        private void showErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}


