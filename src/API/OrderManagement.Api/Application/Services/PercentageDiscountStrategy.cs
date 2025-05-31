using OrderManagement.Api.Application.Abstractions;

namespace OrderManagement.Api.Application.Services;

public class PercentageDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal orderTotal, decimal discountValue)
    {
        return orderTotal * (discountValue / 100);
    }
}
