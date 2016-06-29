using abc_bank.Enum;
using System;

namespace abc_bank
{
  /// <summary>
  /// Representation for Bank Transaction
  /// </summary>
  public class Transaction
  {
    public double Amount { get; set; }
    public TransactionType Type { get; set; }
    private DateTime TransactionDate { get; set; }

    /// <summary>
    /// Transaction Constructor
    /// </summary>
    /// <param name="amount">Amount of Transaction</param>
    /// <param name="type">Type of Transaction</param>
    public Transaction(double amount, TransactionType type) 
    {
      this.Amount = amount;
      this.TransactionDate = DateTime.Now;
      this.Type = type;
    }
  }
}
