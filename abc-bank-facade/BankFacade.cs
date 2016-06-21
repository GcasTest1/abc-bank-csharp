using AbcBank.Data;
using AbcBank.Logic.BusinessLogic;
using AbcBank.Logic.Presentation;

namespace AbcBank.Facade
{
    public class BankFacade
    {
        private readonly BankPresenter _bankPresenter = new BankPresenter();
        private readonly BankService _bankService = new BankService();

        public string CustomerSummary(BankModel bank)
        {
            return _bankPresenter.ToString(_bankService.GetCustomerSummaries(bank));
        }
    }
}
