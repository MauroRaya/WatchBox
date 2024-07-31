using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchBox.DAL;

namespace WatchBox.BLL
{
    static class LoginBLL
    {
        public static void validateFields(string username, string password)
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
        }

        public static string getUserId(string username)
        {
            return LoginDAL.getUserId(username);
        }

        public static bool userExists(string username)
        {
            return LoginDAL.userExists(username);
        }

        public static string getHash(string username)
        {
            return LoginDAL.getHash(username);
        }

        public static string getSalt(string username)
        {
            return LoginDAL.getSalt(username);
        }
    }
}
