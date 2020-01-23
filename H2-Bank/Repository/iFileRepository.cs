using System;
using System.Collections.Generic;
using H2_Bank.Models;

namespace H2_Bank.Repository
{
    public interface iFileRepository
    {
        int AddAccount(Account account);
        Account GetAccount(int id);
        List<AccountListItem> GetAccountList();
        List<Account> GetAllAccounts();
//        List<Account> LoadBank();
        void SaveBank();
        void UpdateAccount(Account acc);
    }
}
