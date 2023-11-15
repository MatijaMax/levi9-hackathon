using FibaCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace FibaInfrastructure
{
    public class FibaContext : DbContext
    {
        public FibaContext(DbContextOptions<FibaContext> options) : base(options)
        {
        }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
