using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WatchBox
{
    public static class Recommendations
    {
        private static bool moviesFetchedOnce = false;
        private static bool showsFetchedOnce = false;
        private static bool fetching = false;

        public static async Task fetchRecommendationData(string type)
        {
            if (fetching || (type == "movie" && moviesFetchedOnce) || (type == "show" && showsFetchedOnce))
            {
                return;
            }

            fetching = true;

            if (type == "movie")
            {
                await storeRecommendationData(Data.movieRecommendations, type);
                moviesFetchedOnce = true;
            }
            else if (type == "show")
            {
                await storeRecommendationData(Data.tvShowRecommendations, type);
                showsFetchedOnce = true;
            }

            fetching = false;
        }

        private static async Task storeRecommendationData(List<string> recommendations, string type)
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

        public static MovieControl createMovieControl(JObject movieData, byte[] chosenPoster)
        {
            MovieControl movieControl = new MovieControl();
            movieControl.Title  = movieData["Title"].ToString();
            movieControl.Rating = movieData["imdbRating"].ToString() + "/10";
            movieControl.Poster = Image.FromStream(new System.IO.MemoryStream(chosenPoster));
            movieControl.IsFavorite = Favorites.getFavorites().Contains(movieData["Title"].ToString());

            Favorites.changeStar(movieControl);

            return movieControl;
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
