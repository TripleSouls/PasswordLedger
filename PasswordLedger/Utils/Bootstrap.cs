using PasswordLedger.Helpers;
using PasswordLedger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLedger.Utils
{
    internal class Bootstrap
    {

        string path = "";
        string ledgerFileName = "ledger.ledger";
        string ledgerFile = "";

        public Bootstrap(string path, string ledgerFileName)
        {
            this.path = path;
            this.ledgerFileName = ledgerFileName;
            this.ledgerFile = Path.Combine(path, ledgerFileName);

        }

        public void Start()
        {
            try
            {
                OperationResult or;
                or = FS.CreateFolderIfNotExists(path);
                if (!or.Success) { Console.WriteLine("Error:\n" + or.FailureSource + "\n" + or.FailureMessage); throw new Exception(or.FailureMessage);  }
                
                or = FS.CreateFileIfNotExists(this.ledgerFile, "{ \"vals\" : []}");
                if (!or.Success) { Console.WriteLine("Error:\n" + or.FailureSource + "\n" + or.FailureMessage); throw new Exception(or.FailureMessage); }

            }
            catch (Exception ex) { Console.WriteLine(); }
        }

        public string GetLedgerFile() => this.ledgerFile;

    }
}
