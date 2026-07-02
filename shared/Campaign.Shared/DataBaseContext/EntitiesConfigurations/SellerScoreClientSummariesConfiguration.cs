using Microsoft.EntityFrameworkCore;
using Campaign.Shared.Extensions.Enums;
using Campaign.Shared.Enums.SellerScoreConsumerType;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class SellerScoreClientSummariesConfiguration : IEntityTypeConfiguration<SellerScoreClientsSummary>
    {
        public void Configure(EntityTypeBuilder<SellerScoreClientsSummary> builder)
        {
            builder.ToTable("CF_CAMPANHA_RESUMO_RCA_SCORE_CLIENTES");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("ID");

            builder.Property(x => x.SellerId)
                   .HasColumnName("RCA_ID");

            builder.Property(x => x.PromotionCode)
                   .HasColumnName("COD_PROMOCAO");

            builder.Property(x => x.Score)
                   .HasColumnName("PONTOS");

            builder.Property(x => x.CustomerId)
                   .HasColumnName("COD_CLIENTE");

            builder.Property(x => x.RegisteredIn)
                   .HasColumnName("DATA_POSITIVACAO");

            builder.Property(x => x.ReactivatedIn)
                   .HasColumnName("DATA_REATIVACAO");

            builder.Property(x => x.CustomerSalesEventType)
                   .HasColumnName("TIPO_EVENTO")
                   .HasConversion(value => value.GetTranslatedDescription(), 
                                  value => value.EnumDescriptionTranslatedToNumericValue<CustomerSalesEventType>());
        }
    }
}
