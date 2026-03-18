using Microsoft.EntityFrameworkCore;
using Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Configuration.DataBaseContext
{
    public class CampaingContextDb(DbContextOptions<CampaingContextDb> options) : DbContext(options)
    {
        public DbSet<User> Users { get; private set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HOMOLOGA");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CampaingContextDb).Assembly);
        }
    }
}
