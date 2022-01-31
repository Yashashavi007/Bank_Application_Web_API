using System;
using System.ComponentModel.DataAnnotations;
using static Technovert.Bankapp.Web.API.Enums.TransactionEnum;

namespace Technovert.Bankapp.Web.API.Models
{
    public class Transaction
    {
        [Key]
        [MaxLength(100)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string SenderAccountNumber { get; set; }
        [MaxLength(100)]
        public string ReceiverAccountNumber { get; set; }
        public TransactionType Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public float Amount { get; set; }

        // Navigation Property for Customer

        public string CustomerID { get; set; }
        public Customer? Customer { get; set; } = null;

    }
}
