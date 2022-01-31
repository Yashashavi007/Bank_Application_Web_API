using System.Collections.Generic;
using Technovert.Bankapp.Web.API.Models;

namespace Technovert.Bankapp.Web.API.Services
{
    public interface IEmployeeService
    {
        void AddCustomer(Customer account);
        void DeleteCustomer(string accNumber);
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(string accNumber);
        void UpdateCustomer(Customer oldCustomer, Customer newCustomer);
    }
}