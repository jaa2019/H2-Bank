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

        public AccountListItem(Account acc)
        {
            AccountHolder = acc.AccountHolder;
            AccountType = acc.AccountType;
            AccountBalance = acc.AccountBalance;
            AccountNo = acc.AccountNo;
        }
    }
}
