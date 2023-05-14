using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLedger.Model
{
    class Credential
    {
        public string Title     = "";
        public string Username  = "";
        public string Password  = "";
        public string Extended  = "";
    
        public Credential() {
            Title = Username = Password = Extended = "";
        }

        public Credential(string title = "", string username = "", string password = "", string extended = "") {
            Title       = title;
            Username    = username;
            Password    = password;
            Extended    = extended;
        }

        public void Setter(string? title = null, string? username = null, string? password = null, string? extended = null)
        {
            if(title    != null) Title      = title;
            if(username != null) Username   = username;
            if(password != null) Password   = password;
            if(extended != null) Extended   = extended;
        }

    }
}
