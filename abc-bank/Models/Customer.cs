using System;
using System.Collections.Generic;
using System.Linq;
using abc_bank.Models.Accounts;

namespace abc_bank.Models
{
    public class Customer
    {
        private readonly string name;
        private readonly List<Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String GetName()
        {
            return name;
        }

        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double TotalInterestEarned()
        {
            return accounts.Sum(a => a.AnnumInterestEarned());
        }

        public IReadOnlyCollection<Account> GetAccounts()
        {
            return accounts;
        }

        public void Transfer(double amount, Account from, Account to)
        {
            if(amount <= 0)
                return;

            from.BeginTransfer(amount *-1);
            to.BeginTransfer(amount);
            to.CompleteTransfer();
            from.CompleteTransfer();
        }

        public double GetTotalBalance()
        {
            return accounts.Sum(a => a.GetAccountBalance());
        }
    }
}
