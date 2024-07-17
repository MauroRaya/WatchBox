using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchBox
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            populateMovies();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populateMovies()
        {
            MovieControl movieControl = new MovieControl();

            movieControl.Name = "Frieren: Beyond Journey's End";
            movieControl.Rating = "9.8/10";

            flowLayoutPanel.Controls.Add(movieControl);
        }
    }
}
