using System;
using AbcBank.Data;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
using AbcBank.Logic.Presentation;
using NUnit.Framework;

namespace abc_bank_tests
{
    [TestFixture]
    public class CustomerTest
    {
        [Test]
        public void TestApp()
        {
            var accountService = new AccountService();
            var customerService = new CustomerService();
            var checkingAccount = new AccountModel(AccountType.Checking);
            var savingsAccount = new AccountModel(AccountType.Savings);

            var henry = new CustomerModel("Henry");
            customerService
                .OpenAccount(henry, checkingAccount)
                .OpenAccount(henry, savingsAccount);

            accountService
                .Deposit(checkingAccount, 100.0)
                .Deposit(savingsAccount, 4000.0)
                .Withdraw(savingsAccount, 200.0);

            var actual = new StatementPresenter().GetStatement(henry);
            string expected = "Statement for Henry" + Environment.NewLine +
                                    Environment.NewLine +
                                    "Checking Account" + Environment.NewLine +
                                    "  deposit $100.00" + Environment.NewLine +
                                    "Total $100.00" + Environment.NewLine +
                                    Environment.NewLine +
                                    "Savings Account" + Environment.NewLine +
                                    "  deposit $4,000.00" + Environment.NewLine +
                                    "  withdrawal $200.00" + Environment.NewLine +
                                    "Total $3,800.00" + Environment.NewLine +
                                    Environment.NewLine +
                                    "Total In All Accounts $3,900.00";
            Assert.AreEqual(expected, actual);
        }

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
