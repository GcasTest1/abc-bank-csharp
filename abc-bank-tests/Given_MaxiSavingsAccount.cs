using System;
using abc_bank.Models;
using abc_bank.Models.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class Given_MaxiSavingsAccount
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void When_OnlyDepositeMade_ThenInterestRateShouldBe5persent()
        {
            //Arrange
            Bank bank = new Bank();
            Account checkingAccount = new MaxiSavingAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            //Act
            checkingAccount.Deposit(3000.0);

            //Assert
            Assert.AreEqual(150.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void When_OnlyDepositeWithWithdrawMade_ThenInterestRateShouldBe01persent()
        {
            //Arrange
            Bank bank = new Bank();
            Account checkingAccount = new MaxiSavingAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            //Act
            checkingAccount.Deposit(3100.0);
            checkingAccount.Withdraw(100.0);

            //Assert
            Assert.AreEqual(3.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
