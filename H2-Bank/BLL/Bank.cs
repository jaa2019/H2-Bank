using System;
using System.Collections.Generic;
using H2_Bank.Models;
using H2_Bank.Repository;
using H2_Bank.Utilities;
using H2_Bank;
using H2_Bank.DAL;

namespace H2_Bank.BLL
{
    delegate void LogHandlerDelegate(string msg);

    public class Bank : iBank
    {
        public string BankName { get; }
        int accNum;

        //Istancierer et object af FileRepository
        FileRepository BankRepoFile = new FileRepository();
        //Istancierer LoghandlerEvent
        LogHandlerDelegate LoghandlerEvent = Bank_LoghandlerEvent;


        /// <summary>
        /// Metode til at hente alle konti
        /// </summary>
        /// <returns>Liste af typen "AccountListItem"</returns>
        public List<AccountListItem> GetAccountList()
        {
            return BankRepoFile.GetAccountList();
        }

        /// <summary>
        /// Henter en liste over kontityper fra Enum(AccountType)
        /// </summary>
        /// <returns>Liste af strings(AccountType)</returns>
        public List<string> GetAccType()
        {
            List<string> result = new List<string>();
            foreach (int item in Enum.GetValues(typeof(AccountType)))
            {
                result.Add("[" + item + "]" + " " + Enum.GetName(typeof(AccountType), item));
            }
            return result;
        }

        /// <summary>
        /// Instancierer banken
        /// </summary>
        /// <param name="name">Navnet på din bank</param>
        /// Kalder metoden LoadBank() som indlæser banken fra datafil
        public Bank(string name)
        {
            BankName = name;
            BankRepoFile.LoadBank();
            LoghandlerEvent("Indlæser bank ...");
        }

        /// <summary>
        /// Opretter en konto
        /// </summary>
        /// <param name="navn">Navn på kontoholder</param>
        /// <param name="type">Type af konto</param>
        /// <returns>Friendly-name på kontoen</returns>
        public string CreateAccount(string navn, AccountType type)
        {
            if (type == AccountType.checkingAccount)
            {
                accNum = BankRepoFile.AddAccount(new CheckingAccount(navn));
                LoghandlerEvent("Der er oprettet en " + type.ToString() + " til " + navn);
                return $"Lønkonto med kontonummer {accNum}";
            }
            else if (type == AccountType.masterCardAccount)
            {
                accNum = BankRepoFile.AddAccount(new MasterCardAccount(navn));
                LoghandlerEvent("Der er oprettet en " + type.ToString() + " til " + navn);
                return $"Kreditkortkonto med kontonummer {accNum}";
            }
            else if (type == AccountType.savingsAccount)
            {
                accNum = BankRepoFile.AddAccount(new SavingsAccount(navn));
                LoghandlerEvent("Der er oprettet en " + type.ToString() + " til " + navn);
                return $"Opsparingskonto med kontonummer {accNum}";
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
            Account searchAcc = BankRepoFile.GetAccount(accountno);
            searchAcc.AccountBalance += amount;
            LoghandlerEvent("SUCCESS! " + "+" + amount + " - " + searchAcc.AccountHolder + " - " + searchAcc.AccountType + " - " + searchAcc.AccountNo + " = " + searchAcc.AccountBalance);
            BankRepoFile.UpdateAccount(searchAcc);
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
            Account searchAcc = BankRepoFile.GetAccount(accountno);

            if (searchAcc.AccountType == "Lønkonto")
            {
                if (searchAcc.AccountBalance >= (searchAcc.AccountLimit + amount))
                {
                    searchAcc.AccountBalance -= amount;
                    LoghandlerEvent("SUCCESS! " + "-" + amount + " - " + searchAcc.AccountHolder + " - " + searchAcc.AccountType + " - " + searchAcc.AccountNo + " = " + searchAcc.AccountBalance);
                    BankRepoFile.UpdateAccount(searchAcc);
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
                    BankRepoFile.UpdateAccount(searchAcc);
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
                    BankRepoFile.UpdateAccount(searchAcc);
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
        /// <param name="accountno">Kontonummer</param>
        /// <returns>Decimal (kontobalance)</returns>
        public decimal Balance(int accountno)
        {
            Account searchAcc = BankRepoFile.GetAccount(accountno);
            LoghandlerEvent("Balance check: " + searchAcc.AccountHolder + " " + searchAcc.AccountNo + " " + searchAcc.AccountType + " " + searchAcc.AccountBalance);
            return searchAcc.AccountBalance;
        }

        /// <summary>
        /// Tilskriver rente på konti
        /// </summary>
        public void Interest()
        {
            LoghandlerEvent("Adding interest...");
            foreach (Account item in BankRepoFile.accountList)
            {
                LoghandlerEvent("Before: " + item.AccountNo + " - " + item.AccountType + " - " + item.AccountBalance);
                item.ChargeInterest();
                LoghandlerEvent("After: " + item.AccountNo + " - " + item.AccountType + " - " + item.AccountBalance);
                BankRepoFile.UpdateAccount(item);
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

        /// <summary>
        /// Lukker programmet med exit code 0 og gemmer alle konti
        /// </summary>
        public void ExitProgram()
        {
            LoghandlerEvent("Gemer bank ...");
            BankRepoFile.SaveBank();
        }
    }
}
