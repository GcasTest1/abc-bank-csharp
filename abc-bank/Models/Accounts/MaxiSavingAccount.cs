using System;
using System.Collections.Generic;
using abc_bank.Extentions;
using abc_bank.Services;

namespace abc_bank.Models.Accounts
{
    public class MaxiSavingAccount : Account
    {
        private const int MAXI_SAVINGS = 2;

        public MaxiSavingAccount() : base(MAXI_SAVINGS)
        { }

        public override double AnnumInterestEarned()
        {
            var amount = GetAccountBalance();
            if ((DateProvider.getInstance().Now() - GetLastWithdrawDate()).Days < 10)
                return amount*0.001;

            return amount * 0.05;
        }

        public override string GetAccountName()
        {
            return "Maxi Savings Account";
        }
    }
}
