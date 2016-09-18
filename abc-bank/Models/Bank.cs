using System;
using System.Collections.Generic;
using System.Linq;
using abc_bank.Services;

namespace abc_bank.Models
{
    public class Bank
    {
        public List<Customer> Customers { get; }

        public Bank()
        {
            Customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public double TotalInterestPaid()
        {
            return Customers.Sum(c => c.TotalInterestEarned());
        }

        //TODO: not used remove
        //public string GetFirstCustomer()
        //{
        //    var customerName = customers.FirstOrDefault()?.GetName();
        //    if(string.IsNullOrEmpty(customerName))
        //    {
        //        Console.Write(customerName);
        //    }
        //    return customerName;
        //}
    }
}
