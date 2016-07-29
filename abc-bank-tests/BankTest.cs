using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Contract;
using abc_bank.Accounts;


namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void Bank_CustomerSummary_Validate()
        {
            Bank bank = new Bank();
            Account checkingAccount = new CheckingAccount();
            Customer john = new Customer("John");

            john.OpenAccount(checkingAccount);
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount_Calculate_Interest()
        {
            Bank bank = new Bank();
            Account checkingAccount = new CheckingAccount();
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void SavingsAccount_Calculate_Interest()
        {
            Bank bank = new Bank();
            Account savingsAccount = new SavingsAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiSavingsAccount_Calculate_Interestt()
        {
            Bank bank = new Bank();
            Account maxiSavingAccount = new MaxiSavingsAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingAccount));

            maxiSavingAccount.Deposit(3000.0);

            Assert.AreEqual(150.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiSavingsAccount_Withdraw_In_ElevenDays_Interest_Rule_Validate()
        {
            Bank bank = new Bank();

            //Use fake dateprovider to create withdraw transaction more than 10 days.
            //Any IOC container could be used here in the future.
            FakeDateProvider_Eleven_Days transDateProvider = new FakeDateProvider_Eleven_Days();
            Account maxiSavingAccount = new MaxiSavingsAccount(transDateProvider);

            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingAccount));

            maxiSavingAccount.Deposit(3000.0);
            maxiSavingAccount.Withdraw(1000.0);

            //Use 5% for interest since last withdraw was more than 10 days ago (11 days).
            Assert.AreEqual(100.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        public void MaxiSavingsAccount_Withdraw_In_FiveDays_Interest_Rule_Validate()
        {
            Bank bank = new Bank();

            //Use fake dateprovider to create withdraw transaction more than 5 days.
            //Any IOC container could be used here in the future.
            FakeDateProvider_Five_Days transDateProvider = new FakeDateProvider_Five_Days();
            Account maxiSavingAccount = new MaxiSavingsAccount(transDateProvider);

            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingAccount));

            maxiSavingAccount.Deposit(3000.0);
            maxiSavingAccount.Withdraw(1000.0);

            //Use 0.1% for interest since last withdraw was in 10 days (5 days).
            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        //Inject to account/transction so the transction date will be 11 days ago
        public class FakeDateProvider_Eleven_Days : IDateProvider
        {
            public DateTime GetDateTimeNow()
            {
                return DateTime.Now.AddDays(-11);
            }
        }

        //Inject to account/transction so the transction date will be 5 days ago
        public class FakeDateProvider_Five_Days : IDateProvider
        {
            public DateTime GetDateTimeNow()
            {
                return DateTime.Now.AddDays(-5);
            }
        }
    }
}
