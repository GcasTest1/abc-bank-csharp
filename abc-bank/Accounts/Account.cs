using abc_bank.Enum;
using System;
using System.Collections.Generic;

namespace abc_bank.Accounts
{
  public abstract class Account
  {
    public string AccountId { get; set; }
    public List<Transaction> Transactions { get; }
    public AccountType AccountType { get; set; }

    public Account(AccountType accountType, string accountId) 
    {
      this.Transactions = new List<Transaction>();
      this.AccountType = accountType;
      this.AccountId = accountId;
    }

    public abstract double InterestEarned();

    public void Process(Transaction transaction)
    {
      if (transaction == null || transaction.Amount <= 0) 
          throw new ArgumentException("amount must be greater than zero");
    }

    public double SumTransactions()
    {
      double amount = 0.0;

      foreach (Transaction transaction in Transactions)
        amount += transaction.Amount;

      return amount;
    }

    public override bool Equals(object obj)
    {
      var that = obj as Account;

      if (that == null)
      {
        return false;
      }

      return this.AccountType == that.AccountType && this.AccountId.Equals(that.AccountId);
    }
  }
}
