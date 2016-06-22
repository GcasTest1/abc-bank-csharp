using System;
using System.Linq;
using AbcBank.Enums;
using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic.Implementation.IntererestCalculation
{
    public class DailyInterestRateReducers
    {
        private static int _periods = 365;

        public static CalculationResult<AccountModel, double> RootReducer(CalculationResult<AccountModel, double> input)
        {
            return input;
        }

        public static CalculationResult<AccountModel, double> MaxiReducer(CalculationResult<AccountModel, double> input)
        {
            if (input.Input.Transactions.Any(i => i.Amount < 0 && i.Date >= DateTime.Now.AddDays(-10)))
                input.Result = input.Result*0.001;
            else
                input.Result = input.Result*0.05;

            input.Result = input.Result/365;
            return input;
        }

        public static CalculationResult<AccountModel, double> MaxiSavingRootReducer(CalculationResult<AccountModel, double> input)
        {
            if (input.Input.AccountType == AccountType.MaxiSavings)
                return input;

            input.StopProcessingChildren = true;
            return input;
        }

        public static CalculationResult<AccountModel, double> SavingBalanceOver1000Reducer(CalculationResult<AccountModel, double> input)
        {
            if (input.Result <= 1000)
                return input;
            input.Result = 1 + (input.Result - 1000)*0.002/365;
            return input;
        }

        public static CalculationResult<AccountModel, double> SavingBalanceUnder1000Reducer(CalculationResult<AccountModel, double> input)
        {
            if (input.Result > 1000)
                return input;
            input.Result = input.Result*0.001/365;
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

            input.Result = input.Result*0.001/365;
            return input;
        }
    }
}