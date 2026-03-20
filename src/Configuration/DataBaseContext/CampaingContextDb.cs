using Microsoft.EntityFrameworkCore;

namespace Campaign.API.Configuration.DataBaseContext
{
    public class CampaingContextDb(DbContextOptions<CampaingContextDb> options) : DbContext(options)
    {
        public DbSet<Entities.Team> Teams { get; private set; }
        public DbSet<Entities.Users.User> Users { get; private set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HOMOLOGA");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CampaingContextDb).Assembly);
        }
    }
}
