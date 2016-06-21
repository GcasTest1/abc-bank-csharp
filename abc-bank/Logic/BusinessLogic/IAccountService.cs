using System.Collections.Generic;
using AbcBank.Data;
using AbcBank.Logic.BusinessLogic.Implementation;

namespace AbcBank.Logic.BusinessLogic
{
    public interface IAccountService
    {
        AccountService Deposit(AccountModel account, double amount);
        AccountService Withdraw(AccountModel account, double amount);
        double InterestEarned(AccountModel account);
        double SumTransactions(IEnumerable<TransactionModel> transactions);
    }
}