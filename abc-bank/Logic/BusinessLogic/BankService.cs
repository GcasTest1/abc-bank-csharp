using System;
using System.Collections.Generic;
using System.Linq;
using AbcBank.Data;

namespace AbcBank.Logic.BusinessLogic
{
    public class BankService
    {
        private readonly List<Customer> _customers;

        public BankService()
        {
            _customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public IList<CustomerSummary> GetCustomerSummaries()
        {
            return _customers.Select(i => new CustomerSummary(i.GetName(), i.GetNumberOfAccounts())).ToList();
        }

        public double TotalInterestPaid()
        {
            double total = 0;
            foreach (var c in _customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public string GetFirstCustomer()
        {
            try
            {
                return _customers[0].GetName();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }


    }
}
