using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLedger.Model
{
    class Credentials
    {
        public List<Credential> credentials = new List<Credential>();

        Credentials() { }
        
        Credentials(List<Credential> credentials) {
            this.credentials = credentials;
        }

        public void Add(Credential credential) => credentials.Add(credential);
        
        public bool Remove(Credential credential) => credentials.Remove(credential);

        public bool Remove(string Title)
        {
            bool rData = false;
            int index = 0;
            foreach (var credential in credentials)
            {
                if(credential.Title == Title)
                {
                    credentials.RemoveAt(index);
                    rData = true;
                    break;
                }
                index++;
            }
            return rData;
        }

        public void SetList(List<Credential> credentials) => this.credentials = credentials;

    }
}