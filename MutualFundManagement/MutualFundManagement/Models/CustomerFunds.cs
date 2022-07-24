using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MutualFundManagement.Models
{
    public class CustomerFunds
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Customers")]
        public int CustomerId { get; set; }

        [Required]
        public float InvestedAmount { get; set; }

        [Required]
        public float InvestedUnits { get; set; }

        [Required]
        [ForeignKey("MutualFundBank")]
        public int FundId { get; set; }

    }
}
