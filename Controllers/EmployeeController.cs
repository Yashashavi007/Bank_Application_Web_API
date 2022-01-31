using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Technovert.Bankapp.Web.API.Models;
using Technovert.Bankapp.Web.API.Services;

namespace Technovert.Bankapp.Web.API.Controllers
{
    [Route("Bank/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employee;
        private readonly IAccountService _account;

        public EmployeeController(IEmployeeService employee, IAccountService account)
        {
            _employee = employee;
            _account = account;
        }

        //HTTP Methods

        [HttpGet]
        public dynamic Get()
        {
            IEnumerable<Customer> customers = _employee.GetAllCustomers();
            return Ok(customers);
        }
        
        [HttpGet("{accNumber}")]
        public dynamic Get(string accNumber)
        {
            try
            {
                return Ok(_employee.GetCustomer(accNumber));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public dynamic Post(Customer newCustomer)
        {
            try
            {
                _account.CreateCustomerAccount(newCustomer.BankIFSCCode, newCustomer.Name, newCustomer.Gender, newCustomer.Balance);
                return Ok("Customer successfully added!!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{accNum}")]
        public dynamic Put(string accNum, Customer newCustomer)
        {
            Customer oldCustomer = _employee.GetCustomer(accNum);
            if (oldCustomer == null)
            {
                return NotFound("Customer not found!!");
            }
            _employee.UpdateCustomer(oldCustomer, newCustomer);
            return Ok("Update successfull!!");
        }

        [HttpDelete("{accNum}")]
        public dynamic Delete(string accNum)
        {
            try
            {
                _employee.DeleteCustomer(accNum);
                return Ok("Account closed successfully!!!");
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
