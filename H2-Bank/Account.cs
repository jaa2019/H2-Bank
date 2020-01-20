using System;
namespace H2_Bank
{
    public class Account
    {
        public string Name { get; }
        public decimal BankBalance { get; set; }
        public int AccountNo { get; }

        public Account(string name, int accno)
        {
            Name = name;
            BankBalance = 0;
            AccountNo = accno;
        }
    }
}
