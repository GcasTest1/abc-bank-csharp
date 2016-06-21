using AbcBank.Logic.BusinessLogic;
using AbcBank.Logic.BusinessLogic.Implementation;
using AbcBank.Logic.Presentation;
using AbcBank.Logic.Presentation.Implementation;
using AbcBank.Models;

namespace AbcBank.Facade
{
    public class BankFacade
    {
        private readonly IAccountService _accountService;
        private readonly IBankPresenter _bankPresenter;
        private readonly IStatementPresenter _statementPresenter;

        private readonly BankService _bankService;

        public BankFacade()
        {
            // The should come from a dependency injection container
            var customerService = new CustomerService(_accountService);

            _accountService = new AccountService(new TransactionModelFactory(DateProvider.GetInstance()));
            _statementPresenter = new StatementPresenter(_accountService);
            _bankService = new BankService(customerService);
            _bankPresenter = new BankPresenter();
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
