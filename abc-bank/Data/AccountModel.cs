using System.Collections.Generic;
using AbcBank.Enums;

namespace AbcBank.Data
{
    public class AccountModel
    {
        public AccountType AccountType { get; private set; }

        public IReadOnlyList<Transaction> Transactions
        {
            get { return _transactions; }
        }

        private List<Transaction> _transactions { get; }

        public AccountModel(AccountType accountType)
        {
            AccountType = accountType;
            _transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }
    }
}