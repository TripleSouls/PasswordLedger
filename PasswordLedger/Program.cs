using PasswordLedger.Helpers;
using PasswordLedger.Model;
using PasswordLedger.Utils;

public class Program
{
    public static void Main()
    {
        Bootstrap bootstrap = new Bootstrap(Functions.GetExePath(), "ledger.ledger");
        bootstrap.Start();

        Credentials credentials = new Credentials();
        credentials.SetFileLocation(bootstrap.GetLedgerFile());
        credentials.LoadListFromFile();
        credentials.credentials.ForEach(x =>  Console.WriteLine(x.Title + " " + x.Username + " -> " + x.Password));

        string data = ModelConverter.CredentialListToJSON(credentials.credentials);
        Console.WriteLine(data);

        Security s = new Security("test");
        Console.WriteLine(s.Encode(data));
        Console.WriteLine(s.Decode(s.Encode(data)));
    }
}
