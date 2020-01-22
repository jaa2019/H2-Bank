using System;
using System.Collections.Generic;
using H2_Bank.Models;
using H2_Bank.Repository;

namespace H2_Bank.BLL
{
    public class Bank : iBank
    {
        public string BankName { get; }
        public List<Account> Accounts { get; set; }
        public int AccountNo { get; set; }

        /// <summary>
        /// Instancierer banken
        /// </summary>
        /// <param name="name">Navnet på din bank</param>
        public Bank(string name)
        {
            BankName = name;
            Accounts = new List<Account>();
        }

        /// <summary>
        /// Opretter en konto
        /// </summary>
        /// <param name="navn">Navn på kontoholder</param>
        /// <param name="type">Type af konto</param>
        /// <returns>Friendly-name på kontoen</returns>
        public string CreateAccount(string navn, AccountType type)
        {
            //Forhøjet kontonummer med én
            ++AccountNo;
            if (type == AccountType.checkingAccount)
            {
                Accounts.Add(new CheckingAccount(navn, AccountNo));
                return "Lønkonto";
            }
            else if (type == AccountType.masterCardAccount)
            {
                Accounts.Add(new MasterCardAccount(navn, AccountNo));
                return "Kreditkortkonto";
            }
            else if (type == AccountType.savingsAccount)
            {
                Accounts.Add(new SavingsAccount(navn, AccountNo));
                return "Opsparingskonto";
            }
            return null;
        }

        /// <summary>
        /// Indsætter penge på kontoen
        /// </summary>
        /// <param name="amount">Værdi</param>
        /// <param name="accountno">Kontonummer</param>
        public void Deposit(decimal amount, int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);
            searchAcc.AccountBalance += amount;

        }

        /// <summary>
        /// Hæver penge på kontoen og kontrollerer om der er kredit nok
        /// </summary>
        /// <param name="amount">Værdi</param>
        /// <param name="accountno">Kontunummer</param>
        /// <returns>Returnerer en bolsk værdi til kontrol af om transaktionen bliver gennemført</returns>
        /// Kaser en exception hvis der ikke er nok dækning på kontoen.
        public bool Withdraw(decimal amount, int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);

            if (searchAcc.AccountType == "Lønkonto")
            {
                if (searchAcc.AccountBalance >= (searchAcc.AccountLimit + amount))
                {
                    searchAcc.AccountBalance -= amount;
                    return true;
                }
                else throw new OverdraftException();
            }
            else if (searchAcc.AccountType == "Opsparingskonto")
            {
                if (searchAcc.AccountBalance >= (searchAcc.AccountLimit + amount))
                {
                    searchAcc.AccountBalance -= amount;
                    return true;
                }
                else throw new OverdraftException();
            }
            else if (searchAcc.AccountType == "Kreditkortkonto")
            {
                if (searchAcc.AccountBalance >= (searchAcc.AccountLimit + amount))
                {
                    searchAcc.AccountBalance -= amount;
                    return true;
                }
                else throw new OverdraftException();
            }
            else
            {
                Console.WriteLine("Der må være sket en fjel");
                return false;
            }
        }

        /// <summary>
        /// Viser saldo på den konto der er valgt
        /// </summary>
        /// <param name="accountno">Kont</param>
        /// <returns>Decimal (kontobalance)</returns>
        public decimal Balance(int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);
            return searchAcc.AccountBalance;
        }

        /// <summary>
        /// Tilskriver rente på konti
        /// </summary>
        public void Interest()
        {
            foreach (Account item in Accounts)
            {
                item.ChargeInterest();
            }
        }

        /// <summary>
        /// Går ud af et loop vha. en ConsoleKeyInfo
        /// </summary>
        /// <param name="x">Console.Readkey(true)</param>
        /// <returns>True hvis brugeren taster X</returns>
        /// Christians lille metode :)
        public bool ExitError(ConsoleKeyInfo x)
        {
            if (x.Key == ConsoleKey.X)
            {
                return true;
            }
            return false;
        }
    }
}
