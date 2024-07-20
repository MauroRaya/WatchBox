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
    public partial class TvShows : Form
    {
        public TvShows()
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
            for (int i = 0; i < 5; i++) 
            {
                var show = Data.chosenTvShows[i];

                MovieControl showControl = new MovieControl();
                showControl.Name   = show["Title"].ToString();
                showControl.Rating = show["Rating"].ToString() + "/10";
                showControl.Poster = Image.FromStream(new System.IO.MemoryStream(Data.chosenTvShowPosters[i]));

                flowLayoutPanel.Controls.Add(showControl);
            }
        }

        private void btnMovies_Click_1(object sender, EventArgs e)
        {
            Movies moviesPage = new Movies();
            moviesPage.Show();
            this.Hide();
        }
    }
}
