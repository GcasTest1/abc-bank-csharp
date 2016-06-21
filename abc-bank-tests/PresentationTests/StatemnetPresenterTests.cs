using System;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic.Implementation;
using AbcBank.Logic.Presentation;
using AbcBank.Logic.Presentation.Implementation;
using AbcBank.Models;
using NUnit.Framework;

namespace abc_bank_tests.PresentationTests
{
    [TestFixture]
    public class StatemnetPresenterTests
    {
        [TestFixture]
        public class GetStatementMethod
        {
            [Test]
            public void GetDetailedStatementAsString()
            {
                var checkingAccount = new AccountModel(AccountType.Checking);
                var savingsAccount = new AccountModel(AccountType.Savings);

                var henry = new CustomerModel("Henry");
                henry.AddAccount(checkingAccount);
                henry.AddAccount(savingsAccount);
                checkingAccount.AddTransaction(new TransactionModel(100, DateTime.Now));
                savingsAccount.AddTransaction(new TransactionModel(4000.0, DateTime.Now));
                savingsAccount.AddTransaction(new TransactionModel(-200, DateTime.Now));

                var actual = new StatementPresenter(new AccountService(null)).GetStatement(henry);
                string expected = "Statement for Henry" + Environment.NewLine +
                                  Environment.NewLine +
                                  "Checking Account" + Environment.NewLine +
                                  "  deposit $100.00" + Environment.NewLine +
                                  "Total $100.00" + Environment.NewLine +
                                  Environment.NewLine +
                                  "Savings Account" + Environment.NewLine +
                                  "  deposit $4,000.00" + Environment.NewLine +
                                  "  withdrawal $200.00" + Environment.NewLine +
                                  "Total $3,800.00" + Environment.NewLine +
                                  Environment.NewLine +
                                  "Total In All Accounts $3,900.00";
                Assert.AreEqual(expected, actual);
            }

        }
    }
}