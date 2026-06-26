using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.Entities.EntitiesConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer.Customer>
    {
        public void Configure(EntityTypeBuilder<Customer.Customer> builder)
        {
            builder.ToTable("PCCLIENT", c => c.ExcludeFromMigrations());

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("CODCLI");

            builder.Property(c => c.RegisteredAt)
                   .HasColumnName("DTCADASTRO");

            builder.Property(c => c.Name)
                   .HasColumnName("CLIENTE");
        }
    }
}
