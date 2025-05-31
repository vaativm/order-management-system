using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Application.Exceptions;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Application.Services;

public class DiscountService : IDiscountService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPromotionRepository _promotionRepository;
    private readonly IDiscountStrategyFactory _discountStrategyFactory;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(
        IOrderRepository orderRepository,
        IPromotionRepository promotionRepository,
        IDiscountStrategyFactory discountStrategyFactory,
        ILogger<DiscountService> logger)
    {
        _orderRepository = orderRepository;
        _promotionRepository = promotionRepository;
        _discountStrategyFactory = discountStrategyFactory;
        _logger = logger;
    }

    public async Task<decimal> CalculateTotalDiscountAsync(Guid orderId)
    {
        Order order = await _orderRepository.GetByIdAsync(orderId)
            ?? throw new NotFoundException($"Order {orderId} not found");

        CustomerSegment customerSegment = await DetermineCustomerSegmentAsync(order.CustomerId);
        IEnumerable<Promotion> promotions = await _promotionRepository.GetApplicablePromotionsAsync(customerSegment);
        decimal totalDiscount = 0m;

        foreach (Promotion promotion in promotions)
        {
            if (totalDiscount > 0)
            {
                continue;
            }

            IDiscountStrategy strategy = _discountStrategyFactory.GetDiscountStrategy(promotion.Type);
            decimal discount = strategy.ApplyDiscount(order.TotalAmount, promotion.Value);
            totalDiscount += discount;

            _logger.LogInformation(
                "Applied promotion {PromotionId} to order {OrderId}. Discount: {Amount}",
                promotion.Id, orderId, discount);
        }

        return totalDiscount;
    }

    public async Task<CustomerSegment> DetermineCustomerSegmentAsync(Guid customerId)
    {
        IEnumerable<Order> orderHistory = await _orderRepository.GetCustomerOrderHistoryAsync(customerId);

        decimal totalSpend = orderHistory.Sum(o => o.TotalAmount);
        int orderCount = orderHistory.Count();

        return (totalSpend, orderCount) switch
        {
            ( > 1000, > 10) => CustomerSegment.VIP,
            ( > 500, > 5) => CustomerSegment.Regular,
            _ => CustomerSegment.New
        };

    }
}
