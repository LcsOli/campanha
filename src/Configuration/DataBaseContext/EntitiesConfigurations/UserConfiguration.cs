using Campaign.API.Enums.Role;
using Microsoft.EntityFrameworkCore;
using Campaign.API.Extensions.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.API.Configuration.DataBaseContext.Entites;

namespace Campaign.API.Configuration.DataBaseContext.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("CF_CAMPANHA_USUARIO");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .HasColumnName("ID");

            builder.Property(t => t.Name)
                   .HasColumnName("NOME")
                   .IsRequired();

            builder.Property(t => t.Document)
                   .HasColumnName("CPF")
                   .IsRequired();

            builder.Property(t => t.Password)
                   .HasColumnName("SENHA")
                   .IsRequired();

            builder.Property(t => t.Roles)
                   .HasColumnName("ROLE")
                   .HasConversion(value => value.GetTranslatedDescription(),
                                  value => value.EnumDescriptionTranslatedToNumericValue<Roles>())
                   .IsRequired();

            builder.Property(t => t.LastAccess)
                   .HasColumnName("ULTIMO_ACESSO")
                   .IsRequired();

            builder.Property(t => t.TeamId)
                   .HasColumnName("EQUIPE_ID")
                   .IsRequired();

            builder.Property(t => t.ManagerId)
                   .HasColumnName("GERENTE_ID");

            builder.HasOne(t => t.Team)
                   .WithOne()
                   .HasForeignKey<User>(t => t.TeamId);
        }
    }
}
