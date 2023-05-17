using PasswordLedger.Helpers;
using PasswordLedger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLedger.Model
{
    class Credentials
    {
        private static Credentials instance;

        public static Credentials Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Credentials();
                }
                return instance;
            }
        }

        protected List<Credential> credentials = new List<Credential>();

        string ledgerFile = "";

        public Credentials() { }
        
        public Credentials(List<Credential> credentials) {
            this.credentials = credentials;
        }

        public void SetFileLocation(string lFile) => ledgerFile = lFile;

        public bool IsTitleExists(string Title)
        {
            for (int i = 0; i < credentials.Count; i++)
                if (credentials[i].Title == Title)
                    return true;
            return false;
        }

        public bool Add(Credential credential)
        {
            bool canAdd = !IsTitleExists(credential.Title);

            if (canAdd)
            {
                credentials.Add(credential);
                SaveListToFile();
                return true;
            }
            return false;
        }

        public bool Remove(Credential credential)
        {
            if(credentials.Contains(credential))
            {
                credentials.Remove(credential);
                return true;
            }
            return false;
        }

        public Credential Get(string Title)
        {
            int index = 0;
            foreach (var credential in credentials)
            {
                if (credential.Title == Title)
                {
                    return credentials[index];
                    break;
                }
                index++;
            }
            return null;
        }


        public List<Credential> GetCredentials()
        {
            return credentials;
        }

        public bool Remove(string Title)
        {
            int index = 0;
            foreach (var credential in credentials)
            {
                if(credential.Title == Title)
                {
                    credentials.RemoveAt(index);
                    return true;
                    break;
                }
                index++;
            }
            return false;
        }

        public void SetList(List<Credential> credentials) => this.credentials = credentials;

        public void LoadListFromFile()
        {
            FS fs = new FS(this.ledgerFile);
            this.SetList(ModelConverter.JSONToListCredential(fs.ReadFile()));
            fs = null;
            GC.Collect();
        }

        public void SaveListToFile()
        {
            FS fs = new FS(this.ledgerFile);
            string JSONString = ModelConverter.CredentialListToJSON(credentials);
            fs.WriteFile(JSONString);
        }

    }
}
