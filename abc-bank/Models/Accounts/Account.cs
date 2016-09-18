using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank.Models.Accounts
{
    public abstract class Account
    {
        private readonly int accountType;
        private DateTime lastWithdrawDate = DateTime.MinValue;

        public List<Transaction> Transactions { get; }

        protected Account(int accountType) 
        {
            this.accountType = accountType;
            this.Transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                var transaction = new Transaction(-amount);
                Transactions.Add(transaction);
                lastWithdrawDate = transaction.TransactionDate;
            }
        }

        private double holdamount;

        public void BeginTransfer(double amount)
        {
            holdamount = amount;
        }

        public void CompleteTransfer()
        {
            if (holdamount > 0)
            {
                Transactions.Add(new Transaction(holdamount));
            }
            else
            {
                var transaction = new Transaction(holdamount);
                Transactions.Add(transaction);
                lastWithdrawDate = transaction.TransactionDate;
            }
            holdamount = 0;
        }

        public abstract double AnnumInterestEarned();

        //TODO
        //public abstract double DailyInterestEarned();

        public double GetAccountBalance()
        {
            return Transactions.Sum(t => t.Amount);
        }

        public int GetAccountType()
        {
            return accountType;
        }

        public abstract string GetAccountName();

        public DateTime GetLastWithdrawDate()
        {
            return lastWithdrawDate;
        }

    }
}
