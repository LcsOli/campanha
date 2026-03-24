using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Configuration.DataBaseContext.EntitiesConfigurations
{
    public class OrderSummaryConfiguration : IEntityTypeConfiguration<OrderSummary>
    {
        public void Configure(EntityTypeBuilder<OrderSummary> builder)
        {
            builder.ToTable("PCPEDC");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                   .HasColumnName("NUMPED");

            builder.Property(o => o.SellerId)
                   .HasColumnName("CODUSUR");

            builder.Property(o => o.BranchId)
                   .HasColumnName("CODFILIAL");

            builder.Property(o => o.DateOfSale)
                   .HasColumnName("DATA");

            builder.Property(o => o.CustomerId)
                   .HasColumnName("CODCLI");

            builder.HasOne(o => o.Seller)
                   .WithOne()
                   .HasForeignKey<OrderSummary>(o => o.SellerId);

            builder.HasOne(o => o.Customer)
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId);

            builder.HasOne(o => o.Branch)
                   .WithOne()
                   .HasForeignKey<OrderSummary>(o => o.BranchId);
        }
    }
}
