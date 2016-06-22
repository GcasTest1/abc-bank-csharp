using System;
using System.Linq;
using AbcBank.Enums;
using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic.Implementation.IntererestCalculation
{
    public class InterestRateReducers
    {
        public static CalculationResult<AccountModel, double> RootBalanceReducer(CalculationResult<AccountModel, double> result)
        {
            return new CalculationResult<AccountModel, double>()
            {
                Input = result.Input,
                Result = result.Input.Transactions.Sum(t => t.Amount)
            };
        }

        public static CalculationResult<AccountModel, double> MaxiReducer(CalculationResult<AccountModel, double> input)
        {
            if (input.Input.Transactions.Any(i => i.Amount < 0 && i.Date >= DateTime.Now.AddDays(-10)))
                input.Result = input.Result*0.001;
            else
                input.Result = input.Result*0.05;
            return input;
        }

        public static CalculationResult<AccountModel, double> MaxiSavingRootReducer(CalculationResult<AccountModel, double> result)
        {
            if (result.Input.AccountType == AccountType.MaxiSavings)
                return result;

            result.StopProcessingChildren = true;
            return result;
        }

        public static CalculationResult<AccountModel, double> SavingBalanceOver1000Reducer(CalculationResult<AccountModel, double> input)
        {
            if (input.Result <= 1000)
                return input;
            input.Result = 1 + (input.Result - 1000)*0.002;
            return input;
        }

        public static CalculationResult<AccountModel, double> SavingBalanceUnder1000Reducer(CalculationResult<AccountModel, double> input)
        {
            if (input.Result > 1000)
                return input;
            input.Result = input.Result*0.001;
            return input;
        }

        public static CalculationResult<AccountModel, double> SavingRootReducer(CalculationResult<AccountModel, double> input)
        {
            if (input.Input.AccountType != AccountType.Savings)
                input.StopProcessingChildren = true;

            return input;
        }

        public static CalculationResult<AccountModel, double> CheckingReducer(CalculationResult<AccountModel, double> input)
        {
            if (input.Input.AccountType != AccountType.Checking)
            {
                input.StopProcessingChildren = true;
                return input;
            }

            input.Result = input.Result*0.001;
            return input;
        }
    }
}