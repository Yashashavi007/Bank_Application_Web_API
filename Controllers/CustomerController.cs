using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Technovert.Bankapp.Web.API.Models;
using Technovert.Bankapp.Web.API.Services;

namespace Technovert.Bankapp.Web.API.Controllers
{
    [Route("Bank/[Controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customer;
        private readonly IAccountService _account;

        public CustomerController(ICustomerService customer, IAccountService account)
        {
            _customer = customer;
            _account = account;
        }

        // Http Services
        [HttpPost]
        [Route("Deposit")]
        public dynamic Deposit(string accountNumber, float amount)
        {
            try
            {
                _customer.Deposit(accountNumber, amount);
                return Ok($"Deposited {amount} successfully!!!!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Withdraw")]
        public dynamic Withdraw(string accountNumber, float amount)
        {
            try
            {
                _customer.Withdraw(accountNumber, amount);
                return Ok($"Withdrawal {amount} Successfull!!!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return BadRequest("Withdrawal Unsuccessfull!!");
        }

        [HttpPost]
        [Route("Transfer")]
        public dynamic Transfer(string sender, string receiver, float amount)
        {
            try
            {
                _customer.Transfer(sender, receiver, amount);
                return Ok("Transfer Successfull!!!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return BadRequest("Transfer Unsuccessfull!!!");
        }

        [HttpGet]
        [Route("GetBalance/{accountNumber}")]
        public dynamic BalanceCheck(string accountNumber)
        {
            try
            {
                return _account.GetBalance(accountNumber);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return BadRequest("Couldn't get the Balance!!!");
        }

        [HttpGet]
        [Route("PrintPassbook/{accountNumber}")]
        public IEnumerable<Transaction> GetPassbook(string accountNumber)
        {
            try
            {
                return _account.ViewPassbook(accountNumber);                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
