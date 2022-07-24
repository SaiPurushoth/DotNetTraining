using System.ComponentModel.DataAnnotations;

namespace MutualFundManagement.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}
