using AbcBank.Data;
using AbcBank.Logic.BusinessLogic.Implementation;

namespace AbcBank.Logic.BusinessLogic
{
    public interface ICustomerService
    {
        CustomerService OpenAccount(CustomerModel customer, AccountModel account);
        int GetNumberOfAccounts(CustomerModel customer);
        double TotalInterestEarned(CustomerModel customer);
    }
}