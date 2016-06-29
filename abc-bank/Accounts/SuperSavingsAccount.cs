using abc_bank.Enum;

namespace abc_bank.Accounts
{
  class SuperSavingsAccount : Account
  {
    public SuperSavingsAccount(AccountType accountType, string accountId) : base(accountType, accountId)
    {
    }

    public override double InterestEarned()
    {
      double amount = SumTransactions();

      if (amount <= 4000)
        return 20;

      return 0;
    }
  }
}
