using System;
using System.Collections.Generic;
using System.Text;
using AbcBank.Data;

namespace AbcBank.Logic.Presentation
{
    public class BankPresenter
    {
        public string ToString(IEnumerable<CustomerSummary> customerSummaries)
        {
            var sb = new StringBuilder();
            sb.Append("Customer Summary");
            foreach (var c in customerSummaries)
                sb.AppendLine().Append(" - ").Append(c.CustomerName).Append(" (").AppendFormat(Format(c.NumberOfAccounts, "account")).Append(")");
            return sb.ToString();
        }

        private string Format(int number, string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            return number + " " + (number == 1 ? word : word + "s");
        }
    }
}
