namespace abc_bank.Accounts
{
    public class SavingsAccount : Account
    {
        public SavingsAccount() : base(AccountType.Savings) { }

        public override double CalculateInterest(double amount)
        {
            if (amount <= 1000)
                return amount * 0.001;
            else
                return 1 + (amount - 1000) * 0.002;
        }


    }
}
