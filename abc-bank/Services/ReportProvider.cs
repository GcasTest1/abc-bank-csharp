using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Models;
using abc_bank.Models.Accounts;

namespace abc_bank.Services
{
    public class ReportProvider
    {
        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        public string Format(int number, string word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }
        
        public string ToDollars(double d)
        {
            return $"{Math.Abs(d):C2}";
        }

        public String GetStatement(Customer c)
        {
            String statement = null;
            statement = "Statement for " + c.GetName() + "\n";
            double total = 0.0;
            foreach (Account a in c.GetAccounts())
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.GetAccountBalance();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        public String StatementForAccount(Account a)
        {
            var s = "";

            //Translate to pretty account type
            s += a.GetAccountName() + "\n";

            //Now total up all the transactions
            double total = 0.0;
            foreach (var t in a.Transactions)
            {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        public string CustomerSummary(IEnumerable<Customer> customers)
        {
            string summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.GetName() + " ("
                    + Format(c.GetNumberOfAccounts(), "account") + ")";
            return summary;
        }
    }
}
