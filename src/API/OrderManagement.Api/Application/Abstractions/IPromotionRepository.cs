using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Application.Abstractions;

public interface IPromotionRepository
{
    Task<IEnumerable<Promotion>> GetApplicablePromotionsAsync(CustomerSegment customerSegment);
}
