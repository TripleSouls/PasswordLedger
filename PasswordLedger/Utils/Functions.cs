using System;
using static System.Net.Mime.MediaTypeNames;

namespace PasswordLedger.Utils
{
    internal class Functions
    {
        public static string GetExePath()
        {
            return Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location
            );
        }
    }
}
