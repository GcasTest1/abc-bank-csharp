using System;

namespace AbcBank
{
    public class Transaction
    {
        public readonly double Amount;

        private DateTime  _transactionDate;

        public Transaction(double amount) 
        {
            Amount = amount;
            _transactionDate = DateProvider.GetInstance().Now();
        }
    }
}
