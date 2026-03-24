using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Configuration.DataBaseContext.EntitiesConfigurations
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
