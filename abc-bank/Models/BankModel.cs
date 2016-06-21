using System.Collections.Generic;

namespace AbcBank.Models
{
    public class BankModel
    {
        private readonly List<CustomerModel> _customers;

        public IEnumerable<CustomerModel> Customers
        {
            get { return _customers; }
        }
        
        public BankModel()
        {
            _customers = new List<CustomerModel>();
        }

        public void AddCustomer(CustomerModel customerService)
        {
            _customers.Add(customerService);
        }
    }
}