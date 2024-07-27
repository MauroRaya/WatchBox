using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchBox
{
    public static class Search
    {
        private static readonly HttpClient client = new HttpClient();

        private static string apiKey = "bonito flakes";
        private static string apiUrl = "http://www.omdbapi.com";

        public async static Task<JObject> fetchMovie(string title)
        {
            string apiRequest = $"{apiUrl}/?apikey={apiKey}&t={title}";

            try
            {
                string responseBody = await client.GetStringAsync(apiRequest);
                return JObject.Parse(responseBody);
            }
            catch (HttpRequestException)
            {
                throw new Exception("Unable to connect to the movie database. Please check your internet connection or try again later.");
            }
            catch (JsonException)
            {
                throw new Exception("Received unexpected data from the movie database. Please try again later.");
            }
        }
    }
}
