using System;
using System.Collections.Generic;
using System.Linq;
using Technovert.Bankapp.Web.API.Exceptions;
using Technovert.Bankapp.Web.API.Models;
using static Technovert.Bankapp.Web.API.Enums.BankEnum;

namespace Technovert.Bankapp.Web.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly BankDbContext _context;
        public AdminService(BankDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Bank> GetBanks()
        {
            return _context.Banks.Where(b => b.Status == BranchStatus.Open).ToList();
        }
        public void CreateBank(string ifscCode, BranchStatus status)
        {
            var bankCheck = _context.Banks.FirstOrDefault(b => b.IFSCCode == ifscCode);
            if (bankCheck == null)
            {
                var bank = new Bank()
                {
                    IFSCCode = ifscCode,
                    Status = status
                };

                _context.Banks.Add(bank);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Bank already exists!!!");
            }

        }

        public void RemoveBank(string ifscCode)
        {
            var bank = _context.Banks.FirstOrDefault(b => b.IFSCCode == ifscCode);
            if (bank != null)
            {
                bank.Status = BranchStatus.Closed;
                _context.SaveChanges();
            }
            else
            {
                throw new BankNotFoundException("Bank does not exist!!!");
            }
        }
    }
}
