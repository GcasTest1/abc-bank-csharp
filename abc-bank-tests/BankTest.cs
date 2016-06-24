using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;
using abc_bank.Enum;

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
        john.OpenAccount(AccountFactory.getInstance().GetAccount(AccountType.CHECKING));
        bank.AddCustomer(john);

        Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
    }

    [TestMethod]
    public void CheckingAccount() {
        Bank bank = new Bank();
        Account checkingAccount = AccountFactory.getInstance().GetAccount(AccountType.CHECKING);
        Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
        bank.AddCustomer(bill);

        checkingAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, 100.0));
        Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
    }

    [TestMethod]
    public void Savings_account() {
      Bank bank = new Bank();
      Account checkingAccount = AccountFactory.getInstance().GetAccount(AccountType.CHECKING);
      bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));
      checkingAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, 2000.0));
      Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
    }

    [TestMethod]
    public void Maxi_savings_account() {
      Bank bank = new Bank();
      Account checkingAccount = AccountFactory.getInstance().GetAccount(AccountType.MAXI_SAVINGS);
      bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));
      checkingAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, 3000.0));
      Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
    }
  }
}
