using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Technovert.Bankapp.Web.API.Enums.CustomerEnum;

namespace Technovert.Bankapp.Web.API.Models
{
    public class Customer
    {
        [Key]
        [MaxLength(100)]
        public string ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public CustomerGender Gender { get; set; }
        [MaxLength(20)]
        public string AccountNumber { get; set; }
        public int Pin { get; set; }
        public float Balance { get; set; }
        public AccountStatus Status { get; set; }

        // Navigation Property for Bank Model
        [MaxLength(20)]
        public string BankIFSCCode { get; set; }
        public Bank? Bank { get; set; } = null;

        // Navigation Property
        public ICollection<Transaction>? Transactions { get; set; } = null;
    }
}
