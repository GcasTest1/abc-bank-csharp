using System;
using AbcBank.Data;
using AbcBank.Enums;

namespace AbcBank.Logic.BusinessLogic
{
    public class AccountService
    {
        public void Deposit(AccountModel account, double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }

            account.AddTransaction(new Transaction(amount));
        }

        public void Withdraw(AccountModel account, double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }

            account.AddTransaction(new Transaction(-amount));
        }

        public double InterestEarned(AccountModel account) 
        {
            var amount = SumTransactions(account);
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

        public double SumTransactions(AccountModel account) {
           return CheckIfTransactionsExist(account);
        }

        private double CheckIfTransactionsExist(AccountModel account) 
        {
            var amount = 0.0;
            foreach (var t in account.Transactions)
                amount += t.Amount;
            return amount;
        }

        public AccountType GetAccountType(AccountModel account) 
        {
            return account.AccountType;
        }

    }
}
