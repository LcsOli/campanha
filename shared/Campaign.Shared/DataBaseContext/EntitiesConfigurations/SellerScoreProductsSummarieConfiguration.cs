using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class SellerScoreProductsSummarieConfiguration : IEntityTypeConfiguration<SellerScoreProductsSummary>
    {
        public void Configure(EntityTypeBuilder<SellerScoreProductsSummary> builder)
        {
            builder.ToTable("CF_CAMPANHA_RESUMO_RCA_SCORE_PRODUTO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("ID");

            builder.Property(x => x.SellerId)
                   .HasColumnName("RCA_ID");

            builder.Property(x => x.PromotionCode)
                   .HasColumnName("COD_PROMOCAO");

            builder.Property(x => x.Score)
                   .HasColumnName("PONTOS");

            builder.Property(x => x.ProductId)
                   .HasColumnName("COD_PRODUTO");

            builder.Property(x => x.CustomerId)
                   .HasColumnName("COD_CLIENTE");
        }
    }
}