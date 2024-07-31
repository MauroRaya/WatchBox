using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchBox.DAL;

namespace WatchBox.BLL
{
    static class RegisterBLL
    {
        public static void validateFields(string username, string password, string confirmPassword)
        {
            Error.setError(false);

            if (string.IsNullOrWhiteSpace(username))
            {
                Error.setError("Invalid username. Try again.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                Error.setError("Invalid password. Try again.");
                return;
            }

            if (password != confirmPassword)
            {
                Error.setError("Passwords do not match. Try again.");
                return;
            }
        }
        public static bool userExists(string username)
        {
            return RegisterDAL.userExists(username);
        }

        public static void createUser(string username, string hash, string salt)
        {
            RegisterDAL.createUser(username, hash, salt);
        }
    }
}
