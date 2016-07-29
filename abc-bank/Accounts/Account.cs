using abc_bank.Contract;
using System;
using System.Collections.Generic;

namespace abc_bank.Accounts
{
    public abstract class Account : IInterestCalculator
    {

        public enum AccountType { Checking, Savings, Maxi_Savings };

        private readonly AccountType accountType;
        public List<Transaction> transactions;

        public Account(AccountType accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        //IDataProvider could be resolved by using an IOC container in the future
        public Account(AccountType accountType, IDateProvider dateProviders)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.TransDateProvider = dateProviders;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, this.TransDateProvider));
            }
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount, this.TransDateProvider));
            }
        }

        public void TransferToAccount(Account toAccount, double amount)
        {
            if (amount > sumTransactions())
            {
                throw new ArgumentException("Insufficient funds");
            }
            else
            {
                this.Withdraw(amount);
                toAccount.Deposit(amount);
            }
        }

        public abstract double CalculateInterest(double amount);

        public double InterestEarned()
        {
            return CalculateInterest(sumTransactions());
        }

        public double sumTransactions()
        {
            return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public AccountType GetAccountType()
        {
            return accountType;
        }

        public double Balance
        {
            get { return this.sumTransactions(); }
        }

        public IDateProvider TransDateProvider
        {
            get; set;
        }
    }
}
