using abc_bank.Contract;
using System;

namespace abc_bank.Infrastructure
{
    public class AbcDateProvider : IDateProvider
    {
        public DateTime GetDateTimeNow()
        {
            return DateProvider.getInstance().Now();
        }
    }
}
