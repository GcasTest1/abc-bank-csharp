using System;
using AbcBank.Data;
using AbcBank.Enums;

namespace AbcBank.Logic.BusinessLogic
{
    public class CustomerService
    {
        private readonly AccountService _accountService = new AccountService();

        public CustomerService OpenAccount(CustomerModel customer, AccountModel account)
        {
            customer.AddAccount(account);
            return this;
        }

        public int GetNumberOfAccounts(CustomerModel customer)
        {
            return customer.Accounts.Count;
        }

        public double TotalInterestEarned(CustomerModel customer) 
        {
            double total = 0;
            foreach (var a in customer.Accounts)
            {
                total += _accountService.InterestEarned(a);
            }
            return total;
        }

        public String GetStatement(CustomerModel customer) 
        {
            var statement = "Statement for " + customer.Name + "\n";
            var total = 0.0;
            foreach (var account in customer.Accounts) 
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
