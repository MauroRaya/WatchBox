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
    public partial class MovieControl : UserControl
    {
        public MovieControl()
        {
            InitializeComponent();
        }

        public Image Poster
        {
            get { return pbPoster.Image;  }
            set { pbPoster.Image = value; }
        }

        public String Name
        {
            get { return lbName.Text;  }
            set { lbName.Text = value; }
        }

        public String Rating
        {
            get { return lbRating.Text;  }
            set { lbRating.Text = value; }
        }
    }
}
