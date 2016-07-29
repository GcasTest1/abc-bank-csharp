namespace abc_bank.Accounts
{
    public class CheckingAccount : Account
    {
        public CheckingAccount() : base(AccountType.Checking) { }

        public override double CalculateInterest(double amount)
        {
            return amount * 0.001;
        }


    }
}
