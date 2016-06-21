using System;
using System.Collections.Generic;
using System.Linq;
using AbcBank.Data;
using AbcBank.Enums;

namespace AbcBank.Logic.BusinessLogic.Implementation
{
    public class AccountService : IAccountService
    {
        public AccountService Deposit(AccountModel account, double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }

            account.AddTransaction(new TransactionModel(amount));
            return this;
        }

        public AccountService Withdraw(AccountModel account, double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }

            account.AddTransaction(new TransactionModel(-amount));
            return this;
        }

        public double InterestEarned(AccountModel account) 
        {
            var amount = SumTransactions(account.Transactions);
            switch(account.AccountType){
                case AccountType.Savings:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case AccountType.MaxiSavings:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05;
                    return 70 + (amount-2000) * 0.1;
                default:
                    return amount * 0.001;
            }
        }

        public double SumTransactions(IEnumerable<TransactionModel> transactions) {
           return transactions.Sum(t => t.Amount);
        }
    }
}
