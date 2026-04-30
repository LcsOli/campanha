using Microsoft.EntityFrameworkCore;

namespace Campaign.Shared.DataBaseContext.Entities
{
    public class CampaingContextDb(DbContextOptions<CampaingContextDb> options) : DbContext(options)
    {
        public DbSet<Team.Team> Teams { get; private set; }
        public DbSet<Users.User> Users { get; private set; }
        public DbSet<Period.Period> Periods { get; private set; }
        public DbSet<Seller.Seller> Sellers { get; private set; }
        public DbSet<Branch.Branch> Branches { get; private set; }
        public DbSet<Customer.Customer> Customers { get; private set; }
        public DbSet<Order.OrderDetail> OrderDetails { get; private set; }
        public DbSet<Seller.SellerScore> SellerScores { get; private set; }
        public DbSet<Order.OrderSummary> OrderSummaries { get; private set; }
        public DbSet<Seller.SellerManagerScore> SellerManagers { get; private set; }
        public DbSet<Product.ProductPromotion> ProductPromotions { get; private set; }
        public DbSet<Product.ProductPromotionSummary> ProductPromotionSummaries { get; private set; }
        public DbSet<Product.ProductPromotionReadDataHistory> ProductPromotionReadDataHistories { get; private set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("MATHEUS");
            modelBuilder.HasDefaultSchema("COMPREFACIL");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CampaingContextDb).Assembly);
        }
    }
}
