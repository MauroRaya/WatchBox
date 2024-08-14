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
            Application.ApplicationExit += new EventHandler(deleteFilesInFolder);
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

        private void deleteFilesInFolder(object sender, EventArgs e)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            if (Directory.Exists(folderPath))
            {
                try
                {
                    string[] files = Directory.GetFiles(folderPath);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting files: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Folder path does not exist.");
            }
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

                if (sharebox.Any(m => m["Title"].ToString() == movie["Title"].ToString()))
                {
                    MessageBox.Show("Movie already in your sharebox. Try another movie.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Favorites.changeStar(movieControl);
                flowLayoutPanel.Controls.Add(movieControl);
                sharebox.Add(movie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

        private async Task<string> fetchColorfulImageDataAndSaveLocally(string url, string movieTitle)
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

                            string imageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Posters");
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

        private async Task<string> fetchGrayscaleImageDataAndSaveLocally(string url, string movieTitle)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    byte[] imageBytes = await httpClient.GetByteArrayAsync(url);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        using (var originalImage = new Bitmap(ms))
                        {
                            // Create an 8-bit per pixel image
                            using (var image = new Bitmap(originalImage.Width, originalImage.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
                            {
                                // Create a grayscale palette
                                var palette = image.Palette;
                                for (int i = 0; i < 256; i++)
                                {
                                    palette.Entries[i] = Color.FromArgb(i, i, i);
                                }
                                image.Palette = palette;

                                // Lock bits of the 8-bit image
                                var rect = new Rectangle(0, 0, image.Width, image.Height);
                                var imageData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, image.PixelFormat);

                                // Copy pixel data
                                for (int y = 0; y < image.Height; y++)
                                {
                                    for (int x = 0; x < image.Width; x++)
                                    {
                                        // Get the pixel from the original image
                                        Color originalColor = originalImage.GetPixel(x, y);

                                        // Convert to grayscale
                                        byte grayScale = (byte)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));

                                        // Set the pixel in the 8-bit image
                                        IntPtr ptr = imageData.Scan0 + y * imageData.Stride + x;
                                        System.Runtime.InteropServices.Marshal.WriteByte(ptr, grayScale);
                                    }
                                }

                                // Unlock bits of the 8-bit image
                                image.UnlockBits(imageData);

                                string sanitizedTitle = string.Join("_", movieTitle.Split(Path.GetInvalidFileNameChars()));

                                string imageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Posters");
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
            DialogResult dr = MessageBox.Show(
                "There is an issue with generating colorful images in Crystal Reports on some machines. " +
                "Select 'Yes' to generate colorful images (which might encounter issues) or 'No' to generate grayscale images (no issues expected).",
                "Warning",
                MessageBoxButtons.YesNoCancel
            );

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            string imageType = (dr == DialogResult.Yes) ? "colorful" : "grayscale";
            await LoadDataFromSharebox(imageType);

            if (shareDataSet == null || shareDataSet.Tables["Movie"] == null)
            {
                MessageBox.Show("No data available to generate the PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (shareDataSet.Tables["Movie"].Rows.Count == 0)
            {
                MessageBox.Show("No data available to generate the PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (shareDataSet.Tables["Movie"].Rows.Count > 20)
            {
                MessageBox.Show("You surpassed the limit of 20 movie recommendations. Try fewer recommendations.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CrystalReport1 report = new CrystalReport1();
            report.SetDataSource(shareDataSet.Tables["Movie"]);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "MovieRecommendations.pdf"
            };

            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    report.ExportToDisk(ExportFormatType.PortableDocFormat, saveFileDialog.FileName);
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show("The file is already open or in use. Please close the file and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while generating the PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadDataFromSharebox(string typeImage)
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
                row["rating"] = movie["imdbRating"].ToString() + "/10";

                if (typeImage == "colorful")
                {
                    row["poster"] = await fetchColorfulImageDataAndSaveLocally(movie["Poster"].ToString(), movie["Title"].ToString());
                }
                else if (typeImage == "grayscale")
                {
                    row["poster"] = await fetchGrayscaleImageDataAndSaveLocally(movie["Poster"].ToString(), movie["Title"].ToString());
                }

                movieTable.Rows.Add(row);
            }
        }
    }
}
