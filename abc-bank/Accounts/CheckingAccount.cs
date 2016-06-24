using abc_bank.Enum;

namespace abc_bank.Accounts
{
  public class CheckingAccount : Account
  {
    public CheckingAccount(AccountType accountType, string accountId) : base(accountType, accountId)
    {
    }

    public override double InterestEarned()
    {
      return SumTransactions() * 0.001;
    }
  }
}
