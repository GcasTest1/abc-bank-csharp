using AbcBank.Data;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
using NUnit.Framework;

namespace abc_bank_tests.ServicesTests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        [TestFixture]
        public class OpenAccountMethod
        {
            [Test]
            public void TestOneAccount()
            {
                var customerService = new CustomerService();
                var oscar = new CustomerModel("Oscar");
                customerService.OpenAccount(oscar, new AccountModel(AccountType.Savings));
                Assert.AreEqual(1, customerService.GetNumberOfAccounts(oscar));
            }

            [Test]
            public void TestTwoAccounts()
            {
                var customerService = new CustomerService();
                var oscar = new CustomerModel("Oscar");
                customerService
                    .OpenAccount(oscar, new AccountModel(AccountType.Savings))
                    .OpenAccount(oscar, new AccountModel(AccountType.Checking));
                Assert.AreEqual(2, customerService.GetNumberOfAccounts(oscar));
            }

            [Test]
            public void TestThreeAccounts()
            {
                var customerService = new CustomerService();
                var oscar = new CustomerModel("Oscar");
                customerService
                    .OpenAccount(oscar, new AccountModel(AccountType.Savings))
                    .OpenAccount(oscar, new AccountModel(AccountType.Checking))
                    .OpenAccount(oscar, new AccountModel(AccountType.MaxiSavings));
                Assert.AreEqual(3, customerService.GetNumberOfAccounts(oscar));
            }
        }
    }
}