using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Application.Abstractions;

public interface IOrderStatusService
{
    Task TransitionStateAsync(Guid orderId, OrderState newState);
}
