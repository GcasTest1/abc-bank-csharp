using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        public readonly int transactionId;
        public readonly double amount;
        public readonly DateTime transactionDate;

        public Transaction(double amount) 
        {
            transactionId = IdGenerator.getInstance().GetNextId();
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().Now();
        }
    }
}
