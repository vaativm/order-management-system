using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Application.Abstractions;

public interface IOrderManagementDbContext
{
    DbSet<Promotion> Promotions { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderStatus> OrderStatuses { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
