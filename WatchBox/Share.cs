using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class Share : Form
    {
        public Share()
        {
            InitializeComponent();
            tbSearchTitle.KeyPress += new KeyPressEventHandler(tbSearchTitle_KeyPress);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMovies_Click_1(object sender, EventArgs e)
        {
            Movies moviesPage = new Movies();
            moviesPage.Show();
            this.Hide();
        }

        private void btnFavorites_Click(object sender, EventArgs e)
        {
            Favorites favoritesPage = new Favorites();
            favoritesPage.Show();
            this.Hide();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            JObject movie = null;

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
                movieControl.IsFavorite = Data.favorites.Contains(movie["Title"].ToString());

                Favorite.changeStar(movieControl);

                flowLayoutPanel.Controls.Add(movieControl);
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

        private void btnShows_Click(object sender, EventArgs e)
        {
            TvShows tvShowsPage = new TvShows();
            tvShowsPage.Show();
            this.Hide();
        }

        private void btnShare_Click(object sender, EventArgs e)
        {
            try
            {
                string reportPath = @"";

                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load(reportPath);

                reportDocument.SetParameterValue("Title",  "title here or something");
                reportDocument.SetParameterValue("Rating", "rating stuff");

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string exportPath  = System.IO.Path.Combine(desktopPath, "relatorio.pdf");

                ExportOptions exportOptions = reportDocument.ExportOptions;
                PdfRtfWordFormatOptions pdfFormatOptions = new PdfRtfWordFormatOptions();
                DiskFileDestinationOptions diskOptions = new DiskFileDestinationOptions();
                diskOptions.DiskFileName = exportPath;

                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                exportOptions.FormatOptions = pdfFormatOptions;
                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                exportOptions.DestinationOptions = diskOptions;

                reportDocument.Export();

                MessageBox.Show("Document exported with success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to export document: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
