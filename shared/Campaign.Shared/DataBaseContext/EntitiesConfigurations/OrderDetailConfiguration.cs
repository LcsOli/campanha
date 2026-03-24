using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.Entities.EntitiesConfigurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("PCPEDI");

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
        }
    }
}
