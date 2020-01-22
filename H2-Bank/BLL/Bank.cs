using System;
using System.Collections.Generic;
using H2_Bank.Models;
using H2_Bank.Repository;
using H2_Bank.Utilities;
using H2_Bank;

namespace H2_Bank.BLL
{
    delegate void LogHandlerDelegate(string msg);

    public class Bank : iBank
    {
        public string BankName { get; }
        public List<Account> Accounts { get; set; }
        public int AccountNo { get; set; }

        public List<AccountListItem> GetAccountList()
        {
            List<AccountListItem> GUIList = new List<AccountListItem>();
            foreach (Account item in Accounts)
            {
                GUIList.Add(new AccountListItem(item));
            }
            return GUIList;
        }

        LogHandlerDelegate LoghandlerEvent = Bank_LoghandlerEvent;

        /// <summary>
        /// Instancierer banken
        /// </summary>
        /// <param name="name">Navnet på din bank</param>
        public Bank(string name)
        {
            BankName = name;
            Accounts = new List<Account>();
        }

        /// <summary>
        /// Opretter en konto
        /// </summary>
        /// <param name="navn">Navn på kontoholder</param>
        /// <param name="type">Type af konto</param>
        /// <returns>Friendly-name på kontoen</returns>
        public string CreateAccount(string navn, AccountType type)
        {
            //Forhøjet kontonummer med én
            ++AccountNo;
            if (type == AccountType.checkingAccount)
            {
                Accounts.Add(new CheckingAccount(navn, AccountNo));
                LoghandlerEvent("Der er oprettet en " + type.ToString() + " til " + navn);
                return "Lønkonto";
            }
            else if (type == AccountType.masterCardAccount)
            {
                Accounts.Add(new MasterCardAccount(navn, AccountNo));
                LoghandlerEvent("Der er oprettet en " + type.ToString() + " til " + navn);
                return "Kreditkortkonto";
            }
            else if (type == AccountType.savingsAccount)
            {
                Accounts.Add(new SavingsAccount(navn, AccountNo));
                LoghandlerEvent("Der er oprettet en " + type.ToString() + " til " + navn);
                return "Opsparingskonto";
            }
            return null;
        }

        /// <summary>
        /// Indsætter penge på kontoen
        /// </summary>
        /// <param name="amount">Værdi</param>
        /// <param name="accountno">Kontonummer</param>
        public void Deposit(decimal amount, int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);
            searchAcc.AccountBalance += amount;
            LoghandlerEvent("SUCCESS! " + "+" + amount + " - " + searchAcc.AccountHolder + " - " + searchAcc.AccountType + " - " + searchAcc.AccountNo + " = " + searchAcc.AccountBalance);

        }

        /// <summary>
        /// Hæver penge på kontoen og kontrollerer om der er kredit nok
        /// </summary>
        /// <param name="amount">Værdi</param>
        /// <param name="accountno">Kontunummer</param>
        /// <returns>Returnerer en bolsk værdi til kontrol af om transaktionen bliver gennemført</returns>
        /// Kaser en exception hvis der ikke er nok dækning på kontoen.
        public bool Withdraw(decimal amount, int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);

            if (searchAcc.AccountType == "Lønkonto")
            {
                if (searchAcc.AccountBalance >= (searchAcc.AccountLimit + amount))
                {
                    searchAcc.AccountBalance -= amount;
                    LoghandlerEvent("SUCCESS! " + "-" + amount + " - " + searchAcc.AccountHolder + " - " + searchAcc.AccountType + " - " + searchAcc.AccountNo + " = " + searchAcc.AccountBalance);
                    return true;
                }
                else
                {
                    LoghandlerEvent("ERROR! " + "-" + amount + " - " + searchAcc.AccountHolder + " - " + searchAcc.AccountType + " - " + searchAcc.AccountNo + " = " + searchAcc.AccountBalance);
                    throw new OverdraftException();
                }
            }
            else if (searchAcc.AccountType == "Opsparingskonto")
            {
                if (searchAcc.AccountBalance >= (searchAcc.AccountLimit + amount))
                {
                    searchAcc.AccountBalance -= amount;
                    LoghandlerEvent("SUCCESS! " + "-" + amount + " - " + searchAcc.AccountHolder + " - " + searchAcc.AccountType + " - " + searchAcc.AccountNo + " = " + searchAcc.AccountBalance);
                    return true;
                }
                else
                {
                    LoghandlerEvent("ERROR! " + "-" + amount + " - " + searchAcc.AccountHolder + " - " + searchAcc.AccountType + " - " + searchAcc.AccountNo + " = " + searchAcc.AccountBalance);
                    throw new OverdraftException();
                }
            }
            else if (searchAcc.AccountType == "Kreditkortkonto")
            {
                if (searchAcc.AccountBalance >= (searchAcc.AccountLimit + amount))
                {
                    searchAcc.AccountBalance -= amount;
                    LoghandlerEvent("SUCCESS! " + "-" + amount + " - " + searchAcc.AccountHolder + " - " + searchAcc.AccountType + " - " + searchAcc.AccountNo + " = " + searchAcc.AccountBalance);
                    return true;
                }
                else
                {
                    LoghandlerEvent("ERROR! " + "-" + amount + " - " + searchAcc.AccountHolder + " - " + searchAcc.AccountType + " - " + searchAcc.AccountNo + " = " + searchAcc.AccountBalance);
                    throw new OverdraftException();
                }
            }
            else
            {
                Console.WriteLine("Der må være sket en fjel");
                return false;
            }
        }

        /// <summary>
        /// Viser saldo på den konto der er valgt
        /// </summary>
        /// <param name="accountno">Kont</param>
        /// <returns>Decimal (kontobalance)</returns>
        public decimal Balance(int accountno)
        {
            Account searchAcc = Accounts.Find(x => x.AccountNo == accountno);
            LoghandlerEvent("Balance check: " + searchAcc.AccountHolder + " " + searchAcc.AccountNo + " " + searchAcc.AccountType + " " + searchAcc.AccountBalance);
            return searchAcc.AccountBalance;
        }

        /// <summary>
        /// Tilskriver rente på konti
        /// </summary>
        public void Interest()
        {
            LoghandlerEvent("Adding interest...");
            foreach (Account item in Accounts)
            {
                LoghandlerEvent("Before: " + item.AccountNo + " - " + item.AccountType + " - " + item.AccountBalance);
                item.ChargeInterest();
                LoghandlerEvent("After: " + item.AccountNo + " - " + item.AccountType + " - " + item.AccountBalance);
            }
            LoghandlerEvent("Job completed ...");
        }

        /// <summary>
        /// Går ud af et loop vha. en ConsoleKeyInfo
        /// </summary>
        /// <param name="x">Console.Readkey(true)</param>
        /// <returns>True hvis brugeren taster X</returns>
        /// Christians lille metode :)
        public bool ExitError(ConsoleKeyInfo x)
        {
            if (x.Key == ConsoleKey.X)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Eventlog til at smide ændringer i programmet ind i logfilen
        /// </summary>
        /// <param name="msg">Besked fra eventet</param>
        static void Bank_LoghandlerEvent(string msg)
        {
            FileLogger.WriteToLog(msg);
        }
   }
}
