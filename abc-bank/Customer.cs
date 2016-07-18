using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        private String name;
        private Dictionary<int, Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new Dictionary<int, Account>();
        }

        public String GetName()
        {
            return name;
        }

        public Customer OpenAccount(Account account)
        {
            if (accounts.ContainsKey(account.AccountId))
            {
                throw new ApplicationException(string.Format("account {0} already exists", account.AccountId));
            }
            accounts.Add(account.AccountId, account);
            return this;
        }

        //refactored code: overload method to return account type object based on AccountType enum
        public Account OpenAccount(AccountType accountType)
        {
          Account account = null;
          switch(accountType)
            {
                case AccountType.CHECKING:
                    account = new CheckingAccount();
                    break;

                case AccountType.SAVINGS:
                    account = new SavingsAccount();
                    break;

                case AccountType.MAXI_SAVINGS:
                    account = new MaxiSavingsAccount();
                    break;
            }
            accounts.Add(account.AccountId, account);
            return account;
        }

        /// <summary>
        /// New feature : A customer can transfer between their accounts
        /// </summary>
        /// <param name="fromAccount"></param>
        /// <param name="toAccount"></param>
        /// <param name="amount"></param>
        public void TransferFunds(Account fromAccount, Account toAccount, double amount)
        {
            if (fromAccount.AccountId == toAccount.AccountId)
            {
                throw new ArgumentException("different accounts required for funds transfer");
            }
            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in accounts.Values)
                total += a.InterestEarned();
            return total;
        }

        public String GetStatement()
        {
            String statement = null;
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts.Values)
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String statementForAccount(Account a)
        {
            String s = "";

            //Translate to pretty account type
            switch (a.GetAccountType)
            {
                case AccountType.CHECKING:
                    s += "Checking Account\n";
                    break;
                case AccountType.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case AccountType.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.Transactions)
            {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        //fixed issue with string format
        private String ToDollars(double d)
        {
           // return String.Format("$%,.2f", Math.Abs(d));
            return String.Format("{0:C2}", Math.Abs(d));
        }
    }
}
