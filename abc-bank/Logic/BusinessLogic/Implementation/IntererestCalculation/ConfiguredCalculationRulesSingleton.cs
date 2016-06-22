using System.Collections.Generic;
using AbcBank.Models;

namespace AbcBank.Logic.BusinessLogic.Implementation.IntererestCalculation
{
    public static class ConfiguredCalculationRulesSingleton
    {
        private static CalculationRule<AccountModel, double> _bar;

        public static CalculationRule<AccountModel, double> Bar
        {
            get
            {
                return _bar ?? (_bar = CalculationRule()); 
            }
        }

        private static CalculationRule<AccountModel, double> CalculationRule()
        {
            var checkingRule = new CalculationRule<AccountModel, double>
            {
                CalculateInternal = input => InterestRateReducers.CheckingReducer(input)
            };

            var savingRule = new CalculationRule<AccountModel, double>
            {
                CalculateInternal = input => InterestRateReducers.SavingRootReducer(input),
                Children = new List<CalculationRule<AccountModel, double>>
                {
                    new CalculationRule<AccountModel, double>
                    {
                        CalculateInternal = input => InterestRateReducers.SavingBalanceUnder1000Reducer(input)
                    },
                    new CalculationRule<AccountModel, double>
                    {
                        CalculateInternal = input => InterestRateReducers.SavingBalanceOver1000Reducer(input)
                    }
                },
            };

            var maxiSavingsRule = new CalculationRule<AccountModel, double>
            {
                CalculateInternal = result => InterestRateReducers.MaxiSavingRootReducer(result),
                Children = new List<CalculationRule<AccountModel, double>>
                {
                    new CalculationRule<AccountModel, double>
                    {
                        CalculateInternal = input => InterestRateReducers.MaxiReducer(input)
                    },
                },
            };

            var rootRule = new CalculationRule<AccountModel, double>()
            {
                CalculateInternal = result => InterestRateReducers.RootBalanceReducer(result),
                Children =
                    new List<CalculationRule<AccountModel, double>> {checkingRule, savingRule, maxiSavingsRule}
            };
            return rootRule;
        }
    }
}