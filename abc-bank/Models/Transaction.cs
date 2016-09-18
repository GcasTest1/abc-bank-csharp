using System;
using abc_bank.Services;

namespace abc_bank.Models
{
    public class Transaction
    {
        public double Amount { get; }

        public DateTime TransactionDate { get; }

        public Transaction(double amount) 
        {
            this.Amount = amount;
            this.TransactionDate = DateProvider.getInstance().Now();
        }
    }
}
