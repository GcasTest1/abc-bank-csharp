using AbcBank;
using AbcBank.Data;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
using NUnit.Framework;

namespace abc_bank_tests.ServicesTests
{
    [TestFixture]
    public class BankServiceTests
    {

        public class GetCustomerSummariesMethod
        {
            [Test]
            public void ReturnsListOfCurrentCustomers()
            {
                var bank = new BankModel();
                var john = new Customer("John");
                john.OpenAccount(new Account(AccountType.Checking));
                bank.AddCustomer(john);

                var actual = new BankService().GetCustomerSummaries(bank);
                Assert.AreEqual("John", actual[0].CustomerName);
                Assert.AreEqual(1, actual[0].NumberOfAccounts);
            }
        }

        public class TotalInterestPaidMethod
        {
            private static readonly double DoubleDelta = 1e-15;

            [Test]
            public void CheckingAccount()
            {
                var bank = new BankModel();
                var checkingAccount = new Account(AccountType.Checking);
                var bill = new Customer("Bill").OpenAccount(checkingAccount);
                bank.AddCustomer(bill);

                checkingAccount.Deposit(100.0);

                Assert.AreEqual(0.1, new BankService().TotalInterestPaid(bank), DoubleDelta);
            }

            [Test]
            public void Savings_account()
            {
                var bank = new BankModel();
                var checkingAccount = new Account(AccountType.Savings);
                bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

                checkingAccount.Deposit(1500.0);

                Assert.AreEqual(2.0, new BankService().TotalInterestPaid(bank), DoubleDelta);
            }

            [Test]
            public void Maxi_savings_account()
            {
                var bank = new BankModel();
                var checkingAccount = new Account(AccountType.MaxiSavings);
                bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

                checkingAccount.Deposit(3000.0);

                Assert.AreEqual(170.0, new BankService().TotalInterestPaid(bank), DoubleDelta);
            }
        }
    }
}
