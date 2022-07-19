using Microsoft.EntityFrameworkCore;

namespace Backend_Api.Models
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : base(options)
        {

        }
        public DbSet<Employees> Employees { get; set; }
    }
}
