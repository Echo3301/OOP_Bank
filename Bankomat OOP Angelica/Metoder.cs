using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bankomat_OOP_Angelica_B
{
    internal partial class Metoder
    {
        //Metod som genererar nya 6 siffriga kontonummer till användaren
        public static int AccountNrGenerator()
        {
            Random RandomAccountNr = new Random();
            int AccountNr = RandomAccountNr.Next(111111, 999999);
            return AccountNr;
        }

        //Metod som genererar ny 4 siffrig pinkod till användaren
        public static int PinNrGenerator()
        {
            Random RandomPinNr = new Random();
            int PinNr = RandomPinNr.Next(1111, 9999);
            return PinNr;
        }

        //Metod som visar användaren tillbaka till menyn
        public static void BackToMenu()
        {
            Console.WriteLine("\nTryck valfri tangent för att återgå till meny");
            Console.ReadKey();
            Console.Clear();
        }
        
        //Metod som skriver ut menyn
        public static void PrintMenu(String[] menuItems)
        {
            String topLeft = "╔";
            String topRight = "╗";
            String middleLeft = "╠";
            String middleRight = "╣";
            String bottomLeft = "╚";
            String bottomRight = "╝";
            String horizontal = "═";
            String vertical = "║";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(topLeft);
            for (int i = 0; i < 27; i++)
            {
                Console.Write(horizontal);
            }
            Console.WriteLine(topRight);
            Console.WriteLine(vertical + $" {menuItems[0]} ".PadRight(27) + vertical);
            Console.Write(middleLeft);
            for (int i = 0; i < 27; i++)
            {
                Console.Write(horizontal);
            }
            Console.WriteLine(middleRight);

            for (int i = 1; i < menuItems.Length; i++)
            {
                Console.WriteLine(vertical + $"{menuItems[i]}".PadRight(27) + vertical);
            }

            Console.Write(bottomLeft);
            for (int i = 0; i < 27; i++)
            {
                Console.Write(horizontal);
            }
            Console.WriteLine(bottomRight);
            Console.WriteLine("\nGör ett menyval och tryck enter:");
        }

        //Metod för insättning
        public static void Deposit(List<Account> accounts)
        {
            Console.WriteLine("Ange kontonummer: ");
            //kollar så att rätt kontonummer skrivs
            int findAccountNr;
            //kontrollerar om användaren har matat in ett ogiltigt tal
            if (!int.TryParse(Console.ReadLine(), out findAccountNr))
            {
                Console.WriteLine("\nOgiltigt kontonummer. Försök igen.");
                return;
            }
            //kollar om kontot användaren angav finns i listan
            var foundAccount = accounts.FirstOrDefault(a => a.AccountNr == findAccountNr);
            //om vi inte hittat kontot
            if (foundAccount == null)
            {
                Console.WriteLine("\nKontot kunde inte hittas");
                return;
            }

            //en for-loop för att kunna kolla igenom alla konton i listan
            for (int i = 0; i < accounts.Count; i++)
            {

                Console.WriteLine("Ange pinkod: ");
                int findPinNr;
                //kontrollerar om användaren har matat in ett ogiltigt tal
                if (!int.TryParse(Console.ReadLine(), out findPinNr))
                {
                    Console.WriteLine("Ogiltig pinkod. Försök igen.");
                    return;
                }
                if (findPinNr != foundAccount.PinNr)
                {
                    Console.WriteLine("\nOgiltig pinkod. Försök igen.");
                    return;
                }
                if (findPinNr == foundAccount.PinNr)
                {
                    Console.WriteLine($"Ange summa av insättning:");
                    //val av insättning 
                    string insats = Console.ReadLine();

                    if (!string.IsNullOrEmpty(insats))
                    {
                        //val av insättning omvandlad till decimal och uträknas
                        decimal saldoDep = Convert.ToDecimal(insats);
                        foundAccount.Saldo += saldoDep;
                        Console.WriteLine($"\nInsättningen är genomförd\n" +
                            $"Ditt banksaldo är nu {foundAccount.Saldo} SEK");
                        break;
                    }
                    //skrivs ut om summa inte anges
                    else
                    {
                        Console.WriteLine("\nSumma måste anges.");
                        break;
                    }
                }

            }
        }

        //Metod för uttag
        public static void Withdraw(List<Account> accounts)
        {
            Console.WriteLine("Ange kontonummer: ");
            //kollar så att rätt kontonummer skrivs
            int findAccountNr;
            //kontrollerar om användaren har matat in ett ogiltigt tal
            if (!int.TryParse(Console.ReadLine(), out findAccountNr))
            {
                Console.WriteLine("\nOgiltigt kontonummer. Försök igen.");
                return;
            }
            //kollar om kontot användaren angav finns i listan
            var foundAccount = accounts.FirstOrDefault(a => a.AccountNr == findAccountNr);

            //om vi inte hittat kontot
            if (foundAccount == null)
            {
                Console.WriteLine("\nKontot kunde inte hittas");
                return;
            }

            //en for-loop för att kunna kolla igenom alla konton i listan
            for (int i = 0; i < accounts.Count; i++)
            {

                Console.WriteLine("Ange pinkod: ");
                int findPinNr;
                if (!int.TryParse(Console.ReadLine(), out findPinNr))
                {
                    Console.WriteLine("Ogiltig pinkod. Försök igen.");
                    return;
                }
                if (findPinNr != foundAccount.PinNr)
                {
                    Console.WriteLine("\nOgiltig pinkod. Försök igen.");
                    return;
                }

                Console.WriteLine($"Ange summa av uttag:");
                //tar in summa av uttag
                string uttag = Console.ReadLine();
                //kontrollera om input är null eller tom
                if (!string.IsNullOrEmpty(uttag))
                {
                    decimal saldoWit = Convert.ToDecimal(uttag);
                    //anger högsta värde av uttaget
                    decimal saldoMin = saldoWit + (foundAccount.MaxKredit * -1);

                    //kollar så uttaget inte är större än saldot
                    if (foundAccount.Saldo >= saldoMin)
                    {
                        foundAccount.Saldo -= saldoWit;
                        Console.WriteLine($"\nUttaget är genomfört\n" +
                            $"Ditt banksaldo är nu {foundAccount.Saldo} SEK");
                        break;
                    }
                    //skrivs ut om saldot är mindre än uttaget
                    else
                    {
                        Console.WriteLine("Kontot saknar balans för uttaget.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Summa av uttag måste anges.");
                    break;
                }
            }
        }

        //Metod för att skriva ut saldo
        public static void Balance(List<Account> accounts)
        {
            Console.WriteLine("Ange kontonummer: ");
            //kollar så att rätt kontonummer skrivs
            int findAccountNr;
            //kontrollerar om användaren har matat in ett ogiltigt tal
            if (!int.TryParse(Console.ReadLine(), out findAccountNr))
            {
                Console.WriteLine("\nOgiltigt kontonummer. Försök igen.");
                return;
            }
            //kollar om kontot användaren angav finns i listan
            var foundAccount = accounts.FirstOrDefault(a => a.AccountNr == findAccountNr);
            //om vi inte hittat kontot
            if (foundAccount == null)
            {
                Console.WriteLine("\nKontot kunde inte hittas");
                return;
            }

            //en for-loop för att kunna kolla igenom alla konton i listan
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine("Ange pinkod: ");
                int findPinNr;
                //kontrollerar om användaren har matat in ett ogiltigt tal
                if (!int.TryParse(Console.ReadLine(), out findPinNr))
                {
                    Console.WriteLine("Ogiltig pinkod. Försök igen.");
                    return;
                }
                if (findPinNr != foundAccount.PinNr)
                {
                    Console.WriteLine("\nOgiltig pinkod. Försök igen.");
                    return;
                }

                //kollar om användaren angivit rätt pinkod
                if (foundAccount.PinNr == findPinNr)
                {
                    //kollar om vi hittat kontot
                    if (foundAccount != null)
                    {
                        Console.WriteLine($"\nDitt saldo är: {foundAccount.Saldo}");
                        break;
                    }
                }
            }
        }
        //Metod för att skriva ut ett konto
        public static void SingleAccount(List<Account> accounts)
        {
            Console.WriteLine("Ange kontonummer: ");
            int findAccountNr = Convert.ToInt32(Console.ReadLine());
            //håller koll på om kontot hittades
            bool accountFound = false;

            //en for-loop för att kunna kolla igenom alla konton i listan
            for (int i = 0; i < accounts.Count; i++)
            {
                //kollar om kontot användaren angav finns i listan
                var foundAccount = accounts.FirstOrDefault(a => a.AccountNr == findAccountNr);

                //kollar om vi hittat kontot
                if (foundAccount != null)
                {
                    Console.WriteLine($"\nKONTOINFORMATION\n");
                    Console.WriteLine($"Kontoinnehavare: {foundAccount.Name}");
                    Console.WriteLine($"Saldo: {foundAccount.Saldo}");
                    Console.WriteLine($"Räntesats: {foundAccount.Intrest}%");
                    Console.WriteLine($"Maxkredit: {foundAccount.MaxKredit}");
                    Console.WriteLine($"Din pinkod är: {foundAccount.PinNr}");

                    Console.WriteLine($"\nFrågor kring ditt konto eller pinkod,\n" +
                        $"var god och kontakta din bank på tel: 073******9");
                    break;
                }
                //skrivs ut om kontot inte kunde hittas
                if (!accountFound)
                {
                    Console.WriteLine("\nKontot kunde inte hittas");
                    BackToMenu();
                    break;
                }

            }
        }
        //Metod för att skriva ut alla konton
        public static void Accounts(List<Account> accounts)
        {
            //menyval beroende på hur användaren vill att informationen visas
            Console.WriteLine("" +
                "1. Visa alla konton i fallande ordning\n" +
                "2. Visa alla konton i stigande ordning");
            Console.WriteLine("\nGör ett menyval och tryck enter:\n");

            //header för att ge info om värden
            Console.WriteLine("ALLA KONTON");
            Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}", "Kontohavare", "Saldo", "Ränta", "Maxkredit", "Kontonummer");
            Console.WriteLine(new string('-', 80));

            //en foreach-loop för att skriva ut alla i listan
            foreach (var account in accounts)
            {
                Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}", account.Name, account.Saldo + " SEK", account.Intrest + "%", account.MaxKredit + " SEK", account.AccountNr);
            }
            //tar input om fallande lr stigande lista
            string val = Console.ReadLine();

            switch (val)
            {
                //visar konton i fallande ordning 
                case "1":
                    //en foreach-loop för att skriva ut alla i listan
                    Console.WriteLine("" +
                        "1. Visa utifrån saldo\n" +
                        "2. Visa utifrån ränta");
                    Console.WriteLine("\nGör ett menyval och tryck enter:");

                    string sortBy = Console.ReadLine();

                    //visar konton i fallande ordning utifrån saldo
                    if (sortBy == "1")
                    {
                        Console.WriteLine("\nALLA KONTON I FALLANDE ORDNING UTIFRÅN SALDO");
                        Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}", "Kontohavare", "Saldo", "Ränta", "Maxkredit", "Kontonummer");
                        Console.WriteLine(new string('-', 80));

                        //sorterar alla i listan i fallande ordning
                        accounts.Sort((x, y) => y.Saldo.CompareTo(x.Saldo));
                        //en foreach-loop för att skriva ut alla i listan
                        foreach (var account in accounts)
                        {
                            Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}", account.Name, account.Saldo + " SEK", account.Intrest + "%", account.MaxKredit + " SEK", account.AccountNr);
                        }
                        break;
                    }
                    //visar konton i fallande ordning utifrån ränta
                    if (sortBy == "2")
                    {
                        Console.WriteLine("\nALLA KONTON I FALLANDE ORDNING UTIFRÅN RÄNTA");
                        Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}", "Kontohavare", "Saldo", "Ränta", "Maxkredit", "Kontonummer");
                        Console.WriteLine(new string('-', 80));
                        //sorterar alla i listan i fallande ordning
                        accounts.Sort((x, y) => y.Intrest.CompareTo(x.Intrest));
                        //en foreach-loop för att skriva ut alla i listan
                        foreach (var account in accounts)
                        {
                            Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}", account.Name, account.Saldo + " SEK", account.Intrest + "%", account.MaxKredit + " SEK", account.AccountNr);
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ogilltigt val");
                    }
                    break;

                //visa alla konton i stigande ordning 
                case "2":
                    Console.WriteLine("" +
                        "1. Visa utifrån saldo\n" +
                        "2. Visa utifrån ränta");
                    Console.WriteLine("\nGör ett menyval och tryck enter:");
                    string sortByInt = Console.ReadLine();
                    if (sortByInt == "1")
                    {
                        Console.WriteLine("\nALLA KONTON I STIGANDE ORDNING UTIFRÅN SALDO");
                        Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}", "Kontohavare", "Saldo", "Ränta", "Maxkredit", "Kontonummer");
                        Console.WriteLine(new string('-', 80));
                        //sorterar alla i listan i stigande ordning utifrån saldo
                        var risingAccounts = accounts.OrderBy(a => a.Saldo);
                        //en foreach-loop för att kunna skriva ut alla i listan
                        foreach (var account in risingAccounts)
                        {
                            Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}", account.Name, account.Saldo + " SEK", account.Intrest + "%", account.MaxKredit + " SEK", account.AccountNr);
                        }
                        break;
                    }
                    if (sortByInt == "2")
                    {
                        Console.WriteLine("\nALLA KONTON I STIGANDE ORDNING UTIFRÅN RÄNTA");
                        Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}", "Kontohavare", "Saldo", "Ränta", "Maxkredit", "Kontonummer");
                        Console.WriteLine(new string('-', 80));
                        //sorterar alla i listan i stigande ordning utifrån ränta
                        var risingAccounts = accounts.OrderBy(a => a.Intrest);
                        //en foreach-loop för att kunna skriva ut alla i listan
                        foreach (var account in risingAccounts)
                        {
                            Console.WriteLine("{0,-15}{1,-15}{2,-10}{3,-15}{4,-15}" +
                                "" , account.Name, account.Saldo + " SEK", account.Intrest + "%", account.MaxKredit + " SEK", account.AccountNr);
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ogilltigt val");
                        BackToMenu();
                    }
                    break;

                //skrivs ut om användaren uppger ett felaktigt menyval
                default:
                    Console.WriteLine("Ogilltigt val, försök igen");
                    break;
            }
        }

        //Metod för att skapa nytt konto
        public static Account CreateAccount()
        {
            //tar in nödvändig information för att skapa kontot
            Console.WriteLine("Ange kontoinehavarens namn: ");
            string name = Console.ReadLine().ToLower();
            Console.WriteLine("Ange summa för insättning: ");
            decimal saldo = 0;
            try 
            {
            saldo = Convert.ToDecimal(Console.ReadLine());                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Du kan endast använda siffror");
                saldo = Convert.ToDecimal(Console.ReadLine());
            }
            Console.WriteLine("Ange fri räntesats: ");
            decimal intrest = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Ange din maxkredit mellan 0 och 10000: ");
            int maxKredit = Convert.ToInt32(Console.ReadLine());
            //kollar att användaren inte anger för hög kredit
            if (maxKredit > 10000)
            {
                Console.WriteLine("Den högsta krediten är 10000 SEK");
                maxKredit = 10000;
            }
            Console.WriteLine($"Din maxkredit är: {maxKredit} SEK");
            //skapar ett slumpmässigt sexsiffrigt kontonummer
            int accountNr = AccountNrGenerator();
            Console.WriteLine($"Ditt kontonummer är: {accountNr}");
            //skapar en slumpmässig fyrasiffrig pinkod
            int pinNr = Metoder.PinNrGenerator();
            Console.WriteLine($"Din pinkod är: {pinNr}");

            //retunerar till listan
            return new Account
            {
                Name = name,
                Saldo = saldo,
                Intrest = intrest,
                MaxKredit = maxKredit,
                AccountNr = accountNr,
                PinNr = pinNr
            };

        }
        //Metod för att ge ränteutdelning
        public static void IntrestAll(List<Account> accounts)
        {
            Console.WriteLine("\nALLA KONTON INNAN RÄNTEUTDELNING");
            Console.WriteLine("{0,-15}{1,-15}{2,-15}", "Kontonummer", "Saldo", "Ränta");
            Console.WriteLine(new string('-', 60));
            //en foreach-loop för att skriva ut alla i listan för att enkelt se skillnad mellan innan och efter
            foreach (var account in accounts)
            {
                Console.WriteLine("{0,-15}{1,-15}{2,-15}", account.AccountNr, account.Saldo + " SEK", account.Intrest + "%");
            }
            Console.WriteLine(" ");
            //en foreach-loop för att ge alla saldon sin ränteutdelning
            foreach (var account in accounts)
            {
                decimal intrestRate = account.Saldo * (account.Intrest / 100);
                account.Saldo += intrestRate;
            }
            Console.WriteLine("\nALLA KONTON EFTER RÄNTEUTDELNING");
            Console.WriteLine("{0,-15}{1,-15}{2,-15}", "Kontonummer", "Saldo", "Ränta");
            Console.WriteLine(new string('-', 60));
            //en foreach-loop för att skriva ut alla i listan för att enkelt se skillnad mellan innan och efter
            foreach (var account in accounts)
            {
                Console.WriteLine("{0,-15}{1,-15}{2,-15}", account.AccountNr, account.Saldo + " SEK", account.Intrest + "%");
            }
            Console.WriteLine("\nUtdelning genomförd");
        }
    }
}
