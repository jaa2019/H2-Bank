using System;
namespace H2_Bank
{
    public abstract class Account
    {
        public string AccountHolder { get; set; }
        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public int AccountNo { get; set; }


        public abstract void ChargeInterest();
    }
}