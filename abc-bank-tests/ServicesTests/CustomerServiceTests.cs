using AbcBank.Data;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
using AbcBank.Logic.BusinessLogic.Implementation;
using Moq;
using NUnit.Framework;

namespace abc_bank_tests.ServicesTests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        [TestFixture]
        public class OpenAccountMethod
        {
            private readonly CustomerService _customerService = new CustomerService(new Mock<IAccountService>().Object);

            [Test]
            public void TestOneAccount()
            {
                var oscar = new CustomerModel("Oscar");
                _customerService.OpenAccount(oscar, new AccountModel(AccountType.Savings));
                Assert.AreEqual(1, _customerService.GetNumberOfAccounts(oscar));
            }

            [Test]
            public void TestTwoAccounts()
            {
                var oscar = new CustomerModel("Oscar");
                _customerService
                    .OpenAccount(oscar, new AccountModel(AccountType.Savings))
                    .OpenAccount(oscar, new AccountModel(AccountType.Checking));
                Assert.AreEqual(2, _customerService.GetNumberOfAccounts(oscar));
            }

            [Test]
            public void TestThreeAccounts()
            {
                var oscar = new CustomerModel("Oscar");
                _customerService
                    .OpenAccount(oscar, new AccountModel(AccountType.Savings))
                    .OpenAccount(oscar, new AccountModel(AccountType.Checking))
                    .OpenAccount(oscar, new AccountModel(AccountType.MaxiSavings));
                Assert.AreEqual(3, _customerService.GetNumberOfAccounts(oscar));
            }
        }
    }
}