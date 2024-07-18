using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchBox
{
    public static class Data
    {
        public static List<byte[]> chosenPosters = new List<byte[]>();

        public static List<Dictionary<string, string>> chosenMovies = new List<Dictionary<string, string>>();

        public static List<Dictionary<string, string>> movieRecomendations = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string>
            {
                { "Title", "Star Wars: Episode V - The Empire Strikes Back" },
                { "Rating", "9.5" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BYmU1NDRjNDgtMzhiMi00NjZmLTg5NGItZDNiZjU5NTU4OTE0XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "The Matrix" },
                { "Rating", "8.7" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BNzQzOTk3OTAtNDQ0Zi00ZTVkLWI0MTEtMDllZjNkYzNjNTc4L2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Fight Club" },
                { "Rating", "8.8" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BMmEzNTkxYjQtZTc0MC00YTVjLTg5ZTEtZWMwOWVlYzY0NWIwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Forrest Gump" },
                { "Rating", "8.8" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BNWIwODRlZTUtY2U3ZS00Yzg1LWJhNzYtMmZiYmEyNmU1NjMzXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Avatar" },
                { "Rating", "7.8" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BZDA0OGQxNTItMDZkMC00N2UyLTg3MzMtYTJmNjg3Nzk5MzRiXkEyXkFqcGdeQXVyMjUzOTY1NTc@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Up" },
                { "Rating", "8.2" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BYjBkM2RjMzItM2M3Ni00N2NjLWE3NzMtMGY4MzE4MDAzMTRiXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "WALL-E" },
                { "Rating", "8.4" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BMjExMTg5OTU0NF5BMl5BanBnXkFtZTcwMjMxMzMzMw@@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Interstellar" },
                { "Rating", "8.6" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Whiplash" },
                { "Rating", "8.5" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BOTA5NDZlZGUtMjAxOS00YTRkLTkwYmMtYWQ0NWEwZDZiNjEzXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "The Avengers" },
                { "Rating", "8.0" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Saving Private Ryan" },
                { "Rating", "8.6" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BZjhkMDM4MWItZTVjOC00ZDRhLThmYTAtM2I5NzBmNmNlMzI1XkEyXkFqcGdeQXVyNDYyMDk5MTU@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Mission Impossible" },
                { "Rating", "7.1" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BOTFhMWY3ZTctNTJlOC00Y2UwLThmOGUtMWU4NDI1Yzg4ODRkXkEyXkFqcGdeQXVyMTUzMDUzNTI3._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Harry Potter and the Philosopher's Stone" },
                { "Rating", "7.6" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BNmQ0ODBhMjUtNDRhOC00MGQzLTk5MTAtZDliODg5NmU5MjZhXkEyXkFqcGdeQXVyNDUyOTg3Njg@._V1_SX300.jpg" }
            }
        };
    }
}
