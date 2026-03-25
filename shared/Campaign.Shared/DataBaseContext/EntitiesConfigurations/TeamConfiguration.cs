using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.Entities.EntitiesConfigurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team.Team>
    {
        public void Configure(EntityTypeBuilder<Team.Team> builder)
        {
            builder.ToTable("CF_CAMPANHA_EQUIPE");

            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Id)
                   .HasColumnName("ID")
                   .ValueGeneratedNever();

            builder.Property(t => t.Name)
                   .HasMaxLength(100)
                   .HasColumnName("NOME");

            builder.Property(t => t.Description)
                   .HasMaxLength(100)
                   .HasColumnName("DESCRICAO");
        }
    }
}
