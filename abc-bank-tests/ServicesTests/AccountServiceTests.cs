using System;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic.Implementation;
using AbcBank.Logic.BusinessLogic.Implementation.IntererestCalculation;
using AbcBank.Models;
using Moq;
using NUnit.Framework;

namespace abc_bank_tests.ServicesTests
{
    [TestFixture]
    public class AccountServiceTests
    {
        [TestFixture]
        public class YearlyInterestEarnedMethod
        {
            private static readonly double DoubleDelta = 1e-15;
            private readonly AccountService _accountService = new AccountService(null);

            [Test]
            public void CheckingAccount_Returns10bpt_ForSmallAmounts()
            {
                var account = new AccountModel(AccountType.Checking);
                account.AddTransaction(new TransactionModel(100.0, DateTime.Now));

                Assert.AreEqual(0.1, _accountService.YearlyInterestEarned(account), DoubleDelta);
            }

            [Test]
            public void CheckingAccount_Returns10bpt_ForLargeAmounts()
            {
                var account = new AccountModel(AccountType.Checking);
                account.AddTransaction(new TransactionModel(1000000.0, DateTime.Now));

                Assert.AreEqual(1000.0, _accountService.YearlyInterestEarned(account), DoubleDelta);
            }

            [Test]
            public void SavingsAccount_Returns10bps_ForAmountsUnder1000()
            {
                var account = new AccountModel(AccountType.Savings);
                account.AddTransaction(new TransactionModel(900.0, DateTime.Now));

                Assert.AreEqual(0.9, _accountService.YearlyInterestEarned(account), DoubleDelta);
            }

            [Test]
            public void SavingsAccount_Returns20bps_ForAmountsOver1000()
            {
                var account = new AccountModel(AccountType.Savings);
                account.AddTransaction(new TransactionModel(1500.0, DateTime.Now));

                Assert.AreEqual(2.0, _accountService.YearlyInterestEarned(account), DoubleDelta);
            }

            [Test]
            public void MaxiSavingsAccount_Returns5Pct_WhenNoWithdrawlInLast10Days()
            {
                var account = new AccountModel(AccountType.MaxiSavings);
                account.AddTransaction(new TransactionModel(800.0, DateTime.Now.AddDays(-1)));

                Assert.AreEqual(40.0, _accountService.YearlyInterestEarned(account), DoubleDelta);
            }

            [Test]
            public void MaxiSavingsAccount_Returns1Bsp_WhenWithdrawlInLast10Days()
            {
                var account = new AccountModel(AccountType.MaxiSavings);
                account.AddTransaction(new TransactionModel(1000.0, DateTime.Now.AddDays(-1)));
                account.AddTransaction(new TransactionModel(-800.0, DateTime.Now.AddDays(-1)));

                Assert.AreEqual(0.2, _accountService.YearlyInterestEarned(account), DoubleDelta);
            }
        }

        [TestFixture]
        public class DailyInterestEarnedMethod
        {
            private static readonly double DoubleDelta = 1e-15;
            private readonly AccountService _accountService = new AccountService(null);

            [Test]
            public void CompoundsOnEveryDay_SinceEarliestTransaction()
            {
                var account = new AccountModel(AccountType.Checking);
                account.AddTransaction(new TransactionModel(10000.0, DateTime.Now.AddDays(-3)));
                account.AddTransaction(new TransactionModel(10000.0, DateTime.Now.AddDays(-10)));
                account.AddTransaction(new TransactionModel(10000.0, DateTime.Now.AddDays(-5)));

                var accountService = new Mock<AccountService>(null) {CallBase = true};
                accountService.Object.InterestEarned(account);
                accountService.Verify(i => i.GetDailyInterestCalculationsRules(), Times.Exactly(11));
            }

            // Other tests for the reducers go here.
        }
    }
}
