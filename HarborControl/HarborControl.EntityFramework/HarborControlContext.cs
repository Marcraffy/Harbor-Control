using HarborControl.Interfaces.Vessels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HarborControl.EntityFramework
{
    public class HarborControlContext : DbContext
    {
        private readonly string connectionString;

        public HarborControlContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("SQL");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<IVessel> Vessels { get; set; }
    }
}