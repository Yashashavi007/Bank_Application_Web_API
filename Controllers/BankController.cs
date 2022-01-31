using Microsoft.AspNetCore.Mvc;
using System;
using Technovert.Bankapp.Web.API.Models;
using Technovert.Bankapp.Web.API.Services;

namespace Technovert.Bankapp.Web.API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class BankController : Controller
    {
        private readonly IAdminService _admin;

        public BankController(IAdminService admin)
        {
            _admin = admin;
        }

        [HttpGet]
        public dynamic GetBanks()
        {
            try
            {
                return Ok(_admin.GetBanks());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public dynamic AddBank(Bank bank)
        {
            try
            {
                _admin.CreateBank(bank.IFSCCode, bank.Status);
                return Ok("Bank created successfully!!!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{ifscCode}")]
        public dynamic DeleteBank(string ifscCode)
        {
            try
            {
                _admin.RemoveBank(ifscCode);
                return Ok("Bank removed successfully!!!");
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
        }
    }
}
