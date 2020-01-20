using System;
namespace H2_Bank
{
    public class Account
    {
        public string Name { get; }
        public decimal BankBalance { get; set; }

        public Account(string name)
        {
            Name = name;
            BankBalance = 0;
        }
    }
}
