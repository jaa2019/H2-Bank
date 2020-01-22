using System;
namespace H2_Bank.Models
{
    public class OverdraftException : Exception
    {
        public OverdraftException() : base(message:"Du har ikke nok kredit på din konto. Skovl")
        {  }
    }
}