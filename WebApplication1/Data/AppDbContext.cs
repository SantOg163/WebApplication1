using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Departaments> Departaments { get; set;}
        public DbSet<Employees> Employees { get; set;}
    }
}
