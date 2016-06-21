using System.Collections.Generic;
using AbcBank.Enums;

namespace AbcBank.Data
{
    public class AccountModel
    {
        public AccountType AccountType { get; private set; }

        public IReadOnlyList<TransactionModel> Transactions
        {
            get { return _transactions; }
        }

        private List<TransactionModel> _transactions { get; }

        public AccountModel(AccountType accountType)
        {
            AccountType = accountType;
            _transactions = new List<TransactionModel>();
        }

        public void AddTransaction(TransactionModel transactionModel)
        {
            _transactions.Add(transactionModel);
        }
    }
}