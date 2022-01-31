namespace Technovert.Bankapp.Web.API.Services
{
    public interface ICustomService
    {
        float InterBankIMPS { get; set; }
        float InterBankRTGS { get; set; }
        float IntraBankIMPS { get; set; }
        float IntraBankRTGS { get; set; }

        string GenerateAccountID(string accountHolderName);
        string GenerateAccountNumber();
        string GenerateEmployeeID(string employeeName);
        int GeneratePin();
        string GenerateTransactionID(string bankIFSCCode, string accountId);
        bool ValidateAccount(string accNo);
        bool ValidateBank(string ifscCode);
        bool ValidatePin(string accNumber, int pin);
    }
}