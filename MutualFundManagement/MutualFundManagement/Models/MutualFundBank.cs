using System.ComponentModel.DataAnnotations;

namespace MutualFundManagement.Models
{
    public class MutualFundBank
    {
        [Key]
        public int FundId { get; set; }

        [Required]
        public string FundName { get; set; }

        [Required]
        public  float NAV { get; set; }

        [Required]
        public float TotalUnits { get; set; }

        [Required]
        public float TotalInvestment { get; set; }

        public ICollection<CustomerFunds> Customers;


    }
}
