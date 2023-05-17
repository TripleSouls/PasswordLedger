using PasswordLedger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLedger
{
    internal class ProgramUI
    {
        public void MainUI()
        {
            int inputValueForMenu;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("0 - Cikis");
                Console.WriteLine("1- Hesap bilgisi Ekle");

                if (Credentials.Instance.GetCredentials().Count > 0)
                {
                    Console.WriteLine("2- Hesap bilgisi Sil");
                    Console.WriteLine("3- Hesap bilgisi Bastır");
                    Console.WriteLine("4- Hesap bilgisi Listele");
                }

                string inputVal = Console.ReadLine().Replace(" ", "");
                inputValueForMenu = Convert.ToInt32(inputVal);

                if (inputValueForMenu < 0 || (Credentials.Instance.GetCredentials().Count>0 ? inputValueForMenu > 4 : inputValueForMenu>1))
                    continue;

                RunMenu(inputValueForMenu);
            }
        }

        private void RunMenu(int inputValueForMenu)
        {
            inputValueForMenu = Math.Clamp(inputValueForMenu, 0, 4);
            switch (inputValueForMenu)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    MenuAdd();
                    break;
                case 2:
                    MenuRemove();
                    break;
                case 3:
                    MenuPrint();
                    break;
                case 4:
                    MenuList();
                    break;
                default:
                    break;
            }
        }

        private void MenuAdd()
        {
            Console.Clear();
            string title;
            string name;
            string pass;

            while (true)
            {
                title = GetValue("Title");
                if (!Credentials.Instance.IsTitleExists(title)) break;
                Console.WriteLine("Bu Title kullanılıyor.");
            }
            name = GetValue("Kullanıcı Adı");
            pass = GetValue("Şifre");

            if (Credentials.Instance.Add(new Credential(title, name, pass)))
                Console.WriteLine("Ekleme basarili.");
            else
                Console.WriteLine("Eklenemedi. Zaten " + title + " icin daha onceden kaydedilmis degerler mevcut.");
        }

        private string GetValue(string label)
        {
            string tempVal = "";
            while (true)
            {
                Console.WriteLine(label + ": ");
                tempVal = Console.ReadLine();
                if (tempVal.Length == 0)
                    continue;
                break;
            }
            return tempVal;
        }

        private void MenuRemove()
        {
            while(true)
            {
                Console.Clear();

                Console.WriteLine("Mevcut Title Listesi:");
                List<Credential> credentialList = Credentials.Instance.GetCredentials();

                for (int i = 0; i < credentialList.Count; i++)
                    Console.WriteLine(i + " - " + credentialList[i].Title);

                Console.WriteLine("Silmek istediginiz Title\'ı secin: (Geri Donmek Icin X Yazınız)");

                string inputVal = Console.ReadLine().Replace(" ", "");

                if (inputVal.ToUpperInvariant() == "X")
                    break;

                int inputValueForMenu = Convert.ToInt32(inputVal);

                if (inputValueForMenu < 0 || inputValueForMenu >= credentialList.Count)
                    continue;

                Credentials.Instance.Remove(credentialList[inputValueForMenu]);
                break;
            }
            
        }

        private void MenuPrint()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Mevcut Title Listesi:");
                List<Credential> credentialList = Credentials.Instance.GetCredentials();

                for (int i = 0; i < credentialList.Count; i++)
                {
                    Console.WriteLine(i + " - " + credentialList[i].Title);
                }

                Console.WriteLine("Bilgilerini ogrenmek istediginiz Title\'ı secin: (Geri Donmek Icin X Yazınız)");

                string inputVal = Console.ReadLine().Replace(" ", "");

                if (inputVal.ToUpperInvariant() == "X")
                    break;

                int inputValueForMenu = Convert.ToInt32(inputVal);

                if (inputValueForMenu < 0 || inputValueForMenu >= credentialList.Count)
                    continue;

                Console.WriteLine(credentialList[inputValueForMenu].Title+"\n|--\tUsername: " + credentialList[inputValueForMenu].Username+"\n|--\tPass: "+ credentialList[inputValueForMenu].Password +"\n");

                Console.WriteLine("Geri Donmek Icin X Yazınız:");

                if (Console.ReadLine().ToUpperInvariant() == "X")
                    break;
            }
        }

        private void MenuList()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Sifresi Cozunmus Halde Liste:");
                List<Credential> credentialList = Credentials.Instance.GetCredentials();

                for (int i = 0; i < credentialList.Count; i++)
                {
                    Console.WriteLine(credentialList[i].Title + "\n|--\tUsername:" + credentialList[i].Username+"\n|--\tPass: " + credentialList[i].Password+"\n");
                }

                Console.WriteLine("Geri Donmek Icin X Yazınız:");

                if (Console.ReadLine().ToUpperInvariant() == "X")
                    break;
            }
        }

    }
}
