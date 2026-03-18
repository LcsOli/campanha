using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.API.Configuration.DataBaseContext.Entites;

namespace Campaign.API.Configuration.DataBaseContext.EntitiesConfigurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("CF_CAMPANHA_EQUIPE");

            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Id)
                   .HasColumnName("ID");
            
            builder.Property(t => t.Description)
                   .HasColumnName("NOME");
        }
    }
}
