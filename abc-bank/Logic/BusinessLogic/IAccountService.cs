using System.Collections.Generic;
using AbcBank.Logic.BusinessLogic.Implementation;
using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic
{
    public interface IAccountService
    {
        AccountService Deposit(AccountModel account, double amount);
        AccountService Withdraw(AccountModel account, double amount);
        double InterestEarned(AccountModel account);
        double SumTransactions(IEnumerable<TransactionModel> transactions);
        AccountService Transfer(AccountModel sourceAccount, AccountModel destAccount, double amount);
    }
}