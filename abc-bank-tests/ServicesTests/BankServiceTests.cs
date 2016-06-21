using AbcBank;
using AbcBank.Enums;
using AbcBank.Logic.BusinessLogic;
using NUnit.Framework;

namespace abc_bank_tests.ServicesTests
{
    [TestFixture]
    public class BankServiceTests
    {

        //private static readonly double DoubleDelta = 1e-15;

        public class GetCustomerSummariesMethod
        {
            [Test]
            public void ReturnsListOfCurrentCustomers()
            {
                var bank = new BankService();
                var john = new Customer("John");
                john.OpenAccount(new Account(AccountType.Checking));
                bank.AddCustomer(john);

                var actual = bank.GetCustomerSummaries();
                Assert.AreEqual("John", actual[0].CustomerName);
                Assert.AreEqual(1, actual[0].NumberOfAccounts);
            }
        }


        //[TestMethod]
        //public void CheckingAccount() {
        //    var bank = new BankService();
        //    var checkingAccount = new Account(AccountType.Checking);
        //    var bill = new Customer("Bill").OpenAccount(checkingAccount);
        //    bank.AddCustomer(bill);

        //    checkingAccount.Deposit(100.0);

        //    Assert.AreEqual(0.1, bank.TotalInterestPaid(), DoubleDelta);
        //}

        //[TestMethod]
        //public void Savings_account() {
        //    var bank = new BankService();
        //    var checkingAccount = new Account(AccountType.Savings);
        //    bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

        //    checkingAccount.Deposit(1500.0);

        //    Assert.AreEqual(2.0, bank.TotalInterestPaid(), DoubleDelta);
        //}

        //[TestMethod]
        //public void Maxi_savings_account() {
        //    var bank = new BankService();
        //    var checkingAccount = new Account(AccountType.MaxiSavings);
        //    bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

        //    checkingAccount.Deposit(3000.0);

        //    Assert.AreEqual(170.0, bank.TotalInterestPaid(), DoubleDelta);
        //}
    }
}
