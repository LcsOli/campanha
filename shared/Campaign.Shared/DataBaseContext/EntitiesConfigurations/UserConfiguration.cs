using Campaign.Shared.Enums.Role;
using Microsoft.EntityFrameworkCore;
using Campaign.Shared.Extensions.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Campaign.Shared.DataBaseContext.Entities.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Users.User>
    {
        public void Configure(EntityTypeBuilder<Users.User> builder)
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
                   .HasColumnName("DOCUMENTO")
                   .HasMaxLength(14)
                   .IsRequired();

            builder.Property(t => t.HashedPassword)
                   .HasColumnName("SENHA_CRIPTOGRAFADA")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(t => t.Roles)
                   .HasColumnName("ROLE")
                   .HasConversion(value => value.GetTranslatedDescription(),
                                  value => value.EnumDescriptionTranslatedToNumericValue<Roles>())
                   .IsRequired();

            builder.Property(t => t.LastAccess)
                   .HasColumnName("ULTIMO_ACESSO");

            builder.Property(t => t.TeamId)
                   .HasColumnName("EQUIPE_ID");

            builder.Property(t => t.SellerId)
                   .HasColumnName("RCA_ID");

            builder.HasOne(t => t.Team)
                   .WithOne()
                   .HasForeignKey<Users.User>(t => t.TeamId);
        }
    }
}