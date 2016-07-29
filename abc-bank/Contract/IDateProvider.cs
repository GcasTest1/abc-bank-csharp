using System;

namespace abc_bank.Contract
{
    public interface IDateProvider
    {
        DateTime GetDateTimeNow();
    }
}
