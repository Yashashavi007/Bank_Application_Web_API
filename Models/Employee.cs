using System.ComponentModel.DataAnnotations;
using static Technovert.Bankapp.Web.API.Enums.EmployeeEnum;

namespace Technovert.Bankapp.Web.API.Models
{
    public class Employee
    {
        [Key]
        [MaxLength(100)]
        public string ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public EmployeeGender Gender { get; set; }
        public EmployeeRole Role { get; set; }
        public int Pin { get; set; }

        //Navigation Property for Bank
        public string BankIFSCCode { get; set; }
        public virtual Bank? Bank { get; set; } = null;
    }
}
