using Campaign.API.Configuration.DataBaseContext.Entites;
using Campaign.API.Enums.Role;
using Campaign.API.Extensions.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
                   .HasConversion(v => v.RoleToDescTranslated(),
                                  v => Enum.Parse<Roles>(v));
        }
    }
}
