using System;
using H2_Bank.DAL;

namespace H2_Bank.Models
{
    public class CheckingAccount : Account
    {
        /// <summary>
        /// Opretter en lønkonto
        /// </summary>
        /// <param name="name">Navn på kontoholder</param>
        public CheckingAccount(string name)
        {
            AccountHolder = name;
            AccountBalance = 0;
            AccountType = "Lønkonto";
            AccountLimit = -5000;
        }


        /// <summary>
        /// Opretter en lønkonto efter import af datafil
        /// </summary>
        /// <param name="name">Navn på kontoholder</param>
        /// <param name="accNum">Kontonummer</param>
        /// <param name="balance">Balance på konto</param>
        public CheckingAccount(string name, int accNum, decimal balance)
        {
            AccountHolder = name;
            AccountBalance = balance;
            AccountType = "Lønkonto";
            AccountLimit = -5000;
            AccountNo = accNum;
        }

        /// <summary>
        /// Tilskriver rente efter fastsatte parametre
        /// </summary>
        public override void ChargeInterest()
        {
            AccountBalance *= 1.005m;
        }
    }
}
