using Campaign.API.Enums.Role;
using Microsoft.EntityFrameworkCore;
using Campaign.API.Extensions.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.API.Configuration.DataBaseContext.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<Entities.Users.User>
    {
        public void Configure(EntityTypeBuilder<Entities.Users.User> builder)
        {
            builder.ToTable("CF_CAMPANHA_USUARIO");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .HasColumnName("ID");

            builder.Property(t => t.Name)
                   .HasColumnName("NOME")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(t => t.Document)
                   .HasColumnName("CPF")
                   .HasMaxLength(14)
                   .IsRequired();

            builder.Property(t => t.Password)
                   .HasColumnName("SENHA")
                   .HasMaxLength(50)
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
                   .HasForeignKey<Entities.Users.User>(t => t.TeamId);

            builder.HasOne(t => t.Manager)
                   .WithOne()
                   .HasForeignKey<Entities.Users.User>(t => t.ManagerId);
        }
    }
}