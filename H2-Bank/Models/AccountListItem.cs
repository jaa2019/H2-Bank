using System;
using System.Collections.Generic;
using H2_Bank.BLL;

namespace H2_Bank.Models
{
    public class AccountListItem
    {
        public string AccountHolder { get; }       // Navn på kontoholder
        public string AccountType { get; }         // Kontotype
        public decimal AccountBalance { get; }     // Kontobalance, bliver sat til 0
        public int AccountNo { get; }              // Kontonummer

        /// <summary>
        /// Er ikke helt sikker på hvad det precist er denne gør. Tilføjer vistnok parametre til nye konti?
        /// </summary>
        /// <param name="acc">Konto (object)</param>
        public AccountListItem(Account acc)
        {
            AccountHolder = acc.AccountHolder;
            AccountType = acc.AccountType;
            AccountBalance = acc.AccountBalance;
            AccountNo = acc.AccountNo;
        }
    }
}
