using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchBox
{
    static class Error
    {
        private static bool error;
        private static string message;

        public static bool getError() { return error; }
        public static string getMessage() { return message; }

        public static void setError(bool _err) { error = _err; }
        public static void setError(string _msg) { error = true; message = _msg; }

        public static void showMessage(string _msg) { MessageBox.Show(_msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
