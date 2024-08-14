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
using System.IO;

namespace WatchBox
{
    public static class Search
    {
        private static readonly HttpClient client = new HttpClient();

        public async static Task<JObject> fetchMovie(string title)
        {
            Error.setError(false);

            string apiUrl = "https://api-watchbox.onrender.com/fetchMovie";
            string requestUrl = $"{apiUrl}?title={Uri.EscapeDataString(title)}";

            try
            {
                string responseBody = await client.GetStringAsync(requestUrl);
                return JObject.Parse(responseBody);
            }
            catch (HttpRequestException)
            {
                Error.setError("Unable to connect to the movie database. Please check your internet connection or try again later.");
                return null;
            }
            catch (JsonException)
            {
                Error.setError("Received unexpected data from the movie database. Please try again later.");
                return null;
            }
        }
    }
}
