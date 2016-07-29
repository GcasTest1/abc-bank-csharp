using System;
using abc_bank.Contract;

namespace abc_bank.Accounts
{
    public class MaxiSavingsAccount : Account
    {
        private static readonly int DAYSLIMITFORFIVE = 10;

        public MaxiSavingsAccount() : base(AccountType.Maxi_Savings) { }

        public MaxiSavingsAccount(IDateProvider dateProviders) : base(AccountType.Maxi_Savings, dateProviders) { }

        public override double CalculateInterest(double amount)
        {
            var lastTransInLimitedDays = this.transactions.Find(findLastTransactionInDayLimit);

            //Check last transaction date for the new rule of 5% interest.
            if (lastTransInLimitedDays == null)
                return amount * 0.05;

            return amount * 0.001;

        }

        private static bool findLastTransactionInDayLimit(Transaction tran)
        {
            TimeSpan ts = DateTime.Now - tran.Date;

            if (tran.amount < 0 && ts.TotalDays <= DAYSLIMITFORFIVE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
