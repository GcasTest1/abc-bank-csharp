using AbcBank;
using AbcBank.Data;
using NUnit.Framework;

namespace abc_bank_tests
{
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void Transaction()
        {
            var t = new TransactionModel(5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(TransactionModel));
        }
    }
}
