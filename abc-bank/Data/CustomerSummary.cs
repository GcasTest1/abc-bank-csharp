namespace AbcBank.Data
{
    public class CustomerSummary
    {
        public string CustomerName { get; }
        public int NumberOfAccounts { get; }

        public CustomerSummary(string customerName, int numberOfAccounts)
        {
            CustomerName = customerName;
            NumberOfAccounts = numberOfAccounts;
        }
    }
}