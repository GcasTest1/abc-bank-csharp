using System;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic.Implementation;
using AbcBank.Models;
using NUnit.Framework;

namespace abc_bank_tests.ServicesTests
{
    [TestFixture]
    public class AccountServiceTests
    {
        [TestFixture]
        public class InterestEarnedMethod
        {
            private static readonly double DoubleDelta = 1e-15;
            private readonly AccountService _accountService = new AccountService(null);

            [Test]
            public void CheckingAccount_Returns10bpt_ForSmallAmounts()
            {
                var account = new AccountModel(AccountType.Checking);
                account.AddTransaction(new TransactionModel(100.0, DateTime.Now));

                Assert.AreEqual(0.1, _accountService.InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void CheckingAccount_Returns10bpt_ForLargeAmounts()
            {
                var account = new AccountModel(AccountType.Checking);
                account.AddTransaction(new TransactionModel(1000000.0, DateTime.Now));

                Assert.AreEqual(1000.0, _accountService.InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void SavingsAccount_Returns10bps_ForAmountsUnder1000()
            {
                var account = new AccountModel(AccountType.Savings);
                account.AddTransaction(new TransactionModel(900.0, DateTime.Now));

                Assert.AreEqual(0.9, _accountService.InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void SavingsAccount_Returns20bps_ForAmountsOver1000()
            {
                var account = new AccountModel(AccountType.Savings);
                account.AddTransaction(new TransactionModel(1500.0, DateTime.Now));

                Assert.AreEqual(2.0, _accountService.InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void MaxiSavingsAccount_Returns5Pct_WhenNoWithdrawlInLast10Days()
            {
                var account = new AccountModel(AccountType.MaxiSavings);
                account.AddTransaction(new TransactionModel(800.0, DateTime.Now.AddDays(-1)));

                Assert.AreEqual(40.0, _accountService.InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void MaxiSavingsAccount_Returns1Bsp_WhenWithdrawlInLast10Days()
            {
                var account = new AccountModel(AccountType.MaxiSavings);
                account.AddTransaction(new TransactionModel(1000.0, DateTime.Now.AddDays(-1)));
                account.AddTransaction(new TransactionModel(-800.0, DateTime.Now.AddDays(-1)));

                Assert.AreEqual(0.2, _accountService.InterestEarned(account), DoubleDelta);
            }
        }
    }
}
