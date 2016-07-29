using System;
using abc_bank.Contract;
using abc_bank.Infrastructure;

namespace abc_bank
{
    public class Transaction
    {
        public readonly double amount;

        private DateTime transactionDate;
        private IDateProvider transDateProvider;

        public Transaction(double amount)
        {
            this.amount = amount;
            this.transactionDate = TransDateProvider.GetDateTimeNow();
        }

        //IDataProvider could be resolved by using an IOC container in the future
        public Transaction(double amount, IDateProvider dateProvider)
        {
            this.amount = amount;
            this.transDateProvider = dateProvider;
            this.transactionDate = TransDateProvider.GetDateTimeNow();
        }

        public DateTime Date
        {
            get { return this.transactionDate; }
        }

        public IDateProvider TransDateProvider
        {
            get
            {
                if (transDateProvider == null)
                    this.transDateProvider = new AbcDateProvider();

                return this.transDateProvider;
            }

            set
            {
                this.transDateProvider = value;
            }
        }
    }
}
