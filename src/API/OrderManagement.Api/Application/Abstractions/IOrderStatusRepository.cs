using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Application.Abstractions;

public interface IOrderStatusRepository
{
    Task<OrderStatus> GetByOrderStatusByIdAsync(Guid orderId);
    Task UpdateAsync(OrderStatus orderStatus);

}
