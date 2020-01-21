using System;
using System.ComponentModel;
using System.Reflection;

namespace H2_Bank.Models
{
    public enum AccountType
    {
        [Description("Lønkonto")]
        checkingAccount = 1,
        [Description("Opsparingskonto")]
        savingsAccount = 2,
        [Description("Kreditkortkonto")]
        masterCardAccount = 3
    }
}