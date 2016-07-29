using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Contract;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            Transaction t = new Transaction(5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }

        [TestMethod]
        public void Transaction_DateProvider_Validate()
        {
            Bank bank = new Bank();

            //Use fake dateprovider to create withdraw transaction more than 5 days.
            //Any IOC container could be used here in the future.
            FakeDateProvider transDateProvider = new FakeDateProvider();

            Transaction transaction = new Transaction(300.0, transDateProvider);

            Assert.AreEqual(transaction.Date, new DateTime(2015, 7, 28));
        }

        //Inject to custom dateprovider to transction.
        public class FakeDateProvider : IDateProvider
        {
            public DateTime GetDateTimeNow()
            {
                return new DateTime(2015, 7, 28);
            }
        }
    }
}
