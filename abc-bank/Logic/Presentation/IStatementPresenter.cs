using AbcBank.Models;

namespace AbcBank.Logic.Presentation
{
    public interface IStatementPresenter
    {
        string GetStatement(CustomerModel customer);
    }
}