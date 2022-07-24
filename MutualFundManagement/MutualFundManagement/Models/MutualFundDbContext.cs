using Microsoft.EntityFrameworkCore;

namespace MutualFundManagement.Models
{
    public class MutualFundDbContext:DbContext
    {
        public MutualFundDbContext()
        {

        }
        public MutualFundDbContext(DbContextOptions<MutualFundDbContext> options) : base(options)
        {

        }

        public virtual DbSet<CustomerFunds> CustomerFunds { get; set; } 
        public virtual DbSet<MutualFundBank> MutualFundBanks { get; set; }  

        public virtual DbSet<Customers>Customers { get; set; }
    }
}
