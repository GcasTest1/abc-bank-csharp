using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Models;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            var t = new Transaction(5);
            Assert.AreEqual(5, t.Amount);
        }
    }
}
