using System.Collections.Generic;
using System.Linq;
using AbcBank.Data;

namespace AbcBank.Logic.BusinessLogic
{
    public class BankService
    {
        public void AddCustomer(BankModel bank, Customer customer)
        {
            bank.AddCustomer(customer);
        }

        public IList<CustomerSummary> GetCustomerSummaries(BankModel bank)
        {
            return bank.Customers.Select(i => new CustomerSummary(i.GetName(), i.GetNumberOfAccounts())).ToList();
        }

        public double TotalInterestPaid(BankModel bank)
        {
            return bank.Customers.Sum(c => c.TotalInterestEarned());
        }
    }
}
