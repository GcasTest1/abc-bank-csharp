using AbcBank.Data;
using AbcBank.Logic.BusinessLogic;
using AbcBank.Logic.BusinessLogic.Implementation;
using AbcBank.Logic.Presentation;

namespace AbcBank.Facade
{
    public class BankFacade
    {
        private readonly BankPresenter _bankPresenter = new BankPresenter();
        private readonly StatementPresenter _statementPresenter= new StatementPresenter();

        private readonly BankService _bankService;

        public BankFacade()
        {
            var accountService = new AccountService();
            var customerService = new CustomerService(accountService);
            _bankService = new BankService(customerService);
        }

        public string CustomerSummary(BankModel bank)
        {
            return _bankPresenter.ToString(_bankService.GetCustomerSummaries(bank));
        }

        public string GetStatement(CustomerModel customer)
        {
            return _statementPresenter.GetStatement(customer);
        }
    }
}
