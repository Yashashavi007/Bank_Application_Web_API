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
    public class AccountService : IAccountService
    {
        private readonly BankDbContext _context;
        private readonly ICustomService _customService;


        public AccountService(BankDbContext context, ICustomService customService)
        {
            _context = context;
            _customService = customService;
        }

        // Get Charges

        public float getIntraBankIMPS()
        {
            return _customService.IntraBankIMPS;
        }
        public float getIntraBankRTGS()
        {
            return _customService.IntraBankRTGS;
        }
        public float getInterBankIMPS()
        {
            return _customService.InterBankIMPS;
        }
        public float getInterBankRTGS()
        {
            return _customService.InterBankRTGS;
        }

        public void AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Customer cannot be added!!");
            }
        }
        public Customer CreateCustomerAccount(string IFSCCode, string name, CustomerGender gender, float balance)
        {
            if (balance < 0)
            {
                throw new NegativeBalanceException("Balance cannot be negative!!!");
            }
            else if (balance > 0 && balance < 500)
            {
                throw new MinimumBalanceException("Minimum Balance cannot be less than 500 Rs.!!!");
            }
            else
            {
                string accountNumber = _customService.GenerateAccountNumber();
                int accountPin = _customService.GeneratePin();
                string accountID = _customService.GenerateAccountID(name);

                var bank = _context.Banks.SingleOrDefault(b => b.IFSCCode == IFSCCode && b.Status == Enums.BankEnum.BranchStatus.Open);
                if (bank != null)
                {
                    var account = new Customer()
                    {
                        ID = accountID,
                        Name = name,
                        Gender = gender,
                        AccountNumber = accountNumber,
                        Balance = balance,
                        Status = AccountStatus.Open,
                        Pin = accountPin,
                        BankIFSCCode = IFSCCode
                    };

                    _context.Customers.Add(account);
                    _context.SaveChanges();
                    return _context.Customers.SingleOrDefault(c => c.ID == accountID);
                }
                else
                {
                    throw new BankNotFoundException("Bank does not exists!!!");
                }
            }
        }

        public void AddEmployee(Employee employee)
        {
            if (employee != null)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Employee cannot be added!!!");
            }
        }
        public Employee CreateEmployeeAccount(string IFSCCode, string name, EmployeeGender gender, EmployeeRole role)
        {
            var empId = _customService.GenerateEmployeeID(name);
            int accPin = _customService.GeneratePin();

            var bank = _context.Banks.SingleOrDefault(b => b.IFSCCode == IFSCCode);
            if (bank != null)
            {
                var employee = new Employee()
                {
                    ID = empId,
                    Name = name,
                    Gender = gender,
                    Pin = accPin,
                    Role = role,
                    BankIFSCCode = IFSCCode
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();

                return _context.Employees.SingleOrDefault(e => e.ID == empId);
            }
            else
            {
                throw new BankNotFoundException("Bank does not exist!!!");
            }
        }

        public void UpdateTransactionHistory(string fromId, string toId, TransactionType type, float amount)
        {
            var sender = _context.Customers.SingleOrDefault(s => s.ID == fromId);
            Console.WriteLine(sender);
            var receiver = _context.Customers.SingleOrDefault(r => r.ID == toId);
            Console.WriteLine(receiver);
            string transactionId = _customService.GenerateTransactionID(sender.BankIFSCCode, receiver.ID);

            var transaction = new Transaction()
            {
                Id = transactionId,
                SenderAccountNumber = sender.AccountNumber,
                ReceiverAccountNumber = receiver.AccountNumber,
                Type = type,
                TimeStamp = DateTime.Now,
                Amount = amount,
                CustomerID = sender.ID
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public float GetBalance(string accountNumber)
        {
            var account = _context.Customers.Single(c => c.AccountNumber == accountNumber && c.Status == AccountStatus.Open);
            if(account == null)
            {
                throw new CustomerNotFoundException("Account does not exist!!!");
            }
            return account.Balance;
        }

        public IEnumerable<Transaction> ViewPassbook(string accountNumber)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.AccountNumber == accountNumber && c.Status == AccountStatus.Open);
            if(customer == null)
            {
                throw new CustomerNotFoundException("Account does not exists!!!");
            }
            var passbook = _context.Transactions.Where(t => t.CustomerID == customer.ID).ToList();
            return passbook;
        }
    }
}
