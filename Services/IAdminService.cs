using System.Collections.Generic;
using Technovert.Bankapp.Web.API.Enums;
using Technovert.Bankapp.Web.API.Models;

namespace Technovert.Bankapp.Web.API.Services
{
    public interface IAdminService
    {
        IEnumerable<Bank> GetBanks();
        void CreateBank(string ifscCode, BankEnum.BranchStatus status);
        void RemoveBank(string ifscCode);
    }
}