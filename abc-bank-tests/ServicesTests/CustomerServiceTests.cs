using System;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
using AbcBank.Logic.BusinessLogic.Implementation;
using AbcBank.Models;
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

        [TestFixture]
        public class TransferMethod
        {
            private readonly CustomerService _customerService = new CustomerService(new Mock<IAccountService>().Object);

            [Test]
            public void ThrowsArgumentException_If_Customer_DoesntHave_SourceAccount()
            {
                Assert.Throws<ArgumentException>(() => _customerService.Transfer(new CustomerModel("Bill"), AccountType.Savings, AccountType.Checking, 100.0));
            }

            [Test]
            public void ThrowsArgumentException_If_Customer_DoesntHave_DestAccount()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var customer = new CustomerModel("Bill");
                    customer.AddAccount(new AccountModel(AccountType.Savings));
                    _customerService.Transfer(customer, AccountType.Savings, AccountType.Checking, 100.0);
                });
            }

            [Test]
            public void TransfersFunds_EvenWhen_SourceDoesntHaveThem()
            {
                    var customer = new CustomerModel("Bill");
                    customer.AddAccount(new AccountModel(AccountType.Savings));
                    customer.AddAccount(new AccountModel(AccountType.Checking));
                    _customerService.Transfer(customer, AccountType.Savings, AccountType.Checking, 100.0);
            }
        }
    }
}