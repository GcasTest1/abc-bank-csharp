using AbcBank;
using AbcBank.Data;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
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
            customerService.OpenAccount(henry, checkingAccount);
            customerService.OpenAccount(henry, savingsAccount);

            accountService.Deposit(checkingAccount, 100.0);
            accountService.Deposit(savingsAccount, 4000.0);
            accountService.Withdraw(savingsAccount, 200.0);

            var actual = customerService.GetStatement(henry);
            const string expected = "Statement for Henry\n" +
                                    "\n" +
                                    "Checking Account\n" +
                                    "  deposit $100.00\n" +
                                    "Total $100.00\n" +
                                    "\n" +
                                    "Savings Account\n" +
                                    "  deposit $4,000.00\n" +
                                    "  withdrawal $200.00\n" +
                                    "Total $3,800.00\n" +
                                    "\n" +
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
            customerService.OpenAccount(oscar, new AccountModel(AccountType.Savings));
            customerService.OpenAccount(oscar, new AccountModel(AccountType.Checking));
            Assert.AreEqual(2, customerService.GetNumberOfAccounts(oscar));
        }

        [Test]
        public void TestThreeAccounts()
        {
            var customerService = new CustomerService();
            var oscar = new CustomerModel("Oscar");
            customerService.OpenAccount(oscar, new AccountModel(AccountType.Savings));
            customerService.OpenAccount(oscar, new AccountModel(AccountType.Checking));
            customerService.OpenAccount(oscar, new AccountModel(AccountType.MaxiSavings));
            Assert.AreEqual(3, customerService.GetNumberOfAccounts(oscar));
        }
    }
}
