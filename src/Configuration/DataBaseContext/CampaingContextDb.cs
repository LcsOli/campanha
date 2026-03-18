using Microsoft.EntityFrameworkCore;

namespace Campaign.API.Configuration.DataBaseContext
{
    public class CampaingContextDb(DbContextOptions<CampaingContextDb> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HOMOLOGA");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CampaingContextDb).Assembly);
        }
    }
}
