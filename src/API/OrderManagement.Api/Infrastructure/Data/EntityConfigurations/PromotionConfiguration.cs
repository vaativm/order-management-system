using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Infrastructure.Data.EntityConfigurations;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()    
            .HasMaxLength(100);

        builder.Property(p => p.Value)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.Type)
           .IsRequired()
           .HasConversion<string>();

        builder.Property(p => p.CustomerSegment)
            .IsRequired()
            .HasConversion<string>(); 

        builder.Property(p => p.ValidFrom)
            .IsRequired();

        builder.Property(p => p.ValidTo)
            .IsRequired(false);

        builder.HasIndex(p => p.CustomerSegment);
        builder.HasIndex(p => p.ValidFrom);

        builder.HasData(
            new Promotion
            {
                Id=Guid.NewGuid(),
                Name="Vuka",
                Type=DiscountType.Percentage,
                Value=10,
                CustomerSegment= CustomerSegment.New,
                ValidFrom=DateTime.UtcNow
            }
            );
    }
}
