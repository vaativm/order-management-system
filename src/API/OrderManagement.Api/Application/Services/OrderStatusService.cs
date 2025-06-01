using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Application.Exceptions;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Application.Services;

public class OrderStatusService : IOrderStatusService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly ILogger<OrderStatusService> _logger;

    public OrderStatusService(
        IOrderRepository orderRepository,
        IOrderStatusRepository orderStatusRepository,
        ILogger<OrderStatusService> logger)
    {
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
        _logger = logger;
    }

    public async Task TransitionStateAsync(Guid orderId, OrderState newState)
    {
        OrderStatus orderStatus = await _orderStatusRepository.GetByOrderStatusByIdAsync(orderId)
            ?? throw new NotFoundException($"No status found for order {orderId}");

        try
        {
            if(CanTransitionTo(orderStatus.State, newState))
            {
                orderStatus.State = newState;
                await _orderStatusRepository.UpdateAsync(orderStatus);
            }
            
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, $"Invalid order state transition requested for order {orderId}");
            throw;
        }
    }

    private bool CanTransitionTo(OrderState currentState, OrderState newState) =>
        (currentState, newState) switch
        {
            (OrderState.Created, OrderState.Processing) => true,
            (OrderState.Processing, OrderState.Shipped) => true,
            (OrderState.Shipped, OrderState.Delivered) => true,
            (_, OrderState.Cancelled) => currentState != OrderState.Delivered,
            _ => false
        };

}
