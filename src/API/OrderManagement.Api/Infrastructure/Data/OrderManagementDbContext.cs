using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Domain.Entities;
using OrderManagement.Api.Infrastructure.Data.EntityConfigurations;

namespace OrderManagement.Api.Infrastructure.Data;

public class OrderManagementDbContext : DbContext, IOrderManagementDbContext
{
    public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options) 
    :base(options){ }

    public DbSet<Promotion> Promotions { get; set ; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PromotionConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
    }
}
