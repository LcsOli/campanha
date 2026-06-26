using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PCPRODUT");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("CODPROD");

            builder.Property(x => x.Description)
                   .HasColumnName("DESCRICAO");
        }
    }
}
