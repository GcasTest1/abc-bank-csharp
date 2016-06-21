using System.Collections.Generic;

namespace AbcBank.Data
{
    public class BankModel
    {
        private readonly List<Customer> _customers;

        public IReadOnlyList<Customer> Customers
        {
            get { return this._customers; }
        }
        
        public BankModel()
        {
            _customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            this._customers.Add(customer);
        }
    }
}