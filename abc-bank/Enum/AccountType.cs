using System.ComponentModel;

namespace abc_bank.Enum
{
  /// <summary>
  /// Enumeration of Account Types
  /// </summary>
  public enum AccountType
  {
    [Description("Checking Account")]
    CHECKING,
    [Description("Savings Account")]
    SAVINGS,
    [Description("Maxi Savings Account")]
    MAXI_SAVINGS,
    [Description("Super Savings Account")]
    SUPER_SAVINGS
  }
}
