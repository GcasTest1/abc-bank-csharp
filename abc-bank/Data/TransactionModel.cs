using System;

namespace AbcBank.Data
{
    public class TransactionModel
    {
        public readonly double Amount;

        private DateTime  _transactionDate;

        public TransactionModel(double amount) 
        {
            Amount = amount;
            _transactionDate = DateProvider.GetInstance().Now();
        }
    }
}
