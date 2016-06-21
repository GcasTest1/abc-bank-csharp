using System;
using System.Collections.Generic;
using AbcBank.Data;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;

namespace AbcBank
{
    public class Customer
    {
        private readonly String _name;
        private readonly List<AccountModel> _accounts;
        private readonly AccountService _accountService = new AccountService();

        public Customer(String name)
        {
            _name = name;
            _accounts = new List<AccountModel>();
        }

        public String GetName()
        {
            return _name;
        }

        public Customer OpenAccount(AccountModel accountService)
        {
            _accounts.Add(accountService);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return _accounts.Count;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (var a in _accounts)
            {
                total += _accountService.InterestEarned(a);
            }
            return total;
        }

        public String GetStatement() 
        {
            var statement = "Statement for " + _name + "\n";
            var total = 0.0;
            foreach (var account in _accounts) 
            {
                statement += "\n" + StatementForAccount(account) + "\n";
                total += _accountService.SumTransactions(account);
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String StatementForAccount(AccountModel account) 
        {
            var s = "";

           //Translate to pretty account type
            switch(account.AccountType){
                case AccountType.Checking:
                    s += "Checking Account\n";
                    break;
                case AccountType.Savings:
                    s += "Savings Account\n";
                    break;
                case AccountType.MaxiSavings:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            var total = 0.0;
            foreach (var t in account.Transactions) {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double d)
        {
            return String.Format("{0:C}", Math.Abs(d));
        }
    }
}
