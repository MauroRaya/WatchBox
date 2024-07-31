using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchBox
{
    public partial class MovieControl : UserControl
    {
        public MovieControl()
        {
            InitializeComponent();
            IsFavorite = false;
        }

        public Image Poster
        {
            get { return pbPoster.Image;  }
            set { pbPoster.Image = value; }
        }

        public String Title
        {
            get { return lbTitle.Text;  }
            set { lbTitle.Text = value; }
        }

        public String Rating
        {
            get { return lbRating.Text;  }
            set { lbRating.Text = value; }
        }

        public Image FavoriteButton
        {
            get { return pbFavorite.Image; }
            set { pbFavorite.Image = value; }
        }

        public bool IsFavorite { get; set; }

        private async void pbPoster_Click(object sender, EventArgs e)
        {
            Data.selectedMovieData = await Search.fetchMovie(lbTitle.Text);

            if (Data.selectedMovieData == null)
            {
                return;
            }

            FormSearched searchedMoviePage = new FormSearched();
            searchedMoviePage.Show();
            this.ParentForm.Hide();
        }

        private void pbFavorite_Click(object sender, EventArgs e)
        {
            if (IsFavorite)
            {
                Favorites.removeFromFavorites(this);

                if (Error.getError())
                {
                    Error.showMessage(Error.getMessage());
                    return;
                }

                if (this.ParentForm is FormFavorites)
                {
                    FormFavorites.removeFavoriteUI(this);

                    if (Error.getError())
                    {
                        Error.showMessage(Error.getMessage());
                        return;
                    }
                }
            }
            else
            {
                Favorites.addToFavorites(this);

                if (Error.getError())
                {
                    Error.showMessage(Error.getMessage());
                    return;
                }
            }
        }
    }
}
