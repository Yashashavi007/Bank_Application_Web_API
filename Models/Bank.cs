using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Technovert.Bankapp.Web.API.Enums.BankEnum;

namespace Technovert.Bankapp.Web.API.Models
{
    public class Bank
    {
        [Key]
        [MaxLength(20)]
        public string IFSCCode { get; set; }
        public BranchStatus Status { get; set; } 

        // Navigation Property for Customers
        public ICollection<Customer>? Customers { get; set; } = null;
    }
}
