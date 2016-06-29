using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;
using abc_bank.Enum;

namespace abc_bank_tests
{
  [TestClass]
  public class CustomerTest
  {
    [TestMethod]
    public void TestApp()
    {
      Account checkingAccount = AccountFactory.getInstance().GetAccount(AccountType.CHECKING);
      Account savingsAccount  = AccountFactory.getInstance().GetAccount(AccountType.SAVINGS);
      Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
      checkingAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, 100.0));
      savingsAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, 4000.0));
      savingsAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.WITHDRAWEL, 200.0));
      string statement = henry.GetStatement();
      Assert.IsTrue(statement.Contains("Checking Account  deposit $100.00"));
      Assert.IsTrue(statement.Contains("Savings Account  deposit $4,000.00"));
      Assert.IsTrue(statement.Contains("withdrawal $200.00"));
      Assert.IsTrue(statement.Contains("Total $3,800.00"));
    }

    [TestMethod]
    public void TestOneAccount()
    {
        Customer oscar = new Customer("Oscar").OpenAccount(AccountFactory.getInstance().GetAccount(AccountType.SAVINGS));
        Assert.AreEqual(1, oscar.GetNumberOfAccounts());
    }

    [TestMethod]
    public void TestTwoAccount()
    {
        Customer oscar = new Customer("Oscar")
              .OpenAccount(AccountFactory.getInstance().GetAccount(AccountType.SAVINGS));
        oscar.OpenAccount(AccountFactory.getInstance().GetAccount(AccountType.CHECKING));
        Assert.AreEqual(2, oscar.GetNumberOfAccounts());
    }

    [TestMethod]
    public void TestThreeAccounts()
    {
        Customer oscar = new Customer("Oscar")
                .OpenAccount(AccountFactory.getInstance().GetAccount(AccountType.SAVINGS));
        oscar.OpenAccount(AccountFactory.getInstance().GetAccount(AccountType.CHECKING));
        Assert.AreNotEqual(3, oscar.GetNumberOfAccounts());
    }

    [TestMethod]
    public void TestAccountTransfer()
    {
      Customer oscar = new Customer("Oscar");
      Account checkingAccount = AccountFactory.getInstance().GetAccount(AccountType.CHECKING);
      Account savingsAccount = AccountFactory.getInstance().GetAccount(AccountType.SAVINGS);
      oscar.OpenAccount(checkingAccount);
      oscar.OpenAccount(savingsAccount);

      checkingAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, 1000));
      savingsAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, 1500));
      // Transfer Money
      oscar.Transfer(savingsAccount, checkingAccount, 500);
      Assert.AreEqual(1500, checkingAccount.SumTransactions());


      checkingAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, 1000.0));
      savingsAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, 4000.0));
      savingsAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.WITHDRAWEL, 500.0));



      Assert.AreNotEqual(3, oscar.GetNumberOfAccounts());
    }
  }
}
