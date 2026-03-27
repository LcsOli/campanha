using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.Entities.EntitiesConfigurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("PCPEDI", o => o.ExcludeFromMigrations());

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                   .HasColumnName("NUMPED");

            builder.Property(o => o.Quantity)
                   .HasColumnName("QT");

            builder.Property(o => o.Price)
                   .HasColumnName("PVENDA");

            builder.Property(o => o.CustomerId)
                   .HasColumnName("CODCLI");

            builder.Property(o => o.ProductId)
                   .HasColumnName("CODPROD");

            builder.Property(o => o.DateOfSale)
                   .HasColumnName("DATA");
        }
    }
}
