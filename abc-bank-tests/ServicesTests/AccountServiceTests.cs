using AbcBank.Data;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic.Implementation;
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

            [Test]
            public void CheckingAccount_Returns10bpt_ForSmallAmounts()
            {
                var account = new AccountModel(AccountType.Checking);
                account.AddTransaction(new TransactionModel(100.0));

                Assert.AreEqual(0.1, new AccountService().InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void CheckingAccount_Returns10bpt_ForLargeAmounts()
            {
                var account = new AccountModel(AccountType.Checking);
                account.AddTransaction(new TransactionModel(1000000.0));

                Assert.AreEqual(1000.0, new AccountService().InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void SavingsAccount_Returns10bps_ForAmountsUnder1000()
            {
                var account = new AccountModel(AccountType.Savings);
                account.AddTransaction(new TransactionModel(900.0));

                Assert.AreEqual(0.9, new AccountService().InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void SavingsAccount_Returns20bps_ForAmountsOver1000()
            {
                var account = new AccountModel(AccountType.Savings);
                account.AddTransaction(new TransactionModel(1500.0));

                Assert.AreEqual(2.0, new AccountService().InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void MaxiSavingsAccount_Returns2Pct_WhenAmountSmaller_Than1000()
            {
                var account = new AccountModel(AccountType.MaxiSavings);
                account.AddTransaction(new TransactionModel(800.0));

                Assert.AreEqual(16.0, new AccountService().InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void MaxiSavingsAccount_Returns5Pct_WhenAmountBetween_1000and20000()
            {
                var account = new AccountModel(AccountType.MaxiSavings);
                account.AddTransaction(new TransactionModel(2500.0));

                Assert.AreEqual(120.0, new AccountService().InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void MaxiSavingsAccount_Returns10Pct_WhenAmountLarger_Than2000()
            {
                var account = new AccountModel(AccountType.MaxiSavings);
                account.AddTransaction(new TransactionModel(3000.0));

                Assert.AreEqual(170.0, new AccountService().InterestEarned(account), DoubleDelta);
            }
        }
    }
}
