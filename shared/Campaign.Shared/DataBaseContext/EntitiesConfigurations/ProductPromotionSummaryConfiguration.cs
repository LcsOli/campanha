using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class ProductPromotionSummaryConfiguration : IEntityTypeConfiguration<ProductPromotionSummary>
    {
        public void Configure(EntityTypeBuilder<ProductPromotionSummary> builder)
        {
            builder.ToTable("PCPROMOC", p => p.ExcludeFromMigrations());

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("CODPROMOCAO");

            builder.Property(p => p.InitIn)
                   .HasColumnName("DTINICIO");

            builder.Property(p => p.EndIn)
                   .HasColumnName("DTFIM");

            builder.Property(p => p.Description)
                   .HasColumnName("DESCRICAO");
        }
    }
}
