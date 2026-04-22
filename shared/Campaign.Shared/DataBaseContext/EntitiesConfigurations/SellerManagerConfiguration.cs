using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities.Seller;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class SellerManagerConfiguration : IEntityTypeConfiguration<SellerManager>
    {
        public void Configure(EntityTypeBuilder<SellerManager> builder)
        {
            builder.ToTable("CF_CAMPANHA_SUPERVISORES");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnName("ID");

            builder.Property(s => s.Code)
                   .HasColumnName("COD_SUPERVISOR")
                   .IsRequired();

            builder.Property(s => s.Name)
                   .HasColumnName("NOME")
                   .IsRequired();

            builder.Property(s => s.TargetRevenue)
                   .HasColumnName("META_FATURAMENTO")
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(s => s.CurrentRevenue)
                   .HasColumnName("FATURAMENTO_MENSAL")
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.HasData(new SellerManager(1, 25, "Neto", 0, 0),
                            new SellerManager(2, 3, "Bruno", 0, 0),
                            new SellerManager(3, 11, "Bruno", 0, 0),
                            new SellerManager(4, 12, "Bruno", 0, 0),
                            new SellerManager(5, 13, "Bruno", 0, 0),
                            new SellerManager(6, 33, "Bruno", 0, 0),
                            new SellerManager(7, 27, "Rodrigo", 0, 0),
                            new SellerManager(8, 30, "Leandro", 0, 0),
                            new SellerManager(9, 34, "Leandro", 0, 0),
                            new SellerManager(10, 14, "Zé Rubens", 0, 0),
                            new SellerManager(11, 15, "Zé Rubens", 0, 0),
                            new SellerManager(12, 28, "Zé Rubens", 0, 0),
                            new SellerManager(13, 9, "Marcia", 0, 0));
        }
    }
}
