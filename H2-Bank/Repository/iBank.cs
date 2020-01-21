using System;
using System.Collections.Generic;
using H2_Bank.Models;

namespace H2_Bank.Repository

{
    public interface iBank
    {
        public string BankName { get; }
        public List<Account> Accounts { get; set; }
        public int AccountNo { get; set; }
    }
}
