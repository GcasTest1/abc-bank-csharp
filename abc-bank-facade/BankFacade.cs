using AbcBank.Logic.BusinessLogic;
using AbcBank.Logic.Presentation;

namespace AbcBank.Facade
{
    public class BankFacade
    {
        public string CustomerSummary(BankService bank)
        {
            return new BankPresenter().ToString(bank.GetCustomerSummaries());
        }

    }
}
