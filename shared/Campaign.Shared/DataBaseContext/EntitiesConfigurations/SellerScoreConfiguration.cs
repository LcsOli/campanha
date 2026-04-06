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
                   .HasColumnName("USUARI_ID")
                   .IsRequired();

            builder.Property(s => s.Score)
                   .HasColumnName("PONTOS");

            builder.Property(s => s.Name)
                   .HasColumnName("NOME");

            builder.Property(s => s.ManagerName)
                   .HasColumnName("NOME_GERENTE");

            builder.Property(s => s.TeamId)
                   .HasColumnName("EQUIPE_ID");

            builder.HasOne(s => s.Team)
                   .WithOne()
                   .HasForeignKey<SellerScore>(s => s.TeamId);
        }
    }
}
