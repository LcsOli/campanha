using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Configuration.DataBaseContext.EntitiesConfigurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("CF_CAMPANHA_EQUIPE");

            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Id)
                   .HasColumnName("ID")
                   .ValueGeneratedNever();

            builder.Property(t => t.Description)
                   .HasMaxLength(100)
                   .HasColumnName("NOME");
        }
    }
}
