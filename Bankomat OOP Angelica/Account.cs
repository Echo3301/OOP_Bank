using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankomat_OOP_Angelica_B
{
    public class Account
    {
        //properties för att ta in information om konton
        public string Name { get; set; }
        public decimal Saldo { get; set; }
        public decimal Intrest { get; set; }
        public int MaxKredit { get; set; }
        public int AccountNr { get; set; }
        public int PinNr { get; set; }

        public Account()
        {

        }
        public Account(string name, decimal saldo, decimal intrest, int maxKredit, int accountNr, int pinNr)
        {
            Name = name;
            Saldo = saldo;
            Intrest = intrest;
            MaxKredit = maxKredit;
            AccountNr = accountNr;
            PinNr = pinNr;
        }
    }

}
