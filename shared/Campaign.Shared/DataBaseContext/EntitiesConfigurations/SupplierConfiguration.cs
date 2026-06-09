using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities.Supplier;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("PCFORNEC");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("CODFORNEC");

            builder.Property(x => x.Name)
                .HasColumnName("FORNECEDOR");
            
            builder.Property(x => x.Document)
                .HasColumnName("CGC");
        }
    }
}
