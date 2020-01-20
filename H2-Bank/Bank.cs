using System;
namespace H2_Bank
{
    public class Bank
    {
        public string BankName { get; }
        public Account Konto { get; set; }

        public Bank(string name)
        {
            BankName = name;
        }

        public void CreateAccount(string navn)
        {
            Konto = new Account(navn);
        }

        public void Deposit(decimal amount)
        {
            Konto.BankBalance += amount;
        }

        public void Withdraw(decimal amount)
        {
            Konto.BankBalance -= amount;
        }

        public decimal Balance()
        {
            return Konto.BankBalance;
        }
    }
}
