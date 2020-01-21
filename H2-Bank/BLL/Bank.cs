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
        public string CreateAccount(string navn, AccountType type)
        {
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
        /// Hæver penge på kontoen
        /// </summary>
        /// <param name="amount">Værdi</param>
        /// <param name="accountno">Kontunummer</param>
        public void Withdraw(decimal amount, int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);
            searchAcc.AccountBalance -= amount;
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
    }
}
