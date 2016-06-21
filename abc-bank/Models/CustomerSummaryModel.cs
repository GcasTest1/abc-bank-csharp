namespace AbcBank.Models
{
    public class CustomerSummaryModel
    {
        public string CustomerName { get; }
        public int NumberOfAccounts { get; }

        public CustomerSummaryModel(string customerName, int numberOfAccounts)
        {
            CustomerName = customerName;
            NumberOfAccounts = numberOfAccounts;
        }
    }
}