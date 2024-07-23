using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WatchBox
{
    public static class Data
    {
        public static List<string> favorites = new List<string>(); //turn into a db table later

        public static JObject selectedMovieData; //data of the click or search, can be either movie or tv show

        public static List<byte[]> chosenMoviePosters = new List<byte[]>();

        public static List<JObject> chosenMovies = new List<JObject>();

        public static List<string> movieRecommendations = new List<string>
        {
            "Star Wars: Episode V - The Empire Strikes Back",
            "The Matrix",
            "Fight Club",
            "Forrest Gump",
            "Avatar",
            "Up",
            "WALL-E",
            "Interstellar",
            "Whiplash",
            "The Avengers",
            "Saving Private Ryan",
            "Mission Impossible",
            "Harry Potter and the Sorcerer's Stone",
            "Me before you",
            "The Notebook",
            "Bullet Train",
            "Lord of the Rings",
            "The Godfather",
            "The Dark Knight",
            "The Karate Kid",
            "Goodfellas",
            "Gladiator",
            "Inception",
            "Back to the Future",
            "Eternal Sunshine of the Spotless Mind"
        };

        public static List<byte[]> chosenTvShowPosters = new List<byte[]>();

        public static List<JObject> chosenTvShows = new List<JObject>();

        public static List<string> tvShowRecommendations = new List<string>
        {
            "Sherlock",
            "The Office",
            "The Crown",
            "Avatar: The Last Airbender",
            "Star Wars: The Clone Wars",
            "Spy x Family",
            "Bridgerton",
            "Virgin River",
            "The Last of Us",
            "Peaky Blinders",
            "Better Call Saul",
            "The Mandalorian",
            "Money Heist",
            "The Umbrella Academy",
            "Re:Zero - Starting Life in Another World",
            "Vinland Saga",
            "Jujutsu Kaisen",
            "Kaguya-sama: Love is War",
            "Frieren: Beyond Journey's End",
            "Daredevil",
            "86"
        };
    }
}
