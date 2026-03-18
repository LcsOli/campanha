using Campaign.API.Extensions.Role;
using Microsoft.EntityFrameworkCore;
using Campaign.API.Extensions.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.API.Configuration.DataBaseContext.Entites;
using Campaign.API.Enums.Role;

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
                   .HasColumnName("NOME");

            builder.Property(t => t.Document)
                   .HasColumnName("CPF");

            builder.Property(t => t.Password)
                   .HasColumnName("SENHA");

            builder.Property(t => t.Roles)
                   .HasColumnName("ROLE")
                   .HasConversion(value => value.GetTranslatedDescription(), 
                                  value => value.EnumDescriptionTranslatedToNumericValue<Roles>());

        }
    }
}
