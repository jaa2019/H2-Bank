using System;
namespace H2_Bank.Models
{
    public class MasterCardAccount : Account
    {
        /// <summary>
        /// Opretter en kredit-konto
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accno">Kontonummer (incrementing constant)</param>
        public MasterCardAccount(string name)
        {
            AccountHolder = name;
            AccountBalance = 0;
            AccountType = "Kreditkortkonto";
            AccountLimit = -20000;
        }

        public override void ChargeInterest()
        {
            if (AccountBalance > 0)
            {
                AccountBalance *= 1.001m;
            }
            else
            {
                AccountBalance /= 0.8m;
            }
        }
    }
}
