using Microsoft.EntityFrameworkCore;

namespace Campaign.Shared.DataBaseContext.Entities
{
    public class CampaingContextDb(DbContextOptions<CampaingContextDb> options) : DbContext(options)
    {
        public DbSet<Team> Teams { get; private set; }
        public DbSet<Users.User> Users { get; private set; }
        public DbSet<OrderSummary> OrderSummaries { get; private set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HOMOLOGA");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CampaingContextDb).Assembly);
        }
    }
}
