using System;
using System.Collections.Generic;
using AbcBank.Data;

namespace AbcBank.Logic.Presentation
{
    public class BankPresenter
    {
        public String ToString(IList<CustomerSummary> customerSummaries)
        {
            var summary = "Customer Summary";
            foreach (var c in customerSummaries)
                summary += "\n - " + c.CustomerName + " (" + Format(c.NumberOfAccounts, "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }
    }
}
