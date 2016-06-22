using System.Collections.Generic;
using AbcBank.Models;

namespace AbcBank.Logic.Presentation
{
    public interface IBankPresenter
    {
        string ToString(IEnumerable<CustomerSummaryModel> customerSummaries);
    }
}