using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic.Implementation
{
    public class TransactionModelFactory : ITransactionModelFactory
    {
        private readonly DateProvider _dateProvider;

        public TransactionModelFactory(DateProvider dateProvider)
        {
            _dateProvider = DateProvider.GetInstance();
        }

        public TransactionModel CreateTransactionModel(double amount)
        {
            return new TransactionModel(amount, _dateProvider.Now());
        }
    }
}