using System;
namespace H2_Bank
{
    public class CheckingAccount : Account
    {
        /// <summary>
        /// Laver en lønkonto.
        /// </summary>
        /// <param name="name">Kontoholders navn</param>
        public CheckingAccount(string name, int accno)
        {
            AccountHolder = name;
            AccountNo = accno;
            AccountBalance = 0;
            AccountType = "Lønkonto";
        }

        public override void ChargeInterest()
        {
            AccountBalance = AccountBalance * 1.005m;
        }
    }
}
