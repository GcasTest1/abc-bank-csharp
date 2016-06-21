using System.Collections.Generic;

namespace AbcBank.Data
{
    public class BankModel
    {
        private readonly List<CustomerModel> _customers;

        public IReadOnlyList<CustomerModel> Customers
        {
            get { return this._customers; }
        }
        
        public BankModel()
        {
            _customers = new List<CustomerModel>();
        }

        public void AddCustomer(CustomerModel customerService)
        {
            this._customers.Add(customerService);
        }
    }
}