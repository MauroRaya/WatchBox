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
    public static class Search
    {
        private static readonly HttpClient client = new HttpClient();

        private static string apiKey = "x.x <- robert died, look at what you did >:I";
        private static string apiUrl = "http://www.omdbapi.com";

        public async static Task fetchMovies(string title)
        {
            string apiRequest = $"{apiUrl}/?apikey={apiKey}&t={title}";

            try
            {
                string responseBody    = await client.GetStringAsync(apiRequest);
                Data.selectedMovieData = JObject.Parse(responseBody);
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
