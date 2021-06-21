using Microsoft.EntityFrameworkCore;

namespace HarborControl.EntityFramework
{
    public class HarborControlContext : DbContext
    {
        public HarborControlContext(DbContextOptions<HarborControlContext> options)
            : base(options)
        {
        }

        public DbSet<Vessel> Vessels { get; set; }
    }
}