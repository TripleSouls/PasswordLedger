using PasswordLedger.Model;
using PasswordLedger.Utils;

public class Program
{
    public static void Main()
    {
        Bootstrap bootstrap = new Bootstrap(Functions.GetExePath(), "ledger.ledger");
        bootstrap.Start();

        Credentials credentials = new Credentials();
        credentials.SetList(bootstrap.GetCredentials());

        credentials.credentials.ForEach(x =>  Console.WriteLine(x.Title + " " + x.Username + " -> " + x.Password));

    }
}
