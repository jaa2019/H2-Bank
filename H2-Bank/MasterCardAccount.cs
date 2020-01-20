using System;
namespace H2_Bank
{
    public class MasterCardAccount : Account
    {
        public MasterCardAccount(string name, int accno)
        {
            AccountHolder = name;
            AccountNo = accno;
            AccountBalance = 0;
        }

        public override void ChargeInterest()
        {
            if (AccountBalance > 0)
            {
                AccountBalance = AccountBalance * 0.001m;
            }
            else
            {
                AccountBalance = AccountBalance / 0.98m;
            }
        }
    }
}
