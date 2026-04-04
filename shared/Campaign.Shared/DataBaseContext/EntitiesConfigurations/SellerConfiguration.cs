using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.Entities.EntitiesConfigurations
{
    public class SellerConfiguration : IEntityTypeConfiguration<Seller.Seller>
    {
        public void Configure(EntityTypeBuilder<Seller.Seller> builder)
        {
            builder.ToTable("PCUSUARI", s => s.ExcludeFromMigrations());

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnName("CODUSUR");

            builder.Property(s => s.Name)
                   .HasColumnName("NOME");

            builder.Property(s => s.ManagerId)
                   .HasColumnName("CODSUPERVISOR");

            builder.Property(s => s.SellerType)
                   .HasColumnName("TIPOVEND");
        }
    }
}
