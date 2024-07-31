using Newtonsoft.Json.Linq;
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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            createDatabase();
            _ = Recomendations.FetchData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            LoginBLL.validateFields(username, password);

            if (Error.getError())
            {
                Error.showMessage(Error.getMessage());
                return;
            }

            if (!LoginBLL.userExists(username))
            {
                Error.showMessage(Error.getMessage());
                return;
            }

            string salt = LoginBLL.getSalt(username);

            if (Error.getError())
            {
                Error.showMessage(Error.getMessage());
                return;
            }

            string hash = hashPassword(password, salt);

            if (hash != LoginBLL.getHash(username))
            {
                Error.showMessage("Invalid login credentials. Please try again.");
                return;
            }

            Data.userId = LoginBLL.getUserId(username);

            FormMovies moviesPage = new FormMovies();
            moviesPage.Show();
            this.Hide();
        }

        private void createDatabase()
        {
            string connectionString = "Data Source=watchbox.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand command = new SQLiteCommand(Data.createDatabaseCommand, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"SQLite Error: {ex.Message}");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
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

        private void btnToRegister_Click(object sender, EventArgs e)
        {
            FormRegister registerPage = new FormRegister();
            registerPage.Show();
            this.Hide();
        }
    }
}
