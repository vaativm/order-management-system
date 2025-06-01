using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Infrastructure.Data.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasData(
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId=Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                TotalAmount = 5000,
            },
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                TotalAmount = 10000
            }
        );
    }
}
