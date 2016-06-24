using abc_bank.Enum;
using System;

namespace abc_bank.Accounts
{
  public class AccountFactory
  {
    private static AccountFactory instance = null;

    public static AccountFactory getInstance()
    {
      if (instance == null)
        instance = new AccountFactory();

      return instance;
    }

    public Account GetAccount(AccountType accountType)
    {
      switch(accountType)
      {
        case AccountType.CHECKING:
          return new CheckingAccount(accountType, Environment.TickCount.ToString());
        case AccountType.MAXI_SAVINGS:
          return new MaxiSavingsAccount(accountType, Environment.TickCount.ToString());
        case AccountType.SAVINGS:
          return new SavingsAccount(accountType, Environment.TickCount.ToString());
        case AccountType.SUPER_SAVINGS:
          return new SuperSavingsAccount(accountType, Environment.TickCount.ToString());
        default:
          return new SavingsAccount(accountType, Environment.TickCount.ToString());
      };
    }

  }
}
