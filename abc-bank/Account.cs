using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Account
    {
        private readonly AccountType _accountType;
        public List<Transaction> Transactions;

        public Account(AccountType accountType) 
        {
            _accountType = accountType;
            Transactions = new List<Transaction>();
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
                Transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned() 
        {
            var amount = SumTransactions();
            switch(_accountType){
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

        public double SumTransactions() {
           return CheckIfTransactionsExist();
        }

        private double CheckIfTransactionsExist() 
        {
            var amount = 0.0;
            foreach (var t in Transactions)
                amount += t.Amount;
            return amount;
        }

        public AccountType GetAccountType() 
        {
            return _accountType;
        }

    }
}
