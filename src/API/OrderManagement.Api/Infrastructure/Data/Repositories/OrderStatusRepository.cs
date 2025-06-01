using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Infrastructure.Data.Repositories;

public class OrderStatusRepository : IOrderStatusRepository
{
    private readonly OrderManagementDbContext _context;

    public OrderStatusRepository(OrderManagementDbContext context)
    {
        _context = context;
    }

    public async Task<OrderStatus> GetByOrderStatusByIdAsync(Guid orderId)
    {
        return await _context.OrderStatuses.FirstOrDefaultAsync(os => os.OrderId == orderId);
    }

    public async Task UpdateAsync(OrderStatus orderStatus)
    {
        _context.OrderStatuses.Update(orderStatus);
        await _context.SaveChangesAsync();
    }

}
