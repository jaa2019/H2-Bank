using System;
namespace H2_Bank.Models
{
    public class CheckingAccount : Account
    {
        /// <summary>
        /// Laver en lønkonto.
        /// </summary>
        /// <param name="name">Kontoholders navn</param>
        /// <param name="accno">Kontonummer (incrementing constant)</param>
        public CheckingAccount(string name, int accno)
        {
            AccountHolder = name;
            AccountNo = accno;
            AccountBalance = 0;
            AccountType = "Lønkonto";
            AccountLimit = -5000;
        }

        public override void ChargeInterest()
        {
            AccountBalance *= 1.005m;
        }
    }
}
