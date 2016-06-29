using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abc_bank
{
  /// <summary>
  /// Representation for Bank Entity
  /// </summary>
  public class Bank
  {
    private List<Customer> Customers { get; set; }

    public Bank()
    {
        this.Customers = new List<Customer>();
    }

    public void AddCustomer(Customer customer)
    {
        Customers.Add(customer);
    }

    public String CustomerSummary() {
      StringBuilder sb = new StringBuilder();
      sb.Append("Customer Summary");

      foreach (Customer customer in Customers)
      {
        int numberOfAccounts = customer.GetNumberOfAccounts();
        sb.Append(String.Format("\n - {0} ({1} {2})", customer.Name, numberOfAccounts, numberOfAccounts == 1? "account" : "accounts" ));
      }

      return sb.ToString();
    }

    public double totalInterestPaid() {
        double total = 0;
        foreach(Customer c in Customers)
            total += c.TotalInterestEarned();
        return total;
    }

    public Customer GetFirstCustomer()
    {
      if (Customers == null || Customers.Count == 0)
        return null;

      return Customers.ElementAt(0);
    }
  }
}
