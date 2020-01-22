using System;
namespace H2_Bank.Models

{
    public abstract class Account
    {
        public string AccountHolder { get; set; }       // Navn på kontoholder
        public string AccountType { get; set; }         // Kontotype
        public decimal AccountBalance { get; set; }     // Kontobalance, bliver sat til 0
        public int AccountNo { get; set; }              // Kontonummer
        public decimal AccountLimit { get; set; }       // Kredit

        public abstract void ChargeInterest();
    }
}