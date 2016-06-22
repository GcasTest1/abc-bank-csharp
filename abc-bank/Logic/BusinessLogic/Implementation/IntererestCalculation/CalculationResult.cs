namespace AbcBank.Logic.BusinessLogic.Implementation.IntererestCalculation
{
    public class CalculationResult<TIn, TOut>
    {
        public TIn Input { get; set; }
        public TOut Result { get; set; }
        public bool StopProcessingChildren { get; set; }
    }
}