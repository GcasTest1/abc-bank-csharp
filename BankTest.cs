using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using System.Collections.Generic;
using abc_bank.Transactions;
using abc_bank.Accounts;

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
            Customer<IAccount> cust = new Customer<IAccount>();
            cust.CustomerName = "John";
            List<ITransaction> trans = new List<ITransaction>();
            cust.OpenAccount(new CheckingAccount(trans,100));
            
            bank.AddCustomer(cust);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount()
        {
            Bank bank = new Bank();
            List<ITransaction> trans = new List<ITransaction>();
            CheckingAccount checkingAccount = new CheckingAccount(trans,100);
            Customer<IAccount> cust = new Customer<IAccount>();
           cust.CustomerName = "Bill";
           cust.OpenAccount(checkingAccount);
           bank.AddCustomer(cust);

           checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account()
        {
            Bank bank = new Bank();
            List<ITransaction> trans = new List<ITransaction>();
            SavingsAccount savingAccount = new SavingsAccount(trans,101);
            Customer<IAccount> cust = new Customer<IAccount>();
            cust.CustomerName = "Bill";
            bank.AddCustomer(cust.OpenAccount(savingAccount));

            savingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account()
        {
            Bank bank = new Bank();
            List<ITransaction> trans = new List<ITransaction>();
            MaxiSavingsAccount maxiSavingsAccount = new MaxiSavingsAccount(trans,102);
            Customer<IAccount> cust = new Customer<IAccount>();
            cust.CustomerName = "Bill";
            bank.AddCustomer(cust.OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
