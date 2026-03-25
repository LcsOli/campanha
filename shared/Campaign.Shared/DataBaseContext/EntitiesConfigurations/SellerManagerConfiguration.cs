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

            builder.HasData(new SellerManager(25, "Neto"),
                            new SellerManager(3, "Bruno"),
                            new SellerManager(11, "Bruno"),
                            new SellerManager(12, "Bruno"),
                            new SellerManager(13, "Bruno"),
                            new SellerManager(33, "Bruno"),
                            new SellerManager(27, "Rodrigo"),
                            new SellerManager(30, "Leandro"),
                            new SellerManager(34, "Leandro"),
                            new SellerManager(14, "Zé Rubens"),
                            new SellerManager(15, "Zé Rubens"),
                            new SellerManager(28, "Zé Rubens"));
        }
    }
}
