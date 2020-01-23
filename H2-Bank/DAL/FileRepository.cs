using System;
using System.Collections.Generic;
using H2_Bank.Models;
using H2_Bank.Repository;

namespace H2_Bank.DAL
{
    public class FileRepository : iFileRepository
    {
        const string fileName = "datafile.data";
        public List<Account> accountList { get; set; }
        public int accountNumberCounter { get; set; }


        public FileRepository()
        {
            accountList = new List<Account>();
            accountNumberCounter = 1;
        }

        public int AddAccount(Account account)
        {
            ++accountNumberCounter;
            account.AccountNo = accountNumberCounter;
            accountList.Add(account);
            return account.AccountNo;
        }

        public Account GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountListItem> GetAccountList()
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public List<Account> LoadBank()
        {
            throw new NotImplementedException();
        }

        public void SaveBank()
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(Account acc)
        {
            throw new NotImplementedException();
        }
    }
}
