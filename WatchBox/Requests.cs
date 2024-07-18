using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchBox
{
    public static class Requests
    {
        private static string apiKey = "nope, good try";
        private static string apiUrl = "http://www.omdbapi.com";

        private static readonly HttpClient client = new HttpClient();
        private static List<string> moviesShown   = new List<string>();

        public static List<JObject> movies = new List<JObject>();

        public static string getRandomMovie(string[] data)
        {
            Random rnd   = new Random();
            string movie = null;

            int attempts    = 0;
            int maxAttempts = 10;

            while (moviesShown.Contains(movie) || attempts < maxAttempts)
            {
                int randomNum = rnd.Next(0, data.Length);
                movie         = data[randomNum];

                attempts++;
            }

            moviesShown.Add(movie);
            return movie;
        }

        public async static Task fetchMovies(string title)
        {
            string apiRequest = $"{apiUrl}/?apikey={apiKey}&t={title}";

            try
            {
                string responseBody = await client.GetStringAsync(apiRequest);
                JObject movieData   = JObject.Parse(responseBody);
                
                movies.Add(movieData);
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}");
            }
            catch (JsonException e)
            {
                MessageBox.Show($"JSON error: {e.Message}");
            }
        }

    }
}
