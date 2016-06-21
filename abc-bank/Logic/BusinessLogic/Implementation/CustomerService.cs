using System;
using System.Linq;
using AbcBank.Enums;
using AbcBank.Models;

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

        public void Transfer(CustomerModel customer, AccountType source, AccountType destination, double amount)
        {
            var sourceAccount = customer.Accounts.FirstOrDefault(i=>i.AccountType == source);
            var destAccount = customer.Accounts.FirstOrDefault(i=>i.AccountType == destination);

            if(sourceAccount == null)
                throw new ArgumentException("Costumer doesn't have a " + source + " account.");
            if (destAccount == null)
                throw new ArgumentException("Costumer doesn't have a " + destination + " account.");

            _accountService.Transfer(sourceAccount, destAccount, amount);
        }
    }
}
