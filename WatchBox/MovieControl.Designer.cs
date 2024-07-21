namespace WatchBox
{
    partial class MovieControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovieControl));
            this.pbPoster = new System.Windows.Forms.PictureBox();
            this.lbName = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbRating = new System.Windows.Forms.Label();
            this.pbFavorite = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbPoster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFavorite)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPoster
            // 
            this.pbPoster.Image = ((System.Drawing.Image)(resources.GetObject("pbPoster.Image")));
            this.pbPoster.Location = new System.Drawing.Point(0, 0);
            this.pbPoster.Name = "pbPoster";
            this.pbPoster.Size = new System.Drawing.Size(194, 296);
            this.pbPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPoster.TabIndex = 0;
            this.pbPoster.TabStop = false;
            this.pbPoster.Click += new System.EventHandler(this.pbPoster_Click);
            // 
            // lbName
            // 
            this.lbName.AutoEllipsis = true;
            this.lbName.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            this.lbName.ForeColor = System.Drawing.Color.White;
            this.lbName.Location = new System.Drawing.Point(1, 299);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(193, 51);
            this.lbName.TabIndex = 1;
            this.lbName.Text = "Frieren: Beyond Journey\'s End";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1, 360);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 31);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // lbRating
            // 
            this.lbRating.AutoSize = true;
            this.lbRating.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            this.lbRating.ForeColor = System.Drawing.Color.White;
            this.lbRating.Location = new System.Drawing.Point(27, 367);
            this.lbRating.Name = "lbRating";
            this.lbRating.Size = new System.Drawing.Size(63, 22);
            this.lbRating.TabIndex = 3;
            this.lbRating.Text = "9,8/10";
            // 
            // pbFavorite
            // 
            this.pbFavorite.Image = ((System.Drawing.Image)(resources.GetObject("pbFavorite.Image")));
            this.pbFavorite.Location = new System.Drawing.Point(163, 362);
            this.pbFavorite.Name = "pbFavorite";
            this.pbFavorite.Size = new System.Drawing.Size(26, 26);
            this.pbFavorite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFavorite.TabIndex = 4;
            this.pbFavorite.TabStop = false;
            this.pbFavorite.Click += new System.EventHandler(this.pbFavorite_Click);
            // 
            // MovieControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(77)))), ((int)(((byte)(75)))));
            this.Controls.Add(this.pbFavorite);
            this.Controls.Add(this.lbRating);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.pbPoster);
            this.Name = "MovieControl";
            this.Size = new System.Drawing.Size(193, 394);
            ((System.ComponentModel.ISupportInitialize)(this.pbPoster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFavorite)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPoster;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbRating;
        private System.Windows.Forms.PictureBox pbFavorite;
    }
}
