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
            public void CheckingAccount()
            {
                var account = new AccountModel(AccountType.Checking);
                account.AddTransaction(new TransactionModel(100.0));

                Assert.AreEqual(0.1, new AccountService().InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void Savings_account()
            {
                var account = new AccountModel(AccountType.Savings);
                account.AddTransaction(new TransactionModel(1500.0));

                Assert.AreEqual(2.0, new AccountService().InterestEarned(account), DoubleDelta);
            }

            [Test]
            public void Maxi_savings_account()
            {
                var account = new AccountModel(AccountType.MaxiSavings);
                account.AddTransaction(new TransactionModel(3000.0));

                Assert.AreEqual(170.0, new AccountService().InterestEarned(account), DoubleDelta);
            }
        }
    }
}
