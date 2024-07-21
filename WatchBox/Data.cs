using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace WatchBox
{
    public static class Data
    {
        public static List<Dictionary<string, string>> favorites = new List<Dictionary<string, string>>(); //turn into a db table later



        public static List<byte[]> chosenMoviePosters = new List<byte[]>();

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
                { "Poster", @"https://m.media-amazon.com/images/I/61PwPadu4-L._AC_UF894,1000_QL80_.jpg" }
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
            },
            new Dictionary<string, string>
            {
                { "Title", "Me before you" },
                { "Rating", "7.4" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BMTQ2NjE4NDE2NV5BMl5BanBnXkFtZTgwOTcwNDE5NzE@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "The Notebook" },
                { "Rating", "7.6" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BNzc3Mzg1OGYtZjc3My00Y2NhLTgyOWUtYjRhMmI4OTkwNDg4XkEyXkFqcGdeQXVyMTU3NDU4MDg2._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Bullet Train" },
                { "Rating", "7.3" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BMDU2ZmM2OTYtNzIxYy00NjM5LTliNGQtN2JmOWQzYTBmZWUzXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_SX300.jpg" }
            },

        };

        public static List<byte[]> chosenTvShowPosters = new List<byte[]>();

        public static List<Dictionary<string, string>> chosenTvShows = new List<Dictionary<string, string>>();

        public static List<Dictionary<string, string>> tvShowRecommendations = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string>
            {
                { "Title", "Sherlock" },
                { "Rating", "9.1" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BNTQzNGZjNDEtOTMwYi00MzFjLWE2ZTYtYzYxYzMwMjZkZDc5XkEyXkFqcGc@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "The Office" },
                { "Rating", "9.0" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BMDNkOTE4NDQtMTNmYi00MWE0LWE4ZTktYTc0NzhhNWIzNzJiXkEyXkFqcGdeQXVyMzQ2MDI5NjU@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "The Crown" },
                { "Rating", "8.6" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BODYxYjZkODQtYTMyZS00NTMwLTkzNmItZDZjZDJjMmIzMjk0XkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Avatar: The Last Airbender" },
                { "Rating", "9.3" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BODc5YTBhMTItMjhkNi00ZTIxLWI0YjAtNTZmOTY0YjRlZGQ0XkEyXkFqcGdeQXVyODUwNjEzMzg@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Star Wars: The Clone Wars" },
                { "Rating", "8.4" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BZWFlNzRmOTItZjY1Ni00ZjZkLTk5MDgtOGFhOTYzNWFhYzhmXkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Spy x Family" },
                { "Rating", "8.3" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BZDY5NTI3ZjUtNGY5ZC00MmE1LWI0ZTUtNDI3YTQ0NGFkMWJlXkEyXkFqcGdeQXVyNTgyNTA4MjM@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Bridgerton" },
                { "Rating", "7.4" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BY2ZiODA2MGYtMmMxMi00YjlmLWFmYjktMWYyOTMwNWFkNWNkXkEyXkFqcGdeQXVyMTUzMTg2ODkz._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Virgin River" },
                { "Rating", "7.4" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BNjcyMjE5NDAtZjRlMy00MjNhLWEwMGEtNWJlMmIwYWQ1MWZlXkEyXkFqcGdeQXVyMTUwMjg2Nzg0._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "The Last of Us" },
                { "Rating", "8.7" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BZGUzYTI3M2EtZmM0Yy00NGUyLWI4ODEtN2Q3ZGJlYzhhZjU3XkEyXkFqcGdeQXVyNTM0OTY1OQ@@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Peaky Blinders" },
                { "Rating", "8.8" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BZjYzZDgzMmYtYjY5Zi00YTk1LThhMDYtNjFlNzM4MTZhYzgyXkEyXkFqcGdeQXVyMTE5NDQ1MzQ3._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Better Call Saul" },
                { "Rating", "8.9" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BZDA4YmE0OTYtMmRmNS00Mzk2LTlhM2MtNjk4NzBjZGE1MmIyXkEyXkFqcGdeQXVyMTMzNDExODE5._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "The Mandalorian" },
                { "Rating", "8.7" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BN2M5YWFjN2YtYzU2YS00NzBlLTgwZWUtYWQzNWFhNDkyYjg3XkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Money Heist" },
                { "Rating", "8.2" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BODI0ZTljYTMtODQ1NC00NmI0LTk1YWUtN2FlNDM1MDExMDlhXkEyXkFqcGdeQXVyMTM0NTUzNDIy._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "The Umbrella Academy" },
                { "Rating", "8.0" },
                { "Poster", @"https://m.media-amazon.com/images/I/61AG5UDNtAL._AC_UF894,1000_QL80_.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Re:Zero - Starting Life in Another World" },
                { "Rating", "8.1" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BN2NlM2Y5Y2MtYjU5Mi00ZjZiLWFjNjMtZDNiYzJlMjhkOWZiXkEyXkFqcGdeQXVyNjc2NjA5MTU@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Vinland Saga" },
                { "Rating", "8.8" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BYTAwM2FhMzItMzFmMS00ZDY2LTk0NDctNTI3MDU2OTNjZWVlXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Jujutsu Kaisen" },
                { "Rating", "8.7" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BNGY4MTg3NzgtMmFkZi00NTg5LWExMmEtMWI3YzI1ODdmMWQ1XkEyXkFqcGdeQXVyMjQwMDg0Ng@@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Kaguya-sama: Love is War" },
                { "Rating", "8.5" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BYjEwNjEwYzgtZGZkMy00MTBjLTg2MmYtNDk0MzY2NmU0MmNiXkEyXkFqcGdeQXVyMzgxODM4NjM@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Frieren: Beyond Journey's End" },
                { "Rating", "9.3" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BMjVjZGU5ZTktYTZiNC00N2Q1LThiZjMtMDVmZDljN2I3ZWIwXkEyXkFqcGdeQXVyMTUzMTg2ODkz._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "Daredevil" },
                { "Rating", "8.9" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BODcwOTg2MDE3NF5BMl5BanBnXkFtZTgwNTUyNTY1NjM@._V1_SX300.jpg" }
            },
            new Dictionary<string, string>
            {
                { "Title", "86" },
                { "Rating", "8.3" },
                { "Poster", @"https://m.media-amazon.com/images/M/MV5BOTA5NGVkMzYtODAzYi00YWQyLThhYzctZTQxNTM1NDY2YjU0XkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_SX300.jpg" }
            },
        };
    }
}
