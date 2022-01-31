namespace Technovert.Bankapp.Web.API.Services
{
    public interface ICustomerService
    {
        void Deposit(string accountNumber, float amount);
        void Deposit(string accountNumber, string currency, float amount);
        void Transfer(string fromAccountNumber, string toAccountNumber, float amount);
        void Withdraw(string accountNumber, float amount);
    }
}