using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.Entities.EntitiesConfigurations
{
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.ToTable("PCUSUARI");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnName("CODUSUR");

            builder.Property(s => s.Name)
                   .HasColumnName("NOME");
        }
    }
}
