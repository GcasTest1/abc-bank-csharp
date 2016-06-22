using AbcBank.Logic.BusinessLogic.Implementation;
using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic
{
    public interface ICustomerService
    {
        CustomerService OpenAccount(CustomerModel customer, AccountModel account);
        int GetNumberOfAccounts(CustomerModel customer);
        double TotalInterestEarned(CustomerModel customer);
    }
}