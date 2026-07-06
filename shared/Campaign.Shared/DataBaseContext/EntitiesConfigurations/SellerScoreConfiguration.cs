using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class SellerScoreConfiguration : IEntityTypeConfiguration<SellerScore>
    {
        public void Configure(EntityTypeBuilder<SellerScore> builder)
        {
            builder.ToTable("CF_CAMPANHA_RCA_SCORE");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnName("ID");

            builder.Property(s => s.SellerId)
                   .HasColumnName("RCA_ID")
                   .IsRequired();

            builder.Property(s => s.Score)
                   .HasColumnName("PONTOS");

            builder.Property(s => s.Name)
                   .HasColumnName("NOME");

            builder.Property(s => s.ManagerName)
                   .HasColumnName("NOME_SUPERVISOR");

            builder.Property(s => s.TeamId)
                   .HasColumnName("EQUIPE_ID")
                   .IsRequired();

            builder.HasOne(s => s.Team)
                   .WithOne()
                   .HasForeignKey<SellerScore>(s => s.TeamId);

            builder.Property(s => s.Coupons)
                   .HasColumnName("CUPONS")
                   .HasDefaultValue(0);

            builder.Property(s => s.CurrentRevenue)
                   .HasColumnName("FATURAMENTO_MENSAL")
                   .HasDefaultValue(0);

            builder.Property(s => s.RevenueTarget)
                   .HasColumnName("META_FATURAMENTO")
                   .HasDefaultValue(0);

            builder.Property(s => s.RevenueMonth1)
                   .HasColumnName("FATURAMENTO_MES_1")
                   .HasDefaultValue(0);

            builder.Property(s => s.RevenueMonth2)
                   .HasColumnName("FATURAMENTO_MES_2")
                   .HasDefaultValue(0);

            builder.Property(s => s.RevenueMonth3)
                   .HasColumnName("FATURAMENTO_MES_3")
                   .HasDefaultValue(0);

            builder.Property(s => s.RevenueMonth4)
                   .HasColumnName("FATURAMENTO_MES_4")
                   .HasDefaultValue(0);

            builder.Property(s => s.RevenueMonth5)
                   .HasColumnName("FATURAMENTO_MES_5")
                   .HasDefaultValue(0);

            builder.Property(s => s.QtyConsumersReactivateds)
                   .HasColumnName("QT_CLIENTES_REATIVADOS")
                   .HasDefaultValue(0);

            builder.Property(s => s.QtyConsumersRegistereds)
                   .HasColumnName("QT_CLIENTES_POSITIVADOS")
                   .HasDefaultValue(0);

            builder.Property(s => s.LastScoreByAccess)
                   .HasColumnName("DT_ULTIMA_PONTUACAO_ACESSO");

            builder.Property(s => s.SellerManagerId)
                   .HasColumnName("COD_SUPERVISOR");


            builder.Property(x => x.ScoreProductsCanceledsOrders)
                   .HasColumnName("PONTOS_PRODUTOS_CANCELADOS");

            builder.Property(x => x.ScoreProductRemovedFromOrders)
                   .HasColumnName("PONTOS_PRODUTOS_CORTADOS");

            builder.Property(s => s.GetPointsByTraining)
                   .HasColumnName("RECEBEU_PONTOS_TREINAMENTO")
                   .HasConversion(x => x ? "S" : "N", x => x == "S");
        }
    }
}
