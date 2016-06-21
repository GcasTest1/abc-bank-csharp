using System.Linq;
using AbcBank.Data;

namespace AbcBank.Logic.BusinessLogic.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IAccountService _accountService;

        public CustomerService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public CustomerService OpenAccount(CustomerModel customer, AccountModel account)
        {
            customer.AddAccount(account);
            return this;
        }

        public int GetNumberOfAccounts(CustomerModel customer)
        {
            return customer.Accounts.Count;
        }

        public double TotalInterestEarned(CustomerModel customer)
        {
            return customer.Accounts.Sum(a => _accountService.InterestEarned(a));
        }
    }
}
