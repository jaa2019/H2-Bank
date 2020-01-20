using System;
namespace H2_Bank
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(string name, int accno)
        {
            AccountHolder = name;
            AccountNo = accno;
            AccountBalance = 0;
        }

        public override void ChargeInterest()
        {
            AccountBalance = AccountBalance * 0.05m;
        }
    }
}
