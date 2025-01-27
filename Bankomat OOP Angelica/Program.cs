using System.Security.Principal;
using Bankomat_OOP_Angelica_B;

namespace Bankomat_OOP_Angelica_B
{
    internal class Program
    {
        public static List<Account> AccountInfo = new List<Account>();

        static void Main(string[] args)
        {
            //Sample data
            Account account0 = new("Jenny", 7000, 1, 2300, 456987, 5447);
            Account account1 = new("Malin", 6000, 5, 400, 156982, 1111);
            Account account2 = new("Fredrik", 16000, 2, 4000, 690087, 1439);
            Account account3 = new("Robin", 10000, 3, 2000, 456357, 5637);

            AccountInfo.Add(account0);
            AccountInfo.Add(account1);
            AccountInfo.Add(account2);
            AccountInfo.Add(account3);

            bool meny = true;

            while (meny)
            {
                string[] MenuChoise = new string[10];
                MenuChoise[0] = "MENY";
                MenuChoise[1] = "1. Gör en insättning";
                MenuChoise[2] = "2. Gör ett uttag";
                MenuChoise[3] = "3. Visa saldo";
                MenuChoise[4] = "4. Visa kontoinformation";
                MenuChoise[5] = "5. Visa alla konton";
                MenuChoise[6] = "6. Skapa nytt konto";
                MenuChoise[7] = "7. Utdelning av ränta";
                MenuChoise[8] = "8. Avsluta";

                Metoder.PrintMenu(MenuChoise);
                string val = Console.ReadLine();
                Console.Clear(); //Rensar inför menyval

                switch (val)
                {
                    //Gör en insättning på ett konto
                    case "1":
                        Metoder.Deposit(AccountInfo); //metod
                        Metoder.BackToMenu();
                        break;

                    //Gör ett uttag på ett konto 
                    case "2":
                        Metoder.Withdraw(AccountInfo); //metod
                        Metoder.BackToMenu();
                        break;

                    //Visa saldot på ett konto 
                    case "3":
                        Metoder.Balance(AccountInfo); //metod
                        Metoder.BackToMenu();
                        break;

                    //Skriv ut en lista på alla kontonr och saldon 
                    case "4":
                        Metoder.SingleAccount(AccountInfo); //metod
                        Metoder.BackToMenu();
                        break;

                    //Skriv ut en lista på alla kontonr och saldon
                    //Val av sortering kommer i metoden
                    case "5":
                        Metoder.Accounts(AccountInfo); //metod
                        Metoder.BackToMenu();
                        break;

                    //Skapa nytt konto 
                    case "6":
                        AccountInfo.Add(Metoder.CreateAccount());//metod
                        Metoder.BackToMenu();
                        break;

                    //Skriv ut en lista på alla kontonr och saldon 
                    case "7":
                        Metoder.IntrestAll(AccountInfo); //metod
                        Metoder.BackToMenu();
                        break;

                    //Avsluta programmet 
                    case "8":
                        meny = false;
                        break;

                    //Skrivs ut om användaren uppger ett felaktigt menyval
                    default:
                        Console.WriteLine("Ogilltigt val, försök igen");

                        break;
                }
            }
        }
    }
}
