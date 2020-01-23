using System;
using System.Threading;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using H2_Bank.Models;
using H2_Bank.BLL;
using H2_Bank.Repository;
using H2_Bank.Utilities;

namespace H2_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            string accountNumInput;                                             //Det input brugeren giver når denne vælger en konto
            string decimalStrInput;                                             //Det input brugeren giver når denne vil indsætte eller hæve
            decimal decimalDecInput;                                            //Konverteret fra string til decimal (try/catch)
            bool menuBool = false;                                              //En bolsk værdi der bliver sat ved try/catch hvis brugeren vælger at prøve igen
            ConsoleKeyInfo menuKey;

            // Istancierer banken med et navn
            Bank myBank = new Bank("Jan's Bank");

            do
            {
                Menu(); //Kalder menuen
                menuKey = Console.ReadKey(true);
                switch (menuKey.Key)
                {
                    #region - Opret konto
                    case ConsoleKey.O:
                        Console.WriteLine();
                        Console.Write("Indtast navn på kontohaver: ");
                        string accNameInput = Console.ReadLine();
                        //Løber alle de tilgængelige konti typer igennem, og viser dem som en "menu"
                        foreach (int item in Enum.GetValues(typeof(AccountType)))
                        {
                            Console.WriteLine("["+item+"]" + " " + Enum.GetName(typeof(AccountType), item));
                        }
                        Console.Write("Indtast kontotype: ");
                        string accTypeInput = Console.ReadLine();
                        AccountType accType = (AccountType)Enum.Parse(typeof(AccountType), accTypeInput);
                        //Opretter given konto, og får en string retur med friendly name
                        string accTypeOut = myBank.CreateAccount(accNameInput, accType);
                        Console.WriteLine();
                        Console.Write("Du har oprettet en {0} i {1}'s navn. Tast enter for at fortsætte", accTypeOut , accNameInput);
                        Console.ReadKey(true);
                        break;
                    #endregion
                    #region - Indsæt penge
                    case ConsoleKey.I:
                        Console.WriteLine();
                        Console.WriteLine("Vælg hvilken konto du vil indsætte penge på: ");
                        //Løber alle konti igennem og viser det som en menu brugeren kan vælge
                        foreach (AccountListItem item in myBank.GetAccountList())
                        {
                            Console.WriteLine("[{0}] - {1} - {2}",item.AccountNo,item.AccountType,item.AccountHolder);
                        }
                        Console.Write("Indtast kontonummer: ");
                        accountNumInput = Console.ReadLine();
                        Console.Write("Indtast beløb: ");
                        decimalStrInput = Console.ReadLine();
                        //Prøver at konvertere og indsætte brugerens input
                        try
                        {
                            myBank.Deposit(Convert.ToDecimal(decimalStrInput), myBank.GetAccountList().Find(s => s.AccountNo == Convert.ToInt16(accountNumInput)).AccountNo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Der er desværre sket en fejl");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Prøv venligst igen.");
                        }
                        Console.WriteLine("Du har indsat {0}, saldoen er nu {1} - Tast enter for at fortsætte", decimalStrInput, myBank.Balance(Convert.ToInt16(accountNumInput)));
                        Console.ReadKey(true);
                        break;
                    #endregion
                    #region - Hæver penge
                    case ConsoleKey.H:
                        //Denne do/while kigger på menuBool som bliver sat TRUE hvis alt blev gennemført.
                        //Hvis brugeren har lavet en fejl bliver den sat FALSE så brugeren kan prøve igen
                        do
                        {
                            Console.WriteLine();
                            Console.WriteLine("Vælg hvilken konto du vil hæve fra:");
                            //Løber alle konti igennem og viser dem som en menu hvor man kan vælge hvilken konto man vil hæve fra
                            foreach (AccountListItem item in myBank.GetAccountList())
                            {
                                Console.WriteLine("[{0}] - {1} - {2}", item.AccountNo, item.AccountType, item.AccountHolder);
                            }
                            Console.Write("Indtast kontonummer: ");
                            accountNumInput = Console.ReadLine();
                            Console.Write("Indtast beløb: ");
                            decimalStrInput = Console.ReadLine();
                            //Try/catch delen hvor input bliver kontrolleret (tal værdi)
                            try
                            {
                                decimalDecInput = Convert.ToDecimal(decimalStrInput);
                                //Tester for at se om der er kredit nok på kontoen (og om kontoen eksiterer)
                                try
                                {
                                    //TRUE bliver returneret af withdraw() om transaktionen blev gennemført
                                    if (myBank.Withdraw(decimalDecInput, myBank.GetAccountList().Find(s => s.AccountNo == Convert.ToInt16(accountNumInput)).AccountNo) == true)
                                    {
                                        Console.WriteLine("Du har hævet -{0}, saldoen er nu {1} - Tast enter for at fortsætte", decimalStrInput, myBank.Balance(Convert.ToInt16(accountNumInput)));
                                        Console.ReadKey(true);
                                        menuBool = true;
                                    }
                                }
                                catch (NullReferenceException)
                                {
                                    Console.WriteLine("Der er desværre sket en fejl");
                                    Console.WriteLine("Den konto du prøver at hæve fra findes ikke.");
                                    Console.WriteLine("Kontroller kontonummer, og prøv igen (tast X for at afbryde)");
                                    if (myBank.ExitError(Console.ReadKey(true)))
                                    {
                                        break;
                                    }
                                    menuBool = false;
                                }
                                catch(OverdraftException e)
                                {
                                    Console.WriteLine(e.Message);
                                    if (myBank.ExitError(Console.ReadKey(true)))
                                    {
                                        break;
                                    }
                                    menuBool = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Der er desværre sket en fejl");
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("Prøv venligst igen (tast X for at afbryde)");
                                if (myBank.ExitError(Console.ReadKey(true)))
                                {
                                    break;
                                }
                                menuBool = false;
                                decimalDecInput = 0;
                            }
                        } while (!menuBool);
                        menuBool = false;
                        break;
                    #endregion
                    #region - Vis saldo
                    case ConsoleKey.V:
                        Console.WriteLine();
                        Console.WriteLine("Vælg hvilken konto du vil se saldo for:");
                        Console.WriteLine("[A]lle konti");
                        //Løber alle konti igennem
                        foreach (AccountListItem item in myBank.GetAccountList())
                        {
                            Console.WriteLine("[{0}] - {1}", item.AccountNo, item.AccountHolder);
                        }
                        Console.Write("Indtast kontonummer: ");
                        accountNumInput = Console.ReadLine();
                        //Hvis brugeren taster A, bliver saldi vist for alle konti.
                        if (accountNumInput == "A" | accountNumInput == "a")
                        {
                            foreach (AccountListItem item in myBank.GetAccountList())
                            {
                                Console.WriteLine("{0} - {1} - {2} - {3}",item.AccountNo, item.AccountHolder, item.AccountType, myBank.Balance(item.AccountNo));
                            }
                        }
                        else
                        {
                            try
                            {
                                //Finder den konto brugeren søgte på
                                AccountListItem searchAcc = myBank.GetAccountList().Find(s => s.AccountNo == Convert.ToInt16(accountNumInput));
                                Console.WriteLine("Saldoen på {0} ({1}) er: {2}", searchAcc.AccountHolder, searchAcc.AccountType, myBank.Balance(searchAcc.AccountNo));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Der er desværre sket en fejl");
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("Prøv venligst igen.");
                            }
                        }
                        Console.WriteLine("Tast enter for at fortsætte");
                        Console.ReadKey(true);
                        break;
                    #endregion
                    #region - Tilskrive rente
                    case ConsoleKey.R:
                        Console.WriteLine();
                        myBank.Interest();
                        Console.WriteLine("Pålægger rente på alle konti");
                        for (int i = 0; i < 28; i++)
                        {
                            Console.Write(".");
                            Thread.Sleep(100);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Rentetilskrivning fuldført");
                        Console.WriteLine("Tast enter for at fortsætte");
                        Console.ReadKey();
                        break;
                    #endregion
                    #region - Viser logfilen
                    case ConsoleKey.L:
                        Console.WriteLine("Loggen indeholder følgende:");
                        Console.WriteLine(FileLogger.ReadFromLog());
                        Console.Write("Tast en vilkårlig tast for at fortsætte");
                        Console.ReadKey(true);
                        break;
                    #endregion
                    default:
                        break;
                }
            } while (menuKey.Key != ConsoleKey.X);
            myBank.ExitProgram();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("             _.-------._		");
            Console.WriteLine("          _-'_.------._ `-_	");
            Console.WriteLine("        _- _-          `-_/	");
            Console.WriteLine("       -  -					");
            Console.WriteLine("   ___/  /______________		");
            Console.WriteLine("  /___  .______________/		");
            Console.WriteLine("   ___| |_____________		");
            Console.WriteLine("  /___  .____________/		");
            Console.WriteLine(@"      \  \					");
            Console.WriteLine("       -_ -_             /|  ");
            Console.WriteLine("         -_ -._        _- |	");
            Console.WriteLine("           -._ `------'_./   ");
            Console.WriteLine("              `-------'		");
            Console.WriteLine("╔═══════════════════════════╗");
            Console.WriteLine("║ Velkommen til Jan's Bank! ║");
            Console.WriteLine("╠═══════════════════════════╣");
            Console.WriteLine("║       Vælg venligst:      ║");
            Console.WriteLine("║       [O]pret konto       ║");
            Console.WriteLine("║       [R]ente på konto    ║");
            Console.WriteLine("║       [I]ndsæt beløb      ║");
            Console.WriteLine("║       [H]æv beløb         ║");
            Console.WriteLine("║       [V]is saldo         ║");
            Console.WriteLine("║       [L]og-check         ║");
            Console.WriteLine("║       [X] Afslut          ║");
            Console.WriteLine("╚═══════════════════════════╝");
        }
    }
}

//TODO - Eventuelt tilføje en do/while på alle menupunkter ved fejl, så brugeren kan prøve igen uden at blive smidt retur til hovedmenuen