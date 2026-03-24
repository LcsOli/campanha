using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.Entities.EntitiesConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("PCCLIENT");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("CODCLI");
        }
    }
}
