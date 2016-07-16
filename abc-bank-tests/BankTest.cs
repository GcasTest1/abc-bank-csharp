using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;


        [TestMethod]
        public void CustomerSummary()
        {
            Bank bank = new Bank();
            Customer john = bank.AddCustomer("John");          
            john.OpenAccount(new CheckingAccount());          

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        //Test case changed to test daily accrued interest rate
        public void CheckingAccount()
        {
            Bank bank = new Bank();
            Customer bill = bank.AddCustomer("Bill");
            Account checkingAccount = bill.OpenAccount(AccountType.CHECKING);
            checkingAccount.Deposit(1000.0);
            Assert.AreEqual(0.002739726027397, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        ///New test case to test failure of withdrawal when there are insufficient funds
        public void CheckingAccountWithdrawalInsufficientFunds()
        {
            Bank bank = new Bank();
            Customer bill = bank.AddCustomer("Bill");
            Account checkingAccount = bill.OpenAccount(AccountType.CHECKING);          
            checkingAccount.Withdraw(1000.0);           
        }

        [TestMethod]
        //Test case changed to test daily accrued interest rate
        public void Savings_account()
        {
            Bank bank = new Bank();
            Customer bill = bank.AddCustomer("Bill");
            Account savingsAccount = bill.OpenAccount(AccountType.SAVINGS);
            savingsAccount.Deposit(1500.0);
            Assert.AreEqual(0.005479452054795, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        //Test case changed to test daily accrued interest rate of 5% when there is no withdrawal in the past 10 days
        public void Maxi_savings_account_no_withdrawal()
        {
            Bank bank = new Bank();
            Customer bill = bank.AddCustomer("Bill");
            Account maxiSavingsAccount = bill.OpenAccount(AccountType.MAXI_SAVINGS);
            maxiSavingsAccount.Deposit(3000.0);
            Assert.AreEqual(0.410958904109590, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        //Test case changed to test daily accrued interest rate of 5% when there is a withdrawal in the past 10 days
        public void Maxi_savings_account_withdrawal()
        {
            Bank bank = new Bank();
            Customer bill = bank.AddCustomer("Bill");
            Account maxiSavingsAccount = bill.OpenAccount(AccountType.MAXI_SAVINGS);
            maxiSavingsAccount.Deposit(3000.0);
            maxiSavingsAccount.Withdraw(1000.0);
            Assert.AreEqual(0.005479452054795, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        ///New test case to test transfer funds
        public void TransferFundsInSufficientFunds()
        {
            Bank bank = new Bank();            
            Customer john = bank.AddCustomer("John");
            Account checkingAccount = john.OpenAccount(AccountType.CHECKING);
            Account savingsAccount = john.OpenAccount(AccountType.SAVINGS);           
            john.TransferFunds(checkingAccount, savingsAccount, 1000);
        }

        [TestMethod]    
        ///New test case to test transfer funds
        public void TransferFunds()
        {
            Bank bank = new Bank();
            Customer john = bank.AddCustomer("John");
            Account checkingAccount = john.OpenAccount(AccountType.CHECKING);
            Account savingsAccount = john.OpenAccount(AccountType.SAVINGS);
            checkingAccount.Deposit(3000.0);
            john.TransferFunds(checkingAccount, savingsAccount, 1000);
            Assert.AreEqual(0.008219178082192, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
