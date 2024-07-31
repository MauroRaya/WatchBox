using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchBox
{
    public static class Recomendations
    {
        private static bool fetchedOnce = false;
        private static bool fetching = false;

        public static async Task FetchData()
        {
            if (fetchedOnce || fetching)
            {
                return;
            }

            fetching = true;
            await fetchMovieData(Data.movieRecommendations, "movie");
            await fetchMovieData(Data.tvShowRecommendations, "show");
            fetchedOnce = true;
            fetching = false;
        }

        private static async Task fetchMovieData(List<string> recommendations, string type)
        {
            Random rnd = new Random();

            string[] movies = recommendations.OrderBy(x => rnd.Next()).Take(5).ToArray();

            foreach (string title in movies)
            {
                JObject movieData = await Search.fetchMovie(title);

                if (movieData == null || movieData["Poster"] == null)
                {
                    continue;
                }

                byte[] imageBytes = await fetchImageData(movieData["Poster"].ToString());

                if (Error.getError() || imageBytes == null)
                {
                    continue;
                }

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

        private static async Task<byte[]> fetchImageData(string url)
        {
            Error.setError(false);

            try
            {
                using (var httpClient = new HttpClient())
                {
                    return await httpClient.GetByteArrayAsync(url);
                }
            }
            catch (HttpRequestException)
            {
                Error.setError("Unable to download the poster image. Please check your internet connection or try again later.");
                return null;
            }
        }
    }
}
