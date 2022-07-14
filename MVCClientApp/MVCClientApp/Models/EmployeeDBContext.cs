using Microsoft.EntityFrameworkCore;
namespace MVCClientApp.Models
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)   
        {
                
        }
        public DbSet<EmployeeDetail> Employees { get; set; }


    }
}
