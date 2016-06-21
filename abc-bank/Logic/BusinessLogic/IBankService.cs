using System.Collections.Generic;
using AbcBank.Data;

namespace AbcBank.Logic.BusinessLogic
{
    public interface IBankService
    {
        void AddCustomer(BankModel bank, CustomerModel customerService);
        IList<CustomerSummary> GetCustomerSummaries(BankModel bank);
        double TotalInterestPaid(BankModel bank);
    }
}