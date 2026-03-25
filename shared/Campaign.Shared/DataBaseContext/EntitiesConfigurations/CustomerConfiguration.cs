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
        }
    }
}
