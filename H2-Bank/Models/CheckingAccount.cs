using System;
using H2_Bank.DAL;

namespace H2_Bank.Models
{
    public class CheckingAccount : Account
    {
        /// <summary>
        /// Laver en lønkonto.
        /// </summary>
        /// <param name="name">Kontoholders navn</param>
        public CheckingAccount(string name)
        {
            AccountHolder = name;
            AccountBalance = 0;
            AccountType = "Lønkonto";
            AccountLimit = -5000;
        }

        public CheckingAccount(string name, int accNum, decimal balance)
        {
            AccountHolder = name;
            AccountBalance = balance;
            AccountType = "Lønkonto";
            AccountLimit = -5000;
            AccountNo = accNum;
        }

        public override void ChargeInterest()
        {
            AccountBalance *= 1.005m;
        }
    }
}
