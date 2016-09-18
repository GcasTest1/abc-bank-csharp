using System;
using System.Collections.Generic;
using System.Linq;
using abc_bank.Extentions;

namespace abc_bank.Models.Accounts
{
    public class CheckingAccount : Account
    {
        private const int CHECKING = 0;

        public CheckingAccount() : base(CHECKING)
        {}
        
        public override double AnnumInterestEarned() 
        {
            var amount = GetAccountBalance();
            return amount * 0.001;
        }

        public override string GetAccountName()
        {
            return "Checking Account";
        }
    }
}
