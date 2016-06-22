﻿using System.Collections.Generic;
using System.Linq;
using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic.Implementation
{
    public class BankService : IBankService
    {
        private readonly ICustomerService _customerService;

        public BankService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void AddCustomer(BankModel bank, CustomerModel customerService)
        {
            bank.AddCustomer(customerService);
        }

        public IList<CustomerSummaryModel> GetCustomerSummaries(BankModel bank)
        {
            return bank.Customers.Select(i => new CustomerSummaryModel(i.Name, _customerService.GetNumberOfAccounts(i))).ToList();
        }

        public double TotalInterestPaid(BankModel bank)
        {
            return bank.Customers.Sum(customer => _customerService.TotalInterestEarned(customer));
        }
    }
}
