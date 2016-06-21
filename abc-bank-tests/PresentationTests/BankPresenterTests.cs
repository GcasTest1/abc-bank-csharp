using AbcBank;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
using AbcBank.Logic.Presentation;
using NUnit.Framework;

namespace abc_bank_tests.PresentationTests
{
    [TestFixture]
    public class BankPresenterTests
    {

        [TestFixture]
        public class ToStringMethodTests
        {
            [Test]
            public void ReturnsEachCustomerOnANewLine()
            {
                var bank = new BankService();
                var john = new Customer("John");
                john.OpenAccount(new Account(AccountType.Checking));
                bank.AddCustomer(john);

                var juan = new Customer("Juan");
                juan.OpenAccount(new Account(AccountType.Checking));
                bank.AddCustomer(juan);

                var actual = new BankPresenter().ToString(bank.GetCustomerSummaries());
                Assert.AreEqual("Customer Summary\n - John (1 account)\n - Juan (1 account)", actual);
            }
        }
    }
}