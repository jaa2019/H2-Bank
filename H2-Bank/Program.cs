using System;

namespace H2_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            string hardcode;

            Bank myBank = new Bank("Jan's Bank");
            Console.WriteLine("******** Velkommen til {0} - Bank 1 ********", myBank.BankName);
            Console.Write("Opret konto, indtast navn: ");
            hardcode = Console.ReadLine();
            myBank.CreateAccount(hardcode);
            Console.WriteLine();
            Console.WriteLine("Saldoen på kontoen {0} er som følger:", hardcode);
            Console.WriteLine(myBank.Balance());
            Console.WriteLine("Hvor meget vil du indsætte på din konto?");
            Console.Write("Indtast beløb (kun tal!!!): ");
            myBank.Deposit(Convert.ToDecimal(Console.ReadLine()));
            Console.WriteLine();
            Console.WriteLine("Saldoen på kontoen {0} er som følger:", hardcode);
            Console.WriteLine(myBank.Balance());
            Console.WriteLine("Hvor meget vil du hæve på din konto?");
            Console.Write("Indtast beløb (kun tal!!!)");
            myBank.Withdraw(Convert.ToDecimal(Console.ReadLine()));
            Console.WriteLine(myBank.Balance());
        }
    }
}
