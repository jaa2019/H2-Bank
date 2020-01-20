using System;
namespace H2_Bank
{
    public class SavingsAccount : Account
    {
        /// <summary>
        /// Opretter en opsparingskonto
        /// </summary>
        /// <param name="name"></param>
        public SavingsAccount(string name, int accno)
        {
            AccountHolder = name;
            AccountNo = accno;
            AccountBalance = 0;
            AccountType = "Opsparingskonto";
        }

        public override void ChargeInterest()
        {
            if (AccountBalance < 50000)
            {
                AccountBalance = AccountBalance * 0.01m;
            }
            else if (AccountBalance > 50000 && AccountBalance < 100000)
            {
                AccountBalance = AccountBalance * 0.02m;
            }
            else if (AccountBalance > 100000)
            {
                AccountBalance = AccountBalance * 0.03m;
            }
        }
    }
}
