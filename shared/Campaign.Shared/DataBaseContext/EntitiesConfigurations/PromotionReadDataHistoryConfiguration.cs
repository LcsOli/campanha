using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class PromotionReadDataHistoryConfiguration : IEntityTypeConfiguration<ProductPromotionReadDataHistory>
    {
        public void Configure(EntityTypeBuilder<ProductPromotionReadDataHistory> builder)
        {
            builder.ToTable("CF_CAMPANHA_PROMOI_HISTORICO_LEITURA");
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("ID");

            builder.Property(p => p.PromotionCode)
                   .HasColumnName("CODPROMOCAO")
                   .IsRequired();

            builder.Property(p => p.ReadAt)
                   .HasColumnName("DATA_LEITURA")
                   .IsRequired();
        }
    }
}
