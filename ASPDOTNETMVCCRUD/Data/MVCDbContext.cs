using ASPDOTNETMVCCRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPDOTNETMVCCRUD.Data
{
    public class MVCDbContex : DbContext
    {
        public MVCDbContex(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
    }
}
