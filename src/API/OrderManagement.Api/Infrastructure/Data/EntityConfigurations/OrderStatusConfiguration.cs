using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Infrastructure.Data.EntityConfigurations;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasOne(os => os.Order)
            .WithOne(os => os.OrderStatus)
            .HasForeignKey<OrderStatus>(os => os.OrderId);

        builder.Property(os => os.State)
            .IsRequired();

        builder.Property(os => os.UpdatedAt)
            .IsRequired();
    }
}
