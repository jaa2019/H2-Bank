using System;
using System.Collections.Generic;

namespace H2_Bank
{
    public class Bank
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
        /// <param name="navn">Navnet på kontoen</param>
        public void CreateAccount(string navn)
        {
            ++AccountNo;
            Accounts.Add(new Account(navn, AccountNo));
        }

        /// <summary>
        /// Indsætter penge på kontoen
        /// </summary>
        /// <param name="amount">Værdi</param>
        /// <param name="accountno">Kontonummer</param>
        public void Deposit(decimal amount, int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);
            searchAcc.BankBalance += amount;

        }

        /// <summary>
        /// Hæver penge på kontoen
        /// </summary>
        /// <param name="amount">Værdi</param>
        /// <param name="accountno">Kontunummer</param>
        public void Withdraw(decimal amount, int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);
            searchAcc.BankBalance -= amount;
        }

        /// <summary>
        /// Viser saldo på den konto der er valgt
        /// </summary>
        /// <param name="accountno">Kont</param>
        /// <returns></returns>
        public decimal Balance(int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);
            return searchAcc.BankBalance;
        }
    }
}
