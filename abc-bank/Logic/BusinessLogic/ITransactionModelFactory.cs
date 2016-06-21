using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic
{
    public interface ITransactionModelFactory
    {
        TransactionModel CreateTransactionModel(double amount);
    }
}