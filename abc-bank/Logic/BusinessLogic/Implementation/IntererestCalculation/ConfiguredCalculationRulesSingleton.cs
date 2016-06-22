using System.Collections.Generic;
using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic.Implementation.IntererestCalculation
{
    public static class ConfiguredCalculationRulesSingleton
    {
        private static CalculationRule<AccountModel, double> _yearlyInterestRateRules;
        private static CalculationRule<AccountModel, double> _dailyInterestRateRules;

        public static CalculationRule<AccountModel, double> YearlyInterestRateRules
        {
            get
            {
                return _yearlyInterestRateRules ?? (_yearlyInterestRateRules = CreateYealyInterestRateRules()); 
            }
        }

        public static CalculationRule<AccountModel, double> DailyInterestRateRules
        {
            get
            {
                return _dailyInterestRateRules ?? (_dailyInterestRateRules = CreateDailyYealyInterestRateRules());
            }
        }

        private static CalculationRule<AccountModel, double> CreateYealyInterestRateRules()
        {
            var checkingRule = new CalculationRule<AccountModel, double>
            {
                CalculateInternal = input => YearlyInterestRateReducers.CheckingReducer(input)
            };

            var savingRule = new CalculationRule<AccountModel, double>
            {
                CalculateInternal = input => YearlyInterestRateReducers.SavingRootReducer(input),
                Children = new List<CalculationRule<AccountModel, double>>
                {
                    new CalculationRule<AccountModel, double>
                    {
                        CalculateInternal = input => YearlyInterestRateReducers.SavingBalanceUnder1000Reducer(input)
                    },
                    new CalculationRule<AccountModel, double>
                    {
                        CalculateInternal = input => YearlyInterestRateReducers.SavingBalanceOver1000Reducer(input)
                    }
                },
            };

            var maxiSavingsRule = new CalculationRule<AccountModel, double>
            {
                CalculateInternal = result => YearlyInterestRateReducers.MaxiSavingRootReducer(result),
                Children = new List<CalculationRule<AccountModel, double>>
                {
                    new CalculationRule<AccountModel, double>
                    {
                        CalculateInternal = input => YearlyInterestRateReducers.MaxiReducer(input)
                    },
                },
            };

            var rootRule = new CalculationRule<AccountModel, double>()
            {
                CalculateInternal = result => YearlyInterestRateReducers.RootReducer(result),
                Children =
                    new List<CalculationRule<AccountModel, double>> {checkingRule, savingRule, maxiSavingsRule}
            };
            return rootRule;
        }

        private static CalculationRule<AccountModel, double> CreateDailyYealyInterestRateRules()
        {
            var checkingRule = new CalculationRule<AccountModel, double>
            {
                CalculateInternal = input => DailyInterestRateReducers.CheckingReducer(input)
            };

            var savingRule = new CalculationRule<AccountModel, double>
            {
                CalculateInternal = input => DailyInterestRateReducers.SavingRootReducer(input),
                Children = new List<CalculationRule<AccountModel, double>>
                {
                    new CalculationRule<AccountModel, double>
                    {
                        CalculateInternal = input => DailyInterestRateReducers.SavingBalanceUnder1000Reducer(input)
                    },
                    new CalculationRule<AccountModel, double>
                    {
                        CalculateInternal = input => DailyInterestRateReducers.SavingBalanceOver1000Reducer(input)
                    }
                },
            };

            var maxiSavingsRule = new CalculationRule<AccountModel, double>
            {
                CalculateInternal = result => DailyInterestRateReducers.MaxiSavingRootReducer(result),
                Children = new List<CalculationRule<AccountModel, double>>
                {
                    new CalculationRule<AccountModel, double>
                    {
                        CalculateInternal = input => DailyInterestRateReducers.MaxiReducer(input)
                    },
                },
            };

            var rootRule = new CalculationRule<AccountModel, double>()
            {
                CalculateInternal = result => DailyInterestRateReducers.RootReducer(result),
                Children =
                    new List<CalculationRule<AccountModel, double>> { checkingRule, savingRule, maxiSavingsRule }
            };
            return rootRule;
        }
    }
}