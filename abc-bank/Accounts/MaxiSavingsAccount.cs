using abc_bank.Enum;

namespace abc_bank.Accounts
{
  public class MaxiSavingsAccount : Account
  {
    public MaxiSavingsAccount(AccountType accountType, string accountId) : base(accountType, accountId)
    {
    }

    public override double InterestEarned()
    {
      double amount = SumTransactions();

      if (amount <= 1000)
        return amount * 0.02;

      if (amount <= 2000)
        return 20 + (amount - 1000) * 0.05;

      return 70 + (amount - 2000) * 0.1;
    }
  }
}
