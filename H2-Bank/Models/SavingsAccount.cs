using System;
namespace H2_Bank.Models
{
    public class SavingsAccount : Account
    {
        /// <summary>
        /// Opretter en opsparingskonto
        /// </summary>
        /// <param name="name">Navn på kontohaver</param>
        public SavingsAccount(string name)
        {
            AccountHolder = name;
            AccountBalance = 0;
            AccountType = "Opsparingskonto";
            AccountLimit = 0;
        }

        /// <summary>
        /// Opretter en opsparingskonto fra datafil
        /// </summary>
        /// <param name="name">Navn på kontohaver</param>
        /// <param name="accNum">Kontonummer</param>
        /// <param name="balance">Balancen på konto</param>
        public SavingsAccount(string name, int accNum, decimal balance)
        {
            AccountHolder = name;
            AccountBalance = balance;
            AccountType = "Opsparingskonto";
            AccountLimit = 0;
            AccountNo = accNum;
        }

        /// <summary>
        /// Tilskriver rente på konti efter fastsatte parametre
        /// </summary>
        public override void ChargeInterest()
        {
            if (AccountBalance < 50000)
            {
                AccountBalance *= 1.01m;
            }
            else if (AccountBalance > 50000 && AccountBalance < 100000)
            {
                AccountBalance *= 1.02m;
            }
            else if (AccountBalance > 100000)
            {
                AccountBalance *= 1.03m;
            }
        }
    }
}
