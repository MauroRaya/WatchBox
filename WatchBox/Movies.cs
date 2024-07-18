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
    public partial class Movies : Form
    {
        public Movies()
        {
            InitializeComponent();
            populateMovies();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void populateMovies()
        {
            List<Task> fetchTasks = new List<Task>();

            for (int i = 0; i < 5; i++)
            {
                string movieTitle = Requests.getRandomMovie(Data.movieSuggestions);
                fetchTasks.Add(Requests.fetchMovies(movieTitle));
            }

            await Task.WhenAll(fetchTasks);

            foreach (JObject movie in Requests.movies)
            {
                MovieControl movieControl = new MovieControl();
                movieControl.Name   = movie["Title"].ToString();
                movieControl.Rating = movie["imdbRating"].ToString() + "/10";

                string posterUrl     = movie["Poster"].ToString();
                using (var webClient = new HttpClient())
                {
                    byte[] imageBytes   = await webClient.GetByteArrayAsync(posterUrl);
                    movieControl.Poster = Image.FromStream(new System.IO.MemoryStream(imageBytes));
                }

                flowLayoutPanel.Controls.Add(movieControl);
            }
        }
    }
}
