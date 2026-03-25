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
                   .HasColumnName("CODSUPERVISOR")
                   .IsRequired();

            builder.Property(s => s.Name)
                   .HasColumnName("NOME")
                   .IsRequired();

            builder.HasData(new SellerManager(1, 25, "Neto"),
                            new SellerManager(2, 3, "Bruno"),
                            new SellerManager(3, 11, "Bruno"),
                            new SellerManager(4, 12, "Bruno"),
                            new SellerManager(5, 13, "Bruno"),
                            new SellerManager(6, 33, "Bruno"),
                            new SellerManager(7, 27, "Rodrigo"),
                            new SellerManager(8, 30, "Leandro"),
                            new SellerManager(9, 34, "Leandro"),
                            new SellerManager(10, 14, "Zé Rubens"),
                            new SellerManager(11, 15, "Zé Rubens"),
                            new SellerManager(12, 28, "Zé Rubens"));
        }
    }
}
