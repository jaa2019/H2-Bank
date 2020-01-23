using System;
namespace H2_Bank.Models
{
    public class MasterCardAccount : Account
    {
        /// <summary>
        /// Opretter en kredit-konto
        /// </summary>
        /// <param name="name">Navn på kontoholder</param>
        public MasterCardAccount(string name)
        {
            AccountHolder = name;
            AccountBalance = 0;
            AccountType = "Kreditkortkonto";
            AccountLimit = -20000;
        }

        /// <summary>
        /// Opretter en kredit-konto efter indlæsning af datafil
        /// </summary>
        /// <param name="name">Navn på kontoholder</param>
        /// <param name="accNum">Kontonummer</param>
        /// <param name="balance">Balance på konto</param>
        public MasterCardAccount(string name, int accNum, decimal balance)
        {
            AccountHolder = name;
            AccountBalance = balance;
            AccountType = "Kreditkortkonto";
            AccountLimit = -20000;
            AccountNo = accNum;
        }

        /// <summary>
        /// Tilskriver rente efter fastsatte parametre
        /// </summary>
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
