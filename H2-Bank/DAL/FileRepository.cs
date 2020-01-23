using System;
using System.Collections.Generic;
using System.IO;
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
            List<AccountListItem> GUIList = new List<AccountListItem>();
            foreach (Account item in accountList)
            {
                GUIList.Add(new AccountListItem(item));
            }
            return GUIList;
        }

        public List<Account> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public void LoadBank()
        {
            DirectoryInfo findBankAcc = new DirectoryInfo(Environment.CurrentDirectory);
            findBankAcc.GetFiles();
            foreach (FileInfo filer in findBankAcc.GetFiles())
            {
                if (filer.Extension == ".data")
                {
                    string foundFile = File.ReadAllText(filer.FullName);
                    string[] parseFile = foundFile.Split(':');
                    for (int q = 0; q < parseFile.Length; q++)
                    {
                        string[] parseACC = parseFile[q].Split(';');

                        if (parseACC[1] == "Lønkonto")
                        {
                            accountList.Add(new CheckingAccount(parseACC[0], parseACC[1], Convert.ToInt16(parseACC[2]), Convert.ToDecimal(parseACC[3]), Convert.ToDecimal(parseACC[4])));
                        }
                        else if (parseACC[1] == "Opsparingskonto")
                        {
                            accountList.Add(new SavingsAccount(parseACC[0], parseACC[1], Convert.ToInt16(parseACC[2]), Convert.ToDecimal(parseACC[3]), Convert.ToDecimal(parseACC[4])));
                        }
                        else if (parseACC[1] == "Kreditkortkonto")
                        {
                            accountList.Add(new MasterCardAccount(parseACC[0], parseACC[1], Convert.ToInt16(parseACC[2]), Convert.ToDecimal(parseACC[3]), Convert.ToDecimal(parseACC[4])));
                        }
                    }
                }
            }
        }

        public void SaveBank()
        {
            string bankAccounts = "";
            foreach (Account item in accountList)
            {
                bankAccounts += item.AccountHolder + ";" + item.AccountType + ";" + item.AccountNo + ";" + item.AccountLimit + ";" + item.AccountBalance + ":";
            }
            bankAccounts = bankAccounts.TrimEnd(':');
            File.WriteAllText(fileName, bankAccounts);
        }

        public void UpdateAccount(Account acc)
        {
            throw new NotImplementedException();
        }
    }
}
