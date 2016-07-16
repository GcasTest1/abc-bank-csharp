using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public enum AccountType
    {
        CHECKING,
        SAVINGS,
        MAXI_SAVINGS
    }

    public abstract class Account
    {        
        public const int DAILY_ACCRUAL_RATE = 365;
        public const int WITHDRAWAL_DAYS_GAP = 10;
        private readonly int m_accountId = 0;
        private readonly AccountType m_accountType;
        private Dictionary<int, Transaction> m_transactions;
        private int m_lastWithdrawalTransactionId = int.MinValue;

        public AccountType GetAccountType
        {
           get { return m_accountType;  }
        }

        public IEnumerable<Transaction> Transactions
        {
            get{ return m_transactions.Values; }
        }

        public int AccountId
        {
            get { return m_accountId; }
        }

        public Account(AccountType accountType) 
        {
            m_accountId = IdGenerator.getInstance().GetNextId();
            this.m_accountType = accountType;
            this.m_transactions = new Dictionary<int, Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transaction deposit = new Transaction(amount);
                m_transactions.Add(deposit.transactionId, deposit);
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {

                if (HasSufficientFunds(amount))
                {
                    Transaction withdrawal = new Transaction(-amount);
                    m_transactions.Add(withdrawal.transactionId, withdrawal);
                    m_lastWithdrawalTransactionId = withdrawal.transactionId;
                }
                else
                {
                    throw new ApplicationException(string.Format("insufficient funds in account with Id: {0}", AccountId));
                }
            }
        }

        public bool HasSufficientFunds(double amountToCheck)
        {
            return sumTransactions() >= amountToCheck;
        }

        public abstract double InterestEarned();
        
        public Transaction GetLastWithdrawal()
        {
            if (m_lastWithdrawalTransactionId == int.MinValue)
                return null;
            return m_transactions[m_lastWithdrawalTransactionId];
        }
        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll) 
        {
            double amount = 0.0;

            if (checkAll)
            {
                foreach (Transaction t in Transactions)
                    amount += t.amount;
            }
            return amount;
        }      

    }

    public class CheckingAccount : Account
    {
        public CheckingAccount()
            :base(AccountType.CHECKING)
        {
        }

        /// <summary>
        ///  New feature : 1. Interest earned has been changed to accrue daily for the requirement:
        ///    "Interest rates should accrue daily (incl. weekends), rates above are per-annum"
        /// </summary>
        /// <returns></returns>
        public override double InterestEarned()
        {
            double amount = sumTransactions();
            if (amount <= 0) return 0.0;

            return amount * (0.001 / DAILY_ACCRUAL_RATE);
        }

        public override string ToString()
        {
            return "Checking Account";
        }
    }

    public class SavingsAccount : Account
    {
        public SavingsAccount()
            : base(AccountType.SAVINGS)
        {

        }

        /// <summary>
        ///  New feature : 1. Interest earned has been changed to accrue daily for the requirement:
        ///    "Interest rates should accrue daily (incl. weekends), rates above are per-annum"
        /// </summary>
        /// <returns></returns>
        public override double InterestEarned()
        {
            double amount = sumTransactions();
            if (amount <= 0) return 0.0;

            if (amount <= 1000)
                return amount * (0.001 / DAILY_ACCRUAL_RATE);
            else
                return ((double)1 / DAILY_ACCRUAL_RATE) + (amount - 1000) * (0.002 / DAILY_ACCRUAL_RATE);
           
        }

        public override string ToString()
        {
            return "Savings Account";
        }
    }

    public class MaxiSavingsAccount : Account
    {

        public MaxiSavingsAccount()
            : base(AccountType.MAXI_SAVINGS)
        {
        }

        /// <summary>
        /// 1. Interest earned has been changed to accrue daily for the requirement:
        ///    "Interest rates should accrue daily (incl. weekends), rates above are per-annum"
        /// 2. Maxi-savings interest has been changed for the requirement:
        ///    "Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%"
        /// </summary>
        /// <returns></returns>
        public override double InterestEarned()
        {
            double amount = sumTransactions();
            if (amount <= 0) return 0.0;

            if(GetLastWithdrawal() == null || 
              (GetLastWithdrawal().transactionDate - DateProvider.getInstance().Now()).TotalDays > WITHDRAWAL_DAYS_GAP )
            {
                return amount * (0.05 / DAILY_ACCRUAL_RATE);
            }
            return amount * (0.001 / DAILY_ACCRUAL_RATE);

            /*  if (amount <= 1000)
                  return amount * 0.02/accrualRate;
              if (amount <= 2000)
                  return 20 + (amount - 1000) * 0.05/accrualRate;
              return 70 + (amount - 2000) * 0.1/accrualRate;*/
        }

        public override string ToString()
        {
            return "Maxi Savings Account";
        }
    }
}
