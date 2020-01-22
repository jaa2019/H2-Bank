using System;
namespace H2_Bank.Models
{
    public class OverdraftException : Exception
    {
        public OverdraftException() : base(message:"Der er desværre sket en fejl:\nDu har ikke nok kredit på din konto.\nPrøv igen med et mindre beløb.\nTast X for at afbryde.")
        {  }
    }
}