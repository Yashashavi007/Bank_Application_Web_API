using System.Collections.Generic;
using Technovert.Bankapp.Web.API.Enums;
using Technovert.Bankapp.Web.API.Models;

namespace Technovert.Bankapp.Web.API.Services
{
    public interface IAccountService
    {
        void AddCustomer(Customer customer);
        void AddEmployee(Employee employee);
        Customer CreateCustomerAccount(string IFSCCode, string name, CustomerEnum.CustomerGender gender, float balance);
        Employee CreateEmployeeAccount(string IFSCCode, string name, EmployeeEnum.EmployeeGender gender, EmployeeEnum.EmployeeRole role);
        float GetBalance(string accountNumber);
        float getInterBankIMPS();
        float getInterBankRTGS();
        float getIntraBankIMPS();
        float getIntraBankRTGS();
        void UpdateTransactionHistory(string fromId, string toId, TransactionEnum.TransactionType type, float amount);
        IEnumerable<Transaction> ViewPassbook(string accountNumber);
    }
}