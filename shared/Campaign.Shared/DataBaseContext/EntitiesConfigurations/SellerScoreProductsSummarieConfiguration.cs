using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.Shared.DataBaseContext.Entities.Summaries;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class SellerScoreProductsSummarieConfiguration : IEntityTypeConfiguration<SellerScoreProductsSummarie>
    {
        public void Configure(EntityTypeBuilder<SellerScoreProductsSummarie> builder)
        {
            builder.ToTable("CF_CAMPANHA_RESUMO_RCA_SCORE_PRODUTO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("ID");

            builder.Property(x => x.SellerId)
                   .HasColumnName("RCA_ID");

            builder.Property(x => x.PromotionCode)
                   .HasColumnName("COD_PROMOCAO");

            builder.Property(x => x.Points)
                   .HasColumnName("PONTOS");

            builder.Property(x => x.ProductName)
                   .HasColumnName("PRODUTO_DESCRICAO");
        }
    }
}