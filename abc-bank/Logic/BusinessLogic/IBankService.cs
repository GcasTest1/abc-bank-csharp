using System.Collections.Generic;
using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic
{
    public interface IBankService
    {
        void AddCustomer(BankModel bank, CustomerModel customerService);
        IList<CustomerSummaryModel> GetCustomerSummaries(BankModel bank);
        double TotalInterestPaid(BankModel bank);
    }
}