using abc_bank;
using abc_bank.Accounts;
using abc_bank.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace abc_bank
{
    public class Bank
    {
        
        private List<Customer<IAccount>> _customers;

        #region Constructor
        public Bank()
        {
           
            _customers = new List<Customer<IAccount>>();
        }

        #endregion

        #region Public Methods
        public void AddCustomer(Customer<IAccount> customer)
        {
            _customers.Add(customer);
        }
        /// <summary>
        /// Gte the summary details of all informations about the Customers.
        /// </summary>
        /// <returns></returns>
        public String CustomerSummary() {
            String summary = Constants.CustSummmary;
            StringBuilder custSummaryBuilder = new StringBuilder();
            custSummaryBuilder.Append(summary);
            _customers.ForEach(cust =>
            {
                custSummaryBuilder.Append(Constants.NextLineBreak).Append(cust.CustomerName).Append(Constants.LeftParans)
                .Append(format(cust.GetNumberOfAccounts(), Constants.Account)).Append(Constants.RightParans);
               
            });

           
            return custSummaryBuilder.ToString();
        }

      

        public double TotalInterestPaid() {
            double total = 0;
            total=_customers.Sum(cust => cust.TotalInterestEarned());
            return total;
        }

        public String GetFirstCustomer()
        {
            try
            {
                _customers = null;
                return _customers.First().CustomerName;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }

        #endregion

        #region Private Mathods
        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        #endregion
    }
}
