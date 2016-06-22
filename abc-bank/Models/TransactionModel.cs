using System;

namespace AbcBank.Models
{
    public class TransactionModel
    {
        public readonly double Amount;
        public DateTime  Date { get; private set; }

        public TransactionModel(double amount, DateTime date) 
        {
            Amount = amount;
            Date = date;
        }
    }
}
