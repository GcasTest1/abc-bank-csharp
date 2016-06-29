using abc_bank.Accounts;
using abc_bank.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace abc_bank
{
  public class Customer
  {
    private String name;
    private List<Account> accounts;

    public Customer(String name)
    {
        this.name = name;
        this.accounts = new List<Account>();
    }

    public List<Account> Accounts
    {
      get { return accounts; }
    }

    public String Name
    {
      get { return name; }
      set { name = value; }
    }

    public object Enumerations { get; private set; }

    public Customer OpenAccount(Account account)
    {
        Accounts.Add(account);
        return this;
    }

    public void Transfer(Account fromAccount, Account toAccount, double amount)
    {
      if (fromAccount.Equals(toAccount) || !Accounts.Contains(fromAccount) || !Accounts.Contains(toAccount) || amount <= 0)
      {
        throw new Exception(String.Format("Invalid Transfer from account {0} to account {1}", fromAccount.AccountId, toAccount.AccountId));
      }

      fromAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.WITHDRAWEL, amount));
      toAccount.Transactions.Add(TransactionFactory.getInstance().GetTransaction(TransactionType.DEPOSIT, amount));
    }

    public int GetNumberOfAccounts()
    {
      if (Accounts == null)
        return 0;

      return Accounts.Count;
    }

    public double TotalInterestEarned() 
    {
      double total = 0;

      foreach (Account account in Accounts)
          total += account.InterestEarned();

      return total;
    }

    public String GetStatement() 
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(String.Format("Statement for {0}\n", name));
      double total = 0.0;

      foreach (Account account in Accounts) 
      {
        sb.Append(String.Format("\n{0}\n", StatementForAccount(account)));
        total += account.SumTransactions();
      }

      sb.Append("\nTotal In All Accounts " + ToDollars(total));
      return sb.ToString();
    }

    private String StatementForAccount(Account account) 
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(EnumHelper.Description<AccountType>(account.AccountType));

      //Now total up all the transactions
      double total = 0.0;

      foreach (Transaction transaction in account.Transactions) {
        sb.Append(String.Format("  {0} {1}\n", transaction.Amount < 0 ? "withdrawal" : "deposit", ToDollars(transaction.Amount)));
        total += transaction.Amount;
      }

      sb.Append("Total " + ToDollars(total));
      return sb.ToString();
     }

    private String ToDollars(double d)
    {
        return String.Format("{0:C}", Math.Abs(d));
    }
  }
}
