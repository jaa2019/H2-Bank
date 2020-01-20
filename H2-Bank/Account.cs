using System;
namespace H2_Bank
{
    public abstract class Account
    {
        public string AccountHolder { get; set; }
        public decimal AccountBalance { get; set; }
        public int AccountNo { get; set; }

        public abstract void ChargeInterest();
        
    }
}


//public Account(string name, int accno)
//{
//    Name = name;
//    BankBalance = 0;
//    AccountNo = accno;
//}