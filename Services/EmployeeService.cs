using System;
using System.Collections.Generic;
using System.Linq;
using Technovert.Bankapp.Web.API.Exceptions;
using Technovert.Bankapp.Web.API.Models;
using static Technovert.Bankapp.Web.API.Enums.CustomerEnum;
using static Technovert.Bankapp.Web.API.Enums.EmployeeEnum;
using static Technovert.Bankapp.Web.API.Enums.TransactionEnum;

namespace Technovert.Bankapp.Web.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly BankDbContext _context;
        private readonly IAccountService _accountService;


        public EmployeeService(BankDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }


        //  Services

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.Where(c => c.Status == AccountStatus.Open).ToList();
        }

        public Customer GetCustomer(string accNumber)
        {
            var customer =  _context.Customers.FirstOrDefault(c => c.AccountNumber == accNumber);
            if(customer == null)
            {
                throw new CustomerNotFoundException("Customer does not exist!!!");
            }
            else
            {
                if(customer.Status == AccountStatus.Close)
                {
                    throw new AccountClosedException($"Account {accNumber} has already been closed!!!");
                }
                else
                {
                    return customer;
                }
            }
        }

        public void AddCustomer(Customer account)
        {
            _accountService.AddCustomer(account);
        }

        public void DeleteCustomer(string accNumber)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.AccountNumber == accNumber);
            if(customer == null)
            {
                throw new CustomerNotFoundException("Account does not exists!!!");
            }
            else
            {
                if(customer.Status == AccountStatus.Close)
                {
                    throw new AccountClosedException("Account already closed!!!");
                }
                else
                {
                    customer.Status = AccountStatus.Close;
                    _context.SaveChanges();
                }
            }
            
        }

        public void UpdateCustomer(Customer oldCustomer, Customer newCustomer)
        {
            oldCustomer.ID = newCustomer.ID;
            oldCustomer.Name = newCustomer.Name;
            oldCustomer.Gender = newCustomer.Gender;
            oldCustomer.AccountNumber = newCustomer.AccountNumber;
            oldCustomer.BankIFSCCode = newCustomer.BankIFSCCode;
            oldCustomer.Balance = newCustomer.Balance;
            oldCustomer.Status = newCustomer.Status;
            oldCustomer.Pin = newCustomer.Pin;

            _context.SaveChanges();
        }

        private void AddCurrencies()
        {
            List<Currency> Data = new List<Currency>();

            Data.Add(new Currency()
            {
                Name = "INR",
                ConversionRate = 1
            });

            Data.Add(new Currency()
            {
                Name = "Dollar",
                ConversionRate = 73.92f
            });

            Data.Add(new Currency()
            {
                Name = "Euro",
                ConversionRate = 83.86f
            });

            Data.Add(new Currency()
            {
                Name = "Australian Dollar",
                ConversionRate = 53.10f
            });

            Data.Add(new Currency()
            {
                Name = "British Pound",
                ConversionRate = 100.43f
            });

            _context.Currencies.AddRange(Data);
            _context.SaveChanges();
        }
    }
}
