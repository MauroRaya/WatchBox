using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WatchBox
{
    public partial class FormShare : Form
    {
        private static ShareDataSet shareDataSet;
        private static List<JObject> sharebox = new List<JObject>();

        public FormShare()
        {
            InitializeComponent();
            displayShareBox();
            tbSearchTitle.KeyPress += new KeyPressEventHandler(tbSearchTitle_KeyPress);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMovies_Click_1(object sender, EventArgs e)
        {
            FormMovies moviesPage = new FormMovies();
            moviesPage.Show();
            this.Hide();
        }

        private void btnFavorites_Click(object sender, EventArgs e)
        {
            FormFavorites favoritesPage = new FormFavorites();
            favoritesPage.Show();
            this.Hide();
        }

        private async void displayShareBox()
        {
            List<string> favorites = Favorites.getFavorites();

            foreach (JObject movie in sharebox)
            {
                MovieControl movieControl = new MovieControl();
                movieControl.Title  = movie["Title"].ToString();
                movieControl.Rating = movie["imdbRating"].ToString() + "/10";
                movieControl.Poster = await fetchImageData(movie["Poster"].ToString());
                movieControl.IsFavorite = favorites.Contains(movie["Title"].ToString());

                Favorites.changeStar(movieControl);
                flowLayoutPanel.Controls.Add(movieControl);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            JObject movie = null;
            List<string> favorites = Favorites.getFavorites();

            try
            {
                movie = await Search.fetchMovie(tbSearchTitle.Text);

                if (movie == null || movie["Response"].ToString() == "False")
                {
                    MessageBox.Show("Movie not found. Make sure to type the title correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MovieControl movieControl = new MovieControl();
                movieControl.Title  = movie["Title"].ToString();
                movieControl.Rating = movie["imdbRating"].ToString() + "/10";
                movieControl.Poster = await fetchImageData(movie["Poster"].ToString());
                movieControl.IsFavorite = favorites.Contains(movie["Title"].ToString());

                Favorites.changeStar(movieControl);
                flowLayoutPanel.Controls.Add(movieControl);

                sharebox.Add(movie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbSearchTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private async Task<Bitmap> fetchImageData(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    byte[] imageBytes = await httpClient.GetByteArrayAsync(url);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        return new Bitmap(ms);
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw new Exception("Unable to download the poster image. Please check your internet connection or try again later.");
            }
        }

        private async Task<string> fetchImageDataAndSaveLocally(string url, string movieTitle)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    byte[] imageBytes = await httpClient.GetByteArrayAsync(url);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        using (var image = new Bitmap(ms))
                        {
                            string sanitizedTitle = string.Join("_", movieTitle.Split(Path.GetInvalidFileNameChars()));

                            string imageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                            if (!Directory.Exists(imageDir))
                            {
                                Directory.CreateDirectory(imageDir);
                            }

                            string imagePath = Path.Combine(imageDir, $"{sanitizedTitle}.png");
                            image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                            return imagePath;
                        }
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw new Exception("Unable to download the poster image. Please check your internet connection or try again later.");
            }
        }



        private void btnShows_Click(object sender, EventArgs e)
        {
            FormShows tvShowsPage = new FormShows();
            tvShowsPage.Show();
            this.Hide();
        }

        private async void btnShare_Click(object sender, EventArgs e)
        {
            await LoadDataFromSharebox();

            if (shareDataSet.Tables["Movie"].Rows.Count == 0)
            {
                MessageBox.Show("No data available to generate the PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CrystalReport1 report = new CrystalReport1();

            report.SetDataSource(shareDataSet.Tables["Movie"]);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "MovieRecommendations.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                report.ExportToDisk(ExportFormatType.PortableDocFormat, saveFileDialog.FileName); //problem when pdf already open
                MessageBox.Show("PDF generated successfully!");
            }
        }

        private async Task LoadDataFromSharebox()
        {
            shareDataSet = new ShareDataSet();
            DataTable movieTable = shareDataSet.Tables["Movie"];

            foreach (var movie in sharebox)
            {
                DataRow row = movieTable.NewRow();

                row["title"] = movie["Title"].ToString();
                row["year"] = movie["Year"].ToString();
                row["runtime"] = movie["Runtime"].ToString();
                row["genre"] = movie["Genre"].ToString();
                row["director"] = movie["Director"].ToString();
                row["writer"] = movie["Writer"].ToString();
                row["actors"] = movie["Actors"].ToString();
                row["plot"] = movie["Plot"].ToString();
                row["poster"] = await fetchImageDataAndSaveLocally(movie["Poster"].ToString(), movie["Title"].ToString());
                row["rating"] = movie["imdbRating"].ToString() + "/10";

                movieTable.Rows.Add(row);
            }
        }
    }
}
