using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
using AbcBank.Models;

namespace AbcBank.Logic.Presentation.Implementation
{
    public class StatementPresenter : IStatementPresenter
    {
        private readonly IAccountService _accountService;

        public StatementPresenter(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public string GetStatement(CustomerModel customer)
        {
            var gradTotal = _accountService.SumTransactions(customer.Accounts.SelectMany(i => i.Transactions));
            var sb = new StringBuilder();

            sb.Append("Statement for ").Append(customer.Name);
            sb.AppendLine();

            foreach (var account in customer.Accounts)
            {
                sb.AppendLine();
                sb.AppendLine(PrepyPrintAccountType(account));
                StatementForAccount(sb, account.Transactions);
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.Append("Total In All Accounts " + ToDollars(gradTotal));

            return sb.ToString();
        }

        private void StatementForAccount(StringBuilder sb, IEnumerable<TransactionModel> transactions)
        {
            var transactionModels = transactions as TransactionModel[] ?? transactions.ToArray();

            foreach (var t in transactionModels)
            {
                sb.AppendLine("  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount));
            }

            sb.Append("Total " + ToDollars(_accountService.SumTransactions(transactionModels)));
        }

        private static string PrepyPrintAccountType(AccountModel account)
        {
            switch (account.AccountType)
            {
                case AccountType.Checking:
                    return "Checking Account";
                case AccountType.Savings:
                    return "Savings Account";
                case AccountType.MaxiSavings:
                    return "Maxi Savings Account";
                default:
                    throw new ArgumentException("Unrecognized AccountType");
            }
        }

        private string ToDollars(double d)
        {
            return string.Format("{0:C}", Math.Abs(d));
        }
    }
}
