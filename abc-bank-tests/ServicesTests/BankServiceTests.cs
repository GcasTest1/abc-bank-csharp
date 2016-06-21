using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
using AbcBank.Logic.BusinessLogic.Implementation;
using AbcBank.Models;
using Moq;
using NUnit.Framework;

namespace abc_bank_tests.ServicesTests
{
    [TestFixture]
    public class BankServiceTests
    {

        public class GetCustomerSummariesMethod
        {
            [Test]
            public void CallsTheCustomerService_ToGetTheNumberOfAccounts()
            {
                var bank = new BankModel();
                var john = new CustomerModel("John");
                john.AddAccount(new AccountModel(AccountType.Checking));
                bank.AddCustomer(john);

                var customerServiceMock = new Mock<ICustomerService>();
                customerServiceMock.Setup(i => i.GetNumberOfAccounts(john));

                new BankService(customerServiceMock.Object).GetCustomerSummaries(bank);
                
                customerServiceMock.VerifyAll();
            }
        }
    }
}
