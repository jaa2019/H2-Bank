﻿using System;
namespace H2_Bank
{
    public class MasterCardAccount : Account
    {
        /// <summary>
        /// Opretter en kredit-konto
        /// </summary>
        /// <param name="name"></param>
        public MasterCardAccount(string name, int accno)
        {
            AccountHolder = name;
            AccountNo = accno;
            AccountBalance = 0;
            AccountType = "Kreditkortkonto";
        }

        public override void ChargeInterest()
        {
            if (AccountBalance > 0)
            {
                AccountBalance = AccountBalance * 0.001m;
            }
            else
            {
                AccountBalance = AccountBalance / 0.98m;
            }
        }
    }
}