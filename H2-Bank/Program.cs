using System;
using System.Collections.Generic;

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
                        myBank.CreateAccount(accNameInput);
                        Console.WriteLine();
                        Console.Write("Du har oprettet en konto der hedder {0}, den fik kontonummer {1}. Tast enter for at fortsætte", accNameInput, myBank.Accounts.Find(s => s.AccountHolder == accNameInput).AccountNo);
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.I:
                        Console.WriteLine();
                        Console.WriteLine("Vælg hvilken konto du vil indsætte penge på:");
                        foreach (Account item in myBank.Accounts)
                        {
                            Console.WriteLine(item.AccountNo + " " + item.AccountHolder);
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
                        Console.Write("Vælg hvilken konto du vil hæve fra:");
                        foreach (Account item in myBank.Accounts)
                        {
                            Console.WriteLine(item.AccountNo + " " + item.AccountHolder);
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
                        foreach (Account item in myBank.Accounts)
                        {
                            Console.WriteLine(item.AccountNo + " " + item.AccountHolder);
                        }
                        Console.Write("Indtast kontonummer: ");
                        accountNumInput = Console.ReadLine();
                        try
                        {
                            Console.WriteLine("Saldoen på {0} er: {1}", myBank.Accounts.Find(s => s.AccountNo == Convert.ToInt16(accountNumInput)).AccountHolder, myBank.Balance(Convert.ToInt16(accountNumInput)));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Der er desværre sket en fejl");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Prøv venligst igen.");
                        }
                        Console.WriteLine("Tast enter for at fortsætte");
                        Console.ReadKey(true);
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
            Console.WriteLine("║       [I]ndsæt beløb      ║");
            Console.WriteLine("║       [H]æv beløb         ║");
            Console.WriteLine("║       [V]is saldo         ║");
            Console.WriteLine("║       [X] Afslut          ║");
            Console.WriteLine("╚═══════════════════════════╝");

        }
    }
}