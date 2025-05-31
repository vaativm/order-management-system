namespace OrderManagement.Api.Application.Abstractions;

public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal orderTotal, decimal discountValue);
}
