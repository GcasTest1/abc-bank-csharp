using System.Collections.Generic;
using System.Linq;
using AbcBank.Data;

namespace AbcBank.Logic.BusinessLogic.Implementation
{
    public class BankService : IBankService
    {
        private readonly ICustomerService _customerService;

        public BankService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void AddCustomer(BankModel bank, CustomerModel customerService)
        {
            bank.AddCustomer(customerService);
        }

        public IList<CustomerSummary> GetCustomerSummaries(BankModel bank)
        {
            return bank.Customers.Select(i => new CustomerSummary(i.Name, _customerService.GetNumberOfAccounts(i))).ToList();
        }

        public double TotalInterestPaid(BankModel bank)
        {
            return bank.Customers.Sum(customer => _customerService.TotalInterestEarned(customer));
        }
    }
}
