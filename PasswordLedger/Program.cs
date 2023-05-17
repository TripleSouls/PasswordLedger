using PasswordLedger;
using PasswordLedger.Helpers;
using PasswordLedger.Model;
using PasswordLedger.Utils;

public class Program
{

    public static void Main()
    {
        ProgramUI programUI = new ProgramUI();
        
        Bootstrap bootstrap = new Bootstrap(Functions.GetExePath(), "ledger.ledger");
        bootstrap.Start();

        // Ana lokasyonu tanimla
        Credentials.Instance.SetFileLocation(bootstrap.GetLedgerFile());
        // Dosyadan oku ve credentialse kaydet
        Credentials.Instance.LoadListFromFile();

        programUI.MainUI();
    }
}
