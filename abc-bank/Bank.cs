using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Bank
    {
        private readonly List<Customer> _customers;

        public Bank()
        {
            _customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public String CustomerSummary() {
            var summary = "Customer Summary";
            foreach (var c in _customers)
                summary += "\n - " + c.GetName() + " (" + Format(c.GetNumberOfAccounts(), "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double TotalInterestPaid() {
            double total = 0;
            foreach(var c in _customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public String GetFirstCustomer()
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
