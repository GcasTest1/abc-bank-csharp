using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Customer
    {
        private readonly String _name;
        private readonly List<Account> _accounts;

        public Customer(String name)
        {
            _name = name;
            _accounts = new List<Account>();
        }

        public String GetName()
        {
            return _name;
        }

        public Customer OpenAccount(Account account)
        {
            _accounts.Add(account);
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
                total += a.InterestEarned();
            return total;
        }

        public String GetStatement() 
        {
            var statement = "Statement for " + _name + "\n";
            var total = 0.0;
            foreach (var a in _accounts) 
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.SumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String StatementForAccount(Account a) 
        {
            var s = "";

           //Translate to pretty account type
            switch(a.GetAccountType()){
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
            foreach (var t in a.Transactions) {
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
