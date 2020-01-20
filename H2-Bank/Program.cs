using System;
using System.Collections.Generic;

namespace H2_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            string hardcode;
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
                        myBank.CreateAccount(Console.ReadLine());
                        Console.WriteLine();
                        Console.Write("Du har oprettet en konto i {0}'s navn. Tast enter for at fortsætte", myBank.Konto.Name);
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.I:
                        Console.WriteLine();
                        Console.WriteLine("Indsæt beløb på {0}'s konto: ", myBank.Konto.Name);
                        hardcode = Console.ReadLine();
                        try
                        {
                            myBank.Deposit(Convert.ToDecimal(hardcode));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Der er desværre sket en fejl");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Prøv venligst igen.");
                        }
                        Console.WriteLine("Du har indsat {0}, saldoen er nu {1} - Tast enter for at fortsætte", hardcode, myBank.Balance());
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.H:
                        Console.WriteLine();
                        Console.Write("Hæv beløb på {0}'s konto: ", myBank.Konto.Name);
                        hardcode = Console.ReadLine();
                        try
                        {
                            myBank.Withdraw(Convert.ToDecimal(hardcode));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Der er desværre sket en fejl");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Prøv venligst igen.");
                        }
                        Console.Write("Du har hævet -{0}, saldoen er nu {1} - Tast enter for at fortsætte", hardcode, myBank.Balance());
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.V:
                        Console.WriteLine();
                        Console.Write("Saldoen på {0}'s konto er: ", myBank.Konto.Name);
                        Console.Write(myBank.Balance());
                        Console.WriteLine();
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
//Test comment/push