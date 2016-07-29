using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void Account_Deposit_Withdraw_Statement_Validate()
        {
            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingsAccount();

            Customer henry = new Customer("Henry");
            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);


            Assert.AreEqual("Statement for Henry\n\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n\n" +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        [TestMethod]
        public void Count_Num_Account_Opened_One()
        {
            Account savingsAccount = new SavingsAccount();

            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(savingsAccount);

            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void Count_Num_Account_Opened_Two()
        {

            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingsAccount();

            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(savingsAccount);
            oscar.OpenAccount(checkingAccount);

            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deposit_ZeroAmount_Throws()
        {

            Account checkingAccount = new CheckingAccount();
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(checkingAccount);

            checkingAccount.Deposit(0.0);

        }

        [TestMethod]
        public void Transfer_Amount_To_Other_Account()
        {

            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingsAccount();

            Customer henry = new Customer("Henry");
            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);

            checkingAccount.Deposit(500.0);
            savingsAccount.Deposit(100.0);
            checkingAccount.TransferToAccount(savingsAccount, 200.0);


            Assert.AreEqual(300.0, checkingAccount.Balance);
            Assert.AreEqual(300.0, savingsAccount.Balance);
        }

       
    }
}
