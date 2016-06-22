using System;
using System.Collections.Generic;
using AbcBank.Logic.Presentation;
using AbcBank.Logic.Presentation.Implementation;
using AbcBank.Models;
using NUnit.Framework;

namespace abc_bank_tests.PresentationTests
{
    [TestFixture]
    public class BankPresenterTests
    {

        [TestFixture]
        public class ToStringMethod
        {
            [Test]
            public void ReturnsEachCustomerOnANewLine()
            {
                var customerSummaries = new List<CustomerSummaryModel>
                {
                    new CustomerSummaryModel("John", 1),
                    new CustomerSummaryModel("Juan", 1)
                };

                var actual = new BankPresenter().ToString(customerSummaries);
                Assert.AreEqual("Customer Summary"+ Environment.NewLine + " - John (1 account)" + Environment.NewLine + " - Juan (1 account)", actual);
            }
        }
    }
}