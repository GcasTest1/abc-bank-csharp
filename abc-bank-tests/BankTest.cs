using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Models;
using abc_bank.Models.Accounts;
using abc_bank.Services;

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
            Customer john = new Customer("John");
            john.OpenAccount(new CheckingAccount());
            bank.AddCustomer(john);
            var reportProvider = new ReportProvider();

            Assert.AreEqual("Customer Summary\n - John (1 account)", reportProvider.CustomerSummary(bank.Customers));
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new CheckingAccount();
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new SavingAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }
        
    }
}
