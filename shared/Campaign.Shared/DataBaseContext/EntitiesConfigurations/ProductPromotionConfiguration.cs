using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Shared.DataBaseContext.Entities.EntitiesConfigurations
{
    public class ProductPromotionConfiguration : IEntityTypeConfiguration<ProductPromotion>
    {
        public void Configure(EntityTypeBuilder<ProductPromotion> builder)
        {
            builder.ToTable("PCPROMOI", p => p.ExcludeFromMigrations());

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("REP_ID");

            builder.Property(p => p.CreateAt)
                   .HasColumnName("REP_DATA");

            builder.Property(p => p.PromotionCode)
                   .HasColumnName("CODPROMOCAO");

            builder.Property(p => p.ProductId)
                   .HasColumnName("CODPROD");

            builder.Property(p => p.QuantityPoints)
                   .HasColumnName("QTPONTOS");

            builder.Property(p => p.QuantityGoals)
                   .HasColumnName("QTMETA");

            builder.Property(p => p.QuantityPointsValue)
                   .HasColumnName("QTPONTOSVALOR");

            builder.Property(p => p.QuantityPointsGoals)
                   .HasColumnName("QTPONTOSCLIENTE");

            builder.Property(p => p.ValuePoints)
                   .HasColumnName("QTPONTOSMETA");

            builder.Property(p => p.MaxPointsValue)
                   .HasColumnName("VLCADAPONTO");

            builder.Property(p => p.QuantityPointsWeight)
                   .HasColumnName("QTMAXPONTO");

            builder.Property(p => p.MinPointsValue)
                   .HasColumnName("QTPONTOSPESO");

            builder.Property(p => p.QuantityMinItem)
                   .HasColumnName("QTMINITEM");

            builder.Property(p => p.QuantityMinWeightPoints)
                   .HasColumnName("QTPESOMINIMOPONTUACAO");
        }
    }
}
