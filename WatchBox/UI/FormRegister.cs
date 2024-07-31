using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatchBox.BLL;
using WatchBox.DAL;

namespace WatchBox
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            string confirmPassword = tbConfirmPassword.Text;

            RegisterBLL.validateFields(username, password, confirmPassword);

            if (Error.getError())
            {
                Error.showMessage(Error.getMessage());
                return;
            }

            if (RegisterBLL.userExists(username))
            {
                Error.showMessage(Error.getMessage());
                return;
            }

            string salt = generateSalt();
            string hash = hashPassword(password, salt);

            RegisterBLL.createUser(username, hash, salt);

            if (Error.getError())
            {
                Error.showMessage(Error.getMessage());
                return;
            }

            FormMovies moviesPage = new FormMovies();
            moviesPage.Show();
            this.Hide();
        }

        public static string generateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string hashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private void btnToLogin_Click(object sender, EventArgs e)
        {
            FormLogin loginPage = new FormLogin();
            loginPage.Show();
            this.Hide();
        }
    }
}
