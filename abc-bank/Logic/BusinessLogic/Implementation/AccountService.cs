using System;
using System.Collections.Generic;
using System.Linq;
using AbcBank.Enums;
using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly ITransactionModelFactory _transactionModelFactory;

        public AccountService(ITransactionModelFactory transactionModelFactory)
        {
            _transactionModelFactory = transactionModelFactory;
        }

        public AccountService Deposit(AccountModel account, double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }

            account.AddTransaction(_transactionModelFactory.CreateTransactionModel(amount));
            return this;
        }

        public AccountService Withdraw(AccountModel account, double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }

            account.AddTransaction(_transactionModelFactory.CreateTransactionModel(-amount));
            return this;
        }

        public double InterestEarned(AccountModel account) 
        {
            var amount = SumTransactions(account.Transactions);
            switch(account.AccountType){
                case AccountType.Savings:
                    if (amount <= 1000)
                        return amount * 0.001;
                    return 1 + (amount-1000) * 0.002;
                case AccountType.MaxiSavings:
                    if (account.Transactions.Any(i => i.Amount < 0 && i.Date >= DateTime.Now.AddDays(-10)))
                        return amount * 0.001;
                    return amount * 0.05;
                case AccountType.Checking:
                    return amount * 0.001;
                default:
                    throw new ArgumentException("Unrecognized account type.");
            }
        }

        public double SumTransactions(IEnumerable<TransactionModel> transactions) {
           return transactions.Sum(t => t.Amount);
        }

        public void Transfer(AccountModel sourceAccount, AccountModel destAccount, double amount)
        {
            Withdraw(sourceAccount, amount);
            Deposit(destAccount, amount);
        }
    }
}
