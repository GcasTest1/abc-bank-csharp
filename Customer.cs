using abc_bank.Accounts;
using abc_bank.EntityParams;
using abc_bank.Utilities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
    {/// <summary>
    ///  Customer would carry the  Generic Account type behavior .
    ///  When we create a Customer instance ,it  would initialize the Accounts collection as well.
    ///  Different account types would be added to this collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Customer<T> where T : IAccount
    {
        private List<T> _accounts=new List<T>();

        
       

        #region Properties
        public List<T> Accounts
        {
            get
            {
                return _accounts;
            }
            set
            {
                _accounts = value;
            }
        }

        public string CustomerName
        {

            get;set;
        }
        #endregion
        public Customer()
        {
           
            
        }


        #region Public Methods 
        /// <summary>
        /// This method would be used for opening new acounts for the customer.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Customer<T> OpenAccount(T account)
        {
            //This validation is introduced to avoid the duplicaytion of Account creation against 
            //the customer.
            if(!IsAccountExist(account))
               _accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return _accounts.Count;
        }

        public void TransferTheFund(AccountTransfer accountTransferParams) 
        {
            if (this.Accounts == null || this.Accounts.Count == 0|| Accounts.Count==1) return;
            this._accounts[0].TransferBetweenAccounts(accountTransferParams);
        }

        /// <summary>
        /// Sum of Interest  Earned for all accounts for the Customer.
        /// </summary>
        /// <returns></returns>
        public double TotalInterestEarned()
        {
            double total = 0;

            total= _accounts.Sum(act => act.InterestEarned());
            
            return total;
        }
        /// <summary>
        /// Method is used for getting the bank statement for the customer for all of the accounts.
        /// </summary>
        /// <returns></returns>
        public String GetStatement()
        {
            StringBuilder stringStatement = new StringBuilder();
            string statement = string.Empty;
            statement =String.Concat(Constants.StatementNote, CustomerName ,Constants.NewLine);
            stringStatement.Append(statement);
            double total = 0;
            _accounts.ForEach(act =>

            {
               // stringStatement.Append(Constants.SpaceBar).Append(act.StatementForAccount<object>()).Append(Constants.NewLine);
                stringStatement.Append(act.StatementForAccount<object>()).Append(Constants.NewLine);
            });
            total = _accounts.Sum(act => act.SumTransactions());
            stringStatement.Append(Constants.StatementNoteWithNln).Append(Utils.ToDollars(total));

            return stringStatement.ToString();
        }

        #endregion

        /// <summary>
        /// This function would be used for validating existance of Account against a Customer.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private bool IsAccountExist(T account)
        {
            bool isExist = false;

            _accounts.ForEach(act =>
            {

                if (act.GetType() == typeof(T)
                || act.GetAccountNumber()==account.GetAccountNumber())
                    isExist = true;
            }
                );

            return isExist;
        }
    }
}
