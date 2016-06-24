using abc_bank.Enum;

namespace abc_bank.Accounts
{
  public class SavingsAccount : Account
  {
    public SavingsAccount(AccountType accountType, string accountId) : base(accountType, accountId)
    {
    }

    public override double InterestEarned()
    {
      double amount = SumTransactions();

      if (amount <= 1000)
        return amount * 0.001;
      else
        return 1 + (amount - 1000) * 0.002;
    }
  }
}
