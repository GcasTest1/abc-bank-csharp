using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            var checkingAccount = new Account(AccountType.Checking);
            var savingsAccount = new Account(AccountType.Savings);

            var henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            var actual = henry.GetStatement();
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

        [TestMethod]
        public void TestOneAccount()
        {
            var oscar = new Customer("Oscar").OpenAccount(new Account(AccountType.Savings));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            var oscar = new Customer("Oscar")
                 .OpenAccount(new Account(AccountType.Savings));
            oscar.OpenAccount(new Account(AccountType.Checking));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        [Ignore]
        public void TestThreeAccounts()
        {
            var oscar = new Customer("Oscar")
                    .OpenAccount(new Account(AccountType.Savings));
            oscar.OpenAccount(new Account(AccountType.Checking));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
