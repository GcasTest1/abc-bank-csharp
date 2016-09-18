using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Models;
using abc_bank.Models.Accounts;
using abc_bank.Services;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestCustomerTotalBalance()
        {
            //Arrange
            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingAccount();

            //Act
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            //Assert
            Assert.AreEqual(henry.GetTotalBalance(), 3900);
        }

        [TestMethod]
        public void TestCustomerStatement()
        {
            //Arrange
            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingAccount();
            var reportProvider = new ReportProvider();

            //Act
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            //Assert
            Assert.AreEqual("Statement for Henry\n" +
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
                    "Total In All Accounts $3,900.00", reportProvider.GetStatement(henry));
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new SavingAccount());
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new SavingAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new SavingAccount());
            oscar.OpenAccount(new CheckingAccount());
            oscar.OpenAccount(new MaxiSavingAccount());
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void When_TransferFromAccount_ThenAmountHaveToBeMovedToNewAccount()
        {
            //Arrange
            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingAccount();

            //Act
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            henry.Transfer(500, savingsAccount,checkingAccount);

            //Assert
            Assert.AreEqual(600, checkingAccount.GetAccountBalance());
            Assert.AreEqual(3500, savingsAccount.GetAccountBalance());
        }
    }
}
