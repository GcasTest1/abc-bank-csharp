using AbcBank;
using NUnit.Framework;

namespace abc_bank_tests
{
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void Transaction()
        {
            var t = new Transaction(5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }
    }
}
