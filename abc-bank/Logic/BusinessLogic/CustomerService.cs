using AbcBank.Data;

namespace AbcBank.Logic.BusinessLogic
{
    public class CustomerService
    {
        private readonly AccountService _accountService = new AccountService();

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
            double total = 0;
            foreach (var a in customer.Accounts)
            {
                total += _accountService.InterestEarned(a);
            }
            return total;
        }
    }
}
