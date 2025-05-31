using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Infrastructure.Data.Repositories;

public class PromotionRepository : IPromotionRepository
{
    private readonly OrderManagementDbContext _context;

    public PromotionRepository(OrderManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Promotion>> GetApplicablePromotionsAsync(CustomerSegment customerSegment)
    {
        return await _context.Promotions
            .Where(p => p.CustomerSegment == customerSegment && p.ValidFrom <= DateTime.UtcNow && (p.ValidTo == null || p.ValidTo > DateTime.UtcNow))
            .ToListAsync();
    }

}
