using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;
using abc_bank.Transactions;
using System.Collections.Generic;
using System.Linq;
using abc_bank.EntityParams;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            List<ITransaction> lstTransactionsChecking = new List<ITransaction>();
            List<ITransaction> lstTransactionsSaving = new List<ITransaction>();
            CheckingAccount checkingAccount = new CheckingAccount(lstTransactionsChecking,100);
            SavingsAccount savingsAccount = new SavingsAccount(lstTransactionsSaving,101);

            Customer<Account> cust = new Customer<Account>();
            cust.CustomerName = "Henry";
            checkingAccount.Deposit(100.0);
            cust.OpenAccount(checkingAccount);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);
            cust.OpenAccount(savingsAccount);
                     

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  Deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  Deposit $4,000.00\n" +
                    "  Withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00",cust.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer<Account> cust = new Customer<Account>();
            cust.CustomerName = "Oscar";
            List<ITransaction> lstTransactionsSaving = new List<ITransaction>();
            SavingsAccount savingsAccount = new SavingsAccount(lstTransactionsSaving, 100);
            cust.OpenAccount(savingsAccount);
          Assert.AreEqual(1, cust.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
           

            Customer<Account> cust = new Customer<Account>();
            cust.CustomerName = "Oscar";
            List<ITransaction> lstTransactionsSaving = new List<ITransaction>();
            List<ITransaction> lstTransactionsChecking = new List<ITransaction>();
            CheckingAccount checkingAccount = new CheckingAccount(lstTransactionsChecking, 100);
            SavingsAccount savingsAccount = new SavingsAccount(lstTransactionsSaving, 101);
            cust.OpenAccount(savingsAccount);
            cust.OpenAccount(checkingAccount);
            Assert.AreEqual(2, cust.GetNumberOfAccounts());
        }

        [TestMethod]
       public void TestThreeAccounts()
        {

            Customer<Account> cust = new Customer<Account>();
            cust.CustomerName = "Oscar";
            List<ITransaction> lstTransactionsSaving = new List<ITransaction>();
            List<ITransaction> lstTransactionsChecking = new List<ITransaction>();
            List<ITransaction> lstTransactionsMaxiAccount = new List<ITransaction>();
            CheckingAccount checkingAccount = new CheckingAccount(lstTransactionsChecking, 100);
            SavingsAccount savingsAccount = new SavingsAccount(lstTransactionsSaving, 101);
            MaxiSavingsAccount maxiSavingsAccount = new MaxiSavingsAccount (lstTransactionsSaving, 102);
            cust.OpenAccount(savingsAccount);
            cust.OpenAccount(checkingAccount);
            cust.OpenAccount(maxiSavingsAccount);
            Assert.AreEqual(3, cust.GetNumberOfAccounts());
            
        }
        /// <summary>
        /// This would create only one account,since there is already a savings account exist 
        /// </summary>
        [TestMethod]
        public void TestDuplicateAccounts()
        {
            Customer<Account> cust = new Customer<Account>();
            cust.CustomerName = "Oscar";
            List<ITransaction> lstTransactionsSaving = new List<ITransaction>();
            SavingsAccount savingsAccount = new SavingsAccount(lstTransactionsSaving, 100);
            cust.OpenAccount(savingsAccount);
            SavingsAccount savingsAccount2 = new SavingsAccount(lstTransactionsSaving, 100);
            cust.OpenAccount(savingsAccount2);
            Assert.AreEqual(1, cust.GetNumberOfAccounts());

        }

        /// This would create only one account,since there is already a savings account exist 
        /// </summary>
        [TestMethod]
        public void TestTransactionBetweenAccounts()
        {
            Customer<Account> cust = new Customer<Account>();
            cust.CustomerName = "Oscar";
            List<ITransaction> lstTransactionsSaving = new List<ITransaction>();
            List<ITransaction> lstTransactionsChecking = new List<ITransaction>();
            CheckingAccount checkingAccount = new CheckingAccount(lstTransactionsChecking, 100);
            SavingsAccount savingsAccount = new SavingsAccount(lstTransactionsSaving, 101);
            cust.OpenAccount(savingsAccount);
            cust.OpenAccount(checkingAccount);
            checkingAccount.Deposit(10000.0);
            savingsAccount.Deposit(4000.0);
            AccountTransfer accountTransferParams = new AccountTransfer();
            accountTransferParams.TransferAmount = 2000;
            accountTransferParams.AccountFrom = checkingAccount;
            accountTransferParams.AccountTo = savingsAccount;
            cust.TransferTheFund(accountTransferParams);
            Assert.AreEqual(8000, cust.Accounts[1].SumTransactions());
            Assert.AreEqual(6000, cust.Accounts[0].SumTransactions());
        }
        /// <summary>
        /// Here Transaction would not happen since no balance.
        /// </summary>
        [TestMethod]
        public void TestTransactionBetweenAccountsWithNoBalance()
        {
            Customer<Account> cust = new Customer<Account>();
            cust.CustomerName = "Oscar";
            List<ITransaction> lstTransactionsSaving = new List<ITransaction>();
            List<ITransaction> lstTransactionsChecking = new List<ITransaction>();
            CheckingAccount checkingAccount = new CheckingAccount(lstTransactionsChecking, 100);
            SavingsAccount savingsAccount = new SavingsAccount(lstTransactionsSaving, 101);
            cust.OpenAccount(savingsAccount);
            cust.OpenAccount(checkingAccount);
            checkingAccount.Deposit(10000.0);
            savingsAccount.Deposit(4000.0);
            AccountTransfer accountTransferParams = new AccountTransfer();
            accountTransferParams.TransferAmount = 12000;
            accountTransferParams.AccountFrom = checkingAccount;
            accountTransferParams.AccountTo = savingsAccount;
            cust.TransferTheFund(accountTransferParams);
            Assert.AreEqual(10000, cust.Accounts[1].SumTransactions());
            Assert.AreEqual(4000, cust.Accounts[0].SumTransactions());
        }


        /// <summary>
        /// Here Interest would be calculated on .05% ,no Withdrawal happend for past 10 days .
        /// </summary>
        [TestMethod]
        public void TestMaxiAccountInterestEarnedWithNoWithdrawalsForPast10Days()
        {
            Customer<Account> cust = new Customer<Account>();
            cust.CustomerName = "Oscar";
            List<ITransaction> lstTransactionsSaving = new List<ITransaction>();
           
            MaxiSavingsAccount savingsAccount = new MaxiSavingsAccount(lstTransactionsSaving, 101);
            cust.OpenAccount(savingsAccount);
            savingsAccount.Deposit(2000.0);
            savingsAccount.Withdraw(1000.0);
            DateTime withDrawDt = DateTime.Today.AddDays(-20);
            savingsAccount.Transactions.Where(g => g.GetType() == typeof(Withdraw))
                .FirstOrDefault().SetTransactionDate(withDrawDt);

            double interestRate = Math.Round(savingsAccount.InterestEarned(),2);
            Assert.AreEqual(51.27, interestRate);
            
        }


        /// <summary>
        /// Here Interest would be calculated on .001% ,Since happend for past 10 days .
        /// </summary>
        [TestMethod]
        public void TestMaxiAccountInterestEarnedWithWithdrawalsForPast10Days()
        {
            Customer<Account> cust = new Customer<Account>();
            cust.CustomerName = "Oscar";
            List<ITransaction> lstTransactionsSaving = new List<ITransaction>();

            MaxiSavingsAccount savingsAccount = new MaxiSavingsAccount(lstTransactionsSaving, 101);
            cust.OpenAccount(savingsAccount);
            savingsAccount.Deposit(2000.0);
            savingsAccount.Withdraw(1000.0);
            double interestRate = Math.Round(savingsAccount.InterestEarned(), 2);
            Assert.AreEqual(1.00, interestRate);

        }
    }
}

