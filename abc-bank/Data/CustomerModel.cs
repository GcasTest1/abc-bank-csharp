using System;
using System.Collections.Generic;

namespace AbcBank.Data
{
    public class CustomerModel
    {
        private readonly List<AccountModel> _accounts;

        public string Name { get; private set; }

        public IReadOnlyList<AccountModel> Accounts
        {
            get { return _accounts; }
        }

        public CustomerModel(string name)
        {
            Name = name;
            _accounts = new List<AccountModel>();
        }

        public void AddAccount(AccountModel account)
        {
            _accounts.Add(account);
        }
    }
}