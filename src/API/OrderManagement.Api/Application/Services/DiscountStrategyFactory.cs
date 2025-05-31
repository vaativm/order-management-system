using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Application.Services;

public class DiscountStrategyFactory : IDiscountStrategyFactory
{
    public IDiscountStrategy GetDiscountStrategy(DiscountType discountType)
    {
        return discountType switch
        {
            DiscountType.Percentage => new PercentageDiscountStrategy(),
            _ => throw new ArgumentException($"Invalid discount type: {discountType}")
        };

    }
}
