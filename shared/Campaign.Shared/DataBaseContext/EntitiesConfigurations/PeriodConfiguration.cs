using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities.Period;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campaign.Shared.DataBaseContext.EntitiesConfigurations
{
    public class PeriodConfiguration : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
            builder.ToTable("CF_CAMPANHA_PERIODO");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("ID")
                   .IsRequired();

            builder.Property(p => p.Year)
                   .HasColumnName("ANO")
                   .IsRequired();

            builder.Property(p => p.Month)
                   .HasColumnName("MES")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(p => p.Init)
                   .HasColumnName("DTINICIO")
                   .IsRequired();

            builder.Property(p => p.End)
                   .HasColumnName("DTFIM")
                   .IsRequired();
        }
    }
}
