using System;
using System.Collections.Generic;
using abc_bank.Extentions;

namespace abc_bank.Models.Accounts
{
    public class SavingAccount : Account
    {
        private const int SAVINGS = 1;

        public SavingAccount() : base(SAVINGS)
        { }

        public override double AnnumInterestEarned()
        {
            var amount = GetAccountBalance();
            if (amount <= 1000)
                return amount*0.001;
            
            return 1 + (amount - 1000)*0.002;
        }

        public override string GetAccountName()
        {
            return "Savings Account";
        }
    }
}
