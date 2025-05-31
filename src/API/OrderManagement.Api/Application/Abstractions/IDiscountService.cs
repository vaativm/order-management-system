using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Application.Abstractions;

public interface IDiscountService
{
    Task<CustomerSegment> DetermineCustomerSegmentAsync(Guid customerId);
    Task<decimal> CalculateTotalDiscountAsync(Guid orderId);

}
