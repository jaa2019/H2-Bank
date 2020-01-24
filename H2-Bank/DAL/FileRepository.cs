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

        /// <summary>
        /// Opretter en liste af konti
        /// </summary>
        public FileRepository()
        {
            accountList = new List<Account>();
        }

        /// <summary>
        /// Tilføjer nye konti til listen
        /// </summary>
        /// <param name="account">Konto (object)</param>
        /// <returns></returns>
        public int AddAccount(Account account)
        {
            ++accountNumberCounter;
            account.AccountNo = accountNumberCounter;
            accountList.Add(account);
            return account.AccountNo;
        }

        /// <summary>
        /// Henter konto på kontonummer
        /// </summary>
        /// <param name="id">ID på konto (nummer)</param>
        /// <returns>Konto (objekt)</returns>
        public Account GetAccount(int id)
        {
            Account searchAcc = accountList.Find(s => s.AccountNo == id);
            return searchAcc;
        }

        /// <summary>
        /// Fremtryller en liste over alle konti
        /// </summary>
        /// <returns>List af typen "AccountListItem"</returns>
        public List<AccountListItem> GetAccountList()
        {
            List<AccountListItem> GUIList = new List<AccountListItem>();
            foreach (Account item in accountList)
            {
                GUIList.Add(new AccountListItem(item));
            }
            return GUIList;
        }

        /// <summary>
        /// Henter alle konti - ej implementeret TODO
        /// </summary>
        /// <returns>Liste af typen "Account"</returns>
        public List<Account> GetAllAccounts()
        {
            List<Account> result = new List<Account>();
            //foreach (Account item in accountList)
            //{
            //    result.Add(new Account(item));
            //}
            return result;
        }

        /// <summary>
        /// Indlæser banken fra datafil.data
        /// </summary>
        public void LoadBank()
        {
            // Kigger i nuværende directory
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
                            accountList.Add(new CheckingAccount(parseACC[0], Convert.ToInt16(parseACC[2]), Convert.ToDecimal(parseACC[3])));
                        }
                        else if (parseACC[1] == "Opsparingskonto")
                        {
                            accountList.Add(new SavingsAccount(parseACC[0], Convert.ToInt16(parseACC[2]), Convert.ToDecimal(parseACC[3])));
                        }
                        else if (parseACC[1] == "Kreditkortkonto")
                        {
                            accountList.Add(new MasterCardAccount(parseACC[0], Convert.ToInt16(parseACC[2]), Convert.ToDecimal(parseACC[3])));
                        }
                    }
                    accountNumberCounter = parseFile.Length;
                }
            }
        }

        /// <summary>
        /// Gemmer banken ved soft-exit
        /// </summary>
        public void SaveBank()
        {
            string bankAccounts = "";
            foreach (Account item in accountList)
            {
                bankAccounts += item.AccountHolder + ";" + item.AccountType + ";" + item.AccountNo + ";" + item.AccountBalance + ":";
            }
            bankAccounts = bankAccounts.TrimEnd(':');
            File.WriteAllText(fileName, bankAccounts);
        }

        /// <summary>
        /// Opdaterer saldo på konto i CSV-fil
        /// </summary>
        /// <param name="acc">Konto</param>
        public void UpdateAccount(Account acc)
        {
            SaveBank();
        }
    }
}
