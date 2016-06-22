using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic.Implementation.IntererestCalculation;
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
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            account.AddTransaction(_transactionModelFactory.CreateTransactionModel(amount));
            return this;
        }
        public AccountService Withdraw(AccountModel account, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            account.AddTransaction(_transactionModelFactory.CreateTransactionModel(-amount));
            return this;
        }
        public AccountService Transfer(AccountModel sourceAccount, AccountModel destAccount, double amount)
        {
            Withdraw(sourceAccount, amount);
            Deposit(destAccount, amount);
            return this;
        }
        public double YearlyInterestEarned(AccountModel account)
        {
            return
                GetYealyInterestCalculationsRules()
                    .Calculate(new CalculationResult<AccountModel, double>
                    {
                        Input = account,
                        Result = SumTransactions(account.Transactions)
                    })
                    .Result;
        }
        public double InterestEarned(AccountModel account)
        {
            var startDay = account.Transactions.Min(i => i.Date).Date;
            var dailySums =
                account.Transactions
                    .GroupBy(i => i.Date.Date)
                    .Select(i => new { Day = i.Key, Amount = SumTransactions(i) })
                    .ToDictionary(i => i.Day);

            var balance = 0.0;
            var totalInterest = 0.0;
            for (var currentDay = startDay.Date; currentDay < DateTime.Now; currentDay = currentDay.AddDays(1))
            {
                if (dailySums.ContainsKey(currentDay))
                    balance += dailySums[currentDay].Amount;

                var currentDayInterest = GetDailyInterestCalculationsRules().Calculate(new CalculationResult<AccountModel, double> {Input = account, Result = balance}).Result;
                totalInterest += currentDayInterest;

                balance += currentDayInterest;
            }

            return totalInterest;
        }
        public double SumTransactions(IEnumerable<TransactionModel> transactions)
        {
            return transactions.Sum(t => t.Amount);
        }

        protected internal virtual CalculationRule<AccountModel, double> GetYealyInterestCalculationsRules()
        {
            return ConfiguredCalculationRulesSingleton.YearlyInterestRateRules;
        }
        protected internal virtual CalculationRule<AccountModel, double> GetDailyInterestCalculationsRules()
        {
            return ConfiguredCalculationRulesSingleton.DailyInterestRateRules;
        }
    }
}
