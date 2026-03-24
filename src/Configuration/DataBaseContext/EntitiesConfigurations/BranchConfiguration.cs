using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Configuration.DataBaseContext.EntitiesConfigurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("PCFILIAL");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                   .HasColumnName("CODIGO")
                   .HasConversion(id => id.ToString(),
                                  id => int.Parse(id));
        }
    }
}
