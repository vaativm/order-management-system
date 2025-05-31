using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Application.Abstractions;

public interface IDiscountStrategyFactory
{
    IDiscountStrategy GetDiscountStrategy(DiscountType discountType);
}
