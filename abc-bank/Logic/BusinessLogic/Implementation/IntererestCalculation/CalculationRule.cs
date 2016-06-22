using System;
using System.Collections.Generic;

namespace AbcBank.Logic.BusinessLogic.Implementation.IntererestCalculation
{
    public class CalculationRule<TIn, TOut>
    {
        public IEnumerable<CalculationRule<TIn, TOut>> Children { get; set; }
        public Func<CalculationResult<TIn, TOut>, CalculationResult<TIn, TOut>> CalculateInternal { get; set; }

        public CalculationRule()
        {
            Children = new List<CalculationRule<TIn, TOut>>();
            CalculateInternal = result => result;
        }

        public CalculationResult<TIn, TOut> Calculate(CalculationResult<TIn, TOut> input)
        {
            var result = CalculateInternal(input);
            if (result.StopProcessingChildren)
            {
                result.StopProcessingChildren = false;
                return result;
            }

            foreach (var rule in Children)
                result = rule.Calculate(result);

            return result;
        }
    }
}