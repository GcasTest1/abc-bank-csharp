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
                var customerService = new CustomerService();
                var bank = new BankModel();
                var john = new CustomerModel("John");
                customerService.OpenAccount(john, new AccountModel(AccountType.Checking));
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
                var customerService = new CustomerService();
                var bank = new BankModel();
                var checkingAccount = new AccountModel(AccountType.Checking);
                var bill = new CustomerModel("Bill");
                customerService.OpenAccount(bill, checkingAccount);
                bank.AddCustomer(bill);

                new AccountService().Deposit(checkingAccount, 100.0);

                Assert.AreEqual(0.1, new BankService().TotalInterestPaid(bank), DoubleDelta);
            }

            [Test]
            public void Savings_account()
            {
                var customerService = new CustomerService();
                var bank = new BankModel();
                var checkingAccount = new AccountModel(AccountType.Savings);
                var bill = new CustomerModel("Bill");
                customerService.OpenAccount(bill, checkingAccount);
                bank.AddCustomer(bill);

                new AccountService().Deposit(checkingAccount, 1500.0);

                Assert.AreEqual(2.0, new BankService().TotalInterestPaid(bank), DoubleDelta);
            }

            [Test]
            public void Maxi_savings_account()
            {
                var customerService = new CustomerService();
                var bank = new BankModel();
                var checkingAccount = new AccountModel(AccountType.MaxiSavings);
                var bill = new CustomerModel("Bill");
                customerService.OpenAccount(bill, checkingAccount);
                bank.AddCustomer(bill);

                new AccountService().Deposit(checkingAccount, 3000.0);

                Assert.AreEqual(170.0, new BankService().TotalInterestPaid(bank), DoubleDelta);
            }
        }
    }
}
