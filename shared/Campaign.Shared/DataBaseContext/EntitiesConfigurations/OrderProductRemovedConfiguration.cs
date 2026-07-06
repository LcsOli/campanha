using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.Shared.DataBaseContext.Entities.OrderProductRemoved;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class OrderProductRemovedConfiguration : IEntityTypeConfiguration<OrderProductRemoved>
    {
        public void Configure(EntityTypeBuilder<OrderProductRemoved> builder)
        {
            builder.ToTable("PCCORTEI");

            builder.HasKey(x => new
            {
                x.ProductId,
                x.OrderId,
                x.ShipmentNumber,
                x.SequenceNumber
            });

            builder.Property(x => x.ProductId)
                   .HasColumnName("CODPROD");

            builder.Property(x => x.ShipmentNumber)
                   .HasColumnName("NUMCAR");

            builder.Property(x => x.OrderId)
                   .HasColumnName("NUMPED");

            builder.Property(x => x.SequenceNumber)
                   .HasColumnName("NUMSEQ");

            builder.Property(x => x.QtyProductHeld)
                   .HasColumnName("QTSEPARADA");

            builder.Property(x => x.QtyProductRemoved)
                   .HasColumnName("QTCORTADA");

            builder.Property(x => x.SellerId)
                   .HasColumnName("CODUSUR");
        }
    }
}
