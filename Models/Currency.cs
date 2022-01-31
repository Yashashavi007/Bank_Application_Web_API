using System.ComponentModel.DataAnnotations;

namespace Technovert.Bankapp.Web.API.Models
{
    public class Currency
    {
        [Key]
        public string Name { get; set; }
        public float ConversionRate { get; set; }
    }
}
