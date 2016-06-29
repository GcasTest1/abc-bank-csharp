using abc_bank.Enum;

namespace abc_bank
{
  public class TransactionFactory
  {
    private static TransactionFactory instance = null;

    public static TransactionFactory getInstance()
    {
      if (instance == null)
        instance = new TransactionFactory();

      return instance;
    }

    public Transaction GetTransaction(TransactionType type, double amount)
    {
      switch (type)
      {
        case TransactionType.DEPOSIT:
          return new Transaction(amount, type);
        case TransactionType.WITHDRAWEL:
          return new Transaction(-1 * amount, type);
        default:
          return new Transaction(amount, TransactionType.INVALID);
      }
    }
  }
}
