using employee_db.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_db.Services
{
    public class ApplicationDbContext : DbContext
    {
        //constructor for DB - extending to base class
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        // Adding Models>employee_info for migration
        public DbSet<EmployeeInfo> Employee { get; set; }
    }
}
