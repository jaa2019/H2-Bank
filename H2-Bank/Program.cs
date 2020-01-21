using System;
using System.Collections.Generic;
using System.Threading;

namespace H2_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            string accountNumInput;
            string decimalInput;
            ConsoleKeyInfo menuKey;


            Bank myBank = new Bank("Jan's Bank");
            do
            {
                Menu();
                menuKey = Console.ReadKey(true);
                switch (menuKey.Key)
                {
                    case ConsoleKey.M:
                        Menu();
                        break;
                    case ConsoleKey.O:
                        Console.WriteLine();
                        Console.Write("Indtast navn på kontohaver: ");
                        string accNameInput = Console.ReadLine();
                        Console.WriteLine("[1] - Lønkonto");
                        Console.WriteLine("[2] - Kreditkortkonto");
                        Console.WriteLine("[3] - Opsparingskonto");
                        Console.Write("Indtast kontotype: ");
                        int accTypeInput = Convert.ToInt16(Console.ReadLine());
                        string accTypeOut = myBank.CreateAccount(accNameInput, accTypeInput);
                        Console.WriteLine();
                        Console.Write("Du har oprettet en {0} i {1}'s navn, den fik kontonummer {2}. Tast enter for at fortsætte", accTypeOut , accNameInput, myBank.AccountNo);
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.I:
                        Console.WriteLine();
                        Console.WriteLine("Vælg hvilken konto du vil indsætte penge på: ");
                        foreach (Account item in myBank.Accounts)
                        {
                            Console.WriteLine("{0} - {1} - {2}",item.AccountNo,item.AccountType,item.AccountHolder);
                        }
                        Console.Write("Indtast kontonummer: ");
                        accountNumInput = Console.ReadLine();
                        Console.Write("Indtast beløb: ");
                        decimalInput = Console.ReadLine();
                        try
                        {
                            myBank.Deposit(Convert.ToDecimal(decimalInput), myBank.Accounts.Find(s => s.AccountNo == Convert.ToInt16(accountNumInput)).AccountNo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Der er desværre sket en fejl");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Prøv venligst igen.");
                        }
                        Console.WriteLine("Du har indsat {0}, saldoen er nu {1} - Tast enter for at fortsætte", decimalInput, myBank.Balance(Convert.ToInt16(accountNumInput)));
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.H:
                        Console.WriteLine();
                        Console.WriteLine("Vælg hvilken konto du vil hæve fra:");
                        foreach (Account item in myBank.Accounts)
                        {
                            Console.WriteLine("{0} - {1} - {2}", item.AccountNo, item.AccountType, item.AccountHolder);
                        }
                        Console.Write("Indtast kontonummer: ");
                        accountNumInput = Console.ReadLine();
                        Console.Write("Indtast beløb: ");
                        decimalInput = Console.ReadLine();
                        try
                        {
                            myBank.Withdraw(Convert.ToDecimal(decimalInput), myBank.Accounts.Find(s => s.AccountNo == Convert.ToInt16(accountNumInput)).AccountNo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Der er desværre sket en fejl");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Prøv venligst igen.");
                        }
                        Console.WriteLine("Du har hævet -{0}, saldoen er nu {1} - Tast enter for at fortsætte", decimalInput, myBank.Balance(Convert.ToInt16(accountNumInput)));
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.V:
                        Console.WriteLine();
                        Console.WriteLine("Vælg hvilken konto du vil se saldo for:");
                        Console.WriteLine("[A]lle konti");
                        foreach (Account item in myBank.Accounts)
                        {
                            Console.WriteLine(item.AccountNo + " " + item.AccountHolder);
                        }
                        Console.Write("Indtast kontonummer: ");
                        accountNumInput = Console.ReadLine();
                        if (accountNumInput == "A" | accountNumInput == "a")
                        {
                            foreach (Account item in myBank.Accounts)
                            {
                                Console.WriteLine("{0} - {1} - {2} - {3}",item.AccountNo, item.AccountHolder, item.AccountType, item.AccountBalance);
                            }
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Saldoen på {0} ({1}) er: {2}", myBank.Accounts.Find(s => s.AccountNo == Convert.ToInt16(accountNumInput)).AccountHolder, myBank.Accounts.Find(s => s.AccountNo == Convert.ToInt16(accountNumInput)).AccountType, myBank.Balance(Convert.ToInt16(accountNumInput)));
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
                    default:
                        break;
                }
            } while (menuKey.Key != ConsoleKey.X);

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
            Console.WriteLine("║       [M]enu              ║");
            Console.WriteLine("║       [O]pret konto       ║");
            Console.WriteLine("║       [R]ente på konto    ║");
            Console.WriteLine("║       [I]ndsæt beløb      ║");
            Console.WriteLine("║       [H]æv beløb         ║");
            Console.WriteLine("║       [V]is saldo         ║");
            Console.WriteLine("║       [X] Afslut          ║");
            Console.WriteLine("╚═══════════════════════════╝");

        }
    }
}