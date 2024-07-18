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
            displayRecomendations();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void displayRecomendations()
        {
            Random rnd       = new Random();
            var chosenMovies = Data.movieRecomendations
                                  .OrderBy(x => rnd.Next())
                                  .Take(5)
                                  .ToList();

            for (int i = 0; i < 5; i++) 
            {
                var movie = Data.chosenMovies[i];

                MovieControl movieControl = new MovieControl();
                movieControl.Name   = movie["Title"].ToString();
                movieControl.Rating = movie["Rating"].ToString() + "/10";
                movieControl.Poster = Image.FromStream(new System.IO.MemoryStream(Data.chosenPosters[i]));

                flowLayoutPanel.Controls.Add(movieControl);
            }
        }
    }
}
